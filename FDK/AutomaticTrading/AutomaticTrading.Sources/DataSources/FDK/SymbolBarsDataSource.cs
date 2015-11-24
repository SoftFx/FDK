namespace AutomaticTrading.Sources.DataSources
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using SoftFX.AutomaticTrading.Hosting;
    using SoftFX.AutomaticTrading.Hosting.DataSources;
    using SoftFX.Extended;
    using SoftFX.Extended.Events;
    using SoftFX.Extended.Storage;

    public class SymbolBarsDataSource : IDataSource
    {
        readonly DataFeedStorage storage;
        readonly DataFeed dataFeed;
        readonly string symbol;
        readonly PriceType priceType;

        public SymbolBarsDataSource(DataFeed dataFeed, DataFeedStorage storage, string symbol, PriceType priceType)
        {
            this.dataFeed = dataFeed;
            this.storage = storage;
            this.symbol = symbol;
            this.priceType = priceType;
            this.Name = symbol + (priceType == PriceType.Ask ? " (ASK)" : " (BID)");
        }

        public string Name { get; private set; }

        public Type DataType
        {
            get { return typeof(Bar); }
        }

        public IEnumerable<TimeSpan> SupportedPeriodicities
        {
            get
            {
                yield return TimeSpan.FromSeconds(10);
                yield return TimeSpan.FromMinutes(1);
                yield return TimeSpan.FromHours(1);
            }
        }

        public bool IsRealTimeDataSupported
        {
            get { return true; }
        }

        public IObservable<object> GetRealTimeData(TimeSpan? periodicity)
        {
            this.dataFeed.Server.SubscribeToQuotes(new[] { this.symbol }, 1);

            return Observable.FromEventPattern<TickEventArgs>(this.dataFeed, "Tick")
                             .Select(o => o.EventArgs.Tick)
                             .Where(o => o.Symbol == this.symbol)
                             .Select(o => this.GetLastBar(o, periodicity))
                             .Where(o => o != null)
                             .DistinctUntilChanged(BarTimeEqualityComparer.Instance);
        }

        Bar GetLastBar(Quote tick, TimeSpan? periodicity)
        {
            var bars = this.storage.Online.GetBars(this.symbol, this.priceType, ToBarPeriod(periodicity), tick.CreatingTime, -1);
            return bars.FirstOrDefault();
        }

        public bool IsHistoricalDataSupported
        {
            get { return true; }
        }

        public TimeFrame GetHistoricalDataTimeFrame(TimeSpan? periodicity)
        {
            var info = this.storage.Offline.GetBarsInfo(this.symbol, this.priceType, ToBarPeriod(periodicity));
            return new TimeFrame(info.AvailableFrom, info.AvailableTo);
        }

        public IEnumerable<object> GetHistoricalData(TimeSpan? periodicity, DateTime start, DateTime end)
        {
            return this.storage.Offline.GetBars(this.symbol, this.priceType, ToBarPeriod(periodicity), start, end);
        }

        public IEnumerable<object> GetHistoricalData(TimeSpan? periodicity, DateTime start, int count)
        {
            return this.storage.Offline.GetBars(this.symbol, this.priceType, ToBarPeriod(periodicity), start, count);
        }

        static BarPeriod ToBarPeriod(TimeSpan? periodicity)
        {
            if (periodicity.HasValue)
            {
                if (periodicity.Value == TimeSpan.FromSeconds(10))
                    return BarPeriod.S10;
                else if (periodicity.Value == TimeSpan.FromMinutes(1))
                    return BarPeriod.M1;
                else if (periodicity.Value == TimeSpan.FromHours(1))
                    return BarPeriod.H1;
            }

            throw new InvalidOperationException("Periodicity is not supported!");
        }

        public IEnumerable<ITypedDataMapper> DataMappers
        {
            get
            {
                yield return Mappers.Open;
                yield return Mappers.Close;
                yield return Mappers.High;
                yield return Mappers.Low;
            }
        }

        sealed class BarTimeEqualityComparer : EqualityComparer<Bar>
        {
            public static readonly IEqualityComparer<Bar> Instance = new BarTimeEqualityComparer();

            BarTimeEqualityComparer()
            {
            }

            public override bool Equals(Bar x, Bar y)
            {
                return x.From == y.From;
            }

            public override int GetHashCode(Bar obj)
            {
                return obj.From.GetHashCode();
            }
        }
    }
}
