namespace SoftFX.AutomaticTrading.Hosting.DataSources
{
    using System.Collections.Generic;

    public interface IDataSourceProvider
    {
        string Name { get; }

        IEnumerable<IDataSource> DataSources { get; }
    }
}
