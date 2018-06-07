#pragma once


class CAssetInfo
{
public:
	string Currency;
	double Balance;
	double LockedAmount;
	double TradeAmount;
    Nullable<double> CurrencyToUsdConversionRate;
    Nullable<double> UsdToCurrencyConversionRate;
public:
	CAssetInfo::CAssetInfo() : Balance(), LockedAmount(), TradeAmount()
	{
	}
};