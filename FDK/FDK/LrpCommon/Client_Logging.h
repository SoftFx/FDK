// This is always generated file. Do not change anything.

#pragma once
class Client
{
private:
	ILrpTextStream* m_stream;
public:
	Client(ILrpTextStream* pStream);
public:
	void OnHeartBeatRequest();
	void OnHeartBeatResponse();
	void OnLogonMsg(const std::string& protocolVersion);
	void OnLogoutMsg(const FxLogoutReason& reason, const std::string& description);
	void OnSessionInfoMsg(const std::string& requestId, const CFxSessionInfo& sessionInfo);
	void OnSessionInfoMsg2(const std::string& requestId, const CFxSessionInfo& sessionInfo);
	void OnCurrenciesInfoMsg(const std::string& requestId, const std::vector<CFxCurrencyInfo>& currencies);
	void OnSymbolsInfoMsg(const std::string& requestId, const std::vector<CFxSymbolInfo>& symbols);
	void OnSymbolsInfoMsg2(const std::string& requestId, const std::vector<CFxSymbolInfo>& symbols);
	void OnSymbolsInfoMsg3(const std::string& requestId, const std::vector<CFxSymbolInfo>& symbols);
	void OnSymbolsInfoMsg4(const std::string& requestId, const std::vector<CFxSymbolInfo>& symbols);
	void OnSymbolsInfoMsg5(const std::string& requestId, const std::vector<CFxSymbolInfo>& symbols);
	void OnSymbolsInfoMsg6(const std::string& requestId, const std::vector<CFxSymbolInfo>& symbols);
	void OnSymbolsInfoMsg7(const std::string& requestId, const std::vector<CFxSymbolInfo>& symbols);
	void OnComponentsInfoMsg(const std::string& requestId, const __int32& quotesStorageVersion);
	void OnQuotesSubscriptionMsg(const std::string& requestId, const __int32& status, const std::string& message);
	void OnFileChunkMsg(const std::string& requestId, const CFxFileChunk& chunk);
	void OnDataHistoryMsg(const std::string& requestId, const CFxDataHistoryResponse& response);
	void OnDataHistoryMetaInfoMsg(const std::string& requestId, const __int32& status, const std::string& field);
	void OnQuoteRawMsg(const MemoryBuffer& data);
	void OnNotificationMsg(const CNotification& notification);
};

class SimpleCodec
{
private:
	ILrpTextStream* m_stream;
public:
	SimpleCodec(ILrpTextStream* pStream);
public:
	void OnSymbolIndexMsg(const std::string& symbol, const unsigned __int32& index);
	void OnTimeSynchMsg(const CDateTime& time);
	void OnSymbol8Time32Level2Msg(const unsigned __int8& symbolIndex, const __int32& timeOffset, const MemoryBuffer& data);
	void OnQuoteZipRawMsg(const MemoryBuffer& data);
};

