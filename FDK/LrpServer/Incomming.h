#pragma once


class CChannel;

class CLocalServerHandlerProxy;
class CIncomming
{
public:
    CIncomming(CChannel& channel, CLocalServerHandlerProxy& proxy);
private:
    CIncomming(const CIncomming&);
    CIncomming& operator = (const CIncomming&);
public:
    static const char* Signature();
public:
    CIncomming& GetServer();
public:
    void OnHeartBeatRequest();
    void OnHeartBeatResponse();
    void OnTwoFactorAuthRequest(const FxTwoFactorReason reason, const std::string& otp);
    void OnSessionInfoRequest(const string& requestId);
    void OnCurrenciesInfoRequest(const string& requestId);
    void OnSymbolsInfoRequest(const string& requestId);
    void OnSubscribeToQuotesRequest(const string& requestId, const vector<string>& symbols, int32 marketDepth);
    void OnUnsubscribeQuotesRequest(const string& requestId, const vector<string>& symbols);
    void OnComponentsInfoRequest(const string& requestId, const int clientVersion);
    void OnDataHistoryRequest(const string& requestId, const CFxDataHistoryRequest& request);
    void OnFileChunkRequest(const string& requestId, const string& fileId, const uint32 chunkId);
    void OnBarsHistoryMetaInfoFileRequest(const string& requestId, const string& symbol, int32 priceType, const string& period);
    void OnQuotesHistoryMetaInfoFileRequest(const string& requestId, const string& symbol, bool includeLevel2);
public:
    HRESULT Process(MemoryBuffer& buffer);
private:
    HRESULT DoProcess(MemoryBuffer& buffer);
private:
    CChannel& m_channel;
    CLocalServerHandlerProxy& m_proxy;
    const uint64 m_id;
};