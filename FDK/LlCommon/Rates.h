#pragma once

#include "PriceEntry.h"

namespace FDK
{
	class CSymbolEntries;
	class CPriceEntries;
	class CRateEntry;
	class CSymbolEntry;
	class CCurrencyEntries;

	typedef Nullable<CPriceEntry> (*ResolvePriceHandler)(const char* pSymbol, void* pUserData);

	class CRates
	{
	public:
		CRates();
		CRates(const CRates& rates);
		CRates& operator = (const CRates& rates);
		CRates(ResolvePriceHandler handler, void* pUserData);
		~CRates();

	private:
		void Finalize();

	public:
		void Initialize(const CSymbolEntries& symbols, const CCurrencyEntries& currencies, CPriceEntries& prices);

	public:
		Nullable<double> CalculateProfitRate(double profit, ptrdiff_t xxx, ptrdiff_t yyy, ptrdiff_t zzz);
		Nullable<double> CalculateMarginRate(TradeSide side, ptrdiff_t xxx, ptrdiff_t yyy, ptrdiff_t zzz);
		Nullable<CPriceEntry> TryGetRate(const CSymbolEntry& symbol);

	private:
		Nullable<CPriceEntry> TryGetRate(ptrdiff_t to, ptrdiff_t from);

	private:
		ResolvePriceHandler m_handler;
		void* m_userData;

	private:
		CPriceEntries* m_prices;
		size_t m_size;
		CRateEntry** m_table;
		CRateEntry* m_data;
		LrpVector(ptrdiff_t) m_currencies;
	};
}