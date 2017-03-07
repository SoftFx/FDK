namespace SoftFX.Extended
{
    using SoftFX.Extended.Features;

    /// <summary>
    /// Contains symbol parameters.
    /// </summary>
    public class SymbolInfo : FeaturesInfo<SymbolInfoFeaturesProvider, SymbolInfoFeatures>
    {
        internal SymbolInfo()
        {
        }

        #region Properties

        /// <summary>
        /// Gets symbol name.
        /// </summary>
        /// <exception cref="SoftFX.Extended.Errors.UnsupportedFeatureException">If the feature is not supported by used protocol version.</exception>
        public string Name
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.Name);
                return this.name;
            }
            internal set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// Gets currency of the symbol.
        /// </summary>
        /// <exception cref="SoftFX.Extended.Errors.UnsupportedFeatureException">If the feature is not supported by used protocol version.</exception>
        public string Currency
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.Currency);
                return this.currency;
            }
            internal set
            {
                this.currency = value;
            }
        }

        /// <summary>
        /// Gets settlement currency of the symbol.
        /// </summary>
        /// <exception cref="SoftFX.Extended.Errors.UnsupportedFeatureException">If the feature is not supported by used protocol version.</exception>
        public string SettlementCurrency
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.SettlementCurrency);
                return this.settlementCurrency;
            }
            internal set
            {
                this.settlementCurrency = value;
            }
        }

        /// <summary>
        /// Gets description of the symbol.
        /// </summary>
        /// <exception cref="SoftFX.Extended.Errors.UnsupportedFeatureException">If the feature is not supported by used protocol version.</exception>
        public string Description
        {
            get
            {
//                this.ThrowIfPropertyNotSupported(() => this.Description);
                return this.description;
            }
            internal set
            {
                this.description = value;
            }
        }

        /// <summary>
        /// Gets precision of the symbol.
        /// </summary>
        /// <exception cref="SoftFX.Extended.Errors.UnsupportedFeatureException">If the feature is not supported by used protocol version.</exception>
        public int Precision
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.Precision);
                return this.precision;
            }
            internal set
            {
                this.precision = value;
            }
        }

        /// <summary>
        /// Gets round lot of the symbol.
        /// </summary>
        /// <exception cref="SoftFX.Extended.Errors.UnsupportedFeatureException">If the feature is not supported by used protocol version.</exception>
        public double RoundLot
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.RoundLot);
                return this.roundLot;
            }
            internal set
            {
                this.roundLot = value;
            }
        }

        /// <summary>
        /// Gets minimum trade volume of the symbol.
        /// </summary>
        /// <exception cref="SoftFX.Extended.Errors.UnsupportedFeatureException">If the feature is not supported by used protocol version.</exception>
        public double MinTradeVolume
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.MinTradeVolume);
                return this.minTradeVolume;
            }
            internal set
            {
                this.minTradeVolume = value;
            }
        }

        /// <summary>
        /// Gets maximum trade volume of the symbol.
        /// </summary>
        /// <exception cref="SoftFX.Extended.Errors.UnsupportedFeatureException">If the feature is not supported by used protocol version.</exception>
        public double MaxTradeVolume
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.MaxTradeVolume);
                return this.maxTradeVolume;
            }
            internal set
            {
                this.maxTradeVolume = value;
            }
        }

        /// <summary>
        /// Gets trading volume step of the symbol.
        /// </summary>
        /// <exception cref="SoftFX.Extended.Errors.UnsupportedFeatureException">If the feature is not supported by used protocol version.</exception>
        public double TradeVolumeStep
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.TradeVolumeStep);
                return this.tradeVolumeStep;
            }
            internal set
            {
                this.tradeVolumeStep = value;
            }
        }

        /// <summary>
        /// Gets profit calculation mode of the symbol.
        /// </summary>
        /// <exception cref="SoftFX.Extended.Errors.UnsupportedFeatureException">If the feature is not supported by used protocol version.</exception>
        public ProfitCalcMode ProfitCalcMode
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.ProfitCalcMode);
                return this.profitCalcMode;
            }
            internal set
            {
                this.profitCalcMode = value;
            }
        }

        /// <summary>
        /// Gets margin calculation mode of the symbol.
        /// </summary>
        /// <exception cref="SoftFX.Extended.Errors.UnsupportedFeatureException">If the feature is not supported by used protocol version.</exception>
        public MarginCalcMode MarginCalcMode
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.MarginCalcMode);
                return this.marginCalcMode;
            }
            internal set
            {
                this.marginCalcMode = value;
            }
        }

        /// <summary>
        /// Gets margin hedge of the symbol.
        /// </summary>
        /// <exception cref="SoftFX.Extended.Errors.UnsupportedFeatureException">If the feature is not supported by used protocol version.</exception>
        public double MarginHedge
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.MarginHedge);
                return this.marginHedge;
            }
            internal set
            {
                this.marginHedge = value;
            }
        }

        /// <summary>
        /// Gets margin factor of the symbol.
        /// </summary>
        /// <exception cref="SoftFX.Extended.Errors.UnsupportedFeatureException">If the feature is not supported by used protocol version.</exception>
        public int MarginFactor
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.MarginFactor);
                return this.marginFactor;
            }
            internal set
            {
                this.marginFactor = value;
            }
        }

        /// <summary>
        /// Gets margin factor of the symbol.
        /// </summary>
        /// <exception cref="SoftFX.Extended.Errors.UnsupportedFeatureException">If the feature is not supported by used protocol version.</exception>
        public double? MarginFactorFractional
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.MarginFactorFractional);
                return this.marginFactorFractional;
            }
            internal set
            {
                this.marginFactorFractional = value;
            }
        }

        /// <summary>
        /// Gets contract multiplier.
        /// </summary>
        /// <exception cref="SoftFX.Extended.Errors.UnsupportedFeatureException">If the feature is not supported by used protocol version.</exception>
        public double ContractMultiplier
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.ContractMultiplier);
                return this.contractMultiplier;
            }
            internal set
            {
                this.contractMultiplier = value;
            }
        }

        /// <summary>
        /// Gets color of the symbol assigned by server.
        /// </summary>
        /// <exception cref="SoftFX.Extended.Errors.UnsupportedFeatureException">If the feature is not supported by used protocol version.</exception>
        public int Color
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.Color);
                return this.color;
            }
            internal set
            {
                this.color = value;
            }
        }

        /// <summary>
        /// Gets commission type.
        /// </summary>
        /// <exception cref="SoftFX.Extended.Errors.UnsupportedFeatureException">If the feature is not supported by used protocol version.</exception>
        public CommissionType CommissionType
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.CommissionType);
                return this.commType;
            }
            internal set
            {
                this.commType = value;
            }
        }

        /// <summary>
        /// Gets commission charge type.
        /// </summary>
        /// <exception cref="SoftFX.Extended.Errors.UnsupportedFeatureException">If the feature is not supported by used protocol version.</exception>
        public CommissionChargeType CommissionChargeType
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.CommissionChargeType);
                return this.commChargeType;
            }
            internal set
            {
                this.commChargeType = value;
            }
        }

        /// <summary>
        /// Gets commission charge method.
        /// </summary>
        /// <exception cref="SoftFX.Extended.Errors.UnsupportedFeatureException">If the feature is not supported by used protocol version.</exception>
        public CommissionChargeMethod CommissionChargeMethod
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.CommissionChargeMethod);
                return this.commChargeMethod;
            }
            internal set
            {
                this.commChargeMethod = value;
            }
        }

        /// <summary>
        /// Gets commission value for limits.
        /// </summary>
        /// <exception cref="SoftFX.Extended.Errors.UnsupportedFeatureException">If the feature is not supported by used protocol version.</exception>
        public double LimitsCommission
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.LimitsCommission);
                return this.limitsCommission;
            }
            internal set
            {
                this.limitsCommission = value;
            }
        }

        /// <summary>
        /// Gets commission value.
        /// </summary>
        /// <exception cref="SoftFX.Extended.Errors.UnsupportedFeatureException">If the feature is not supported by used protocol version.</exception>
        public double Commission
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.Commission);
                return this.commission;
            }
            internal set
            {
                this.commission = value;
            }
        }

        /// <summary>
        /// Gets swap size short.
        /// </summary>
        public double? SwapSizeShort
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.SwapSizeShort);
                return this.swapSizeShort;
            }
            set
            {
                this.swapSizeShort = value;
            }
        }

        /// <summary>
        /// Gets swap size long.
        /// </summary>
        public double? SwapSizeLong
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.SwapSizeLong);
                return this.swapSizeLong;
            }
            set
            {
                this.swapSizeLong = value;
            }
        }

        /// <summary>
        /// Gets default slippage.
        /// </summary>
        public double? DefaultSlippage
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.DefaultSlippage);
                return this.defaultSlippage;
            }
            set
            {
                this.defaultSlippage = value;
            }
        }

        /// <summary>
        /// Gets whether trade is enabled for this symbol.
        /// </summary>
        public bool IsTradeEnabled
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.IsTradeEnabled);
                return this.isTradeEnabled;
            }
            set
            {
                this.isTradeEnabled = value;
            }
        }

        internal int GroupSortOrder
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.GroupSortOrder);
                return this.groupSortOrder;
            }
            set
            {
                this.groupSortOrder = value;
            }
        }

        internal int SortOrder
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.SortOrder);
                return this.sortOrder;
            }
            set
            {
                this.sortOrder = value;
            }
        }

        internal int CurrencySortOrder
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.CurrencySortOrder);
                return this.currencySortOrder;
            }
            set
            {
                this.currencySortOrder = value;
            }
        }

        internal int SettlementCurrencySortOrder
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.SettlementCurrencySortOrder);
                return this.settlementCurrencySortOrder;
            }
            set
            {
                this.settlementCurrencySortOrder = value;
            }
        }

        internal int CurrencyPrecision
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.CurrencyPrecision);
                return this.currencyPrecision;
            }
            set
            {
                this.currencyPrecision = value;
            }
        }

        internal int SettlementCurrencyPrecision
        {
            get
            {
                this.ThrowIfPropertyNotSupported(() => this.SettlementCurrencyPrecision);
                return this.settlementCurrencyPrecision;
            }
            set
            {
                this.settlementCurrencyPrecision = value;
            }
        }

        /// <summary>
        /// Symbol status group id.
        /// </summary>
        public string StatusGroupId
        {
            get
            {
//                this.ThrowIfPropertyNotSupported(() => this.StatusGroupId);
                return this.statusGroupId;
            }
            internal set
            {
                this.statusGroupId = value;
            }
        }

        /// <summary>
        /// Symbol security name.
        /// </summary>
        public string SecurityName
        {
            get
            {
//                this.ThrowIfPropertyNotSupported(() => this.SecurityName);
                return this.securityName;
            }
            internal set
            {
                this.securityName = value;
            }
        }

        /// <summary>
        /// Symbol security description.
        /// </summary>
        public string SecurityDescription
        {
            get
            {
//                this.ThrowIfPropertyNotSupported(() => this.SecurityDescription);
                return this.securityDescription;
            }
            internal set
            {
                this.securityDescription = value;
            }
        }

        #endregion

        /// <summary>
        /// Converts SymbolInfo to string; format is 'Name = {0}; ContractMultiplier = {1}'
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var result = string.Format("Name = {0}; ContractMultiplier = {1}; StatusGroupId = {2}; Descrtiption = {3}", this.Name, this.ContractMultiplier, this.StatusGroupId, this.Description);
            return result;
        }

        #region Members

        string name;
        string currency;
        string settlementCurrency;
        string description;
        int precision;
        double roundLot;
        double minTradeVolume;
        double maxTradeVolume;
        double tradeVolumeStep;
        ProfitCalcMode profitCalcMode;
        MarginCalcMode marginCalcMode;
        double marginHedge;
        int marginFactor;
        double? marginFactorFractional;
        double contractMultiplier;
        int color;
        double limitsCommission;
        double commission;
        CommissionType commType;
        CommissionChargeType commChargeType;
        CommissionChargeMethod commChargeMethod;
        double? swapSizeShort;
        double? swapSizeLong;
        double? defaultSlippage;
        bool isTradeEnabled;
        int groupSortOrder;
        int sortOrder;
        int currencySortOrder;
        int settlementCurrencySortOrder;
        int currencyPrecision;
        int settlementCurrencyPrecision;
        string statusGroupId;
        string securityName;
        string securityDescription;

        #endregion
    }
}
