namespace SoftFX.Extended.Storage
{
    using System;

    /// <summary>
    /// Define storage methods.
    /// </summary>
    public interface IStorage
    {
        /// <summary>
        /// The method returns quotes for required date time interval.
        /// All quotes should be in the following time range: Min(startTime, endTime) &lt;= Quote.CreatingTime &lt;= Max(startTime, endTime)
        /// </summary>
        /// <param name="symbol">A required symbol; can not be null.</param>
        /// <param name="startTime">A start time of ticks enumeration.</param>
        /// <param name="endTime">A end time of ticks enumeration.</param>
        /// <param name="depth">
        /// 0 - full book
        /// (1..n) - restricted book
        /// </param>
        /// <returns>Can not be null</returns>
        Quote[] GetQuotes(string symbol, DateTime startTime, DateTime endTime, int depth);

        /// <summary>
        /// The method returns quotes for required count.
        /// </summary>
        /// <param name="symbol">A required symbol; can not be null.</param>
        /// <param name="startTime">A start time of ticks enumeration.</param>
        /// <param name="quotesNumber">Requested quotes number; positive value means forward enumeration; negative value means backward enumeration.</param>
        /// <param name="depth">
        /// 0 - full book
        /// (1..n) - restricted book
        /// </param>
        /// <returns>Can not be null</returns>
        Quote[] GetQuotes(string symbol, DateTime startTime, int quotesNumber, int depth);

        /// <summary>
        /// Gets Bars from storage.
        /// </summary>
        /// <param name="symbol">A required symbol; can not be null.</param>
        /// <param name="priceType"></param>
        /// <param name="period"></param>
        /// <param name="startTime">A start time of ticks enumeration.</param>
        /// <param name="endTime">A end time of ticks enumeration.</param>
        /// <returns>Returns Bars array.</returns>
        Bar[] GetBars(string symbol, PriceType priceType, BarPeriod period, DateTime startTime, DateTime endTime);

        /// <summary>
        /// Gets Bars from storage.
        /// </summary>
        /// <param name="symbol">A required symbol; can not be null.</param>
        /// <param name="priceType"></param>
        /// <param name="period"></param>
        /// <param name="startTime">A start time of ticks enumeration.</param>
        /// <param name="barsNumber">Requested bars number; positive value means forward enumeration; negative value means backward enumeration.</param>
        /// <returns>Returns Bars array.</returns>
        Bar[] GetBars(string symbol, PriceType priceType, BarPeriod period, DateTime startTime, int barsNumber);

        /// <summary>
        /// Gets PairBars from storage.
        /// </summary>
        /// <param name="symbol">A required symbol; can not be null.</param>
        /// <param name="period"></param>
        /// <param name="startTime">A start time of ticks enumeration.</param>
        /// <param name="endTime">A end time of ticks enumeration.</param>
        /// <returns>Returns PairBars array.</returns>
        PairBar[] GetPairBars(string symbol, BarPeriod period, DateTime startTime, DateTime endTime);

        /// <summary>
        /// Gets PairBars from storage.
        /// </summary>
        /// <param name="symbol">A required symbol; can not be null.</param>
        /// <param name="period"></param>
        /// <param name="startTime">A start time of ticks enumeration.</param>
        /// <param name="barsNumber">Requested bars number; positive value means forward enumeration; negative value means backward enumeration.</param>
        /// <returns>Returns PairBars array.</returns>
        PairBar[] GetPairBars(string symbol, BarPeriod period, DateTime startTime, int barsNumber);

        /// <summary>
        /// Gets quotes information.
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="depth"></param>
        /// <returns>Returns quotes information.</returns>
        HistoryInfo GetQuotesInfo(string symbol, int depth);

        /// <summary>
        /// Gets bars information.
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="priceType"></param>
        /// <param name="period"></param>
        /// <returns>Return bars information.</returns>
        HistoryInfo GetBarsInfo(string symbol, PriceType priceType, BarPeriod period);
    }
}
