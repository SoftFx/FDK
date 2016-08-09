#include "stdafx.h"
#include "AggrSender.h"

void CAggrSender::VSendGetOrders(const string& id)
{
    CFxEventInfo info;
    info.ID = id;

    CFxExecutionReport report;
    report.ExecutionType = FxExecutionType_OrderStatus;
    m_receiver->VExecution(info, report);
}
void CAggrSender::VSendDataHistoryRequest(const string& id, const CFxDataHistoryRequest& request)
{
    CFxEventInfo info;
    info.ID = id;
    CFxDataHistoryResponse response;
    m_receiver->VDataHistoryResponse(info, response);
}
void CAggrSender::VSendGetTradeTransactionReportsAndSubscribeToNotifications(const string& id, FxTimeDirection /*direction*/, bool /*subscribe*/, const Nullable<CDateTime>& /*from*/, const Nullable<CDateTime>& /*to*/, uint32 /*bufferSize*/, const string& /*position*/)
{
    CFxEventInfo info;
    info.ID = id;
    m_receiver->VGetTradeTransactionReportsAndSubscribeToNotifications(info, 0, 0, true);
}

void CAggrSender::VSendUnsubscribeTradeTransactionReports(const string& id)
{
    CFxEventInfo info;
    info.ID = id;
    m_receiver->VUnsubscribeTradeTransactionReportsNotifications(info);
}
