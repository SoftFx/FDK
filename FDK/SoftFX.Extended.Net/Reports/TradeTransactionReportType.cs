namespace SoftFX.Extended.Reports
{
    /// <summary>
    /// Trade transaction report type.
    /// </summary>
    public enum TradeTransactionReportType
    {
        /// <summary>
        ///
        /// </summary>
        None = -1,

        /// <summary>
        ///
        /// </summary>
        OrderOpened = 0,

        /// <summary>
        ///
        /// </summary>
        OrderCanceled = 1,

        /// <summary>
        ///
        /// </summary>
        OrderExpired = 2,

        /// <summary>
        ///
        /// </summary>
        OrderFilled = 3,

        /// <summary>
        ///
        /// </summary>
        PositionClosed = 4,

        /// <summary>
        ///
        /// </summary>
        BalanceTransaction = 5,

        /// <summary>
        ///
        /// </summary>
        Credit = 6,

        /// <summary>
        ///
        /// </summary>
        PositionOpened = 7,

        /// <summary>
        ///
        /// </summary>
        OrderActivated = 8
    }
}
