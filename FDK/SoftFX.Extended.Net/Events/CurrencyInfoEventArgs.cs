namespace SoftFX.Extended.Events
{
    using System;
    using SoftFX.Extended.Core;

    /// <summary>
    /// Contains data for currency info event.
    /// </summary>
    public class CurrencyInfoEventArgs : EventArgs
    {
        #region Construction

        internal unsafe CurrencyInfoEventArgs(FxMessage message)
        {
            this.Information = message.Currencies();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets currencies information; can not be null.
        /// </summary>
        public CurrencyInfo[] Information { get; private set; }

        #endregion
    }
}
