namespace SoftFX.Extended.Financial
{
    /// <summary>
    /// Possible states of trade entry properties.
    /// </summary>
    public enum TradeEntryStatus
    {
        /// <summary>
        /// Property of trade entry are not calculated.
        /// </summary>
        NotCalculated = 0,

        /// <summary>
        /// Property of trade entry are calculated successfully.
        /// </summary>
        Calculated = 1,

        /// <summary>
        /// Can not calculate property of trade entry due to unknown symbol.
        /// </summary>
        UnknownSymbol = 2,

        /// <summary>
        /// Can not calculate property of trade entry due to off quotes.
        /// </summary>
        OffQuotes = 3
    }
}
