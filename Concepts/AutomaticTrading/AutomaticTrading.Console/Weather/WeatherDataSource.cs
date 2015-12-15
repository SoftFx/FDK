namespace AutomaticTrading.Console.Weather
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Linq;
    using SoftFX.AutomaticTrading.Hosting;
    using SoftFX.AutomaticTrading.Hosting.DataSources;

    class WeatherDataSource : IDataSource
    {
        readonly IWeatherService service;

        public WeatherDataSource(IWeatherService service)
        {
            this.service = service;
        }

        public string Name
        {
            get { return "Weather Riga"; }
        }

        public IEnumerable<TimeSpan> SupportedPeriodicities
        {
            get
            {
                yield return TimeSpan.FromSeconds(10);
            }
        }

        public Type DataType
        {
            get { return typeof(double); }
        }

        public bool IsRealTimeDataSupported
        {
            get { return true; }
        }

        public IObservable<object> GetRealTimeData(TimeSpan? periodicity)
        {
            return Observable.Interval(periodicity.Value)
                             .Select(_ => (object)service.Temperature);
        }

        public bool IsHistoricalDataSupported
        {
            get { return false; }
        }

        public TimeFrame GetHistoricalDataTimeFrame(TimeSpan? periodicity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetHistoricalData(TimeSpan? periodicity, DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetHistoricalData(TimeSpan? periodicity, DateTime start, int count)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITypedDataMapper> DataMappers
        {
            get
            { 
                yield break;
            }
        }
    }
}
