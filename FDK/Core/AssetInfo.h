#pragma once


class CAssetInfo
{
public:
	string Currency;
	double Balance;
	double LockedAmount;
	double TradeAmount;
    Nullable<double> SrcAssetToUsdConversionRate;
    Nullable<double> UsdToSrcAssetConversionRate;
public:
	CAssetInfo::CAssetInfo() : Balance(), LockedAmount(), TradeAmount()
	{
	}
};