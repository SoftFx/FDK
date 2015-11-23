namespace SoftFX.AutomaticTrading.Hosting.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SoftFX.AutomaticTrading.Hosting.DataSources;

    public class DataSourceManager : IDisposable
    {
        public DataSourceManager(IDataSourceProviderDiscovery discovery)
        {
            if (discovery == null)
                throw new ArgumentNullException("discovery");

            this.Providers = discovery.Providers;

            if (this.Providers == null)
                throw new ArgumentException("Providers cannot be null.", "discovery");
        }

        public IEnumerable<IDataSourceProvider> Providers { get; private set; }

        public void Dispose()
        {
            foreach (var provider in this.Providers.OfType<IDisposable>())
            {
                provider.Dispose();
            }
        }
    }


}
