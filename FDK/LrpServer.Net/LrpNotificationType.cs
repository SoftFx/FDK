namespace LrpServer.Net
{
    /// <summary>
    /// Contains possible values of notification type.
    /// </summary>
    public enum LrpNotificationType
    {
        /// <summary>
        /// Generic notification.
        /// </summary>
        None = 0,

        /// <summary>
        /// Margin call notification.
        /// </summary>
        MarginCall = 1,

        /// <summary>
        /// Margin call revocation notification.
        /// </summary>
        MarginCallRevocation = 2,

        /// <summary>
        /// Stop out notification.
        /// </summary>
        StopOut = 3,

        /// <summary>
        /// Balance operation: deposit, withdrawal.
        /// </summary>
        Balance = 4,

        /// <summary>
        /// Configuration has been changed.
        /// </summary>
        ConfigUpdated = 5
    }
}
