namespace SoftFX.Extended
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using SoftFX.Extended.Storage;

    /// <summary>
    /// Bars enumeration.
    /// </summary>
    public class PairBars : IEnumerable<PairBar>
    {
        #region Construction

        internal PairBars(IEnumerable<Bar> bids, IEnumerable<Bar> asks, bool positive)
        {
            if (bids == null)
                throw new ArgumentNullException(nameof(bids), "bids bar enumeration can not be null");

            if (asks == null)
                throw new ArgumentNullException(nameof(asks), "asks bar enumeration can not be null");

            this.bids = bids;
            this.asks = asks;
            this.positive = positive;
            this.count = int.MaxValue;
        }

        /// <summary>
        /// Creates a new PairBars stream instance.
        /// If startTime is less or equal than endTime then this is forward bars enumeration (from past to future), otherwise this is backward enumeration (from future to past).
        /// Anyway all bars should be in the following time range: Min(startTime, endTime) &lt;= Bar.From and Bar.To &lt;= Max(startTime, endTime)
        /// </summary>
        /// <param name="datafeed">Datafeed instance; can not be null.</param>
        /// <param name="symbol">A required symbol; can not be null.</param>
        /// <param name="period">Bar period instance; can not be null.</param>
        /// <param name="startTime">A start time of bars enumeration.</param>
        /// <param name="endTime">A end time of bars enumeration.</param>
        /// <exception cref="System.ArgumentNullException">If datafeed, period or symbol is null.</exception>
        public PairBars(DataFeed datafeed, string symbol, BarPeriod period, DateTime startTime, DateTime endTime)
        {
            this.bids = new Bars(datafeed, symbol, PriceType.Bid, period, startTime, endTime);
            this.asks = new Bars(datafeed, symbol, PriceType.Ask, period, startTime, endTime);
            this.positive = DateTime.Compare(startTime, endTime) >= 0;
            this.count = int.MaxValue;
        }

        /// <summary>
        /// Creates a new PairBars stream instance.
        /// If startTime is less or equal than endTime then this is forward bars enumeration (from past to future), otherwise this is backward enumeration (from future to past).
        /// Anyway all bars should be in the following time range: Min(startTime, endTime) &lt;= Bar.From and Bar.To &lt;= Max(startTime, endTime)
        /// </summary>
        /// <param name="storage">Online/Offline provider of data feed storage instance; can not be null.</param>
        /// <param name="symbol">A required symbol; can not be null.</param>
        /// <param name="period">Bar period instance; can not be null.</param>
        /// <param name="startTime">A start time of bars enumeration.</param>
        /// <param name="endTime">A end time of bars enumeration.</param>
        /// <exception cref="System.ArgumentNullException">If datafeed, period or symbol is null.</exception>
        public PairBars(IStorage storage, string symbol, BarPeriod period, DateTime startTime, DateTime endTime)
        {
            this.bids = new Bars(storage, symbol, PriceType.Bid, period, startTime, endTime);
            this.asks = new Bars(storage, symbol, PriceType.Ask, period, startTime, endTime);
            this.positive = DateTime.Compare(startTime, endTime) >= 0;
            this.count = int.MaxValue;
        }

        /// <summary>
        /// Creates a new Bars stream instance.
        /// If startTime is less or equal than endTime then this is forward bars enumeration (from past to future), otherwise this is backward enumeration (from future to past).
        /// Anyway all bars should be in the following time range: Min(startTime, endTime) &lt;= Bar.From and Bar.To &lt;= Max(startTime, endTime)
        /// </summary>
        /// <param name="datafeed">Datafeed instance; can not be null.</param>
        /// <param name="symbol">A required symbol; can not be null.</param>
        /// <param name="period">Bar period instance; can not be null.</param>
        /// <param name="startTime">A start time of bars enumeration.</param>
        /// <param name="endTime">A end time of bars enumeration.</param>
        /// <param name="preferredBufferSize">Bars enumeration requests bars from server by chunks. This is preferred chunk size. It should be positive.</param>
        /// <exception cref="System.ArgumentNullException">If datafeed, period or symbol is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">If preferredBufferSize is less than 2.</exception>
        public PairBars(DataFeed datafeed, string symbol, BarPeriod period, DateTime startTime, DateTime endTime, int preferredBufferSize)
        {
            this.bids = new Bars(datafeed, symbol, PriceType.Bid, period, startTime, endTime, preferredBufferSize);
            this.asks = new Bars(datafeed, symbol, PriceType.Ask, period, startTime, endTime, preferredBufferSize);
            this.positive = DateTime.Compare(startTime, endTime) >= 0;
            this.count = int.MaxValue;
        }

        /// <summary>
        /// Creates a new Bars stream instance.
        /// If barsNumber is less than zero then this is forward bars enumeration (from past to future), otherwise this is backward enumeration (from future to past).
        /// Anyway all bars should be in the following time range:
        ///		Bar.From &gt;= startTime for forward enumeration
        ///		Bar.To &lt;= startTime for backward enumeration
        /// </summary>
        /// <param name="datafeed">DataFeed instance; can not be null.</param>
        /// <param name="symbol">A required symbol; can not be null.</param>
        /// <param name="period">Bar period instance; can not be null.</param>
        /// <param name="startTime">A start time of bars enumeration.</param>
        /// <param name="barsNumber">Requested bars number; positive value means forward enumeration; negative value means backward enumeration.</param>
        /// <exception cref="System.ArgumentNullException">If datafeed, period or symbol is null.</exception>
        public PairBars(DataFeed datafeed, string symbol, BarPeriod period, DateTime startTime, int barsNumber)
        {
            this.bids = new Bars(datafeed, symbol, PriceType.Bid, period, startTime, barsNumber);
            this.asks = new Bars(datafeed, symbol, PriceType.Ask, period, startTime, barsNumber);
            this.positive = (barsNumber >= 0);
            this.count = Math.Abs(barsNumber);
        }

        /// <summary>
        /// Creates a new Bars stream instance.
        /// If barsNumber is less than zero then this is forward bars enumeration (from past to future), otherwise this is backward enumeration (from future to past).
        /// Anyway all bars should be in the following time range:
        ///		Bar.From &gt;= startTime for forward enumeration
        ///		Bar.To &lt;= startTime for backward enumeration
        /// </summary>
        /// <param name="storage">Online/Offline provider of data feed storage instance; can not be null.</param>
        /// <param name="symbol">A required symbol; can not be null.</param>
        /// <param name="period">Bar period instance; can not be null.</param>
        /// <param name="startTime">A start time of bars enumeration.</param>
        /// <param name="barsNumber">Requested bars number; positive value means forward enumeration; negative value means backward enumeration.</param>
        /// <exception cref="System.ArgumentNullException">If datafeed, period or symbol is null.</exception>
        public PairBars(IStorage storage, string symbol, BarPeriod period, DateTime startTime, int barsNumber)
        {
            this.bids = new Bars(storage, symbol, PriceType.Bid, period, startTime, barsNumber);
            this.asks = new Bars(storage, symbol, PriceType.Ask, period, startTime, barsNumber);
            this.positive = (barsNumber >= 0);
            this.count = Math.Abs(barsNumber);
        }

        /// <summary>
        /// Creates a new Bars stream instance.
        /// If barsNumber is less than zero then this is forward bars enumeration (from past to future), otherwise this is backward enumeration (from future to past).
        /// Anyway all bars should be in the following time range:
        ///		Bar.From &gt;= startTime for forward enumeration
        ///		Bar.To &lt;= startTime for backward enumeration
        /// </summary>
        /// <param name="datafeed">Datafeed instance; can not be null.</param>
        /// <param name="symbol">A required symbol; can not be null.</param>
        /// <param name="period">Bar period instance; can not be null.</param>
        /// <param name="startTime">A start time of bars enumeration.</param>
        /// <param name="barsNumber">Requested bars number; positive value means forward enumeration; negative value means backward enumeration.</param>
        /// <param name="preferredBufferSize">Bars enumeration requests bars from server by chunks. This is preferred chunk size. It should be positive.</param>
        /// <exception cref="System.ArgumentNullException">If datafeed, period or symbol is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">If preferredBufferSize is less than 2.</exception>
        public PairBars(DataFeed datafeed, string symbol, BarPeriod period, DateTime startTime, int barsNumber, int preferredBufferSize)
        {
            this.bids = new Bars(datafeed, symbol, PriceType.Bid, period, startTime, barsNumber, preferredBufferSize);
            this.asks = new Bars(datafeed, symbol, PriceType.Ask, period, startTime, barsNumber, preferredBufferSize);
            this.positive = (barsNumber >= 0);
            this.count = Math.Abs(barsNumber);
        }

        #endregion

        /// <summary>
        /// The method returns bars enumerator.
        /// </summary>
        /// <returns>Can not be null.</returns>
        public IEnumerator<PairBar> GetEnumerator()
        {
            var result = new PairBarsEnumerator(this.bids, this.asks, this.positive, this.count);
            return result;
        }

        /// <summary>
        /// The method returns bars enumerator.
        /// </summary>
        /// <returns>Can not be null.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            var result = new PairBarsEnumerator(this.bids, this.asks, this.positive, this.count);
            return result;
        }

        #region Members

        readonly IEnumerable<Bar> bids;
        readonly IEnumerable<Bar> asks;
        readonly bool positive;
        readonly int count;

        #endregion
    }
}
