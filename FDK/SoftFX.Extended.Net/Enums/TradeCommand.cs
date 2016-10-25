namespace SoftFX.Extended
{
    /// <summary>
    /// Contains possible trade commands.
    /// </summary>
    public enum TradeCommand
    {
        /// <summary>
        /// An order that an investor makes through a broker or brokerage service to buy or sell an investment immediately at the best available current price.
        /// </summary>
        Market = 0,

        /// <summary>
        /// An order placed with a brokerage to buy or sell a set number of shares at a specified price or better.
        /// Limit orders also allow an investor to limit the length of time an order can be outstanding before being canceled.
        /// </summary>
        Limit = 2,

        /// <summary>
        /// An order to buy or sell a security when its price surpasses a particular point.
        /// </summary>
        Stop = 3,

        /// <summary>
        /// An order to buy or sell a security only if its price matches a particular point.
        /// </summary>
        IoC = 4,

        /// <summary>
        /// Market order with slippage. Will be send as a limit order with IOC flag.
        /// </summary>
        MarketWithSlippage = 5
    }
}
