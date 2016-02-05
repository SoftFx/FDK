namespace SoftFX.Extended
{
    /// <summary>
    /// Describes possible trade record executions.
    /// </summary>
    public enum ExecutionStatus
    {
        /// <summary>
        /// Execution report has unknown status
        /// </summary>
        None = -1,

        /// <summary>
        /// User request has been accepted by dealer.
        /// </summary>
        New = 0,

        /// <summary>
        /// 
        /// </summary>
        Calculated = 1,

        /// <summary>
        /// 
        /// </summary>
        Filled = 2,

        /// <summary>
        /// 
        /// </summary>
        Partial = 3,

        /// <summary>
        /// 
        /// </summary>
        Canceled = 4,

        /// <summary>
        /// 
        /// </summary>
        PendingCancel = 5,

        /// <summary>
        /// 
        /// </summary>
        Rejected = 6,

        /// <summary>
        /// 
        /// </summary>
        Expired = 7,

        /// <summary>
        /// 
        /// </summary>
        PendingReplacement = 8,

        /// <summary>
        /// 
        /// </summary>
        PendingClose = 9
    }
}
