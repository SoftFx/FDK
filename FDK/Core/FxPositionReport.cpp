#include "stdafx.h"
#include "FxPositionReport.h"


CFxPositionReport::CFxPositionReport()
    : SettlementPrice()
    , BuyAmount()
    , SellAmount()
    , Commission()
    , AgentCommission()
    , Swap()
	, PosReportType()
{
}

CFxPositionReport::CFxPositionReport(const string& symbol)
    : SettlementPrice()
    , BuyAmount()
    , SellAmount()
    , Commission()
    , AgentCommission()
    , Swap()
    , Symbol(symbol)
	, PosReportType()
{
}

const string& CFxPositionReport::GetSymbol() const
{
	return this->Symbol;
}
