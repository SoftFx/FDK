#pragma once
#include "Parameters.h"
#include "ChannelsPool.h"
#include "Incomming.h"
#include "LocalServerHandlerProxy.h"


class CMessage;
class CLrpStServer;

class CServer
{
public:
    CServer(CChannelsPool& channels, int port, const string& certificateFilename, const string& certificatePassword, void* handler);
    ~CServer();
private:
    CServer(const CServer&);
    CServer& operator = (const CServer&);
public:
    void Start();
    void Stop();
public:
    void SendSessionInfo(int64 id, const string& requestId, const CFxSessionInfo& sessionInfo);
    void SendCurrenciesInfo(int64 id, const string& requestId, const vector<CFxCurrencyInfo>& currenciesInfo);
    void SendSymbolsInfo(int64 id, const string& requestId, const vector<CFxSymbolInfo>& symbolsInfo);
    void SendQuotesSubscriptionConfirm(int64 id, const string& requestId);
    void SendQuotesSubscriptionReject(int64 id, const string& requestId, const string& message);
    void SendQuotesHistoryVersion(int64 id, const string& requestId, const int32 version);
    void SendQuote(int64 id, const CFxQuote& quote);
    void SendMarketHistoryMetadataResponse(int64 id, const string& requestId, int32 status, const string& field);
    void SendMarketHistoryMetadataReject(int64 id, const string& requestId, int32 status, const string& field);
    void SendDataHistoryResponse(int64 id, const string& requestId, const CFxDataHistoryResponse& response);
    void SendDataHistoryReject(int64 id, const string& requestId, FxMarketHistoryRejectType rejectType, const string& rejectReason);
    void SendFileChunk(int64 id, const string& requestId, const CFxFileChunk& chunk);
    void SendNotification(int64 id, const CNotification& notification);
    void SendBusinessReject(int64 id, const string& rejectReason, const string& rejectTag);
public:
    CLocalServerHandlerProxy& GetProxy();
    CLrpLogger& GetLogger();
public:
    void BeginConnection(const uint64 id, const string& address);
    void EndConnection(const uint64 id, const int32 status);
    void BeginLogon(const uint64 id, const string& address, const string& username, const string& password);
    void EndLogon(const uint64 id, const HRESULT status, const string& message);
    void ShutdownConnection(const uint64 id);
public:
    void BeginInvoke(const uint64 id, MemoryBuffer& buffer);
    HRESULT SendMessage(const uint64 id, const CMessage& message);
private:
    void DoEndConnection(const uint64 id, const int32 status);
    void DoBeginLogon(const uint64 id, const string& address, const string& username, const string& password);
    void DoEndLogon(const uint64 id, const HRESULT status, const string& message);
private:
    static unsigned __stdcall ThreadFunction(void* arg);
    void Loop();
    void CreateNewChannel(SOCKET client);
private:
    const int m_port;
    const string m_certificateFilename;
    const string m_certificatePassword;
    CChannelsPool& m_channels;
    CLrpLogger& m_logger;
    CAcceptor m_acceptor;
    CLocalServerHandlerProxy m_proxy;
private:
    volatile bool m_continue;
    HANDLE m_thread;
};