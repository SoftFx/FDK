#pragma once

class CSymbolToKey
{
public:
	CSymbolToKey();
public:
	static ptrdiff_t GetKeyFromSymbol(const string& symbol);
private:
	ptrdiff_t DoGetKeyFromSymbol(const string& symbol);
private:
	CSharedExclusiveLock m_synchronizer;
    unordered_map<string, ptrdiff_t> m_static;
    unordered_map<string, ptrdiff_t> m_dynamic;
};