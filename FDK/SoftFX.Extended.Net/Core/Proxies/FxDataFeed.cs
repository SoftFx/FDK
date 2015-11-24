namespace SoftFX.Extended.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SoftFX.Lrp;

    unsafe struct FxDataFeed
    {
        #region Creating and Converting

        public static FxDataFeed Create(string connectionString)
        {
            var handle = Native.FeedServer.Create(connectionString);
            return new FxDataFeed(handle);
        }

        public FxDataFeed(LPtr handle)
        {
            this.handle = handle;
        }

        public FxHandle Handle
        {
            get
            {
                return new FxHandle(this.handle);
            }
        }

        public FxDataClient DataClient
        {
            get
            {
                return new FxDataClient(this.handle);
            }
        }

        #endregion

        #region Server Methods

        public CurrencyInfo[] GetCurrencies(int timeoutInMilliseconds)
        {
            this.VerifyInitialized();

            return Native.FeedServer.GetCurrencies(this.handle, (uint)timeoutInMilliseconds);
        }

        public SymbolInfo[] GetSymbols(int timeoutInMilliseconds)
        {
            this.VerifyInitialized();

            var symbols = Native.FeedServer.GetSymbols(this.handle, (uint)timeoutInMilliseconds);
            var protocolVersion = new FixProtocolVersion(this.DataClient.ProtocolVersion);

            foreach (var symbol in symbols)
            {
                symbol.ProtocolVersion = protocolVersion;
            }

            return symbols;
        }

        public void SubscribeToQuotes(IEnumerable<string> symbols, int depth, int timeoutInMilliseconds)
        {
            this.VerifyInitialized();

            Native.FeedServer.SubscribeToQuotes(this.handle, symbols.ToArray(), depth, (uint)timeoutInMilliseconds);
        }

        public void UnsubscribeQuotes(IEnumerable<string> symbols, int timeoutInMilliseconds)
        {
            this.VerifyInitialized();

            Native.FeedServer.UnsubscribeQuotes(this.handle, symbols.ToArray(), (uint)timeoutInMilliseconds);
        }

        public DataHistoryInfo GetHistoryBars(string symbol, DateTime time, int barsNumber, PriceType priceType, BarPeriod period, int timeoutInMilliseconds)
        {
            this.VerifyInitialized();

            var info = Native.FeedServer.GetHistoryBars(this.handle, symbol, time, barsNumber, priceType, period.ToString(), (uint)timeoutInMilliseconds);
            foreach (var bar in info.Bars)
            {
                bar.To = bar.From + period;
            }
            return info;
        }

        public DataHistoryInfo GetBarsHistoryFiles(string symbol, DateTime time, PriceType priceType, string period, int timeoutInMilliseconds)
        {
            this.VerifyInitialized();

            return Native.FeedServer.GetBarsHistoryFiles(this.handle, symbol, priceType, period, time, (uint)timeoutInMilliseconds);
        }

        public DataHistoryInfo GetQuoteHistoryFiles(string symbol, bool includeLevel2, DateTime time, int timeoutInMilliseconds)
        {
            this.VerifyInitialized();

            return Native.FeedServer.GetQuoteHistoryFiles(this.handle, symbol, includeLevel2, time, (uint)timeoutInMilliseconds);
        }

        public string GetBarsHistoryMetaInfoFile(string symbol, PriceType priceType, string period, int timeoutInMilliseconds)
        {
            this.VerifyInitialized();

            return Native.FeedServer.GetBarsHistoryMetaInfoFile(this.handle, symbol, priceType, period, (uint)timeoutInMilliseconds);
        }

        public string GetQuotesHistoryMetaInfoFile(string symbol, bool includeLevel2, int timeoutInMilliseconds)
        {
            this.VerifyInitialized();

            return Native.FeedServer.GetQuotesHistoryMetaInfoFile(this.handle, symbol, includeLevel2, (uint)timeoutInMilliseconds);
        }

        #endregion

        #region Local Methods

        public bool TryGetBid(string symbol, out double price, out double volume, out DateTime creationTime)
        {
            this.VerifyInitialized();

            return Native.FeedCache.TryGetBid(this.handle, symbol, out price, out volume, out creationTime);
        }

        public bool TryGetAsk(string symbol, out double price, out double volume, out DateTime creationTime)
        {
            this.VerifyInitialized();

            return Native.FeedCache.TryGetAsk(this.handle, symbol, out price, out volume, out creationTime);
        }

        public Quote GetLevel2(string symbol)
        {
            this.VerifyInitialized();

            Quote quote;
            Native.FeedCache.TryGetQuote(this.handle, symbol, out quote);
            return quote;
        }

        public bool TryGetLevel2(string symbol, out Quote quote)
        {
            this.VerifyInitialized();

            return Native.FeedCache.TryGetQuote(this.handle, symbol, out quote);
        }

        public int GetQueueThreshold()
        {
            this.VerifyInitialized();

            return Native.FeedServer.GetQueueThreshold(this.handle);
        }

        public void SetQueueThreshold(int newSize)
        {
            this.VerifyInitialized();

            Native.FeedServer.SetQueueThreshold(this.handle, newSize);
        }

        public int GetQuotesHistoryVersion(int timeoutInMilliseconds)
        {
            this.VerifyInitialized();

            return Native.FeedServer.GetQuotesHistoryVersion(this.handle, (uint)timeoutInMilliseconds);
        }

        public SymbolInfo[] Symbols
        {
            get
            {
                this.VerifyInitialized();

                var symbols = Native.FeedCache.GetSymbols(this.handle);
                var protocolVersion = new FixProtocolVersion(this.DataClient.ProtocolVersion);

                foreach (var symbol in symbols)
                {
                    symbol.ProtocolVersion = protocolVersion;
                }

                return symbols;
            }
        }

        public CurrencyInfo[] Currencies
        {
            get
            {
                this.VerifyInitialized();

                return Native.FeedCache.GetCurrencies(this.handle);
            }
        }

        #endregion

        void VerifyInitialized()
        {
            if (this.handle.IsZero)
                throw new InvalidOperationException(string.Format("Cannot use not initialized {0} object.", this.GetType().Name));
        }

        #region Members

        readonly LPtr handle;

        #endregion
    }
}
