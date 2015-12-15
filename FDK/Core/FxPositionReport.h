#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)
#pragma warning (disable : 4251)

class CORE_API CFxPositionReport
{
public:
	CFxPositionReport();
	CFxPositionReport(const string& symbol);
public:
	const string& GetSymbol()const;
public:
	double SettlementPrice;
	double BuyAmount;
	double SellAmount;
	double Commission;
	double AgentCommission;
    double Swap;
	Nullable<double> Profit;
	Nullable<double> Balance;
	Nullable<double> BuyPrice;
	Nullable<double> SellPrice;
	string Symbol;
};

#pragma warning (pop)