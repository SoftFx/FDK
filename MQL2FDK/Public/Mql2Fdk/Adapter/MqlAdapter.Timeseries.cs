namespace Mql2Fdk
{
    using System;
    using SoftFX.Extended;

    /// <summary>
    /// 
    /// </summary>
    public partial class MqlAdapter
    {
        #region Timeseries and Indicators Access

        /// <summary>
        /// Series array that contains open prices of each bar of the current chart.
        /// </summary>
        protected BarPrices Open { get; private set; }

        /// <summary>
        /// Series array that contains close prices for each bar of the current chart.
        /// </summary>
        protected BarPrices Close { get; private set; }

        /// <summary>
        /// Series array that contains the highest prices of each bar of the current chart.
        /// </summary>
        protected BarPrices High { get; private set; }

        /// <summary>
        /// Series array that contains the lowest prices of each bar of the current chart.
        /// </summary>
        protected BarPrices Low { get; private set; }

        /// <summary>
        /// Series array that contains tick volumes of each bar of the current chart.
        /// </summary>
        protected BarVolumes Volume { get; private set; }

        /// <summary>
        /// Series array that contains open time of each bar of the current chart. Data like datetime represent time, in seconds, that has passed since 00:00 a.m. of 1 January, 1970.
        /// Series array elements are indexed in the reverse order, i.e., from the last one to the first one. The current bar which is the last in the array is indexed as 0. The oldest bar, the first in the chart, is indexed as Bars-1.
        /// </summary>
        protected datetime[] Time { get; set; }

        /// <summary>
        /// Number of bars in the current chart.
        /// </summary>
        protected int Bars
        {
            get { return this.currentSnapshot.Bars.Length; }
        }

        Bar[] GetBars(string symbol, int timeframe)
        {
            if (symbol == null)
                symbol = this.symbol;

            var periodicity = this.PeriodicityFromTimeFrame(timeframe);
            var result = this.currentSnapshot.GetBars(symbol, periodicity);
            return result;
        }

        BarPeriod PeriodicityFromTimeFrame(int timeframe)
        {
            if (timeframe == 0)
                return this.periodicity;

            if (timeframe == PERIOD_M1)
                return BarPeriod.M1;

            if (timeframe == PERIOD_M5)
                return BarPeriod.M5;

            if (timeframe == PERIOD_M15)
                return BarPeriod.M15;

            if (timeframe == PERIOD_M30)
                return BarPeriod.M30;

            if (timeframe == PERIOD_H1)
                return BarPeriod.H1;

            if (timeframe == PERIOD_H4)
                return BarPeriod.H4;

            if (timeframe == PERIOD_D1)
                return BarPeriod.D1;

            if (timeframe == PERIOD_W1)
                return BarPeriod.W1;

            if (timeframe == PERIOD_MN1)
                return BarPeriod.MN1;

            var message = string.Format("Unsupported time frame = {0}", timeframe);
            throw new ArgumentException(message, "timeframe");
        }

        BarValues GetBarValues(string symbol, int timeframe, int type)
        {
            var bars = this.GetBars(symbol, timeframe);

            if (type == MODE_OPEN)
                return new BarValues(bars, BarValues.Open);
            else if (type == MODE_LOW)
                return new BarValues(bars, BarValues.Low);
            else if (type == MODE_HIGH)
                return new BarValues(bars, BarValues.High);
            else if (type == MODE_CLOSE)
                return new BarValues(bars, BarValues.Close);
            else if (type == MODE_VOLUME)
                return new BarValues(bars, BarValues.Volume);
            else if (type == MODE_TIME)
                return new BarValues(bars, BarValues.Time);

            var message = string.Format("Unsupported type = {0}", type);
            throw new ArgumentException(message, "type");
        }

        /// <summary>
        /// Refreshing of data in pre-defined variables and series arrays.
        /// </summary>
        /// <returns></returns>
        protected bool RefreshRates()
        {
            lock (this.synchronizer)
            {
                if (this.nextSnapshot != null)
                    this.currentSnapshot = this.nextSnapshot;
            }

            return true;
        }

        /// <summary>
        /// Returns High value for the bar of indicated symbol with timeframe and shift. If local history is empty (not loaded), function returns 0.
        /// </summary>
        /// <param name="symbol">symbol on that data need to calculate indicator; NULL means current symbol</param>
        /// <param name="timeframe">it can be any of Timeframe enumeration values; 0 means the current chart timeframe</param>
        /// <param name="shift">index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago)</param>
        /// <returns></returns>
        protected double iHigh(string symbol, int timeframe, int shift)
        {
            var bars = this.GetBars(symbol, timeframe);
            var bar = bars[shift];
            return bar.High;
        }

        /// <summary>
        /// Returns Low value for the bar of indicated symbol with timeframe and shift. If local history is empty (not loaded), function returns 0.
        /// </summary>
        /// <param name="symbol">symbol the data of which should be used to calculate indicator; NULL means the current symbol</param>
        /// <param name="timeframe">it can be any of Timeframe enumeration values. 0 means the current chart timeframe</param>
        /// <param name="shift">index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago)</param>
        /// <returns></returns>
        protected double iLow(string symbol, int timeframe, int shift)
        {
            var bars = this.GetBars(symbol, timeframe);
            var bar = bars[shift];
            return bar.Low;
        }

        /// <summary>
        /// Returns Open value for the bar of indicated symbol with timeframe and shift. If local history is empty (not loaded), function returns 0.
        /// </summary>
        /// <param name="symbol">symbol the data of which should be used to calculate indicator; NULL means the current symbol</param>
        /// <param name="timeframe">it can be any of Timeframe enumeration values; 0 means the current chart timeframe</param>
        /// <param name="shift">index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago)</param>
        /// <returns></returns>
        protected double iOpen(string symbol, int timeframe, int shift)
        {
            var bars = this.GetBars(symbol, timeframe);
            var bar = bars[shift];
            return bar.Open;
        }

        /// <summary>
        /// Returns Close value for the bar of indicated symbol with timeframe and shift. If local history is empty (not loaded), function returns 0.
        /// </summary>
        /// <param name="symbol">symbol the data of which should be used to calculate indicator; NULL means the current symbol</param>
        /// <param name="timeframe">it can be any of Timeframe enumeration values; 0 means the current chart timeframe</param>
        /// <param name="shift">index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago)</param>
        /// <returns></returns>
        protected double iClose(string symbol, int timeframe, int shift)
        {
            var bars = this.GetBars(symbol, timeframe);
            var bar = bars[shift];
            return bar.Close;
        }


        /// <summary>
        /// Returns Time value for the bar of indicated symbol with timeframe and shift. If local history is empty (not loaded), function returns 0.
        /// </summary>
        /// <param name="symbol">symbol the data of which should be used to calculate indicator; NULL means the current symbol</param>
        /// <param name="timeframe">it can be any of Timeframe enumeration values; 0 means the current chart timeframe</param>
        /// <param name="shift">index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago)</param>
        /// <returns></returns>
        protected datetime iTime(string symbol, int timeframe, int shift)
        {
            var bars = this.GetBars(symbol, timeframe);
            return 0;
        }

        /// <summary>
        /// Returns Tick Volume value for the bar of indicated symbol with timeframe and shift. If local history is empty (not loaded), function returns 0.
        /// </summary>
        /// <param name="symbol">symbol the data of which should be used to calculate indicator; NULL means the current symbol</param>
        /// <param name="timeframe">it can be any of Timeframe enumeration values. 0 means the current chart timeframe</param>
        /// <param name="shift">index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago)</param>
        /// <returns></returns>
        protected double iVolume(string symbol, int timeframe, int shift)
        {
            var bars = this.GetBars(symbol, timeframe);
            var bar = bars[shift];
            return bar.Volume;
        }

        /// <summary>
        /// Returns the number of bars on the specified chart.
        /// </summary>
        /// <param name="symbol">symbol the data of which should be used to calculate indicator; NULL means the current symbol</param>
        /// <param name="timeframe">timeframe; tt can be any of Timeframe enumeration values; 0 means the current chart timeframe</param>
        /// <returns></returns>
        protected int iBars(string symbol, int timeframe)
        {
            var bars = this.GetBars(symbol, timeframe);
            return bars.Length;
        }

        /// <summary>
        /// Search for bar by open time. The function returns bar shift with the open time specified.
        /// If the bar having the specified open time is missing, the function will return -1 or the nearest bar shift depending on the exact.
        /// </summary>
        /// <param name="symbol">symbol the data of which should be used to calculate indicator; NULL means the current symbol</param>
        /// <param name="timeframe">tt can be any of Timeframe enumeration values; 0 means the current chart timeframe</param>
        /// <param name="time">value to find (bar's open time)</param>
        /// <param name="exact">return mode when bar not found; false - iBarShift returns nearest; true - iBarShift returns -1. </param>
        /// <returns></returns>
        protected int iBarShift(string symbol, int timeframe, datetime time, bool exact = false)
        {
            var shift = -1;
            var result = -1;
            var minimumDelta = int.MaxValue;
            var bars = this.GetBars(symbol, timeframe);
            foreach (var element in bars)
            {
                ++shift;
                var current = (datetime)element.From;
                int delta = Math.Abs(current.Value - time.Value);
                if (delta < minimumDelta)
                {
                    delta = minimumDelta;
                    result = shift;
                }
            }

            if (result == -1)
                return result;

            if (minimumDelta == 0)
                return result;

            if (exact)
                result = -1;

            return result;
        }

        /// <summary>
        /// Returns the shift of the least value over a specific number of periods depending on type.
        /// </summary>
        /// <param name="symbol">symbol the data of which should be used to calculate indicator; NULL means the current symbol</param>
        /// <param name="timeframe">it can be any of Timeframe enumeration values; 0 means the current chart timeframe</param>
        /// <param name="type">series array identifier; it can be any of Series array identifier enumeration values</param>
        /// <param name="count">number of periods (in direction from the start bar to the back one) on which the calculation is carried out</param>
        /// <param name="start">shift showing the bar, relative to the current bar, that the data should be taken from</param>
        /// <returns></returns>
        protected int iLowest(string symbol, int timeframe, int type, int count = WHOLE_ARRAY, int start = 0)
        {
            var values = this.GetBarValues(symbol, timeframe, type);
            var end = Math.Min(values.Count, start + count);
            if (count == WHOLE_ARRAY)
                end = values.Count;

            var result = -1;
            var minimum = double.MaxValue;

            for (var index = start; result < end; ++index)
            {
                var value = values[index];
                if (value < minimum)
                {
                    minimum = value;
                    result = index;
                }
            }

            return result;
        }

        /// <summary>
        /// Returns the shift of the maximum value over a specific number of periods depending on type.
        /// </summary>
        /// <param name="symbol">symbol the data of which should be used to calculate indicatorl null means the current symbol</param>
        /// <param name="timeframe">timeframe; it can be any of Timeframe enumeration values; 0 means the current chart timeframe</param>
        /// <param name="type"> MODE_OPEN, MODE_LOW, MODE_HIGH, MODE_CLOSE, MODE_VOLUME, MODE_TIME</param>
        /// <param name="count">number of periods (in direction from the start bar to the back one) on which the calculation is carried out</param>
        /// <param name="start">shift showing the bar, relative to the current bar, that the data should be taken from</param>
        /// <returns></returns>
        protected int iHighest(string symbol, int timeframe, int type, int count = WHOLE_ARRAY, int start = 0)
        {
            var values = this.GetBarValues(symbol, timeframe, type);
            var end = Math.Min(values.Count, start + count);
            if (count == WHOLE_ARRAY)
                end = values.Count;

            var result = -1;
            var maximum = -double.MaxValue;

            for (var index = start; result < end; ++index)
            {
                var value = values[index];
                if (value > maximum)
                {
                    maximum = value;
                    result = index;
                }
            }

            return result;
        }

        #endregion
    }
}
