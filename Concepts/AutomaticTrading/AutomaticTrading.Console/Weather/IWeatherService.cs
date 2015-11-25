namespace AutomaticTrading.Console.Weather
{
    using System;

    interface IWeatherService : IDisposable
    {
        double Temperature { get; }
    }
}
