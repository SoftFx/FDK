namespace SoftFX.Extended.Functional.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using SoftFX.Extended;

    [TestClass]
    public class DataTradeNetTests : TestBase
    {
        [TestMethod]
        public void CloseOrDeleteOrTradeRecords()
        {
            TestHelpers.Execute(this.CloseOrDeleteOrTradeRecords, Configuration.ConnectionBuilders(AccountType.Net, Configuration.ConnectionType.Trade));
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

        [TestMethod]
        public void SendMarketOrder()
        {
            TestHelpers.Execute(this.SendMarketOrder, Configuration.ConnectionBuilders(AccountType.Net, Configuration.ConnectionType.Trade));
        }

        void SendMarketOrder(ConnectionStringBuilder builder)
        {
            var connectionString = builder.ToString();
            this.dataTrade = new DataTrade(connectionString);

            this.dataTrade.Logon += this.OnLogon;
            this.dataTrade.Start();

            var status = this.logonEvent.WaitOne(LogonWaitingTimeout);
            Assert.IsTrue(status, "Timeout of logon event");
            var order = this.dataTrade.Server.SendOrderEx("EURUSD", TradeCommand.Market, TradeRecordSide.Buy, 1.1, 10000, 0, null, null, 0, null, "comment", null, null, 1000000);
            Assert.IsTrue(order.Price > 0, "Invalid order price = {0}", order.Price);

            CacheDontHavePositions();

            this.dataTrade.Logon -= this.OnLogon;
            this.dataTrade.Stop();
            this.dataTrade.Dispose();
        }

        [TestMethod]
        public void SendLimitOrder()
        {
            TestHelpers.Execute(this.SendLimitOrder, Configuration.ConnectionBuilders(AccountType.Gross, Configuration.ConnectionType.Trade));
        }

        void SendLimitOrder(ConnectionStringBuilder builder)
        {
            var connectionString = builder.ToString();
            this.dataTrade = new DataTrade(connectionString);

            this.dataTrade.Logon += this.OnLogon;
            this.dataTrade.Start();

            var status = this.logonEvent.WaitOne(LogonWaitingTimeout);
            Assert.IsTrue(status, "Timeout of logon event");
            const double price = 1.1;
            var order = this.dataTrade.Server.SendOrderEx("EURUSD", TradeCommand.Limit, TradeRecordSide.Buy, price, 10000, 0, null, null, 0, null, "comment", null, null, 1000000);

            Assert.IsTrue(order.Price == price, "Invalid order price = {0}", order.Price);
            this.dataTrade.Logon -= this.OnLogon;
            this.dataTrade.Stop();
            this.dataTrade.Dispose();
        }

        [TestMethod]
        public void SendStopOrder()
        {
            TestHelpers.Execute(this.SendStopOrder, Configuration.ConnectionBuilders(AccountType.Gross, Configuration.ConnectionType.Trade));
        }

        void SendStopOrder(ConnectionStringBuilder builder)
        {
            var connectionString = builder.ToString();
            this.dataTrade = new DataTrade(connectionString);

            this.dataTrade.Logon += this.OnLogon;
            this.dataTrade.Start();

            var status = this.logonEvent.WaitOne(LogonWaitingTimeout);
            Assert.IsTrue(status, "Timeout of logon event");
            const double price = 1.9;
            var order = this.dataTrade.Server.SendOrderEx("EURUSD", TradeCommand.Stop, TradeRecordSide.Buy, price, 10000, null, null, null, null, null, "comment", null, null, 1000000);
            Assert.IsTrue(order.Price == price, "Invalid order price = {0}", order.Price);
            this.dataTrade.Logon -= this.OnLogon;
            this.dataTrade.Stop();
            this.dataTrade.Dispose();
        }

        [TestMethod]
        public void ModifyLimitOrder()
        {
            TestHelpers.Execute(this.ModifyLimitOrder, Configuration.ConnectionBuilders(AccountType.Gross, Configuration.ConnectionType.Trade));
        }

        void ModifyLimitOrder(ConnectionStringBuilder builder)
        {
            var connectionString = builder.ToString();
            this.dataTrade = new DataTrade(connectionString);
            this.dataTrade.SynchOperationTimeout = 20000;
            this.dataTrade.Logon += this.OnLogon;
            this.dataTrade.Start();

            Boolean status = this.logonEvent.WaitOne(LogonWaitingTimeout);
            Assert.IsTrue(status, "Timeout of logon event");

            const Double price = 1.1;

            var start = DateTime.UtcNow;
            var order = this.dataTrade.Server.SendOrderEx("EURUSD", TradeCommand.Limit, TradeRecordSide.Buy, price, 10000, null, null, null, null, null, "comment", null, null, 1000000);
            var end = DateTime.UtcNow;
            var interval = (end - start);
            Console.WriteLine("Interval = {0}", interval);

            Assert.IsTrue(order.Price == price, "Invalid order price = {0}", order.Price);

            order.Modify(1.0, null, null, null, null, null, null, null);

            order.Delete();

            this.dataTrade.Logon -= this.OnLogon;
            this.dataTrade.Stop();
            this.dataTrade.Dispose();
        }
    }
}