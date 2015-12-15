namespace SoftFX.Extended.Functional.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using SoftFX.Extended;
    using SoftFX.Extended.Events;

    [TestClass]
    public class DataFeedTests
    {
        #region Members

        const int LogonWaitingTimeout = 10000;
        const int TickWaitingTimeout = 10000;
        const int LogoutWaitingTimeout = 10000;

        readonly AutoResetEvent logonEvent = new AutoResetEvent(false);
        readonly AutoResetEvent tickEvent = new AutoResetEvent(false);
        readonly AutoResetEvent logoutEvent = new AutoResetEvent(false);

        LogoutReason logoutReason;
        DataFeed dataFeed;

        #endregion

        #region Event Handlers

        void OnLogon(object sender, LogonEventArgs e)
        {
            this.logonEvent.Set();
        }

        void OnLogout(object sender, LogoutEventArgs e)
        {
            this.logoutReason = e.Reason;
            this.logoutEvent.Set();
        }

        void OnTick(object sender, TickEventArgs e)
        {
            this.tickEvent.Set();
        }

        #endregion

        #region Test Initialization and Cleanup

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
            if (this.dataFeed != null)
            {
                if (this.dataFeed.IsStarted)
                {
                    this.dataFeed.Stop();
                }
                this.dataFeed.Dispose();
                this.dataFeed = null;
            }
        }

        #endregion

        #region Create and Destroy

        [TestMethod]
        public void CreateAndDestroy()
        {
            TestHelpers.Execute(this.CreateAndDestroy, Configuration.ConnectionBuilders(AccountType.Gross, Configuration.ConnectionType.Feed));
        }

        void CreateAndDestroy(ConnectionStringBuilder builder)
        {
            var connectionString = builder.ToString();
            this.dataFeed = new DataFeed(connectionString);
        }

        #endregion

        #region Create, Connect and Dispose

        [TestMethod]
        public void CreateConnectAndDispose()
        {
        }

        void CreateConnectAndDispose(ConnectionStringBuilder builder)
        {
        }

        #endregion

        #region logon

        [TestMethod]
        public void Logon()
        {
            TestHelpers.Execute(this.Logon, Configuration.ConnectionBuilders(AccountType.Gross, Configuration.ConnectionType.Feed));
        }

        void Logon(ConnectionStringBuilder builder)
        {
            var connectionString = builder.ToString();
            this.dataFeed = new DataFeed(connectionString);
            this.dataFeed.Logon += this.OnLogon;
            this.dataFeed.Start();

            var status = this.logonEvent.WaitOne(LogonWaitingTimeout);
            Assert.IsTrue(status, "Timeout of logon event");

            var fixBuilder = builder as FixConnectionStringBuilder;
            if (fixBuilder != null)
            {
                var st = this.dataFeed.UsedProtocolVersion.ToString();
                Assert.IsTrue(fixBuilder.ProtocolVersion == st, "Server is not supported required protocol version");
            }
            this.dataFeed.Stop();
            this.dataFeed.Logon -= this.OnLogon;
        }

        #endregion

        #region logout

        [TestMethod]
        public void RegularLogout()
        {
            TestHelpers.Execute(this.RegularLogout, Configuration.ConnectionBuilders(AccountType.Gross, Configuration.ConnectionType.Feed));
        }

        void RegularLogout(ConnectionStringBuilder builder)
        {
            var fixBuilder = builder as FixConnectionStringBuilder;
            var connectionString = builder.ToString();
            this.dataFeed = new DataFeed(connectionString);
            this.dataFeed.Logon += this.OnLogon;
            this.dataFeed.Logout += this.OnLogout;
            this.dataFeed.Start();

            var status = this.logonEvent.WaitOne(LogonWaitingTimeout);
            Assert.IsTrue(status, "Timeout of logon event");

            this.dataFeed.Stop();
            status = this.logoutEvent.WaitOne(0);
            Assert.IsTrue(status, "Timeout of logout event");

            this.dataFeed.Logon -= this.OnLogon;
            this.dataFeed.Logout -= this.OnLogout;
            Assert.IsTrue(LogoutReason.ClientInitiated == this.logoutReason);
        }

        [TestMethod, Ignore]
        public void ConnectionErrorLogout()
        {
            TestHelpers.Execute(this.ConnectionErrorLogout, Configuration.ConnectionBuilders(AccountType.Gross, Configuration.ConnectionType.Feed));
        }

        void ConnectionErrorLogout(ConnectionStringBuilder builder)
        {
            var fixBuilder = builder as FixConnectionStringBuilder;
            fixBuilder.Address = "localhost";
            fixBuilder.FixLogDirectory = "C:\\Temporary\\Logs";

            var connectionString = builder.ToString();
            this.dataFeed = new DataFeed(connectionString);
            this.dataFeed.Logon += this.OnLogon;
            this.dataFeed.Logout += this.OnLogout;
            this.dataFeed.Start();

            var status = this.logonEvent.WaitOne(LogonWaitingTimeout);
            Assert.IsTrue(!status, "Logon has been generated by error");

            status = this.logoutEvent.WaitOne(10 * LogoutWaitingTimeout);
            Assert.IsTrue(status, "Timeout of logout event");
            Assert.IsTrue(LogoutReason.NetworkError == this.logoutReason);

            this.dataFeed.Stop();
            this.dataFeed.Logon -= this.OnLogon;
            this.dataFeed.Logout -= this.OnLogout;
        }

        [TestMethod]
        public void NotValidUsernameAndPasswordLogout()
        {
            TestHelpers.Execute(this.NotValidUsernameAndPasswordLogout, Configuration.ConnectionBuilders(AccountType.Gross, Configuration.ConnectionType.Feed));
        }

        void NotValidUsernameAndPasswordLogout(ConnectionStringBuilder builder)
        {
            var fixBuilder = builder as FixConnectionStringBuilder;
            fixBuilder.Username = "user";
            fixBuilder.Password = "12345";
            fixBuilder.FixLogDirectory = "C:\\Temporary\\logs";

            var connectionString = builder.ToString();
            this.dataFeed = new DataFeed(connectionString);
            this.dataFeed.Logon += OnLogon;
            this.dataFeed.Logout += OnLogout;
            this.dataFeed.Start();

            var status = this.logonEvent.WaitOne(LogonWaitingTimeout);
            Assert.IsTrue(!status, "Logon has been generated by error");

            status = this.logoutEvent.WaitOne(10 * LogoutWaitingTimeout);
            Assert.IsTrue(status, "Timeout of logout event");
            if ("ext.0.0" == fixBuilder.ProtocolVersion)
            {
                Assert.IsTrue(LogoutReason.None == this.logoutReason);
            }
            else
            {
                Assert.IsTrue(LogoutReason.InvalidCredentials == this.logoutReason);
            }
            this.dataFeed.Stop();
            this.dataFeed.Logon -= OnLogon;
            this.dataFeed.Logout -= OnLogout;
        }

        #endregion

        #region Subscribe

        [TestMethod]
        public void CorrectSubscribeToQuotes()
        {
            TestHelpers.Execute(this.CorrectSubscribeToQuotes, Configuration.ConnectionBuilders(AccountType.Gross, Configuration.ConnectionType.Feed));
        }

        void CorrectSubscribeToQuotes(ConnectionStringBuilder builder)
        {
            var connectionString = builder.ToString();
            this.dataFeed = new DataFeed(connectionString);
            this.dataFeed.Logon += this.OnLogon;
            this.dataFeed.Start();

            var status = this.logonEvent.WaitOne(LogonWaitingTimeout);
            Assert.IsTrue(status, "Timeout of logon event");

            var symbols = new[] { "EURUSD" };
            this.dataFeed.Server.SubscribeToQuotes(symbols, 0);

            this.dataFeed.Stop();
            this.dataFeed.Logon -= this.OnLogon;
        }

        [TestMethod]
        public void IncorrectSubscribeToQuotes()
        {
            TestHelpers.Execute(this.IncorrectSubscribeToQuotes, Configuration.ConnectionBuilders(AccountType.Gross, Configuration.ConnectionType.Feed));
        }

        void IncorrectSubscribeToQuotes(ConnectionStringBuilder builder)
        {
            var connectionString = builder.ToString();
            this.dataFeed = new DataFeed(connectionString);
            this.dataFeed.Logon += this.OnLogon;
            this.dataFeed.Start();

            var status = this.logonEvent.WaitOne(LogonWaitingTimeout);
            Assert.IsTrue(status, "Timeout of logon event");

            try
            {
                this.dataFeed.Server.SubscribeToQuotes(null, 0);
                Assert.Fail("Null parameter has been accepted");
            }
            catch
            {
            }

            try
            {
                var symbols = new[] { "EURUSD" };
                this.dataFeed.Server.SubscribeToQuotes(symbols, -1);
                Assert.Fail("Negative parameter has been accepted");
            }
            catch
            {
            }

            try
            {
                var symbols = new[] { "EURUSD" };
                this.dataFeed.Server.SubscribeToQuotes(symbols, 6);
                Assert.Fail("Negative parameter has been accepted");
            }
            catch
            {
            }

            try
            {
                var symbols = new[] { "YYY/XXX" };
                this.dataFeed.Server.SubscribeToQuotes(symbols, 0);
                Assert.Fail("Invalid symbols have been accepted");
            }
            catch
            {
            }

            this.dataFeed.Stop();
            this.dataFeed.Logon -= this.OnLogon;
        }

        #endregion

        #region Ticks

        [TestMethod]
        public void Tick()
        {
            TestHelpers.Execute(this.Tick, Configuration.ConnectionBuilders(AccountType.Gross, Configuration.ConnectionType.Feed));
        }

        public void Tick(ConnectionStringBuilder builder)
        {
            var connectionString = builder.ToString();
            this.dataFeed = new DataFeed(connectionString);
            this.dataFeed.Logon += this.OnLogon;
            this.dataFeed.Tick += this.OnTick;
            this.dataFeed.Start();

            var status = this.logonEvent.WaitOne(LogonWaitingTimeout);
            Assert.IsTrue(status, "Timeout of logon event");

            var symbols = new[] { "EURUSD" };
            this.dataFeed.Server.SubscribeToQuotes(symbols, 0);

            status = this.tickEvent.WaitOne(TickWaitingTimeout);
            Assert.IsTrue(status, "Timeout of tick event");

            this.dataFeed.Stop();
            this.dataFeed.Logon -= this.OnLogon;
            this.dataFeed.Tick -= this.OnTick;
        }
        #endregion

        #region Bars

        //[TestMethod]
        //public void ForwardAndBackwardBarsRequestConsistency()
        //{
        //    UnitEx.Execute(this.ForwardAndBackwardBarsRequestConsistency, Configuration.ConnectionBuilders(AccountType.Gross, Configuration.ConnectionType.Feed));
        //}

        //private void ForwardAndBackwardBarsRequestConsistency(ConnectionStringBuilder builder)
        //{
        //    var connectionString = builder.ToString();
        //    this.dataFeed = new DataFeed(connectionString);
        //    this.dataFeed.Logon += this.OnLogon;
        //    this.dataFeed.Start();

        //    var status = this.logonEvent.WaitOne(LogonWaitingTimeout);
        //    Assert.IsTrue(status, "Timeout of logon event");


        //    var startTime = new DateTime(2011, 10, 10, 12, 0, 0, 0, DateTimeKind.Utc);

        //    const int count = 60;
        //    var forwardBars = this.dataFeed.Server.GetHistoryBars("EURUSD", startTime, count, PriceType.Bid, BarPeriod.S1);
        //    Assert.IsTrue(count == forwardBars.Length);

        //    var endTime = forwardBars[count - 1].To - BarPeriod.S1;

        //    var backwardBars = this.dataFeed.Server.GetHistoryBars("EURUSD", endTime, -count, PriceType.Bid, BarPeriod.S1);
        //    Assert.IsTrue(count == backwardBars.Length);

        //    for (var index = 0; index < count; ++index)
        //    {
        //        var first = forwardBars[index];
        //        var second = backwardBars[count - 1 - index];
        //        Assert.IsTrue(first.From == second.From);
        //        Assert.IsTrue(first.To == second.To);
        //    }
        //    this.dataFeed.Logon -= this.OnLogon;
        //    this.dataFeed.Stop();
        //}

        [TestMethod]
        public void ForwardBarsEnumeration()
        {
            TestHelpers.Execute(this.ForwardBarsEnumeration, Configuration.ConnectionBuilders(AccountType.Gross, Configuration.ConnectionType.Feed));
        }

        void ForwardBarsEnumeration(ConnectionStringBuilder builder)
        {
            var connectionString = builder.ToString();
            this.dataFeed = new DataFeed(connectionString);
            this.dataFeed.Logon += this.OnLogon;
            this.dataFeed.Start();

            var status = this.logonEvent.WaitOne(LogonWaitingTimeout);
            Assert.IsTrue(status, "Timeout of logon event");


            var startTime = new DateTime(2015, 10, 10, 12, 0, 0, 0, DateTimeKind.Utc);
            var endTime = new DateTime(2015, 12, 10, 12, 1, 0, 0, DateTimeKind.Utc);

            var data = new List<Bar>();
            var bars = new Bars(this.dataFeed, "EURUSD", PriceType.Bid, BarPeriod.D1, startTime, endTime);


            foreach (var element in bars)
            {
                Assert.IsTrue(startTime <= element.From);
                Assert.IsTrue(element.To <= endTime);
                Assert.IsTrue(element.From < element.To, "Invalid a bar timestamp");
                data.Add(element);
            }

            this.dataFeed.Logon -= OnLogon;
            this.dataFeed.Stop();

            for (var index = 1; index < data.Count; ++index)
            {
                var first = data[index - 1];
                var second = data[index];
                Assert.IsTrue(first.To <= second.From, "Invalid bars sequence order.");
            }
            //Assert.IsTrue(startTime == data[0].From);
            //Assert.IsTrue(endTime == data[data.Count - 1].To);
        }

        [TestMethod]
        public void BackwardBarsEnumeration()
        {
            TestHelpers.Execute(this.BackwardBarsEnumeration, Configuration.ConnectionBuilders(AccountType.Gross, Configuration.ConnectionType.Feed));
        }

        void BackwardBarsEnumeration(ConnectionStringBuilder builder)
        {
            var connectionString = builder.ToString();
            this.dataFeed = new DataFeed(connectionString);
            this.dataFeed.Logon += this.OnLogon;
            this.dataFeed.Start();

            var status = this.logonEvent.WaitOne(LogonWaitingTimeout);
            Assert.IsTrue(status, "Timeout of logon event");

            var startTime = new DateTime(2015, 10, 10, 12, 0, 0, 0, DateTimeKind.Utc);
            var endTime = new DateTime(2015, 10, 10, 12, 1, 0, 0, DateTimeKind.Utc);

            var data = new List<Bar>();
            var bars = new Bars(this.dataFeed, "EURUSD", PriceType.Bid, BarPeriod.S1, endTime, startTime);

            foreach (var element in bars)
            {
                Assert.IsTrue(startTime <= element.From, "startTime <= element.From");
                Assert.IsTrue(element.To <= endTime, "element.To <= endTime");
                Assert.IsTrue(element.From < element.To, "Invalid a bar timestamp");
                data.Add(element);
            }

            this.dataFeed.Logon -= this.OnLogon;
            this.dataFeed.Stop();

            for (var index = 1; index < data.Count; ++index)
            {
                var first = data[index];
                var second = data[index - 1];
                Assert.IsTrue(first.To <= second.From, "Invalid bars sequence order.");
            }
        }

        [TestMethod]
        public void ForwardAndBackwardBarsEnumerationConsistency()
        {
            TestHelpers.Execute(this.ForwardAndBackwardBarsEnumerationConsistency, Configuration.ConnectionBuilders(AccountType.Gross, Configuration.ConnectionType.Feed));
        }

        void ForwardAndBackwardBarsEnumerationConsistency(ConnectionStringBuilder builder)
        {
            var connectionString = builder.ToString();
            this.dataFeed = new DataFeed(connectionString);
            this.dataFeed.Logon += this.OnLogon;
            this.dataFeed.Start();

            var status = this.logonEvent.WaitOne(LogonWaitingTimeout);
            Assert.IsTrue(status, "Timeout of logon event");


            var startTime = new DateTime(2011, 10, 10, 12, 0, 0, 0, DateTimeKind.Utc);
            var endTime = new DateTime(2011, 10, 10, 12, 1, 0, 0, DateTimeKind.Utc);


            var forwardBars = new Bars(this.dataFeed, "EURUSD", PriceType.Bid, BarPeriod.S1, startTime, endTime);
            var forwardData = forwardBars.ToList();

            var backwardBars = new Bars(this.dataFeed, "EURUSD", PriceType.Bid, BarPeriod.S1, endTime, startTime);
            var backwardData = backwardBars.ToList();

            var count = forwardData.Count;
            Assert.IsTrue(forwardData.Count == backwardData.Count, "forwardData.Count == backwardData.Count");
            for (var index = 0; index < count; ++index)
            {
                var first = forwardData[index];
                var second = backwardData[count - 1 - index];
                Assert.IsTrue(first.From == second.From);
                Assert.IsTrue(first.To == second.To);
            }
        }


        #endregion
    }
}
