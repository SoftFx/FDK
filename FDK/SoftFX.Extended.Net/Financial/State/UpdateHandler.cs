namespace SoftFX.Extended.Financial
{
    using System;
    using SoftFX.Extended.Events;

    sealed class UpdateHandler
    {
        readonly Processor processor;
        readonly Action<SymbolInfo[], AccountInfo, Quote> updateCallback;

        public object SyncRoot { get; private set; }

        public UpdateHandler(DataTrade trade, DataFeed feed, Action<SymbolInfo[], AccountInfo, Quote> updateCallback, Processor processor)
        {
            if (trade == null)
                throw new ArgumentNullException(nameof(trade));

            if (feed == null)
                throw new ArgumentNullException(nameof(feed));

            if (updateCallback == null)
                throw new ArgumentNullException(nameof(updateCallback));

            if (processor == null)
                throw new ArgumentNullException(nameof(processor));

            this.updateCallback = updateCallback;
            this.processor = processor;

            this.SyncRoot = new object();

            feed.SymbolInfo += this.OnSymbolInfo;
            feed.Tick += this.OnTick;

            trade.AccountInfo += this.OnAccountInfo;
            trade.BalanceOperation += this.OnBalanceOperation;
            trade.ExecutionReport += this.OnExecutionReport;
            trade.PositionReport += this.OnPositionReport;
        }

        #region Events Handlers

        void OnSymbolInfo(object sender, SymbolInfoEventArgs e)
        {
            lock (this.SyncRoot)
            {
                this.updateCallback(e.Information, null, null);
                this.processor.WakeUp();
            }
        }

        void OnTick(object sender, TickEventArgs e)
        {
            var quote = e.Tick;

            lock (this.SyncRoot)
            {
                this.updateCallback(null, null, quote);
                this.processor.WakeUp();
            }
        }

        void OnAccountInfo(object sender, AccountInfoEventArgs e)
        {
            lock (this.SyncRoot)
            {
                this.updateCallback(null, e.Information, null);
                this.processor.WakeUp();
            }
        }

        void OnBalanceOperation(object sender, NotificationEventArgs<BalanceOperation> e)
        {
            lock (this.SyncRoot)
            {
                this.processor.WakeUp();
            }
        }

        void OnExecutionReport(object sender, ExecutionReportEventArgs e)
        {
            lock (this.SyncRoot)
            {
                this.processor.WakeUp();
            }
        }

        void OnPositionReport(object sender, PositionReportEventArgs e)
        {
            lock (this.SyncRoot)
            {
                this.processor.WakeUp();
            }
        }

        #endregion
    }
}
