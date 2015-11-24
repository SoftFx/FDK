namespace AutomaticTrading.Console.Weather
{
    using System;
    using System.Linq;
    using SoftFX.AutomaticTrading.Hosting.Infrastructure;

    class Termometer : IDisposable
    {
        readonly RealTimeDataMonitor monitor;

        public Termometer(TimeSpan pollInterval)
        {
            var dataSourceProvider = new WeatherDataSourceProvider();
            var dataSource = dataSourceProvider.DataSources.FirstOrDefault(o => o.Name == "Weather Riga");
            if (dataSource == null)
                throw new Exception("Missing data source.");
            
            this.monitor = new RealTimeDataMonitor(dataSource, pollInterval);
            this.monitor.NewData += this.OnNewData;   
        }

        void OnNewData(object source, object args)
        {
            var temperature = (double)args;
            this.RaiseTemperatureReport(temperature);
        }

        public void Start()
        {
            this.monitor.Start();
        }

        public void Stop()
        {
            this.monitor.Stop();
        }

        void RaiseTemperatureReport(double temperature)
        {
            var eh = this.TemperatureReport;
            if (eh != null)
            {
                var args = new TemperatureReportEventArgs
                {
                    Temperature = temperature,
                    Time = DateTime.Now
                };

                eh(this, args);
            }
        }

        public event EventHandler<TemperatureReportEventArgs> TemperatureReport;

        public void Dispose()
        {
            this.monitor.Dispose();
        }
    }

}
