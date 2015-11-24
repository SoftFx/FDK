using NUnit.Framework;
using RHost;

namespace TestRClrHost
{
    [TestFixture]
    public class TestSmokeTestingHistoryInfo
    {
        [Test]
        public void TestGetQuotesInfo()
        {
            Assert.AreEqual(0, FdkHelper.ConnectToFdk("", "", "", ""));
            
            var bars = FdkBars.ComputeGetQuotesInfo("EURUSD", 3);
            
            FdkHelper.Disconnect();
        }

        [Test]
        public void TestGetBarsInfo()
        {
            Assert.AreEqual(0, FdkHelper.ConnectToFdk("", "", "", ""));

            var bars = FdkBars.ComputeGetBarsInfo("EURUSD", "Ask", "M1");

            FdkHelper.Disconnect();
        }

    }
}