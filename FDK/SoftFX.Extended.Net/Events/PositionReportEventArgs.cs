namespace SoftFX.Extended.Events
{
    using System;
    using SoftFX.Extended.Core;

    /// <summary>
    /// Contains data for position report event.
    /// </summary>
    public class PositionReportEventArgs : EventArgs
    {
        internal unsafe PositionReportEventArgs(FxMessage message)
        {
            this.Report = message.Position();
        }

        /// <summary>
        /// Gets a position report; can not be null.
        /// </summary>
        public Position Report { get; private set; }

        /// <summary>
        /// Returns formatted string for the class instance.
        /// </summary>
        /// <returns>Can not be null.</returns>
        public override string ToString()
        {
            return this.Report.ToString();
        }
    }
}
