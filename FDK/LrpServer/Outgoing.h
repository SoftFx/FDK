#pragma once


class CChannel;

class COutgoing : public ILrpChannel
{
public:
    COutgoing(CChannel& channel);
    virtual ~COutgoing();
public:
    void Initialize(const string& remoteSignature);
public:
    virtual void Initialize(MemoryBuffer& buffer);
    virtual HRESULT Invoke(uint16 componentId, uint16 methodId, MemoryBuffer& buffer);
    virtual bool IsSupported(uint16 componentId) const;
    virtual bool IsSupported(uint16 componentId, uint16 methodId) const;
public:
    HRESULT DoInvoke(const ptrdiff_t key, uint16 componentId, uint16 methodId, MemoryBuffer& buffer);
private:
    COutgoing(const COutgoing&);
    COutgoing& operator = (const COutgoing&);
public:
    void SendHeartBeatRequest();
    void SendLogon(const string& protocolVersion);
    void SendLogout(const FxLogoutReason reason, const string& description);
    void SendSessionInfo(const string& requestId, const CFxSessionInfo& sessionInfo);
    void SendCurrenciesInfo(const string& requestId, const vector<CFxCurrencyInfo>& symbolsInfo);
    void SendSymbolsInfo(const string& requestId, const vector<CFxSymbolInfo>& symbolsInfo);
    void SendSubscribeToQuotesResponse(const string& requestId, const int32 status, const string& message);
    void SendUnsubscribeQuotesResponse(const string& requestId, const int32 status, const string& message);
    void SendQuotesHistoryVersion(const string& requestId, const int32 version);
    void SendQuote(const ptrdiff_t key, const CFxQuote& quote);
    void SendMarketHistoryMetadataResponse(const string& requestId, const int32 status, const string& field);
    void SendMarketHistoryMetadataReject(const string& requestId, const int32 status, const string& field);
    void SendDataHistoryResponse(const string& requestId, const CFxDataHistoryResponse& response);
    void SendDataHistoryReject(const string& requestId, FxMarketHistoryRejectType rejectType, const string& rejectReason);
    void SendFileChunk(const string& requestId, const CFxFileChunk& chunk);
    void SendNotification(const CNotification& notification);
    void SendReject(const string& rejectReason, const string& rejectTag);
public:
    void SendSimpleCodec();
private:
    CChannel& m_channel;
    CTranslators m_translators;
};