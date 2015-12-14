using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SoftFX.Extended;
using SoftFX.Extended.Events;
using SoftFX.Extended.Financial;

namespace SoftFX.Extended.Functional.Tests
{
    [TestClass]
    public class FeedTradeTest
    {
        #region Members

        const int LogonWaitingTimeout = 10000;
        const int LogoutWaitingTimeout = 10000;

        readonly AutoResetEvent feedLogonEvent = new AutoResetEvent(false);
        readonly AutoResetEvent tradeLogonEvent = new AutoResetEvent(false);
        readonly AutoResetEvent accountInfoEvent = new AutoResetEvent(false);
        readonly AutoResetEvent stateInfoEvent = new AutoResetEvent(false);

        DataFeed dataFeed;
        DataTrade dataTrade;

        #endregion

        #region Event Handlers

        void OnFeedLogon(object sender, LogonEventArgs e)
        {
            this.feedLogonEvent.Set();
        }

        void OnTradeLogon(object sender, LogonEventArgs e)
        {
            this.tradeLogonEvent.Set();
        }
        void DataTrade_AccountInfo(object sender, AccountInfoEventArgs e)
        {
            this.accountInfoEvent.Set();
        }
        #endregion

        [TestInitialize]
        public void Initialize()
        {
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

        private void StateCalculator_StateInfoChanged(object sender, StateInfoEventArgs e)
        {
           // stateInfoEvent.Set();
        }

        [TestMethod]
        public void StateInfoChanged()
        {
            string connectionString = Configuration.ConnectionBuilders(AccountType.Gross, Configuration.ConnectionType.Feed).First().ToString();
            this.dataFeed = new DataFeed(connectionString);
            this.dataFeed.Logon += this.OnFeedLogon;

            connectionString = Configuration.ConnectionBuilders(AccountType.Gross, Configuration.ConnectionType.Trade).First().ToString();
            this.dataTrade = new DataTrade(connectionString);
            this.dataTrade.Logon += this.OnTradeLogon;

            StateCalculator stateCalculator = new StateCalculator(dataTrade, dataFeed);
            stateCalculator.StateInfoChanged += StateCalculator_StateInfoChanged;

            this.dataFeed.Start();
            this.dataTrade.Start();
            var status = this.feedLogonEvent.WaitOne(LogonWaitingTimeout);
            Assert.IsTrue(status, "Timeout of feed logon event");
            status = this.tradeLogonEvent.WaitOne(LogonWaitingTimeout);
            Assert.IsTrue(status, "Timeout of trade logon event");

            status = this.stateInfoEvent.WaitOne(LogonWaitingTimeout*10000);

            this.dataFeed.Stop();
            this.dataTrade.Stop();
            this.dataFeed.Logon -= this.OnFeedLogon;
            this.dataTrade.Logon -= this.OnTradeLogon;
        }


    }
}
