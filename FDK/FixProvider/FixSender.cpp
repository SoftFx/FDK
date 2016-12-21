#include "stdafx.h"
#include "FixSender.h"

namespace
{
    const string cInvalidSide = "Unsupported trade record side.";
    const string cInvalidType = "Unsupported trade record type.";
    const string cInvalidPriceType = "Unsupported price type.";
    const string cInvalidReportType = "Unsupported report type.";
    const string cInvalidGraphType = "Unsupported graph type.";
    const string cSessionIsNotFound = "Session is not found.";
    const string cInvalidTimeDirection = "Invalid time direction.";
}

namespace
{
    const int cClientQuoteHistoryVersion = 1;

    void FillSymbols(const vector<string>& symbols, FIX44::MarketDataRequest &message)
    {
        message.SetNoMDEntryTypes(1);
        FIX44::MarketDataRequest::NoMDEntryTypes entryType;
        entryType.SetMDEntryType(FIX::MDEntryType_TRADE);
        message.addGroup(entryType);


        message.SetNoRelatedSym(static_cast<int32>(symbols.size()));

        foreach(const auto& element, symbols)
        {
            FIX44::MarketDataRequest::NoRelatedSym symbol;
            symbol.SetSymbol(element);
            message.addGroup(symbol);
        }
    }

    string& StdToUtf8(string& dest, const wstring& src)
    {
        if (! src.length())
        {
            dest = "";
            return dest;
        }

        int result = WideCharToMultiByte(CP_UTF8, 0, src.data(), (int) src.length(), 0, 0, NULL, NULL);

        if (! result)
            throw logic_error("Invalid string to convert to UTF-8");

        dest.resize(result);

        result = WideCharToMultiByte(CP_UTF8, 0, src.data(), (int) src.length(), const_cast<char*>(dest.data()), (int) dest.length(), NULL, NULL);

        if (! result)
            throw logic_error("Invalid string to convert to UTF-8");

        return dest;
    }
}

CFixSender::CFixSender()
{
}

void CFixSender::SessionID(const FIX::SessionID& sessionId)
{
    m_sessionID = sessionId;
}

void CFixSender::SendVersion(const CFixVersion& version)
{
    m_version = version;
}

void CFixSender::AppId(const std::string& appId)
{
    m_appId = appId;
}

#ifdef LOG_PERFORMANCE
void CFixSender::setLogger(Performance::Logger* logger)
{
    logger_ = logger;
}
#endif

void CFixSender::SendMessage(FIX::Message& message)
{
    bool status = false;

    try
    {
        status = FIX::Session::sendToTarget(message, m_sessionID);
    }
    catch (const FIX::SessionNotFound&)
    {
    }

    if (!status)
    {
        throw CSendException(cSessionIsNotFound);
    }
}

void CFixSender::VSendTwoFactorResponse(const FxTwoFactorReason reason, const std::string& otp)
{
    FIX44::TwoFactorLogon message;
    message.SetTwoFactorReason('0' + (char)reason);
    if (!otp.empty())
        message.SetOneTimePassword(otp);
    return SendMessage(message);
}

void CFixSender::VSendGetCurrencies(const string& id)
{
    FIX44::CurrencyListRequest message;
    message.SetCurrencyReqID(id);
    message.SetCurrencyListRequestType(FIX::CurrencyListRequestType_ALLCURRENCIES);
    return SendMessage(message);
}

void CFixSender::VSendGetSupportedSymbols(const string& id)
{
    FIX44::SecurityListRequest message;
    message.SetSecurityReqID(id);
    message.SetSecurityListRequestType(FIX::SecurityListRequestType_SYMBOL);
    return SendMessage(message);
}

void CFixSender::VSendGetSessionInfo(const string& id)
{
    FIX44::TradingSessionStatusRequest message;
    message.SetTradSesReqID(id);
    message.SetSubscriptionRequestType(FIX::SubscriptionRequestType_SNAPSHOT);

    return SendMessage(message);
}

void CFixSender::VSendGetAccountInfo(const string& id)
{
    FIX44::AccountInfoRequest message;
    message.SetAcInfReqID(id);
    return SendMessage(message);
}

