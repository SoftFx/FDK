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
	Nullable<double> Margin;
	Nullable<double> Profit;
	Nullable<double> Balance;
	Nullable<double> BuyPrice;
	Nullable<double> SellPrice;
	Nullable<double> CurrentBestAsk;
	Nullable<double> CurrentBestBid;
	Nullable<CDateTime> PosModified;
	string PosID;
	string Symbol;
};

#pragma warning (pop)