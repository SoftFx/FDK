namespace AutomaticTrading.Console.Weather
{
    using System.Collections.Generic;
    using SoftFX.AutomaticTrading.Hosting.DataSources;

    class WeatherDataSourceProvider : IDataSourceProvider
    {
        public WeatherDataSourceProvider()
        {
            this.DataSources = new[] { new WeatherDataSource(GetWeatherService(WeatherServiceType.WeatherUnderground)) };
        }

        public string Name
        {
            get { return "Weather"; }
        }

        public IEnumerable<IDataSource> DataSources { get; private set; }

        enum WeatherServiceType
        {
            WeatherUnderground,
            Fake
        }

        static IWeatherService GetWeatherService(WeatherServiceType type)
        {
            switch (type)
            {
                case WeatherServiceType.WeatherUnderground:
                    return new WeatherUndergroundService("e7fe61b98ca8b216", "Latvia", "Riga");
                default:
                    return new FakeWeatherService();
            }
        }
    }
}
