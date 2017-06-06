#pragma once



class CTradeImpl
{
public:
    void* Create(const std::string& name, const std::string& connectionString);
    void* GetTradeTransactionReportsAndSubscribe(void* handle, int direction, bool subscribe, const Nullable<CDateTime>& from, const Nullable<CDateTime>& to, size_t preferdBufferSize, const Nullable<int>& skipCancel, const size_t timeoutInMilliseconds);
    void UnsubscribeTradeTransactionReports(void* handle, size_t timeoutInMilliseconds);
    void* GetTradeCaptureReports(void* handle, const Nullable<CDateTime>& from, const Nullable<CDateTime>& to, const size_t timeoutInMilliseconds);
    CFxTradeServerInfo GetTradeServerInfo(void* handle, size_t timeoutInMilliseconds);
    CFxAccountInfo GetAccountInfo(void* handle, size_t timeoutInMilliseconds);
    size_t CloseAllPositions(void* handle, const string& operationId, size_t timeoutInMilliseconds);
    bool CloseByPositions(void* handle, const string& operationId, const string& first, const string& second, size_t timeoutInMilliseconds);
    vector<CFxOrder> GetRecords(void* handle, size_t timeoutInMilliseconds);
public:
    void DeleteOrder(void* handle, const string& operationId, const string& orderId, const string& clientOrderId, FxTradeRecordSide side, size_t timeoutInMilliseconds);
    CFxOrder OpenNewOrder(void* handle, const string& operationId, const CFxOrder& order, size_t timeoutInMilliseconds);
    CFxOrder ModifyOrder(void* handle, const string& operationId, const CFxOrder& order, size_t timeoutInMilliseconds);
    CFxClosePositionResult CloseOrder(void* handle, const string& operationId, const string& orderId, Nullable<double> closingVolume, const size_t timeoutInMilliseconds);
};
