namespace SoftFX.Extended
{
    /// <summary>
    /// Possible logout reasons.
    /// </summary>
    public enum LogoutReason
    {
        /// <summary>
        /// Logout reason is not specified.
        /// </summary>
        None = -1,

        /// <summary>
        /// Unknown logout reason.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Physical connection error.
        /// </summary>
        NetworkError = 1,

        /// <summary>
        /// Connection timeout.
        /// </summary>
        Timeout = 2,

        /// <summary>
        /// Your account is blocked.
        /// </summary>
        BlockedAccount = 3,

        /// <summary>
        /// Logout has been initiated by client.
        /// </summary>
        ClientInitiated = 4,

        /// <summary>
        /// Invalid username and/or password.
        /// </summary>
        InvalidCredentials = 5,

        /// <summary>
        /// Your connection is slow.
        /// </summary>
        SlowConnection = 6,

        /// <summary>
        /// FIX: invalid session ID.
        /// </summary>
        InvalidSession = 7,

        /// <summary>
        /// Server error.
        /// </summary>
        ServerError = 8,

        /// <summary>
        /// Your account is temporarily blocked.
        /// </summary>
        LoginTimeout = 9,

        /// <summary>
        /// Account was deleted.
        /// </summary>
        LoginDeleted = 10,

        /// <summary>
        /// Session dropped by a server.
        /// </summary>
        ServerLogout = 11,

        /// <summary>
        /// Account must change password.
        /// </summary>
        MustChangePassword = 12,
    }
}
