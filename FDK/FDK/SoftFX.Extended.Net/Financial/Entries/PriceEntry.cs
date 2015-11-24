namespace SoftFX.Extended.Financial
{
    using System;

    /// <summary>
    /// Represents bid/ask prices entry.
    /// </summary>
    public struct PriceEntry
    {
        #region Fields

        /// <summary>
        /// The best price of bids.
        /// </summary>
        public double Bid { get; private set; }

        /// <summary>
        /// The best price of asks.
        /// </summary>
        public double Ask { get; private set; }

        #endregion

        #region Construction

        /// <summary>
        /// Creates a new price entry instance.
        /// </summary>
        /// <param name="bid">a price for bid</param>
        /// <param name="ask">a price for ask</param>
        internal PriceEntry(double bid, double ask)
            : this()
        {
            this.Bid = bid;
            this.Ask = ask;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns ask for buy and bid for sell
        /// </summary>
        /// <param name="side">trade entry side</param>
        /// <returns></returns>
        public double PriceFromSide(TradeRecordSide side)
        {
            if (side == TradeRecordSide.Buy)
                return this.Ask;
            else if (side == TradeRecordSide.Sell)
                return this.Bid;

            var message = string.Format("Unknown side = {0}", side);
            throw new ArgumentException(message, "side");
        }

        /// <summary>
        /// Returns bid for buy and ask for sell
        /// </summary>
        /// <param name="side">trade entry side</param>
        /// <returns></returns>
        public double PriceFromOppositeSide(TradeRecordSide side)
        {
            if (side == TradeRecordSide.Buy)
                return this.Bid;
            else if (side == TradeRecordSide.Sell)
                return this.Ask;

            var message = string.Format("Unknown side = {0}", side);
            throw new ArgumentException(message, "side");
        }

        /// <summary>
        /// Returns price rate, which should be used as multiplier for converting profit
        /// from profit currency to account currency.
        /// Example: xxx/yyy => zzz
        /// in this case we need profit conversion from yyy to zzz
        /// profit(zzz) = profit(yyy) * PriceMultiplierFromProfit(profit)
        /// </summary>
        /// <param name="profit">a converting profit</param>
        /// <returns></returns>
        public double PriceMultiplierFromProfit(double profit)
        {
            // Price1 - ask if Py < 0, bid if Py >= 0;
            if (profit >= 0)
                return this.Bid;

            return this.Ask;
        }

        /// <summary>
        /// Returns price rate, which should be used as divisor for converting profit
        /// from profit currency to account currency.
        /// Example: xxx/yyy => zzz
        /// in this case we need profit conversion from yyy to zzz
        /// profit(zzz) = profit(yyy) / PriceDivisorFromProfit(profit)
        /// </summary>
        /// <param name="profit">a converting profit</param>
        /// <returns></returns>
        public double PriceDivisorFromProfit(double profit)
        {
            // Price2 - bid if Py < 0, ask if Py >= 0;
            if (profit >= 0)
                return this.Ask;

            return this.Bid;
        }

        /// <summary>
        /// Returns price rate, which should be used as multiplier for converting asset
        /// from asset currency to account currency.
        /// </summary>
        /// <param name="asset">a converting asset</param>
        /// <returns></returns>
        public double PriceMultiplierFromAsset(double asset)
        {
            if (asset >= 0)
                return this.Bid;

            return this.Ask;
        }

        /// <summary>
        /// Returns price rate, which should be used as divisor for converting asset
        /// from asset currency to account currency.
        /// </summary>
        /// <param name="asset">a converting asset</param>
        /// <returns></returns>
        public double PriceDivisorFromAsset(double asset)
        {
            if (asset >= 0)
                return this.Ask;

            return this.Bid;
        }

        #endregion
    }
}
