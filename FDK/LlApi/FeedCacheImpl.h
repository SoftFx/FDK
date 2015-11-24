#pragma once


class CFeedCacheImpl
{
public:
	vector<CFxSymbolInfo> GetSymbols(void* handle);
    vector<CFxCurrencyInfo> GetCurrencies(void* handle);
	bool TryGetBid(void* handle, const string& symbol, double& price, double& volume, CDateTime& creationTime);
	bool TryGetAsk(void* handle, const string& symbol, double& price, double& volume, CDateTime& creationTime);
	bool TryGetQuote(void* handle, const string& symbol, CFxQuote& quote);
};