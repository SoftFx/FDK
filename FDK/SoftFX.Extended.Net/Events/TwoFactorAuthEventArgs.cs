namespace SoftFX.Extended.Events
{
    using SoftFX.Extended.Core;

    /// <summary>
    /// Contains data for the two factor auth event.
    /// </summary>
    public class TwoFactorAuthEventArgs : DataEventArgs
    {
        /// <summary>
        /// Contains information about feed/trade two factor auth info.
        /// </summary>
        public TwoFactorAuth TwoFactorAuth { get; private set; }

        internal TwoFactorAuthEventArgs(FxMessage message) : base(message)
        {
            this.TwoFactorAuth = message.TwoFactorAuth();
        }

        /// <summary>
        /// Returns formatted string for the class instance.
        /// </summary>
        /// <returns>can not be null</returns>
        public override string ToString()
        {
            var result = TwoFactorAuth.ToString();
            return result;
        }
    }
}
