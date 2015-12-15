namespace LrpServer.Net
{
    using System;

    public class LrpQuote
    {
        /// <summary>
        /// Get the quote creating time.
        /// </summary>
        public DateTime CreatingTime;

        /// <summary>
        /// Gets symbol name.
        /// </summary>
        public string Symbol;

        /// <summary>
        /// Gets bid quotes; returned array can not be null.
        /// </summary>
        public LrpQuoteEntry[] Bids;

        /// <summary>
        /// Gets ask quotes; returned array can not be null.
        /// </summary>
        public LrpQuoteEntry[] Asks;

        /// <summary>
        /// The identifier is used by quotes storage.
        /// </summary>
        public string Id;
    }
}
