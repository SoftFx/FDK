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
        /// Server request two factor auth.
        /// </summary>
        ServerRequest = 1,

        /// <summary>
        /// Server success response two factor auth.
        /// </summary>
        ServerSuccess = 2,

        /// <summary>
        /// Server error response two factor auth.
        /// </summary>
        ServerError = 3,

        /// <summary>
        /// Client response two factor auth.
        /// </summary>
        ClientResponse = 4,

        /// <summary>
        /// Client resume two factor session.
        /// </summary>
        ClientResume = 5
    }
}
