#include "stdafx.h"
#include "FinancialCalculator.h"
#include "Rates.h"
#include "Constants.h"
#include "Formating.h"

namespace
{
	const char* cVersionName = "Version";
	string cInitialVersion = "Initial";
}

namespace
{
	bool getline_ex(istream& stream, string& st)
	{
		if (!getline(stream, st))
		{
			return false;
		}
		if (st.empty())
		{
			return true;
		}
		if ('\r' == st.back())
		{
			st.erase(st.end() - 1);
		}
		if (st.empty())
		{
			return true;
		}
		if ('\n' == st.back())
		{
			st.erase(st.end() - 1);
		}
		return true;
	}
}


namespace FDK
{
	CFinancialCalculator::CFinancialCalculator()
        : m_marginMode(MarginMode_Dynamic)
	{
	}

	CFinancialCalculator::CFinancialCalculator(const char* text)
        : m_marginMode(MarginMode_Dynamic)
	{
		stringstream stream;
		stream << text;
		string line;
		if (!getline_ex(stream, line))
		{
			throw runtime_error("CFinancialCalculator::CFinancialCalculator(): can not read a line");
		}
		{
			stringstream _stream;
			_stream << line;
			ParseCalculator(_stream);
			ValidateEndOfStream(_stream);
		}
		for (; getline_ex(stream, line);)
		{
			if (line.empty())
			{
				continue;
			}

			stringstream _stream;
			_stream << line;

			string tag;
			_stream>>tag;

			ValidateVerbatimText(" ", _stream);
			tag += " ";

			if (cCurrencyTag == tag)
			{
				ParseCurrency(_stream);
			}
			else if (cSymbolTag == tag)
			{
				ParseSymbol(_stream);
			}
			else if (cPriceTag == tag)
			{
				ParsePrice(_stream);
			}
			else if (cAccountTag == tag)
			{
				ParseAccount(_stream);
			}
			else if (cTradeTag == tag)
			{
				ParseTrade(_stream);
			}
			else
			{
				throw runtime_error("CFinancialCalculator::CFinancialCalculator(): unknown tag = " + tag);
			}
			ValidateEndOfStream(_stream);
		}
	}

	void CFinancialCalculator::ParseCalculator(istream& stream)
	{
		ValidateVerbatimText(cCalculatorTag, stream);
		string version;
		Process(cVersionName, version, stream);
		if (cInitialVersion != version)
		{
			throw runtime_error("CFinancialCalculator::ParseCalculator(): unsupported version = " + version);
		}
		Process("MarginMode", m_marginMode, stream);
	}

	void CFinancialCalculator::ParseCurrency(istream& stream)
	{
		string currency;
		Process(nullptr, currency, stream);
		Currencies.push_back(currency);
	}

	void CFinancialCalculator::ParseSymbol(istream& stream)
	{
		CSymbolEntry entry;
		stream>>entry;
		Symbols.insert(entry);
	}

	void CFinancialCalculator::ParsePrice(istream& stream)
	{
		string symbol;
		Process("Symbol", symbol, stream);

		CPriceEntry entry;
		stream>>entry;

		Prices.Update(symbol, entry.Bid, entry.Ask);
	}

	void CFinancialCalculator::ParseAccount(istream& stream)
	{
		CAccountEntry entry;
		stream >> entry;
		Accounts.push_back(entry);
	}

	void CFinancialCalculator::ParseTrade(istream& stream)
	{
		if (Accounts.empty())
		{
			throw runtime_error("CFinancialCalculator::ParseTrade(): reading trade entry before account entry");
		}
		CTradeEntry entry;
		stream >> entry;
		Accounts.back().Trades.push_back(entry);
	}

	CFinancialCalculator::CFinancialCalculator(ResolvePriceHandler handler, void* pUserData)
        : m_marginMode(MarginMode_Dynamic)
        , m_rates(handler, pUserData)
	{
	}

	CFinancialCalculator::CFinancialCalculator(const CFinancialCalculator& calc)
        : Symbols(calc.Symbols)
        , Currencies(calc.Currencies)
        , Prices(calc.Prices)
        , Accounts(calc.Accounts)
        , m_marginMode(calc.m_marginMode)
        , m_rates(calc.m_rates)
	{
	}

	CFinancialCalculator& CFinancialCalculator::operator = (const CFinancialCalculator& calc)
	{
		if (this != &calc)
		{
			Symbols = calc.Symbols;
			Currencies = calc.Currencies;
			Prices = calc.Prices;
			Accounts = calc.Accounts;
			m_marginMode = calc.m_marginMode;
			m_rates = calc.m_rates;
		}
		return *this;
	}

	CFinancialCalculator::~CFinancialCalculator()
	{
	}

	const char* CFinancialCalculator::ToString() const
	{
		return ToString("\r\n");
	}

	const char* CFinancialCalculator::ToString(const char* endline) const
	{
		stringstream _stream;
		ostream& stream = _stream;
		// write calculator settings
		stream << cCalculatorTag;
		Process(cVersionName, cInitialVersion, stream);
		Process("MarginMode", m_marginMode, stream);
		stream << endline;
		// write currencies
		for each(const auto& element in Currencies.GetEntries())
		{
			stream << cCurrencyTag;
			Process(nullptr, element, stream);
			stream << endline;
		}
		// write symbols
		for each(const auto& element in Symbols.GetEntris())
		{
			stream << cSymbolTag << element.second << endline;
		}
		// write prices
		for each(const auto& element in Prices.GetEntries())
		{
			stream << cPriceTag;
			Process("Symbol", element.first, stream);
			stream << element.second << endline;
		}
		// write accounts
		for each(const auto& account in Accounts)
		{
			stream << cAccountTag << account << endline;
			for each(const auto& trade in account.Trades)
			{
				stream << cTradeTag << trade << endline;
			}
		}
		m_cache = _stream.str();
		return m_cache.c_str();
	}

	void CFinancialCalculator::SetMarginMode(const MarginMode newMarginMode)
	{
		if ((MarginMode_Dynamic != newMarginMode) && (MarginMode_Static != newMarginMode) && (MarginMode_StaticIfPossible != newMarginMode))
		{
			throw runtime_error("Invalid margin mode; new value can be Dynamic, Static or StaticIfPossible only");
		}
		m_marginMode = newMarginMode;
	}

	void CFinancialCalculator::Calculate()
	{
		Clear();

		Symbols.MakeIndex();
		m_rates.Initialize(Symbols, Currencies, Prices);

		auto it = Accounts.begin();
		auto end = Accounts.end();

		for (; it != end; ++it)
		{
			it->Calcualte(*this);
		}
	}

	void CFinancialCalculator::Clear()
	{
		auto it = Accounts.begin();
		auto end = Accounts.end();

		for (; it != end; ++it)
		{
			it->Clear();
		}
	}
}