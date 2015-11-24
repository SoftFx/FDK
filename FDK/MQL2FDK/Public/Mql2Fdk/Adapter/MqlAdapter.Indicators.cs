namespace Mql2Fdk
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public partial class MqlAdapter
    {
        #region Custom Indicators

        /// <summary>
        /// The function returns the amount of bars not changed after the indicator had been launched last. The most calculated bars do not need any recalculation. In most cases, same count of index values do not need for recalculation. The function is used to optimize calculating.
        /// Note: The latest bar is not considered to be calculated and, in the most cases, it is necessary to recalculate only this bar. However, there occur some boundary cases where custom indicator is called from the expert at the first tick of the new bar. It is possible that the last tick of the previous bar had not been processed (because the last-but-one tick was being processed when this last tick came), the custom indicator was not called and it was not calculated because of this. To avoid indicator calculation errors in such situations, the IndicatorCounted() function returns the count of bars minus one. 
        /// </summary>
        /// <returns></returns>
        protected int IndicatorCounted()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets precision format (the count of digits after decimal point) to visualize indicator values. The symbol price preicision is used by default, the indicator being attached to this symbol chart. 
        /// </summary>
        /// <param name="digits"></param>
        protected void IndicatorDigits(int digits)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets precision format (the count of digits after decimal point) to visualize indicator values. The symbol price preicision is used by default, the indicator being attached to this symbol chart. 
        /// </summary>
        /// <param name="digits"></param>
        protected void SetIndexDrawBegin(int index, int begin)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the "short" name of a custom indicator to be shown in the DataWindow and in the chart subwindow. 
        /// </summary>
        /// <param name="name"></param>
        protected void IndicatorShortName(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Allocates memory for buffers used for custom indicator calculations. The amount of buffers cannot exceed 8 or be less than the value given in the indicator_buffers property. If custom indicator requires additional buffers for counting, this function must be used for specifying of the total amount of buffers. 
        /// </summary>
        /// <param name="count"></param>
        protected void IndicatorBuffers(int count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Binds the array variable declared at a global level to the custom indicator pre-defined buffer. The amount of buffers needed to calculate the indicator is set with the IndicatorBuffers() function and cannot exceed 8. If it succeeds, TRUE will be returned, otherwise, it will be FALSE. To get the extended information about the error, one has to call the GetLastError() function. 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        protected bool SetIndexBuffer(int index, double[] array)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets drawing line description for showing in the DataWindow and in the tooltip. 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="text"></param>
        protected void SetIndexLabel(int index, string text)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets offset for the drawing line. For positive values, the line drawing will be shifted to the right, otherwise it will be shifted to the left. I.e., the value calculated on the current bar will be drawn shifted relatively to the current bar. 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="shift"></param>
        protected void SetIndexShift(int index, int shift)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets offset for the drawing line. For positive values, the line drawing will be shifted to the right, otherwise it will be shifted to the left. I.e., the value calculated on the current bar will be drawn shifted relatively to the current bar. 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="shift"></param>
        protected void SetIndexStyle(int index, int type, int style = EMPTY, int width = EMPTY, color clr = default(color))
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Technical Indicators

        /// <summary>
        /// Calculates the Bollinger Bands® indicator and returns its value. 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="timeframe"></param>
        /// <param name="period"></param>
        /// <param name="deviation"></param>
        /// <param name="bands_shift"></param>
        /// <param name="applied_price"></param>
        /// <param name="mode"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        protected double iBands(string symbol, int timeframe, int period, int deviation, int bands_shift, int applied_price, int mode, int shift)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the Bears Power indicator and returns its value. 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="timeframe"></param>
        /// <param name="period"></param>
        /// <param name="applied_price"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        protected double iBearsPower(string symbol, int timeframe, int period, int applied_price, int shift)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the Bulls Power indicator and returns its value. 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="timeframe"></param>
        /// <param name="period"></param>
        /// <param name="applied_price"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        protected double iBullsPower(string symbol, int timeframe, int period, int applied_price, int shift)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the Commodity channel index and returns its value. 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="timeframe"></param>
        /// <param name="period"></param>
        /// <param name="applied_price"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        protected double iCCI(string symbol, int timeframe, int period, int applied_price, int shift)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the specified custom indicator and returns its value. The custom indicator must be compiled (*.EX4 file) and be in the terminal_directory\experts\indicators directory. 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="timeframe"></param>
        /// <param name="name"></param>
        /// <param name="dataPoint"></param>
        /// <param name="mode"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        protected double iCustom(string symbol, int timeframe, string name, object dataPoint, int mode, int shift)
        {
            return this.iCustomImpl(symbol, timeframe, name, mode, shift, dataPoint);
        }


        protected double iCustom(string symbol, int timeframe, string name, int mode, int shift)
        {
            return this.iCustomImpl(symbol, timeframe, name, mode, shift);
        }


        protected double iCustom(string symbol, int timeframe, string name,
                                 object dataPoint, object dataPoint2,
                                 int mode, int shift)
        {
            return this.iCustomImpl(symbol, timeframe, name, mode, shift);
        }

        protected double iCustom(string symbol, int timeframe, string name,
                                 object dataPoint, object dataPoint2, object dataPoint3,
                                 int mode, int shift)
        {
            return this.iCustomImpl(symbol, timeframe, name, mode, shift);
        }

        protected double iCustom(string symbol, int timeframe, string name,
                                 object dataPoint, object dataPoint2, object dataPoint3, object dataPoint4,
                                 int mode, int shift)
        {
            return this.iCustomImpl(symbol, timeframe, name, mode, shift);
        }

        protected double iCustom(string symbol, int timeframe, string name,
                                 object dataPoint, object dataPoint2, object dataPoint3, object dataPoint4, object dataPoint5,
                                 int mode, int shift)
        {
            return this.iCustomImpl(symbol, timeframe, name, mode, shift);
        }

        protected double iCustom(string symbol, int timeframe, string name,
                                 object dataPoint, object dataPoint2, object dataPoint3, object dataPoint4, object dataPoint5,
                                 object dataPoint6,
                                 int mode, int shift)
        {
            return this.iCustomImpl(symbol, timeframe, name, mode, shift);
        }

        protected double iCustom(string symbol, int timeframe, string name,
                                 object dataPoint, object dataPoint2, object dataPoint3, object dataPoint4, object dataPoint5,
                                 object dataPoint6, object dataPoint7,
                                 int mode, int shift)
        {
            return this.iCustomImpl(symbol, timeframe, name, mode, shift);
        }

        protected double iCustom(string symbol, int timeframe, string name,
                                 object dataPoint, object dataPoint2, object dataPoint3, object dataPoint4, object dataPoint5,
                                 object dataPoint6, object dataPoint7, object dataPoint8, object dataPoint9,
                                 int mode, int shift)
        {
            return this.iCustomImpl(symbol, timeframe, name, mode, shift);
        }

        protected double iCustomImpl(string symbol, int timeframe, string name, int mode, int shift,
                                     params object[] dataPoints)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the DeMarker indicator and returns its value. 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="timeframe"></param>
        /// <param name="period"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        protected double iDeMarker(string symbol, int timeframe, int period, int shift)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the Envelopes indicator and returns its value. 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="timeframe"></param>
        /// <param name="ma_period"></param>
        /// <param name="ma_method"></param>
        /// <param name="ma_shift"></param>
        /// <param name="applied_price"></param>
        /// <param name="deviation"></param>
        /// <param name="mode"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        protected double iEnvelopes(string symbol, int timeframe, int ma_period, int ma_method, int ma_shift,
                                    int applied_price, double deviation, int mode, int shift)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the Bill Williams' Accelerator/Decelerator oscillator. 
        /// </summary>
        /// <param name="symbol">Symbol name of the security on the data of which the indicator will be calculated. NULL means the current symbol.</param>
        /// <param name="timeframe">Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe.</param>
        /// <param name="shift">Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago).</param>
        /// <returns></returns>
        protected double iAC(string symbol, int timeframe, int shift)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the Movement directional index and returns its value. 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="timeframe"></param>
        /// <param name="period"></param>
        /// <param name="applied_price"></param>
        /// <param name="mode"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        protected double iADX(string symbol, int timeframe, int period, int applied_price, int mode, int shift)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the Bill Williams' Alligator and returns its value. 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="timeframe"></param>
        /// <param name="jaw_period"></param>
        /// <param name="jaw_shift"></param>
        /// <param name="teeth_period"></param>
        /// <param name="teeth_shift"></param>
        /// <param name="lips_period"></param>
        /// <param name="lips_shift"></param>
        /// <param name="ma_method"></param>
        /// <param name="applied_price"></param>
        /// <param name="mode"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        protected double iAlligator(string symbol, int timeframe, int jaw_period, int jaw_shift, int teeth_period,
                                    int teeth_shift, int lips_period, int lips_shift, int ma_method, int applied_price,
                                    int mode, int shift)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the Indicator of the average true range and returns its value. 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="timeframe"></param>
        /// <param name="period"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        protected double iATR(string symbol, int timeframe, int period, int shift)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the Bill Williams' Awesome oscillator and returns its value. 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="timeframe"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        protected double iAO(string symbol, int timeframe, int shift)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the Fractals and returns its value
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="timeframe"></param>
        /// <param name="mode"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        protected double iFractals(string symbol, int timeframe, int mode, int shift)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the Moving average indicator and returns its value. 
        /// </summary>
        /// <param name="symbol">Symbol the data of which should be used to calculate indicator. NULL means the current symbol.</param>
        /// <param name="timeframe">Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe</param>
        /// <param name="period">Averaging period for calculation.</param>
        /// <param name="ma_shift">MA shift. Indicators line offset relate to the chart by timeframe.</param>
        /// <param name="ma_method">MA method. It can be any of the Moving Average method enumeration value.</param>
        /// <param name="applied_price">Applied price. It can be any of Applied price enumeration values.</param>
        /// <param name="shift">Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago).</param>
        /// <returns>Moving average indicator</returns>
        protected double iMA(string symbol, int timeframe, int period, int ma_shift, int ma_method, int applied_price,
                             int shift)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the Moving averages convergence/divergence and returns its value. In the systems where OsMA 
        /// is called MACD Histogram, this indicator is displayed as two lines.
        /// In the Client Terminal, the Moving Average Convergence/Divergence is drawn as a histogram. 
        /// </summary>
        /// <param name="symbol">Symbol the data of which should be used to calculate indicator. NULL means the current symbol</param>
        /// <param name="timeframe">Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe.</param>
        /// <param name="fast_ema_period">Number of periods for fast moving average calculation.</param>
        /// <param name="slow_ema_period">Number of periods for slow moving average calculation.</param>
        /// <param name="signal_period">Number of periods for signal moving average calculation.</param>
        /// <param name="applied_price">Applied price. It can be any of Applied price enumeration values.</param>
        /// <param name="mode">Indicator line index. It can be any of the Indicators line identifiers enumeration value.</param>
        /// <param name="shift">Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago).</param>
        /// <returns></returns>
        protected double iMACD(string symbol, int timeframe, int fast_ema_period, int slow_ema_period, int signal_period,
                               int applied_price, int mode, int shift)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the Money flow index and returns its value. 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="timeframe"></param>
        /// <param name="period"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        protected double iMFI(string symbol, int timeframe, int period, int shift)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the Momentum indicator and returns its value. 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="timeframe"></param>
        /// <param name="period"></param>
        /// <param name="applied_price"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        protected double iMomentum(string symbol, int timeframe, int period, int applied_price, int shift)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the Moving Average of Oscillator and returns its value. Sometimes called MACD Histogram in some systems. 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="timeframe"></param>
        /// <param name="fast_ema_period"></param>
        /// <param name="slow_ema_period"></param>
        /// <param name="signal_period"></param>
        /// <param name="applied_price"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        protected double iOsMA(string symbol, int timeframe, int fast_ema_period, int slow_ema_period, int signal_period,
                               int applied_price, int shift)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the Relative strength index and returns its value. 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="timeframe"></param>
        /// <param name="period"></param>
        /// <param name="applied_price"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        protected double iRSI(string symbol, int timeframe, int period, int applied_price, int shift)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the Parabolic Stop and Reverse system and returns its value. 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="timeframe"></param>
        /// <param name="step"></param>
        /// <param name="maximum"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        protected double iSAR(string symbol, int timeframe, double step, double maximum, int shift)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the Stochastic oscillator and returns its value. 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="timeframe"></param>
        /// <param name="Kperiod"></param>
        /// <param name="Dperiod"></param>
        /// <param name="slowing"></param>
        /// <param name="method"></param>
        /// <param name="price_field"></param>
        /// <param name="mode"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        protected double iStochastic(string symbol, int timeframe, int Kperiod, int Dperiod, int slowing, int method,
                                     int price_field, int mode, int shift)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Calculates the Larry William's percent range indicator and returns its value. 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="timeframe"></param>
        /// <param name="period"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        protected double iWPR(string symbol, int timeframe, int period, int shift)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
