namespace SoftFX.Extended
{
    using System.Globalization;

    /// <summary>
    /// This class has sense for cash accounts only
    /// </summary>
    public class AssetInfo
    {
        /// <summary>
        /// Creates a new empty instance of AssetInfo.
        /// </summary>
        internal AssetInfo()
        {
        }

        /// <summary>
        /// Creates a new instance of AssetInfo.
        /// </summary>
        /// <param name="currency"></param>
        /// <param name="balance"></param>
        /// <param name="lockedAmount"></param>
        /// <param name="tradeAmount"></param>
        /// <param name="currencyToUsdConversionRate"></param>
        /// <param name="usdToCurrencyConversionRate"></param>
        public AssetInfo(string currency, double balance, double lockedAmount, double tradeAmount, double currencyToUsdConversionRate, double usdToCurrencyConversionRate)
        {
            this.Currency = currency;
            this.Balance = balance;
            this.LockedAmount = lockedAmount;
            this.TradeAmount = tradeAmount;
            this.CurrencyToUsdConversionRate = currencyToUsdConversionRate;
            this.UsdToCurrencyConversionRate = usdToCurrencyConversionRate;
        }

        /// <summary>
        /// Gets or sets asset currency.
        /// </summary>
        public string Currency { get; internal set; }

        /// <summary>
        /// Gets or sets asset's balance.
        /// </summary>
        public double Balance { get; internal set; }

        /// <summary>
        /// Gets or sets asset's locked amount.
        /// </summary>
        public double LockedAmount { get; internal set; }

        /// <summary>
        /// Gets or sets asset's trade amount.
        /// </summary>
        public double TradeAmount { get; internal set; }

        /// <summary>
        /// Asset to USD conversion rate.
        /// </summary>
        public double? CurrencyToUsdConversionRate { get; internal set; }

        /// <summary>
        /// USD to Asset conversion rate.
        /// </summary>
        public double? UsdToCurrencyConversionRate { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var result = string.Format("Currency = {0}; Trade Amount = {1}; Locked Amount = {2}; Balance = {3}; CurrencyToUsdConversionRate = {4}; UsdToCurrencyConversionRate = {5}", this.Currency, this.TradeAmount.ToString(CultureInfo.InvariantCulture), this.LockedAmount.ToString(CultureInfo.InvariantCulture), this.Balance.ToString(CultureInfo.InvariantCulture), this.CurrencyToUsdConversionRate, this.UsdToCurrencyConversionRate);
            return result;
        }
    }
}
