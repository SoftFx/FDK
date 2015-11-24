#pragma once


class CQuotesResolver
{
public:
	CQuotesResolver();
public:
	static Nullable<CPriceEntry> ResolvePrice(const char* symbol, void* pUserData);
private:
	Nullable<CPriceEntry> ResolvePrice(const char* symbol) const;
private:
	map<string, CPriceEntry> m_quotes;
};