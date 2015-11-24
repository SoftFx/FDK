using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftFX;
using ArbitrageAggregator.Saver;
using SoftFX.Extended;

namespace ArbitrageAggregator
{
	class Program
	{
		static void Main(string[] commandArgs)
		{
            Library.Path = Environment.CurrentDirectory;

            if (commandArgs.Count() != 1)
            {
                ShowHelp();
                return;
            }
            Type ChosenAdvisorType;
            switch (commandArgs[0].ToLower())
            {
                case "-watcher": 
                        ChosenAdvisorType = typeof(Watcher);
                        break;
                case "-save":
                        ChosenAdvisorType = typeof(QuotesSaver);
                        break;
                default:
                    ShowHelp();
                    return;
            }


			Dictionary<int, ConnectionStrings> args = AggrConnectionStringBuilder.CreateConnectionsStrings();
            using (MultiAdviserLauncher<int> launcher = new MultiAdviserLauncher<int>(args, ChosenAdvisorType))
			{
				Console.WriteLine("Press any key to stop...");
				launcher.Start();
				Console.ReadKey();
				launcher.Stop();
			}
			Log.Shutdown();
		}

        static void ShowHelp()
        {
            Console.WriteLine("Supported parameters: -watcher;-save");
        }
	}
}
