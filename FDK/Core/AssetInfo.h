#pragma once


class CAssetInfo
{
public:
	string Currency;
	double Balance;
	double LockedAmount;
	double TradeAmount;
public:
	CAssetInfo::CAssetInfo() : Balance(), LockedAmount(), TradeAmount()
	{
	}
};