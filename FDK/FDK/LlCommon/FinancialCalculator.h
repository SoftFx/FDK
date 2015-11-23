#pragma once

#include "PriceEntries.h"
#include "MarginCalcMode.h"
#include "SymbolEntries.h"
#include "AccountEntries.h"
#include "CurrencyEntries.h"
#include "MarginMode.h"
#include "Rates.h"


namespace FDK
{
	class CFinancialCalculator
	{
	public:
		CSymbolEntries Symbols;
		CCurrencyEntries Currencies;
		CPriceEntries Prices;
		CAccountEntries Accounts;
	public:
		LLCOMMON_API CFinancialCalculator();
		LLCOMMON_API CFinancialCalculator(const char* text);
		LLCOMMON_API CFinancialCalculator(ResolvePriceHandler handler, void* pUserData);
		LLCOMMON_API CFinancialCalculator(const CFinancialCalculator& calc);
		LLCOMMON_API CFinancialCalculator& operator = (const CFinancialCalculator& calc);
		LLCOMMON_API ~CFinancialCalculator();
	private:
		void ParseCalculator(std::istream& stream);
		void ParseCurrency(std::istream& stream);
		void ParseSymbol(std::istream& stream);
		void ParsePrice(std::istream& stream);
		void ParseAccount(std::istream& stream);
		void ParseTrade(std::istream& stream);
	public:
		MarginMode GetMarginMode() const { return m_marginMode; }
		LLCOMMON_API void SetMarginMode(const MarginMode newMarginMode);
	internal:
		inline CRates& GetRates() { return m_rates; }
	public:
		LLCOMMON_API void Calculate();
		LLCOMMON_API void Clear();
	public:
		LLCOMMON_API const char* ToString() const;
	internal:
		const char* ToString(const char* endline) const;
	private:
		MarginMode m_marginMode;
		CRates m_rates;
	private:
		mutable LrpString m_cache;
	};

	inline std::ostream& operator << (std::ostream& stream, const CFinancialCalculator& calc)
	{
		stream << calc.ToString();
		return stream;
	}
}