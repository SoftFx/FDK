namespace SoftFX.Extended.Extensions
{
    using System;
    using SoftFX.Extended.Financial;
    using TickTrader.BusinessLogic;

    /// <summary>
    /// Financial calculator extensions.
    /// </summary>
    public static class FinancialCalculatorExtensions
    {
        /// <summary>
        ///  Calculates asset cross rate.
        /// </summary>
        /// <param name="calculator">Financial calculator.</param>
        /// <param name="asset">Asset volume.</param>
        /// <param name="assetCurrency">Asset currency.</param>
        /// <param name="currency">Deposit currency.</param>
        /// <returns>Rate or null if rate cannot be calculated.</returns>
        public static double? CalculateAssetRate(this FinancialCalculator calculator, double asset, string assetCurrency, string currency)
        {
            if (calculator == null)
                throw new ArgumentNullException("calculator");
            if (assetCurrency == null)
                throw new ArgumentNullException("assetCurrency");
            if (currency == null)
                throw new ArgumentNullException("currency");

            if (calculator.MarketState == null)
                calculator.Calculate();

            try
            {
                if (asset >= 0)
                    return (double)calculator.MarketState.ConversionMap.GetPositiveAssetConversion(assetCurrency, currency).Value;
                else
                    return (double)calculator.MarketState.ConversionMap.GetNegativeAssetConversion(assetCurrency, currency).Value;
            }
            catch (BusinessLogicException)
            {
                return null;
            }
        }
    }
}
