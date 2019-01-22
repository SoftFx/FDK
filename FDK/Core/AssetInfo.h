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
    Nullable<double> CurrencyToReportConversionRate;
    Nullable<double> ReportToCurrencyConversionRate;
public:
	CAssetInfo::CAssetInfo() : Balance(), LockedAmount(), TradeAmount()
	{
	}
};