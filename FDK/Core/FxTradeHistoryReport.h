#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)
#pragma warning (disable : 4251)

class CORE_API CFxTradeHistoryReport : public FxTradeHistoryReport
{
public:
	string Symbol;
	int32 TradeType;
	int32 ExecutionType;
	double Amount;
	double OpenPrice;
	double ClosePrice;
	CDateTime OpenTime;
	CDateTime CloseTime;
	CDateTime TransactTime;
public:
	CFxTradeHistoryReport();
};

#pragma warning (pop)
