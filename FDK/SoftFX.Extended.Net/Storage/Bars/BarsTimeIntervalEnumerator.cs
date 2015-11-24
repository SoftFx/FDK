namespace SoftFX.Extended
{
    using System;
    using SoftFX.Extended.Storage;

    sealed class BarsTimeIntervalEnumerator : DirectionalBarsEnumerator
    {
        #region Construction

        public BarsTimeIntervalEnumerator(DataFeed dataFeed, string symbol, PriceType priceType, BarPeriod period, DateTime startTime, DateTime endTime, int preferredBufferSize)
            : this(dataFeed, null, symbol, priceType, period, startTime, endTime, preferredBufferSize)
        {
        }

        public BarsTimeIntervalEnumerator(IStorage storage, string symbol, PriceType priceType, BarPeriod period, DateTime startTime, DateTime endTime)
            : this(null, storage, symbol, priceType, period, startTime, endTime, 0)
        {
        }

        public BarsTimeIntervalEnumerator(DataFeed dataFeed, IStorage storage, string symbol, PriceType priceType, BarPeriod period, DateTime startTime, DateTime endTime, int preferredBufferSize)
            : base(dataFeed, storage, symbol, priceType, period, startTime, preferredBufferSize)
        {
            this.endTime = endTime;

            this.Reset();
        }

        #endregion

        #region Overrides

        protected override Direction GetDirection()
        {
            return this.StartTime <= this.endTime ? Direction.Forward : Direction.Backward;
        }

        protected override bool CheckIsFinished()
        {
            return base.CheckIsFinished() || (this.GetDirection() == Direction.Forward ? this.Bar.To > this.endTime : this.Bar.From < this.endTime);
        }

        public override ICloneableEnumerator<Bar> Clone()
        {
            return new BarsTimeIntervalEnumerator(this.DataFeed, this.Storage, this.Symbol, this.PriceType, this.Period, this.StartTime, this.endTime, this.PreferredBufferSize);
        }

        #endregion

        #region Fields

        readonly DateTime endTime;

        #endregion
    }
}
