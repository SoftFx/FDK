#pragma once

#include "SymbolEntry.h"

namespace FDK
{
	/// <summary>
	/// Provides access to symbol entries collection.
	/// </summary>
	class CSymbolEntries
	{
	public:
		LLCOMMON_API CSymbolEntries();
		LLCOMMON_API ~CSymbolEntries();
		LLCOMMON_API CSymbolEntries(const CSymbolEntries& entries);
		LLCOMMON_API CSymbolEntries& operator = (const CSymbolEntries& entries);
	public:
		/// <summary>
		/// Adds a new symbol entry to the container.
		/// </summary>
		/// <param name="entry">a valid symbol entry</param>
		LLCOMMON_API void insert(const CSymbolEntry& entry);
		/// <summary>
		/// Removes an existing symbol entry from the container.
		/// </summary>
		/// <param name="symbol">a valid symbol entry</param>
		inline void erase(const std::string& symbol)
		{
			erase(symbol.c_str());
		}
		/// <summary>
		/// Removes an existing symbol entry from the container.
		/// </summary>
		/// <param name="symbol">a valid symbol entry</param>
		LLCOMMON_API void erase(const char* symbol);
		/// <summary>
		/// Removes all existing symbol entries from the container.
		/// </summary>
		LLCOMMON_API void clear();
	internal:
		void MakeIndex();
		size_t GetNumberOfCurrencies() const;
		ptrdiff_t TryGetCurrencyIndex(const LrpString& currency) const;
		CSymbolEntry* TryGetSymbolEntry(const LrpString& symbol);
		const LrpMap(string, CSymbolEntry)& GetEntris() const;
		void ClearMargin();
		double GrossMargin() const;
		double NetMargin() const;
	internal:
		const LrpMap(std::string, CSymbolEntry)& GetEntries() const
		{
			return m_entries;
		}
	private:
		ptrdiff_t IndexFromCurrency(const LrpString& currency);
	private:
		LrpMap(std::string, CSymbolEntry) m_entries;
		LrpMap(std::string, ptrdiff_t) m_currencies;
	private:
	};
}