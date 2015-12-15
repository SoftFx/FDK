namespace FeedRebuild
{
    using System;
    using SoftFX.Extended;
    using SoftFX.Extended.Storage;

	class Program
	{
		static void Main(string[] args)
		{
            // Bootstrap FDK libraries
            Bootstrapper.Initialize();

            if (args.Length == 4)
			{
				var location = args[0];
				var symbol = args[1];
				var source = args[2];
				var target = args[3];

				using (var storage = new DataFeedStorage(location, StorageProvider.Ntfs, null, true))
				{
					storage.RebuildBarsFromBars(symbol, source, target);
				}
			}
			else
			{
				Console.WriteLine("Usage:");
				Console.WriteLine("\tFeedRebuilder <location> <symbol> <source periodicity> <target periodicity>");
				Console.WriteLine("List of supported periodicities:");
				Console.WriteLine("\tS1 S10 M1 M5 M15 M30 H1 H4 D1 W1 MN1");
				Console.WriteLine("Example:");
				Console.WriteLine("\tFeedRebuilder C:\\Storage EURUSD M15 M30");
			}
		}
	}
}
