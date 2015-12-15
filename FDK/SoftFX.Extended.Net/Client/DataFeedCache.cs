namespace SoftFX.Extended
{
    using System;

    /// <summary>
    /// The class contains methods, which are executed in client side.
    /// </summary>
    public class DataFeedCache : DataCache<DataFeed>
    {
        internal DataFeedCache(DataFeed handle)
            : base(handle)
        {
        }

        #region Getting Bid

        /// <summary>
        /// The method gets the best bid price by symbol.
        /// </summary>
        /// <param name="symbol">a required financial security.</param>
        /// <returns>The best bid price.</returns>
        public double GetBid(string symbol)
        {
            double result;
            if (!this.TryGetBid(symbol, out result))
            {
                var message = string.Format("Off quotes for symbol = {0}", symbol);
                throw new ArgumentException(message);
            }
            return result;
        }

        /// <summary>
        /// The method gets the best bid price by symbol.
        /// </summary>
        /// <param name="symbol">a required financial security.</param>
        /// <param name="price">the best bid.</param>
        /// <returns>false, if off quotes, otherwise true.</returns>
        public bool TryGetBid(string symbol, out double price)
        {
            double volume;
            DateTime creationTime;
            return this.TryGetBid(symbol, out price, out volume, out creationTime);
        }

        /// <summary>
        /// The method gets the best bid price, volume and creation time by symbol.
        /// </summary>
        /// <param name="symbol">Can not be null.</param>
        /// <param name="price">the best bid.</param>
        /// <param name="volume">volume of the best bid.</param>
        /// <param name="creationTime">the quote creation time.</param>
        /// <returns>false, if off quotes, otherwise true.</returns>
        public bool TryGetBid(string symbol, out double price, out double volume, out DateTime creationTime)
        {
            return this.Client.DataFeedHandle.TryGetBid(symbol, out price, out volume, out creationTime);
        }

        #endregion

        #region Getting Ask

        /// <summary>
        /// The method gets the best ask price by symbol.
        /// </summary>
        /// <param name="symbol">a required financial security.</param>
        /// <returns>The best ask price.</returns>
        public double GetAsk(string symbol)
        {
            double result;
            if (!this.TryGetAsk(symbol, out result))
            {
                var message = string.Format("Off quotes for symbol = {0}", symbol);
                throw new ArgumentException(message);
            }

            return result;
        }

        /// <summary>
        /// The method gets the best ask price by symbol.
        /// </summary>
        /// <param name="symbol">a required financial security.</param>
        /// <param name="price">the best ask.</param>
        /// <returns>false, if off quotes, otherwise true.</returns>
        public bool TryGetAsk(string symbol, out double price)
        {
            double volume;
            DateTime creationTime;
            return this.TryGetAsk(symbol, out price, out volume, out creationTime);
        }

        /// <summary>
        /// The method gets the best ask price, volume and creation time by symbol.
        /// </summary>
        /// <param name="symbol">Can not be null.</param>
        /// <param name="price">the best ask.</param>
        /// <param name="volume">volume of the best ask.</param>
        /// <param name="creationTime">the quote creation time.</param>
        /// <returns>false, if off quotes, otherwise true.</returns>
        public bool TryGetAsk(string symbol, out double price, out double volume, out DateTime creationTime)
        {
            return this.Client.DataFeedHandle.TryGetAsk(symbol, out price, out volume, out creationTime);
        }

        #endregion

        /// <summary>
        /// The method gets level2 quotes by symbol.
        /// </summary>
        /// <param name="symbol">Can not be null.</param>
        /// <returns>Level2 quotes.</returns>
        public Quote GetLevel2(string symbol)
        {
            return this.Client.DataFeedHandle.GetLevel2(symbol);
        }

        /// <summary>
        /// The method gets level2 quotes by symbol.
        /// </summary>
        /// <param name="symbol">Can not be null.</param>
        /// <param name="quote"></param>
        /// <returns>True, if quote for the symbol is presented, otherwise false.</returns>
        public bool TryGetLevel2(string symbol, out Quote quote)
        {
            return this.Client.DataFeedHandle.TryGetLevel2(symbol, out quote);
        }

        /// <summary>
        /// Gets symbols information. Returned value can not be null.
        /// </summary>
        public SymbolInfo[] Symbols
        {
            get
            {
                return this.Client.DataFeedHandle.Symbols;
            }
        }

        /// <summary>
        /// Gets currencies information. Returned value can not be null.
        /// </summary>
        public CurrencyInfo[] Currencies
        {
            get
            {
                return this.Client.DataFeedHandle.Currencies;
            }
        }
    }
}
