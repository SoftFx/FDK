namespace SoftFX.AutomaticTrading.Hosting.Infrastructure
{
    using System;
    using SoftFX.AutomaticTrading.Hosting.DataSources;

    public class RealTimeDataMonitor : IDisposable
    {
        readonly IObserver<object> observer;
        IDisposable subscription;

        public RealTimeDataMonitor(IDataSource dataSource, TimeSpan? periodicity)
        {
            if (dataSource == null)
                throw new ArgumentNullException("dataSource");

            if (!dataSource.IsRealTimeDataSupported)
                throw new InvalidOperationException("Real time data is not supported by this data source.");

            this.DataSource = dataSource;
            this.Periodicity = periodicity;
            this.observer = new RealTimeDataObserver(this);
        }

        public TimeSpan? Periodicity { get; private set; }

        public IDataSource DataSource { get; private set; }

        public void Start()
        {
            lock (this.observer)
            {
                if (subscription != null)
                    return;

                var data = this.DataSource.GetRealTimeData(this.Periodicity);
                this.subscription = data.Subscribe(this.observer);
            }
        }

        public void Stop()
        {
            lock (this.observer)
            {
                if (subscription == null)
                    return;

                this.subscription.Dispose();
                this.subscription = null;
            }
        }

        void RaiseNewData(object value)
        {
            var eh = this.NewData;
            if (eh != null)
            {
                eh(this, value);
            }
        }

        public EventHandler<object> NewData;

        sealed class RealTimeDataObserver : IObserver<object>
        {
            readonly RealTimeDataMonitor monitor;

            public RealTimeDataObserver(RealTimeDataMonitor monitor)
            {
                if (monitor == null)
                    throw new ArgumentNullException("monitor");

                this.monitor = monitor;
            }

            public void OnCompleted()
            {
            }

            public void OnError(Exception error)
            {
            }

            public void OnNext(object value)
            {
                this.monitor.RaiseNewData(value);
            }
        }

        public void Dispose()
        {
            this.Stop();
        }
    }
}
