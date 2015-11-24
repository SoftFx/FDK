using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using SoftFX.Extended;

namespace RHost
{
	struct QuoteLevel2Data
	{
		public double AsksPrice { get; set; }
		public double AskVolume { get; set; }
		public double BidPrice { get; set; }
		public double BidVolume { get; set; }
		public DateTime CreateTime { get; set; }
		public double IndexOrder { get; set; }
		public int Level { get; set; }

		public override string ToString()
		{
			return string.Format("[QuoteLevel2Data AsksPrice={0}, BidPrice={1}]", AsksPrice, BidPrice);
		}
	}

	public class FdkLevel2
	{
		static readonly ILog Log = LogManager.GetLogger(typeof(FdkLevel2));
		/// <summary>
		/// Get quote packed 
		/// </summary>
		/// <param name="symbol">Symbol to get quotes on</param>
		/// <param name="startTime"></param>
		/// <param name="endTime"></param>
		/// <param name="levelDbl"></param>
		/// <returns></returns>
        public static string GetQuotePacked(string symbol, DateTime startTime, DateTime endTime, double levelDbl = 2)
		{
			try
			{
				var level = (int) levelDbl;

				Log.InfoFormat("FdkLevel2.GetQuotePacked( symbol: {0}, startTime: {1}, endTime: {2}, level: {3})",
					symbol, startTime, endTime, levelDbl);

				Quote[] quotesData = FdkQuotes.CalculateHistoryForSymbolArray(symbol, startTime, endTime, level);
				var quoteLevel2Data = BuildQuoteMultiLevelData(quotesData, level);

				var quoteHistory = FdkVars.RegisterVariable(quoteLevel2Data, "quotesL2");
            	return quoteHistory;
			}
			catch (Exception ex)
			{
				Log.Error(ex);
				throw;
			}
        }

        static readonly QuoteEntry NullQuote = new QuoteEntry(0,0);

		static QuoteLevel2Data[] BuildQuoteMultiLevelData(Quote[] quotesData, int depth)
		{
			var itemsToAdd = new List<QuoteLevel2Data>(capacity: quotesData.Length*depth);
			var prevTime = new DateTime(1970, 1, 1);
			var indexOrder = 0;
			foreach (var quote in quotesData) 
            {
				if (prevTime == quote.CreatingTime) 
                {
					indexOrder++;
				} else 
                {
					indexOrder = 0;
				}
				var timeSpan = quote.CreatingTime.Subtract(prevTime).TotalMilliseconds;
                var maxLength = Math.Max(quote.Asks.Length, quote.Bids.Length);
				
				for (var index = 0; index < depth; index++) {
                    var quoteEntryAsk = index < maxLength && index<quote.Asks.Length ? quote.Asks[index] : NullQuote;
                    var quoteEntryBid = index < maxLength && index < quote.Bids.Length ? quote.Bids[index] : NullQuote;
                    if (quoteEntryAsk.Price.Equals(NullQuote.Price) && quoteEntryBid.Price.Equals(NullQuote.Price))
                        continue;
					var newQuoteL2Data = new QuoteLevel2Data() {
						AskVolume = quoteEntryAsk.Volume,
						AsksPrice = quoteEntryAsk.Price,
						BidVolume = quoteEntryBid.Volume,
						BidPrice = quoteEntryBid.Price,
						CreateTime = quote.CreatingTime,
						IndexOrder = timeSpan + indexOrder / 100.0,
						Level = index+1
					};
					itemsToAdd.Add(newQuoteL2Data);
				}
			}
			QuoteLevel2Data[] quoteLevel2Data = itemsToAdd.ToArray();
			return quoteLevel2Data;
		}
        public static DateTime[] QuotesCreateTime(string bars)
        {
            var quotes = FdkVars.GetValue<QuoteLevel2Data[]>(bars);
            return quotes.SelectToArray(ql2 => ql2.CreateTime.AddUtc());
        }

        public static double[] QuotesVolumeAsk(string bars)
        {
            var quotes = FdkVars.GetValue<QuoteLevel2Data[]>(bars);
            return quotes.SelectToArray(q =>ToNonZeroValue(q.AskVolume));
        }
        public static double[] QuotesVolumeBid(string bars)
        {
            var quotes = FdkVars.GetValue<QuoteLevel2Data[]>(bars);
            return quotes.SelectToArray(q=> ToNonZeroValue(q.BidVolume));
        }

        public static double[] QuotesPriceAsk(string bars)
        {
            var quotes = FdkVars.GetValue<QuoteLevel2Data[]>(bars);
            return quotes.SelectToArray(q => ToNonZeroValue(q.AsksPrice));
        }

        public static double[] QuotesPriceBid(string bars)
        {
            var quotes = FdkVars.GetValue<QuoteLevel2Data[]>(bars);

            return quotes.SelectToArray(q => ToNonZeroValue(q.BidPrice));
        }
 
        private static double ToNonZeroValue(double bidPrice)
        {
            return bidPrice == 0.0 ? double.NaN : bidPrice;
        }

        public static double[] QuotesIndex(string bars)
        {
            var quotes = FdkVars.GetValue<QuoteLevel2Data[]>(bars);

            return quotes.SelectToArray(q => q.IndexOrder);
        }

        public static double[] QuotesLevel(string bars)
        {
            var quotes = FdkVars.GetValue<QuoteLevel2Data[]>(bars);

            return quotes.SelectToArray(q => (double)q.Level);
        }
    }
}