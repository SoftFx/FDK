namespace SoftFX.Extended.Functional.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Threading;
    using SoftFX.Extended;
    using SoftFX.Extended.Events;

    [TestClass]
    public class TestBase
    {
        #region Members

        protected readonly AutoResetEvent logonEvent;
        protected DataFeed dataFeed;
        protected DataTrade dataTrade;

        protected const int LogonWaitingTimeout = 10000;

        #endregion

        #region Test initialization and Cleanup

        public TestBase()
        {
            this.logonEvent = new AutoResetEvent(false);
        }

        [TestInitialize]
        public void Initialize()
        {
            this.logonEvent.WaitOne(0);
        }

        [TestCleanup]
        public void Cleanup()
        {
            Cleanup(this.dataFeed);
            this.dataFeed = null;
            Cleanup(this.dataTrade);
            this.dataTrade = null;
        }

        static void Cleanup(DataClient client)
        {
            if (client == null)
                return;

            if (client.IsStarted)
                client.Stop();

            client.Dispose();
        }

        #endregion

        #region Event Handlers

        protected void OnLogon(object sender, LogonEventArgs e)
        {
            this.logonEvent.Set();
        }

        #endregion

        #region Methods

        protected void CacheDontHavePositions()
        {
            if (this.dataTrade == null)
                return;

            var records = this.dataTrade.Cache.TradeRecords;
            foreach (var element in records)
            {
                Assert.IsTrue(element.IsPendingOrder);
            }
        }

        #endregion

        #region Static Methods

        public static void Output(string format, params object[] args)
        {
            for (var index = 0; index < args.Length; ++index)
            {
                if (args[index] == null)
                {
                    args[index] = "null";
                }
            }

            Console.WriteLine(format, args);
        }

        #endregion
    }
}
