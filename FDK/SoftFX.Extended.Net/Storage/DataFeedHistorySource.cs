namespace SoftFX.Extended.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TickTrader.BusinessObjects;
    using TickTrader.BusinessObjects.QuoteHistory;
    using TickTrader.BusinessObjects.QuoteHistory.Engine;
    using TickTrader.Common.Business;
    using TickTrader.Common.Time;
    using TickTraderHistoryInfo = TickTrader.BusinessObjects.QuoteHistory.HistoryInfo;

    sealed class DataFeedHistorySource : IHistorySource
    {
        public DataFeedHistorySource(DataFeed dataFeed, int attemptsNumber)
            : this(dataFeed, attemptsNumber, 0)
        {
        }

        public DataFeedHistorySource(DataFeed dataFeed, int attemptsNumber, int timeoutInMilliseconds)
        {
            if (dataFeed == null)
                throw new ArgumentNullException("dataFeed", "Data feed can not be null");

            if (attemptsNumber < 1)
                throw new ArgumentOutOfRangeException("attemptsNumber", attemptsNumber, "Attempts number shuld be more than zero");

            this.dataFeed = dataFeed;
            this.attemptsNumber = attemptsNumber;
            this.Timeout = timeoutInMilliseconds;
        }

        #region Properties

        public int Timeout { get; set; }

        #endregion

        #region Retry Helpers

        TResult Attempt<TArg0, TArg1, TResult>(Func<TArg0, TArg1, TResult> function, TArg0 arg0, TArg1 arg1)
        {
            return this.Attempt(() => function(arg0, arg1));
        }

        TResult Attempt<TArg0, TArg1, TArg2, TResult>(Func<TArg0, TArg1, TArg2, TResult> function, TArg0 arg0, TArg1 arg1, TArg2 arg2)
        {
            return this.Attempt(() => function(arg0, arg1, arg2));
        }

        TResult Attempt<TArg0, TArg1, TArg2, TArg3, TResult>(Func<TArg0, TArg1, TArg2, TArg3, TResult> function, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            return this.Attempt(() => function(arg0, arg1, arg2, arg3));
        }

        T Attempt<T>(Func<T> function)
        {
            if (function == null)
                throw new ArgumentNullException("function");

            for (var attempt = 1; attempt < this.attemptsNumber; ++attempt)
            {
                try
                {
                    return function();
                }
                catch
                {
                    if (!this.CheckAttemptState())
                        throw;
                }
            }

            return function();
        }

        bool CheckAttemptState()
        {
            return !this.dataFeed.IsStopped && this.dataFeed.DataFeedHandle.DataClient.WaitForLogon(this.dataFeed.SynchOperationTimeout);
        }

        #endregion

        /// <summary>
        /// Reads data chunk for a specified symbol.
        /// </summary>
        /// <param name="symbol">Can not be null.</param>
        /// <param name="periodicity">Can not be null.</param>
        /// <param name="priceType"></param>
        /// <returns>Can not be null</returns>
        byte[] GetBarMetadataFile(string symbol, string periodicity, FxPriceType priceType)
        {
            var type = StorageConvert.ToPriceType(priceType);

            return this.Attempt(this.DoGetBarMetadataFile, symbol, type, periodicity);
        }

        byte[] DoGetBarMetadataFile(string symbol, PriceType type, string periodicity)
        {
            var file = this.Timeout > 0 ? this.dataFeed.Server.GetBarsHistoryMetaInfoFileEx(symbol, type, periodicity, this.Timeout)
                                        : this.dataFeed.Server.GetBarsHistoryMetaInfoFile(symbol, type, periodicity);

            using (var stream = new DataStream(this.dataFeed, file, this.Timeout))
            {
                return stream.ToArray();
            }
        }

        byte[] GetTickMetadataFile(string symbol, bool includeLevel2)
        {
            return this.Attempt(this.DoGetTickMetadataFile, symbol, includeLevel2);
        }

        byte[] DoGetTickMetadataFile(string symbol, bool includeLevel2)
        {
            var file = this.Timeout > 0 ? this.dataFeed.Server.GetQuotesHistoryMetaInfoFileEx(symbol, includeLevel2, this.Timeout)
                                        : this.dataFeed.Server.GetQuotesHistoryMetaInfoFile(symbol, includeLevel2);

            using (var stream = new DataStream(this.dataFeed, file, this.Timeout))
            {
                return stream.ToArray();
            }
        }

        public MarketHistoryFileReport QueryTickHistoryFile(DateTime to, string symbol, bool includeLevel2)
        {
            return this.Attempt(this.DoQueryTicksHistoryFile, to, symbol, includeLevel2);
        }

        MarketHistoryFileReport DoQueryTicksHistoryFile(DateTime to, string symbol, bool includeLevel2)
        {
            var info = this.dataFeed.Server.GetQuotesHistoryFiles(symbol, includeLevel2, to);

            var result = new MarketHistoryFileReport
            {
                Symbol = symbol,
                AvailableFrom = info.FromAll,
                AvailableTo = info.ToAll,
                From = info.From ?? DateTime.MinValue,
                To = info.To ?? DateTime.MinValue,
                LastTickId = !string.IsNullOrEmpty(info.LastTickId) ? FeedTickId.Parse(info.LastTickId) : default(FeedTickId?)
            };

            var count = info.Files.Length;
            result.Files = new MarketHistoryFile[count];

            for (var index = 0; index < count; ++index)
            {
                var file = info.Files[index];
                using (var stream = new DataStream(this.dataFeed, file, this.Timeout))
                {
                    var part = new MarketHistoryFile
                    {
                        FileBytes = stream.ToArray()
                    };

                    result.Files[index] = part;
                }
            }

            return result;
        }

        public MarketHistoryMetaFileReport QueryTickHistoryMetaFile(string symbol, bool includeLevel2)
        {
            var result = new MarketHistoryMetaFileReport
            {
                FileBytes = GetTickMetadataFile(symbol, includeLevel2)
            };

            return result;
        }

        public MarketBarHistoryFileReport QueryBarHistoryFile(DateTime to, string symbol, string periodicity, FxPriceType priceType)
        {
            var type = StorageConvert.ToPriceType(priceType);
            return this.Attempt(this.DoQueryBarHistoryFile, to, symbol, periodicity, type);
        }

        MarketBarHistoryFileReport DoQueryBarHistoryFile(DateTime to, string symbol, string periodicity, PriceType type)
        {
            DataHistoryInfo info;

            if (this.Timeout > 0)
                info = this.dataFeed.Server.GetBarsHistoryFilesEx(symbol, to, type, periodicity, this.Timeout);
            else
                info = this.dataFeed.Server.GetBarsHistoryFiles(symbol, to, type, periodicity);

            var result = new MarketBarHistoryFileReport
            {
                Symbol = symbol,
                AvailableFrom = info.FromAll,
                AvailableTo = info.ToAll,
                From = info.From ?? DateTime.MinValue,
                To = info.To ?? DateTime.MinValue,
                LastTickId = !string.IsNullOrEmpty(info.LastTickId) ? FeedTickId.Parse(info.LastTickId) : default(FeedTickId?)
            };

            var count = info.Files.Length;
            result.Files = new MarketHistoryFile[count];

            for (var index = 0; index < count; ++index)
            {
                var file = info.Files[index];
                using (var stream = new DataStream(this.dataFeed, file, this.Timeout))
                {
                    var part = new MarketHistoryFile
                    {
                        FileBytes = stream.ToArray()
                    };

                    result.Files[index] = part;
                }
            }

            return result;
        }

        public MarketHistoryMetaFileReport QueryBarHistoryMetaFile(string symbol, string periodicity, FxPriceType priceType)
        {
            var result = new MarketHistoryMetaFileReport
            {
                FileBytes = this.GetBarMetadataFile(symbol, periodicity, priceType)
            };

            return result;
        }

        public Dictionary<Periodicity, TimeInterval> GetSupportedBarPeriodicities(string symbol)
        {
            return DataFeedStorage.GetSupportedPeriodicityToStoreLevel(this.dataFeed.Server.GetQuotesHistoryVersion());
        }

        public MarketHistoryItemsReport<HistoryBar> QueryBarHistory(DateTime to, int maxBars, string symbol, string periodicity, FxPriceType priceType)
        {
            var type = FxPriceType.Bid == priceType ? PriceType.Bid : PriceType.Ask;
            var period = new BarPeriod(periodicity);
            var info = this.dataFeed.Server.GetHistoryBars(symbol, to, -maxBars, type, period);

            var result = new MarketHistoryItemsReport<HistoryBar>
            {
                Items = info.Bars.Select(StorageConvert.ToHistoryBar).ToList(),
                AvailableFrom = info.FromAll,
                AvailableTo = info.ToAll,
                From = info.From.Value,
                To = info.To.Value,
                LastTickId = !string.IsNullOrEmpty(info.LastTickId) ? FeedTickId.Parse(info.LastTickId) : default(FeedTickId?),
                Symbol = symbol
            };

            return result;
        }

        public MarketHistoryItemsReport<TickValue> QueryTickHistory(DateTime to, int maxTicks, string symbol, bool includeLevel2)
        {
            throw new NotImplementedException();
        }

        public int GetHistoryVersion()
        {
            return this.dataFeed.Server.GetQuotesHistoryVersion();
        }

        public TickTraderHistoryInfo GetBarsHistoryInfo(string symbol, Periodicity periodicity, FxPriceType priceType)
        {
            var apiPriceType = StorageConvert.ToPriceType(priceType);
            var apiPeriodicity = StorageConvert.ToBarPeriod(periodicity);

            var info = this.dataFeed.Server.GetBarsHistoryFiles(symbol, ZeroDateTime, apiPriceType, apiPeriodicity.ToString());

            var result = new TickTraderHistoryInfo
            {
                AvailableFrom = info.FromAll,
                AvailableTo = info.ToAll
            };

            return result;
        }

        public TickTraderHistoryInfo GetTicksHistoryInfo(string symbol, bool isLevel2)
        {
            var info = this.dataFeed.Server.GetQuotesHistoryFiles(symbol, isLevel2, ZeroDateTime);

            var result = new TickTraderHistoryInfo
            {
                AvailableFrom = info.FromAll,
                AvailableTo = info.ToAll
            };

            return result;
        }

        public List<string> GetSupportedSymbols()
        {
            var result = this.dataFeed.Server
                                      .GetSymbols()
                                      .Select(o => o.Name)
                                      .ToList();

            return result;
        }

        #region Members

        readonly DataFeed dataFeed;
        readonly int attemptsNumber;

        static readonly DateTime ZeroDateTime = new DateTime(1970, 1, 1);

        #endregion
    }
}
