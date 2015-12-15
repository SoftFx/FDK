#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)
#pragma warning (disable : 4251)

class CORE_API CFxOrder
{
public:
	CFxOrder();
public:
	string OrderId;
	string ClientOrderId;
	string Symbol;
	FxTradeRecordType Type;
	FxTradeRecordSide Side;
	double Price;
	Nullable<double> NewPrice;
	Nullable<double> StopLoss;
	Nullable<double> TakeProfit;
	double InitialVolume;
	double Volume;
	double Commission;
	double AgentCommission;
	double Swap;
	Nullable<double> Profit;
	Nullable<CDateTime> Expiration;
	Nullable<CDateTime> Created;
	Nullable<CDateTime> Modified;
	wstring Comment;
};


#pragma warning (pop)
