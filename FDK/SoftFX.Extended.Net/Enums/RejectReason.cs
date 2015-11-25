namespace SoftFX.Extended
{
    /// <summary>
    /// Possible reject reasons.
    /// </summary>
    public enum RejectReason
    {
        /// <summary>
        /// 
        /// </summary>
        None = -1,

        /// <summary>
        /// Dealer reject.
        /// </summary>
        DealerReject = 0,

        /// <summary>
        /// Unknown symbol.
        /// </summary>
        UnknownSymbol = 1,

        /// <summary>
        /// Trade session is closed.
        /// </summary>
        TradeSessionIsClosed = 2,

        /// <summary>
        /// Order exceeds limit.
        /// </summary>
        OrderExceedsLImit = 3,

        /// <summary>
        /// Off quotes
        /// </summary>
        OffQuotes = 4,

        /// <summary>
        /// You try to use (modify, close, delete etc.) unknown order.
        /// </summary>
        UnknownOrder = 5,

        /// <summary>
        /// Duplicate client order ID.
        /// </summary>
        DuplicateClientOrderId = 6,

        /// <summary>
        /// Unsupported order characteristic.
        /// </summary>
        InvalidTradeRecordParameters = 11,

        /// <summary>
        /// Incorrect quantity.
        /// </summary>
        IncorrectQuantity = 13,

        /// <summary>
        /// Trade Not Allowed.
        /// </summary>
        TradeNotAllowed = 14,

        /// <summary>
        /// Unknown error.
        /// </summary>
        Other = 99
    }
}
