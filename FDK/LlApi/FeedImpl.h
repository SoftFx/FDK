#pragma once

#include <string>

class CFeedImpl
{
public:
	void* Create(const std::string& name, const std::string& connectionString);
	CDataHistoryInfo GetBarsHistoryFiles(void* handle, const string& symbol, int priceType, const string& period, CDateTime time, size_t timeoutInMilliseconds);
	CDataHistoryInfo GetQuoteHistoryFiles(void* handle, const string& symbol, bool includeLevel2, CDateTime time, size_t timeoutInMilliseconds);
    std::vector<CFxCurrencyInfo> GetCurrencies(void* handle, size_t timeoutInMilliseconds);
	std::vector<CFxSymbolInfo> GetSymbols(void* handle, size_t timeoutInMilliseconds);
	void SubscribeToQuotes(void* handle, const vector<string>& symbols, int depth, size_t timeoutInMilliseconds);
	void UnsubscribeQuotes(void* handle, const vector<string>& symbols, size_t timeoutInMilliseconds);
	std::string GetBarsHistoryMetaInfoFile(void* handle, const string& symbol, FxPriceType priceType, const string& period, const size_t timeoutInMilliseconds);
	std::string GetQuotesHistoryMetaInfoFile(void* handle, const string& symbol, bool includeLevel2, const size_t timeoutInMilliseconds);
	CDataHistoryInfo GetHistoryBars(void* handle, const string& symbol, CDateTime& time, int barsNumber, FxPriceType priceType, const string& period, const size_t timeoutInMilliseconds);
	int GetQueueThreshold(void* handle);
	void SetQueueThreshold(void* handle, int newSize);
	int GetQuotesHistoryVersion(void* handle, const uint32 timeoutInMilliseconds);
};


