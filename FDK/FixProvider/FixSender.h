#ifndef __FixProvider_Fix_Sender__
#define __FixProvider_Fix_Sender__

#include "FixVersion.h"

class CFixSender : public ISender
{
public:
    CFixSender();
    void SessionID(const FIX::SessionID& sessionId);
    void SendVersion(const CFixVersion& version);
    void AppId(const std::string& appId);
#ifdef LOG_PERFORMANCE
    void setLogger(Performance::Logger* logger);
#endif
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
    virtual void VSendOpenNewOrder(const string& id, const CFxOrder& required);
    virtual void VSendModifyOrder(const string& id, const CFxOrder& request);
    virtual void VSendDataHistoryRequest(const string& id, const CFxDataHistoryRequest& request);
    virtual void VSendGetTradeHistory(const string& id, const Nullable<CDateTime>& from, const Nullable<CDateTime>& to);
    virtual void VSendGetFileChunk(const string& id, const string& fileId, const uint32 chunkId);
    virtual void VSendGetBarsHistoryMetaInfoFile(const string& id, const string& symbol, int32 priceType, const string& period);
    virtual void VSendGetTicksHistoryMetaInfoFile(const string& id, const string& symbol, bool includeLevel2);
    virtual void VSendGetTradeTransactionReportsAndSubscribeToNotifications(const string& id, FxTimeDirection direction, bool subscribe, const Nullable<CDateTime>& from, const Nullable<CDateTime>& to, uint32 bufferSize, const string& position);
    virtual void VSendUnsubscribeTradeTransactionReports(const string& id);
    virtual void VSendPositionReportRequest(const string& id, const string& account);
    virtual void VSendQuotesHistoryRequest(const string& id);
private:
    void SendMessage(FIX::Message& message);
private:
    FIX::SessionID m_sessionID;
    CFixVersion m_version;
    std::string m_appId;
#ifdef LOG_PERFORMANCE
    Performance::Logger* logger_;
#endif
};
#endif
