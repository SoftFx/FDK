namespace LrpServer.Net
{
    /// <summary>
    /// Contains possible values of market history rejct type.
    /// </summary>
    public enum LrpMarketHistoryRejectType
    {
        /// <summary>
        /// Success.
        /// </summary>
        Success = 0,

        /// <summary>
        /// Invalid symbol error.
        /// </summary>
        InvalidSymbol = 1,

        /// <summary>
        /// Invalid periodicity.
        /// </summary>
        InvalidPeriodicity = 2,

        /// <summary>
        /// Unknown error.
        /// </summary>
        UnknownError = 99
    }
}
