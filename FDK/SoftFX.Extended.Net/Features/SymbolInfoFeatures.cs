namespace SoftFX.Extended.Features
{
    using System;

    /// <summary>
    /// The class provides properties, which indicate supported properties and methods of a corresponding symbol info.
    /// </summary>
    public class SymbolInfoFeatures
    {
        readonly SymbolInfoFeaturesProvider provider;

        /// <summary>
        /// Creates a new instance of SymbolInfoFeatures
        /// </summary>
        /// <param name="protocolVersion">A protocol version; can not be null.</param>
        /// <param name="provider"></param>
        /// <exception cref="System.ArgumentNullException">If protocol version is null.</exception>
        /// <exception cref="System.ArgumentException">If an input protocol version is unknown.</exception>
        public SymbolInfoFeatures(FixProtocolVersion protocolVersion, SymbolInfoFeaturesProvider provider)
        {
            if (protocolVersion == null)
                throw new ArgumentNullException(nameof(protocolVersion));

            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            this.protocolVersion = protocolVersion;
            this.provider = provider;
        }

        #region Properties

        /// <summary>
        /// Returns true, if a corresponding symbol info supports 'Name' property, otherwise false.
        /// </summary>
        public bool IsNameSupported
        {
            get
            {
                return this.provider.IsNameSupported(this.protocolVersion);
            }
        }

        /// <summary>
        /// Returns true, if a corresponding symbol info supports 'Currency' property, otherwise false.
        /// </summary>
        public bool IsCurrencySupported
        {
            get
            {
                return this.provider.IsCurrencySupported(this.protocolVersion);
            }
        }

        /// <summary>
        /// Returns true, if a corresponding symbol info supports 'SettlementCurrency' property, otherwise false.
        /// </summary>
        public bool IsSettlementCurrencySupported
        {
            get
            {
                return this.provider.IsSettlementCurrencySupported(this.protocolVersion);
            }
        }

        /// <summary>
        /// Returns true, if a corresponding symbol info supports 'Precision' property, otherwise false.
        /// </summary>
        public bool IsPrecisionSupported
        {
            get
            {
                return this.provider.IsPrecisionSupported(this.protocolVersion);
            }
        }

        /// <summary>
        /// Returns true, if a corresponding symbol info supports 'RoundLot' property, otherwise false.
        /// </summary>
        public bool IsRoundLotSupported
        {
            get
            {
                return this.provider.IsRoundLotSupported(this.protocolVersion);
            }
        }

        /// <summary>
        /// Returns true, if a corresponding symbol info supports 'MinTradeVolume' property, otherwise false.
        /// </summary>
        public bool IsMinTradeVolumeSupported
        {
            get
            {
                return this.provider.IsMinTradeVolumeSupported(this.protocolVersion);
            }
        }

        /// <summary>
        /// Returns true, if a corresponding symbol info supports 'MinTradeVolume' property, otherwise false.
        /// </summary>
        public bool IsMaxTradeVolumeSupported
        {
            get
            {
                return this.provider.IsMaxTradeVolumeSupported(this.protocolVersion);
            }
        }

        /// <summary>
        /// Returns true, if a corresponding symbol info supports 'radeVolumeStep' property, otherwise false.
        /// </summary>
        public bool IsTradeVolumeStepSupported
        {
            get
            {
                return this.provider.IsTradeVolumeStepSupported(this.protocolVersion);
            }
        }

        /// <summary>
        /// Returns true, if a corresponding symbol info supports 'ProfitCalcMode' property, otherwise false.
        /// </summary>
        public bool IsProfitCalcModeSupported
        {
            get
            {
                return this.provider.IsProfitCalcModeSupported(this.protocolVersion);
            }
        }

        /// <summary>
        /// Returns true, if a corresponding symbol info supports 'MarginCalcMode' property, otherwise false.
        /// </summary>
        public bool IsMarginCalcModeSupported
        {
            get
            {
                return this.provider.IsMarginCalcModeSupported(this.protocolVersion);
            }
        }

        /// <summary>
        /// Returns true, if a corresponding symbol info supports 'MarginHedge' property, otherwise false.
        /// </summary>
        public bool IsMarginHedgeSupported
        {
            get
            {
                return this.provider.IsMarginHedgeSupported(this.protocolVersion);
            }
        }

        /// <summary>
        /// Returns true, if a corresponding symbol info supports 'MarginFactor' property, otherwise false.
        /// </summary>
        public bool IsMarginFactorSupported
        {
            get
            {
                return this.provider.IsMarginFactorSupported(this.protocolVersion);
            }
        }

        /// <summary>
        /// Returns true, if a corresponding symbol info supports 'MarginFactorFractional' property, otherwise false.
        /// </summary>
        public bool IsMarginFactorFractionalSupported
        {
            get
            {
                return this.provider.IsMarginFactorFractionalSupported(this.protocolVersion);
            }
        }

        /// <summary>
        /// Returns true, if a corresponding symbol info supports 'ContractMultiplier' property, otherwise false.
        /// </summary>
        public bool IsContractMultiplierSupported
        {
            get
            {
                return this.provider.IsContractMultiplierSupported(this.protocolVersion);
            }
        }

        /// <summary>
        /// Returns true, if a corresponding symbol info supports 'Color' property, otherwise false.
        /// </summary>
        public bool IsColorSupported
        {
            get
            {
                return this.provider.IsColorSupported(this.protocolVersion);
            }
        }

        /// <summary>
        /// Returns true, if a corresponding symbol info supports 'CommissionType' property, otherwise false.
        /// </summary>
        public bool IsCommissionTypeSupported
        {
            get
            {
                return this.provider.IsCommissionTypeSupported(this.protocolVersion);
            }
        }

        /// <summary>
        /// Returns true, if a corresponding symbol info supports 'CommissionChargeType' property, otherwise false.
        /// </summary>
        public bool IsCommissionChargeTypeSupported
        {
            get
            {
                return this.provider.IsCommissionChargeTypeSupported(this.protocolVersion);
            }
        }

        /// <summary>
        /// Returns true, if a corresponding symbol info supports 'CommissionChargeMethod' property, otherwise false.
        /// </summary>
        public bool IsCommissionChargeMethodSupported
        {
            get
            {
                return this.provider.IsCommissionChargeMethodSupported(this.protocolVersion);
            }
        }

        /// <summary>
        /// Returns true, if a corresponding symbol info supports 'LimitsCommission' property, otherwise false.
        /// </summary>
        public bool IsLimitsCommissionSupported
        {
            get
            {
                return this.provider.IsLimitsCommissionSupported(this.protocolVersion);
            }
        }

        /// <summary>
        /// Returns true, if a corresponding symbol info supports 'Commission' property, otherwise false.
        /// </summary>
        public bool IsCommissionSupported
        {
            get
            {
                return this.provider.IsCommissionSupported(this.protocolVersion);
            }
        }

        /// <summary>
        /// Returns true, if a corresponding symbol info supports 'LimitsCommission' property, otherwise false.
        /// </summary>
        public bool IsSwapSizeShortSupported
        {
            get
            {
                return this.provider.IsSwapSizeSupported(this.protocolVersion);
            }
        }

        /// <summary>
        /// Returns true, if a corresponding symbol info supports 'DefaultSlippage' property, otherwise false.
        /// </summary>
        public bool IsDefaultSlippageSupported
        {
            get
            {
                return this.provider.IsDefaultSlippageSupported(this.protocolVersion);
            }
        }

        /// <summary>
        /// Returns true, if a corresponding symbol info supports 'LimitsCommission' property, otherwise false.
        /// </summary>
        public bool IsSwapSizeLongSupported
        {
            get
            {
                return this.provider.IsSwapSizeSupported(this.protocolVersion);
            }
        }

        /// <summary>
        /// Returns true, if a corresponding symbol info supports 'IsTradeEnabled' property, otherwise false.
        /// </summary>
        public bool IsIsTradeEnabledSupported
        {
            get
            {
                return this.provider.IsIsTradeEnabledSupported(this.protocolVersion);
            }
        }

        public bool IsGroupSortOrderSupported
        {
            get
            {
                return this.provider.IsSortOrderSupported(this.protocolVersion);
            }
        }

        public bool IsSortOrderSupported
        {
            get
            {
                return this.provider.IsSortOrderSupported(this.protocolVersion);
            }
        }

        public bool IsCurrencySortOrderSupported
        {
            get
            {
                return this.provider.IsCurrencySortOrderSupported(this.protocolVersion);
            }
        }

        public bool IsSettlementCurrencySortOrderSupported
        {
            get
            {
                return this.provider.IsCurrencySortOrderSupported(this.protocolVersion);
            }
        }

        public bool IsCurrencyPrecisionSupported
        {
            get
            {
                return this.provider.IsCurrencyPrecisionSupported(this.protocolVersion);
            }
        }

        public bool IsSettlementCurrencyPrecisionSupported
        {
            get
            {
                return this.provider.IsCurrencyPrecisionSupported(this.protocolVersion);
            }
        }

        #endregion

        #region Members

        readonly FixProtocolVersion protocolVersion;

        #endregion
    }
}
