namespace SoftFX.Extended
{
    using System;
    using SoftFX.Extended.Storage;

    abstract class DirectionalBarsEnumerator : BarsEnumeratorBase
    {
        protected enum Direction
        {
            Forward,
            Backward,
            None
        }

        protected DirectionalBarsEnumerator(DataFeed dataFeed, IStorage storage, string symbol, PriceType priceType, BarPeriod period, DateTime startTime, int preferredBufferSize)
            : base(dataFeed, storage, symbol, priceType, period, startTime, preferredBufferSize)
        {
        }

        protected abstract Direction GetDirection();

        protected virtual bool CheckIsFinished()
        {
            return this.Bar == null;
        }

        protected override void ResetImplementation()
        {
            base.ResetImplementation();

            this.NextTime = this.StartTime;
            this.Bars = null;
            this.Index = -1;
            this.Count = 0;
        }

        protected sealed override bool MoveNextImplementation()
        {
            switch (this.GetDirection())
            {
                case Direction.Forward:
                    return this.MoveForward();
                case Direction.Backward:
                    return this.MoveBackward();
                default:
                    return false;
            }
        }

        bool MoveForward()
        {
            if (this.IsFinished)
                return false;

            this.StepForward();

            this.IsFinished |= this.CheckIsFinished();

            return !this.IsFinished;
        }

        void StepForward()
        {
            if (this.Bars == null || this.Index >= this.Bars.Length)
            {
                this.Index = 0;

                this.StepBarsForward();

                if (this.Bars.Length == 0)
                    this.IsFinished = true;
                else
                    this.Bar = this.Bars[this.Index++];
            }
            else
                this.Bar = this.Bars[this.Index++];

            if (!this.IsFinished)
                this.NextTime = this.Bar.To;

            this.Count++;
        }

        void StepBarsForward()
        {
            if (this.Storage != null)
                this.Bars = this.Storage.GetBars(this.Symbol, this.PriceType, this.Period, this.NextTime, StorageSize);
            else
            {
                var info = this.DataFeed.Server.GetHistoryBars(this.Symbol, this.NextTime, this.PreferredBufferSize, this.PriceType, this.Period);
                this.Bars = info.Bars;
            }
        }

        bool MoveBackward()
        {
            if (this.IsFinished)
                return false;

            this.StepBackward();

            this.IsFinished |= this.CheckIsFinished();

            return !this.IsFinished;
        }

        void StepBackward()
        {
            if (this.Bars == null || this.Index >= this.Bars.Length)
            {
                this.Index = 0;

                this.StepBarsBackward();

                if (this.Bars.Length == 0)
                    this.IsFinished = true;
                else
                    this.Bar = this.Bars[this.Index++];
            }
            else
                this.Bar = this.Bars[this.Index++];

            if (!this.IsFinished)
                this.NextTime = this.Bar.From;

            this.Count--;
        }

        protected void StepBarsBackward()
        {
            if (this.Storage != null)
                this.Bars = this.Storage.GetBars(this.Symbol, this.PriceType, this.Period, this.NextTime, -StorageSize);
            else
            {
                var info = this.DataFeed.Server.GetHistoryBars(this.Symbol, this.NextTime - this.Period, -this.PreferredBufferSize, this.PriceType, this.Period);
                this.Bars = info.Bars;
            }
        }

        protected Bar[] Bars { get; private set; }
        protected DateTime NextTime { get; private set; }

        protected int Index { get; private set; }
        protected int Count { get; private set; }


        #region Constants

        const int StorageSize = 512;

        #endregion
    }
}
