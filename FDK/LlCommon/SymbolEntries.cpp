#include "stdafx.h"
#include "SymbolEntries.h"
#include "Constants.h"

namespace FDK
{
	CSymbolEntries::CSymbolEntries()
	{
	}

	CSymbolEntries::~CSymbolEntries()
	{
	}

	CSymbolEntries::CSymbolEntries(const CSymbolEntries& entries)
        : m_entries(entries.m_entries)
        , m_currencies(entries.m_currencies)
	{
	}

	CSymbolEntries& CSymbolEntries::operator = (const CSymbolEntries& entries)
	{
		if (this != &entries)
		{
			m_entries = entries.m_entries;
			m_currencies = entries.m_currencies;
		}
		return *this;
	}

	void CSymbolEntries::insert(const CSymbolEntry& entry)
	{
		const string& key = entry.GetSymbol();

		for each (const auto& element in m_entries)
		{
			if (key == element.first)
			{
				string message = "Duplicate symbol alias = " + key;
				throw runtime_error(message);
			}
		}
		m_entries[key] = entry;
	}

	void CSymbolEntries::erase(const char* symbol)
	{
		string key = symbol;
		m_entries.erase(key);
	}

	void CSymbolEntries::clear()
	{
		m_entries.clear();
	}

	void CSymbolEntries::MakeIndex()
	{
		m_currencies.clear();

		auto it = m_entries.begin();
		auto end = m_entries.end();
	

		for(ptrdiff_t index = 0; it != end; ++it, ++index)
		{
			CSymbolEntry& symbol = it->second;
			symbol.SymbolIndex = index;
			symbol.FromIndex = IndexFromCurrency(symbol.GetFrom());
			symbol.ToIndex = IndexFromCurrency(symbol.GetTo());
		}
	}

	size_t CSymbolEntries::GetNumberOfCurrencies() const
	{
		return m_currencies.size();
	}

	ptrdiff_t CSymbolEntries::IndexFromCurrency(const LrpString& currency)
	{
		auto it = m_currencies.find(currency);
		if (m_currencies.end() != it)
		{
			return it->second;
		}
		ptrdiff_t result = static_cast<ptrdiff_t>(m_currencies.size());
		m_currencies[currency] = result;
		return result;
	}

	const LrpMap(string, CSymbolEntry)& CSymbolEntries::GetEntris() const
	{
		return m_entries;
	}

	ptrdiff_t CSymbolEntries::TryGetCurrencyIndex(const LrpString& currency) const
	{
		auto it = m_currencies.find(currency);
		if (m_currencies.end() != it)
		{
			return it->second;
		}
		return -1;
	}

	CSymbolEntry* CSymbolEntries::TryGetSymbolEntry(const LrpString& symbol)
	{
		auto it = m_entries.find(symbol);
		if (m_entries.end() != it)
		{
			return &(it->second);
		}
		return nullptr;
	}

	void CSymbolEntries::ClearMargin()
	{
		auto it = m_entries.begin();
		auto end = m_entries.end();
		for (; it != end; ++it)
		{
			it->second.ClearMargin();
		}
	}

	double CSymbolEntries::GrossMargin() const
	{
		double result = 0;
		for each (const auto& element in m_entries)
		{
			result += element.second.CalculateGrossMargin();
		}
		return result;
	}

	double CSymbolEntries::NetMargin() const
	{
		double result = 0;
		for each (const auto& element in m_entries)
		{
			result += element.second.CalculateNetMargin();
		}
		return result;
	}
}