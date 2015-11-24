namespace SoftFX.Extended.Events
{
    using System;
    using SoftFX.Extended.Core;

    /// <summary>
    /// Contains data for symbol info event.
    /// </summary>
    public class SymbolInfoEventArgs : EventArgs
    {
        #region Construction

        internal unsafe SymbolInfoEventArgs(FxMessage message, string usedProtocolVersion)
        {
            var protocolVersion = new FixProtocolVersion(usedProtocolVersion);
            this.Information = message.Symbols(protocolVersion);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets symbols information; can not be null.
        /// </summary>
        public SymbolInfo[] Information { get; private set; }

        #endregion
    }
}
