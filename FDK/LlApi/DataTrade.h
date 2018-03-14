#ifndef __Native_Data_Trade__
#define __Native_Data_Trade__

#include "Client.h"
#include "DataTradeCache.h"

class CDataTrade : public CClient
{
public:

    CDataTrade(const string& name, const string& connectionString);

    const CDataTradeCache& Cache()const;
    CDataTradeCache& Cache();

    CFxTradeServerInfo GetTradeServerInfo(const uint32 timeoutInMilliseconds);
    CFxAccountInfo GetAccountInfo(const uint32 timeoutInMilliseconds);
    vector<CFxOrder> GetOrders(const size_t timeoutInMilliseconds);
    CFxOrder OpenNewOrder(const string& operationId, const CFxOrder& order, const size_t timeoutInMilliseconds);
    CFxOrder ModifyOrder(const string& operationId, const CFxOrder& order, const size_t timeoutInMilliseconds);
    void DeleteOrder(const string& operationId, const string& orderId, const string& clientId, FxTradeRecordSide side, size_t timeoutInMilliseconds);
    CFxClosePositionResult CloseOrder(const string& operationId, const string& orderId, Nullable<double> closingVolume, const size_t timeoutInMilliseconds);
    bool CloseByOrders(const string& operationId, const string& firstOrderId, const string& secondOrderId, const size_t timeoutInMilliseconds);
    size_t CloseAllOrders(const string& operationId, const uint32 timeoutInMilliseconds);
    FxIterator GetTradeTransactionReportsAndSubscribeToNotifications(FxTimeDirection direction, bool subscribe, const Nullable<CDateTime>& from, const Nullable<CDateTime>& to, uint32 bufferSize, const Nullable<bool>& skipCancel, uint32 timeoutInMilliseconds);
    void UnsubscribeTradeTransactionReports(size_t timeoutInMilliseconds);
    FxIterator GetDailyAccountSnapshotReports(FxTimeDirection direction, const Nullable<CDateTime>& from, const Nullable<CDateTime>& to, uint32 bufferSize, uint32 timeoutInMilliseconds);

    virtual void VLogon(const CFxEventInfo& eventInfo, const string& protocolVersion, bool twofactor);
    virtual void VTwoFactorAuth(const CFxEventInfo& eventInfo, const FxTwoFactorReason reason, const std::string& text, const CDateTime& expire);
    virtual void VLogout(const CFxEventInfo& eventInfo, const FxLogoutReason reason, const string& description);
    virtual void VTradeServerInfoReport(const CFxEventInfo& eventInfo, CFxTradeServerInfo& tradeServerInfo);
    virtual void VAccountInfo(const CFxEventInfo& eventInfo, CFxAccountInfo& accountInfo);
    virtual void VClosePositions(const CFxEventInfo& eventInfo, CFxClosePositionsResponse& response);
    virtual void VExecution(const CFxEventInfo& eventInfo, CFxExecutionReport& executionReport);
    virtual void VTradeHistoryResponse(const CFxEventInfo& eventInfo, CFxTradeHistoryResponse& response);
    virtual void VTradeHistoryReport(const CFxEventInfo& eventInfo, CFxTradeHistoryReport& report);
    virtual void VGetTradeTransactionReportsAndSubscribeToNotifications(const CFxEventInfo& info, const int32 curReportsNumber, const int32 totReportsNumber, const bool endOfStream);
    virtual void VTradeTransactionReport(const CFxEventInfo& info, CFxTradeTransactionReport& report);
    virtual void VUnsubscribeTradeTransactionReportsNotifications(const CFxEventInfo& info);
    virtual void VPositionReport(const CFxEventInfo& info, CFxPositionReport& positionReport);
    virtual void VNotify(const CFxEventInfo& eventInfo, const CNotification& notification);
    virtual void VGetDailyAccountSnapshotReports(const CFxEventInfo& info, const int32 curReportsNumber, const int32 totReportsNumber, const bool endOfStream);
    virtual void VDailyAccountSnapshotReport(const CFxEventInfo& eventInfo, CFxDailyAccountSnapshotReport& report);

protected:

    virtual void AfterLogon();

private:

    friend class CFxTradeTransactionReportIterator;
    friend class CFxDailyAccountSnapshotReportIterator;

    CDataTrade(const CDataTrade&);
    CDataTrade& operator = (const CDataTrade&);

    void UpdateAccountInfo(FxAccountType accountType, const string& account);

    FxAccountType m_accountType;
    CDataTradeCache m_cache;
};

#pragma region inline methods

inline const CDataTradeCache& CDataTrade::Cache() const
{
    return m_cache;
}

inline CDataTradeCache& CDataTrade::Cache()
{
    return m_cache;
}

#pragma endregion

#endif
