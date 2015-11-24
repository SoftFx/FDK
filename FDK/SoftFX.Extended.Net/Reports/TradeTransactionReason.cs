namespace SoftFX.Extended.Reports
{
    /// <summary>
    /// Trade transaction reason.
    /// </summary>
    public enum TradeTransactionReason
    {
        /// <summary>
        /// 
        /// </summary>
        None = -1,

        /// <summary>
        /// 
        /// </summary>
        ClientRequest = 0,

        /// <summary>
        /// 
        /// </summary>
        PendingOrderActivation = 1,

        /// <summary>
        /// 
        /// </summary>
        StopOut = 2,

        /// <summary>
        /// 
        /// </summary>
        StopLossActivation = 3,

        /// <summary>
        /// 
        /// </summary>
        TakeProfitActivation = 4,

        /// <summary>
        /// 
        /// </summary>
        DealerDecision = 5
    }
}
