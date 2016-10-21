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
    , Commission()
    , AgentCommission()
    , Swap()
    , Profit()
    , Magic()
{
}
