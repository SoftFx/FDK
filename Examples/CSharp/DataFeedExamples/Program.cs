namespace DataFeedExamples
{
    using System;

    class Program
    {
        static void Main()
        {
            string address = "tpdemo.fxopen.com";
            string username = "59932";
            string password = "8mEx7zZ2";
            var useFixProtocol = true;

            //Library.WriteFullDumpOnError(@"D:\full.dmp");

            var example = new SymbolInfoExample(address, username, password, useFixProtocol);
            //var example = new TicksExample(address, username, password, useFixProtocol);
            //var example = new BarsHistoryExample(address, username, password, useFixProtocol);
            //var example = new StorageTicksHistoryExample(address, username, password, useFixProtocol);
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
