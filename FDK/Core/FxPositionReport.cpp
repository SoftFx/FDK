#include "stdafx.h"
#include "FxPositionReport.h"


CFxPositionReport::CFxPositionReport()
    : SettlementPrice()
    , BuyAmount()
    , SellAmount()
    , Commission()
    , AgentCommission()
    , Swap()
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
{
}

const string& CFxPositionReport::GetSymbol() const
{
	return this->Symbol;
}
