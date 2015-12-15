#include "stdafx.h"
#include "DataFeedCache.h"

#ifndef _MSC_VER

typedef CDataCache __super;

#endif

inline CDataFeedCache::CacheInitializationState operator | (const CDataFeedCache::CacheInitializationState a, const CDataFeedCache::CacheInitializationState b)
{
    return static_cast<CDataFeedCache::CacheInitializationState>(static_cast<int>(a) | static_cast<int>(b));
}

inline CDataFeedCache::CacheInitializationState operator |= (CDataFeedCache::CacheInitializationState& a, const CDataFeedCache::CacheInitializationState b)
{
    return a = a | b;
}

CDataFeedCache::CDataFeedCache(CFxQueue& queue)
    : CDataCache(queue)
    , m_cacheState(CACHE_INIT_NONE)
{
}

bool CDataFeedCache::TryGetBid(const string& symbol, double& price, double& volume, CDateTime& creatingTime) const
{
	CSharedLocker lock(m_synchronizer);

	auto it = m_quotes.find(symbol);
	if (m_quotes.end() == it)
	{
		return false;
	}

	const CFxQuote& quote = it->second;
	const bool result = quote.TryGetBid(price, volume, creatingTime);
	return result;
}

bool CDataFeedCache::TryGetAsk(const string& symbol, double& price, double& volume, CDateTime& creatingTime) const
{
	CSharedLocker lock(m_synchronizer);

	auto it = m_quotes.find(symbol);
	if (m_quotes.end() == it)
	{
		return false;
	}

	const CFxQuote& quote = it->second;
	const bool result = quote.TryGetAsk(price, volume, creatingTime);
	return result;
}

bool CDataFeedCache::TryGetQuote(const string& symbol, CFxQuote& quote) const
{
	CSharedLocker lock(m_synchronizer);

	auto it = m_quotes.find(symbol);
	if (m_quotes.end() == it)
	{
		return false;
	}

	quote = it->second;
	return true;
}

vector<CFxCurrencyInfo> CDataFeedCache::GetCurrencies() const
{
    CSharedLocker lock(m_synchronizer);

    return m_currencies;
}

vector<CFxSymbolInfo> CDataFeedCache::GetSymbols() const
{
    CSharedLocker lock(m_synchronizer);

    return m_symbols;
}

bool CDataFeedCache::GetFeedSnapshot(map<string, CFxQuote>& quotes, vector<CFxSymbolInfo>& symbols) const
{
    CSharedLocker lock(m_synchronizer);

    quotes = m_quotes;
    symbols = m_symbols;
    return true;
}

Nullable<int> CDataFeedCache::GetServerQuotesHistoryVersion() const
{
    Nullable<int> result;

    {
        CSharedLocker lock(m_synchronizer);
        result = m_serverQuotesHistoryVersion;
    }

    return result;
}

void CDataFeedCache::Clear()
{
	DoClear();
	RaiseChanged();
}

void CDataFeedCache::DoClear()
{
	CExclusiveLocker lock(m_synchronizer);

	m_serverQuotesHistoryVersion = Nullable<int>();
	m_quotes.clear();
	m_symbols.clear();
    m_currencies.clear();

    m_cacheState = CACHE_INIT_NONE;

    __super::Clear();
}

void CDataFeedCache::UpdateQuotes(const CFxQuote& quote)
{
	DoUpdateQuotes(quote);
	RaiseChanged();
}

void CDataFeedCache::DoUpdateQuotes(const CFxQuote& quote)
{
	CExclusiveLocker lock(m_synchronizer);

	m_quotes[quote.Symbol] = quote;
}

void CDataFeedCache::UpdateSymbols(const vector<CFxSymbolInfo>& symbols)
{
	DoUpdateSymbols(symbols);
	RaiseChanged();
}

void CDataFeedCache::DoUpdateSymbols(const vector<CFxSymbolInfo>& symbols)
{
	CExclusiveLocker lock(m_synchronizer);

	m_symbols = symbols;

    m_cacheState |= CACHE_INIT_SYMBOLS;

	Update();
}

void CDataFeedCache::UpdateCurrencies(const vector<CFxCurrencyInfo>& currencies)
{
    DoUpdateCurrencies(currencies);
    RaiseChanged();
}

void CDataFeedCache::DoUpdateCurrencies(const vector<CFxCurrencyInfo>& currencies)
{
    CExclusiveLocker lock(m_synchronizer);

    m_currencies = currencies;

    m_cacheState |= CACHE_INIT_CURRENCIES;

    Update();
}

void CDataFeedCache::SetServerQuotesHistoryVersion(int version)
{
    CExclusiveLocker lock(m_synchronizer);

    m_serverQuotesHistoryVersion = version;

    Update();
}

void CDataFeedCache::Update()
{
    __super::Update(m_cacheState == CACHE_INIT_ALL);
}

void CDataFeedCache::RaiseChanged()
{
	Changed(*this);
}
