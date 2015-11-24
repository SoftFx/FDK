namespace SoftFX.Extended.Functional.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using SoftFX.Extended;

    [TestClass]
    public class ScriptsTests : TestBase
    {
        [TestMethod]
        public void CloseOrDeleteOrTradeRecords()
        {
            TestHelpers.Execute(this.CloseOrDeleteOrTradeRecords, Configuration.DataTradeGrossConnectionBuilders);
        }

        void CloseOrDeleteOrTradeRecords(ConnectionStringBuilder builder)
        {
            var connectionString = builder.ToString();
            this.dataTrade = new DataTrade(connectionString);

            this.dataTrade.Logon += this.OnLogon;
            this.dataTrade.Start();

            var status = this.logonEvent.WaitOne(LogonWaitingTimeout);
            Assert.IsTrue(status, "Timeout of logon event");

            this.dataTrade.SynchOperationTimeout = 20000;

            var count = 0;

            for (var records = this.dataTrade.Server.GetTradeRecords(); records.Length > 0; records = this.dataTrade.Server.GetTradeRecords())
            {
                foreach (var element in records)
                {
                    if (element.IsPendingOrder)
                        element.Delete();
                    else
                        element.Close();

                    ++count;
                }
            }

            Console.WriteLine("{0} trade records have been deleted/closed", count);
        }
    }
}
