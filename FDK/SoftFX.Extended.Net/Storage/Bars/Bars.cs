namespace SoftFX.Extended
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using SoftFX.Extended.Storage;

    /// <summary>
    /// Bars enumeration.
    /// </summary>
    public class Bars : IEnumerable<Bar>
    {
        #region Construction

        /// <summary>
        /// Creates a new Bars stream instance.
        /// If startTime is less or equal than endTime then this is forward bars enumeration (from past to future), otherwise this is backward enumeration (from future to past).
        /// Anyway all bars should be in the following time range: Min(startTime, endTime) &lt;= Bar.From and Bar.To &lt;= Max(startTime, endTime)
        /// </summary>
        /// <param name="datafeed">DataFeed instance; can not be null.</param>
        /// <param name="symbol">A required symbol; can not be null.</param>
        /// <param name="priceType">A required price type: Bid or Ask.</param>
        /// <param name="period">Bar period instance; can not be null.</param>
        /// <param name="startTime">A start time of bars enumeration.</param>
        /// <param name="endTime">A end time of bars enumeration.</param>
        /// <exception cref="System.ArgumentNullException">If datafeed, period or symbol is null.</exception>
        public Bars(DataFeed datafeed, string symbol, PriceType priceType, BarPeriod period, DateTime startTime, DateTime endTime)
            : this(datafeed, symbol, priceType, period, startTime, endTime, DefaultBufferSize)
        {
        }

        /// <summary>
        /// Creates a new Bars stream instance.
        /// If startTime is less or equal than endTime then this is forward bars enumeration (from past to future), otherwise this is backward enumeration (from future to past).
        /// Anyway all bars should be in the following time range: Min(startTime, endTime) &lt;= Bar.From and Bar.To &lt;= Max(startTime, endTime)
        /// </summary>
        /// <param name="storage">Online/Offline provider of data feed storage instance; can not be null.</param>
        /// <param name="symbol">A required symbol; can not be null.</param>
        /// <param name="priceType">A required price type: Bid or Ask.</param>
        /// <param name="period">Bar period instance; can not be null.</param>
        /// <param name="startTime">A start time of bars enumeration.</param>
        /// <param name="endTime">A end time of bars enumeration.</param>
        /// <exception cref="System.ArgumentNullException">If datafeed, period or symbol is null.</exception>
        public Bars(IStorage storage, string symbol, PriceType priceType, BarPeriod period, DateTime startTime, DateTime endTime)
        {
            ValidateArguments(storage, symbol, period);
            this.enumerator = new BarsTimeIntervalEnumerator(storage, symbol, priceType, period, startTime, endTime);
        }

        /// <summary>
        /// Creates a new Bars stream instance.
        /// If startTime is less or equal than endTime then this is forward bars enumeration (from past to future), otherwise this is backward enumeration (from future to past).
        /// Anyway all bars should be in the following time range: Min(startTime, endTime) &lt;= Bar.From and Bar.To &lt;= Max(startTime, endTime)
        /// </summary>
        /// <param name="datafeed">DataFeed instance; can not be null.</param>
        /// <param name="symbol">A required symbol; can not be null.</param>
        /// <param name="priceType">A required price type: Bid or Ask.</param>
        /// <param name="period">Bar period instance; can not be null.</param>
        /// <param name="startTime">A start time of bars enumeration.</param>
        /// <param name="endTime">A end time of bars enumeration.</param>
        /// <param name="preferredBufferSize">Bars enumeration requests bars from server by chunks. This is preferred chunk size. It should be positive.</param>
        /// <exception cref="System.ArgumentNullException">If datafeed, period or symbol is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">If preferredBufferSize is less than 2.</exception>
        public Bars(DataFeed datafeed, string symbol, PriceType priceType, BarPeriod period, DateTime startTime, DateTime endTime, int preferredBufferSize)
        {
            ValidateArguments(datafeed, symbol, period, preferredBufferSize);
            this.enumerator = new BarsTimeIntervalEnumerator(datafeed, symbol, priceType, period, startTime, endTime, preferredBufferSize);
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
        /// <param name="priceType">A required price type: Bid or Ask.</param>
        /// <param name="period">Bar period instance; can not be null.</param>
        /// <param name="startTime">A start time of bars enumeration.</param>
        /// <param name="barsNumber">Requested bars number; positive value means forward enumeration; negative value means backward enumeration.</param>
        /// <exception cref="System.ArgumentNullException">If datafeed, period or symbol is null.</exception>
        public Bars(DataFeed datafeed, string symbol, PriceType priceType, BarPeriod period, DateTime startTime, int barsNumber)
            : this(datafeed, symbol, priceType, period, startTime, barsNumber, DefaultBufferSize)
        {
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
        /// <param name="priceType">A required price type: Bid or Ask.</param>
        /// <param name="period">Bar period instance; can not be null.</param>
        /// <param name="startTime">A start time of bars enumeration.</param>
        /// <param name="barsNumber">Requested bars number; positive value means forward enumeration; negative value means backward enumeration.</param>
        /// <exception cref="System.ArgumentNullException">If storage, period or symbol is null.</exception>
        public Bars(IStorage storage, string symbol, PriceType priceType, BarPeriod period, DateTime startTime, int barsNumber)
        {
            ValidateArguments(storage, symbol, period);
            this.enumerator = new BarsCountEnumerator(storage, symbol, priceType, period, startTime, barsNumber);
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
        /// <param name="priceType">A required price type: Bid or Ask.</param>
        /// <param name="period">Bar period instance; can not be null.</param>
        /// <param name="startTime">A start time of bars enumeration.</param>
        /// <param name="barsNumber">Requested bars number; positive value means forward enumeration; negative value means backward enumeration.</param>
        /// <param name="preferredBufferSize">Bars enumeration requests bars from server by chunks. This is preferred chunk size. It should be positive.</param>
        /// <exception cref="System.ArgumentNullException">If datafeed, period or symbol is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">If preferredBufferSize is less than 2.</exception>
        public Bars(DataFeed datafeed, string symbol, PriceType priceType, BarPeriod period, DateTime startTime, int barsNumber, int preferredBufferSize)
        {
            ValidateArguments(datafeed, symbol, period, preferredBufferSize);
            this.enumerator = new BarsCountEnumerator(datafeed, symbol, priceType, period, startTime, barsNumber, preferredBufferSize);
        }

        static void ValidateArguments(DataFeed datafeed, string symbol, BarPeriod period, int preferredBufferSize)
        {
            if (datafeed == null)
                throw new ArgumentNullException(nameof(datafeed), "DataFeed instance can not be null.");

            if (symbol == null)
                throw new ArgumentNullException(nameof(symbol), "Symbol can not be null.");

            if (period == null)
                throw new ArgumentNullException(nameof(period), "Bar period instance can not be null.");

            if (preferredBufferSize <= 1)
                throw new ArgumentOutOfRangeException(nameof(preferredBufferSize), preferredBufferSize, "Preferred buffer size should be more than 1.");
        }

        static void ValidateArguments(IStorage storage, string symbol, BarPeriod period)
        {
            if (storage == null)
                throw new ArgumentNullException(nameof(storage), "Storage provider instance can not be null.");

            if (symbol == null)
                throw new ArgumentNullException(nameof(symbol), "Symbol can not be null.");

            if (period == null)
                throw new ArgumentNullException(nameof(period), "Bar period instance can not be null.");
        }

        #endregion

        /// <summary>
        /// The method returns bars enumerator.
        /// </summary>
        /// <returns>Can not be null.</returns>
        public IEnumerator<Bar> GetEnumerator()
        {
            var result = this.enumerator.Clone();
            return result;
        }

        /// <summary>
        /// The method returns bars enumerator.
        /// </summary>
        /// <returns>Can not be null.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            var result = this.enumerator.Clone();
            return result;
        }

        #region Members

        const int DefaultBufferSize = 16;
        readonly ICloneableEnumerator<Bar> enumerator;

        #endregion
    }
}
