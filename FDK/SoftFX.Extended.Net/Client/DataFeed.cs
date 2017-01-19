namespace SoftFX.Extended
{
    using System;
    using SoftFX.Extended.Core;
    using SoftFX.Extended.Events;

    /// <summary>
    /// This class connects to trading platform and receives quotes and other notifications.
    /// </summary>
    public class DataFeed : DataClient
    {
        #region Fields

        FxDataFeed handle;

        #endregion

        #region Construction

        /// <summary>
        /// Creates a new data feed instance. You should use Initialize method to finish the instance initialization.
        /// </summary>
        public DataFeed() :
            this(null, "Feed")
        {
        }

        /// <summary>
        /// Creates and initializes a new data feed instance.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If connectionString is null.</exception>
        public DataFeed(string connectionString) :
            this(connectionString, "Feed")
        {
        }

        /// <summary>
        /// Creates and initializes a new data feed instance.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If connectionString is null.</exception>
        public DataFeed(string connectionString, string name) :
            base(name)
        {
            this.Server = new DataFeedServer(this);
            this.Cache = new DataFeedCache(this);
            if (!string.IsNullOrEmpty(connectionString))
                this.Initialize(connectionString);
        }

        internal override FxDataClient CreateFxDataClient(string connectionString)
        {
            this.handle.Handle.Delete();
            this.handle = new FxDataFeed();
            this.handle = FxDataFeed.Create(name_, connectionString);

            return this.handle.DataClient;
        }

        /// <summary>
        /// This method is called when DataFeed object is constructed.
        /// </summary>
        protected sealed override void OnInitialized()
        {
            this.Server = new DataFeedServer(this);
            this.Cache = new DataFeedCache(this);
        }

        static DataFeed()
        {
            Native.Initialize();
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when a new quote is received.
        /// </summary>
        public event TickHandler Tick;

        /// <summary>
        /// Occurs when currencies information is initialized.
        /// </summary>
        public event CurrencyInfoHandler CurrencyInfo;

        /// <summary>
        /// Occurs when symbols information is initialized.
        /// </summary>
        public event SymbolInfoHandler SymbolInfo;

        /// <summary>
        /// Occurs when a notification is received.
        /// </summary>
        public event NotifyHandler Notify;

        #endregion

        #region Properties

        internal FxDataFeed DataFeedHandle
        {
            get
            {
                return this.handle;
            }
        }

        /// <summary>
        /// Gets object, which encapsulates server side methods.
        /// </summary>
        public DataFeedServer Server { get; private set; }

        /// <summary>
        /// Gets object, which encapsulates client cache methods.
        /// </summary>
        public DataFeedCache Cache { get; private set; }

        /// <summary>
        /// Gets or sets queue size for quotes.
        /// Note: FDK uses a separated queue for every symbol.
        /// </summary>
        public int QuotesQueueThresholdSize
        {
            get
            {
                return this.handle.GetQueueThreshold();
            }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "Quotes queue size must be positive");

                this.handle.SetQueueThreshold(value);
            }
        }
        #endregion

        #region Methods

        internal override DataServer DataServer
        {
            get
            {
                return this.Server;
            }
        }

        internal override bool ProcessMessage(FxMessage message)
        {
            if (base.ProcessMessage(message))
                return true;
            switch (message.Type)
            {
                case Native.FX_MSG_TICK:
                    this.RaiseTick(message);
                    break;
                case Native.FX_MSG_SYMBOL_INFO:
                    this.RaiseSymbolInfo(message);
                    break;
                case Native.FX_MSG_NOTIFICATION:
                    var notification = message.Notification();
                    if (notification.Type == NotificationType.ConfigUpdated)
                    {
                        var e = new NotificationEventArgs(notification);
                        this.RaiseNotification(e);
                    }
                    else
                        return false;
                    break;
                case Native.FX_MSG_CURRENCY_INFO:
                    this.RaiseCurrencyInfo(message);
                    break;
                default:
                    return false;
            }

            return true;
        }

        void RaiseTick(FxMessage message)
        {
#if LOG_PERFORMANCE
            ulong timestamp = loggerOut_.GetTimestamp();
            string id = message.Quote().Id;
            loggerOut_.LogTimestamp(id, timestamp, "Tick");
#endif
            var eh = this.Tick;
            if (eh != null)
            {
                var e = new TickEventArgs(message);
                eh(this, e);
            }
        }

        void RaiseCurrencyInfo(FxMessage message)
        {
            var eh = this.CurrencyInfo;
            if (eh != null)
            {
                var e = new CurrencyInfoEventArgs(message);
                eh(this, e);
            }
        }

        void RaiseSymbolInfo(FxMessage message)
        {
            var eh = this.SymbolInfo;
            if (eh != null)
            {
                var e = new SymbolInfoEventArgs(message, this.UsedProtocolVersion);
                eh(this, e);
            }
        }

        void RaiseNotification(NotificationEventArgs e)
        {
            var eh = this.Notify;
            if (eh != null)
            {
                eh(this, e);
            }
        }

        #endregion

        #region Disposing

        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            // Stop data client thread
            if (this.IsStarted)
                this.Stop();

            if (! this.handle.Handle.IsNull)
            {
                this.handle.Handle.Delete();
                this.handle = new FxDataFeed();
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}
