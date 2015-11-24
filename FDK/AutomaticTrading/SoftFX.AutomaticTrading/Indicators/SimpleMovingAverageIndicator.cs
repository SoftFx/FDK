namespace SoftFX.AutomaticTrading.Indicators
{
    using System;
    using SoftFX.AutomaticTrading.Core.Indicators;

    [Serializable]
    public class SimpleMovingAverageIndicator : PeriodicIndicator<double>
    {
        public SimpleMovingAverageIndicator()
            : base()
        {
        }

        public SimpleMovingAverageIndicator(int period)
            : base(period)
        {
        }

        protected override double OnCalculate(double value, bool current)
        {
            var average = this.LastValue + value / this.Period;

            if (current)
            {
                average -= this.PeriodData[this.PeriodData.Count - 1] / this.Period;
                this.PeriodData.RemoveAt(this.PeriodData.Count - 1);
            }

            if (this.IsReady)
            {
                average -= this.PeriodData[0] / this.Period;
                this.PeriodData.RemoveAt(0);
            }

            this.PeriodData.Add(value);

            return average;
        }
    }
}
