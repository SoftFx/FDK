namespace SoftFX.Extended.Financial
{
    /// <summary>
    /// Represents real asset for cash account or exposure for net / gross account.
    /// </summary>
    public class Asset : FinancialEntry<AccountEntry>
    {
        internal Asset(AccountEntry owner)
            : base(owner)
        {
        }

        /// <summary>
        /// The asset currency.
        /// </summary>
        public string Currency { get; internal set; }

        /// <summary>
        /// The asset volume.
        /// </summary>
        public double Volume { get; internal set; }

        /// <summary>
        /// The asset locked volume. For cash accounts only.
        /// </summary>
        public double LockedVolume { get; internal set; }

        /// <summary>
        /// Price rate, which used for converting from volume to deposit currency.
        /// Will be 1 for cash account asset.
        /// </summary>
        public double Rate { get; internal set; }

        /// <summary>
        /// Amount of actually spent deposit currency = Volume * Rate / Leverage.
        /// Equals to Volume for cash account asset.
        /// </summary>
        public double DepositCurrency { get; internal set; }

        /// <summary>
        /// Returns formatted string for the class instance.
        /// </summary>
        /// <returns>Can not be null.</returns>
        public override string ToString()
        {
            var result = string.Format("{0}; Volume = {1}; Rate = {2}", this.Currency, this.Volume, this.Rate);
            return result;
        }
    }
}
