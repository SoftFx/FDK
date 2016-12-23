#ifndef __Core_ISender__
#define __Core_ISender__
#include "FxDataHistoryRequest.h"
#include "FxOrder.h"


class ISender
{
public:
    virtual void VSendTwoFactorResponse(const FxTwoFactorReason reason, const std::string& otp) = 0;
    virtual void VSendGetCurrencies(const string& id) = 0;
    virtual void VSendGetSupportedSymbols(const string& id) = 0;
    virtual void VSendGetSessionInfo(const string& id) = 0;
    virtual void VSendGetTradeServerInfo(const string& id) = 0;
    virtual void VSendGetAccountInfo(const string& id) = 0;
    virtual void VSendSubscribeToQuotes(const string& id, const vector<string>& symbols, int32 depth) = 0;
    virtual void VSendUnsubscribeQuotes(const string& id, const vector<string>& symbols) = 0;
    virtual void VSendDeleteOrder(const string& id, const string& orderID, const string& clientID, int32 side) = 0;
    virtual void VSendCloseOrder(const string& id, const string& orderId, Nullable<double> closingVolume) = 0;
    virtual void VSendCloseByOrders(const string& id, const string& firstOrderId, const string& secondOrderId) = 0;
    virtual void VSendCloseAllOrders(const string& id) = 0;
    virtual void VSendGetOrders(const string& id) = 0;
    virtual void VSendOpenNewOrder(const string& id, const CFxOrder& request) = 0;
    virtual void VSendModifyOrder(const string& id, const CFxOrder& request) = 0;
    virtual void VSendDataHistoryRequest(const string& id, const CFxDataHistoryRequest& request) = 0;
    //virtual void VSendGetTradeHistory(const string& id, const Nullable<CDateTime>& from, const Nullable<CDateTime>& to) = 0;
    virtual void VSendGetFileChunk(const string& id, const string& fileId, const uint32 chunkId) = 0;
    virtual void VSendGetBarsHistoryMetaInfoFile(const string& id, const string& symbol, int32 priceType, const string& period) = 0;
    virtual void VSendGetTicksHistoryMetaInfoFile(const string& id, const string& symbol, bool includeLevel2) = 0;
    virtual void VSendGetTradeTransactionReportsAndSubscribeToNotifications(const string& id, FxTimeDirection direction, bool subscribe, const Nullable<CDateTime>& from, const Nullable<CDateTime>& to, uint32 bufferSize, const string& position) = 0;
    virtual void VSendUnsubscribeTradeTransactionReports(const string& id) = 0;
    virtual void VSendPositionReportRequest(const string& id, const string& account) = 0;
    virtual void VSendQuotesHistoryRequest(const string& id) = 0;
    virtual ~ISender() {};
};
#endif
