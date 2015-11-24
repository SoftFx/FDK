#include "stdafx.h"
#include "AccountEntry.h"
#include "Functions.h"
#include "SymbolEntries.h"
#include "FinancialCalculator.h"
#include "Formating.h"

namespace FDK
{

	CAccountEntry::CAccountEntry()
        : m_type(AccountType_None)
        , m_leverage(1)
        , m_balance()
        , m_profitStatus(AccountEntryStatus_NotCalculated)
        , m_marginStatus(AccountEntryStatus_NotCalculated)
	{
	}

	CAccountEntry::CAccountEntry(const CAccountEntry& entry)
        : Tag(entry.Tag)
        , Trades(entry.Trades)
        , m_type(entry.m_type)
        , m_leverage(entry.m_leverage)
        , m_balance(entry.m_balance)
        , m_currency(entry.m_currency)
        , m_profit(entry.m_profit)
        , m_profitStatus(entry.m_profitStatus)
        , m_margin(entry.m_margin)
        , m_marginStatus(entry.m_marginStatus)
	{
	}

	CAccountEntry& CAccountEntry::operator = (const CAccountEntry& entry)
	{
		if (this != &entry)
		{
			Tag = entry.Tag;
			Trades = entry.Trades;
			m_type = entry.m_type;
			m_leverage = entry.m_leverage;
			m_balance = entry.m_balance;
			m_currency = entry.m_currency;
			m_profit = entry.m_profit;
			m_profitStatus = entry.m_profitStatus;
			m_margin = entry.m_margin;
			m_marginStatus = entry.m_marginStatus;
		}
		return *this;
	}

#ifdef new
	#pragma push_macro("new")
	#undef new
	#define FX_RESTORE_NEW
#endif

	void CAccountEntry::Construct(AccountType type, const char* currency, double leverage, double balance)
	{
		m_type = type;
		new (&m_currency) std::string(currency);
		m_leverage = leverage;
		m_balance = balance;
		m_profitStatus = AccountEntryStatus_NotCalculated;
		m_marginStatus = AccountEntryStatus_NotCalculated;
	}

#ifdef FX_RESTORE_NEW
	#pragma pop_macro("new")
	#undef FX_RESTORE_NEW
#endif

	CAccountEntry::~CAccountEntry()
	{
	}

	void CAccountEntry::SetAccountType(const AccountType newAccounType)
	{
		if ((AccountType_Net != newAccounType) && (AccountType_Gross != newAccounType))
		{
			throw runtime_error("Unsupported account type");
		}
		m_type = newAccounType;
	}

	void CAccountEntry::SetLeverage(const double newLeverage)
	{
		m_leverage = ValidatePositiveValue(__FUNCTION__, "newLeverage", newLeverage);
	}

	void CAccountEntry::SetBalance(const double newBalance)
	{
		m_balance = ValidateFiniteValue(__FUNCTION__, "newBalance", newBalance);
	}

	const char* CAccountEntry::DoGetCurrency() const
	{
		return m_currency.c_str();
	}

	void CAccountEntry::DoSetCurrency(const char* newCurrency)
	{
		m_currency = newCurrency;
	}

	Nullable<double> CAccountEntry::GetEquity() const
	{
		if (!m_profit.HasValue())
		{
			return Nullable<double>();
		}
        double result = m_balance + m_profit.Value() + GetCommission() + GetAgentCommission() + GetSwap();
		return result;
	}

	Nullable<double> CAccountEntry::GetMarginLevel() const
	{
		if (!m_margin.HasValue())
		{
			return nullptr;
		}
	
		Nullable<double> equity = this->GetEquity();
		if (!equity.HasValue())
		{
			return nullptr;
		}
		double result = equity.Value() / m_margin.Value();
		return result;
	}

	AccountEntryStatus CAccountEntry::GetMarginLevelStatus() const
	{
		if ((AccountEntryStatus_NotCalculated == m_profitStatus) || (AccountEntryStatus_NotCalculated == m_marginStatus))
		{
			return AccountEntryStatus_NotCalculated;
		}
		if ((AccountEntryStatus_UnknownAccountCurrency == m_profitStatus) || (AccountEntryStatus_UnknownAccountCurrency == m_marginStatus))
		{
			return AccountEntryStatus_UnknownAccountCurrency;
		}
		if ((AccountEntryStatus_CalculatedWithErrors == m_profitStatus) || (AccountEntryStatus_CalculatedWithErrors == m_marginStatus))
		{
			return AccountEntryStatus_CalculatedWithErrors;
		}
		return AccountEntryStatus_Calculated;
	}

    double CAccountEntry::GetCommission() const
	{
        double commission = 0;

		auto it = Trades.begin();
		auto end = Trades.end();

		for (; it != end; ++it)
		{
            commission += it->GetCommission();
		}

        return commission;
	}

    double CAccountEntry::GetAgentCommission() const
	{
        double agentCommission = 0;

		auto it = Trades.begin();
		auto end = Trades.end();

		for (; it != end; ++it)
		{
            agentCommission += it->GetAgentCommission();
		}

        return agentCommission;
	}

    double CAccountEntry::GetSwap() const
	{
        double swap = 0;

		auto it = Trades.begin();
		auto end = Trades.end();

		for (; it != end; ++it)
		{
            swap += it->GetSwap();
		}

        return swap;
	}

