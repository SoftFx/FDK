#pragma once
#include "SimpleCodec.h"

class CLrpSender;


class CLrpReceiver
{
public:
    CLrpReceiver();
    CLrpReceiver(IReceiver* pReceiver, CLrpSender* pSender);
public:
    static const char* Signature();
public:
    CLrpReceiver& GetClient();
    CSimpleCodec& GetSimpleCodec();
public:
    void OnHeartBeatRequest();
    void OnHeartBeatResponse();
    void OnLogonMsg(const string& protocolVersion);
    void OnTwoFactorAuthMsg(const FxTwoFactorReason reason, const string& text, const CDateTime& expire);
    void OnLogoutMsg(const FxLogoutReason reason, const string& description);
    void OnQuotesSubscriptionMsg(const string& requestId, int32 status, const string& message);
    void OnSessionInfoMsg(const string& requestId, const CFxSessionInfo& info);
    void OnSessionInfoMsg2(const string& requestId, const CFxSessionInfo& info);
    void OnCurrenciesInfoMsg(const string& requestId, const vector<CFxCurrencyInfo>& currencies);
    void OnSymbolsInfoMsg(const string& requestId, const vector<CFxSymbolInfo>& symbols);
    void OnSymbolsInfoMsg2(const string& requestId, const vector<CFxSymbolInfo>& symbols);
    void OnSymbolsInfoMsg3(const string& requestId, const vector<CFxSymbolInfo>& symbols);
    void OnSymbolsInfoMsg4(const string& requestId, const vector<CFxSymbolInfo>& symbols);
    void OnSymbolsInfoMsg5(const string& requestId, const vector<CFxSymbolInfo>& symbols);
    void OnSymbolsInfoMsg6(const string& requestId, const vector<CFxSymbolInfo>& symbols);
    void OnSymbolsInfoMsg7(const string& requestId, const vector<CFxSymbolInfo>& symbols);
    void OnComponentsInfoMsg(const string& requestId, int version);
    void OnDataHistoryMetaInfoResponseMsg(const string& requestId, const int32 status, const string& field);
    void OnDataHistoryMetaInfoRejectMsg(const string& requestId, const int32 status, const string& field);
    void OnDataHistoryResponseMsg(const string& requestId, const CFxDataHistoryResponse& response);
    void OnDataHistoryRejectMsg(const string& requestId, FxMarketHistoryRejectType rejectType, const string& rejectReason);
    void OnFileChunkMsg(const string& requestId, const CFxFileChunk& chunk);
    void OnNotificationMsg(const CNotification& notification);
    void OnBusinessRejectMsg(const string& rejectReason, const string& rejectTag);
public:
    void OnQuoteRawMsg(MemoryBuffer& buffer);
    static bool ShouldBeLogged(const uint16 componentId, const uint16 methodId);
public:
    HRESULT Process(MemoryBuffer& buffer);
private:
    HRESULT DoProcess(MemoryBuffer& buffer);
    void AdjustSymbolsTradeAmounts(vector<CFxSymbolInfo>& symbols);
private:
    IReceiver* m_receiver;
    CLrpSender* m_sender;
    CSimpleCodec m_codec;
};
