#include "stdafx.h"
#include "FxDailyAccountSnapshotReport.h"

CFxDailyAccountSnapshotReport::CFxDailyAccountSnapshotReport()
    : Timestamp()
    , AccountId()
    , Type(FxAccountType_None)
    , BalanceCurrency()
    , Leverage()
    , Balance()
    , Margin()
    , MarginLevel()
    , Equity()
    , Swap()
    , Profit()
    , Commission()
    , AgentCommission()
    , IsValid(true)
    , IsReadOnly(false)
    , IsBlocked(false)
{
}
