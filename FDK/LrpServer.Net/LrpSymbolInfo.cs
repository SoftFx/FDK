namespace LrpServer.Net
{
    public class LrpSymbolInfo
    {
        public string Name;
        public string Currency;
        public string SettlementCurrency;
        public string Description;
        public int Precision;
        public double RoundLot;
        public double MinTradeVolume;
        public double MaxTradeVolume;
        public double TradeVolumeStep;
        public LrpProfitCalcMode ProfitCalcMode;
        public LrpMarginCalcMode MarginCalcMode;
        public double MarginHedge;
        public int MarginFactor;
        public double? MarginFactorFractional;
        public double ContractMultiplier;
        public int Color;
        public LrpCommissionType CommissionType;
        public LrpCommissionChargeType CommissionChargeType;
        public LrpCommissionChargeMethod CommissionChargeMethod;
        public double LimitsCommission;
        public double Commission;
        public double? SwapSizeShort;
        public double? SwapSizeLong;
        public double? DefaultSlippage;
        public bool IsTradeEnabled;
        public int GroupSortOrder;
        public int SortOrder;
        public int CurrencySortOrder;
        public int SettlementCurrencySortOrder;
        public int CurrencyPrecision;
        public int SettlementCurrencyPrecision;
        public string StatusGroupId;
        public string SecurityDescription;
    }
}
