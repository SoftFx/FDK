#ifndef __Core_SymbolInfo__
#define __Core_SymbolInfo__

class CFxSymbolInfo
{
public:
    CFxSymbolInfo();
    CFxSymbolInfo(const string& name, const string& currency, const string& settlementCurrency);

public:
    string Name;
    string Currency;
    string SettlementCurrency;
    wstring Description;
    double ContractMultiplier;
    int32 Precision;
    double RoundLot;
    double MinTradeVolume;
    double MaxTradeVolume;
    double TradeVolumeStep;
    ProfitCalcMode ProfitCalcMode;
    MarginCalcMode MarginCalcMode;
    double MarginHedge;
    int32 MarginFactor;
    Nullable<double> MarginFactorFractional;
    int32 Color;
    FxCommissionType CommissionType;
    FxCommissionChargeType CommissionChargeType;
    FxCommissionChargeMethod CommissionChargeMethod;
    double Commission;
    double LimitsCommission;
    double MinCommission;
    wstring MinCommissionCurrency;
    SwapType SwapType;
    int32 TripleSwapDay;
    Nullable<double> SwapSizeShort;
    Nullable<double> SwapSizeLong;
    Nullable<double> DefaultSlippage;
    bool IsTradeEnabled;
    int32 GroupSortOrder;
    int32 SortOrder;
    int32 CurrencySortOrder;
    int32 SettlementCurrencySortOrder;
    int32 CurrencyPrecision;
    int32 SettlementCurrencyPrecision;
    string StatusGroupId;
    string SecurityName;
    wstring SecurityDescription;
    Nullable<double> StopOrderMarginReduction;
    Nullable<double> HiddenLimitOrderMarginReduction;
    bool IsCloseOnly;
};

inline CFxSymbolInfo::CFxSymbolInfo()
{
}

inline CFxSymbolInfo::CFxSymbolInfo(const string& name, const string& currency, const string& settlementCurrency)
    : Name(name), Currency(currency), SettlementCurrency(settlementCurrency), MinCommission(0.0), SwapType(FxSwapType_Points), TripleSwapDay(0)
{
}

#endif
