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
	string AccountId;
	FxAccountType Type;
	string Name;
	string Email;
	string Comment;
	string Currency;
	Nullable<CDateTime> RegistredDate;
	int32 Leverage;
	double Balance;
	double Margin;
	double Equity;
	double MarginCallLevel;
	double StopOutLevel;
	bool IsValid;
	bool IsReadOnly;
	bool IsBlocked;
public:
	vector<CAssetInfo> Assets;
};

#pragma warning (pop)