#include "stdafx.h"
#include "QuotesResolver.h"

CQuotesResolver::CQuotesResolver()
{
	m_quotes["EURUSD"] = CPriceEntry(1.36156, 1.36166);
	m_quotes["EURJPY"] = CPriceEntry(138.327, 138.357);
	m_quotes["USDJPY"] = CPriceEntry(101.53, 101.54);
}

Nullable<CPriceEntry> CQuotesResolver::ResolvePrice(const char* symbol, void* pUserData)
{
	CQuotesResolver* pThis = reinterpret_cast<CQuotesResolver*>(pUserData);
	return pThis->ResolvePrice(symbol);
}
Nullable<CPriceEntry> CQuotesResolver::ResolvePrice(const char* symbol) const
{
	string key = symbol;
	auto it = m_quotes.find(key);
	if (m_quotes.end() != it)
	{
		return it->second;
	}
	return nullptr;
}

