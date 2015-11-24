namespace SoftFX.Extended.Events
{
    using SoftFX.Extended.Core;

    /// <summary>
    /// This message contains current feed/trade session information. It received by the client in following circumstances:
    /// 1. After successful login;
    /// 2. After trading session status is changed on server (opened to closed, closed to opened);
    /// </summary>
    public class SessionInfoEventArgs : DataEventArgs
    {
        /// <summary>
        /// Contains information about feed/trade session info.
        /// </summary>
        public SessionInfo Information { get; private set; }

        internal SessionInfoEventArgs(FxMessage message) : base(message)
        {
            this.Information = message.SessionInfo();
        }

        /// <summary>
        /// Returns formatted string for the class instance.
        /// </summary>
        /// <returns>can not be null</returns>
        public override string ToString()
        {
            var result = Information.ToString();
            return result;
        }
    }
}
