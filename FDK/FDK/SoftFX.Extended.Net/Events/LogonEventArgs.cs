namespace SoftFX.Extended.Events
{
    using SoftFX.Extended.Core;

    /// <summary>
    /// Contains data for the logon event.
    /// </summary>
    public class LogonEventArgs : DataEventArgs
    {
        /// <summary>
        /// Get protocol version of logon event.
        /// </summary>
        public string ProtocolVersion { get; private set; }

        internal unsafe LogonEventArgs(FxMessage message)
            : base(message)
        {
            this.ProtocolVersion = message.ProtocolVersion();
        }

        /// <summary>
        /// Returns formatted string for the class instance.
        /// </summary>
        /// <returns>can not be null</returns>
        public override string ToString()
        {
            var result = this.ProtocolVersion;
            if (string.IsNullOrEmpty(result))
            {
                result = base.ToString();
            }
            return result;
        }
    }
}
