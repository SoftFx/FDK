#include "stdafx.h"
#include "Server.h"
#include "ChannelSharedAccessor.h"
#include "Channel.h"


CServer::CServer(CChannelsPool& channels, int port, const string& certificateFilename, const string& certificatePassword, void* handler)
    : m_port(port)
    , m_certificateFilename(certificateFilename)
    , m_certificatePassword(certificatePassword)
    , m_channels(channels)
    , m_logger(channels.GetLogger())
    , m_acceptor(port, !m_certificateFilename.empty())
    , m_proxy(handler)
    , m_continue(true)
    , m_thread(nullptr)
{
    CLogStream()<<"CServer::CServer(port = "<<port<<", certificateFilename = "<<certificateFilename<<", certificatePassword = "<<certificatePassword<<")">>m_logger;
}

CServer::~CServer()
{
    CLogStream()<<"CServer::~CServer(): stopping">>m_logger;
    Stop();
    CLogStream()<<"CServer::~CServer(): stopped">>m_logger;
}

void CServer::Start()
{
    CLogStream()<<"CServer::Start(): starting">>m_logger;
    if (nullptr == m_thread)
    {
        m_thread = reinterpret_cast<HANDLE>(_beginthreadex(nullptr, 0, &CServer::ThreadFunction, this, 0, nullptr));
        if (nullptr == m_thread)
        {
            throw runtime_error("Can not start a new thread");
        }
    }
    CLogStream()<<"CServer::Start(): started">>m_logger;
}

void CServer::Stop()
{
    CLogStream()<<"CServer::Stop(): stopping">>m_logger;
    if (nullptr != m_thread)
    {
        m_continue = false;
        m_acceptor.Finalize();
        WaitForSingleObject(m_thread, INFINITE);
        CloseHandle(m_thread);
        m_thread = nullptr;
    }
    CLogStream()<<"CServer::Stop(): stopped">>m_logger;
}

CLocalServerHandlerProxy& CServer::GetProxy()
{
    return m_proxy;
}

HRESULT CServer::SendMessage(const uint64 id, const CMessage& message)
{
    CChannelSharedAccessor accessor(id, m_channels);
    CChannel* pChannel = accessor.GetChannel();
    if (nullptr == pChannel)
    {
        return E_POINTER;
    }
    try
    {
        const HRESULT result = pChannel->SendMessage(message);
        return result;
    }
    catch (const exception&)
    {
        return E_FAIL;
    }
}

unsigned __stdcall CServer::ThreadFunction(void* arg)
{
    CServer* pServer = reinterpret_cast<CServer*>(arg);
    __try
    {
        pServer->Loop();
        return 0;
    }
    __except(EXCEPTION_EXECUTE_HANDLER)
    {
        return 1;
    }
}

void CServer::Loop()
{
    const char* ceritificateFileName = m_certificateFilename.c_str();
    const char* password = m_certificatePassword.c_str();


    for (; m_continue; )
    {
        const SOCKET client = m_acceptor.Accept(ceritificateFileName, password);
        if (INVALID_SOCKET != client)
        {
            CreateNewChannel(client);
        }
        else
        {
            if (m_continue)
            {
                Sleep(64);
            }
        }
    }
}

void CServer::CreateNewChannel(SOCKET client)
{
    EnableKeepAlive(client);

    try
    {
        m_channels.AddConnection(client, *this);
    }
    catch (const std::exception&)
    {
        shutdown(client, SD_BOTH);
        closesocket(client);
    }
}

void CServer::EndConnection(const uint64 id, const int32 status)
{
    __try
    {
        DoEndConnection(id, status);
    }
    __except(EXCEPTION_EXECUTE_HANDLER)
    {
    }
}

void CServer::DoEndConnection(const uint64 id, const int32 status)
{
    CChannelSharedAccessor accessor(id, m_channels);
    CChannel* pChannel = accessor.GetChannel();
    if (nullptr != pChannel)
    {
        pChannel->Connect(status);
    }
}

void CServer::BeginLogon(const uint64 id, const string& address, const string& username, const string& password)
{
    __try
    {
        DoBeginLogon(id, address, username, password);
    }
    __except(EXCEPTION_EXECUTE_HANDLER)
    {
    }
}

void CServer::DoBeginLogon(const uint64 id, const string& address, const string& username, const string& password)
{
    m_proxy.BeginLogonRequest(id, address, m_port, username, password);
}

void CServer::EndLogon(const uint64 id, const HRESULT status, const string& message)
{
    __try
    {
        DoEndLogon(id, status, message);
    }
    __except(EXCEPTION_EXECUTE_HANDLER)
    {
    }
}

void CServer::DoEndLogon(const uint64 id, const HRESULT status, const string& message)
{
    CChannelSharedAccessor accessor(id, m_channels);
    CChannel* pChannel = accessor.GetChannel();
    if (nullptr != pChannel)
    {
        pChannel->Logon(status, message);
    }
}

void CServer::SendTwoFactorAuth(int64 id, const FxTwoFactorReason reason, const string& text, const CDateTime& expire)
{
    CChannelSharedAccessor accessor(id, m_channels);
    CChannel* pChannel = accessor.GetChannel();
    if (nullptr != pChannel)
    {
        pChannel->Outgoing().SendTwoFactorAuth(reason, text, expire);
    }
}

