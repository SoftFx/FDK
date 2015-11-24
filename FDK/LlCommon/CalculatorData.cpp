#include "stdafx.h"
#include "CalculatorData.h"


namespace FDK
{
	CCalculatorData::CCalculatorData()
        : MarginMode(MarginMode_Dynamic)
	{
	}

	CCalculatorData::CCalculatorData(const CFinancialCalculator& calc)
	{
		MarginMode = calc.GetMarginMode();

		Prices.reserve(calc.Prices.GetEntries().size());
		for each(const auto& element in calc.Prices.GetEntries())
		{
			Prices.push_back(CPriceData(element.first, element.second.Bid, element.second.Ask));
		}

		Symbols.reserve(calc.Symbols.GetEntries().size());

		for each(const auto& element in calc.Symbols.GetEntries())
		{
			Symbols.push_back(CSymbolData(element.second));
		}

		Accounts.reserve(calc.Accounts.size());

		for each(const auto& element in calc.Accounts)
		{
			Accounts.push_back(CAccountData(element));
		}

		Currencies = calc.Currencies.GetEntries();
	}

}