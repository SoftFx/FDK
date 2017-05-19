namespace SoftFX.Extended.Events
{
    using SoftFX.Extended.Core;

    /// <summary>
    /// Contains data for the subscribed event.
    /// </summary>
    public class SubscribedEventArgs : DataEventArgs
    {
        internal SubscribedEventArgs(FxMessage message)
            : base(message)
        {
            this.Tick = message.Quote();
        }

        /// <summary>
        /// Gets snapshot tick.
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

    /// <summary>
    /// Contains data for the unsubscribed event.
    /// </summary>
    public class UnsubscribedEventArgs : DataEventArgs
    {
        internal UnsubscribedEventArgs(FxMessage message)
            : base(message)
        {
            this.Symbol = message.Symbol();
        }

        /// <summary>
        /// Gets symbol name.
        /// </summary>
        public string Symbol { get; private set; }

        /// <summary>
        /// Returns formatted string for class instance.
        /// </summary>
        /// <returns>can not be null</returns>
        public override string ToString()
        {
            var result = this.Symbol;
            return result;
        }
    }
}
