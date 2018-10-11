#pragma once

#include "Types.h"

class CThrottlingMethodInfo
{
public:
    FxThrottlingMethod Method;
    int RequestsPerSecond;
public:
    CThrottlingMethodInfo::CThrottlingMethodInfo() : Method(), RequestsPerSecond()
    {
    }
};
