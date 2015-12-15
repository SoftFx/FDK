namespace QuotesCsvExporter
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Threading;
    using SoftFX.Extended;
    using SoftFX.Extended.Storage;
    using SoftFX.Extended.Storage.Sequences;

    class Exporter
    {
        public Exporter(string storageLocation, string outputFile, string symbol, BarPeriod period, DateTime from, DateTime to, double contractSize, bool removeDuplicateEntries, int progressResolution)
        {
            this.m_storageLocation = storageLocation;
            this.m_outputFile = outputFile;
            this.m_symbol = symbol;
            this.m_period = period;
            this.m_from = from;
            this.m_to = to;
            this.m_contractSize = contractSize;
            var interval = m_to - m_from;
            this.m_interval = interval.TotalSeconds;
            this.m_removeDuplicateEntries = removeDuplicateEntries;
            this.m_progressResolution = progressResolution;
            this.thread = new Thread(ThreadMethod);
        }

        #region Public Methods

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
            this.thread.Join();
        }

        #endregion

        #region Private Methods

        void ThreadMethod()
        {
            var status = true;
            string message = null;

            try
            {
                using (var storage = new DataFeedStorage(m_storageLocation, StorageProvider.Ntfs, null, false))
                {
                    using (var writer = new StreamWriter(m_outputFile))
                    {
                        if (m_period != null)
                        {
                            this.ExportBars(writer, storage);
                        }
                        else
                        {
                            this.ExportTicks(writer, storage);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                status = false;
                message = ex.ToString();
            }

            var func = this.Finish;
            if (func != null)
            {
                var e = new FinishEventArgs(status, message);
                func(this, e);
            }
        }

        void ExportTicks(StreamWriter writer, DataFeedStorage storage)
        {
            writer.WriteLine("Time,Ask,Bid,AskVolume,BidVolume");
            var quotes = new QuotesSingleSequence(storage.Offline, m_symbol, m_from, m_to, 1);

            var previousBidPrice = double.NaN;
            var previousAskPrice = double.NaN;

            foreach (var element in quotes)
            {
                if (!continueMonitoring)
                {
                    break;
                }

                var bid = element.Bids[0];
                var ask = element.Asks[0];

                if (this.m_removeDuplicateEntries)
                {
                    if (previousBidPrice == bid.Price && previousAskPrice == ask.Price)
                    {
                        continue;
                    }

                    previousBidPrice = bid.Price;
                    previousAskPrice = ask.Price;
                }
                var dateTime = element.CreatingTime.ToString("yyyy.MM.dd HH:mm:ss.fff");
                var bidPrice = bid.Price.ToString(CultureInfo.InvariantCulture);
                var askPrice = ask.Price.ToString(CultureInfo.InvariantCulture);
                var bidVolume = (bid.Volume / m_contractSize).ToString(CultureInfo.InvariantCulture);
                var askVolume = (ask.Volume / m_contractSize).ToString(CultureInfo.InvariantCulture);
                writer.WriteLine("{0},{1},{2},{3},{4}", dateTime, askPrice, bidPrice, askVolume, bidVolume);
                this.RaiseProgress(element.CreatingTime);
            }
        }

        void ExportBars(StreamWriter writer, DataFeedStorage storage)
        {
            writer.WriteLine("Time,Open,High,Low,Close,Volume");
            var bars = new Bars(storage.Offline, m_symbol, PriceType.Bid, m_period, m_from, m_to);

            foreach (var element in bars)
            {
                if (!continueMonitoring)
                {
                    break;
                }
                var dateTime = element.From.ToString("yyyy.MM.dd HH:mm:ss.fff");
                var stOpen = element.Open.ToString(CultureInfo.InvariantCulture);
                var stHigh = element.High.ToString(CultureInfo.InvariantCulture);
                var stLow = element.Low.ToString(CultureInfo.InvariantCulture);
                var stClose = element.Close.ToString(CultureInfo.InvariantCulture);
                var stVolume = element.Volume.ToString(CultureInfo.InvariantCulture);
                writer.WriteLine("{0},{1},{2},{3},{4},{5}", dateTime, stOpen, stHigh, stLow, stClose, stVolume);
                this.RaiseProgress(element.From);
            }
        }

        void RaiseProgress(DateTime time)
        {
            var interval = time - m_from;
            var value = (int)(interval.TotalSeconds / this.m_interval * this.m_progressResolution);
            if (this.progress == value)
            {
                return;
            }
            this.progress = value;
            var func = this.Progress;
            if (func != null)
            {
                var e = new ProgressEventArgs(value);
                func(this, e);
            }
        }

        #endregion

        #region Events

        public event EventHandler<ProgressEventArgs> Progress;
        public event EventHandler<FinishEventArgs> Finish;

        #endregion

        #region Input Members

        readonly string m_storageLocation;
        readonly string m_outputFile;
        readonly string m_symbol;
        readonly BarPeriod m_period;
        readonly DateTime m_from;
        readonly DateTime m_to;
        readonly double m_interval;
        readonly double m_contractSize;
        readonly bool m_removeDuplicateEntries;
        readonly int m_progressResolution;

        #endregion

        #region Members

        bool continueMonitoring = true;
        readonly Thread thread;
        int progress = 0;

        #endregion
    }
}
