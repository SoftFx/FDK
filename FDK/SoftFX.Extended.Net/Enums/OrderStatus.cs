namespace SoftFX.Extended
{
    /// <summary>
    /// Possible FIX order statuses.
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// 
        /// </summary>
        None = -1,

        /// <summary>
        /// 
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
        PartiallyFilled = 3,

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
        PendingReplace = 8,

        /// <summary>
        /// 
        /// </summary>
        Done = 9,

        /// <summary>
        /// 
        /// </summary>
        PendingClose = 10
    }
}
