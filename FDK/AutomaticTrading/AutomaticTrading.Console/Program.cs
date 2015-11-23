namespace AutomaticTrading.Console
{
    using System;
    using System.Linq;
    using AutomaticTrading.Sources.DataSources;
    using SoftFX.AutomaticTrading.Hosting.Infrastructure;

    class Program
    {
        static void Main(string[] args)
        {
            using (var provider = new StorageDataSourceProvider())
            {
                foreach (var source in provider.DataSources)
                {
                    Console.WriteLine(source.Name);
                }

                Console.ReadKey();

                var s = provider.DataSources.First();
                
                var monitor = new RealTimeDataMonitor(s, TimeSpan.FromSeconds(10));
                monitor.NewData += OnNewData;
                monitor.Start();

                
                Console.WriteLine("Press any key..");
                Console.ReadKey();

                monitor.Stop();
            }
        }

        static void OnNewData(object sender, object args)
        {
            Console.WriteLine(args);
        }
    }
}
