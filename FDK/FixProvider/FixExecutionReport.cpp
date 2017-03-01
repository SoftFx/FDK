#include "stdafx.h"
#include "FixExecutionReport.h"

// region double methods
double CFixExecutionReport::GetFxExecutedVolume()const
{
    double result = numeric_limits<double>::quiet_NaN();
    this->TryGetCumQty(result);
    return result;
}
Nullable<double> CFixExecutionReport::GetFxInitialVolume()const
{
    double result = 0;
    if (TryGetOrderQty(result))
    {
        return result;
    }
    return Nullable<double>();
}
double CFixExecutionReport::GetFxLeavesVolume()const
{
    double result = numeric_limits<double>::quiet_NaN();
    this->TryGetLeavesQty(result);
    return result;
}
Nullable<double> CFixExecutionReport::GetFxHiddenVolume()const
{
    double result = 0;
    if (TryGetHiddenQty(result))
    {
        return result;
    }
    return Nullable<double>();
}
Nullable<double> CFixExecutionReport::GetFxTradeAmount()const
{
    double result = 0;
    if (TryGetLastQty(result))
    {
        return result;
    }
    return Nullable<double>();
}
Nullable<double> CFixExecutionReport::GetFxAveragePrice()const
{
    double result = 0;
    if (TryGetAvgPx(result))
    {
        return result;
    }
    return Nullable<double>();
}
Nullable<double> CFixExecutionReport::GetFxPrice()const
{
    double result = 0;
    if (TryGetPrice(result))
    {
        return result;
    }
    return Nullable<double>();
}
Nullable<double> CFixExecutionReport::GetFxStopPrice()const
{
    double result = 0;
    if (TryGetStopPx(result))
    {
        return result;
    }
    return Nullable<double>();
}
Nullable<double> CFixExecutionReport::GetFxTakeProfit()const
{
    double result = 0;
    if (TryGetTakeProfit(result))
    {
        return result;
    }
    return Nullable<double>();
}
Nullable<double> CFixExecutionReport::GetFxStopLoss()const
{
    double result = 0;
    if (TryGetStopLoss(result))
    {
        return result;
    }
    return Nullable<double>();
}
double CFixExecutionReport::GetFxTradePrice() const
{
    double result = numeric_limits<double>::quiet_NaN();
    this->TryGetLastPx(result);
    return result;
}
double CFixExecutionReport::GetFxBalance() const
{
    double result = numeric_limits<double>::quiet_NaN();
    this->TryGetAccBalance(result);
    return result;
}
double CFixExecutionReport::GetFxCommission() const
{
    double result = 0;
    this->TryGetCommission(result);
    return result;
}
double CFixExecutionReport::GetFxAgentCommission() const
{
    double result = 0;
    this->TryGetAgentCommission(result);
    return result;
}
double CFixExecutionReport::GetFxSwap() const
{
    double result = 0;
    this->TryGetSwap(result);
    return result;
}
//endregion

//region date time methods
Nullable<CDateTime> CFixExecutionReport::GetFxExpiration() const
{
    FIX::UtcTimeStamp expiration(time_t(0));
    if (TryGetExpireTime(expiration))
    {
        return expiration.toFileTime();
    }
    return Nullable<CDateTime>();
}
Nullable<CDateTime> CFixExecutionReport::GetCreatedDateTime() const
{
    FIX::UtcTimeStamp time(time_t(0));
    if (TryGetOrdCreated(time))
    {
        return time.toFileTime();
    }
    return Nullable<CDateTime>();
}
Nullable<CDateTime> CFixExecutionReport::GetModifiedDateTime() const
{
    FIX::UtcTimeStamp time(time_t(0));
    if (TryGetOrdModified(time))
    {
        return time.toFileTime();
    }
    return Nullable<CDateTime>();
}
//endregion


//region string methods
std::string CFixExecutionReport::GetFxOrderId()const
{
    string result = this->GetOrderID();
    return result;
}
std::string CFixExecutionReport::GetFxClientOrderId()const
{
    string result;
    TryGetClOrdID(result);
    return result;
}
std::string CFixExecutionReport::GetFxSymbol()const
{
    string result = this->GetSymbol();
    return result;
}
string CFixExecutionReport::GetFxText() const
{
    string result;
    TryGetText(result);
    return result;
}
string CFixExecutionReport::GetFxClosePositionRequestId()const
{
    string result;
    TryGetClosePosReqID(result);
    return result;
}
//endregion

