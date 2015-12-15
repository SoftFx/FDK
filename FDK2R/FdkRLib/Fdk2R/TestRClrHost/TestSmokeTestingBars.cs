using System;
using NUnit.Framework;
using RHost;

namespace TestRClrHost
{
    [TestFixture]
    public class TestSmokeTestingBars
    {
        [Test]
        public void TestBarsRange()
        {
            //Assert.AreEqual(0, FdkHelper.ConnectToFdk("tp.dev.soft-fx.eu", "100106", "123qwe123", ""));
            Assert.AreEqual(0, FdkHelper.ConnectToFdk("", "", "", ""));
            var time = DateTime.Now;
            var prevHour = time.AddDays(-1);

            var bars = FdkBars.ComputeBarsRangeTime("EURUSD", "Bid", "M1", prevHour, DateTime.Now, 10000);
            var highs = FdkBars.BarHighs(bars);
            var lows = FdkBars.BarLows(bars);
            var opens = FdkBars.BarOpens(bars);
            var volumes = FdkBars.BarVolumes(bars);
            var closes = FdkBars.BarCloses(bars);
            var froms = FdkBars.BarFroms(bars);
            var tos = FdkBars.BarTos(bars);
            FdkVars.Unregister(bars);

            FdkHelper.Disconnect();
        }

    }
}