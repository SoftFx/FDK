namespace QuotesDownloader
{
    using System;
    using System.IO;
    using System.Threading;
    using SoftFX.Extended;
    using SoftFX.Extended.Storage;

    public class Downloader
    {
        Downloader()
        {
            this.continueMonitoring = true;
            this.thread = new Thread(ThreadMethod);
        }

        public Downloader(DataFeed dataFeed, Type storageType, String location, String symbol, DateTime from, DateTime to, Boolean includeLevel2)
            : this()
        {
            this.dataFeed = dataFeed;
            this.storageType = storageType;
            this.location = location;
            this.symbol = symbol;
            this.from = from;
            this.to = to;
            this.includeLevel2 = includeLevel2;
        }

        public Downloader(DataFeed dataFeed, Type storageType, String location, String symbol, DateTime from, DateTime to, PriceType priceType, BarPeriod period)
            : this()
        {
            this.dataFeed = dataFeed;
            this.storageType = storageType;
            this.location = location;
            this.symbol = symbol;
            this.from = from;
            this.to = to;
            this.priceType = priceType;
            this.period = period;
        }

        #region Properties

        public bool IsFinished
        {
            get
            {
                return this.thread == null;
            }
        }

        #endregion

        #region Control Methods

        public void Start()
        {
            this.thread.Start();
        }

        public void Stop()
        {
            this.continueMonitoring = false;
        }

        public void Join()
        {
            var thread = this.thread;
            if (thread != null)
            {
                thread.Join();
            }
        }

        #endregion

        #region Events

        public event EventHandler<EventArgs<string>> Message;
        public event EventHandler<EventArgs> Finish;

        #endregion

        #region Helper Methods

        void ThreadMethod()
        {
            try
            {
                var from = this.from;
                var to = this.to;
                this.AdjustTimes(ref from, ref to);
                Directory.CreateDirectory(this.location);

                var attempt = 1;

                using (var storage = new DataFeedStorage(this.location, this.storageType, this.dataFeed, true))
                {
                    for (var current = from; this.continueMonitoring && (current < to);)
                    {
                        this.Log("Synchronizing = {0}; attempt = {1}", current, attempt);

                        var next = this.Download(storage, current);
                        if (current == next)
                        {
                            attempt++;
                        }
                        else
                        {
                            attempt = 1;
                            current = next;
                            this.Log("Synchronized");
                        }
                    }
                    this.Log("Flushing quotes storage");
                }
                this.Log("Quotes storage has been flushed");
            }
            catch (Exception ex)
            {
                this.Log(ex.Message);
            }
            this.RaiseFinish();
            this.thread = null;
        }

        void AdjustTimes(ref DateTime startTime, ref DateTime endTime)
        {
            var info = this.dataFeed.Server.GetQuotesHistoryFiles(this.symbol, this.includeLevel2, startTime);
            if (startTime < info.FromAll)
            {
                startTime = info.FromAll;
                this.Log("Adjust start time = {0}", startTime);
            }
            if (endTime > info.ToAll)
            {
                endTime = info.ToAll;
                this.Log("Adjust end time = {0}", endTime);
            }
        }

        DateTime Download(DataFeedStorage storage, DateTime time)
        {
            try
            {
                var result = time + Interval;
                if (this.period == null)
                {
                    var marketDepth = includeLevel2 ? 0 : 1;
                    storage.Synchronize(this.symbol, time, result, marketDepth);
                }
                else
                {
                    storage.Synchronize(this.symbol, this.priceType, this.period, time, result);
                }
                return result;

            }
            catch (Exception ex)
            {
                this.Log("Exception = {0}", ex.Message);
                Thread.Sleep(1000);
                return time;
            }
        }

        void Log(string text)
        {
            var eh = this.Message;

            if (eh != null)
            {
                var e = new EventArgs<string>(text);
                eh(this, e);
            }
        }

        void Log(string format, params object[] arguments)
        {
            var text = string.Format(format, arguments);
            this.Log(text);
        }

        void RaiseFinish()
        {
            var eh = this.Finish;
            if (eh != null)
            {
                eh(this, EventArgs.Empty);
            }
        }

        #endregion

        #region Members

        volatile bool continueMonitoring;
        readonly DataFeed dataFeed;
        readonly Type storageType;
        readonly string location;
        readonly string symbol;
        readonly DateTime from;
        readonly DateTime to;
        readonly Boolean includeLevel2;
        readonly PriceType priceType;
        readonly BarPeriod period;
        Thread thread;

        #endregion

        #region Constants

        static readonly TimeSpan Interval = new TimeSpan(1, 0, 0);

        #endregion
    }
}
