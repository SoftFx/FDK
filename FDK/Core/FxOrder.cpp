#include "stdafx.h"
#include "FxOrder.h"

CFxOrder::CFxOrder()
    : InitialType(FxTradeRecordType_None)
    , Type(FxTradeRecordType_None)
    , Side(FxTradeRecordSide_None)
    , InitialVolume()
    , Volume()
    , MaxVisibleVolume()
    , Price()
    , StopPrice()
    , StopLoss()
    , TakeProfit()
    , Commission()
    , AgentCommission()
    , Swap()
    , Profit()
    , Magic()
    , IsReducedOpenCommission(false)
    , IsReducedCloseCommission(false)
    , ImmediateOrCancel(false)
    , MarketWithSlippage(false)
    , IOCOverride()
    , IFMOverride()
    , PrevVolume()
{
}
