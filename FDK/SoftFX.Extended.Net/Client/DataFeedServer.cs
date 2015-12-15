namespace SoftFX.Extended
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The class contains methods, which are executed in server side.
    /// </summary>
    public class DataFeedServer : DataServer<DataFeed>
    {
        internal DataFeedServer(DataFeed dataFeed)
            : base(dataFeed)
        {
        }

        /// <summary>
        /// The method returns list of currencies supported by server.
        /// </summary>
        /// <returns></returns>
        public CurrencyInfo[] GetCurrencies()
        {
            return this.GetCurrencies(this.Client.SynchOperationTimeout);
        }

        /// <summary>
        /// The method returns list of currencies supported by server.
        /// </summary>
        /// <param name="timeoutInMilliseconds">timeout of the operation</param>
        /// <returns></returns>
        public CurrencyInfo[] GetCurrencies(int timeoutInMilliseconds)
        {
            return this.Client.DataFeedHandle.GetCurrencies(timeoutInMilliseconds);
        }

        /// <summary>
        /// The method returns list of symbols supported by server.
        /// </summary>
        /// <returns>can not be null</returns>
        public SymbolInfo[] GetSymbols()
        {
            return this.GetSymbolsEx(this.Client.SynchOperationTimeout);
        }

        /// <summary>
        /// The method returns list of symbols supported by server.
        /// </summary>
        /// <param name="timeoutInMilliseconds">timeout of the operation</param>
        /// <returns>can not be null</returns>
        public SymbolInfo[] GetSymbolsEx(int timeoutInMilliseconds)
        {
            return this.Client.DataFeedHandle.GetSymbols(timeoutInMilliseconds);
        }

        /// <summary>
        /// Returns version of server quotes history.
        /// </summary>
        /// <returns>quote history version</returns>
        public int GetQuotesHistoryVersion()
        {
            return this.Client.DataFeedHandle.GetQuotesHistoryVersion(this.Client.SynchOperationTimeout);
        }

        /// <summary>
        /// The method subscribes to quotes.
        /// </summary>
        /// <param name="symbols">list of requested symbols; can not be null</param>
        /// <param name="depth">
        /// 0 - full book
        /// (1..5) - restricted book
        /// </param>
        public void SubscribeToQuotes(IEnumerable<string> symbols, int depth)
        {
            this.SubscribeToQuotesEx(symbols, depth, this.Client.SynchOperationTimeout);
        }

        /// <summary>
        /// The method subscribes to quotes.
        /// </summary>
        /// <param name="symbols">list of requested symbols; can not be null</param>
        /// <param name="depth">
        /// 0 - full book
        /// (1..5) - restricted book
        /// </param>
        /// <param name="timeoutInMilliseconds">Timeout of the operation</param>
        public void SubscribeToQuotesEx(IEnumerable<string> symbols, int depth, int timeoutInMilliseconds)
        {
            this.Client.DataFeedHandle.SubscribeToQuotes(symbols, depth, timeoutInMilliseconds);
        }

        /// <summary>
        /// The method unsubscribes quotes.
        /// </summary>
        /// <param name="symbols">list of symbols, which server should not send to the client; can not be null</param>
        public void UnsubscribeQuotes(IEnumerable<string> symbols)
        {
            this.UnsubscribeQuotesEx(symbols, this.Client.SynchOperationTimeout);
        }

        /// <summary>
        /// The method unsubscribes quotes.
        /// </summary>
        /// <param name="symbols">list of symbols, which server should not send to the client; can not be null</param>
        /// <param name="timeoutInMilliseconds">timeout of the operation</param>
        public void UnsubscribeQuotesEx(IEnumerable<string> symbols, int timeoutInMilliseconds)
        {
            this.Client.DataFeedHandle.UnsubscribeQuotes(symbols, timeoutInMilliseconds);
        }

        /// <summary>
        /// The method gets history bars from the server.
        /// </summary>
        /// <param name="symbol">A required symbol; can not be null.</param>
        /// <param name="priceType">A required price type: Bid or Ask.</param>
        /// <param name="startTime">A start time of bars enumeration.</param>
        /// <param name="endTime">A end time of bars enumeration.</param>
        /// <param name="period">Bar period instance; can not be null.</param>
        /// <returns></returns>
        public Bars GetBarsHistory(string symbol, PriceType priceType, BarPeriod period, DateTime startTime, DateTime endTime)
        {
            return new Bars(this.Client, symbol, priceType, period, startTime, endTime);
        }

        /// <summary>
        /// The method gets history bars from the server.
        /// </summary>
        /// <param name="symbol">A required symbol; can not be null.</param>
        /// <param name="time">Date and time which specifies the historical point.</param>
        /// <param name="barsNumber">The maximum number of bars in the requested chart. The value can be negative or positive.
        /// Positive value means historical chart from the specified historical point to future.</param>
        /// <param name="priceType">Can be bid or ask.</param>
        /// <param name="period">Chart periodicity.</param>
        /// <returns>Can not be null.</returns>
        public DataHistoryInfo GetHistoryBars(string symbol, DateTime time, int barsNumber, PriceType priceType, BarPeriod period)
        {
            return this.GetHistoryBarsEx(symbol, time, barsNumber, priceType, period, this.Client.SynchOperationTimeout);
        }

        /// <summary>
        /// The method gets history bars from the server.
        /// </summary>
        /// <param name="symbol">A required symbol; can not be null.</param>
        /// <param name="time">Date and time which specifies the historical point.</param>
        /// <param name="barsNumber">The maximum number of bars in the requested chart. The value can be negative or positive.
        /// Positive value means historical chart from the specified historical point to future.</param>
        /// <param name="priceType">Can be bid or ask.</param>
        /// <param name="period">Chart periodicity.</param>
        /// <param name="timeoutInMilliseconds">timeout of the operation</param>
        /// <returns>Can not be null.</returns>
        public DataHistoryInfo GetHistoryBarsEx(string symbol, DateTime time, int barsNumber, PriceType priceType, BarPeriod period, int timeoutInMilliseconds)
        {
            return this.Client.DataFeedHandle.GetHistoryBars(symbol, time, barsNumber, priceType, period, timeoutInMilliseconds);
        }

        /// <summary>
        /// The method gets history bars from the server.
        /// </summary>
        /// <param name="symbol">A required symbol; can not be null.</param>
        /// <param name="time">Date and time which specifies the historical point.</param>
        /// <param name="priceType"></param>
        /// <param name="period"></param>
        /// <returns>Can not be null.</returns>
        public DataHistoryInfo GetBarsHistoryFiles(string symbol, DateTime time, PriceType priceType, string period)
        {
            return this.GetBarsHistoryFilesEx(symbol, time, priceType, period, this.Client.SynchOperationTimeout);
        }

        /// <summary>
        /// The method gets history bars from the server.
        /// </summary>
        /// <param name="symbol">A required symbol; can not be null.</param>
        /// <param name="time">Date and time which specifies the historical point.</param>
        /// <param name="timeoutInMilliseconds">Timeout of the operation in milliseconds.</param>
        /// <param name="priceType"></param>
        /// <param name="period"></param>
        /// <returns>Can not be null.</returns>
        public DataHistoryInfo GetBarsHistoryFilesEx(string symbol, DateTime time, PriceType priceType, string period, int timeoutInMilliseconds)
        {
            return this.Client.DataFeedHandle.GetBarsHistoryFiles(symbol, time, priceType, period, timeoutInMilliseconds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="includeLevel2"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public DataHistoryInfo GetQuotesHistoryFiles(string symbol, bool includeLevel2, DateTime time)
        {
            return this.GetQuotesHistoryFilesEx(symbol, includeLevel2, time, this.Client.SynchOperationTimeout);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="includeLevel2"></param>
        /// <param name="time"></param>
        /// <param name="timeoutInMilliseconds"></param>
        /// <returns></returns>
        public DataHistoryInfo GetQuotesHistoryFilesEx(string symbol, bool includeLevel2, DateTime time, int timeoutInMilliseconds)
        {
            return this.Client.DataFeedHandle.GetQuoteHistoryFiles(symbol, includeLevel2, time, timeoutInMilliseconds);
        }

        /// <summary>
        /// Gets meta information file ID for a specified input arguments.
        /// </summary>
        /// <param name="symbol">Can not be null.</param>
        /// <param name="priceType"></param>
        /// <param name="period">Can not be null</param>
        /// <returns></returns>
        public string GetBarsHistoryMetaInfoFile(string symbol, PriceType priceType, string period)
        {
            return this.GetBarsHistoryMetaInfoFileEx(symbol, priceType, period, this.Client.SynchOperationTimeout);
        }

        /// <summary>
        /// Gets meta information file ID for a specified input arguments.
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="priceType"></param>
        /// <param name="period"></param>
        /// <param name="timeoutInMilliseconds">Timeout of the operation in milliseconds.</param>
        /// <returns></returns>
        public string GetBarsHistoryMetaInfoFileEx(string symbol, PriceType priceType, string period, int timeoutInMilliseconds)
        {
            return this.Client.DataFeedHandle.GetBarsHistoryMetaInfoFile(symbol, priceType, period, timeoutInMilliseconds);
        }

        /// <summary>
        /// Gets meta information file ID for a specified input arguments.
        /// </summary>
        /// <param name="symbol">Can not be null.</param>
        /// <param name="includeLevel2">False: ticks contains only the best bid/ask prices; true: ticks contains full level2.</param>
        /// <returns></returns>
        public string GetQuotesHistoryMetaInfoFile(string symbol, bool includeLevel2)
        {
            return this.GetQuotesHistoryMetaInfoFileEx(symbol, includeLevel2, this.Client.SynchOperationTimeout);
        }

        /// <summary>
        /// Gets meta information file ID for a specified input arguments.
        /// </summary>
        /// <param name="symbol">Can not be null.</param>
        /// <param name="includeLevel2">False: ticks contains only the best bid/ask prices; true: ticks contains full level2.</param>
        /// <param name="timeoutInMilliseconds">Timeout of the operation in milliseconds.</param>
        /// <returns></returns>
        public string GetQuotesHistoryMetaInfoFileEx(string symbol, bool includeLevel2, int timeoutInMilliseconds)
        {
            return this.Client.DataFeedHandle.GetQuotesHistoryMetaInfoFile(symbol, includeLevel2, timeoutInMilliseconds);
        }
    }
}
