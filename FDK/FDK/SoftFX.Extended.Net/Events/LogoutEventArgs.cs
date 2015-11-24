namespace SoftFX.Extended.Events
{
    using SoftFX.Extended.Core;

    /// <summary>
    /// Contains data for the logout event.
    /// </summary>
    public class LogoutEventArgs : DataEventArgs
    {
        #region Properties

        /// <summary>
        /// Get text description of logout event.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Gets logout reason; supported for version >= ext.1.0.
        /// </summary>
        public LogoutReason Reason { get; private set; }

        /// <summary>
        /// Gets GetLastError() code, if logout reason is connection problem, otherwise 0.
        /// </summary>
        public int Code { get; private set; }

        #endregion

        internal unsafe LogoutEventArgs(FxMessage message) : base(message)
        {
            string text;
            LogoutReason reason;
            int code;
            message.GetLogoutInfo(out text, out reason, out code);

            this.Text = text;
            this.Reason = reason;
            this.Code = code;
        }

        /// <summary>
        /// Returns formatted string for the class instance.
        /// </summary>
        /// <returns>can not be null</returns>
        public override string ToString()
        {
            var result = this.Text;
            if (string.IsNullOrEmpty(result))
            {
                result = base.ToString();
            }
            return result;
        }
    }
}
