#include "stdafx.h"
#include "Receiver.h"

const string CReceiver::NextIdIfEmpty(const string& prefix, const string& externalId)
{
    if (!externalId.empty())
    {
        return externalId;
    }
    return NextId(prefix);
}

const string CReceiver::NextId(const string& prefix)
{
    return m_idGenerator.Next(prefix);
}

const string CReceiver::NextId()
{
    return NextId(string());
}

void CReceiver::RegisterWaiter(const type_info& info, const string& id, IWaiter* pWaiter)
{
    m_synchInvoker.RegisterWaiter(info, id, pWaiter);
}

void CReceiver::ReleaseWaiter(const type_info& info, const string& id)
{
    m_synchInvoker.ReleaseWaiter(info, id);
}

void CReceiver::VLogon(const CFxEventInfo& eventInfo, const string& protocolVersion)
{
    CFxMessage message(FX_MSG_LOGON, eventInfo);
    message.Data = new CFxMsgLogon(protocolVersion);
    ProcessMessage(message);
}

void CReceiver::VLogout(const CFxEventInfo& eventInfo, const FxLogoutReason reason, const string& description)
{
    CFxMessage message(FX_MSG_LOGOUT, eventInfo);
    int32 code = GetLastError();
    message.Data = new CFxMsgLogout(reason, code, description);
    ProcessMessage(message);
    m_synchInvoker.Disconnect();
}

void CReceiver::VBusinessReject(const CFxEventInfo& eventInfo)
{
    m_synchInvoker.Response(eventInfo);
}

void CReceiver::VTick(const CFxEventInfo& /*eventInfo*/, const CFxQuote& /*quotes*/)
{
}

void CReceiver::VSessionInfo(const CFxEventInfo& eventInfo, CFxSessionInfo& sessionInfo)
{
    if (eventInfo.IsNotification())
    {
        CFxMessage message(FX_MSG_SESSION_INFO, eventInfo);
        message.Data = new CFxMsgSessionInfo(sessionInfo);
        ProcessMessage(message);
    }
    else
    {
        m_synchInvoker.Response(eventInfo, sessionInfo);
    }
}

void CReceiver::VGetCurrencies(const CFxEventInfo& eventInfo, const vector<CFxCurrencyInfo>& currencies)
{
    if (eventInfo.IsInternalAsynchCall())
    {
        CFxMessage message(FX_MSG_CURRENCY_INFO, eventInfo);
        message.Data = new CFxMsgCurrencyInfo(currencies);
        ProcessMessage(message);
    }
    vector<CFxCurrencyInfo> temp = currencies;
    m_synchInvoker.Response(eventInfo, temp);
}

void CReceiver::VGetSupportedSymbols(const CFxEventInfo& eventInfo, const vector<CFxSymbolInfo>& symbols)
{
    if (eventInfo.IsInternalAsynchCall())
    {
        CFxMessage message(FX_MSG_SYMBOL_INFO, eventInfo);
        message.Data = new CFxMsgSymbolInfo(symbols);
        ProcessMessage(message);
    }
    vector<CFxSymbolInfo> temp = symbols;
    m_synchInvoker.Response(eventInfo, temp);
}

void CReceiver::VSubscribeToQuotes(const CFxEventInfo& eventInfo, HRESULT status)
{
    m_synchInvoker.Response(eventInfo, status);
}

void CReceiver::VAccountInfo(const CFxEventInfo& eventInfo, CFxAccountInfo& accountInfo)
{
    m_synchInvoker.Response(eventInfo, accountInfo);
}

void CReceiver::VClosePositions(const CFxEventInfo& eventInfo, CFxClosePositionsResponse& response)
{
    m_synchInvoker.Response(eventInfo, response);
}

void CReceiver::VExecution(const CFxEventInfo& eventInfo, CFxExecutionReport& executionReport)
{
    CFxMessage message(FX_MSG_EXECUTION_REPORT, eventInfo);

    if (!eventInfo.IsInternalAsynchCall() && (FxExecutionType_OrderStatus != executionReport.ExecutionType))
    {
        CFxExecutionReport temp = executionReport;
        message.Data = new CFxMsgExecutionReport(temp);
        ProcessMessage(message);
    }
    m_synchInvoker.Response(eventInfo, executionReport);
}

void CReceiver::VDataHistoryResponse(const CFxEventInfo& eventInfo, CFxDataHistoryResponse& response)
{
    m_synchInvoker.Response(eventInfo, response);
}

void CReceiver::VTradeHistoryResponse(const CFxEventInfo& eventInfo, CFxTradeHistoryResponse& response)
{
    m_synchInvoker.Response(eventInfo, response);
}

void CReceiver::VTradeHistoryReport(const CFxEventInfo& eventInfo, CFxTradeHistoryReport& report)
{
    m_synchInvoker.Response(eventInfo, report);
}

void CReceiver::VFileChunk(const CFxEventInfo& eventInfo, CFxFileChunk& chunk)
{
    m_synchInvoker.Response(eventInfo, chunk);
}

void CReceiver::VMetaInfoFile(const CFxEventInfo& eventInfo, string& file)
{
    m_synchInvoker.Response(eventInfo, file);
}

void CReceiver::VGetTradeTransactionReportsAndSubscribeToNotifications(const CFxEventInfo& info, const int32 curReportsNumber, const int32 totReportsNumber, const bool endOfStream)
{
    tuple<int32, int32, bool> response(curReportsNumber, totReportsNumber, endOfStream);
    m_synchInvoker.Response(info, response);
}

void CReceiver::VTradeTransactionReport(const CFxEventInfo& info, CFxTradeTransactionReport& report)
{
    if (info.IsNotification())
    {
        CFxMessage message(FX_MSG_TRADE_TRANSACTION_REPORT, info);
        CFxTradeTransactionReport temp(report);
        message.Data = new CFxMsgTradeTransactionReport(temp);
        ProcessMessage(message);
    }
    else
    {
        m_synchInvoker.Response(info, report);
    }
}

void CReceiver::VUnsubscribeTradeTransactionReportsNotifications(const CFxEventInfo& info)
{
    HRESULT response = info.Status;
    m_synchInvoker.Response(info, response);
}

void CReceiver::VPositionReport(const CFxEventInfo& info, CFxPositionReport& positionReport)
{
    CFxMessage message(FX_MSG_POSITION_REPORT, info);
    CFxPositionReport temp = positionReport;
    message.Data = new CFxMsgPositionReport(temp);
    ProcessMessage(message);
}

void CReceiver::VNotify(const CFxEventInfo& eventInfo, const CNotification& notification)
{
    CFxMessage message(FX_MSG_NOTIFICATION, eventInfo);
    CNotification temp(notification);
    message.Data = new CFxMsgNotification(temp);
    ProcessMessage(message);
}

void CReceiver::VQuotesHistoryResponse(const CFxEventInfo& /*eventInfo*/, const int /*version*/)
{
}
