namespace SoftFX.Extended.Events
{
    using System;
    using SoftFX.Extended.Core;

    /// <summary>
    /// Contains account information.
    /// </summary>
    public class AccountInfoEventArgs : EventArgs
    {
        /// <summary>
        /// Gets account information.
        /// </summary>
        public AccountInfo Information { get; private set; }

        internal unsafe AccountInfoEventArgs(FxMessage message)
        {
            this.Information = message.AccountInfo();
        }
    }
}
