using System;
using System.Linq;
using log4net;
using SoftFX.Extended;

namespace RHost
{
	public class FdkQuotes
	{
		static readonly ILog Log = LogManager.GetLogger(typeof(FdkQuotes));

		public static string ComputeQuoteHistory(string symbol, DateTime startTime, DateTime endTime, double depthDbl)
		{
			try
			{
				var depth = (int)depthDbl;
				Log.InfoFormat("FdkQuotes.ComputeQuoteHistory( symbol: {0}, startTime: {1}, endTime: {2}, level: {3})",
					symbol, startTime, endTime, depthDbl);
				
				var quotesData = CalculateHistoryForSymbolArray(symbol, startTime, endTime, depth);
				var quoteHistory = FdkVars.RegisterVariable(quotesData, "quotes");
            	return quoteHistory;
			}
			catch (Exception ex)
			{
				Log.Error(ex);
				throw;
			}
        }

        public static Quote[] CalculateHistoryForSymbolArray(string symbol, DateTime startTime, DateTime endTime, int depth)
        {
            return FdkHelper.Wrapper.ConnectLogic.Storage.Online.GetQuotes(symbol, startTime, endTime, depth);
        }

        public static double[] QuotesAsk(string bars)
        {
            var quotes = FdkVars.GetValue<Quote[]>(bars);
            return QuoteArrayAsk(quotes);
        }

        public static double[] QuotesBid(string bars)
        {
            var quotes = FdkVars.GetValue<Quote[]>(bars);
            return QuoteArrayBid(quotes);
        }
        public static DateTime[] QuotesCreatingTime(string bars)
        {
            var quotes = FdkVars.GetValue<Quote[]>(bars);

            return QuoteArrayCreateTime(quotes);
        }

        public static double[] QuotesSpread(string bars)
        {
            var quotes = FdkVars.GetValue<Quote[]>(bars);
            return QuoteArraySpread(quotes);
        }

        internal static double[] QuoteArrayBid(Quote[] quotes)
        {
            return quotes.SelectToArray(b => b.HasBid ? b.Bid : -1);
        }


        internal static double[] QuoteArrayAsk(Quote[] quotes)
        {
            return quotes.SelectToArray(b => b.HasAsk ? b.Ask : -1);
        }

        internal static DateTime[] QuoteArrayCreateTime(Quote[] quotes)
        {
            var timesAsEpoch = quotes.SelectToArray(b => b.CreatingTime);
            return timesAsEpoch;
        }

        internal static double[] QuoteArraySpread(Quote[] quotes)
        {
            return quotes.SelectToArray(b => b.Spread);
        }
    }
}