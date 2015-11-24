namespace SoftFX.Extended.Events
{
    using SoftFX.Extended.Core;

    /// <summary>
    /// Contains data for the tick event.
    /// </summary>
    public class TickEventArgs : DataEventArgs
    {
        internal TickEventArgs(FxMessage message)
            : base(message)
        {
            this.Tick = message.Quote();
        }

        /// <summary>
        /// Gets quotes.
        /// </summary>
        public Quote Tick { get; private set; }

        /// <summary>
        /// Returns formatted string for class instance.
        /// </summary>
        /// <returns>can not be null</returns>
        public override string ToString()
        {
            var result = this.Tick.ToString();
            return result;
        }
    }
}
