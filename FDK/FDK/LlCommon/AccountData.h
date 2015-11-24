#pragma once

#include "TradeData.h"
#include "AccountEntry.h"

namespace FDK
{
	class CAccountData
	{
	public:
		CAccountData();
		CAccountData(const CAccountEntry& entry);
	public:
		string Tag;
		AccountType Type;
		double Leverage;
		double Balance;
		string Currency;
		Nullable<double> Profit;
		AccountEntryStatus ProfitStatus;
		Nullable<double> Margin;
		AccountEntryStatus MarginStatus;
		vector<CTradeData> Trades;
	public:
		CAccountEntry ToAccountEntry() const;
	};
}