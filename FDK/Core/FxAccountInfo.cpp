#include "stdafx.h"
#include "FxAccountInfo.h"

CFxAccountInfo::CFxAccountInfo()
    : Type(FxAccountType_None)
    , Leverage()
    , Balance()
    , Margin()
    , Equity()
    , MarginCallLevel()
    , StopOutLevel()
    , IsValid(true)
    , IsReadOnly(false)
    , IsBlocked(false)
{
}
