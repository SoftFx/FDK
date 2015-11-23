namespace Mql2Fdk
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using System.Threading;
    using SoftFX.Basic;
    using SoftFX.Extended;

    /// <summary>
    /// 
    /// </summary>
    public abstract partial class MqlAdapter : Strategy
    {
        #region Construction

        /// <summary>
        /// 
        /// </summary>
        protected MqlAdapter()
        {
            this.random = new Random();
            this.synchronizer = new object();
            this.syncEvent = new AutoResetEvent(false);

            this.Open = new BarPrices(BarPriceType.Open, this);
            this.Close = new BarPrices(BarPriceType.Close, this);
            this.Low = new BarPrices(BarPriceType.Low, this);
            this.High = new BarPrices(BarPriceType.High, this);
            this.Volume = new BarVolumes(this);

            this.initMethod = this.GetType().GetMethod("init", BindingFlags.NonPublic | BindingFlags.Instance);
            this.startMethod = this.GetType().GetMethod("start", BindingFlags.NonPublic | BindingFlags.Instance);
            this.deinitMethod = this.GetType().GetMethod("deinit", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        #endregion

        #region Properties

        internal Snapshot CurrentSnapshot
        {
            get { return this.currentSnapshot; }
        }

        #endregion

        #region Control Methods

        internal override void Initialize(Manager manager, IStrategyLog log, string symbol, PriceType priceType, BarPeriod periodicity)
        {
            if (manager == null)
                throw new ArgumentNullException("manager");

            if (log == null)
                throw new ArgumentNullException("log");

            this.manager = manager;
            this.log = log;
            this.symbol = symbol;
            this.priceType = priceType;
            this.periodicity = periodicity;
            this.manager.Updated += this.OnUpdated;
        }

        internal override void Start()
        {
            this.isStopped = false;
            this.thread = new Thread(this.ThreadLoop);
            this.thread.Start();
        }

        internal override void Stop()
        {
            this.isStopped = true;

            this.syncEvent.Set();

            if (this.thread != null)
            {
                this.thread.Join();
                this.thread = null;
            }
        }

        #endregion

        #region Thread Methods

        void ThreadLoop()
        {
            for (this.syncEvent.WaitOne(); !this.IsStopped(); this.syncEvent.WaitOne())
            {
                if (this.SafeThreadInit())
                {
                    break;
                }
            }

            if (!this.IsStopped())
            {
                for (this.syncEvent.WaitOne(); !this.IsStopped(); this.syncEvent.WaitOne())
                {
                    this.ThreadStep();
                }
            }

            this.ThreadDeinit();
        }

        bool SafeThreadInit()
        {
            try
            {
                lock (this.synchronizer)
                {
                    if (this.nextSnapshot == null)
                        return false;

                    this.currentSnapshot = this.nextSnapshot;
                    this.nextSnapshot = null;
                }

                var result = this.SafeIsSnapshotInitialized();
                if (result)
                    this.ThreadInit();

                return result;
            }
            catch
            {
                return false;
            }
        }

        void ThreadInit()
        {
            if (!this.SafeIsSnapshotInitialized())
                return;

            if (this.initMethod != null)
                this.initMethod.Invoke(this, null);
        }

        void ThreadStep()
        {
            lock (this.synchronizer)
            {
                if (this.nextSnapshot == null)
                    return;

                this.currentSnapshot = this.nextSnapshot;
                this.nextSnapshot = null;
            }

            if (!this.SafeIsSnapshotInitialized())
                return;

            if (this.startMethod != null)
                this.startMethod.Invoke(this, null);
        }

        void ThreadDeinit()
        {
            if (this.deinitMethod != null)
                this.deinitMethod.Invoke(this, null);
        }

        bool SafeIsSnapshotInitialized()
        {
            try
            {
                return this.IsSnapshotInitialized();
            }
            catch
            {
                return false;
            }
        }

        bool IsSnapshotInitialized()
        {
            if (this.currentSnapshot == null)
                return false;

            if (this.currentSnapshot.AccountInfo == null)
                return false;

            if (this.currentSnapshot.Quotes == null)
                return false;

            if (this.currentSnapshot.TradeRecords == null)
                return false;

            if (this.currentSnapshot.Symbols == null)
                return false;

            if (!this.currentSnapshot.Quotes.ContainsKey(this.symbol))
                return false;

            if (this.currentSnapshot.Bars == null)
                return false;

            return true;
        }

        void OnUpdated(object sender, EventArgs e)
        {
            lock (this.synchronizer)
            {
                this.nextSnapshot = this.manager.TakeSnapshot(this.symbol, this.priceType, this.periodicity);
            }

            this.syncEvent.Set();
        }

        #endregion

        #region Fields

        #region Advisers and Symbols Settings

        string symbol;
        PriceType priceType;
        BarPeriod periodicity;
        Manager manager;

        #endregion

        #region Thread Members

        bool isStopped;
        Thread thread;
        readonly AutoResetEvent syncEvent;
        MethodBase initMethod;
        MethodBase startMethod;
        MethodBase deinitMethod;

        #endregion

        #region Snapshots

        readonly object synchronizer;
        Snapshot currentSnapshot;
        Snapshot nextSnapshot;

        #endregion

        #region Members

        TradeRecord selectedTradeRecord;
        Random random;

        IStrategyLog log;

        #endregion

        #endregion
    }
}