int32 CFixExecutionReport::GetFxReportsNumber()const
{
    int32 result = 0;
    this->TryGetTotNumReports(result);
    return result;
}

FxTradeRecordSide CFixExecutionReport::GetFxSide() const
{
    const char ch = GetSide();
    if (FIX::Side_BUY == ch)
    {
        return FxTradeRecordSide_Buy;
    }
    else if (FIX::Side_SELL == ch)
    {
        return FxTradeRecordSide_Sell;
    }
    throw CRuntimeError("CFixExecutionReport::GetFxSide(); invalid fix side = ") + ch;
}

FxRejectReason CFixExecutionReport::GetFxRejectReason() const
{
    int fixOrdRejReason = 0;
    if (!this->TryGetOrdRejReason(fixOrdRejReason))
    {
        return FxRejectReason_None;
    }
    if (FIX::OrdRejReason_BROKER == fixOrdRejReason)
    {
        return FxRejectReason_DealerReject;
    }
    if (FIX::OrdRejReason_UNKNOWNSYM == fixOrdRejReason)
    {
        return FxRejectReason_UnknownSymbol;
    }
    if (FIX::OrdRejReason_EXCHCLOSED == fixOrdRejReason)
    {
        return FxRejectReason_TradeSessionIsClosed;
    }
    if (FIX::OrdRejReason_TOO_LATE_TO_ENTER == fixOrdRejReason)
    {
        return FxRejectReason_OffQuotes;
    }
    if (FIX::OrdRejReason_UNKNOWN_ORDER == fixOrdRejReason)
    {
        return FxRejectReason_UnknownOrder;
    }
    if (FIX::OrdRejReason_DUPLICATE_ORDER == fixOrdRejReason)
    {
        return FxRejectReason_DuplicateClientOrderId;
    }
    if (FIX::OrdRejReason_ORDER_EXCEEDS_LIMIT == fixOrdRejReason)
    {
        return FxRejectReason_OrderExceedsLImit;
    }
    if (FIX::OrdRejReason_UNSUPPORTED_ORDER_CHARACTERISTIC == fixOrdRejReason)
    {
        return FxRejectReason_InvalidTradeRecordParameters;
    }
    if (FIX::OrdRejReason_INCORRECTQUANTITY == fixOrdRejReason)
    {
        return FxRejectReason_IncorrectQuantity;
    }
    return FxRejectReason_Unknown;
}

FxOrderType CFixExecutionReport::GetFxOrderType() const
{
    char orderType = 0;
    if (!this->TryGetOrdType(orderType))
    {
        return FxOrderType_None;
    }
    if (FIX::OrdType_MARKET == orderType)
    {
        return FxOrderType_Market;
    }
    else if (FIX::OrdType_LIMIT == orderType)
    {
        return FxOrderType_Limit;
    }
    else if (FIX::OrdType_STOP == orderType)
    {
        return FxOrderType_Stop;
    }
    else if(FIX::OrdType_POSITION == orderType)
    {
        return FxOrderType_Position;
    }
    else if (FIX::OrdType_STOPLIMIT == orderType)
    {
        return FxOrderType_StopLimit;
    }
    throw CRuntimeError("CFixExecutionReport::GetFxOrderType(); invalid fix order type = ") + orderType;
}

FxExecutionType CFixExecutionReport::GetFxExecutionType() const
{
    const char executionType = GetExecType();
    if (FIX::ExecType_NEW == executionType)
    {
        return FxExecutionType_New;
    }
    if (FIX::ExecType_CANCELED == executionType)
    {
        return FxExecutionType_Canceled;
    }
    if (FIX::ExecType_PENDING_CANCEL == executionType)
    {
        return FxExecutionType_PendingCancel;
    }
    if (FIX::ExecType_REJECTED == executionType)
    {
        return FxExecutionType_Rejected;
    }
    if (FIX::ExecType_CALCULATED == executionType)
    {
        return FxExecutionType_Calculated;
    }
    if (FIX::ExecType_EXPIRED == executionType)
    {
        return FxExecutionType_Expired;
    }
    if (FIX::ExecType_PENDING_REPLACE == executionType)
    {
        return FxExecutionType_PendingReplace;
    }
    if (FIX::ExecType_REPLACE == executionType)
    {
        return FxExecutionType_Replace;
    }
    if (FIX::ExecType_TRADE == executionType)
    {
        return FxExecutionType_Trade;
    }
    if (FIX::ExecType_ORDER_STATUS == executionType)
    {
        return FxExecutionType_OrderStatus;
    }
    if (FIX::ExecType_PENDINGCLOSE == executionType)
    {
        return FxExecutionType_PendingClose;
    }
    throw CRuntimeError("CFixExecutionReport::GetFxExecutionType(): invalid execution type = ") + executionType;
}

