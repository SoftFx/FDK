#include "stdafx.h"
#include "Client.h"
#include "Waiter.h"
#include "LlApi.h"
#include "ConnectionBuilder.h"


const string cExternalSynchCall = "ES";
const string cInternalASynchCall = "IA";


CClient::CClient(CDataCache& cache, const string& connectionString)
    : m_sender(nullptr)
    , m_cache(cache)
    , m_state(ClientState_Stopped)
    , m_connection(nullptr)
    , m_afterLogonInvoked(false)
{
    m_stateEvent = CreateEvent(nullptr, TRUE, FALSE, nullptr);
    m_connection = CreateConnection(connectionString);
    m_connection->VReceiver(this);
    m_sender = m_connection->VSender();
}

CClient::~CClient()
{
    delete m_connection;
    SetEvent(m_stateEvent);
    CloseHandle(m_stateEvent);
    m_stateEvent = nullptr;
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
    const HRESULT result = CFxQueue::Construct();
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
    CFxQueue::ReleaseQueue();
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
    CFxQueue::Dispose();
    m_state = ClientState_Stopped;
    return S_OK;
}

void CClient::GetNetworkActivity(uint64* pLogicalBytesSent, uint64* pPhysicalBytesSent, uint64* pLogicalBytesReceived, uint64* pPhysicalBytesReceived)
{
    m_connection->VGetActivity(pLogicalBytesSent, pPhysicalBytesSent, pLogicalBytesReceived, pPhysicalBytesReceived);
}

const string CClient::NextIdIfEmpty(const string& prefix, const string& externalId)
{
    if (!externalId.empty())
    {
        return externalId;
    }
    return NextId(prefix);
}

const string CClient::NextId(const string& prefix)
{
    return m_idGenerator.Next(prefix);
}

const string CClient::NextId()
{
    return NextId(string());
}

void CClient::RegisterWaiter(const type_info& info, const string& id, IWaiter* pWaiter)
{
    m_synchInvoker.RegisterWaiter(info, id, pWaiter);
}

