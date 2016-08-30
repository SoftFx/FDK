#include "stdafx.h"
#include "Client.h"
#include "Waiter.h"
#include "LlApi.h"
#include "ConnectionBuilder.h"

#ifndef _MSC_VER
typedef CReceiver __super;
#endif


const string cExternalSynchCall = "ES";
const string cInternalASynchCall = "IA";


CClient::CClient(CDataCache& cache, const string& connectionString)
    : m_sender(nullptr)
    , m_cache(cache)
    , m_state(ClientState_Stopped)
    , m_connection(nullptr)
{
    m_stateEvent = CreateEvent(nullptr, TRUE, FALSE, nullptr);
    m_connection = CreateConnection(connectionString);
    m_connection->VReceiver(this);
    m_sender = m_connection->VSender();
}

bool CClient::Start()
{
    CLock lock(m_stateSynchronizer);
    if (ClientState_Started == m_state)
    {
        return false;
    }
    if (ClientState_Stopped != m_state)
    {
        throw runtime_error("Can not start not stopped client");
    }
    const HRESULT result = __super::Construct();
    if (SUCCEEDED(result))
    {
        m_connection->VStart();
        m_state = ClientState_Started;
        return true;
    }
    else
    {
        throw runtime_error("Can not start client");
    }
}

bool CClient::WaitForLogon(size_t timeoutInMilliseconds)
{
    const DWORD status = WaitForSingleObject(m_stateEvent, static_cast<DWORD>(timeoutInMilliseconds));
    return (WAIT_OBJECT_0 == status);
}

HRESULT CClient::Shutdown()
{
    CLock lock(m_stateSynchronizer);
    if (ClientState_Shutdown == m_state)
    {
        return S_FALSE;
    }
    if (ClientState_Started != m_state)
    {
        return E_FAIL;
    }
    m_connection->VShutdown();
    __super::ReleaseQueue();
    m_state = ClientState_Shutdown;
    return S_OK;
}

HRESULT CClient::Stop()
{
    CLock lock(m_stateSynchronizer);
    if (ClientState_Stopped == m_state)
    {
        return S_FALSE;
    }
    if (ClientState_Shutdown != m_state)
    {
        return E_FAIL;
    }
    m_connection->VStop();
    __super::Dispose();
    m_state = ClientState_Stopped;
    return S_OK;
}

void CClient::GetNetworkActivity(uint64* pLogicalBytesSent, uint64* pPhysicalBytesSent, uint64* pLogicalBytesReceived, uint64* pPhysicalBytesReceived)
{
    m_connection->VGetActivity(pLogicalBytesSent, pPhysicalBytesSent, pLogicalBytesReceived, pPhysicalBytesReceived);
}

CClient::~CClient()
{
    delete m_connection;
    SetEvent(m_stateEvent);
    CloseHandle(m_stateEvent);
    m_stateEvent = nullptr;
}

#pragma region session information

void CClient::SendTwoFactorResponse(const FxTwoFactorReason reason, const std::string& otp)
{
    m_sender->VSendTwoFactorResponse(reason, otp);
}

CFxSessionInfo CClient::GetSessionInfo(const size_t timeoutInMilliseconds)
{
    Waiter<CFxSessionInfo> waiter(static_cast<uint32>(timeoutInMilliseconds), cExternalSynchCall, *this);

    m_sender->VSendGetSessionInfo(waiter.Id());

    CFxSessionInfo result = waiter.WaitForResponse();
    return result;
}

void CClient::VLogon(const CFxEventInfo& eventInfo, const string& protocolVersion)
{
    {
        CLock lock(m_dataSynchronizer);
        m_protocolVersion = protocolVersion;
        SetEvent(m_stateEvent);
    }

    __super::VLogon(eventInfo, protocolVersion);

    AfterLogon();
}

void CClient::VTwoFactorAuth(const CFxEventInfo& eventInfo, const FxTwoFactorReason reason, const std::string& text, const CDateTime& expire)
{
    __super::VTwoFactorAuth(eventInfo, reason, text, expire);

    if (reason == FxTwoFactorReason_ServerSuccess)
        AfterLogon();
}

void CClient::AfterLogon()
{
}

void CClient::VSessionInfo(const CFxEventInfo& eventInfo, CFxSessionInfo& sessionInfo)
{
    if (eventInfo.IsNotification())
    {
        m_cache.UpdateSessionInfo(sessionInfo);
    }
    __super::VSessionInfo(eventInfo, sessionInfo);
}

void CClient::VLogout(const CFxEventInfo& eventInfo, const FxLogoutReason reason, const string& description)
{
    {
        CLock lock(m_dataSynchronizer);
        m_protocolVersion.clear();
        ResetEvent(m_stateEvent);
    }
    __super::VLogout(eventInfo, reason, description);
}

string CClient::GetProtocolVersion() const
{
    CLock lock(m_dataSynchronizer);
    return m_protocolVersion;
}

bool CClient::CheckProtocolVersion(const CProtocolVersion& requiredVersion) const
{
    auto version = GetProtocolVersion();
    if (version.empty())
        return true;

    CProtocolVersion currentVersion(version);
    return requiredVersion <= currentVersion;
}

CFxFileChunk CClient::GetFileChunk(const string& fileId, uint32 chunkId, const size_t timeoutInMilliseconds)
{
    Waiter<CFxFileChunk> waiter(static_cast<uint32>(timeoutInMilliseconds), cExternalSynchCall, fileId, *this);

    m_sender->VSendGetFileChunk(waiter.Id(), fileId, chunkId);

    CFxFileChunk result = waiter.WaitForResponse();
    return result;
}

#pragma endregion