void CFixSender::VSendSubscribeToQuotes(const string& id, const vector<string>& symbols, int32 depth)
{
    FIX44::MarketDataRequest message;

    message.SetMDReqID(id);
    message.SetSubscriptionRequestType(FIX::SubscriptionRequestType_SNAPSHOT_PLUS_UPDATES);
    message.SetMarketDepth(depth);
    message.SetMDUpdateType(FIX::MDUpdateType_FULL_REFRESH);

    FillSymbols(symbols, message);

    return SendMessage(message);
}

void CFixSender::VSendUnsubscribeQuotes(const string& id, const vector<string>& symbols)
{
    FIX44::MarketDataRequest message;

    message.SetMDReqID(id);
    message.SetSubscriptionRequestType(FIX::SubscriptionRequestType_UNSUBSCRIBE);
    message.SetMarketDepth(0);

    FillSymbols(symbols, message);

    return SendMessage(message);
}

void CFixSender::VSendDeleteOrder(const string& id, const string& orderID, const string& clientID, int32 side)
{
    FIX44::OrderCancelRequest message;
    if (!orderID.empty())
    {
        message.SetOrderID(orderID);
    }
    if (!clientID.empty())
    {
        message.SetOrigClOrdID(clientID);
    }
    message.SetClOrdID(id);
    if (FxTradeRecordSide_Buy == side)
    {
        message.SetSide(FIX::Side_BUY);
    }
    else if (FxTradeRecordSide_Sell == side)
    {
        message.SetSide(FIX::Side_SELL);
    }
    else
    {
        throw CArgumentException(cInvalidSide);
    }
    FIX::UtcTimeStamp utcNow;
    message.SetTransactTime(utcNow);

    if (m_version.SupportsAppId())
    {
        if (!m_appId.empty())
            message.SetClAppID(m_appId);
    }

    return SendMessage(message);
}

void CFixSender::VSendCloseOrder(const string& id, const string& orderId, Nullable<double> closingVolume)
{
    FIX44::ClosePositionRequest message;
    message.SetClosePosReqID(id);
    if (!orderId.empty())
    {
        message.SetOrderID(orderId);
    }
    if (closingVolume.HasValue())
    {
        message.SetQuantity(*closingVolume);
    }
    message.SetPosCloseType(FIX::PosCloseType_CLOSE);

    if (m_version.SupportsAppId())
    {
        if (!m_appId.empty())
            message.SetClAppID(m_appId);
    }

    return SendMessage(message);
}

void CFixSender::VSendCloseByOrders(const string& id, const string& firstOrderId, const string& secondOrderId)
{
    FIX44::ClosePositionRequest message;
    message.SetClosePosReqID(id);
    if (!firstOrderId.empty())
    {
        message.SetOrderID(firstOrderId);
    }
    if (!secondOrderId.empty())
    {
        message.SetSecondaryOrderID(secondOrderId);
    }
    message.SetPosCloseType(FIX::PosCloseType_CLOSE_BY);

    if (m_version.SupportsAppId())
    {
        if (!m_appId.empty())
            message.SetClAppID(m_appId);
    }

    return SendMessage(message);
}

void CFixSender::VSendCloseAllOrders(const string& id)
{
    FIX44::ClosePositionRequest message;
    message.SetClosePosReqID(id);
    message.SetPosCloseType(FIX::PosCloseType_CLOSE_ALL);

    if (m_version.SupportsAppId())
    {
        if (!m_appId.empty())
            message.SetClAppID(m_appId);
    }

    return SendMessage(message);
}

void CFixSender::VSendGetOrders(const string& id)
{
    FIX44::OrderMassStatusRequest message;
    message.SetMassStatusReqID(id);
    message.SetMassStatusReqType(FIX::MassStatusReqType_STATUS_FOR_ALL_ORDERS);
    return SendMessage(message);
}

