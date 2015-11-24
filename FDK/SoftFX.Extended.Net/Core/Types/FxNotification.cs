namespace SoftFX.Extended.Data
{
    class Notification
    {
        public Severity Severity { get; set; }
        public NotificationType Type { get; set; }
        public string Text { get; set; }
        public double Balance { get; set; }
        public double TransactionAmount { get; set; }
        public string TransactionCurrency { get; set; }
    }
}
