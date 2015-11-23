#pragma once


class CAssetInfo
{
public:
	string Currency;
	double Balance;
	double TradeAmount;
public:
	CAssetInfo::CAssetInfo() : Balance(), TradeAmount()
	{
	}
};