void CFixSender::VSendOpenNewOrder(const string& id, const CFxOrder& required)
{
    FIX44::NewOrderSingle message;
    message.SetClOrdID(id);

    if (FxTradeRecordType_Market == required.Type)
    {
        message.SetPrice(required.Price);
        message.SetOrdType(FIX::OrdType_MARKET);
    }
    else if (FxTradeRecordType_Limit == required.Type || FxTradeRecordType_IoC == required.Type || FxTradeRecordType_MarketWithSlippage == required.Type)
    {
        message.SetPrice(required.Price);
        message.SetOrdType(FIX::OrdType_LIMIT);
    }
    else if (FxTradeRecordType_Stop == required.Type)
    {
        message.SetStopPx(required.Price);
        message.SetOrdType(FIX::OrdType_STOP);
    }
    else if (FxTradeRecordType_StopLimit == required.Type)
    {
        message.SetOrdType(FIX::OrdType_STOP_LIMIT);
        message.SetPrice(required.Price);
        if (required.StopPrice.HasValue())
            message.SetStopPx(required.StopPrice.Value());
    }
    else
    {
        throw CArgumentException(cInvalidType);
    }
    message.SetSymbol(required.Symbol);
    if(FxTradeRecordSide_Buy == required.Side)
    {
        message.SetSide(FIX::Side_BUY);
    }
    else if (FxTradeRecordSide_Sell == required.Side)
    {
        message.SetSide(FIX::Side_SELL);
    }
    else
    {
        throw CArgumentException(cInvalidSide);
    }
    message.SetOrderQty(required.Volume);

    if (required.HiddenVolume.HasValue())
    {
        message.SetHiddenQty(required.HiddenVolume.Value());
    }

    if (required.TakeProfit.HasValue())
    {
        message.SetTakeProfit(*required.TakeProfit);
    }
    if (required.StopLoss.HasValue())
    {
        message.SetStopLoss(*required.StopLoss);
    }

    if (required.Expiration.HasValue())
    {
        message.SetTimeInForce(FIX::TimeInForce_GOOD_TILL_DATE);
        FIX::UtcTimeStamp utc(*required.Expiration);
        message.SetExpireTime(utc);
    }
    else
    {
        if (FxTradeRecordType_IoC == required.Type)
            message.SetTimeInForce(FIX::TimeInForce_IMMEDIATE_OR_CANCEL);
        else
            message.SetTimeInForce(FIX::TimeInForce_GOOD_TILL_CANCEL);
    }

    FIX::UtcTimeStamp now;
    message.SetTransactTime(now);

    if (!required.Comment.empty())
    {
        string encodedComment;
        StdToUtf8(encodedComment, required.Comment);
        message.SetEncodedCommentLen((int) encodedComment.length());
        message.SetEncodedComment(encodedComment);
    }

    if (!required.Tag.empty())
    {
        string encodedTag;
        StdToUtf8(encodedTag, required.Tag);
        message.SetEncodedTagLen((int) encodedTag.length());
        message.SetEncodedTag(encodedTag);
    }

    if (required.Magic.HasValue())
        message.SetMagic(*required.Magic);

    if (m_version.SupportsMarketWithSlippage())
    {
        if (FxTradeRecordType_IoC == required.Type)
            message.SetImmediateOrCancelFlag(FIX::ImmediateOrCancelFlag_YES);

        if (FxTradeRecordType_MarketWithSlippage == required.Type)
            message.SetMarketWithSlippageFlag(FIX::MarketWithSlippageFlag_YES);
    }

    if (m_version.SupportsAppId())
    {
        if (! m_appId.empty())
            message.SetClAppID(m_appId);
    }
    
#ifdef LOG_PERFORMANCE
    uint64_t timestamp = logger_->getTimestamp();
    logger_->logTimestamp(id.c_str(), timestamp, "NewOrder");
#endif

    return SendMessage(message);
}

void CFixSender::VSendDataHistoryRequest(const string& id, const CFxDataHistoryRequest& request)
{
    FIX44::MarketDataHistoryRequest message;
    message.SetMarketHistReqID(id);
    message.SetSymbol(request.GetSymbol());


    FIX::UtcTimeStamp time(request.Time);
    message.SetHstTo(time);
    if (0 != request.BarsNumber)
    {
        message.SetHstReqBars(-request.BarsNumber);
    }
    // price type
    if (FxPriceType_Bid == request.PriceType)
    {
        message.SetForexPriceType(FIX::ForexPriceType_BID);
    }
    else if (FxPriceType_Ask == request.PriceType)
    {
        message.SetForexPriceType(FIX::ForexPriceType_ASK);
    }
    else if (FxPriceType_None != request.PriceType)
    {
        throw CSendException(cInvalidReportType);
    }
    // graph period
    if (!request.GraphPeriod.empty())
    {
        message.SetHstGraphPeriodID(request.GetPeriod());
    }
    // report type
    if (FX_REPORT_TYPE_GROUPS == request.ReportType)
    {
        message.SetHstReportType(FIX::HstReportType_FIXGROUPS);
    }
    else if (FX_REPORT_TYPE_BINARY == request.ReportType)
    {
        message.SetHstReportType(FIX::HstReportType_BINDATA);
    }
    else if (FX_REPORT_TYPE_FILE == request.ReportType)
    {
        message.SetHstReportType(FIX::HstReportType_FILE);
    }
    else
    {
        throw CArgumentException(cInvalidReportType);
    }

    // graph type
    if (FX_GRAPH_TYPE_BARS == request.GraphType)
    {
        message.SetHstGraphType(FIX::HstGraphType_BARS);
    }
    else if(FX_GRAPH_TYPE_TICKS == request.GraphType)
    {
        message.SetHstGraphType(FIX::HstGraphType_TICKS);
    }
    else if (FX_GRAPH_TYPE_LEVEL2 == request.GraphType)
    {
        message.SetHstGraphType(FIX::HstGraphType_TICKS_LEVEL2);
    }
    else
    {
        throw CSendException(cInvalidGraphType);
    }
    return SendMessage(message);
}

