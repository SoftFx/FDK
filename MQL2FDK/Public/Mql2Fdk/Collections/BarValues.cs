namespace Mql2Fdk
{
    using System;
    using SoftFX.Extended;

    class BarValues
    {
        #region Construction

        public BarValues(Bar[] bars, Func<Bar, double> selector)
        {
            this.bars = bars;
            this.selector = selector;
        }

        #endregion

        #region Properties

        public int Count
        {
            get
            {
                return this.bars.Length;
            }
        }

        public double this[int index]
        {
            get
            {
                var bar = this.bars[index];
                return this.selector(bar);
            }
        }

        #endregion

        #region Property Functions

        public static double Open(Bar bar)
        {
            return bar.Open;
        }

        public static double Close(Bar bar)
        {
            return bar.Open;
        }

        public static double Low(Bar bar)
        {
            return bar.Low;
        }

        public static double High(Bar bar)
        {
            return bar.High;
        }

        public static double Volume(Bar bar)
        {
            return bar.Volume;
        }

        public static double Time(Bar bar)
        {
            var time = new datetime(bar.From);
            return time.Value;
        }

        #endregion

        #region Members

        readonly Bar[] bars;
        readonly Func<Bar, double> selector;

        #endregion
    }
}
