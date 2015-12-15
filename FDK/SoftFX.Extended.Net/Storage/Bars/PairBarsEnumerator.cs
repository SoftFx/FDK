namespace SoftFX.Extended
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    class PairBarsEnumerator : IEnumerator<PairBar>
    {
        public PairBarsEnumerator(IEnumerable<Bar> bids, IEnumerable<Bar> asks)
        {
            this.bids = bids;
            this.asks = asks;
            this.Reset();
        }

        public PairBar Current
        {
            get
            {
                return this.current;
            }
        }

        public void Dispose()
        {
        }

        object IEnumerator.Current
        {
            get
            {
                return this.current;
            }
        }

        public bool MoveNext()
        {
            this.ResetCurrent();
            this.Move();
            var result = this.UpdateCurrent();
            return result;
        }

        public void Reset()
        {
            var bidEnumerator = this.bids.GetEnumerator();
            var askEnumerator = this.asks.GetEnumerator();
            this.bidEnumerator = bidEnumerator;
            this.askEnumerator = askEnumerator;
            this.current = new PairBar();
        }

        void ResetCurrent()
        {
            if (this.bid == null)
            {
                this.ask = null;
            }
            else if (this.ask == null)
            {
                this.bid = null;
            }
            else
            {
                var status = DateTime.Compare(this.bid.From, this.ask.From);
                if (status <= 0)
                {
                    this.bid = null;
                }
                if (status >= 0)
                {
                    this.ask = null;
                }
            }
        }

        void Move()
        {
            if (this.bid == null)
            {
                if (this.bidEnumerator.MoveNext())
                {
                    this.bid = this.bidEnumerator.Current;
                }
            }
            if (this.ask == null)
            {
                if (this.askEnumerator.MoveNext())
                {
                    this.ask = this.askEnumerator.Current;
                }
            }
        }

        bool UpdateCurrent()
        {
            var bid = this.bid;
            var ask = this.ask;
            if (bid != null && ask != null)
            {
                var status = DateTime.Compare(bid.From, ask.From);
                if (status < 0)
                {
                    ask = null;
                }
                if (status > 0)
                {
                    bid = null;
                }
            }
            this.current = new PairBar(bid, ask);
            if (bid == null && ask == null)
            {
                return false;
            }
            return true;
        }

        #region Members

        readonly IEnumerable<Bar> bids;
        readonly IEnumerable<Bar> asks;
        IEnumerator<Bar> bidEnumerator;
        IEnumerator<Bar> askEnumerator;
        Bar bid;
        Bar ask;
        PairBar current;

        #endregion
    }
}