void CFixSender::VSendGetTradeHistory(const string& id, const Nullable<CDateTime>& from, const Nullable<CDateTime>& to)
{
    FIX44::TradeCaptureReportRequest message;
    message.SetTradeRequestID(id);
    message.SetTradeRequestType(TradeRequestType_ALL_TRADES);
    if (from.HasValue())
    {
        message.SetHstFrom(*from);
    }
    if (to.HasValue())
    {
        message.SetHstTo(*to);
    }
    return SendMessage(message);
}

void CFixSender::VSendModifyOrder(const string& id, const CFxOrder& request)
{
    FIX44::OrderCancelReplaceRequest message;

    if (!request.OrderId.empty())
    {
        message.SetOrderID(request.OrderId);
    }
    if (!request.ClientOrderId.empty())
    {
        message.SetOrigClOrdID(request.ClientOrderId);
    }
    message.SetClOrdID(id);

    message.SetSymbol(request.Symbol);
    if (FxTradeRecordType_Limit == request.Type)
    {
        message.SetOrdType(FIX::OrdType_LIMIT);
        if (request.NewPrice.HasValue())
        {
            message.SetPrice(*request.NewPrice);
        }
    }
    else if (FxTradeRecordType_Stop == request.Type)
    {
        message.SetOrdType(FIX::OrdType_STOP);
        if (request.NewPrice.HasValue())
        {
            message.SetStopPx(*request.NewPrice);
        }
    }
    else if (FxTradeRecordType_Position == request.Type)
    {
        message.SetOrdType(FIX::OrdType_POSITION);
    }
    else if (FxTradeRecordType_StopLimit == request.Type)
    {
        message.SetOrdType(FIX::OrdType_STOP_LIMIT);
        if (request.NewPrice.HasValue())
            message.SetPrice(request.NewPrice.Value());
        if (request.StopPrice.HasValue())
            message.SetStopPx(request.StopPrice.Value());
    }
    else
    {
        throw CArgumentException(cInvalidType);
    }

    if (FxTradeRecordSide_Buy == request.Side)
    {
        message.SetSide(FIX::Side_BUY);
    }
    else if (FxTradeRecordSide_Sell == request.Side)
    {
        message.SetSide(FIX::Side_SELL);
    }
    else
    {
        throw CArgumentException(cInvalidSide);
    }
    if (request.StopLoss.HasValue())
    {
        message.SetStopLoss(*request.StopLoss);
    }
    if (request.HiddenVolume.HasValue())
    {
        message.SetHiddenQty(request.HiddenVolume.Value());
    }
    if (request.TakeProfit.HasValue())
    {
        message.SetTakeProfit(*request.TakeProfit);
    }

    if (request.Expiration.HasValue())
    {
        message.SetTimeInForce(FIX::TimeInForce_GOOD_TILL_DATE);
        FIX::UtcTimeStamp utc(*request.Expiration);
        message.SetExpireTime(utc);
    }

    message.SetOrderQty(request.Volume);

    FIX::UtcTimeStamp now;
    message.SetTransactTime(now);


    if (!request.Comment.empty())
    {
        string encodedComment;
        StdToUtf8(encodedComment, request.Comment);
        message.SetEncodedCommentLen((int) encodedComment.length());
        message.SetEncodedComment(encodedComment);
    }

    if (!request.Tag.empty())
    {
        string encodedTag;
        StdToUtf8(encodedTag, request.Tag);
        message.SetEncodedTagLen((int) encodedTag.length());
        message.SetEncodedTag(encodedTag);
    }

    if (request.Magic.HasValue())
        message.SetMagic(*request.Magic);

    if (m_version.SupportsAppId())
    {
        if (!m_appId.empty())
            message.SetClAppID(m_appId);
    }

    return SendMessage(message);
}

