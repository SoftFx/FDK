// This is always generated file. Do not change anything.

#pragma once
class Server
{
private:
	ILrpTextStream* m_stream;
public:
	Server(ILrpTextStream* pStream);
public:
	void GetSupportedSymbols(const std::string& id);
	void GetSessionInfo(const std::string& id);
	void SubscribeToQuotes(const std::string& id, const std::vector<std::string>& symbols, const __int32& marketDepth);
	void UnsubscribeQuotes(const std::string& id, const std::vector<std::string>& symbols);
	void GetQuotesHistoryVersion(const std::string& id, const __int32& clientVersion);
	void SendDataHistoryRequest(const std::string& id, const CFxDataHistoryRequest& request);
	void SendGetFileChunk(const std::string& id, const std::string& fieldId, const unsigned __int32& chunkId);
	void SendGetBarsHistoryMetaInfoFile(const std::string& id, const std::string& symbol, const __int32& priceType, const std::string& period);
	void SendGetTicksHistoryMetaInfoFile(const std::string& id, const std::string& symbol, const bool& includeLevel2);
};

