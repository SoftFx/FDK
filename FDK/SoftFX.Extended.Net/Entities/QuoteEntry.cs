namespace SoftFX.Extended
{
    using System;

    /// <summary>
    /// Contains price and volume of bid or ask.
    /// </summary>
    public struct QuoteEntry
    {
        /// <summary>
        /// Creates a new quote entry.
        /// </summary>
        /// <param name="price">Price should be positive or zero.</param>
        /// <param name="volume">Volume should be positive or zero.</param>
        /// <exception cref="System.ArgumentOutOfRangeException ">If price/volume are negative or NAN.</exception>
        public QuoteEntry(double price, double volume)
            : this()
        {
            if (double.IsNaN(price) || (price < 0))
            {
                var message = string.Format("Invalid price = {0}", price);
                throw new ArgumentOutOfRangeException("price", message);
            }

            if (double.IsNaN(volume) || (volume < 0))
            {
                var message = string.Format("Invalid volume = {0}", volume);
                throw new ArgumentOutOfRangeException("volume", message);
            }

            this.Price = price;
            this.Volume = volume;
        }

        /// <summary>
        /// Price of the quote.
        /// </summary>
        public double Price { get; internal set; }

        /// <summary>
        /// Volume of the quote.
        /// </summary>
        public double Volume { get; internal set; }

        /// <summary>
        /// Returns formatted string for the class instance.
        /// </summary>
        /// <returns>can not be null</returns>
        public override string ToString()
        {
            return string.Format("Price = {0}; Volume = {1};", this.Price, this.Volume);
        }
    }
}
