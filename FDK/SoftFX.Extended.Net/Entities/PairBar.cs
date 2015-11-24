namespace SoftFX.Extended
{
    /// <summary>
    /// Contains bar information for bid and/or ask.
    /// </summary>
    public struct PairBar
    {
        /// <summary>
        /// Creates a new instance of PairBar class.
        /// </summary>
        /// <param name="bid">a bar for bid.</param>
        /// <param name="ask">a bar for ask.</param>
        public PairBar(Bar bid, Bar ask)
            : this()
        {
            this.Bid = bid;
            this.Ask = ask;
        }

        /// <summary>
        /// Gets bar information for bid.
        /// </summary>
        public Bar Bid { get; private set; }

        /// <summary>
        /// Gets bar information for ask.
        /// </summary>
        public Bar Ask { get; private set; }
    }
}
