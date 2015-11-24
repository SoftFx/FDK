#pragma once
#include "FinancialCalculator.h"
#include "PriceData.h"
#include "SymbolData.h"
#include "AccountData.h"

namespace FDK
{
	class CCalculatorData
	{
	public:
		CCalculatorData();
		CCalculatorData(const CFinancialCalculator& calc);
	public:
		MarginMode MarginMode;
		vector<CPriceData> Prices;
		vector<CSymbolData> Symbols;
		vector<CAccountData> Accounts;
		vector<string> Currencies;
	};
}