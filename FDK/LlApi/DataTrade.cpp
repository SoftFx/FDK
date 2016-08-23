#include "stdafx.h"
#include "DataTrade.h"
#include "Waiter.h"
#include "FxTradeTransactionReportIterator.h"
#ifdef _MSC_VER
#pragma warning (disable : 4355)
#else
typedef CClient __super;
#endif

CDataTrade::CDataTrade(const string& connectionString)
    : CClient(m_cache, connectionString)
    , m_accountType(FxAccountType_None)
    , m_cache(*this)
{
}

CFxAccountInfo CDataTrade::GetAccountInfo(const uint32 timeoutInMilliseconds)
{
    Waiter<CFxAccountInfo> waiter(timeoutInMilliseconds, cExternalSynchCall, *this);
    m_sender->VSendGetAccountInfo(waiter.Id());

    CFxAccountInfo result = waiter.WaitForResponse();
    return result;
}

CFxOrder CDataTrade::OpenNewOrder(const string& operationId, const CFxOrder& order, const size_t timeoutInMilliseconds)
{
    string id = NextIdIfEmpty(cExternalSynchCall, operationId);
    Waiter<CFxExecutionReport> waiter(static_cast<uint32>(timeoutInMilliseconds), string(), id, *this);
    m_sender->VSendOpenNewOrder(waiter.Id(), order);

    CFxOrder result;

    if (FxOrderType_Market == order.Type)
    {
        FxAccountType type = m_cache.GetAccountType();
        size_t count = 0;
        if (FxAccountType_Gross == type)
        {
            count = 3;
        }
        else if (FxAccountType_Net == type)
        {
            count = 2;
        }
        else
        {
            throw runtime_error("Market orders can not be used for not initialized account type");
        }

        CFxExecutionReport report;
        for (size_t step = 0; step < count; ++step)
        {
            CFxEventInfo info;
            report = waiter.WaitForResponse(info);

            if (report.IsReject())
            {
                throw CRejectException(report.Text, report.RejectReason);
            }
        }
        if (report.TryGetTradeRecord(result))
        {
            return result;
        }
        else
        {
            throw runtime_error("Internal error");
        }
    }
    else
    {
        for (;;)
        {
            CFxEventInfo info;
            CFxExecutionReport report = waiter.WaitForResponse(info);

            if (report.TryGetTradeRecord(result))
            {
                return result;
            }
            if (report.IsReject())
            {
                throw CRejectException(report.Text, report.RejectReason);
            }
        }
    }
}

vector<CFxOrder> CDataTrade::GetOrders(const size_t timeoutInMilliseconds)
{
    Waiter<CFxExecutionReport> waiter(static_cast<uint32>(timeoutInMilliseconds), cExternalSynchCall, *this);
    m_sender->VSendGetOrders(waiter.Id());
    vector<CFxOrder> result;
    for (int count = 1;; ++count)
    {
        CFxExecutionReport report = waiter.WaitForResponse();
        if (0 == report.ReportsNumber)
        {
            return result;
        }
        CFxOrder order;
        if (report.TryGetTradeRecord(order))
        {
            result.push_back(order);
        }
        if (count == report.ReportsNumber)
        {
            return result;
        }
    }
}

void CDataTrade::DeleteOrder(const string& /*operationId*/, const string& orderId, const string& clientId, FxTradeRecordSide side, size_t timeoutInMilliseconds)
{
    Waiter<CFxExecutionReport> waiter(static_cast<uint32>(timeoutInMilliseconds), cExternalSynchCall, *this);
    m_sender->VSendDeleteOrder(waiter.Id(), orderId, clientId, side);

    CFxEventInfo info;
    CFxExecutionReport executionReport = waiter.WaitForResponse(info);
    if (FxOrderStatus_Rejected == executionReport.OrderStatus)
    {
        throw CRejectException(executionReport.Text, executionReport.RejectReason);
    }
}

