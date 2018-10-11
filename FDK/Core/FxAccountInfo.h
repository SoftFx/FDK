#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)
#pragma warning (disable : 4251)

#include "AssetInfo.h"
#include "ThrottlingMethodInfo.h"

class CORE_API CFxAccountInfo
{
public:
    CFxAccountInfo();
public:
    string AccountId;
    FxAccountType Type;
    string Name;
    string Email;
    wstring Comment;
    string Currency;
    Nullable<CDateTime> RegistredDate;
    Nullable<CDateTime> ModifiedTime;
    int32 Leverage;
    double Balance;
    double Margin;
    double Equity;
    double MarginCallLevel;
    double StopOutLevel;
    bool IsValid;
    bool IsReadOnly;
    bool IsBlocked;
    int32 SessionsPerAccount;
    int32 RequestsPerSecond;
public:
    vector<CAssetInfo> Assets;
    vector<CThrottlingMethodInfo> ThrottlingMethods;
};

#pragma warning (pop)