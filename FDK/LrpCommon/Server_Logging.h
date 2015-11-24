// This is always generated file. Do not change anything.

#pragma once
class Server
{
private:
	ILrpTextStream* m_stream;
public:
	Server(ILrpTextStream* pStream);
public:
	void OnHeartBeatRequest();
	void OnHeartBeatResponse();
	void OnCurrenciesInfoRequest(const std::string& id);
	void OnSymbolsInfoRequest(const std::string& id);
	void OnSessionInfoRequest(const std::string& id);
	void OnSubscribeToQuotesRequest(const std::string& id, const std::vector<std::string>& symbols, const __int32& marketDepth);
	void OnUnsubscribeQuotesRequest(const std::string& id, const std::vector<std::string>& symbols);
	void OnComponentsInfoRequest(const std::string& id, const __int32& clientVersion);
	void OnDataHistoryRequest(const std::string& id, const CFxDataHistoryRequest& request);
	void OnFileChunkRequest(const std::string& id, const std::string& fieldId, const unsigned __int32& chunkId);
	void OnBarsHistoryMetaInfoFileRequest(const std::string& id, const std::string& symbol, const __int32& priceType, const std::string& period);
	void OnQuotesHistoryMetaInfoFileRequest(const std::string& id, const std::string& symbol, const bool& includeLevel2);
};

