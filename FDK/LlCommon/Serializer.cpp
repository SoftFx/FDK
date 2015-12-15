#include "stdafx.h"
#include "Serializer.h"


namespace FDK
{
	std::string CSerializer::Serialize(const CCalculatorData& calc)
	{
		CFinancialCalculator calculator;
		calculator.SetMarginMode(calc.MarginMode);

		for each(const auto& element in calc.Prices)
		{
			calculator.Prices.Update(element.Symbol, element.Bid, element.Ask);
		}

		for each(const auto& element in calc.Symbols)
		{
			calculator.Symbols.insert(element.ToSymbolEntry());
		}

		for each(const auto& element in calc.Accounts)
		{
			calculator.Accounts.push_back(element.ToAccountEntry());
		}

		for each(const auto& element in calc.Currencies)
		{
			calculator.Currencies.push_back(element);
		}

		std::string result = calculator.ToString("\r\n");
		return result;
	}

	CCalculatorData CSerializer::Deserialize(const string& text)
	{
		CFinancialCalculator calc(text.c_str());
		CCalculatorData result(calc);
		return result;
	}
}