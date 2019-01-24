#pragma once

#include "AssetInfo.h"
#include "FxPositionReport.h"

#pragma warning (push)
#pragma warning (disable : 4275)
#pragma warning (disable : 4251)

class CORE_API CFxDailyAccountSnapshotReport
{
public:
    CFxDailyAccountSnapshotReport();
public:
    CDateTime Timestamp;
    string AccountId;
    FxAccountType Type;
    std::string BalanceCurrency;
    int32 Leverage;
    double Balance;
    double Margin;
    double MarginLevel;
    double Equity;
    double Swap;
    double Profit;
    double Commission;
    double AgentCommission;
    bool IsValid;
    bool IsReadOnly;
    bool IsBlocked;

    Nullable<double> BalanceCurrencyToUsdConversionRate;
    Nullable<double> UsdToBalanceCurrencyConversionRate;
    Nullable<double> ProfitCurrencyToUsdConversionRate;
    Nullable<double> UsdToProfitCurrencyConversionRate;

    Nullable<double> BalanceCurrencyToReportConversionRate;
    Nullable<double> ReportToBalanceCurrencyConversionRate;
    Nullable<double> ProfitCurrencyToReportConversionRate;
    Nullable<double> ReportToProfitCurrencyConversionRate;

    vector<CAssetInfo> Assets;
	vector<CFxPositionReport> Positions;

    std::string NextStreamPositionId;
    std::string ReportCurrency;
};

#pragma warning (pop)
