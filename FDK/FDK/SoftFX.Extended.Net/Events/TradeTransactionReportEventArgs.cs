namespace SoftFX.Extended.Events
{
    using System;
    using SoftFX.Extended.Core;
    using SoftFX.Extended.Reports;

    /// <summary>
    /// Data for TradeTransactionReport event.
    /// </summary>
    public class TradeTransactionReportEventArgs : EventArgs
    {
        internal unsafe TradeTransactionReportEventArgs(FxMessage message)
        {
            this.Report = message.TradeTransactionReport();
        }

        /// <summary>
        /// Trade transaction report
        /// </summary>
        public TradeTransactionReport Report { get; private set; }
    }
}
