#include "stdafx.h"
#include "FxTwoFactorAuth.h"

CFxTwoFactorAuth::CFxTwoFactorAuth()
    : Reason()
    , Text()
    , Expire()
{
}

CFxTwoFactorAuth::CFxTwoFactorAuth(const FxTwoFactorReason reason, const std::string& text, const CDateTime& expire)
    : Reason(reason)
    , Text(text)
    , Expire(expire)
{
}
