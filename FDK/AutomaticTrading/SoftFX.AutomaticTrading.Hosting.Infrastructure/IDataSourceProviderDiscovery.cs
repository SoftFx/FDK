namespace SoftFX.AutomaticTrading.Hosting.Infrastructure
{
    using System.Collections.Generic;
    using SoftFX.AutomaticTrading.Hosting.DataSources;

    public interface IDataSourceProviderDiscovery
    {
        IEnumerable<IDataSourceProvider> Providers { get; }
    }
}
