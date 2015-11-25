namespace SoftFX.Extended.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using SoftFX.Extended.Core;
    using SoftFX.Extended.Events;
    using TickTrader.BusinessObjects;
    using TickTrader.BusinessObjects.QuoteHistory.Engine;
    using TickTrader.Common.Business;
    using TickTrader.Common.Time;
    using TickTrader.Server.Monitoring;
    using TickTrader.Server.QuoteHistory.Caching;
    using TickTrader.Server.QuoteHistory.Engine.HistoryManagers;
    using TickTrader.Server.QuoteHistory.Interoperability;
    using TickTrader.Server.QuoteHistory.Store;

	/// <summary>
	/// High level functionality of data feed history local cache.
	/// </summary>
	public class DataFeedStorage : IDisposable
	{
		#region Construction

		/// <summary>
		/// Creates and initializes a new data feed storage.
		/// </summary>
		/// <param name="location">Specified location for data feed history local cache.</param>
		/// <param name="storageProviderType">Type of storage provider.</param>
		/// <param name="storageVersion">Storage version.</param>
		/// <param name="dataFeed">Can be null, in this case you can use storage in offline mode only.</param>
		/// <param name="flushOnDispose">If true, then quotes cache in memory will be flushed to hard drive.</param>
		/// <param name="saveTickLevel2History">Save incomning ticks as level2 history or not</param>
		/// <exception cref="System.ArgumentNullException">If location is null.</exception>
		public DataFeedStorage(string location, Type storageProviderType, int storageVersion, DataFeed dataFeed, bool flushOnDispose, bool saveTickLevel2History)
		{
			if (location == null)
				throw new ArgumentNullException(nameof(location), "Location argument can not be null.");

			if (location.Length == 0)
				throw new ArgumentException("Location argument can not be empty string", nameof(location));

			if (location[location.Length - 1] == Path.DirectorySeparatorChar)
				location = location.Substring(0, location.Length - 1);

			this.saveTickLevel2History = saveTickLevel2History;
			this.storageVersion = storageVersion;
			this.historyFeed = dataFeed;
			this.Location = location;
			this.flushOnDispose = flushOnDispose;

			this.store = StorageProvider.CreateStore(storageProviderType, location, NullMonitoringService);

			var status = this.store.OpenOrCreate(storageVersion, forceCreateNewVersion: true);

            if (dataFeed != null)
                this.source = new DataFeedHistorySource(dataFeed, attemptsNumber: 1);

            this.Bind(dataFeed);

			this.continueMonitoring = true;
			this.thread = new Thread(this.Loop);
			this.thread.Start();

			this.Offline = new SmartStorage(this);
			if (this.source != null)
				this.Online = new SmartStorage(this, this.source);
		}

        /// <summary>
        /// Creates and initializes a new data feed offline storage.
		/// </summary>
		/// <param name="location">Specified location for data feed history local cache.</param>
		/// <param name="storageProviderType">Type of storage provider.</param>
	    /// <param name="storageVersion">Storage version.</param>
		/// <param name="flushOnDispose">If true, then quotes cache in memory will be flushed to hard drive.</param>
		/// <param name="saveTickLevel2History">Save incomning ticks as level2 history or not</param>
		/// <exception cref="System.ArgumentNullException">If location is null.</exception>
        public DataFeedStorage(string location, Type storageProviderType, int storageVersion, bool flushOnDispose, bool saveTickLevel2History)
            : this(location, storageProviderType, storageVersion, null, flushOnDispose, saveTickLevel2History)
        {
        }

		/// <summary>
		/// Creates and initializes a new data feed storage.
		/// </summary>
		/// <param name="location">Specified location for data feed history local cache.</param>
		/// <param name="storageProviderType">Type of storage provider.</param>
		/// <param name="storageVersion">Storage version.</param>
		/// <param name="dataFeed">Can be null, in this case you can use storage in offline mode only.</param>
		/// <param name="flushOnDispose">If true, then quotes cache in memory will be flushed to hard drive.</param>
		/// <exception cref="System.ArgumentNullException">If location is null.</exception>
		public DataFeedStorage(string location, Type storageProviderType, int storageVersion, DataFeed dataFeed, bool flushOnDispose)
            : this(location, storageProviderType, storageVersion, dataFeed, flushOnDispose, true)
		{
		}

        /// <summary>
        /// Creates and initializes a new data feed offline storage.
        /// </summary>
        /// <param name="location">Specified location for data feed history local cache.</param>
        /// <param name="storageProviderType">Type of storage provider.</param>
        /// <param name="storageVersion">Storage version.</param>
        /// <param name="flushOnDispose">If true, then quotes cache in memory will be flushed to hard drive.</param>
        /// <exception cref="System.ArgumentNullException">If location is null.</exception>
        public DataFeedStorage(string location, Type storageProviderType, int storageVersion, bool flushOnDispose)
            : this(location, storageProviderType, storageVersion, null, flushOnDispose)
        {
        }

		/// <summary>
		/// Creates and initializes a new data feed storage Ver.1.
		/// </summary>
		/// <param name="location">Specified location for data feed history local cache.</param>
		/// <param name="storageProviderType">Type of storage provider.</param>
		/// <param name="dataFeed">Can be null, in this case you can use storage in offline mode only.</param>
		/// <param name="flushOnDispose">If true, then quotes cache in memory will be flushed to hard drive.</param>
		/// <exception cref="System.ArgumentNullException">If location is null.</exception>
		public DataFeedStorage(string location, Type storageProviderType, DataFeed dataFeed, bool flushOnDispose)
            : this(location, storageProviderType, 1, dataFeed, flushOnDispose)
		{
		}

        /// <summary>
        /// Creates and initializes a new data feed offline storage Ver.1.
        /// </summary>
        /// <param name="location">Specified location for data feed history local cache.</param>
        /// <param name="storageProviderType">Type of storage provider.</param>
        /// <param name="flushOnDispose">If true, then quotes cache in memory will be flushed to hard drive.</param>
        /// <exception cref="System.ArgumentNullException">If location is null.</exception>
        public DataFeedStorage(string location, Type storageProviderType, bool flushOnDispose)
            : this(location, storageProviderType, null, flushOnDispose)
        {
        }

		#endregion

		#region Event Handlers

		void OnTick(object sender, TickEventArgs e)
		{
			lock (this.synchronizer)
			{
				this.first.Add(e.Tick);
			}
			this.syncEvent.Set();
		}

		void OnLogout(object sender, LogoutEventArgs e)
		{
			lock (this.synchronizer)
			{
				this.first.Add(null);
			}
			this.syncEvent.Set();
		}

		#endregion

		#region Thread Methods

		void Loop()
		{
			for (this.syncEvent.WaitOne(); this.continueMonitoring; this.syncEvent.WaitOne())
			{
				this.SafeStep();
			}
		}

		void SafeStep()
		{
			try
			{
				this.Step();
			}
			catch
			{
			}

			this.second.Clear();
		}

		void Step()
		{
			lock (this.synchronizer)
			{
				var temp = this.first;
				this.first = this.second;
				this.second = temp;
			}

			foreach (var element in this.second)
			{
				if (element != null)
					this.Update(element);
				else
					this.Update();
			}
		}

		void Update(Quote quote)
		{
			var adapter = this.GetOrCreateHistoryManagerAdapter(quote.Symbol);
			adapter.Update(quote);
		}

		void Update()
		{
			var symbols = this.GetSymbols();
			foreach (var symbol in symbols)
			{
				var adapter = this.GetOrCreateHistoryManagerAdapter(symbol);
				adapter.Update();
			}
		}

		#endregion

		#region Static Members

		static DataFeedStorage()
		{
			Native.Initialize();

            NullMonitoringService = new NullMonitoringService();
            NullMonitoringItem = new NullMonitoringService.NullComponentState();

		}

        static readonly IMonitoringService NullMonitoringService;
        static readonly IMonitoringItem NullMonitoringItem;

        static readonly Dictionary<Periodicity, TimeInterval> PeriodicityMappingVer1 = new Dictionary<Periodicity, TimeInterval>
	    {
	        { new Periodicity(TimeInterval.Second, 1),  TimeInterval.Hour   },
	        { new Periodicity(TimeInterval.Second, 10), TimeInterval.Day    },
	        { new Periodicity(TimeInterval.Minute, 1),  TimeInterval.Day    },
	        { new Periodicity(TimeInterval.Minute, 5),  TimeInterval.Day    },
	        { new Periodicity(TimeInterval.Minute, 15), TimeInterval.Day    },
	        { new Periodicity(TimeInterval.Minute, 30), TimeInterval.Day    },
	        { new Periodicity(TimeInterval.Hour, 1),    TimeInterval.Month  },
	        { new Periodicity(TimeInterval.Hour, 4),    TimeInterval.Month  },
	        { new Periodicity(TimeInterval.Day, 1),     TimeInterval.Year   },
	        { new Periodicity(TimeInterval.Week, 1),    TimeInterval.Year   },
	        { new Periodicity(TimeInterval.Month, 1),   TimeInterval.Year   },
	    };

	    static readonly Dictionary<Periodicity, TimeInterval> PeriodicityMappingVer0 = new Dictionary<Periodicity, TimeInterval>
	    {
	        { new Periodicity(TimeInterval.Second, 1),  TimeInterval.Hour   },
	        { new Periodicity(TimeInterval.Second, 10), TimeInterval.Day    },
	        { new Periodicity(TimeInterval.Minute, 1),  TimeInterval.Day    },
	        { new Periodicity(TimeInterval.Minute, 5),  TimeInterval.Month  },
	        { new Periodicity(TimeInterval.Minute, 15), TimeInterval.Month  },
	        { new Periodicity(TimeInterval.Minute, 30), TimeInterval.Month  },
	        { new Periodicity(TimeInterval.Hour, 1),    TimeInterval.Year   },
	        { new Periodicity(TimeInterval.Hour, 4),    TimeInterval.Year   },
	        { new Periodicity(TimeInterval.Day, 1),     TimeInterval.Year   },
	        { new Periodicity(TimeInterval.Week, 1),    TimeInterval.Year   },
	        { new Periodicity(TimeInterval.Month, 1),   TimeInterval.Year   },
	    };

    	/// <summary>
		/// 
		/// </summary>
		/// <param name="qhVersion"></param>
		/// <returns></returns>
	    internal static Dictionary<Periodicity, TimeInterval> GetSupportedPeriodicityToStoreLevel(int qhVersion)
	    {
	        switch (qhVersion)
	        {
                case 0:
	                return PeriodicityMappingVer0;
                case 1:
                    return PeriodicityMappingVer1;
                default:
                    goto case 0;
	        }
	    }

		#endregion

		#region Properties

		/// <summary>
		/// Gets the data feed storage location.
		/// </summary>
		public string Location { get; private set; }

		/// <summary>
		/// Local quotes storage.
		/// </summary>
		public IStorage Offline { get; private set; }

		/// <summary>
		/// Server quotes storage.
		/// </summary>
		public IStorage Online { get; private set; }

		#endregion

		#region Internal Properties

		internal IHistoryStore Store
		{
			get
			{
				return this.store;
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Binds / unbinds data feed for tick events.
		/// Storage uses tick events, if newDataFeed is not null
		/// Storage does not use tick events, if newDataFeed is null
		/// </summary>
		/// <param name="newDataFeed">A data feed instance or null</param>
		public void Bind(DataFeed newDataFeed)
		{
			lock (this.synchronizer)
			{
				if (this.updateFeed != newDataFeed)
				{
					this.DoBind(newDataFeed);
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="symbol"></param>
		/// <param name="from"></param>
		/// <param name="to"></param>
		public void RebuildBarsFromBars(string symbol, string from, string to)
		{
			var start = DateTime.MinValue.AddYears(1);
			var finish = DateTime.MaxValue.AddYears(-1);

			try
			{
                var periodicities = new[]
                {
				    Periodicity.Parse(from),
				    Periodicity.Parse(to),
                };

				var writer = BulkHistoryWriter.Create(this.store, DataFeedStorage.GetSupportedPeriodicityToStoreLevel(this.storageVersion), NullMonitoringService);
				writer.RebuildBarsFromBars(symbol, FxPriceType.Bid, start, finish, periodicities);
				writer.RebuildBarsFromBars(symbol, FxPriceType.Ask, start, finish, periodicities);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		/// <summary>
		/// The method append a new quote to the storage.
		/// </summary>
		/// <param name="quote"></param>
		public void Append(Quote quote)
		{
			var manager = this.GetOrCreateHistoryManager(quote.Symbol);
			var converter = new StorageConvert();
			var tick = converter.ToFeedTick(quote);
			manager.Append(tick);
		}

		/// <summary>
		/// The method stops quotes appending for a symbol.
		/// </summary>
		/// <param name="symbol"></param>
		public void StopAppend(string symbol)
		{
			var manager = this.GetOrCreateHistoryManager(symbol);
			manager.StopAppend();
		}

        /// <summary>
        /// Imports quotes to storage.
        /// </summary>
        /// <param name="ticks">Ticks, which should be imported.</param>
        /// <param name="level2">Ignore depth of level2, if false.</param>
        /// <param name="overwriteTickChainsWithEqualTime"></param>
        /// <param name="rebuild">The method rebuilds all bars, if true.</param>
        public void Import(IEnumerable<Quote> ticks, bool level2, bool overwriteTickChainsWithEqualTime, bool rebuild)
        {
            var groupdeBySymbols = ticks.GroupBy(t => t.Symbol);
            foreach (var symbolsTicks in groupdeBySymbols)
            {
                var groupedTicksValues = symbolsTicks.Select(qt =>
                {
                    var tl2Bids = qt.Bids.Select(qa => new FeedLevel2Record
                    {
                        Price = (decimal)qa.Price,
                        Type = FxPriceType.Bid,
                        Volume = qa.Volume
                    });
                    var tl2Asks = qt.Asks.Select(qa => new FeedLevel2Record
                    {
                        Price = (decimal)qa.Price,
                        Type = FxPriceType.Ask,
                        Volume = qa.Volume
                    });

                    var ret = new TickValue(qt.CreatingTime, tl2Asks.Concat(tl2Bids));
                    return ret;
                });

                var provider = this.GetOrCreateHistoryManager(symbolsTicks.Key);
                provider.ImportTicks(groupedTicksValues, symbolsTicks.Key, level2, true, overwriteTickChainsWithEqualTime ? TicksImportRules.Replace
                                                                                                                          : TicksImportRules.Append, null);
                if (rebuild)
                {
                }
            }
        }

        /// <summary>
        /// Imports bars to storage.
        /// </summary>
        /// <param name="symbol">Symbol of importing bars</param>
        /// <param name="period">Period of importing bars</param>
        /// <param name="bars">Importing bars</param>
        /// <param name="priceType">Price type of importing bars</param>
        /// <param name="overwriteBarChainsWithEqualTime"></param>
        public void Import(string symbol, BarPeriod period, IEnumerable<Bar> bars, PriceType priceType, bool overwriteBarChainsWithEqualTime)
        {
            var provider = this.GetOrCreateHistoryManager(symbol);
            var fxPeriod = StorageConvert.ToPeriodicity(period);
            var fxPriceType = StorageConvert.ToFxPriceType(priceType);
            var fxBars = bars.Select(StorageConvert.ToHistoryBar);

            provider.ImportBars(
                fxBars,
                symbol,
                fxPeriod,
                fxPriceType,
                true,
                overwriteBarChainsWithEqualTime ? BarsImportRules.Replace : BarsImportRules.Skip,
                null
                );
        }

        /// <summary>
        /// Flushes all data to storage.
        /// </summary>
        public void Dispose()
        {
            if (this.updateFeed != null)
            {
                this.updateFeed.Logout -= this.OnLogout;
                this.updateFeed.Tick -= this.OnTick;
                this.updateFeed = null;
            }

            if (this.thread != null)
            {
                this.continueMonitoring = false;
                this.syncEvent.Set();
                this.thread.Join();
                this.syncEvent.Dispose();
            }

            foreach (var element in this.symbol2cache)
            {
                element.Value.Dispose();
            }

            if (this.store != null)
            {
                this.store.Dispose();
                this.store = null;
            }
        }

		#endregion

		#region Synchronization

		/// <summary>
		/// The method synchronizes ticks/level2.
		/// </summary>
		/// <param name="symbol"></param>
		/// <param name="startTime"></param>
		/// <param name="endTime"></param>
		/// <param name="marketDepth"></param>
		public void Synchronize(string symbol, DateTime startTime, DateTime endTime, int marketDepth)
		{
            if (this.source == null)
                throw new InvalidOperationException("Can't synchronize in offline mode.");

            var manager = this.GetOrCreateHistoryManager(symbol);
			var includeLevel2 = marketDepth != 1;

            manager.SynchronizeTicks(this.source, symbol, includeLevel2, startTime, endTime, false, NullCallback);
		}

		/// <summary>
		/// The method synchronizes bars.
		/// </summary>
		/// <param name="symbol"></param>
		/// <param name="priceType"></param>
		/// <param name="period"></param>
		/// <param name="startTime"></param>
		/// <param name="endTime"></param>
		public void Synchronize(string symbol, PriceType priceType, BarPeriod period, DateTime startTime, DateTime endTime)
		{
            if (this.source == null)
                throw new InvalidOperationException("Can't synchronize in offline mode.");

            var manager = this.GetOrCreateHistoryManager(symbol);
			var periodicity = StorageConvert.ToPeriodicity(period);
			var fxPriceType = StorageConvert.ToFxPriceType(priceType);

            manager.SynchronizeBars(this.source, symbol, periodicity, fxPriceType, startTime, endTime, false, NullCallback);
		}

		static void NullCallback(int completed, int total, DateTime completedStart, TimeSpan completedLength)
		{
		}

		#endregion

		#region Internal Methods

		void DoBind(DataFeed newDataFeed)
		{
			if (this.updateFeed != null)
			{
				this.updateFeed.Tick -= this.OnTick;
				this.updateFeed.Logout -= this.OnLogout;
				this.updateFeed = null;
			}

            if (newDataFeed != null)
            {
                newDataFeed.Logout += this.OnLogout;
            }

			try
			{
                if (newDataFeed != null)
                {
                    newDataFeed.Tick += this.OnTick;
                }
				this.updateFeed = newDataFeed;
			}
			catch
			{
                if (newDataFeed != null)
                {
                    newDataFeed.Logout -= this.OnLogout;
                }
				throw;
			}
		}

		HistoryManagerAdapter GetOrCreateHistoryManagerAdapter(string symbol)
		{
			HistoryManagerAdapter result;

			lock (this.synchronizer)
			{
				if (!this.symbol2cache.TryGetValue(symbol, out result))
				{
                    var symbols = new HashSet<string>
                    {
                        symbol
                    };

					var storageVersion = this.storageVersion;

					if (this.historyFeed != null)
						storageVersion = this.historyFeed.Server.GetQuotesHistoryVersion();

					var provider = HistoryManager.Create(
                        storageVersion,
                        this.store,
                        symbols,
                        DataFeedStorage.GetSupportedPeriodicityToStoreLevel(this.storageVersion),
                        this.saveTickLevel2History,
                        Cache.ClientInstance(Guid.NewGuid().ToString()),
                        true,
                        this.flushOnDispose,
                        NullMonitoringService,
                        NullMonitoringItem);

					result = new HistoryManagerAdapter(provider);
					this.symbol2cache[symbol] = result;
				}
			}

			return result;
		}

		internal IHistoryManager GetOrCreateHistoryManager(string symbol)
		{
			var adapter = this.GetOrCreateHistoryManagerAdapter(symbol);
			return adapter.Provider;
		}

		string[] GetSymbols()
		{
			lock (this.synchronizer)
			{
				return this.symbol2cache.Keys.ToArray();
			}
		}

		#endregion

		#region Members

		readonly bool saveTickLevel2History;
		readonly int storageVersion;
		readonly bool flushOnDispose = false;
		readonly object synchronizer = new object();

		readonly DataFeed historyFeed;
		DataFeed updateFeed;

		volatile bool continueMonitoring;
		Thread thread;

		IList<Quote> first = new List<Quote>();
		IList<Quote> second = new List<Quote>();

		readonly AutoResetEvent syncEvent = new AutoResetEvent(false);
		readonly IHistorySource source;

		IHistoryStore store;
		readonly IDictionary<string, HistoryManagerAdapter> symbol2cache = new Dictionary<string, HistoryManagerAdapter>();

		#endregion
	}
}
