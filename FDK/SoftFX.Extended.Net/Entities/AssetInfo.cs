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
        public AssetInfo(string currency, double balance, double lockedAmount, double tradeAmount)
        {
            this.Currency = currency;
            this.Balance = balance;
            this.LockedAmount = lockedAmount;
            this.TradeAmount = tradeAmount;
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
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var result = string.Format("Currency = {0}; Trade Amount = {1}; Locked Amount = {2}; Balance = {3}", this.Currency, this.TradeAmount.ToString(CultureInfo.InvariantCulture), this.LockedAmount.ToString(CultureInfo.InvariantCulture), this.Balance.ToString(CultureInfo.InvariantCulture));
            return result;
        }
    }
}
