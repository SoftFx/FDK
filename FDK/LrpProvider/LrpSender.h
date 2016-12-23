#pragma once



class CLrpConnection;

class CLrpSender : public ISender, private ILrpChannel
{
public:
    CLrpSender(CLrpConnection& connection);
private:
    CLrpSender(const CLrpSender&);
    CLrpSender& operator = (const CLrpSender&);
public:
    void Initialize(const string& remoteSignature);
public:
    virtual void VSendTwoFactorResponse(const FxTwoFactorReason reason, const std::string& otp);
    virtual void VSendGetCurrencies(const string& id);
    virtual void VSendGetSupportedSymbols(const string& id);
    virtual void VSendGetSessionInfo(const string& id);
    virtual void VSendGetTradeServerInfo(const string& id);
    virtual void VSendGetAccountInfo(const string& id);
    virtual void VSendSubscribeToQuotes(const string& id, const vector<string>& symbols, int32 depth);
    virtual void VSendUnsubscribeQuotes(const string& id, const vector<string>& symbols);
    virtual void VSendDeleteOrder(const string& id, const string& orderID, const string& clientID, int32 side);
    virtual void VSendCloseOrder(const string& id, const string& orderId, Nullable<double> closingVolume);
    virtual void VSendCloseByOrders(const string& id, const string& firstOrderId, const string& secondOrderId);
    virtual void VSendCloseAllOrders(const string& id);
    virtual void VSendGetOrders(const string& id);
    virtual void VSendOpenNewOrder(const string& id, const CFxOrder& request);
    virtual void VSendModifyOrder(const string& id, const CFxOrder& request);
    virtual void VSendDataHistoryRequest(const string& id, const CFxDataHistoryRequest& request);
    virtual void VSendGetFileChunk(const string& id, const string& fileId, const uint32 chunkId);
    virtual void VSendGetBarsHistoryMetaInfoFile(const string& id, const string& symbol, int32 priceType, const string& period);
    virtual void VSendGetTicksHistoryMetaInfoFile(const string& id, const string& symbol, bool includeLevel2);
    virtual void VSendGetTradeTransactionReportsAndSubscribeToNotifications(const string& id, FxTimeDirection direction, bool subscribe, const Nullable<CDateTime>& from, const Nullable<CDateTime>& to, uint32 bufferSize, const string& position);
    virtual void VSendUnsubscribeTradeTransactionReports(const string& id);
    virtual void VSendPositionReportRequest(const string& id, const string& account);
    virtual void VSendQuotesHistoryRequest(const string& id);
public:
    void SendHeartBeatResponse();
private:
    virtual void Initialize(MemoryBuffer& buffer);
    virtual HRESULT Invoke(uint16 componentId, uint16 methodId, MemoryBuffer& buffer);
    virtual bool IsSupported(uint16 componentId) const;
    virtual bool IsSupported(uint16 componentId, uint16 methodId) const;
private:
    CLrpConnection& m_connection;
    CTranslators m_translators;
};