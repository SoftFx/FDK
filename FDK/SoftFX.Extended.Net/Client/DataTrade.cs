namespace SoftFX.Extended
{
    using System;
    using SoftFX.Extended.Core;
    using SoftFX.Extended.Events;

    /// <summary>
    /// This class connects to trading platform and provides trading functionality.
    /// </summary>
    public class DataTrade : DataClient
    {
        #region Fields

        FxDataTrade handle;

        #endregion

        #region Construction


        /// <summary>
        /// Creates a new data trade instance.
        /// </summary>
        public DataTrade() :
            this(null, "Trade")
        {
        }

        /// <summary>
        /// Creates and initializes a new data trade instance.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If connectionString is null.</exception>
        public DataTrade(string connectionString) :
            this(connectionString, "Trade")
        {
        }

        /// <summary>
        /// Creates and initializes a new data trade instance.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If connectionString is null.</exception>
        public DataTrade(string connectionString, string name) :
            base(name)
        {
            this.Server = new DataTradeServer(this);
            this.Cache = new DataTradeCache(this);
            if (!string.IsNullOrEmpty(connectionString))
                this.Initialize(connectionString);
        }

        internal override FxDataClient CreateFxDataClient(string connectionString)
        {
            this.handle.Handle.Delete();
            this.handle = new FxDataTrade();
            this.handle = FxDataTrade.Create(name_, connectionString);

            return this.handle.DataClient;
        }

        /// <summary>
        /// This method is called when DataTrade object is constructed.
        /// </summary>
        protected sealed override void OnInitialized()
        {
            this.Server = new DataTradeServer(this);
            this.Cache = new DataTradeCache(this);
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when account information is changed.
        /// </summary>
        public event AccountInfoHandler AccountInfo;

        /// <summary>
        /// Occurs when a trade operation is executing.
        /// </summary>
        public event ExecutionReportHandler ExecutionReport;

        /// <summary>
        ///
        /// </summary>
        public event TradeTransactionReportHandler TradeTransactionReport;

        /// <summary>
        /// The event is supported by Net account only.
        /// </summary>
        public event PositionReportHandler PositionReport;

        /// <summary>
        /// Occurs when a notification is received.
        /// </summary>
        public event NotifyHandler Notify;

        /// <summary>
        /// Occurs when a notification of balance operation is received.
        /// </summary>
        public event NotifyHandler<BalanceOperation> BalanceOperation;

        #endregion

        #region Properties

        internal FxDataTrade DataTradeHandle
        {
            get
            {
                return this.handle;
            }
        }


        /// <summary>
        /// Gets object, which encapsulates server side methods.
        /// </summary>
        public DataTradeServer Server { get; private set; }

        /// <summary>
        /// Gets object, which encapsulates client cache methods.
        /// </summary>
        public DataTradeCache Cache { get; private set; }

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
                case Native.FX_MSG_ACCOUNT_INFO:
                    this.RaiseAccountInfo(message);
                    break;
                case Native.FX_MSG_EXECUTION_REPORT:
                    this.RaiseExecutionReport(message);
                    break;
                case Native.FX_MSG_TRADE_TRANSACTION_REPORT:
                    this.RaiseTradeTransactionReport(message);
                    break;
                case Native.FX_MSG_POSITION_REPORT:
                    this.RaisePositionReport(message);
                    break;
                case Native.FX_MSG_NOTIFICATION:
                    this.RaiseNotification(message);
                    break;
                default:
                    return false;
            }

            return true;
        }

        void RaiseAccountInfo(FxMessage message)
        {
            var eh = this.AccountInfo;
            if (eh != null)
            {
                var e = new AccountInfoEventArgs(message);
                eh(this, e);
            }
        }

        void RaiseExecutionReport(FxMessage message)
        {
#if LOG_PERFORMANCE
            if (message.ExecutionReport().ExecutionType == ExecutionType.Trade)
            {
                ulong timestamp = loggerOut_.GetTimestamp();
                string id = message.ExecutionReport().ClientOrderId;
                loggerOut_.LogTimestamp(id, timestamp, "ExecReport");
            }
#endif
            var eh = this.ExecutionReport;
            if (eh != null)
            {
                var e = new ExecutionReportEventArgs (message);
                eh(this, e);
            }
        }

        void RaiseTradeTransactionReport(FxMessage message)
        {
            var eh = this.TradeTransactionReport;
            if (eh != null)
            {
                var e = new TradeTransactionReportEventArgs(message);
                eh(this, e);
            }
        }

        void RaisePositionReport(FxMessage message)
        {
            var eh = this.PositionReport;
            if (eh != null)
            {
                var e = new PositionReportEventArgs(message);
                eh(this, e);
            }
        }

        unsafe void RaiseNotification(FxMessage message)
        {
            var notification = message.Notification();

            if (notification.Type == NotificationType.Balance)
            {
                var e = new NotificationEventArgs<BalanceOperation>(notification)
                {
                    Data = new BalanceOperation(notification)
                };

                this.RaiseBalanceOperationNotification(e);
            }
            else
            {
                var e = new NotificationEventArgs(notification);
                this.RaiseNotification(e);
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

        void RaiseBalanceOperationNotification(NotificationEventArgs<BalanceOperation> e)
        {
            this.RaiseNotification(e);
            var eh = this.BalanceOperation;
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
                this.handle = new FxDataTrade();
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}
