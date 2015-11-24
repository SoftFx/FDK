namespace Mql2Fdk
{
    using System;
    using SoftFX.Extended;

    /// <summary>
    /// 
    /// </summary>
    public partial class MqlAdapter
    {
        #region Market Info

        /// <summary>
        ///  Returns various market data.
        /// </summary>
        /// <param name="symbol">Security symbol</param>
        /// <param name="type">
        /// Expected: MODE_BID, MODE_ASK, MODE_POINT, MODE_DIGITS, MODE_SPREAD, MODE_MINLOT, MODE_LOTSTEP, MODE_MAXLOT
        /// </param>
        /// <returns></returns>
        protected double MarketInfo(string symbol, int type)
        {
            if (type == MODE_BID)
            {
                var quote = this.QuoteFromSymbol(symbol);
                return quote.Bid;
            }

            if (type == MODE_ASK)
            {
                var quote = this.QuoteFromSymbol(symbol);
                return quote.Ask;
            }

            if (type == MODE_SPREAD)
            {
                var quote = this.QuoteFromSymbol(symbol);
                return quote.Spread;
            }

            var info = this.currentSnapshot.Symbols[symbol];
            if (type == MODE_DIGITS)
                return info.Precision;

            if (type == MODE_POINT)
                return Math.Pow(10, -info.Precision);

            if (type == MODE_MINLOT)
                return info.MinTradeVolume / info.RoundLot;

            if (type == MODE_LOTSTEP)
                return info.TradeVolumeStep / info.RoundLot;

            if (type == MODE_MAXLOT)
                return info.MaxTradeVolume / info.RoundLot;

            if (type == MODE_LOTSIZE)
                return info.RoundLot;

            if (type == MODE_SWAPLONG)
                return info.SwapSizeLong ?? 0;

            if (type == MODE_SWAPSHORT)
                return info.SwapSizeShort ?? 0;

            var message = string.Format(
                    "Unsupported type = {0}; expected = MODE_BID, MODE_ASK, MODE_POINT, MODE_DIGITS, MODE_SPREAD, MODE_MINLOT, MODE_LOTSTEP, MODE_MAXLOT, MODE_LOTSIZE, MODE_SWAPLONG or MODE_SWAPSHORT",
                    type
                    );

            throw new ArgumentException(message);
        }

        /// <summary>
        /// The latest known buyer's price (bid price) of the current symbol.
        /// </summary>
        protected double Bid
        {
            get
            {
                var quote = this.QuoteFromSymbol(this.symbol);
                return quote != null ? quote.Bid : 0D;
            }
        }

        /// <summary>
        /// The latest known seller's price (ask price) for the current symbol.
        /// </summary>
        protected double Ask
        {
            get
            {
                var quote = this.QuoteFromSymbol(this.symbol);
                return quote != null ? quote.Ask : 0D;
            }
        }

        /// <summary>
        /// The current symbol point value in the quote currency.
        /// </summary>
        protected double Point
        {
            get
            {
                return Math.Pow(10, -this.Digits);
            }
        }

        /// <summary>
        /// Number of digits after decimal point for the current symbol prices.
        /// </summary>
        protected int Digits
        {
            get
            {
                var info = this.currentSnapshot.Symbols[this.symbol];
                return info.Precision;
            }
        }

        Quote QuoteFromSymbol(string symbol)
        {
            var snapshot = this.currentSnapshot;
            if (snapshot == null)
                return null;

            Quote quote;
            snapshot.Quotes.TryGetValue(symbol, out quote);
            return quote;
        }

        #endregion
    }
}
