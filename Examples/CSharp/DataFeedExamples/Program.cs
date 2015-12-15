namespace DataFeedExamples
{
    using System;

    class Program
    {
        static void Main()
        {
            var address = "tpdemo.fxopen.com";
            var username = "59932";
            var password = "8mEx7zZ2";

            //Library.WriteFullDumpOnError(@"D:\full.dmp");

            var example = new SymbolInfoExample(address, username, password);
            //var example = new TicksExample(address, username, password);
            //var example = new BarsHistoryExample(address, username, password);
            //var example = new StorageTicksHistoryExample(address, username, password);
            //var example = new StorageTicksRangeIteratorHistoryExample(address, username, password);
            //var example = new StorageBarsHistoryExample(address, username, password);
            //var example = new StorageUpdatingExample(address, username, password);
            // var example = new ImportQuotesExample();
            //var example = new StorageMultithreadingExample(address, username, password);

            using (example)
            {
                example.Run();
                Console.WriteLine("Press any key to continue ...");
                Console.ReadKey();
            }
        }
    }
}
