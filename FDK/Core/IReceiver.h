#ifndef __Core_IReceiver__
#define __Core_IReceiver__
#include "FxEventInfo.h"
#include "FxSessionInfo.h"
#include "FxQuote.h"
#include "FxAccountInfo.h"
#include "FxClosePositionsResponse.h"
#include "FxExecutionReport.h"
#include "FxDataHistoryResponse.h"
#include "FxTradeHistoryReport.h"
#include "FxTradeHistoryResponse.h"
#include "FxFileChunk.h"
#include "FxTradeTransactionReport.h"
#include "FxPositionReport.h"
#include "Notification.h"
#include "FxSymbolInfo.h"
#include "FxCurrencyInfo.h"

class IReceiver
{
public:
    virtual void VLogon(const CFxEventInfo& eventInfo, const string& protocolVersion) = 0;
    virtual void VLogout(const CFxEventInfo& eventInfo, const FxLogoutReason reason, const string& description) = 0;
    virtual void VTwoFactorAuth(const CFxEventInfo& eventInfo, const FxTwoFactorReason reason, const string& text, const CDateTime& expire) = 0;
    virtual void VBusinessReject(const CFxEventInfo& eventInfo) = 0;
    virtual void VTick(const CFxEventInfo& eventInfo, const CFxQuote& quotes) = 0;
    virtual void VSessionInfo(const CFxEventInfo& eventInfo, CFxSessionInfo& sessionInfo) = 0;
    virtual void VAccountInfo(const CFxEventInfo& eventInfo, CFxAccountInfo& accountInfo) = 0;
    virtual void VGetCurrencies(const CFxEventInfo& eventInfo, const vector<CFxCurrencyInfo>& currencies) = 0;
    virtual void VGetSupportedSymbols(const CFxEventInfo& eventInfo, const vector<CFxSymbolInfo>& symbols) = 0;
    virtual void VSubscribeToQuotes(const CFxEventInfo& eventInfo, HRESULT status) = 0;
    virtual void VClosePositions(const CFxEventInfo& eventInfo, CFxClosePositionsResponse& response) = 0;
    virtual void VExecution(const CFxEventInfo& eventInfo, CFxExecutionReport& executionReport) = 0;
    virtual void VDataHistoryResponse(const CFxEventInfo& eventInfo, CFxDataHistoryResponse& response) = 0;
    virtual void VTradeHistoryResponse(const CFxEventInfo& eventInfo, CFxTradeHistoryResponse& response) = 0;
    virtual void VTradeHistoryReport(const CFxEventInfo& eventInfo, CFxTradeHistoryReport& report) = 0;
    virtual void VFileChunk(const CFxEventInfo& eventInfo, CFxFileChunk& chunk) = 0;
    virtual void VMetaInfoFile(const CFxEventInfo& eventInfo, string& file) = 0;
    virtual void VGetTradeTransactionReportsAndSubscribeToNotifications(const CFxEventInfo& info, const int32 curReportsNumber, const int32 totReportsNumber, const bool endOfStream) = 0;
    virtual void VTradeTransactionReport(const CFxEventInfo& info, CFxTradeTransactionReport& report) = 0;
    virtual void VUnsubscribeTradeTransactionReportsNotifications(const CFxEventInfo& info) = 0;
    virtual void VPositionReport(const CFxEventInfo& eventInfo, CFxPositionReport& positionReport) = 0;
    virtual void VNotify(const CFxEventInfo& eventInfo, const CNotification& notification) = 0;
    virtual void VQuotesHistoryResponse(const CFxEventInfo& eventInfo, const int version) = 0;
    virtual ~IReceiver(){};
};
#endif
