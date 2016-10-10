#ifndef __Native_Client__
#define __Native_Client__

#include "Receiver.h"
#include "DataCache.h"
#include "ProtocolVersion.h"


extern const string cExternalSynchCall;
extern const string cInternalASynchCall;



class CClient : public CReceiver
{
protected:

    CClient(CDataCache& cache, const string& connectionString);

public:
    virtual ~CClient();

public:
    bool Start();
    bool WaitForLogon(size_t timeoutInMilliseconds);
    HRESULT Shutdown();
    HRESULT Stop();
    const CDataCache& Cache()const;
    ISender& Sender();

public:
    void GetNetworkActivity(uint64* pLogicalBytesSent, uint64* pPhysicalBytesSent, uint64* pLogicalBytesReceived, uint64* pPhysicalBytesReceived);

public:
    void SendTwoFactorResponse(const FxTwoFactorReason reason, const std::string& otp);
    CFxSessionInfo GetSessionInfo(const size_t timeoutInMilliseconds);
    CFxFileChunk GetFileChunk(const string& fileId, uint32 chunkId, const size_t timeoutInMilliseconds);
    string GetProtocolVersion()const;

public:
    virtual void VLogon(const CFxEventInfo& eventInfo, const string& protocolVersion, bool twofactor);
    virtual void VTwoFactorAuth(const CFxEventInfo& eventInfo, const FxTwoFactorReason reason, const std::string& text, const CDateTime& expire);
    virtual void VSessionInfo(const CFxEventInfo& eventInfo, CFxSessionInfo& sessionInfo);
    virtual void VLogout(const CFxEventInfo& eventInfo, const FxLogoutReason reason, const string& description);

protected:
    bool CheckProtocolVersion(const CProtocolVersion& requiredVersion) const;
    virtual void AfterLogon();

private:
    CClient(const CClient&);
    CClient& operator = (const CClient&);

protected:
    ISender* m_sender;

private:
    HANDLE m_stateEvent;

private:
    CDataCache& m_cache;
    ClientState m_state;
    IConnection* m_connection;
    string m_protocolVersion;
    mutable CCriticalSection m_stateSynchronizer;
    mutable CCriticalSection m_dataSynchronizer;
    bool m_afterLogonInvoked;
};


#pragma region inline methods

inline const CDataCache& CClient::Cache() const
{
    return m_cache;
}

inline ISender& CClient::Sender()
{
    return *m_sender;
}

#pragma endregion

#endif
