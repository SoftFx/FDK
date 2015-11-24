namespace LrpServer.Net
{
    public class LrpNotification
    {
        /// <summary>
        /// Severity.
        /// </summary>
        public LrpSeverity Severity;

        /// <summary>
        /// Notification type.
        /// </summary>
        public LrpNotificationType Type;

        /// <summary>
        /// Message.
        /// </summary>
        public string Text;

        /// <summary>
        /// Balance.
        /// </summary>
        public double Balance;

        /// <summary>
        /// Transaction amount.
        /// </summary>
        public double TransactionAmount;

        /// <summary>
        /// Transaction currency.
        /// </summary>
        public string TransactionCurrency;
    }
}
