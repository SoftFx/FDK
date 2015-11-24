namespace SoftFX.Extended
{
    using System;

    /// <summary>
    /// Contains bar information.
    /// </summary>
    public class Bar
    {
        internal Bar()
        {
        }

        /// <summary>
        /// Creates a new bar instance. The constructor doesn't validate input arguments.
        /// </summary>
        /// <param name="from">Start time of the bar.</param>
        /// <param name="to">End time of the bar.</param>
        /// <param name="open">Open price of the bar.</param>
        /// <param name="close">Close price of the bar.</param>
        /// <param name="low">Low price of the bar.</param>
        /// <param name="high">Hight price of the bar.</param>
        /// <param name="volume">Volume of the bar.</param>
        public Bar(DateTime from, DateTime to, double open, double close, double low, double high, double volume)
        {
            this.From = from;
            this.To = to;
            this.Open = open;
            this.Close = close;
            this.Low = low;
            this.High = high;
            this.Volume = volume;
        }

        /// <summary>
        /// Start date and time of the bar.
        /// </summary>
        public DateTime From { get; internal set; }

        /// <summary>
        /// End date and time of the bar.
        /// </summary>
        public DateTime To { get; internal set; }

        /// <summary>
        /// Gets bar open price.
        /// </summary>
        public double Open { get; internal set; }

        /// <summary>
        /// Gets bar close price.
        /// </summary>
        public double Close { get; internal set; }

        /// <summary>
        /// Gets bar highest price.
        /// </summary>
        public double High { get; internal set; }

        /// <summary>
        /// Gets bar lowest price.
        /// </summary>
        public double Low { get; internal set; }

        /// <summary>
        /// Gets volume of the bar period.
        /// </summary>
        public double Volume { get; internal set; }

        /// <summary>
        /// Returns formatted string for the class instance.
        /// </summary>
        /// <returns>Can not be null.</returns>
        public override string ToString()
        {
            return string.Format("From = {0}; To = {1}; Open = {2}; Close = {3}; Low = {4}; High = {5}; Volume = {6}", this.From, this.To, this.Open, this.Close, this.Low, this.High, this.Volume);
        }
    }
}
