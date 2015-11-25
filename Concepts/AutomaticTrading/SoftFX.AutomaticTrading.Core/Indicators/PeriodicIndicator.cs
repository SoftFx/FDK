namespace SoftFX.AutomaticTrading.Core.Indicators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provides basic functionality for building periodical indicators.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    [Serializable]
    public abstract class PeriodicIndicator<TValue, TResult> : IndicatorBase<TValue, TResult>
    {
        const int DefaultPeriod = 10;

        /// <summary>
        /// Gets input values for last indicator period.
        /// </summary>
        protected IList<TResult> PeriodData { get; private set; }

        /// <summary>
        /// Creates a new periodic indicator.
        /// </summary>
        protected PeriodicIndicator()
            : this(DefaultPeriod)
        {
        }

        /// <summary>
        /// Creates a new periodic indicator.
        /// </summary>
        /// <param name="period">Period length.</param>
        protected PeriodicIndicator(int period)
        {
            if (period < 1)
                throw new ArgumentOutOfRangeException("period");

            this.Period = period;
            this.PeriodData = new List<TResult>(period);
        }

        /// <summary>
        /// Gets indicator period.
        /// </summary>
        public int Period { get; private set; }

        /// <summary>
        /// Returns ready when indicator data is enough for configured period.
        /// </summary>
        public override bool IsReady
        {
            get { return this.PeriodData.Count >= this.Period; }
        }

        /// <summary>
        /// Called when indicator is reset.
        /// </summary>
        protected override void OnReset()
        {
            base.OnReset();
            this.PeriodData.Clear();
        }
    }

    /// <summary>
    /// Provides basic functionality for building periodical indicators.
    /// </summary>
    [Serializable]
    public abstract class PeriodicIndicator<T> : PeriodicIndicator<T, T>
    {
        /// <summary>
        /// Creates new periodic indicator.
        /// </summary>
        protected PeriodicIndicator()
            : base()
        {
        }

        /// <summary>
        /// Creates new periodic indicator.
        /// </summary>
        /// <param name="period">Period length.</param>
        protected PeriodicIndicator(int period)
            : base(period)
        {
        }
    }
}
