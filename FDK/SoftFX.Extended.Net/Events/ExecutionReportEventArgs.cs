namespace SoftFX.Extended.Events
{
    using System;
    using SoftFX.Extended.Core;

    /// <summary>
    /// Contains data for execution report event.
    /// </summary>
    public class ExecutionReportEventArgs : EventArgs
    {
        internal unsafe ExecutionReportEventArgs(FxMessage message)
        {
            this.Report = message.ExecutionReport();
        }

        /// <summary>
        /// Get corresponded execution report; can not be null.
        /// </summary>
        public ExecutionReport Report { get; private set; }
    }
}
