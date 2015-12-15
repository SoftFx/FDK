namespace SoftFX.Extended.Storage
{
    using System;
    using TickTrader.Server.QuoteHistory.Engine.HistoryManagers;

	sealed class HistoryManagerAdapter : IDisposable
	{
		public HistoryManagerAdapter(IHistoryManager provider)
		{
            this.converter = new StorageConvert();
			this.Provider = provider;
		}

        public IHistoryManager Provider { get; private set; }

		public void Dispose()
		{
			var provider = this.Provider;
			this.Provider = null;
			if (provider != null)
				provider.Dispose();
		}

        public void Update(Quote quote)
		{
			var tick = this.converter.ToFeedTick(quote);
			this.Provider.Append(tick);
		}

		public void Update()
		{
			this.Provider.StopAppend();
		}

		readonly StorageConvert converter;
	}
}
