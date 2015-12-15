using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NLog;
using SoftFX.Extended;
using SoftFX.Extended.Events;
using TickTrader.Server.QuoteHistory.Engine.HistoryManagers;
using TickTrader.Server.QuoteHistory.Store.Ntfs;
using TickTrader.BusinessObjects.QuoteHistory.Engine;
using TickTrader.Common.Business;
using TickTrader.Server.QuoteHistory.Store;
using System.IO;
using TickTrader.BusinessObjects;
using TickTrader.BusinessObjects.QuoteHistory;
using TickTrader.Server.QuoteHistory.Caching;
using TickTrader.Server.Monitoring;
using TickTrader.Common.Time;
using TickTrader.Server.Common.Clients;

namespace RHost.Shared
{
    public class QuotesHistoryConnector
    {
        // singleton
        private QuotesHistoryConnector() { }
        private static readonly QuotesHistoryConnector StaticInstance = new QuotesHistoryConnector();
        public static QuotesHistoryConnector Instance { get { return StaticInstance; } }

        static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        
        //private static HistoryServiceClient _historyServiceClient = null;
        private static IHistorySource _historySource = null;

        private static NtfsHistoryStore _historyStore = null;
        private static IHistoryManager _historyManager = null;

        private static HistoryServiceClient _historyServiceClient = null;
        public string Address { get; set; }
        public string StorageFolderName { get; set; }

        public bool IsConnected
        {
            get
            {
                //return _historyServiceClient != null && _historySource != null;
                return false;
            }
        }

        private List<string> _symbolNames = null;

        public bool Connect()
        {
            if (!IsConnected)
            {
                if (string.IsNullOrEmpty(Address))
                {
                    return false;
                }

                if (_historyServiceClient == null)
                {
                    _historyServiceClient = new HistoryServiceClient(Address);
                }

                if (_historySource == null)
                {
                    _historySource = _historyServiceClient.ChannelFactory.CreateChannel();
                }
            }

            return IsConnected;
        }

        public bool Reconnect()
        {
            Disconnect();
            return Connect();
        }

        public void Disconnect()
        {
            if (_historyManager != null)
            {
                _historyManager.Flush();
                _historyManager.Dispose();
                _historyManager = null;
            }

            if (_historyStore != null)
            {
                _historyStore.Dispose();
                _historyStore = null;
            }

            if (_historyServiceClient != null)
			{
				_historyServiceClient = new HistoryServiceClient(Address);
            }

            if (_historySource == null)
			{
				_historySource = _historyServiceClient.ChannelFactory.CreateChannel();
            }
        }

        public void SetSymbols(List<string> symbols)
        {
            _symbolNames = symbols;
        }

        public List<string> GetSymbols()
        {
            if (_symbolNames != null && _symbolNames.Count > 0)
            {
                return _symbolNames;
            }

            if (!IsConnected)
            {
                if (!Connect())
                {
                    return _symbolNames;
                }
            }

            _symbolNames = _historySource.GetSupportedSymbols();

            if (_symbolNames == null || _symbolNames.Count == 0)
            {
                _logger.Debug("QuotesHistoryConnector.GetSymbols(): Failed to get symbols.");
                return _symbolNames;
            }

            return _symbolNames;
        }

        public HistoryBar GetHistory(string symbol, DateTime dateTime)
        {
            HistoryBar historyBar = new HistoryBar();

            if (!IsConnected)
            {
                if (!Connect())
                {
                    return historyBar;
                }
            }

            if (string.IsNullOrEmpty(StorageFolderName))
            {
                return historyBar;
            }

            List<string> symbolNames = GetSymbols();
            if (symbolNames == null || symbolNames.Count == 0)
            {
                _logger.Debug("QuotesHistoryConnector.GetHistory(): Failed to get symbols.");
                return historyBar;
            }

            try
            {
                if (_historyStore == null)
                {
                    string assemblyFileName = GetType().Assembly.Location;
                    FileInfo fileInfo = new FileInfo(assemblyFileName);

                    int historySourceVersion = _historySource.GetHistoryVersion();
                    _historyStore = new NtfsHistoryStore(Path.Combine(fileInfo.DirectoryName, "DCQuotesStorage", StorageFolderName + "_v" + historySourceVersion), new NullMonitoringService());
                    StoreStatus storeStatus = _historyStore.OpenOrCreate(historySourceVersion, false);

                    if (storeStatus != StoreStatus.Ok)
                    {
                        _logger.Debug("QuotesHistoryConnector.GetHistory(): Failed to open or create quotes storage. Store status: {0}", storeStatus);
                        return historyBar;
                    }
                }

                if (_historyManager == null)
                {
                    Dictionary<Periodicity, TimeInterval> periodicityMap = new Dictionary<Periodicity, TimeInterval>();
                    periodicityMap.Add(new Periodicity(TimeInterval.Second), TimeInterval.Hour);
                    periodicityMap.Add(new Periodicity(TimeInterval.Second, 10), TimeInterval.Day);
                    periodicityMap.Add(new Periodicity(TimeInterval.Minute), TimeInterval.Day);
                    periodicityMap.Add(new Periodicity(TimeInterval.Minute, 5), TimeInterval.Day);
                    periodicityMap.Add(new Periodicity(TimeInterval.Minute, 15), TimeInterval.Day);
                    periodicityMap.Add(new Periodicity(TimeInterval.Minute, 30), TimeInterval.Day);
                    periodicityMap.Add(new Periodicity(TimeInterval.Hour), TimeInterval.Month);
                    periodicityMap.Add(new Periodicity(TimeInterval.Hour, 4), TimeInterval.Month);
                    periodicityMap.Add(new Periodicity(TimeInterval.Day), TimeInterval.Year);
                    periodicityMap.Add(new Periodicity(TimeInterval.Week), TimeInterval.Year);
                    periodicityMap.Add(new Periodicity(TimeInterval.Month), TimeInterval.Year);

                    _historyManager = HistoryManager.Create(_historyStore, new SortedSet<string>(symbolNames),
                        periodicityMap, true,
                        Cache.ClientInstance("CacheInstance"), true, true, new NullMonitoringService(), null);
                }

                if (!_historyManager.BarsAreSynchronized(_historySource, symbol, new Periodicity(TimeInterval.Minute),
                        TickTrader.Common.Business.FxPriceType.Bid,
                        new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0),
                        new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour + 1, 0, 0), true))
                {
                    _historyManager.SynchronizeBars(_historySource, symbol, new Periodicity(TimeInterval.Minute),
                        TickTrader.Common.Business.FxPriceType.Bid,
                        new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0),
                        new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour + 1, 0, 0), true,
                        delegate (int completed, int total, DateTime start, TimeSpan length) { });
                }

                MarketHistoryItemsReport<HistoryBar> report = _historyManager.QueryBarHistory(dateTime, 1, symbol,
                    new Periodicity(TimeInterval.Minute).ToString(), TickTrader.Common.Business.FxPriceType.Bid);

                if (report.Items.Count > 0)
                {
                    historyBar = report.Items[0];
                }
                else
                {
                    _logger.Debug("QuotesHistoryConnector.GetHistory(): Failed to query bar history (empry collection returned).");
                }
            }
            catch (Exception ex)
            {
                _logger.Debug(ex, "QuotesHistoryConnector.GetHistory(): Failed to query bar history (exception): {0}", ex.Message);
            }

            return historyBar;
        }
    }
    
}