using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftFX.Extended;
using SoftFX.Adapters.C;

namespace AdviserExamples
{
	class Program
	{
		static void Main(string[] args)
		{
			//Library.Path = @"<FRE>";
            Library.Path = Environment.CurrentDirectory;

			string address = Settings1.Default.Server;
			string username = Settings1.Default.Login;
			string password = Settings1.Default.Password;

			Console.WriteLine();

			MyAdviser adviser = new MyAdviser();
            adviser.MaximumOpenVolumeLots = Settings1.Default.MaximumOpenVolumeLots;
            adviser.RateTrailingDistance = Settings1.Default.RateTrailingDistance;
            using (CLauncher launcher = new CLauncher(address, username, password, Environment.CurrentDirectory, Settings1.Default.Symbol, PriceType.Bid, BarPeriod.M1, adviser))
			{
				launcher.Start();
				Console.ReadKey();
				launcher.Stop();
			}
		}
	}
}
