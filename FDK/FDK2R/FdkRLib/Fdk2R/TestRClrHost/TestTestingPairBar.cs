using System;
using NUnit.Framework;
using RHost;

namespace TestRClrHost
{
    [TestFixture]
    public class TestTestingPairBar
    {
        [Test]
        public void TestBarPairs()
        {
            //Assert.AreEqual(0, FdkHelper.ConnectToFdk("tp.dev.soft-fx.eu", "100106", "123qwe123", ""));
            Assert.AreEqual(0, FdkHelper.ConnectToFdk("", "", "", @"C:\FdkCaches\Cache3"));
            var time = DateTime.Now;
            var prevHour = time.AddYears(-10);

            var timeDouble = FdkHelper.GetCreatedEpoch(time);

            var bars = FdkBarPairs.ComputeGetPairBars("EURUSD", "M1", prevHour, time,  10000);
            var askhighs = FdkBarPairs.GetBarsAskHigh(bars);
            var asklows = FdkBarPairs.GetBarsAskLow(bars);
            var askopen = FdkBarPairs.GetBarsAskOpen(bars);
            var askClose = FdkBarPairs.GetBarsAskClose(bars);
            var askVolume = FdkBarPairs.GetBarsAskVolume(bars);

            FdkBarPairs.GetBarsAskFrom(bars);
            

            var bidhighs = FdkBarPairs.GetBarsBidHigh(bars);
            var bidlows = FdkBarPairs.GetBarsBidLow(bars);
            var bidOpen = FdkBarPairs.GetBarsBidOpen(bars);
            var bidClose = FdkBarPairs.GetBarsBidClose(bars);
            var bidVolume = FdkBarPairs.GetBarsBidVolume(bars);
            
            FdkVars.Unregister(bars);
            FdkHelper.Disconnect();
        }
    }
}