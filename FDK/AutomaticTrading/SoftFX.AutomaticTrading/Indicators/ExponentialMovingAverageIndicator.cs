namespace SoftFX.AutomaticTrading.Indicators
{
    using System;
    using System.Linq;
    using SoftFX.AutomaticTrading.Core.Indicators;

    [Serializable]
    public class ExponentialMovingAverageIndicator : PeriodicIndicator<double>
    {
        public double Factor { get; private set; }

        public ExponentialMovingAverageIndicator(int period, double factor)
            : base(period)
        {
            if (factor < 0 || factor > 1)
                throw new ArgumentOutOfRangeException("factor", "Factor should be in [0; 1] range.");

            this.Factor = factor;
        }

        protected override double OnCalculate(double value, bool current)
        {
            if (current)
                this.PeriodData.RemoveAt(this.PeriodData.Count - 1);

            this.PeriodData.Add(value);

            if (!this.IsReady)
                return this.LastValue;

            if (this.PeriodData.Count == this.Period)
                return this.PeriodData.Sum() / this.Period;
            else
            {
                this.PeriodData.RemoveAt(0);

                var average = (value - this.LastValue) * this.Factor + this.LastValue;
                return average;
            }
        }
    }
}
