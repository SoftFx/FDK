namespace SoftFX.Basic
{
    using System;

    /// <summary>
    /// Contains information about exception.
    /// </summary>
    public class ErrorEventArgs : EventArgs
    {
        /// <summary>
        /// Creates a new instance of ErrorEventArgs.
        /// </summary>
        /// <param name="ex">an exception instance; can be null</param>
        public ErrorEventArgs(Exception ex)
        {
            this.Exception = ex;
        }

        /// <summary>
        /// Gets a caught exception.
        /// </summary>
        public Exception Exception { get; private set; }
    }
}

namespace SoftFX.Basic
{
    /// <summary>
    /// Represents the method that will handle an error.
    /// </summary>
    /// <param name="sender">the source of the event</param>
    /// <param name="e">an ErrorEventArgs that contains no event data</param>
    public delegate void ErrorHandler(object sender, ErrorEventArgs e);
}