void CServer::SendSessionInfo(int64 id, const string& requestId, const CFxSessionInfo& sessionInfo)
{
    CChannelSharedAccessor accessor(id, m_channels);
    CChannel* pChannel = accessor.GetChannel();
    if (nullptr != pChannel)
    {
        pChannel->Outgoing().SendSessionInfo(requestId, sessionInfo);
    }
}

void CServer::SendCurrenciesInfo(int64 id, const string& requestId, const vector<CFxCurrencyInfo>& currenciesInfo)
{
    CChannelSharedAccessor accessor(id, m_channels);
    CChannel* pChannel = accessor.GetChannel();
    if (nullptr != pChannel)
    {
        pChannel->Outgoing().SendCurrenciesInfo(requestId, currenciesInfo);
    }
}

void CServer::SendSymbolsInfo(int64 id, const string& requestId, const vector<CFxSymbolInfo>& symbolsInfo)
{
    CChannelSharedAccessor accessor(id, m_channels);
    CChannel* pChannel = accessor.GetChannel();
    if (nullptr != pChannel)
    {
        pChannel->Outgoing().SendSymbolsInfo(requestId, symbolsInfo);
    }
}

void CServer::SendQuotesSubscriptionConfirm(int64 id, const string& requestId)
{
    CChannelSharedAccessor accessor(id, m_channels);
    CChannel* pChannel = accessor.GetChannel();
    if (nullptr != pChannel)
    {
        pChannel->Outgoing().SendSubscribeToQuotesResponse(requestId, 0, string());
    }
}

void CServer::SendQuotesSubscriptionReject(int64 id, const string& requestId, const string& message)
{
    CChannelSharedAccessor accessor(id, m_channels);
    CChannel* pChannel = accessor.GetChannel();
    if (nullptr != pChannel)
    {
        pChannel->Outgoing().SendSubscribeToQuotesResponse(requestId, E_FAIL, message);
    }
}

void CServer::SendQuotesHistoryVersion(int64 id, const string& requestId, const int32 version)
{
    CChannelSharedAccessor accessor(id, m_channels);
    CChannel* pChannel = accessor.GetChannel();
    if (nullptr != pChannel)
    {
        pChannel->Outgoing().SendQuotesHistoryVersion(requestId, version);
    }
}

void CServer::SendQuote(int64 id, const CFxQuote& quote)
{
    CChannelSharedAccessor accessor(id, m_channels);
    CChannel* pChannel = accessor.GetChannel();
    if (nullptr != pChannel)
    {
        pChannel->SendQuote(quote);
    }
}

void CServer::SendMarketHistoryMetadataResponse(int64 id, const string& requestId, int32 status, const string& field)
{
    CChannelSharedAccessor accessor(id, m_channels);
    CChannel* pChannel = accessor.GetChannel();
    if (nullptr != pChannel)
    {
        pChannel->Outgoing().SendMarketHistoryMetadataResponse(requestId, status, field);
    }
}

void CServer::SendMarketHistoryMetadataReject(int64 id, const string& requestId, int32 status, const string& field)
{
    CChannelSharedAccessor accessor(id, m_channels);
    CChannel* pChannel = accessor.GetChannel();
    if (nullptr != pChannel)
    {
        pChannel->Outgoing().SendMarketHistoryMetadataReject(requestId, status, field);
    }
}

void CServer::SendDataHistoryResponse(int64 id, const string& requestId, const CFxDataHistoryResponse& response)
{
    CChannelSharedAccessor accessor(id, m_channels);
    CChannel* pChannel = accessor.GetChannel();
    if (nullptr != pChannel)
    {
        pChannel->Outgoing().SendDataHistoryResponse(requestId, response);
    }
}

void CServer::SendDataHistoryReject(int64 id, const string& requestId, FxMarketHistoryRejectType rejectType, const string& rejectReason)
{
    CChannelSharedAccessor accessor(id, m_channels);
    CChannel* pChannel = accessor.GetChannel();
    if (nullptr != pChannel)
    {
        pChannel->Outgoing().SendDataHistoryReject(requestId, rejectType, rejectReason);
    }
}

void CServer::SendFileChunk(int64 id, const string& requestId, const CFxFileChunk& chunk)
{
    CChannelSharedAccessor accessor(id, m_channels);
    CChannel* pChannel = accessor.GetChannel();
    if (nullptr != pChannel)
    {
        pChannel->Outgoing().SendFileChunk(requestId, chunk);
    }
}

void CServer::SendNotification(int64 id, const CNotification& notification)
{
    CChannelSharedAccessor accessor(id, m_channels);
    CChannel* pChannel = accessor.GetChannel();
    if (nullptr != pChannel)
    {
        pChannel->Outgoing().SendNotification(notification);
    }
}

void CServer::SendBusinessReject(int64 id, const string& rejectReason, const string& rejectTag)
{
    CChannelSharedAccessor accessor(id, m_channels);
    CChannel* pChannel = accessor.GetChannel();
    if (nullptr != pChannel)
    {
        pChannel->Outgoing().SendBusinessReject(rejectReason, rejectTag);
    }
}

void CServer::ShutdownConnection(const uint64 id)
{
    __try
    {
        m_proxy.BeginShutdownConnectionNotification(id);
    }
    __except (EXCEPTION_EXECUTE_HANDLER)
    {
    }
}

CLrpLogger& CServer::GetLogger()
{
    return m_logger;
}
