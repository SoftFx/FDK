namespace SoftFX.Extended
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using SoftFX.Extended.Core;
    using SoftFX.Extended.Events;

    /// <summary>
    /// Provides common methods of DataTrade and DataFeed classes.
    /// </summary>
    public abstract unsafe class DataClient : IDisposable
    {
        #region Fields

        readonly static object synchronizer = new object();
        FxDataClient handle;
        Thread thread;

        #endregion

        #region Construction

        static DataClient()
        {
            Native.Initialize();
        }

        internal DataClient(string name)
        {
            this.name_ = name;
            this.SynchOperationTimeout = 60000;
        }

        void Construct(FxDataClient client)
        {
            this.handle = client;
            this.Network = new Network(client);
        }

        /// <summary>
        /// Initializes the data feed instance; it must be stopped.
        /// </summary>
        /// <param name="connectionString">Can not be null.</param>
        /// <exception cref="System.ArgumentNullException">If connectionString is null.</exception>
        /// <exception cref="System.InvalidOperationException">If the instance is not stopped.</exception>
        public void Initialize(string connectionString)
        {
            if (connectionString == null)
                throw new ArgumentNullException(nameof(connectionString), "Connection string can not be null.");

#if LOG_PERFORMANCE
            service_ = new Core.Performance.Service(0);
            loggerOut_ = new Core.Performance.Logger(service_, name_ + ".t3", name_ + " .NET Out", ".\\Logs");
            loggerIn_ = new Core.Performance.Logger(service_, name_ + ".t0", name_ + " .NET In", ".\\Logs");
#endif
            lock (synchronizer)
            {
                if (this.IsStarted)
                    throw new InvalidOperationException("Can not initialize data trade/feed object, if it is running.");

                this.Construct(this.CreateFxDataClient(connectionString));
                this.OnInitialized();
            }
        }

        internal abstract FxDataClient CreateFxDataClient(string connectionString);

        /// <summary>
        /// This method is called when DataClient object is constructed.
        /// </summary>
        protected virtual void OnInitialized()
        {
        }

        #endregion

        #region Properties

        internal FxDataClient Handle
        {
            get
            {
                return this.handle;
            }
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get { return name_;  }
        }

        /// <summary>
        /// </summary>
        protected string name_;

        internal abstract DataServer DataServer { get; }

        /// <summary>
        /// Gets or sets default synchronous operation timeout in milliseconds.
        /// </summary>
        public int SynchOperationTimeout { get; set; }

        /// <summary>
        /// Gets used protocol version of the object.
        /// </summary>
        public string UsedProtocolVersion
        {
            get { return this.handle.ProtocolVersion; }
        }

        /// <summary>
        /// Returns true, the data trade/feed object is started, otherwise false.
        /// </summary>
        public bool IsStarted
        {
            get
            {
                lock (synchronizer)
                {
                    return this.thread != null;
                }
            }
        }

        /// <summary>
        /// Returns true, the data trade/feed object is stopped, otherwise false.
        /// </summary>
        public bool IsStopped
        {
            get
            {
                lock (synchronizer)
                {
                    return this.thread == null;
                }
            }
        }

        /// <summary>
        /// Returns a network information of corresponded client connection; can not be null.
        /// </summary>
        public Network Network { get; private set; }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when data feed is logon.
        /// </summary>
        public event LogonHandler Logon;

        /// <summary>
        /// Occurs when data feed is logout.
        /// </summary>
        public event LogoutHandler Logout;

        /// <summary>
        /// Occurs when data feed is required two factor auth.
        /// </summary>
        public event TwoFactorAuthHandler TwoFactorAuth;

        /// <summary>
        /// Occurs when session info received or changed.
        /// </summary>
        public event SessionInfoHandler SessionInfo;

        /// <summary>
        /// Occurs when local cache initialized.
        /// </summary>
        public event CacheHandler CacheInitialized;

        #endregion

        #region Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="timeoutInMilliseconds"></param>
        /// <returns></returns>
        public bool WaitForLogon(int timeoutInMilliseconds)
        {
            return this.handle.WaitForLogon(timeoutInMilliseconds);
        }

        /// <summary>
        /// Starts data feed/trade instance.
        /// </summary>
        public void Start()
        {
            lock (synchronizer)
            {
                if (this.handle.Start())
                {
                    Debug.Assert(this.thread == null);
                    this.thread = new Thread(this.SafeLoop);
                    this.thread.IsBackground = true;
                    this.thread.Start();
                }
            }
        }

        /// <summary>
        /// Starts data feed/trade instance and waits for logon event.
        /// </summary>
        /// <param name="timeoutInMilliseconds">Timeout of logon waiting.</param>
        /// <returns>true, if logon event is occurred, otherwise false</returns>
        public bool Start(int timeoutInMilliseconds)
        {
            this.Start();
            return this.handle.WaitForLogon(timeoutInMilliseconds);
        }

        /// <summary>
        /// Stops data feed instance. The method can not be called into any feed/trade event handler.
        /// </summary>
        public void Stop()
        {
            lock (synchronizer)
            {
                if (this.handle.Shutdown())
                {
                    this.thread.Join();
                    this.thread = null;
                }
                this.handle.Stop();
            }
        }

        /// <summary>
        /// The method generates a new unique string ID.
        /// </summary>
        /// <returns>Can not be null.</returns>
        public string GenerateOperationId()
        {
            var result = this.handle.NextId();
            return result;
        }

        void SafeLoop()
        {
#if !DEBUG
            try
            {
#endif
                this.Loop();
#if !DEBUG
            }
            catch
            {
            }
#endif
        }

        void Loop()
        {
            for (; ; )
            {
                FxMessage message;
                if (!this.handle.GetNextMessage(out message))
                    break;

                this.SafeProcessMessage(message);
            }
        }

        void SafeProcessMessage(FxMessage message)
        {
#if !DEBUG
            try
            {
#endif
                this.ProcessMessage(message);
#if !DEBUG
            }
            catch
            {
            }
#endif
            this.handle.DispatchMessage(message);
        }

        internal virtual bool ProcessMessage(FxMessage message)
        {
            switch (message.Type)
            {
                case Native.FX_MSG_LOGON:
                    this.RaiseLogon(message);
                    break;
                case Native.FX_MSG_LOGOUT:
                    this.RaiseLogout(message);
                    break;
                case Native.FX_MSG_SESSION_INFO:
                    this.RaiseSessionInfo(message);
                    break;
                case Native.FX_MSG_CACHE_UPDATED:
                    this.RaiseCacheUpdated(message);
                    break;
                case Native.FX_MSG_TWO_FACTOR_AUTH:
                    this.RaiseTwoFactorAuth(message);
                    break;
                default:
                    return false;
            }

            return true;
        }

        void RaiseLogon(FxMessage message)
        {
            var eh = this.Logon;
            if (eh != null)
            {
                var e = new LogonEventArgs(message);
                eh(this, e);
            }
        }

        void RaiseLogout(FxMessage message)
        {
            var eh = this.Logout;
            if (eh != null)
            {
                var e = new LogoutEventArgs(message);
                eh(this, e);
            }
        }

        void RaiseTwoFactorAuth(FxMessage message)
        {
            var eh = this.TwoFactorAuth;
            if (eh != null)
            {
                var e = new TwoFactorAuthEventArgs(message);
                eh(this, e);
            }
        }

        void RaiseSessionInfo(FxMessage message)
        {
            var eh = this.SessionInfo;
            if (eh != null)
            {
                var e = new SessionInfoEventArgs(message);
                eh(this, e);
            }
        }

        void RaiseCacheUpdated(FxMessage message)
        {
            var eh = this.CacheInitialized;
            if (eh != null)
            {
                var e = new CacheEventArgs();
                eh(this, e);
            }
        }

        #endregion

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        ~DataClient()
        {
            if (!Environment.HasShutdownStarted)
                this.Dispose(false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
#if LOG_PERFORMANCE
                if (loggerOut_ != null)
                    loggerOut_.Dispose();

                if (loggerIn_ != null)
                    loggerIn_.Dispose();

                if (service_ != null)
                    service_.Dispose();
#endif
            }
        }

#if LOG_PERFORMANCE
        internal Core.Performance.Service service_;
        internal Core.Performance.Logger loggerOut_;
        internal Core.Performance.Logger loggerIn_;
#endif
    }
}
