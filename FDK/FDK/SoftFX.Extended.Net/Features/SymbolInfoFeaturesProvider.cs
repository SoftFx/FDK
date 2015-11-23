namespace SoftFX.Extended.Features
{
    /// <summary>
    /// The class provides methods, which indicate supported properties and methods for a protocol version.
    /// </summary>
    public class SymbolInfoFeaturesProvider : IFeaturesInfoProvider<SymbolInfoFeatures>
    {
        /// <summary>
        /// Create a new SymbolInfoFeatures instance for a specified version.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If protocol version is null.</exception>
        /// <exception cref="System.ArgumentException">If an input protocol version is unknown.</exception>
        /// <returns>True, if the corresponding property or method is supported for a protocol version, otherwise false.</returns>
        public SymbolInfoFeatures GetInfo(FixProtocolVersion protocolVersion)
        {
            return new SymbolInfoFeatures(protocolVersion, this);
        }

        #region Version Methods

        /// <summary>
        /// Returns true, if a protocol version of symbol info supports 'Name' property, otherwise false.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If protocol version is null.</exception>
        /// <exception cref="System.ArgumentException">If an input protocol version is unknown.</exception>
        /// <returns>True, if the corresponding property or method is supported for a protocol version, otherwise false.</returns>
        public bool IsNameSupported(FixProtocolVersion protocolVersion)
        {
            return protocolVersion >= FixProtocolVersion.Initial;
        }

        /// <summary>
        /// Returns true, if a protocol version of symbol info supports 'Currency' property, otherwise false.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If protocol version is null.</exception>
        /// <exception cref="System.ArgumentException">If an input protocol version is unknown.</exception>
        /// <returns>True, if the corresponding property or method is supported for a protocol version, otherwise false.</returns>
        public bool IsCurrencySupported(FixProtocolVersion protocolVersion)
        {
            return protocolVersion >= FixProtocolVersion.SymbolExtending;
        }

        /// <summary>
        /// Returns true, if a protocol version of symbol info supports 'SettlementCurrency' property, otherwise false.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If protocol version is null.</exception>
        /// <exception cref="System.ArgumentException">If an input protocol version is unknown.</exception>
        /// <returns>True, if the corresponding property or method is supported for a protocol version, otherwise false.</returns>
        public bool IsSettlementCurrencySupported(FixProtocolVersion protocolVersion)
        {
            return protocolVersion >= FixProtocolVersion.SymbolExtending;
        }

        /// <summary>
        /// Returns true, if a protocol version of symbol info supports 'Precision' property, otherwise false.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If protocol version is null.</exception>
        /// <exception cref="System.ArgumentException">If an input protocol version is unknown.</exception>
        /// <returns>True, if the corresponding property or method is supported for a protocol version, otherwise false.</returns>
        public bool IsPrecisionSupported(FixProtocolVersion protocolVersion)
        {
            return protocolVersion >= FixProtocolVersion.SymbolExtending;
        }

        /// <summary>
        /// Returns true, if a protocol version of symbol info supports 'RoundLot' property, otherwise false.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If protocol version is null.</exception>
        /// <exception cref="System.ArgumentException">If an input protocol version is unknown.</exception>
        /// <returns>True, if the corresponding property or method is supported for a protocol version, otherwise false.</returns>
        public bool IsRoundLotSupported(FixProtocolVersion protocolVersion)
        {
            return protocolVersion >= FixProtocolVersion.SymbolExtending;
        }

        /// <summary>
        /// Returns true, if a protocol version of symbol info supports 'MinTradeVolume' property, otherwise false.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If protocol version is null.</exception>
        /// <exception cref="System.ArgumentException">If an input protocol version is unknown.</exception>
        /// <returns>True, if the corresponding property or method is supported for a protocol version, otherwise false.</returns>
        public bool IsMinTradeVolumeSupported(FixProtocolVersion protocolVersion)
        {
            return protocolVersion >= FixProtocolVersion.SymbolExtending;
        }

        /// <summary>
        /// Returns true, if a protocol version of symbol info supports 'MaxTradeVolume' property, otherwise false.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If protocol version is null.</exception>
        /// <exception cref="System.ArgumentException">If an input protocol version is unknown.</exception>
        /// <returns>True, if the corresponding property or method is supported for a protocol version, otherwise false.</returns>
        public bool IsMaxTradeVolumeSupported(FixProtocolVersion protocolVersion)
        {
            return protocolVersion >= FixProtocolVersion.SymbolExtending;
        }

        /// <summary>
        /// Returns true, if a protocol version of symbol info supports 'TradeVolumeStep' property, otherwise false.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If protocol version is null.</exception>
        /// <exception cref="System.ArgumentException">If an input protocol version is unknown.</exception>
        /// <returns>True, if the corresponding property or method is supported for a protocol version, otherwise false.</returns>
        public bool IsTradeVolumeStepSupported(FixProtocolVersion protocolVersion)
        {
            return protocolVersion >= FixProtocolVersion.SymbolExtending;
        }

        /// <summary>
        /// Returns true, if a protocol version of symbol info supports 'ProfitCalcMode' property, otherwise false.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If protocol version is null.</exception>
        /// <exception cref="System.ArgumentException">If an input protocol version is unknown.</exception>
        /// <returns>True, if the corresponding property or method is supported for a protocol version, otherwise false.</returns>
        public bool IsProfitCalcModeSupported(FixProtocolVersion protocolVersion)
        {
            return protocolVersion >= FixProtocolVersion.SymbolExtending;
        }

        /// <summary>
        /// Returns true, if a protocol version of symbol info supports 'MarginCalcMode' property, otherwise false.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If protocol version is null.</exception>
        /// <exception cref="System.ArgumentException">If an input protocol version is unknown.</exception>
        /// <returns>True, if the corresponding property or method is supported for a protocol version, otherwise false.</returns>
        public bool IsMarginCalcModeSupported(FixProtocolVersion protocolVersion)
        {
            return protocolVersion >= FixProtocolVersion.SymbolExtending;
        }

        /// <summary>
        /// Returns true, if a protocol version of symbol info supports 'MarginHedge' property, otherwise false.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If protocol version is null.</exception>
        /// <exception cref="System.ArgumentException">If an input protocol version is unknown.</exception>
        /// <returns>True, if the corresponding property or method is supported for a protocol version, otherwise false.</returns>
        public bool IsMarginHedgeSupported(FixProtocolVersion protocolVersion)
        {
            return protocolVersion >= FixProtocolVersion.SymbolExtending;
        }

        /// <summary>
        /// Returns true, if a protocol version of symbol info supports 'MarginFactor' property, otherwise false.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If protocol version is null.</exception>
        /// <exception cref="System.ArgumentException">If an input protocol version is unknown.</exception>
        /// <returns>True, if the corresponding property or method is supported for a protocol version, otherwise false.</returns>
        public bool IsMarginFactorSupported(FixProtocolVersion protocolVersion)
        {
            return protocolVersion >= FixProtocolVersion.SymbolExtending;
        }

        /// <summary>
        /// Returns true, if a protocol version of symbol info supports 'MarginFactorFractional' property, otherwise false.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If protocol version is null.</exception>
        /// <exception cref="System.ArgumentException">If an input protocol version is unknown.</exception>
        /// <returns>True, if the corresponding property or method is supported for a protocol version, otherwise false.</returns>
        public bool IsMarginFactorFractionalSupported(FixProtocolVersion protocolVersion)
        {
            return protocolVersion >= FixProtocolVersion.Version20;
        }

        /// <summary>
        /// Returns true, if a protocol version of symbol info supports 'ContractMultiplier' property, otherwise false.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If protocol version is null.</exception>
        /// <exception cref="System.ArgumentException">If an input protocol version is unknown.</exception>
        /// <returns>True, if the corresponding property or method is supported for a protocol version, otherwise false.</returns>
        public bool IsContractMultiplierSupported(FixProtocolVersion protocolVersion)
        {
            return protocolVersion >= FixProtocolVersion.Initial;
        }

        /// <summary>
        /// Returns true, if a protocol version of symbol info supports 'Color' property, otherwise false.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If protocol version is null.</exception>
        /// <exception cref="System.ArgumentException">If an input protocol version is unknown.</exception>
        /// <returns>True, if the corresponding property or method is supported for a protocol version, otherwise false.</returns>
        public bool IsColorSupported(FixProtocolVersion protocolVersion)
        {
            return protocolVersion >= FixProtocolVersion.SymbolColorExtending;
        }

        /// <summary>
        /// Returns true, if a protocol version of symbol info supports 'CommissionType' property, otherwise false.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If protocol version is null.</exception>
        /// <exception cref="System.ArgumentException">If an input protocol version is unknown.</exception>
        /// <returns>True, if the corresponding property or method is supported for a protocol version, otherwise false.</returns>
        public bool IsCommissionTypeSupported(FixProtocolVersion protocolVersion)
        {
            return protocolVersion >= FixProtocolVersion.Version15;
        }

        /// <summary>
        /// Returns true, if a protocol version of symbol info supports 'CommissionChargeType' property, otherwise false.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If protocol version is null.</exception>
        /// <exception cref="System.ArgumentException">If an input protocol version is unknown.</exception>
        /// <returns>True, if the corresponding property or method is supported for a protocol version, otherwise false.</returns>
        public bool IsCommissionChargeTypeSupported(FixProtocolVersion protocolVersion)
        {
            return protocolVersion >= FixProtocolVersion.Version15;
        }

        /// <summary>
        /// Returns true, if a protocol version of symbol info supports 'CommissionChargeMethod' property, otherwise false.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If protocol version is null.</exception>
        /// <exception cref="System.ArgumentException">If an input protocol version is unknown.</exception>
        /// <returns>True, if the corresponding property or method is supported for a protocol version, otherwise false.</returns>
        public bool IsCommissionChargeMethodSupported(FixProtocolVersion protocolVersion)
        {
            return protocolVersion >= FixProtocolVersion.Version25;
        }
       

        /// <summary>
        /// Returns true, if a protocol version of symbol info supports 'LimitsCommission' property, otherwise false.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If protocol version is null.</exception>
        /// <exception cref="System.ArgumentException">If an input protocol version is unknown.</exception>
        /// <returns>True, if the corresponding property or method is supported for a protocol version, otherwise false.</returns>
        public bool IsLimitsCommissionSupported(FixProtocolVersion protocolVersion)
        {
            return protocolVersion >= FixProtocolVersion.Version15;
        }

        /// <summary>
        /// Returns true, if a protocol version of symbol info supports 'Commission' property, otherwise false.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If protocol version is null.</exception>
        /// <exception cref="System.ArgumentException">If an input protocol version is unknown.</exception>
        /// <returns>True, if the corresponding property or method is supported for a protocol version, otherwise false.</returns>
        public bool IsCommissionSupported(FixProtocolVersion protocolVersion)
        {
            return protocolVersion >= FixProtocolVersion.Version15;
        }

        /// <summary>
        /// Returns true, if a protocol version of symbol info supports 'SwapSizeShort' and 'SwapSizeLong' properties, otherwise false.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If protocol version is null.</exception>
        /// <exception cref="System.ArgumentException">If an input protocol version is unknown.</exception>
        /// <returns>True, if the corresponding property or method is supported for a protocol version, otherwise false.</returns>
        public bool IsSwapSizeSupported(FixProtocolVersion protocolVersion)
        {
            return protocolVersion >= FixProtocolVersion.Version15;
        }

        /// <summary>
        /// Returns true, if a protocol version of symbol info supports 'TradeEnabled' property, otherwise false.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If protocol version is null.</exception>
        /// <exception cref="System.ArgumentException">If an input protocol version is unknown.</exception>
        /// <returns>True, if the corresponding property or method is supported for a protocol version, otherwise false.</returns>
        public bool IsIsTradeEnabledSupported(FixProtocolVersion protocolVersion)
        {
            return protocolVersion >= FixProtocolVersion.Version16;
        }

        internal bool IsSortOrderSupported(FixProtocolVersion protocolVersion)
        {
            return protocolVersion >= FixProtocolVersion.Version18;
        }

        internal bool IsCurrencySortOrderSupported(FixProtocolVersion protocolVersion)
        {
            return protocolVersion >= FixProtocolVersion.Version20;
        }

        internal bool IsCurrencyPrecisionSupported(FixProtocolVersion protocolVersion)
        {
            return protocolVersion >= FixProtocolVersion.Version23;
        }

        #endregion
    }
}
