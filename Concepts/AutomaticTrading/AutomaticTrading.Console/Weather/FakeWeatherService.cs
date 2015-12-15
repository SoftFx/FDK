namespace AutomaticTrading.Console.Weather
{
    using System;

    class FakeWeatherService : IWeatherService
    {
        double current;

        public double Temperature
        {
            get
            {
                var temperature = Math.Round(Math.Cos(current) * 15, 2);
                current += 0.02;
                return temperature;
            }
        }

        public void Dispose()
        {
        }
    }
}
