using System;
using NUnit.Framework;
using RHost;
using SoftFX.Extended;
using SoftFX.Extended.Reports;
using System.Threading;


namespace TestRClrHost
{
	[TestFixture]
	public class WideSpreadInQuotesL2
	{
		[Test]
		public void TestQuotesLevel2WideSpreadLastMinute()
		{
			//Assert.AreEqual(0, FdkHelper.ConnectToFdk("tp.dev.soft-fx.eu", "100106", "123qwe123", ""));
			Assert.AreEqual(0, FdkHelper.ConnectToFdk("", "", "", @"c:\FdkCaches\Cache1"));
			var time = DateTime.UtcNow;
			var prevHour = time.AddMinutes(-1);
			var quotes = FdkLevel2.GetQuotePacked("AUDUSD", prevHour, time, 2);
			var volumesAsk = FdkLevel2.QuotesVolumeBid(quotes);
			var volumesBid = FdkLevel2.QuotesVolumeAsk(quotes);
			Assert.AreNotEqual(0, volumesAsk.Length);
			Assert.AreNotEqual(0, volumesBid.Length);
			FdkVars.Unregister(quotes);
		}

		[Test]
		public void TestQuotesLevel2WideSpreadLastSeconds()
		{
			//Assert.AreEqual(0, FdkHelper.ConnectToFdk("tp.dev.soft-fx.eu", "100106", "123qwe123", ""));
			Assert.AreEqual(0, FdkHelper.ConnectToFdk("", "", "", @"c:\FdkCaches\Cache1"));
			var time = DateTime.UtcNow;
			var prevHour = time.AddMinutes(-1);
			var quotes = FdkLevel2.GetQuotePacked("AUDUSD", prevHour, time, 1);
			var volumesAsk = FdkLevel2.QuotesPriceAsk(quotes);
			var volumesBid = FdkLevel2.QuotesPriceBid(quotes);
			
			
			var quotes2 = FdkLevel2.GetQuotePacked("AUDUSD", prevHour, time, 2);
			var volumesAsk2 = FdkLevel2.QuotesPriceAsk(quotes2);
			var volumesBid2 = FdkLevel2.QuotesPriceBid(quotes2);
			
			Assert.AreNotEqual(0, volumesAsk.Length);
			Assert.AreNotEqual(0, volumesBid.Length);
			FdkVars.Unregister(quotes);
		}

		[Test]
		public void TestTradeReports()
		{
            Assert.AreEqual(0, FdkHelper.ConnectToFdk("ttdemo.fxopen.com", "100106", "123qwe123", ""));

			var _trader = FdkTradeReports.Trade;
			StreamIterator<TradeTransactionReport> tradeTransactions = _trader.Server.GetTradeTransactionReports(TimeDirection.Forward, true, null, null, 5000);

			for (; !tradeTransactions.EndOfStream; tradeTransactions.Next())
			{ 
				TradeTransactionReport ttr = tradeTransactions.Item; 
			}

		}
        [Test]
        public void TestTradeReports2()
        {
			Library.Path = "<FRE>";
            Assert.AreEqual(0, FdkHelper.ConnectToFdk("ttdemo.fxopen.com", "50001933", "123123", ""));

            var _trader = FdkTradeReports.Trade;
            var startTime = new DateTime(2015,05, 1, 0,0, 0);
            var endTime = new DateTime(2015,07, 30, 0,0, 0);
            StreamIterator<TradeTransactionReport> tradeTransactions = _trader.Server.GetTradeTransactionReports(TimeDirection.Forward, true, 
                startTime, endTime, 50);

            for (; !tradeTransactions.EndOfStream; tradeTransactions.Next())
            {
                TradeTransactionReport ttr = tradeTransactions.Item;
            }

        }
	}
}


