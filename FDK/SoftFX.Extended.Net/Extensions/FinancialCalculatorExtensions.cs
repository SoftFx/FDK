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
        [Obsolete]
        public static double? CalculateAssetRate(this FinancialCalculator calculator, double asset, string assetCurrency, string currency)
        {
            if (calculator == null)
                throw new ArgumentNullException(nameof(calculator));
            if (assetCurrency == null)
                throw new ArgumentNullException(nameof(assetCurrency));
            if (currency == null)
                throw new ArgumentNullException(nameof(currency));

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
        /// <summary>
        ///  Converts volume in currency Y to currency Z (profit currency to deposit currency)
        /// </summary>
        /// <param name="calculator">Financial calculator.</param>
        /// <param name="amount">Volume.</param>
        /// <param name="symbol">Symbol X/Y.</param>
        /// <param name="depositCurrency">Deposit currency.</param>
        /// <returns>Rate or null if rate cannot be calculated.</returns>
        public static double? ConvertYToZ(this FinancialCalculator calculator, double amount, string symbol, string depositCurrency)
        {
            if (calculator == null)
                throw new ArgumentNullException(nameof(calculator));
            if (symbol == null)
                throw new ArgumentNullException(nameof(symbol));
            if (depositCurrency == null)
                throw new ArgumentNullException(nameof(depositCurrency));

            if (calculator.MarketState == null)
                calculator.Calculate();

            try
            {
                double rate = 1.0;
                if (amount >= 0)
                {
                    rate = (double)calculator.MarketState.ConversionMap.GetPositiveProfitConversion(symbol, depositCurrency).Value;
                }
                else
                {
                    rate = (double)calculator.MarketState.ConversionMap.GetNegativeProfitConversion(symbol, depositCurrency).Value;
                }
                return rate * amount;
            }
            catch (BusinessLogicException)
            {
                return null;
            }
        } 
        
        /// <summary>
        ///  Converts volume in currency X to currency Z (margin currency to deposit currency)
        /// </summary>
        /// <param name="calculator">Financial calculator.</param>
        /// <param name="amount">Volume.</param>
        /// <param name="symbol">Symbol X/Y.</param>
        /// <param name="depositCurrency">Deposit currency.</param>
        /// <returns>Rate or null if rate cannot be calculated.</returns>
        public static double? ConvertXToZ(this FinancialCalculator calculator, double amount, string symbol, string depositCurrency)
        {
            if (calculator == null)
                throw new ArgumentNullException(nameof(calculator));
            if (symbol == null)
                throw new ArgumentNullException(nameof(symbol));
            if (depositCurrency == null)
                throw new ArgumentNullException(nameof(depositCurrency));

            if (calculator.MarketState == null)
                calculator.Calculate();

            try
            {
                double rate = (double)calculator.MarketState.ConversionMap.GetMarginConversion(symbol, depositCurrency).Value;
                
                return rate * amount;                
            }
            catch (BusinessLogicException)
            {
                return null;
            }
        }
    }
}