CFxClosePositionResult CDataTrade::CloseOrder(const string& operationId, const string& orderId, Nullable<double> closingVolume, const size_t timeoutInMilliseconds)
{
    string id = NextIdIfEmpty(cExternalSynchCall, operationId);
    Waiter<CFxClosePositionsResponse> responseWaiter(static_cast<uint32>(timeoutInMilliseconds), string(), id, *this);
    Waiter<CFxExecutionReport> reportWaiter(static_cast<uint32>(timeoutInMilliseconds), string(), id, *this);
    m_sender->VSendCloseOrder(id, orderId, closingVolume);

    CFxEventInfo info;
    CFxClosePositionsResponse response = responseWaiter.WaitForResponse(info);

    CFxClosePositionResult result;
    if (S_OK != response.Status)
    {
        throw runtime_error(response.Description);
    }
    CFxExecutionReport report = reportWaiter.WaitForResponse(info);
    if (report.IsReject())
    {
        throw CRejectException(report.Text, report.RejectReason);
    }
    report = reportWaiter.WaitForResponse(info);
    if (report.IsReject())
    {
        throw CRejectException(report.Text, report.RejectReason);
    }
    result.Sucess = true;
    result.ExecutedPrice = report.TradePrice;
    result.ExecutedVolume = *report.TradeAmount;
    return result;
}

bool CDataTrade::CloseByOrders(const string& firstOrderId, const string& secondOrderId, const size_t timeoutInMilliseconds)
{
    Waiter<CFxClosePositionsResponse> waiter(static_cast<uint32>(timeoutInMilliseconds), cExternalSynchCall, *this);
    m_sender->VSendCloseByOrders(waiter.Id(), firstOrderId, secondOrderId);
    CFxClosePositionsResponse response = waiter.WaitForResponse();
    const bool result = SUCCEEDED(response.Status);
    return result;
}

size_t CDataTrade::CloseAllOrders(const uint32 timeoutInMilliseconds)
{
    Waiter<CFxClosePositionsResponse> waiter(timeoutInMilliseconds, cExternalSynchCall, *this);
    m_sender->VSendCloseAllOrders(waiter.Id());

    CFxClosePositionsResponse response = waiter.WaitForResponse();

    if (FAILED(response.Status))
    {
        throw std::runtime_error(response.Description);
    }
    const size_t result = response.Orders.size();
    return result;
}

void CDataTrade::VExecution(const CFxEventInfo& eventInfo, CFxExecutionReport& executionReport)
{
    if (FxAccountType_Gross == m_accountType)
    {
        m_cache.UpdateOrders(executionReport);
    }
    else if ((FxOrderType_Limit == executionReport.OrderType) || (FxOrderType_Stop == executionReport.OrderType))
    {
        m_cache.UpdateOrders(executionReport);
    }
    else if (IsFinite(executionReport.Balance))
    {
        m_cache.UpdateBalance(executionReport.Balance);
    }

    if (FxAccountType_Cash == m_accountType)
    {
        if (executionReport.Assets.size() > 0)
        {
            m_cache.UpdateAssets(executionReport.Assets);
        }
    }

    __super::VExecution(eventInfo, executionReport);
}

void CDataTrade::VLogon(const CFxEventInfo& eventInfo, const string& protocolVersion)
{
    m_accountType = FxAccountType_None;
    m_cache.Clear();

    string id = NextId(cInternalASynchCall);
    m_sender->VSendGetAccountInfo(id);

    id = NextId(cInternalASynchCall);
    m_sender->VSendGetOrders(id);

    __super::VLogon(eventInfo, protocolVersion);
}

void CDataTrade::VTwoFactorAuth(const CFxEventInfo& eventInfo, const FxTwoFactorReason reason, const std::string& text, const CDateTime& expire)
{
    m_accountType = FxAccountType_None;
    m_cache.Clear();

    string id = NextId(cInternalASynchCall);
    m_sender->VSendGetAccountInfo(id);

    id = NextId(cInternalASynchCall);
    m_sender->VSendGetOrders(id);

    __super::VTwoFactorAuth(eventInfo, reason, text, expire);
}

