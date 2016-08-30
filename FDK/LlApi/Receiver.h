#ifndef __Native_Receiver__
#define __Native_Receiver__

#include "FxQueue.h"
#include "ClientState.h"
#include "SynchInvoker.h"
#include "IdGenerator.h"


class CReceiver : public CFxQueue, public IReceiver
{
public:
    virtual void VLogon(const CFxEventInfo& eventInfo, const string& protocolVersion);
    virtual void VTwoFactorAuth(const CFxEventInfo& eventInfo, const FxTwoFactorReason reason, const std::string& text, const CDateTime& expire);
    virtual void VLogout(const CFxEventInfo& eventInfo, const FxLogoutReason reason, const string& description);
    virtual void VBusinessReject(const CFxEventInfo& eventInfo);
    virtual void VTick(const CFxEventInfo& eventInfo, const CFxQuote& quotes);
    virtual void VSessionInfo(const CFxEventInfo& eventInfo, CFxSessionInfo& sessionInfo);
    virtual void VAccountInfo(const CFxEventInfo& eventInfo, CFxAccountInfo& accountInfo);
    virtual void VGetCurrencies(const CFxEventInfo& eventInfo, const vector<CFxCurrencyInfo>& currencies);
    virtual void VGetSupportedSymbols(const CFxEventInfo& eventInfo, const vector<CFxSymbolInfo>& symbols);
    virtual void VSubscribeToQuotes(const CFxEventInfo& eventInfo, HRESULT status);
    virtual void VClosePositions(const CFxEventInfo& eventInfo, CFxClosePositionsResponse& response);
    virtual void VExecution(const CFxEventInfo& eventInfo, CFxExecutionReport& executionReport);
    virtual void VDataHistoryResponse(const CFxEventInfo& eventInfo, CFxDataHistoryResponse& response);
    virtual void VTradeHistoryResponse(const CFxEventInfo& eventInfo, CFxTradeHistoryResponse& response);
    virtual void VTradeHistoryReport(const CFxEventInfo& eventInfo, CFxTradeHistoryReport& report);
    virtual void VFileChunk(const CFxEventInfo& eventInfo, CFxFileChunk& chunk);
    virtual void VMetaInfoFile(const CFxEventInfo& eventInfo, string& file);
    virtual void VGetTradeTransactionReportsAndSubscribeToNotifications(const CFxEventInfo& info, const int32 curReportsNumber, const int32 totReportsNumber, const bool endOfStream);
    virtual void VTradeTransactionReport(const CFxEventInfo& info, CFxTradeTransactionReport& report);
    virtual void VUnsubscribeTradeTransactionReportsNotifications(const CFxEventInfo& info);
    virtual void VPositionReport(const CFxEventInfo& info, CFxPositionReport& positionReport);
    virtual void VNotify(const CFxEventInfo& eventInfo, const CNotification& notification);
    virtual void VQuotesHistoryResponse(const CFxEventInfo& eventInfo, const int version);

public:
    const string NextId();
    const string NextId(const string& prefix);
    const string NextIdIfEmpty(const string& prefix, const string& externalId);
    void RegisterWaiter(const type_info& info, const string& id, IWaiter* pWaiter);
    void ReleaseWaiter(const type_info& info, const string& id);

private:
    CIdGenerator m_idGenerator;
    CSynchInvoker m_synchInvoker;
};

#endif
