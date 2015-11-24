#include "stdafx.h"
#include "AccountData.h"


namespace FDK
{
	CAccountData::CAccountData()
        : Type(AccountType_None)
        , Leverage()
        , Balance()
        , ProfitStatus(AccountEntryStatus_NotCalculated)
        , MarginStatus(AccountEntryStatus_NotCalculated)
	{
	}

	CAccountData::CAccountData(const CAccountEntry& entry)
	{
		stringstream stream;
		stream << entry.Tag;
		Tag = stream.str();

		Type = entry.GetAccountType();
		Leverage = entry.GetLeverage();
		Balance = entry.GetBalance();
		Currency = entry.GetCurrency();
		Profit = entry.GetProfit();
		ProfitStatus = entry.GetProfitStatus();
		Margin = entry.GetMargin();
		MarginStatus = entry.GetMarginStatus();

		Trades.reserve(entry.Trades.size());
		for each(const auto& element in entry.Trades)
		{
			Trades.push_back(CTradeData(element));
		}
	}

	CAccountEntry CAccountData::ToAccountEntry() const
	{
		CAccountEntry result(Type, Currency, Leverage, Balance);
		result.Tag = Tag;
		result.m_profit = Profit;
		result.m_profitStatus = ProfitStatus;
		result.m_margin = Margin;
		result.m_marginStatus = MarginStatus;

		for each(const auto& element in Trades)
		{
			result.Trades.push_back(element.ToTradeEntry());
		}
		return result;
	}

}