FxOrderStatus CFixExecutionReport::GetFxOrderStatus() const
{
    const char orderStatus = GetOrdStatus();
    if (FIX::OrdStatus_NEW == orderStatus)
    {
        return FxOrderStatus_New;
    }
    if (FIX::OrdStatus_PARTIALLY_FILLED == orderStatus)
    {
        return FxOrderStatus_PartiallyFilled;
    }
    if (FIX::OrdStatus_FILLED == orderStatus)
    {
        return FxOrderStatus_Filled;
    }
    if (FIX::OrdStatus_DONE == orderStatus)
    {
        return FxOrderStatus_Activated;
    }
    if (FIX::OrdStatus_CANCELED == orderStatus)
    {
        return FxOrderStatus_Canceled;
    }
    if (FIX::OrdStatus_PENDING_CANCEL== orderStatus)
    {
        return FxOrderStatus_PendingCancel;
    }
    if (FIX::OrdStatus_REJECTED == orderStatus)
    {
        return FxOrderStatus_Rejected;
    }
    if (FIX::OrdStatus_CALCULATED == orderStatus)
    {
        return FxOrderStatus_Calculated;
    }
    if (FIX::OrdStatus_EXPIRED == orderStatus)
    {
        return FxOrderStatus_Expired;
    }
    if (FIX::OrdStatus_PENDING_REPLACE == orderStatus)
    {
        return FxOrderStatus_PendingReplace;
    }
    if (FIX::OrdStatus_DONE == orderStatus)
    {
        return FxOrderStatus_Done;
    }
    if (FIX::OrdStatus_PENDINGCLOSE == orderStatus)
    {
        return FxOrderStatus_PendingClose;
    }
    throw CRuntimeError("CFixExecutionReport::GetFxOrderStatus(): invalid order status = ") + orderStatus;
}

bool CFixExecutionReport::TryGetMassStatusRequestId(string& st) const
{
    const bool result = this->TryGetMassStatusReqID(st);
    return result;
}
std::string CFixExecutionReport::GetComment() const
{
    std::string result;
    this->TryGetEncodedComment(result);
    return result;
}
std::string CFixExecutionReport::GetTag() const
{
    std::string result;
    this->TryGetEncodedTag(result);
    return result;
}
Nullable<int> CFixExecutionReport::GetMagic() const
{
    int magic = 0;
    if (TryGetMagic(magic))
    {
        return magic;
    }
    return Nullable<int>();
}
bool CFixExecutionReport::GetImmediateOrCancelFlag() const
{
    bool ioc = 0;
    if (TryGetImmediateOrCancelFlag(ioc))
    {
        return ioc;
    }
    return false;
}
bool CFixExecutionReport::GetMarketWithSlippageFlag() const
{
    bool mws = 0;
    if (TryGetMarketWithSlippageFlag(mws))
    {
        return mws;
    }
    return false;
}

void CFixExecutionReport::GetAssets(vector<CAssetInfo>& assets) const
{
    assets.clear();
    int count = 0;
    if (TryGetNoAssets(count))
    {
        assets.reserve(static_cast<size_t>(count));
        for (int index = 1; index <= count; ++index)
        {
            FIX44::AccountInfo::NoAssets group;
            getGroup(index, group);

            CAssetInfo asset;
            asset.Currency = group.GetAssetCurrency();
            asset.Balance = group.GetAssetBalance();
            group.TryGetAssetLockedAmt(asset.LockedAmount);
            group.TryGetAssetTradeAmt(asset.TradeAmount);

            assets.push_back(asset);
        }
    }
}
