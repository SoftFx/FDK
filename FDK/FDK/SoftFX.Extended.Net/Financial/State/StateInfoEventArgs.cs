namespace SoftFX.Extended.Financial
{
    using System;

    /// <summary>
    /// Provides data for the SoftFX.Extended.Financial.StateCalculator.StateInfoChanged event.
    /// </summary>
    public class StateInfoEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the SoftFX.Extended.Financial.StateInfoEventArgs
        /// </summary>
        /// <param name="info">Represents the current state.</param>
        public StateInfoEventArgs(StateInfo info)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            this.Information = info;
        }

        /// <summary>
        /// Current state information.
        /// </summary>
        public StateInfo Information { get; private set; }
    }
}
