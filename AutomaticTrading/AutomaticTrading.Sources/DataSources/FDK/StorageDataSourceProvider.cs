namespace AutomaticTrading.Sources.DataSources
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.IO;
    using AutomaticTrading.Sources.Properties;
    using SoftFX.AutomaticTrading.Hosting.DataSources;
    using SoftFX.Extended;
    using SoftFX.Extended.Storage;

    public class StorageDataSourceProvider : IDataSourceProvider, IDisposable
    {
        readonly DataFeedStorage storage;
        readonly DataFeed dataFeed;

        static StorageDataSourceProvider()
        {
            Library.Path = "<FRE>";
        }

        public StorageDataSourceProvider()
        {
            this.dataFeed = CreateDataFeed();
            this.storage = new DataFeedStorage(Settings.Default.DataSources_FDK_StorageLocation, StorageProvider.Ntfs, this.dataFeed, flushOnDispose: true);
            this.dataFeed.Start();

            var symbols = this.GetSymbols();

            var sources = symbols.Select(o =>
                {
                    return new []
                    {
                        new SymbolBarsDataSource(this.dataFeed, this.storage, o, PriceType.Bid),
                        new SymbolBarsDataSource(this.dataFeed, this.storage, o, PriceType.Ask)
                    };
                });

            this.DataSources = sources.SelectMany(o => o)
                                      .ToArray();
        }

        static DataFeed CreateDataFeed()
        {
            var builder = new FixConnectionStringBuilder
            {
                SecureConnection = true,
                Port = 5003,
                Address = Settings.Default.DataSources_FDK_Server,
                Username = Settings.Default.DataSources_FDK_Username,
                Password = Settings.Default.DataSources_FDK_Password,
            };

            var dataFeed = new DataFeed(builder.ToString());
            return dataFeed;
        }

        public string Name
        {
            get { return "FDK"; }
        }

        public IEnumerable<IDataSource> DataSources { get; private set; }

        public void Dispose()
        {
            this.storage.Dispose();
            this.dataFeed.Stop();
            this.dataFeed.Dispose();
        }

        IEnumerable<string> GetSymbols()
        {
            yield return "USDRUB";
            //return Directory.EnumerateDirectories(this.storage.Location)
            //                .Select(o => o.Split(Path.DirectorySeparatorChar).Last())
            //                .Where(o => !o.StartsWith("."));
        }
    }
}
