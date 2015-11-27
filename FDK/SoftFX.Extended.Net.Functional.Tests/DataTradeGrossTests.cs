namespace SoftFX.Extended.Functional.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Threading;
    using SoftFX.Extended;
    using SoftFX.Extended.Events;
    using Reports;

    [TestClass]
    public class DataTradeGrossTests
    {
        #region Members

        readonly AutoResetEvent logonEvent = new AutoResetEvent(false);
        readonly AutoResetEvent accountInfoEvent = new AutoResetEvent(false);
        readonly AutoResetEvent tickEvent = new AutoResetEvent(false);
        readonly AutoResetEvent logoutEvent = new AutoResetEvent(false);

        const int LogonWaitingTimeout = 10000;
        const int TickWaitingTimeout = 10000;
        const int LogoutWaitingTimeout = 10000;

        LogoutReason logoutReason;
        DataTrade dataTrade;

        #region Event Handlers

        void OnLogon(Object sender, LogonEventArgs e)
        {
            this.logonEvent.Set();
        }

        void OnLogout(Object sender, LogoutEventArgs e)
        {
            this.logoutReason = e.Reason;
            this.logoutEvent.Set();
        }

        void OnTick(Object sender, TickEventArgs e)
        {
            this.tickEvent.Set();
        }
        void DataTrade_AccountInfo(object sender, AccountInfoEventArgs e)
        {
            this.accountInfoEvent.Set();
        }


        #endregion

        #endregion

        #region Initialize and Cleanup

        [TestInitialize]
        public void Initialize()
        {
            this.logonEvent.WaitOne(0);
            this.tickEvent.WaitOne(0);
            this.logoutEvent.WaitOne(0);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (this.dataTrade != null)
            {
                if (this.dataTrade.IsStarted)
                {
                    this.dataTrade.Stop();
                }
                this.dataTrade.Dispose();
                this.dataTrade = null;
            }
        }

        #endregion

        [TestMethod]
        public void CreateAndDestroy()
        {
            TestHelpers.Execute(this.CreateAndDestroy, Configuration.DataTradeGrossConnectionBuilders);
        }

        void CreateAndDestroy(ConnectionStringBuilder builder)
        {
            var connectionString = builder.ToString();
            this.dataTrade = new DataTrade(connectionString);
            this.dataTrade.Dispose();
        }

        [TestMethod]
        public void SendMarketOrder()
        {
            TestHelpers.Execute(this.SendMarketOrder, Configuration.DataTradeGrossConnectionBuilders);
        }

        void SendMarketOrder(ConnectionStringBuilder builder)
        {
            var connectionString = builder.ToString();
            this.dataTrade = new DataTrade(connectionString);

            this.dataTrade.Logon += this.OnLogon;
            this.dataTrade.Start();

            var status = this.logonEvent.WaitOne(LogonWaitingTimeout);
            Assert.IsTrue(status, "Timeout of logon event");
            var order = this.dataTrade.Server.SendOrderEx("EURUSD", TradeCommand.Market, TradeRecordSide.Buy, 1.1, 10000, 0, 0, null, "comment", 1000000);
            Assert.IsTrue(order.Price > 0, "Invalid order price = {0}", order.Price);
            this.dataTrade.Logon -= this.OnLogon;
            this.dataTrade.Stop();
            this.dataTrade.Dispose();
        }

        [TestMethod]
        public void SendLimitOrder()
        {
            TestHelpers.Execute(this.SendLimitOrder, Configuration.DataTradeGrossConnectionBuilders);
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
            var order = this.dataTrade.Server.SendOrderEx("EURUSD", TradeCommand.Limit, TradeRecordSide.Buy, price, 10000, 0, 0, null, "comment", 1000000);

            Assert.IsTrue(order.Price == price, "Invalid order price = {0}", order.Price);
            this.dataTrade.Logon -= this.OnLogon;
            this.dataTrade.Stop();
            this.dataTrade.Dispose();
        }

        [TestMethod]
        public void SendStopOrder()
        {
            TestHelpers.Execute(this.SendStopOrder, Configuration.DataTradeGrossConnectionBuilders);
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
            var order = this.dataTrade.Server.SendOrderEx("EURUSD", TradeCommand.Stop, TradeRecordSide.Buy, price, 10000, 0, 0, null, "comment", 1000000);
            Assert.IsTrue(order.Price == price, "Invalid order price = {0}", order.Price);
            this.dataTrade.Logon -= OnLogon;
            this.dataTrade.Stop();
            this.dataTrade.Dispose();
        }

        [TestMethod]
        public void ModifyLimitOrder()
        {
            TestHelpers.Execute(this.ModifyLimitOrder, Configuration.DataTradeGrossConnectionBuilders);
        }

        void ModifyLimitOrder(ConnectionStringBuilder builder)
        {
            var connectionString = builder.ToString();
            this.dataTrade = new DataTrade(connectionString);

            this.dataTrade.Logon += this.OnLogon;
            this.dataTrade.Start();

            var status = this.logonEvent.WaitOne(LogonWaitingTimeout);
            Assert.IsTrue(status, "Timeout of logon event");

            //const double activationPrice = 1.1;
            //const double newActivationPrice = 1.0;
            //const double newStopLoss = 0.8;
            //const double newTakeProfit = 1.3;
            var newExpirationTime = DateTime.UtcNow.AddHours(1);

            var order = this.dataTrade.Server.SendOrderEx("EURUSD", TradeCommand.Limit, TradeRecordSide.Buy, 1.1, 10000, null, null, null, "comment", 1000000);
/*
            //this.ModifyLimitOrder(order, activationPrice, null, null, null, null);
            this.ModifyLimitOrder(order, activationPrice, null, null, null, newExpirationTime);

            //this.ModifyLimitOrder(order, activationPrice, null, null, newTakeProfit, null);
            this.ModifyLimitOrder(order, activationPrice, null, null, newTakeProfit, newExpirationTime);


            //this.ModifyLimitOrder(order, activationPrice, null, newStopLoss, null, null);
            this.ModifyLimitOrder(order, activationPrice, null, newStopLoss, null, newExpirationTime);

            //this.ModifyLimitOrder(order, activationPrice, null, newStopLoss, newTakeProfit, null);
            this.ModifyLimitOrder(order, activationPrice, null, newStopLoss, newTakeProfit, newExpirationTime);



            //this.ModifyLimitOrder(order, activationPrice, newActivationPrice, null, null, null);
            this.ModifyLimitOrder(order, activationPrice, newActivationPrice, null, null, newExpirationTime);

            //this.ModifyLimitOrder(order, activationPrice, newActivationPrice, null, newTakeProfit, null);
            this.ModifyLimitOrder(order, activationPrice, newActivationPrice, null, newTakeProfit, newExpirationTime);

            //this.ModifyLimitOrder(order, activationPrice, newActivationPrice, newStopLoss, null, null);
            this.ModifyLimitOrder(order, activationPrice, newActivationPrice, newStopLoss, null, newExpirationTime);

            //this.ModifyLimitOrder(order, activationPrice, newActivationPrice, newStopLoss, newTakeProfit, null);
            this.ModifyLimitOrder(order, activationPrice, newActivationPrice, newStopLoss, newTakeProfit, newExpirationTime);
            */
            order.Delete();

            this.dataTrade.Logon -= this.OnLogon;
            this.dataTrade.Stop();
            this.dataTrade.Dispose();
        }

        void ModifyLimitOrder(TradeRecord order, double initialActivationPrice, double? newActivationPrice, double? newStopLoss, double? newTakeProfit, DateTime? newExpirationTime)
        {
            var initial = ModifyLimitOrder(order, initialActivationPrice, null, null, null); // reset all


            // check all
            Assert.IsTrue(initial.Price == initialActivationPrice);
            //Assert.IsNull(initial.StopLoss);
            //Assert.IsNull(initial.TakeProfit);
            //Assert.IsNull(initial.Expiration);

            // modify
            var modified = this.ModifyLimitOrder(initial, newActivationPrice, newStopLoss, newTakeProfit, newExpirationTime);
            
            // check
            if (newActivationPrice == null)
            {
                Assert.IsTrue(initialActivationPrice == modified.Price);
            }
            else
            {
                Assert.IsTrue(newActivationPrice == modified.Price);
            }

            Assert.IsTrue(newStopLoss == modified.StopLoss);
            Assert.IsTrue(newTakeProfit == modified.TakeProfit);
            if (newExpirationTime == null)
            {
                Assert.IsNull(modified.Expiration);
            }
            else
            {
                Assert.IsNotNull(modified.Expiration);
                var interval = (DateTime)modified.Expiration - (DateTime)newExpirationTime;
                Assert.IsTrue(Math.Abs(interval.TotalMilliseconds) < 1000);
            }
        }

        TradeRecord ModifyLimitOrder(TradeRecord initial, double? newActivationPrice, double? newStopLoss, double? newTakeProfit, DateTime? newExpirationTime)
        {
            var start = DateTime.UtcNow;
            TradeRecord result = null; 
            try
            {
                result = initial.Modify(newActivationPrice, newStopLoss, newTakeProfit, newExpirationTime);
            }
            catch
            {
                TestBase.Output("(price = {0}; sl = {1}; tp = {2}; expiration = {3}) = {4}", newActivationPrice, newStopLoss, newTakeProfit, newExpirationTime, "exception");
                throw;
            }
            
            var finish = DateTime.UtcNow;
            var interval = (finish - start);

            TestBase.Output("(price = {0}; sl = {1}; tp = {2}; expiration = {3}) = {4}", newActivationPrice, newStopLoss, newTakeProfit, newExpirationTime, interval);

            return result;
        }

        [TestMethod]
        public void ModifyPosition()
        {
            TestHelpers.Execute(this.ModifyPosition, Configuration.DataTradeGrossConnectionBuilders);
        }

        void ModifyPosition(ConnectionStringBuilder builder)
        {
            var connectionString = builder.ToString();
            this.dataTrade = new DataTrade(connectionString);

            this.dataTrade.Logon += OnLogon;
            this.dataTrade.AccountInfo += DataTrade_AccountInfo;
            this.dataTrade.Start();

            bool status = this.logonEvent.WaitOne(LogonWaitingTimeout);
            status &= this.accountInfoEvent.WaitOne(LogonWaitingTimeout);
            
            Assert.IsTrue(status, "Timeout of logon event");

            var start = DateTime.UtcNow;
            TradeRecord order = this.dataTrade.Server.SendOrderEx("EURUSD", TradeCommand.Market, TradeRecordSide.Buy, 0, 10000, null, null, null, "comment", 1000000);
            DateTime end = DateTime.UtcNow;
            TimeSpan interval = (end - start);
            Console.WriteLine("Interval = {0}", interval);

            var modified = order.Modify(null, 1.0, null, null);

            order.Close();

            this.dataTrade.Logon -= this.OnLogon;
            this.dataTrade.AccountInfo -= DataTrade_AccountInfo;
            this.dataTrade.Stop();
            this.dataTrade.Dispose();
        }

        [TestMethod]
        public void TradeReports()
        {
            TestHelpers.Execute(this.TradeReports, Configuration.DataTradeGrossConnectionBuilders);
        }

        void TradeReports(ConnectionStringBuilder builder)
        {
            var connectionString = builder.ToString();
            this.dataTrade = new DataTrade(connectionString);

            this.dataTrade.Logon += this.OnLogon;
            this.dataTrade.Start();

            var status = this.logonEvent.WaitOne(LogonWaitingTimeout);
            Assert.IsTrue(status, "Timeout of logon event");
            const double price = 0.5;
            var order = this.dataTrade.Server.SendOrderEx("EURUSD", TradeCommand.Limit, TradeRecordSide.Buy, price, 10000, null, null, DateTime.UtcNow.AddHours(-1), "comment", 1000000);
            Assert.IsTrue(order.Price == price, "Invalid order price = {0}", order.Price);

            Reports.TradeTransactionReport tradeReport = this.dataTrade.Server.GetTradeTransactionReports(TimeDirection.Backward, false, DateTime.UtcNow.AddMinutes(-5), DateTime.UtcNow.AddMinutes(5)).Item;
            Assert.IsTrue(tradeReport.TradeTransactionReason == Reports.TradeTransactionReason.Expired);

            this.dataTrade.Logon -= this.OnLogon;
            this.dataTrade.Stop();
            this.dataTrade.Dispose();
        }
        [TestMethod]
        public void CloseBy()
        {
            TestHelpers.Execute(this.CloseBy, Configuration.DataTradeGrossConnectionBuilders);
        }
        void CloseBy(ConnectionStringBuilder builder)
        {
            var connectionString = builder.ToString();
            using (this.dataTrade = new DataTrade(connectionString))
            {

                this.dataTrade.Logon += OnLogon;
                this.dataTrade.AccountInfo += DataTrade_AccountInfo;
                this.dataTrade.Start();

                bool status = this.logonEvent.WaitOne(LogonWaitingTimeout);
                status &= this.accountInfoEvent.WaitOne(LogonWaitingTimeout);
                Assert.IsTrue(status, "Timeout of logon event");

                TradeRecord order1 = this.dataTrade.Server.SendOrderEx("EURUSD", TradeCommand.Market, TradeRecordSide.Buy, 0, 10000, null, null, null, "comment", 1000000);
                TradeRecord order2 = this.dataTrade.Server.SendOrderEx("EURUSD", TradeCommand.Market, TradeRecordSide.Sell, 0, 10000, null, null, null, "comment", 1000000);

                Assert.IsTrue( dataTrade.Server.CloseByPositions(order1.OrderId, order2.OrderId) );
                var iter = this.dataTrade.Server.GetTradeTransactionReports(TimeDirection.Backward, false, DateTime.UtcNow.AddMinutes(-5), DateTime.UtcNow.AddMinutes(5));
                TradeTransactionReport tradeReport1 = iter.Item;
                iter.Next();
                TradeTransactionReport tradeReport2 = iter.Item;

                //Assert.IsTrue(tradeReport1.PositionById == order1.OrderId);
                Assert.IsTrue(tradeReport2.PositionById == order2.OrderId);


                this.dataTrade.Logon -= this.OnLogon;
                this.dataTrade.AccountInfo -= DataTrade_AccountInfo;
                this.dataTrade.Stop();
            }
        }
    }
}
