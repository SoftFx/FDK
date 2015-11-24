namespace SoftFX.Extended
{
    using System;
    using System.Collections;
    using SoftFX.Extended.Storage;

    abstract class BarsEnumeratorBase : ICloneableEnumerator<Bar>
    {
        protected BarsEnumeratorBase(DataFeed dataFeed, IStorage storage, string symbol, PriceType priceType, BarPeriod period, DateTime startTime, int preferredBufferSize)
        {
            this.DataFeed = dataFeed;
            this.Storage = storage;
            this.Symbol = symbol;
            this.PriceType = priceType;
            this.Period = period;
            this.StartTime = startTime;

            this.PreferredBufferSize = preferredBufferSize;
        }

        public Bar Current
        {
            get { return this.GetCurrent(); }
        }

        object IEnumerator.Current
        {
            get { return this.GetCurrent(); }
        }

        Bar GetCurrent()
        {
            if (this.IsFinished)
                throw new InvalidOperationException("End of bars enumeration has been reached; use Reset and MoveNext methods.");

            if (this.Bar == null)
                throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");

            return this.Bar;
        }

        public bool MoveNext()
        {
            return this.MoveNextImplementation();
        }

        protected abstract bool MoveNextImplementation();


        public void Reset()
        {
            this.ResetImplementation();
        }

        protected virtual void ResetImplementation()
        {
            this.IsFinished = false;
            this.Bar = null;
        }

        public virtual void Dispose()
        {
        }

        public abstract ICloneableEnumerator<Bar> Clone();

        protected DataFeed DataFeed { get; private set; }
        protected IStorage Storage { get; private set; }
        protected string Symbol { get; private set; }
        protected PriceType PriceType { get; private set; }
        protected int PreferredBufferSize { get; private set; }
        protected BarPeriod Period { get; private set; }
        protected DateTime StartTime { get; private set; }

        protected Bar Bar { get; set; }
        protected bool IsFinished { get; set; }
    }
}
