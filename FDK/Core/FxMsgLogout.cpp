#include "stdafx.h"
#include "FxMsgLogout.h"

CFxMsgLogout::CFxMsgLogout(const FxLogoutReason reason, int32 code, const string& text)
    : Reason(reason)
    , Code(code)
    , Text(text)
{
}

