namespace SoftFX.Basic
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using SoftFX.Extended;
    using SoftFX.Extended.Events;
    using SoftFX.Extended.Financial;
    using SoftFX.Extended.Storage;

    /// <summary>
    /// Contains feed/trade methods.
    /// </summary>
    public class Manager : IDisposable
    {
        #region Construction

        /// <summary>
        /// Creates a new instance of Manager class by two connection strings.
        /// </summary>
        /// <param name="feedConnectionString">a feed connection string</param>
        /// <param name="tradeConnectionString">a trade connection string</param>
        /// <param name="location">a relative or absolute path location for history</param>
        /// <exception cref="System.ArgumentNullException">if feedConnectionString or tradeConnectionString are null</exception>
        public Manager(string tradeConnectionString, string feedConnectionString, string location)
        {
            if (tradeConnectionString == null)
                throw new ArgumentNullException("tradeConnectionString");

            if (feedConnectionString == null)
                throw new ArgumentNullException("feedConnectionString");

            if (location == null)
                throw new ArgumentNullException("location");

            try
            {
                this.feed = new DataFeed(feedConnectionString);
                this.trade = new DataTrade(tradeConnectionString);

                var path = Path.Combine(location, "Quotes");
                this.storage = new DataFeedStorage(path, StorageProvider.Ntfs, feed, false);
                this.calculator = new StateCalculator(trade, feed);

                this.feed.SymbolInfo += this.OnSymbolInfo;
                this.Snapshot = new Snapshot(this.trade, this.feed, this.calculator, this.synchronizer, this.syncEvent);
            }
            catch
            {
                this.Dispose();
                throw;
            }
        }

        #endregion

        #region Feed Events

        void OnSymbolInfo(object sender, SymbolInfoEventArgs e)
        {
            try
            {
                var symbols = e.Information
                               .Select(o => o.Name)
                               .ToArray();

                this.feed.Server.SubscribeToQuotes(symbols, 1);
            }
            catch
            {
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Allows to catch and log exception in silent mode.
        /// </summary>
        public event ErrorHandler Error;

        /// <summary>
        /// Raises when any data have been updated.
        /// </summary>
        public event EventHandler Updated;

        #endregion

        #region Control Methods

        /// <summary>
        /// Starts the manager instance.
        /// </summary>
        public void Start()
        {
            lock (this.synchronizer)
            {
                if (this.thread != null)
                    throw new InvalidOperationException("The manager already is started");

                this.continueMonitoring = true;
                this.syncEvent.WaitOne(0);
                this.thread = new Thread(this.ThreadLoop);
                this.thread.Start();

                this.feed.Start();
                this.trade.Start();
            }
        }

        /// <summary>
        /// Stops the manager instance.
        /// </summary>
        public void Stop()
        {
            lock (this.synchronizer)
            {
                if (thread == null)
                    throw new InvalidOperationException("The manager already is stopped");

                this.continueMonitoring = false;
                this.syncEvent.Set();
                this.thread.Join();
                this.thread = null;
            }

            this.trade.Stop();
            this.feed.Stop();
        }

        #endregion

        #region Snapshot Methods

        /// <summary>
        /// The method recalculates snapshot fully.
        /// </summary>
        public void RefreshSnapshot()
        {
            this.Snapshot.Refresh();
        }

        /// <summary>
        /// The method takes the full snapshot of the manager state.
        /// </summary>
        /// <returns></returns>
        public Snapshot TakeSnapshot(string symbol, PriceType priceType, BarPeriod periodicity)
        {
            lock (this.synchronizer)
            {
                var result = new Snapshot(this.Snapshot, this.storage, symbol, periodicity, priceType);
                return result;
            }
        }

        #endregion

        #region Internal Methods

        internal void Acquire()
        {
            Monitor.Enter(this.synchronizer);
        }

        internal void Release()
        {
            Monitor.Exit(this.synchronizer);
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Provides direct access to data trade.
        /// </summary>
        public DataTrade Trader
        {
            get
            {
                return this.trade;
            }
        }

        /// <summary>
        /// Provides direct access to data feed.
        /// </summary>
        public DataFeed Feeder
        {
            get
            {
                return this.feed;
            }
        }

        internal Snapshot Snapshot { get; private set; }

        #endregion

        #region Thread Methods

        void ThreadLoop()
        {
            for (this.syncEvent.WaitOne(); this.continueMonitoring; this.syncEvent.WaitOne())
            {
                this.ThreadStep();
            }
        }

        void ThreadStep()
        {
            var mode = this.ErrorMode;

            if (mode == ErrorMode.Default)
            {
                if (Debugger.IsAttached)
                    this.ThreadStepThrow();
                else
                    this.ThreadStepSilent();
            }
            else if (mode == ErrorMode.Throw)
                this.ThreadStepThrow();
            else
                this.ThreadStepSilent();
        }

        void ThreadStepThrow()
        {
            var eh = this.Updated;
            if (eh != null)
                eh(this, EventArgs.Empty);
        }

        void ThreadStepSilent()
        {
            try
            {
                this.ThreadStepThrow();
            }
            catch (Exception ex)
            {
                var eh = this.Error;
                if (eh != null)
                {
                    var e = new ErrorEventArgs(ex);
                    eh(this, e);
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets error mode of the manager.
        /// </summary>
        public ErrorMode ErrorMode
        {
            get
            {
                return this.mode;
            }
            set
            {
                if (value != ErrorMode.Default && value != ErrorMode.Throw && value != ErrorMode.Silent)
                    throw new ArgumentOutOfRangeException("value", value, "Expected: Default, Throw or Silent");

                this.mode = value;
            }
        }

        #endregion

        #region IDisposable Interface

        /// <summary>
        /// Closes all connections, flushes all data.
        /// </summary>
        public void Dispose()
        {
            if (this.continueMonitoring)
            {
                this.Stop();
            }

            if (this.Snapshot != null)
            {
                this.Snapshot.Stop();
            }

            if (this.storage != null)
            {
                this.storage.Dispose();
                this.storage = null;
            }

            if (this.feed != null)
            {
                this.feed.Stop();
                this.feed.Dispose();
                this.feed = null;
            }

            if (this.trade != null)
            {
                this.trade.Stop();
                this.trade.Dispose();
                this.trade = null;
            }

            this.syncEvent.Dispose();
        }

        #endregion

        #region Members

        readonly object synchronizer = new object();
        readonly AutoResetEvent syncEvent = new AutoResetEvent(false);
        volatile bool continueMonitoring = false;
        ErrorMode mode = ErrorMode.Default;
        Thread thread;
        DataFeed feed;
        DataTrade trade;
        DataFeedStorage storage;
        StateCalculator calculator;

        #endregion
    }
}