void CFixSender::VSendGetFileChunk(const string& /*id*/, const string& fileId, const uint32 chunkId)
{
    FIX44::FileChunkReq message;
    message.SetFileId(fileId);

    message.SetChunkId(chunkId);
    return SendMessage(message);
}

void CFixSender::VSendGetBarsHistoryMetaInfoFile(const string& id, const string& symbol, int32 priceType, const string& period)
{
    FIX44::MarketDataHistoryMetadataRequest message(id, FIX::HstGraphType_BARS, symbol);
    message.SetHstGraphPeriodID(period);
    if (FxPriceType_Bid == priceType)
    {
        message.SetForexPriceType(FIX::ForexPriceType_BID);
    }
    else if (FxPriceType_Ask == priceType)
    {
        message.SetForexPriceType(FIX::ForexPriceType_ASK);
    }
    else
    {
        throw CArgumentException(cInvalidPriceType);
    }
    return SendMessage(message);
}

void CFixSender::VSendGetTicksHistoryMetaInfoFile(const string& id, const string& symbol, bool includeLevel2)
{
    FIX::HstGraphType type = includeLevel2 ? FIX::HstGraphType_TICKS_LEVEL2 : FIX::HstGraphType_TICKS;
    FIX44::MarketDataHistoryMetadataRequest message(id, type, symbol);
    return SendMessage(message);
}

void CFixSender::VSendGetTradeTransactionReportsAndSubscribeToNotifications(const string& id, FxTimeDirection direction, bool subscribe, const Nullable<CDateTime>& from, const Nullable<CDateTime>& to, uint32 bufferSize, const string& position)
{
    FIX44::TradeTransactionReportRequest message;
    if ((!from.HasValue()) ^ (!to.HasValue()))
    {
        throw CArgumentException("From and to parameters should be specified simultaneously.");
    }
    message.SetTradeRequestID(id);
    if (subscribe)
    {
        message.SetSubscriptionRequestType(FIX::SubscriptionRequestType_SNAPSHOT_PLUS_UPDATES);
    }
    else
    {
        message.SetSubscriptionRequestType(FIX::SubscriptionRequestType_SNAPSHOT);
    }
    if (FxTimeDirection_Forward == direction)
    {
        message.SetStrmngDirection(FIX::StrmngDirection_FORWARD);
    }
    else if (FxTimeDirection_Backward == direction)
    {
        message.SetStrmngDirection(FIX::StrmngDirection_BACKWARD);
    }
    else
    {
        throw CArgumentException("Incorrect time direction; possible values: forward and backward.");
    }
    message.SetStrmngBufSize(bufferSize);
    if (!position.empty())
    {
        message.SetStrmngPosID(position);
    }
    if (from.HasValue())
    {
        message.SetHstFrom(*from);
    }
    if (to.HasValue())
    {
        message.SetHstTo(*to);
    }
    return SendMessage(message);
}

void CFixSender::VSendUnsubscribeTradeTransactionReports(const string& id)
{
    FIX44::TradeTransactionReportRequest message;
    message.SetTradeRequestID(id);
    message.SetSubscriptionRequestType(FIX::SubscriptionRequestType_UNSUBSCRIBE);
    return SendMessage(message);
}

void CFixSender::VSendPositionReportRequest(const string& id, const string& account)
{
    FIX44::RequestForPositions message;
    message.SetPosReqID(id);
    message.SetAccount(account);
    message.SetPosReqType(FIX::PosReqType_POSITIONS);
    message.SetAccountType(FIX::AccountType_ACCOUNTCUSTOMER);
    FIX::UtcTimeStamp now;
    message.SetTransactTime(now);
    message.SetClearingBusinessDate(FIX::UtcTimeStampConvertor::convert(now));
    message.SetSubscriptionRequestType(FIX::SubscriptionRequestType_SNAPSHOT);
    return SendMessage(message);
}

void CFixSender::VSendQuotesHistoryRequest(const string& id)
{
    FIX44::ComponentsInfoRequest request;
    if (id.length())
        request.SetCompReqID(id);
    request.SetClientQuoteHistoryVersion(cClientQuoteHistoryVersion);
    return SendMessage(request);
}
