namespace SoftFX.Extended
{
    /// <summary>
    /// Throttling Method Name.
    /// </summary>
    public enum ThrottlingMethod
    {
        /// <summary>
        /// </summary>
        Login = 0,

        /// <summary>
        /// </summary>
        TwoFactor = 1,

        /// <summary>
        /// </summary>
        SessionInfo = 2,

        /// <summary>
        /// </summary>
        Currencies = 3,

        /// <summary>
        /// </summary>
        Symbols = 4,

        /// <summary>
        /// </summary>
        Ticks = 5,

        /// <summary>
        /// </summary>
        Level2 = 6,

        /// <summary>
        /// </summary>
        Tickers = 7,

        /// <summary>
        /// </summary>
        FeedSubscribe = 8,

        /// <summary>
        /// </summary>
        QuoteHistory = 9,

        /// <summary>
        /// </summary>
        QuoteHistoryCache = 10,

        /// <summary>
        /// </summary>
        TradeSessionInfo = 11,

        /// <summary>
        /// </summary>
        TradeServerInfo = 12,

        /// <summary>
        /// </summary>
        Account = 13,

        /// <summary>
        /// </summary>
        Assets = 14,

        /// <summary>
        /// </summary>
        Positions = 15,

        /// <summary>
        /// </summary>
        Trades = 16,

        /// <summary>
        /// </summary>
        TradeCreate = 17,

        /// <summary>
        /// </summary>
        TradeModify = 18,

        /// <summary>
        /// </summary>
        TradeDelete = 19,

        /// <summary>
        /// </summary>
        TradeHistory = 20,

        /// <summary>
        /// </summary>
        DailyAccountSnapshots = 21,

        /// <summary>
        /// </summary>
        UnknownMethod = 999
    }
}
