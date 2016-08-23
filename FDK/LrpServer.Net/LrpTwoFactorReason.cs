namespace LrpServer.Net
{
    /// <summary>
    /// Contains possible values of two factor auth reason.
    /// </summary>
    public enum LrpTwoFactorReason
    {
        /// <summary>
        /// None reason.
        /// </summary>
        None = -1,

        /// <summary>
        /// Unknown reason.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Request two factor auth.
        /// </summary>
        Request = 1,

        /// <summary>
        /// Response two factor auth.
        /// </summary>
        Response = 2,

        /// <summary>
        /// Resume two factor session.
        /// </summary>
        Resume = 3,

        /// <summary>
        /// Invalid one-time-password.
        /// </summary>
        InvalidOtp = 4,

        /// <summary>
        /// Two factro auth is not set for account.
        /// </summary>
        TfaNotSet = 5
    }
}
