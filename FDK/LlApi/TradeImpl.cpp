#include "stdafx.h"
#include "TradeImpl.h"
#include "DataTrade.h"


namespace
{
    FxRef<CDataTrade> DataTradeFromHandle(void* handle)
    {
        if (nullptr == handle)
        {
            throw CArgumentNullException("handle can not be null");
        }
        FxRef<CDataTrade> result = TypeFromHandle<CDataTrade>(handle);
        if (!result)
        {
            throw CInvalidHandleException("invalid handle");
        }
        return result;
    }
}

void* CTradeImpl::Create(const std::string& name, const std::string& connectionString)
{
    CDataTrade* result = new CDataTrade(name, connectionString);
    return result;
}

void* CTradeImpl::GetTradeTransactionReportsAndSubscribe(void* handle, int direction, bool subscribe, const Nullable<CDateTime>& from, const Nullable<CDateTime>& to, size_t preferdBufferSize, const Nullable<int>& skipCancel, const size_t timeoutInMilliseconds)
{
    FxRef<CDataTrade> trade = DataTradeFromHandle(handle);
    Nullable<bool> skipCancelBool;
    if (skipCancel.HasValue())
        skipCancelBool = skipCancel.Value() != 0;
    void* result = trade->GetTradeTransactionReportsAndSubscribeToNotifications((FxTimeDirection)direction, subscribe, from, to, (uint32)preferdBufferSize, skipCancelBool, (uint32)timeoutInMilliseconds);
    return result;
}

void CTradeImpl::UnsubscribeTradeTransactionReports(void* handle, size_t timeoutInMilliseconds)
{
    FxRef<CDataTrade> trade = DataTradeFromHandle(handle);
    trade->UnsubscribeTradeTransactionReports(timeoutInMilliseconds);
}

void* CTradeImpl::GetTradeCaptureReports(void* /*handle*/, const Nullable<CDateTime>& /*from*/, const Nullable<CDateTime>& /*to*/, const size_t /*timeoutInMilliseconds*/)
{
    throw CNotImplementedException();
}

CFxTradeServerInfo CTradeImpl::GetTradeServerInfo(void* handle, size_t timeoutInMilliseconds)
{
    FxRef<CDataTrade> trade = DataTradeFromHandle(handle);
    return trade->GetTradeServerInfo(static_cast<uint32>(timeoutInMilliseconds));
}

CFxAccountInfo CTradeImpl::GetAccountInfo(void* handle, size_t timeoutInMilliseconds)
{
    FxRef<CDataTrade> trade = DataTradeFromHandle(handle);
    return trade->GetAccountInfo(static_cast<uint32>(timeoutInMilliseconds));
}

size_t CTradeImpl::CloseAllPositions(void* handle, const string& operationId, size_t timeoutInMilliseconds)
{
    FxRef<CDataTrade> trade = DataTradeFromHandle(handle);
    size_t result = trade->CloseAllOrders(operationId, static_cast<uint32>(timeoutInMilliseconds));
    return result;
}

bool CTradeImpl::CloseByPositions(void* handle, const string& operationId, const string& first, const string& second, size_t timeoutInMilliseconds)
{
    FxRef<CDataTrade> trade = DataTradeFromHandle(handle);
    const bool result = trade->CloseByOrders(operationId, first, second, timeoutInMilliseconds);
    return result;
}

vector<CFxOrder> CTradeImpl::GetRecords(void* handle, size_t timeoutInMilliseconds)
{
    FxRef<CDataTrade> trade = DataTradeFromHandle(handle);
    vector<CFxOrder> result = trade->GetOrders(timeoutInMilliseconds);
    return result;
}

void CTradeImpl::DeleteOrder(void* handle, const string& operationId, const string& orderId, const string& clientOrderId, FxTradeRecordSide side, size_t timeoutInMilliseconds)
{
    FxRef<CDataTrade> trade = DataTradeFromHandle(handle);
    trade->DeleteOrder(operationId, orderId, clientOrderId, side, timeoutInMilliseconds);
}

CFxOrder CTradeImpl::OpenNewOrder(void* handle, const string& operationId, const CFxOrder& order, size_t timeoutInMilliseconds)
{
    FxRef<CDataTrade> trade = DataTradeFromHandle(handle);
    return trade->OpenNewOrder(operationId, order, timeoutInMilliseconds);
}

CFxOrder CTradeImpl::ModifyOrder(void* handle, const string& operationId, const CFxOrder& order, size_t timeoutInMilliseconds)
{
    FxRef<CDataTrade> trade = DataTradeFromHandle(handle);
    return trade->ModifyOrder(operationId, order, timeoutInMilliseconds);
}

CFxClosePositionResult CTradeImpl::CloseOrder(void* handle, const string& operationId, const string& orderId, Nullable<double> closingVolume, const size_t timeoutInMilliseconds)
{
    FxRef<CDataTrade> trade = DataTradeFromHandle(handle);
    return trade->CloseOrder(operationId, orderId, closingVolume, timeoutInMilliseconds);
}

void* CTradeImpl::GetDailyAccountSnapshots(void* handle, int direction, const Nullable<CDateTime>& from, const Nullable<CDateTime>& to, size_t preferdBufferSize, const size_t timeoutInMilliseconds)
{
    FxRef<CDataTrade> trade = DataTradeFromHandle(handle);
    void* result = trade->GetDailyAccountSnapshotReports((FxTimeDirection)direction, from, to, (uint32)preferdBufferSize, (uint32)timeoutInMilliseconds);
    return result;
}
