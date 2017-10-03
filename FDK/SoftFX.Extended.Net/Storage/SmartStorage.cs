namespace SoftFX.Extended.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SoftFX.Extended.Errors;
    using TickTrader.BusinessObjects;
    using TickTrader.BusinessObjects.QuoteHistory.Engine;
    using TickTrader.Common.Business;
    using TickTrader.Server.QuoteHistory.Engine.HistoryManagers;

    using StorageHistoryNotFoundException = TickTrader.BusinessObjects.QuoteHistory.Exceptions.HistoryNotFoundException;

    sealed class SmartStorage : IStorage
    {
        public SmartStorage(DataFeedStorage storage, IHistorySource source = null)
        {
            if (storage == null)
                throw new ArgumentNullException(nameof(storage));

            this.storage = storage;
            this.source = source;
        }

        #region GetQuotes method

        public Quote[] GetQuotes(string symbol, DateTime startTime, DateTime endTime, int depth)
        {
            try
            {
                if (depth == 0)
                    depth = int.MaxValue;

                var manager = this.storage.GetOrCreateHistoryManager(symbol);
                var includeLevel2 = depth != 1;

                if (this.source != null)
                {
                    // online mode
                    if (!manager.TicksAreSynchronized(this.source, symbol, includeLevel2, startTime, endTime, false))
                        manager.SynchronizeTicks(this.source, symbol, includeLevel2, startTime, endTime, false, NullCallback);
                }

                var ticks = new List<TickValue>();
                if (startTime < endTime)
                    ForwardFillTicks(manager, symbol, includeLevel2, startTime, endTime, ticks);
                else
                    BackwardFillTicks(manager, symbol, includeLevel2, startTime, endTime, ticks);

                var converter = new StorageConvert();
                var result = ticks.Select(o => converter.ToQuote(symbol, o, depth)).ToArray();

                return result;
            }
            catch (StorageHistoryNotFoundException ex)
            {
                throw new HistoryNotFoundException("GetQuotes", ex);
            }
        }

        static void ForwardFillTicks(IHistoryManager cache, string symbol, bool includeLevel2, DateTime startTime, DateTime endTime, ICollection<TickValue> ticks)
        {
            try
            {
                for (var current = startTime; current <= endTime; )
                {
                    var report = cache.QueryTickHistory(current, -RequestedTicksNumber, symbol, includeLevel2);
                    var items = report.Items;
                    var count = items.Count - 1;

                    if (items.Count == RequestedTicksNumber)
                    {
                        for (; count > 0; --count)
                        {
                            var first = items[count - 1];
                            var second = items[count];
                            if (first.Time != second.Time)
                            {
                                current = first.Time;
                                break;
                            }
                        }

                        if (count == 0)
                        {
                            var time = items[0].Time;
                            var message = string.Format("Internal error: cache contains more than {0} ticks for the same time = {1}; symbol = {2}.", RequestedTicksNumber, time, symbol);
                            throw new Exception(message);
                        }
                    }
                    else
                    {
                        current = DateTime.MaxValue;
                        ++count;
                    }

                    for (var index = 0; index < count; ++index)
                    {
                        var value = items[index];
                        if (value.Time > endTime)
                        {
                            return;
                        }
                        ticks.Add(value);
                    }

                    if (current < DateTime.MaxValue)
                    {
                        current = current.AddMilliseconds(1);
                    }
                }
            }
            catch (StorageHistoryNotFoundException)
            {
            }
        }

        static void BackwardFillTicks(IHistoryManager cache, string symbol, bool includeLevel2, DateTime startTime, DateTime endTime, List<TickValue> ticks)
        {
            ForwardFillTicks(cache, symbol, includeLevel2, endTime, startTime, ticks);
            ticks.Reverse();
        }

        public Quote[] GetQuotes(string symbol, DateTime startTime, int quotesNumber, int depth)
        {
            try
            {
                if (depth == 0)
                    depth = int.MaxValue;

                var includeLevel2 = depth != 1;
                var manager = this.storage.GetOrCreateHistoryManager(symbol);
                var endTime = startTime.AddHours((quotesNumber > 0) ? 1 : -1);

                if (symbol.EndsWith("_L"))
                    endTime = startTime.AddMonths((quotesNumber > 0) ? 1 : -1);

                if (this.source != null)
                {
                    // online mode
                    if (!manager.TicksAreSynchronized(this.source, symbol, includeLevel2, startTime, endTime, false))
                        manager.SynchronizeTicks(this.source, symbol, includeLevel2, startTime, endTime, false, NullCallback);
                }

                if (this.source != null)
                    return this.GetOnlineQuotes(symbol, startTime, quotesNumber, depth);
                else
                    return this.GetOfflineQuotes(symbol, startTime, quotesNumber, depth);
            }
            catch (StorageHistoryNotFoundException ex)
            {
                throw new HistoryNotFoundException("GetQuotes", ex);
            }
        }

        Quote[] GetOnlineQuotes(string symbol, DateTime startTime, int quotesNumber, int depth)
        {
            var includeLevel2 = depth != 1;

            var manager = this.storage.GetOrCreateHistoryManager(symbol);

            var ticks = new List<TickValue>();
            if (quotesNumber > 0)
                ForwardFillTicks(manager, symbol, includeLevel2, startTime, quotesNumber, ticks);
            else if (quotesNumber < 0)
                BackwardFillTicks(manager, symbol, includeLevel2, startTime, -quotesNumber, ticks);

            var converter = new StorageConvert();
            var result = ticks.Select(o => converter.ToQuote(symbol, o, depth)).ToArray();

            return result;
        }

        Quote[] GetOfflineQuotes(string symbol, DateTime startTime, int quotesNumber, int depth)
        {
            var includeLevel2 = depth != 1;

            var manager = this.storage.GetOrCreateHistoryManager(symbol);
            var report = manager.QueryTickHistory(startTime, -quotesNumber, symbol, includeLevel2);

            var items = report.Items;

            var converter = new StorageConvert();
            if (quotesNumber > 0)
                return items.Select(o => converter.ToQuote(symbol, o, depth)).ToArray();
            else if (quotesNumber < 0)
                return items.Select(o => converter.ToQuote(symbol, o, depth)).Reverse().ToArray();

            return Enumerable.Empty<Quote>().ToArray();
        }

        static void ForwardFillTicks(IHistoryManager cache, string symbol, bool includeLevel2, DateTime startTime, int quotesNumber, ICollection<TickValue> ticks)
        {
            try
            {
                var report = cache.QueryTickHistory(startTime, -quotesNumber, symbol, includeLevel2);
                var items = report.Items;
                var count = items.Count;

                for (var index = 0; index < count; ++index)
                {
                    var value = items[index];
                    ticks.Add(value);
                }
            }
            catch (StorageHistoryNotFoundException)
            {
            }
        }

        static void BackwardFillTicks(IHistoryManager cache, string symbol, bool includeLevel2, DateTime startTime, int quotesNumber, List<TickValue> ticks)
        {
            ForwardFillTicks(cache, symbol, includeLevel2, startTime, -quotesNumber, ticks);
            ticks.Reverse();
        }

        #endregion

        #region GetBars Method

        public Bar[] GetBars(string symbol, PriceType priceType, BarPeriod period, DateTime startTime, DateTime endTime)
        {
            try
            {
                var forexPeriodicity = StorageConvert.ToPeriodicity(period);
                var forexPriceType = StorageConvert.ToFxPriceType(priceType);
                var manager = this.storage.GetOrCreateHistoryManager(symbol);

                if (this.source != null)
                {
                    // online mode
                    if (!manager.BarsAreSynchronized(this.source, symbol, forexPeriodicity, forexPriceType, startTime, endTime, false))
                        manager.SynchronizeBars(this.source, symbol, forexPeriodicity, forexPriceType, startTime, endTime, false, NullCallback);
                }

                var bars = new List<HistoryBar>();
                if (startTime < endTime)
                    ForwardFillBars(manager, symbol, forexPeriodicity, forexPriceType, startTime, endTime, bars);
                else
                    BackwardFillBars(manager, symbol, forexPeriodicity, forexPriceType, startTime, endTime, bars);

                var result = bars.Select(o => StorageConvert.ToBar(o, period)).ToArray();
                return result;
            }
            catch (StorageHistoryNotFoundException ex)
            {
                throw new HistoryNotFoundException("GetBars", ex);
            }
        }

        static void ForwardFillBars(IHistoryManager cache, string symbol, Periodicity periodicity, FxPriceType priceType, DateTime startTime, DateTime endTime, ICollection<HistoryBar> bars)
        {
            try
            {
                for (var current = startTime; current < endTime;)
                {
                    var report = cache.QueryBarHistory(current, -RequestedBarsNumber, symbol, periodicity.ToString(), priceType);
                    var items = report.Items;

                    foreach (var element in report.Items)
                    {
                        if (element.Time >= endTime)
                        {
                            return;
                        }
                        bars.Add(element);
                    }
                    if (items.Count == 0)
                    {
                        return;
                    }
                    current = items.Last().Time;
                    current = current + periodicity;
                }
            }
            catch (StorageHistoryNotFoundException)
            {
            }
        }

        static void BackwardFillBars(IHistoryManager cache, string symbol, Periodicity periodicity, FxPriceType priceType, DateTime startTime, DateTime endTime, List<HistoryBar> bars)
        {
            ForwardFillBars(cache, symbol, periodicity, priceType, endTime, startTime, bars);
            bars.Reverse();
        }

        public Bar[] GetBars(string symbol, PriceType priceType, BarPeriod period, DateTime startTime, int barsNumber)
        {
            try
            {
                if (this.source != null)
                    return this.GetOnlineBars(symbol, priceType, period, startTime, barsNumber);
                else
                    return this.GetOfflineBars(symbol, priceType, period, startTime, barsNumber);
            }
            catch (StorageHistoryNotFoundException ex)
            {
                throw new HistoryNotFoundException("GetBars", ex);
            }
        }

        Bar[] GetOnlineBars(string symbol, PriceType priceType, BarPeriod period, DateTime startTime, int barsNumber)
        {
            var bars = new List<Bar>();
            if (barsNumber > 0)
                this.ForwardFillBars(symbol, period, priceType, startTime, barsNumber, bars);
            else if (barsNumber < 0)
                this.BackwardFillBars(symbol, period, priceType, startTime, -barsNumber, bars);

            return bars.ToArray();
        }

        Bar[] GetOfflineBars(string symbol, PriceType priceType, BarPeriod period, DateTime startTime, int barsNumber)
        {
            var manager = this.storage.GetOrCreateHistoryManager(symbol);
            var fxPriceType = StorageConvert.ToFxPriceType(priceType);

            var report = manager.QueryBarHistory(startTime, -barsNumber, symbol, period.ToString(), fxPriceType);

            var items = report.Items;

            if (barsNumber > 0)
                return items.Select(o => StorageConvert.ToBar(o, period)).ToArray();
            else if (barsNumber < 0)
                return items.Select(o => StorageConvert.ToBar(o, period)).Reverse().ToArray();

            return Enumerable.Empty<Bar>().ToArray();
        }

        void ForwardFillBars(string symbol, BarPeriod period, PriceType priceType, DateTime startTime, int barsNumber, ICollection<Bar> bars)
        {
            var attempts = 7 * 24;
            for (; bars.Count < barsNumber; )
            {
                var endTime = CaculateNextDateTime(startTime, period, barsNumber);
                var items = this.GetBars(symbol, priceType, period, startTime, endTime);
                if (items.Length == 0)
                {
                    if (attempts == 0)
                    {
                        return;
                    }
                    --attempts;
                }
                foreach (var element in items)
                {
                    if (bars.Count == barsNumber)
                    {
                        break;
                    }
                    bars.Add(element);
                }
                startTime = endTime;
            }
        }

        void BackwardFillBars(string symbol, BarPeriod periodicity, PriceType priceType, DateTime startTime, int barsNumber, ICollection<Bar> bars)
        {
            var attempts = 7 * 24;
            for (; bars.Count < barsNumber; )
            {
                var endTime = CaculateNextDateTime(startTime, periodicity, -barsNumber);
                var items = this.GetBars(symbol, priceType, periodicity, startTime, endTime);
                if (items.Length == 0)
                {
                    if (attempts == 0)
                    {
                        return;
                    }
                    --attempts;
                }
                foreach (var element in items)
                {
                    if (bars.Count == barsNumber)
                    {
                        break;
                    }
                    bars.Add(element);
                }
                startTime = endTime;
            }
        }

        static DateTime CaculateNextDateTime(DateTime time, BarPeriod periodicity, int count)
        {
            var result = time;

            for (var step = 0; step < count; ++step)
            {
                result = result + periodicity;
            }

            for (var step = 0; step > count; --step)
            {
                if (result < TimeThreshold)
                {
                    break;
                }
                result = result - periodicity;
            }

            if (count > 0)
            {
                var next = time.AddHours(1);
                if (next > result)
                {
                    result = next;
                }
            }
            else
            {
                var next = time.AddHours(-1);
                if (next < result)
                {
                    result = next;
                }
            }

            return result;
        }

        #endregion

        #region GetPairBars Method

        public PairBar[] GetPairBars(string symbol, BarPeriod period, DateTime startTime, DateTime endTime)
        {
            try
            {
                var bids = this.GetBars(symbol, PriceType.Bid, period, startTime, endTime);
                var asks = this.GetBars(symbol, PriceType.Ask, period, startTime, endTime);
                var bars = new PairBars(bids, asks, DateTime.Compare(endTime, startTime) >= 0);
                var result = bars.ToArray();
                return result;
            }
            catch (StorageHistoryNotFoundException ex)
            {
                throw new HistoryNotFoundException("GetPairBars", ex);
            }
        }

        public PairBar[] GetPairBars(string symbol, BarPeriod period, DateTime startTime, int barsNumber)
        {
            try
            {
                var bids = this.GetBars(symbol, PriceType.Bid, period, startTime, barsNumber);
                var asks = this.GetBars(symbol, PriceType.Ask, period, startTime, barsNumber);
                var bars = new PairBars(bids, asks, barsNumber >= 0);
                var result = bars.ToArray();
                return result;
            }
            catch (StorageHistoryNotFoundException ex)
            {
                throw new HistoryNotFoundException("GetPairBars", ex);
            }
        }

        #endregion

        #region GetInfo

        public HistoryInfo GetQuotesInfo(string symbol, int depth)
        {
            try
            {
                var source = this.GetRemoteOrLocalSource(symbol);
                var isLevel2 = depth != 1;
                var info = source.GetTicksHistoryInfo(symbol, isLevel2);
                var result = new HistoryInfo(info.AvailableFrom.Value, info.AvailableTo.Value);
                return result;
            }
            catch (StorageHistoryNotFoundException ex)
            {
                throw new HistoryNotFoundException("GetQuotesInfo", ex);
            }
        }

        public HistoryInfo GetBarsInfo(string symbol, PriceType priceType, BarPeriod period)
        {
            try
            {
                var source = this.GetRemoteOrLocalSource(symbol);
                var periodicity = StorageConvert.ToPeriodicity(period);
                var priceTypeEx = StorageConvert.ToFxPriceType(priceType);
                var info = source.GetBarsHistoryInfo(symbol, periodicity, priceTypeEx);

                var result = new HistoryInfo(info.AvailableFrom.Value, info.AvailableTo.Value);
                return result;
            }
            catch (StorageHistoryNotFoundException ex)
            {
                throw new HistoryNotFoundException("GetBarsInfo", ex);
            }
        }

        #endregion

        #region Helper Methods

        static void NullCallback(int completed, int total, DateTime completedStart, TimeSpan completedLength)
        {
        }

        IHistorySource GetRemoteOrLocalSource(string symbol)
        {
            return this.source ?? this.storage.GetOrCreateHistoryManager(symbol);
        }

        #endregion

        #region Members

        readonly DataFeedStorage storage;
        readonly IHistorySource source;

        #endregion

        #region Constants

        const int RequestedTicksNumber = 1024;
        const int RequestedBarsNumber = 1024;
        static readonly TimeSpan MaximumInterval = new TimeSpan(365);
        static readonly DateTime TimeThreshold = new DateTime(100, 1, 1);

        #endregion
    }
}
