namespace SoftFX.AutomaticTrading.Hosting.DataSources
{
    using System;
    using System.Collections.Generic;

    public interface IDataSource
    {
        string Name { get; }

        IEnumerable<TimeSpan> SupportedPeriodicities { get; }

        Type DataType { get; }

        bool IsRealTimeDataSupported { get; }

        IObservable<object> GetRealTimeData(TimeSpan? periodicity);

        bool IsHistoricalDataSupported { get; }

        TimeFrame GetHistoricalDataTimeFrame(TimeSpan? periodicity);

        IEnumerable<object> GetHistoricalData(TimeSpan? periodicity, DateTime start, DateTime end);

        IEnumerable<object> GetHistoricalData(TimeSpan? periodicity, DateTime start, int count);

        IEnumerable<ITypedDataMapper> DataMappers { get; }
    }
}