	void CAccountEntry::Clear()
	{
		m_profit = nullptr;
		m_profitStatus = AccountEntryStatus_NotCalculated;
		m_margin = nullptr;
		m_marginStatus = AccountEntryStatus_NotCalculated;

		auto it = Trades.begin();
		auto end = Trades.end();

		for (; it != end; ++it)
		{
			it->Clear();
		}
	}

	void CAccountEntry::Calcualte(CFinancialCalculator& owner)
	{
		ptrdiff_t zzz = owner.Symbols.TryGetCurrencyIndex(m_currency);
		if (zzz >= 0)
		{
			DoCalculate(zzz, owner);
		}
		else
		{
			m_profitStatus = AccountEntryStatus_UnknownAccountCurrency;
			m_marginStatus = AccountEntryStatus_UnknownAccountCurrency;
		}
	}

	void CAccountEntry::DoCalculate(const ptrdiff_t zzz, CFinancialCalculator& owner)
	{
		CSymbolEntries& symbols = owner.Symbols;

		auto it = Trades.begin();
		auto end = Trades.end();

		for (; it != end; ++it)
		{
			it->SymbolEntry = symbols.TryGetSymbolEntry(it->GetSymbol());
		}

		const MarginMode mode = owner.GetMarginMode();
		symbols.ClearMargin();

		CRates& rates = owner.GetRates();


		if (MarginMode_Dynamic == mode)
		{
			DoCalculateDynamic(zzz, rates);
		}
		else if (MarginMode_Static == mode)
		{
			DoCalculateStatic(zzz, rates);
		}
		else if (MarginMode_StaticIfPossible == mode)
		{
			DoCalculateStaticIfPossible(zzz, rates);
		}
		else
		{
			throw runtime_error("Unknown margin mode");
		}
		DoCollectProfit();
		DoCollectMargin(owner);
	}

	void CAccountEntry::DoCalculateDynamic(const ptrdiff_t zzz, CRates& rates)
	{
		auto it = Trades.begin();
		auto end = Trades.end();
		for(; it != end; ++it)
		{
			if (it->CaculateProlog())
			{
				it->CalculateProfit(zzz, rates);
				it->CalculateDynamicMargin(zzz, m_leverage, rates);
			}
		}
	}

	void CAccountEntry::DoCalculateStatic(const ptrdiff_t zzz, CRates& rates)
	{
		auto it = Trades.begin();
		auto end = Trades.end();
		for(; it != end; ++it)
		{
			if (it->CaculateProlog())
			{
				it->CalculateProfit(zzz, rates);
				it->CalculateStaticMargin(m_leverage);
			}
		}
	}

	void CAccountEntry::DoCalculateStaticIfPossible(const ptrdiff_t zzz, CRates& rates)
	{
		auto it = Trades.begin();
		auto end = Trades.end();
		for(; it != end; ++it)
		{
			if (it->CaculateProlog())
			{
				it->CalculateProfit(zzz, rates);
				it->CalculateStaticIfPossibleMargin(zzz, m_leverage, rates);
			}
		}
	}

	void CAccountEntry::DoCollectProfit()
	{
		AccountEntryStatus status = AccountEntryStatus_Calculated;
		double profit = 0;
		for each (const auto& element in this->Trades)
		{
			if (TradeEntryStatus_Calculated == element.GetProfitStatus())
			{
				Nullable<double> value = element.GetProfit();
				if (value.HasValue())
				{
					profit += value.Value();
				}
			}
			else
			{
				status = AccountEntryStatus_CalculatedWithErrors;
			}
		}
		m_profit = profit;
		m_profitStatus = status;
	}

	void CAccountEntry::DoCollectMargin(CFinancialCalculator& owner)
	{
		CSymbolEntries& symbols = owner.Symbols;
		AccountEntryStatus status = AccountEntryStatus_Calculated;

		for each (const auto& element in this->Trades)
		{
			Nullable<double> value = element.GetMargin();
			if (!value.HasValue())
			{
				status = AccountEntryStatus_CalculatedWithErrors;
				break;
			}
		}
		m_marginStatus = status;
		if (AccountType_Gross == m_type)
		{
			m_margin = symbols.GrossMargin();
		}
		else
		{
			m_margin = symbols.NetMargin();
		}
	}

	std::ostream& operator << (std::ostream& stream, const CAccountEntry& entry)
	{
		Process("Tag", entry.Tag, stream);
		Process("Type", entry.m_type, stream);
		Process("Leverage", entry.m_leverage, stream);
		Process("Balance", entry.m_balance, stream);
		Process("Currency", entry.m_currency, stream);
		Process("Profit", entry.m_profit, stream);
		Process("ProfitStatus", entry.m_profitStatus, stream);
		Process("Margin", entry.m_margin, stream);
		Process("MarginStatus", entry.m_marginStatus, stream);
		return stream;
	}

	std::istream& operator >> (std::istream& stream, CAccountEntry& entry)
	{
		Process("Tag", entry.Tag, stream);
		Process("Type", entry.m_type, stream);
		Process("Leverage", entry.m_leverage, stream);
		Process("Balance", entry.m_balance, stream);
		Process("Currency", entry.m_currency, stream);
		Process("Profit", entry.m_profit, stream);
		Process("ProfitStatus", entry.m_profitStatus, stream);
		Process("Margin", entry.m_margin, stream);
		Process("MarginStatus", entry.m_marginStatus, stream);
		return stream;
	}
}