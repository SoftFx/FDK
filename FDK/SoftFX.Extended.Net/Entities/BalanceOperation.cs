namespace SoftFX.Extended
{
    using SoftFX.Extended.Data;

    /// <summary>
    /// The class contains details of balance operation.
    /// </summary>
    public class BalanceOperation
    {
        internal BalanceOperation(Notification notification)
        {
            this.Balance = notification.Balance;
            this.TransactionAmount = notification.TransactionAmount;
            this.TransactionCurrency = notification.TransactionCurrency;
        }

        /// <summary>
        /// Actual account balance after balance operation.
        /// </summary>
        public double Balance { get; private set; }

        /// <summary>
        /// Amount of a balance transaction.
        /// </summary>
        public double TransactionAmount { get; private set; }

        /// <summary>
        /// Currency of a balance transaction.
        /// </summary>
        public string TransactionCurrency { get; private set; }

        /// <summary>
        /// Returns formatted string for the class instance.
        /// </summary>
        /// <returns>Can not be null.</returns>
        public override string ToString()
        {
            return string.Format("Transaction currency = {0}; Balance = {1}; Transaction amount = {2}", this.TransactionCurrency, this.Balance, this.TransactionAmount);
        }
    }
}
