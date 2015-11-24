namespace SoftFX.AutomaticTrading.Indicators
{
    using System;
    using System.Linq;
    using SoftFX.AutomaticTrading.Core.Indicators;

    [Serializable]
    public class WeightedMovingAverageIndicator : PeriodicIndicator<double>
    {
        readonly int divider;

        public WeightedMovingAverageIndicator(int period)
            : base(period)
        {
            this.divider = Enumerable.Range(1, period).Sum();
        }

        protected override double OnCalculate(double value, bool current)
        {
            if (current)
                this.PeriodData.RemoveAt(this.PeriodData.Count - 1);

            if (this.IsReady)
                this.PeriodData.RemoveAt(0);

            this.PeriodData.Add(value);

            var average = this.PeriodData.Select((o, n) => o * (n + 1)).Sum() / this.divider;

            return average;
        }
    }
}
