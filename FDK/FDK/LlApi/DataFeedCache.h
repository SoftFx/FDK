#ifndef __Native_Data_Feed_Cache__
#define __Native_Data_Feed_Cache__

#include "DataCache.h"

class CDataFeedCache;

typedef void FeedCacheChangedSignature(const CDataFeedCache&);
typedef Delegate<FeedCacheChangedSignature> FeedCacheHandler;


class CDataFeedCache : public CDataCache
{
public:
	CDataFeedCache(CFxQueue& queue);

public:
	bool TryGetBid(const string& symbol, double& price, double& volume, CDateTime& creatingTime) const;
	bool TryGetAsk(const string& symbol, double& price, double& volume, CDateTime& creatingTime) const;
	bool TryGetQuote(const string& symbol, CFxQuote& quote) const;
	vector<CFxSymbolInfo> GetSymbols() const;
    vector<CFxCurrencyInfo> GetCurrencies() const;
	bool GetFeedSnapshot(map<string, CFxQuote>& quotes, vector<CFxSymbolInfo>& symbols) const;
	Nullable<int> GetServerQuotesHistoryVersion() const;

public:
	void Clear();
	void SetServerQuotesHistoryVersion(int version);
	void UpdateQuotes(const CFxQuote& quote);
    void UpdateCurrencies(const vector<CFxCurrencyInfo>& currencies);
	void UpdateSymbols(const vector<CFxSymbolInfo>& symbols);

private:
	void DoClear();
	void DoUpdateQuotes(const CFxQuote& quote);
    void DoUpdateCurrencies(const vector<CFxCurrencyInfo>& currencies);
	void DoUpdateSymbols(const vector<CFxSymbolInfo>& symbols);

public:
	Event<CDataFeedCache, FeedCacheChangedSignature> Changed;

private:
	void Update();
	void RaiseChanged();

private:
    enum CacheInitializationState
    {
        CACHE_INIT_NONE       = 0,
        CACHE_INIT_CURRENCIES = 1,
        CACHE_INIT_SYMBOLS    = 2,
        CACHE_INIT_ALL        = 3
    };

    CacheInitializationState m_cacheState;
	Nullable<int>            m_serverQuotesHistoryVersion;
	map<string, CFxQuote>    m_quotes;
	vector<CFxSymbolInfo>    m_symbols;
    vector<CFxCurrencyInfo>  m_currencies;

    friend CacheInitializationState operator | (const CacheInitializationState a, const CacheInitializationState b);
    friend CacheInitializationState operator |= (CacheInitializationState& a, const CacheInitializationState b);
};

#endif
