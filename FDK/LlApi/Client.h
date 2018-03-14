#ifndef __Native_Client__
#define __Native_Client__

#include "FxQueue.h"
#include "DataCache.h"
#include "ClientState.h"
#include "SynchInvoker.h"
#include "IdGenerator.h"
#include "ProtocolVersion.h"

extern const string cExternalSynchCall;
extern const string cInternalASynchCall;

class CClient : public CFxQueue, public IReceiver
{
public:

    CClient(CDataCache& cache, const string& name, const string& connectionString);
    virtual ~CClient();

    string getName();

    bool Start();

    HRESULT Dispose();
    HRESULT Shutdown();
    HRESULT Stop();

    bool WaitForLogon(size_t timeoutInMilliseconds);

    const CDataCache& Cache()const;
    CDataCache& Cache();

public:

    string GetProtocolVersion()const;

    void SendTwoFactorResponse(const FxTwoFactorReason reason, const std::string& otp);

    CFxSessionInfo GetSessionInfo(const size_t timeoutInMilliseconds);
    CFxFileChunk GetFileChunk(const string& fileId, uint32 chunkId, const size_t timeoutInMilliseconds);

    void GetNetworkActivity(uint64* pLogicalBytesSent, uint64* pPhysicalBytesSent, uint64* pLogicalBytesReceived, uint64* pPhysicalBytesReceived);

    const string NextId();
    const string NextId(const string& prefix);
    const string NextIdIfEmpty(const string& prefix, const string& externalId);

    void RegisterWaiter(const type_info& info, const string& id, IWaiter* pWaiter);
    void ReleaseWaiter(const type_info& info, const string& id);

    virtual void VLogon(const CFxEventInfo& eventInfo, const string& protocolVersion, bool twofactor);
    virtual void VTwoFactorAuth(const CFxEventInfo& eventInfo, const FxTwoFactorReason reason, const std::string& text, const CDateTime& expire);
    virtual void VLogout(const CFxEventInfo& eventInfo, const FxLogoutReason reason, const string& description);
    virtual void VBusinessReject(const CFxEventInfo& eventInfo);
    virtual void VSubscribed(const CFxEventInfo& eventInfo, const CFxQuote& snapshot);
    virtual void VUnsubscribed(const CFxEventInfo& eventInfo, const string& symbol);
    virtual void VTick(const CFxEventInfo& eventInfo, const CFxQuote& quotes);
    virtual void VSessionInfo(const CFxEventInfo& eventInfo, CFxSessionInfo& sessionInfo);
    virtual void VTradeServerInfoReport(const CFxEventInfo& eventInfo, CFxTradeServerInfo& tradeServerInfo);
    virtual void VAccountInfo(const CFxEventInfo& eventInfo, CFxAccountInfo& accountInfo);
    virtual void VGetCurrencies(const CFxEventInfo& eventInfo, const vector<CFxCurrencyInfo>& currencies);
    virtual void VGetSupportedSymbols(const CFxEventInfo& eventInfo, const vector<CFxSymbolInfo>& symbols);
    virtual void VSubscribeToQuotes(const CFxEventInfo& eventInfo, HRESULT status);
    virtual void VClosePositions(const CFxEventInfo& eventInfo, CFxClosePositionsResponse& response);
    virtual void VExecution(const CFxEventInfo& eventInfo, CFxExecutionReport& executionReport);
    virtual void VDataHistoryResponse(const CFxEventInfo& eventInfo, CFxDataHistoryResponse& response);
    virtual void VTradeHistoryResponse(const CFxEventInfo& eventInfo, CFxTradeHistoryResponse& response);
    virtual void VTradeHistoryReport(const CFxEventInfo& eventInfo, CFxTradeHistoryReport& report);
    virtual void VFileChunk(const CFxEventInfo& eventInfo, CFxFileChunk& chunk);
    virtual void VMetaInfoFile(const CFxEventInfo& eventInfo, string& file);
    virtual void VGetTradeTransactionReportsAndSubscribeToNotifications(const CFxEventInfo& info, const int32 curReportsNumber, const int32 totReportsNumber, const bool endOfStream);
    virtual void VTradeTransactionReport(const CFxEventInfo& info, CFxTradeTransactionReport& report);
    virtual void VUnsubscribeTradeTransactionReportsNotifications(const CFxEventInfo& info);
    virtual void VPositionReport(const CFxEventInfo& info, CFxPositionReport& positionReport);
    virtual void VNotify(const CFxEventInfo& eventInfo, const CNotification& notification);
    virtual void VQuotesHistoryResponse(const CFxEventInfo& eventInfo, const int version);
    virtual void VGetDailyAccountSnapshotReports(const CFxEventInfo& info, const int32 curReportsNumber, const int32 totReportsNumber, const bool endOfStream);
    virtual void VDailyAccountSnapshotReport(const CFxEventInfo& eventInfo, CFxDailyAccountSnapshotReport& report);

protected:

    bool CheckProtocolVersion(const CProtocolVersion& requiredVersion) const;

    virtual void AfterLogon();

    CDataCache& m_cache;
    string name_;
    ClientState m_state;
    HANDLE m_stateEvent;
    CIdGenerator m_idGenerator;
    CSynchInvoker m_synchInvoker;
    IConnection* m_connection;
    ISender* m_sender;
    string m_protocolVersion;
    mutable CCriticalSection m_stateSynchronizer;
    mutable CCriticalSection m_dataSynchronizer;
    bool m_afterLogonInvoked;

private:

    CClient(const CClient&);
    CClient& operator = (const CClient&);
};


#pragma region inline methods

inline const CDataCache& CClient::Cache() const
{
    return m_cache;
}

inline CDataCache& CClient::Cache()
{
    return m_cache;
}

#pragma endregion

#endif
