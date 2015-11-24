namespace SoftFX.Extended
{
    using System;
    using SoftFX.Extended.Storage;

    sealed class BarsCountEnumerator : DirectionalBarsEnumerator
    {
        #region Construction

        public BarsCountEnumerator(DataFeed dataFeed, string symbol, PriceType priceType, BarPeriod period, DateTime startTime, int barsNumber, int preferredBufferSize)
            : this(dataFeed, null, symbol, priceType, period, startTime, barsNumber, preferredBufferSize)
        {
        }

        public BarsCountEnumerator(IStorage storage, string symbol, PriceType priceType, BarPeriod period, DateTime startTime, int barsNumber)
            : this(null, storage, symbol, priceType, period, startTime, barsNumber, 0)
        {
        }

        public BarsCountEnumerator(DataFeed dataFeed, IStorage storage, string symbol, PriceType priceType, BarPeriod period, DateTime startTime, int barsNumber, int preferredBufferSize)
            : base(dataFeed, storage, symbol, priceType, period, startTime, preferredBufferSize)
        {
            this.barsNumber = barsNumber;

            this.Reset();
        }

        #endregion

        #region Overrides

        protected override Direction GetDirection()
        {
            if (this.barsNumber > 0)
                return Direction.Forward;
            else if (this.barsNumber < 0)
                return Direction.Backward;

            return Direction.None;
        }

        protected override bool CheckIsFinished()
        {
            return base.CheckIsFinished() || (this.GetDirection() == Direction.Forward ? this.Count > this.barsNumber : this.Count < this.barsNumber);
        }

        public override ICloneableEnumerator<Bar> Clone()
        {
            return new BarsCountEnumerator(this.DataFeed, this.Storage, this.Symbol, this.PriceType, this.Period, this.StartTime, this.barsNumber, this.PreferredBufferSize);
        }

        #endregion

        #region Fields

        readonly int barsNumber;

        #endregion
    }
}
