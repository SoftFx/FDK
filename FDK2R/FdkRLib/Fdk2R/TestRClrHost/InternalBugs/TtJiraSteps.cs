using System;
using System.Linq;
using NUnit.Framework;
using RHost;

namespace TestRClrHost.InternalBugs
{
    [TestFixture] 
    public class TtJiraSteps
    {
        [Test]
        public void TtJira221Steps()
        {
            Assert.AreEqual(0, FdkHelper.ConnectToFdk("", "", "", @"c:\FdkCaches\Cache1"));
            var endTime = DateTime.Now;
            var startTime = endTime.AddSeconds(-0.1);

            var quotes = FdkQuotes.ComputeQuoteHistory("EURUSD_Rl", startTime, endTime, 1);

            FdkVars.Unregister(quotes);
            FdkHelper.Disconnect();
        }

        [Test]
        public void TtJira227Steps()
        {
            Assert.AreEqual(0, FdkHelper.ConnectToFdk("tp.st.soft-fx.eu", "100000", "123321", @"c:\FdkCaches\Cache1"));
            var endTime = DateTime.Now;
            var startTime = endTime.AddSeconds(-0.1);

            var quotes = FdkQuotes.ComputeQuoteHistory("EURUSD_Rl", startTime, endTime, 1);

            FdkVars.Unregister(quotes);
            FdkHelper.Disconnect();
        }


        [Test]
        public void TtJira230Steps()
        {
            Assert.AreEqual(0, FdkHelper.ConnectToFdk("tp.st.soft-fx.eu", "100064", "123qwe!", ""));

            var prevNow = new DateTime(2015, 7, 7, 18, 30, 00);
            var now = new DateTime(2015, 7, 7, 19, 00, 00);
            var varPairBars = FdkBarPairs.ComputeGetPairBars("#SPX", "M30", prevNow, now, 10000);

            FdkVars.Unregister(varPairBars);
            FdkHelper.Disconnect();
        }

        [Test]
        public void TtJira230StepsB()
        {
            Assert.AreEqual(0, FdkHelper.ConnectToFdk("", "", "", ""));

            var prevNow = new DateTime(2015, 7, 7, 18, 30, 00);
            var now = new DateTime(2015, 7, 7, 19, 00, 00);
            FdkBarPairs.ComputeGetPairBars("#SPX", "M30", prevNow, now, 0);

            //RHost.FdkBarPairs

        }
        [Test]
        public void TtJira244()
        {
            Assert.AreEqual(0, FdkHelper.ConnectToFdk("", "", "", ""));

            var prevNow = new DateTime(2015, 7, 30, 8, 30, 00);
            var now = new DateTime(2015, 7, 30, 9, 00, 0);
            var varName = FdkBarPairs.ComputeGetPairBars("EURUSD", "M30", prevNow, now, 0);

            FdkVars.Unregister(varName);
            //RHost.FdkBarPairs

        }
    }
}
