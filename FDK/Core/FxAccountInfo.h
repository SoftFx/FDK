#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)
#pragma warning (disable : 4251)

#include "AssetInfo.h"

class CORE_API CFxAccountInfo
{
public:
	CFxAccountInfo();
public:
	FxAccountType Type;
	int32 Leverage;
	double Balance;
	double Margin;
	double Equity;
	string Name;
	string Currency;
	string AccountId;
	double MarginCallLevel;
	double StopOutLevel;
	bool IsValid;
	bool IsReadOnly;
    bool IsBlocked;
public:
	vector<CAssetInfo> Assets;
};

#pragma warning (pop)