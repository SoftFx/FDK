using System;
using NUnit.Framework;
using RHost;

namespace TestRClrHost
{
    [TestFixture]
    public class TestSmokeTradeReports
    {

        [Test]
        public void TestGetTradeRecordsAll()
        {
            //Assert.AreEqual(0, FdkHelper.ConnectToFdk("tp.st.soft-fx.eu", "100000", "123321", ""));
			Assert.AreEqual(0, FdkHelper.ConnectToFdk("localhost", "100001", "123qwe!", ""));
            var time = DateTime.Now;
            var prevTime = time.AddHours(-1);
            var bars = FdkTradeReports.GetTradeTransactionReportAll();
            var comission = FdkTradeReports.GetTradeAgentCommission(bars);
            FdkVars.Unregister(bars);
        }

        [Test]
        public void TestDualConnect()
        {
            Assert.AreEqual(0, FdkHelper.ConnectToFdk("tp.st.soft-fx.eu", "100033", "123qwe!", ""));
            //Assert.AreEqual(0, FdkHelper.ConnectToFdk("", "", "", ""));
            var time = DateTime.Now;
            var prevTime = time.AddHours(-1);
            var bars = FdkTradeReports.GetTradeTransactionReportAll();
            var comission = FdkTradeReports.GetTradeAgentCommission(bars);
            FdkVars.Unregister(bars);
            FdkHelper.Disconnect();

            Assert.AreEqual(0, FdkHelper.ConnectToFdk("tp.st.soft-fx.eu", "100055", "123qwe!", ""));
            var bars2 = FdkTradeReports.GetTradeTransactionReportAll();
            var comission2 = FdkTradeReports.GetTradeAgentCommission(bars2);
            FdkVars.Unregister(bars2);
            FdkHelper.Disconnect();
        }
        
    }

    
}