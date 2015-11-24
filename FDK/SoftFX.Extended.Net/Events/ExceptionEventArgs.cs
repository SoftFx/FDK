namespace SoftFX.Extended.Events
{
    using System;

    /// <summary>
    /// Contains information about exception.
    /// </summary>
    public class ExceptionEventArgs : EventArgs
    {
        /// <summary>
        /// Gets exception information.
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Creates a new instance of ExceptionEventArgs.
        /// </summary>
        /// <param name="ex"></param>
        public ExceptionEventArgs(Exception ex)
        {
            this.Exception = ex;
        }
    }
}
