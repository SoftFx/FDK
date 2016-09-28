#pragma once


class CLocalServerHandlerProxy
{
public:
    CLocalServerHandlerProxy(void* handle);
    ~CLocalServerHandlerProxy();
private:
    CLocalServerHandlerProxy(const CLocalServerHandlerProxy&);
    CLocalServerHandlerProxy& operator = (const CLocalServerHandlerProxy&);
public:
    void BeginNewConnectionRequest(const uint64 id, const string& address, const int port);
    void BeginLogonRequest(const uint64 id, const string& address, const int port, const string& username, const string& password, const string& deviceid, const string& appsessionid);
    void BeginTwoFactorAuthResponse(const uint64 id, const FxTwoFactorReason reason, const std::string& otp);
    void BeginShutdownConnectionNotification(const uint64 id);
    void BeginCurrenciesInformationRequest(const uint64 id, const string& requestId);
    void BeginSymbolsInformationRequest(const uint64 id, const string& requestId);
    void BeginSessionInformationRequest(const uint64 id, const string& requestId);
    void BeginSubscribeToQuotesRequest(const uint64 id, const string& requestId, const vector<string>& symbols, int32 marketDepth);
    void BeginUnsubscribeQuotesRequest(const uint64 id, const string& requestId, const vector<string>& symbols);
    void BeginComponentsInformationRequest(const uint64 id, const string& requestId, const int clientVersion);
    void BeginDataHistoryRequest(const uint64 id, const string& requestId, const CFxDataHistoryRequest& request);
    void BeginFileChunkRequest(const uint64 id, const string& requestId, const string& fileId, const uint32 chunkId);
    void BeginBarsHistoryMetaInformationFileRequest(const uint64 id, const string& requestId, const string& symbol, int32 priceType, const string& period);
    void BeginQuotesHistoryMetaInformationFileRequest(const uint64 id, const string& requestId, const string& symbol, bool includeLevel2);
private:
    void* m_handle;
    CLrpLocalClient m_client;
};