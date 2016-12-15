#include "stdafx.h"
#include "FxOrder.h"

CFxOrder::CFxOrder()
    : Type(FxTradeRecordType_None)
    , Side(FxTradeRecordSide_None)
    , Price()
    , StopLoss()
    , TakeProfit()
    , InitialVolume()
    , Volume()
    , HiddenVolume()
    , Commission()
    , AgentCommission()
    , Swap()
    , Profit()
    , Magic()
    , ImmediateOrCancel(false)
    , MarketWithSlippage(false)
{
}