void CClient::ReleaseWaiter(const type_info& info, const string& id)
{
    m_synchInvoker.ReleaseWaiter(info, id);
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

string CClient::GetProtocolVersion() const
{
    CLock lock(m_dataSynchronizer);
    return m_protocolVersion;
}

CFxFileChunk CClient::GetFileChunk(const string& fileId, uint32 chunkId, const size_t timeoutInMilliseconds)
{
    Waiter<CFxFileChunk> waiter(static_cast<uint32>(timeoutInMilliseconds), cExternalSynchCall, fileId, *this);

    m_sender->VSendGetFileChunk(waiter.Id(), fileId, chunkId);

    CFxFileChunk result = waiter.WaitForResponse();
    return result;
}

void CClient::VLogon(const CFxEventInfo& eventInfo, const string& protocolVersion, bool twofactor)
{
    {
        CLock lock(m_dataSynchronizer);
        m_afterLogonInvoked = false;
        m_protocolVersion = protocolVersion;
        SetEvent(m_stateEvent);
    }

    CFxMessage message(FX_MSG_LOGON, eventInfo);
    message.Data = new CFxMsgLogon(protocolVersion);
    ProcessMessage(message);

    if (!twofactor)
        AfterLogon();
}

void CClient::VTwoFactorAuth(const CFxEventInfo& eventInfo, const FxTwoFactorReason reason, const std::string& text, const CDateTime& expire)
{
    CFxMessage message(FX_MSG_TWO_FACTOR_AUTH, eventInfo);
    message.Data = new CFxMsgTwoFactorAuth(CFxTwoFactorAuth(reason, text, expire));
    ProcessMessage(message);

    if ((reason == FxTwoFactorReason_ServerSuccess) && !m_afterLogonInvoked)
        AfterLogon();
}

void CClient::VLogout(const CFxEventInfo& eventInfo, const FxLogoutReason reason, const string& description)
{
    {
        CLock lock(m_dataSynchronizer);
        m_protocolVersion.clear();
        ResetEvent(m_stateEvent);
    }

    CFxMessage message(FX_MSG_LOGOUT, eventInfo);
    int32 code = GetLastError();
    message.Data = new CFxMsgLogout(reason, code, description);
    ProcessMessage(message);

    m_synchInvoker.Disconnect();
}

void CClient::VBusinessReject(const CFxEventInfo& eventInfo)
{
    m_synchInvoker.Response(eventInfo);
}

void CClient::VTick(const CFxEventInfo& /*eventInfo*/, const CFxQuote& /*quotes*/)
{
}

void CClient::VSessionInfo(const CFxEventInfo& eventInfo, CFxSessionInfo& sessionInfo)
{
    if (eventInfo.IsNotification())
    {
        m_cache.UpdateSessionInfo(sessionInfo);

        CFxMessage message(FX_MSG_SESSION_INFO, eventInfo);
        message.Data = new CFxMsgSessionInfo(sessionInfo);
        ProcessMessage(message);
    }
    else
    {
        m_synchInvoker.Response(eventInfo, sessionInfo);
    }
}

void CClient::VAccountInfo(const CFxEventInfo& eventInfo, CFxAccountInfo& accountInfo)
{
    m_synchInvoker.Response(eventInfo, accountInfo);
}

void CClient::VGetCurrencies(const CFxEventInfo& eventInfo, const vector<CFxCurrencyInfo>& currencies)
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

void CClient::VGetSupportedSymbols(const CFxEventInfo& eventInfo, const vector<CFxSymbolInfo>& symbols)
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

void CClient::VSubscribeToQuotes(const CFxEventInfo& eventInfo, HRESULT status)
{
    m_synchInvoker.Response(eventInfo, status);
}

void CClient::VClosePositions(const CFxEventInfo& eventInfo, CFxClosePositionsResponse& response)
{
    m_synchInvoker.Response(eventInfo, response);
}

void CClient::VExecution(const CFxEventInfo& eventInfo, CFxExecutionReport& executionReport)
{
    if (!eventInfo.IsInternalAsynchCall() && (FxExecutionType_OrderStatus != executionReport.ExecutionType))
    {
        CFxMessage message(FX_MSG_EXECUTION_REPORT, eventInfo);
        CFxExecutionReport temp = executionReport;
        message.Data = new CFxMsgExecutionReport(temp);
        ProcessMessage(message);
    }
    m_synchInvoker.Response(eventInfo, executionReport);
}

void CClient::VDataHistoryResponse(const CFxEventInfo& eventInfo, CFxDataHistoryResponse& response)
{
    m_synchInvoker.Response(eventInfo, response);
}

void CClient::VTradeHistoryResponse(const CFxEventInfo& eventInfo, CFxTradeHistoryResponse& response)
{
    m_synchInvoker.Response(eventInfo, response);
}

void CClient::VTradeHistoryReport(const CFxEventInfo& eventInfo, CFxTradeHistoryReport& report)
{
    m_synchInvoker.Response(eventInfo, report);
}

void CClient::VFileChunk(const CFxEventInfo& eventInfo, CFxFileChunk& chunk)
{
    m_synchInvoker.Response(eventInfo, chunk);
}

void CClient::VMetaInfoFile(const CFxEventInfo& eventInfo, string& file)
{
    m_synchInvoker.Response(eventInfo, file);
}

void CClient::VGetTradeTransactionReportsAndSubscribeToNotifications(const CFxEventInfo& info, const int32 curReportsNumber, const int32 totReportsNumber, const bool endOfStream)
{
    tuple<int32, int32, bool> response(curReportsNumber, totReportsNumber, endOfStream);
    m_synchInvoker.Response(info, response);
}

void CClient::VTradeTransactionReport(const CFxEventInfo& info, CFxTradeTransactionReport& report)
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

void CClient::VUnsubscribeTradeTransactionReportsNotifications(const CFxEventInfo& info)
{
    HRESULT response = info.Status;
    m_synchInvoker.Response(info, response);
}

void CClient::VPositionReport(const CFxEventInfo& info, CFxPositionReport& positionReport)
{
    CFxMessage message(FX_MSG_POSITION_REPORT, info);
    CFxPositionReport temp = positionReport;
    message.Data = new CFxMsgPositionReport(temp);
    ProcessMessage(message);
}

void CClient::VNotify(const CFxEventInfo& eventInfo, const CNotification& notification)
{
    CFxMessage message(FX_MSG_NOTIFICATION, eventInfo);
    CNotification temp(notification);
    message.Data = new CFxMsgNotification(temp);
    ProcessMessage(message);
}

void CClient::VQuotesHistoryResponse(const CFxEventInfo& /*eventInfo*/, const int /*version*/)
{
}

void CClient::AfterLogon()
{
    CLock lock(m_dataSynchronizer);
    m_afterLogonInvoked = true;
}

bool CClient::CheckProtocolVersion(const CProtocolVersion& requiredVersion) const
{
    auto version = GetProtocolVersion();
    if (version.empty())
        return true;

    CProtocolVersion currentVersion(version);
    return requiredVersion <= currentVersion;
}

#pragma endregion
