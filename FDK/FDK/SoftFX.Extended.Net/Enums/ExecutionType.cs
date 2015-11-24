namespace SoftFX.Extended
{
    /// <summary>
    /// Possible execution types.
    /// </summary>
    public enum ExecutionType
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
        Trade = 1,

        /// <summary>
        /// 
        /// </summary>
        Expired = 2,

        /// <summary>
        /// 
        /// </summary>
        Canceled = 3,

        /// <summary>
        /// 
        /// </summary>
        PendingCancel = 4,

        /// <summary>
        /// 
        /// </summary>
        Rejected = 5,

        /// <summary>
        /// 
        /// </summary>
        Calculated = 6,

        /// <summary>
        /// 
        /// </summary>
        PendingReplace = 7,

        /// <summary>
        /// 
        /// </summary>
        Replace = 8,

        /// <summary>
        /// 
        /// </summary>
        OrderStatus = 9,
    }
}
