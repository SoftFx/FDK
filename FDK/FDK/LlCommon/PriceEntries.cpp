#include "stdafx.h"
#include "PriceEntries.h"
namespace FDK
{
	CPriceEntries::CPriceEntries()
	{
	}

	CPriceEntries::CPriceEntries(const CPriceEntries& entries)
        : m_entries(entries.m_entries)
	{
	}

	CPriceEntries& CPriceEntries::operator = (const CPriceEntries& entries)
	{
		if (this != &entries)
		{
			m_entries = entries.m_entries;
		}
		return *this;
	}

	CPriceEntries::~CPriceEntries()
	{
	}

	void CPriceEntries::Update(const char* symbol, const double bid, const double ask)
	{
		string key = symbol;
		m_entries[key] = CPriceEntry(bid, ask);
	}

	void CPriceEntries::Remove(const char* symbol)
	{
		string key = symbol;
		m_entries.erase(key);
	}

	void CPriceEntries::Clear()
	{
		m_entries.clear();
	}

	Nullable<CPriceEntry> CPriceEntries::TryGetPriceEntry(const char* symbol) const
	{
		string key = symbol;
		auto it = m_entries.find(key);
		if (m_entries.end() != it)
		{
			return it->second;
		}
		return nullptr;
	}

	const LrpMap(string, CPriceEntry)& CPriceEntries::GetEntries() const
	{
		return m_entries;
	}
}