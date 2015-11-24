namespace DataFeedExamples
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using SoftFX.Extended;
    using SoftFX.Extended.Storage;

    class ImportQuotesExample : IDisposable
    {
        public ImportQuotesExample()
        {
            // create and specify log directory
            var assembly = Assembly.GetEntryAssembly();
            var root = assembly.Location;
            root = Path.GetDirectoryName(root);
            var storagePath = Path.Combine(root, "Storage");
            Directory.CreateDirectory(storagePath);
            this.storage = new DataFeedStorage(storagePath, StorageProvider.Ntfs, null, true);
        }

        public void Run()
        {
            this.Import("EURUSD");
        }

        void Import(string symbol)
        {
            var quotes = new List<Quote>();
            var creatingTime = DateTime.UtcNow;

            for (var index = 0; index < 10240; ++index)
            {
                creatingTime = creatingTime.AddMilliseconds(1);

                var bid = System.Math.Round(2 + Math.Sin(creatingTime.Millisecond) / 10, 5);
                var ask = bid + 0.0001;

                quotes.Add(new Quote(symbol, creatingTime, bid, ask));
            }

            this.storage.Import(quotes, false, true, true);

        }
        public void Dispose()
        {
            this.storage.Dispose();
            this.storage = null;
        }

        #region Members

        DataFeedStorage storage;

        #endregion
    }
}
