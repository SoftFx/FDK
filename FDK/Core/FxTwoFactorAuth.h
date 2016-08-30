#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)
#pragma warning (disable : 4251)

#include "FxHandle.h"


class CORE_API CFxTwoFactorAuth : public CFxHandle
{
public:
    FxTwoFactorReason Reason;
    string Text;
    CDateTime Expire;
public:
    CFxTwoFactorAuth();
    CFxTwoFactorAuth(const FxTwoFactorReason reason, const std::string& text, const CDateTime& expire);
};

#pragma warning (pop)
