namespace SoftFX.Extended
{
    /// <summary>
    /// Contains account information.
    /// </summary>
    public class AccountInfo
    {
        internal AccountInfo()
        {
        }

        /// <summary>
        /// Gets the the account id.
        /// </summary>
        public string AccountId { get; internal set; }

        /// <summary>
        /// Gets the accounting type.
        /// </summary>
        public AccountType Type { get; internal set; }

        /// <summary>
        /// Gets account name.
        /// </summary>
        public string Name { get; internal set; }
	
        /// <summary>
        /// Gets account email.
        /// </summary>
        public string Email { get; internal set; }
	
        /// <summary>
        /// Gets account comment.
        /// </summary>
        public string Comment { get; internal set; }
	
        /// <summary>
        /// Gets the account balance currency.
        /// </summary>
        public string Currency { get; internal set; }
	
        /// <summary>
        /// Gets the account registered date.
        /// </summary>
        public DateTime? RegistredDate { get; internal set; }

        /// <summary>
        /// Gets the account leverage.
        /// </summary>
        public int Leverage { get; internal set; }

        /// <summary>
        /// Gets the account balance.
        /// </summary>
        public double Balance { get; internal set; }

        /// <summary>
        /// Gets the account margin.
        /// </summary>
        public double Margin { get; internal set; }

        /// <summary>
        /// Gets the account equity.
        /// </summary>
        public double Equity { get; internal set; }

        /// <summary>
        /// Gets margin call level.
        /// </summary>
        public double MarginCallLevel { get; internal set; }

        /// <summary>
        /// Get stop out level.7
        /// </summary>
        public double StopOutLevel { get; internal set; }

        /// <summary>
        /// Gets account state:
        /// true, if account is valid
        /// false, if account has broken/invalid trades
        /// </summary>
        public bool IsValid { get; internal set; }

        /// <summary>
        /// Gets true, if account can trade, otherwise false (investor password).
        /// </summary>
        public bool IsReadOnly { get; internal set; }

        /// <summary>
        /// Gets whether account is blocked or not.
        /// </summary>
        public bool IsBlocked { get; internal set; }

        /// <summary>
        /// Gets assets; this feature is available for cash accounts only.
        /// </summary>
        public AssetInfo[] Assets { get; internal set; }

        /// <summary>
        /// Returns formatted string for the class instance.
        /// </summary>
        /// <returns>can not be null</returns>
        public override string ToString()
        {
            return string.Format("AccountId = {0}; Type = {1}; Currency = {2}; Leverage = {3}; Balance = {4}; Equity = {5}; Margin = {6}", this.AccountId, this.Type, this.Currency, this.Leverage, this.Balance, this.Equity, this.Margin);
        }
    }
}