void CDataTrade::VLogout(const CFxEventInfo& eventInfo, const FxLogoutReason reason, const string& description)
{
    m_accountType = FxAccountType_None;
    m_cache.Clear();
    __super::VLogout(eventInfo, reason, description);
}

void CDataTrade::VAccountInfo(const CFxEventInfo& eventInfo, CFxAccountInfo& accountInfo)
{
    UpdateAccountInfo(accountInfo.Type, accountInfo.AccountId);
    if (eventInfo.IsInternalAsynchCall() || eventInfo.IsNotification())
    {
        m_cache.UpdateAccountInfo(accountInfo);
        CFxMessage message(FX_MSG_ACCOUNT_INFO, eventInfo);
        message.Data = new CFxMsgAccountInfo(accountInfo);
        ProcessMessage(message);
    }
    else
    {
        __super::VAccountInfo(eventInfo, accountInfo);
    }
}

CFxOrder CDataTrade::ModifyOrder(const string& operationId, const CFxOrder& order, const size_t timeoutInMilliseconds)
{
    const string id = NextIdIfEmpty(cExternalSynchCall, operationId);
    Waiter<CFxExecutionReport> waiter(static_cast<uint32>(timeoutInMilliseconds), string(), id, *this);
    m_sender->VSendModifyOrder(waiter.Id(), order);

    CFxOrder result;
    for (;;)
    {
        CFxEventInfo info;
        CFxExecutionReport report = waiter.WaitForResponse(info);
        if (FxOrderStatus_Calculated == report.OrderStatus)
        {
            report.TryGetTradeRecord(result);
            return result;
        }
        if (FxOrderStatus_Rejected == report.OrderStatus)
        {
            throw CRejectException(info.Message, report.RejectReason);
        }
    }
}

FxIterator CDataTrade::GetTradeTransactionReportsAndSubscribeToNotifications(FxTimeDirection direction, bool subscribe, const Nullable<CDateTime>& from, const Nullable<CDateTime>& to, uint32 bufferSize, uint32 timeoutInMilliseconds)
{
    auto_ptr<CFxTradeTransactionReportIterator> it(new CFxTradeTransactionReportIterator(direction, from, to, bufferSize, *this));
    const HRESULT status = it->Construct(subscribe, timeoutInMilliseconds);
    if (FAILED(status))
    {
        return nullptr;
    }
    FxIterator result = it.release();
    return result;
}

void CDataTrade::UnsubscribeTradeTransactionReports(size_t timeoutInMilliseconds)
{
    Waiter<HRESULT> waiter(static_cast<uint32>(timeoutInMilliseconds), cExternalSynchCall, *this);
    m_sender->VSendUnsubscribeTradeTransactionReports(waiter.Id());
    waiter.WaitForResponse();
}

void CDataTrade::UpdateAccountInfo(FxAccountType accountType, const string& account)
{
    if (FxAccountType_None != m_accountType)
    {
        return;
    }
    m_accountType = accountType;
    if (FxAccountType_Net == accountType)
    {
        string id = NextId(cInternalASynchCall);
        m_sender->VSendPositionReportRequest(id, account);
    }
}

void CDataTrade::VNotify(const CFxEventInfo& eventInfo, const CNotification& notification)
{
    __super::VNotify(eventInfo, notification);

    if (NotificationType_Balance == notification.Type)
    {
        if (FxAccountType_Cash != m_accountType)
        {
            m_cache.UpdateBalance(notification.Balance);
        }
        else
        {
            m_cache.UpdateAssets(notification.TransactionCurrency, notification.Balance);
        }
    }
}

void CDataTrade::VPositionReport(const CFxEventInfo& info, CFxPositionReport& positionReport)
{
    if (FxAccountType_Net == m_accountType)
    {
        m_cache.UpdatePosition(positionReport);
        __super::VPositionReport(info, positionReport);
    }
}
