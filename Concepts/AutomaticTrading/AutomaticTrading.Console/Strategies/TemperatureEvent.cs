namespace AutomaticTrading.Console.Strategies
{
    using System;
    using AutomaticTrading.Console.Weather;
    using SoftFX.AutomaticTrading.Core.Strategies;
    using SoftFX.AutomaticTrading.Indicators;

    class TemperatureEvent : StrategyEvent
    {
        readonly SimpleMovingAverageIndicator movingAverage;
        readonly Termometer termo;
        readonly bool above;

        public TemperatureEvent(Termometer termo, int period, double temperature, bool above)
        {
            this.movingAverage = new SimpleMovingAverageIndicator(period);
            this.termo = termo;
            this.above = above;
        }

        public override void Enable()
        {
            this.movingAverage.Reset();
            this.termo.TemperatureReport += this.OnTemperatureReport;
        }

        public override void Disable()
        {
            this.termo.TemperatureReport -= this.OnTemperatureReport;
        }

        void OnTemperatureReport(object sender, TemperatureReportEventArgs e)
        {
            this.movingAverage.Calculate(e.Temperature);

            Console.WriteLine("{0} {1}", e.Time, e.Temperature);

            if (this.movingAverage.IsReady)
            {
                Console.WriteLine("Average for last {0} periods: {1}", this.movingAverage.Period, this.movingAverage.LastValue);

                if (this.movingAverage.LastValue > 15 && this.above)
                    this.RaiseExecuted();
                else if (this.movingAverage.LastValue < 15 && !this.above)
                    this.RaiseExecuted();
            }
        }
    }
}
