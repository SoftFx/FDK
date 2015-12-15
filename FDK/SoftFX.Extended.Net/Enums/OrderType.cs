namespace SoftFX.Extended
{
    enum OrderType
    {
        /// <summary>
        /// An opened position.
        /// </summary>
        Position = 1,

        /// <summary>
        /// An order placed with a brokerage to buy or sell a set number of shares at a specified price or better.
        /// Limit orders also allow an investor to limit the length of time an order can be outstanding before being canceled.
        /// </summary>
        Limit = 2,

        /// <summary>
        /// A stop-limit order will be executed at a specified price (or better) after a given stop price has been reached.
        /// Once the stop price is reached, the stop-limit order becomes a limit order to buy (or sell) at the limit price or better.
        /// </summary>
        Stop = 3
    }
}
