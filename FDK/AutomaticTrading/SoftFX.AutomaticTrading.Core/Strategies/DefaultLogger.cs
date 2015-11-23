namespace SoftFX.AutomaticTrading.Core.Strategies
{
    using System;

    class DefaultLogger : IStrategyLogger
    {
        public void Log(string text)
        {
            Console.WriteLine("STRATEGY: " + text);
        }
    }
}
