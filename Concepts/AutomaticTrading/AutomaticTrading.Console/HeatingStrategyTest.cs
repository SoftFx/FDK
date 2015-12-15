namespace AutomaticTrading.Console
{
    using System;
    using AutomaticTrading.Console.Strategies;
    using AutomaticTrading.Console.Weather;
    using SoftFX.AutomaticTrading.Core.Indicators;
    using SoftFX.AutomaticTrading.Indicators;
    
    class HeatingStrategyTest
    {
        static readonly TimeSpan PollInterval = TimeSpan.FromSeconds(10);

        static void Main(string[] args)
        {
            var controller = new HeatingController();

            using (var termo = new Termometer(PollInterval))
            {
                var onEvent = new TemperatureEvent(termo, 3, 15, false);
                var offEvent = new TemperatureEvent(termo, 3, 15, true);

                var strategy = new HeatingStrategy(controller, onEvent, offEvent);
                //strategy.Start();

                termo.Start();

                //strategy.Stop();
                Console.ReadKey();
            }

            Console.WriteLine("Press any key..");
            Console.ReadKey();
        }

        enum WeatherServiceType
        {
            WeatherUnderground,
            Fake
        }
    }
}
