using System;
using System.Threading;
using NUnit.Framework;
using RHost;

namespace TestRClrHost
{
    [TestFixture]
    public class TestRealTimeData
    {
        [Test]
        public void TestRealTimeQuotes()
        {
            Assert.AreEqual(0, FdkHelper.ConnectToFdk("", "", "", ""));
            var eventId = FdkRealTime.MonitorSymbol("EURUSD", 3);


            //5 seconds
            Thread.Sleep(new TimeSpan(0,0,15));
            var eventData = FdkRealTime.GetEventById(eventId);

            Assert.AreNotEqual(0, eventData.LastEventData.Tick.Bids.Length, "Some feed events should work");

            var snapshot = FdkRealTime.SnapshotMonitoredSymbol(eventId);
			var bid = FdkRealTime.QuoteRealTimeAskPrice(snapshot);
			var ask = FdkRealTime.QuoteRealTimeAskVolume(snapshot);
            var createTime = FdkRealTime.QuoteRealTimeBidPrice(snapshot);
            var spread = FdkRealTime.QuoteRealTimeBidVolume(snapshot);
            
			FdkVars.Unregister (snapshot);

			FdkRealTime.RemoveEvent (eventId);

            var bidLength = bid.Length;
            Assert.AreEqual(bidLength, ask.Length);
            Assert.AreEqual(bidLength, createTime.Length);
            Assert.AreEqual(bidLength, spread.Length);

			FdkHelper.Disconnect();
        }

    }
}