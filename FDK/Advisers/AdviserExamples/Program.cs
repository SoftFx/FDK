using System;
using System.Collections.Generic;
using System.Text;
using SoftFX.Extended;
using AdviserExamples.MQL4;
using SoftFX;

namespace AdviserExamples
{
	class Program
	{
		static void Main()
		{
			Library.Path = "<FRE>";

			//string address = "fxopentrader.staging.fxopen.org";
			//string username = "26";
			//string password = "26";

			Dictionary<int, ConnectionStrings> args = new Dictionary<int, ConnectionStrings>();
			args[1] = CreateConnectionStrings(1, 1);
			//args[11] = CreateConnectionStrings(11);


			using(MultiAdviserLauncher<int> launcher = new MultiAdviserLauncher<int>(args, typeof(MultiAdviserExample)))
			{
				launcher.Start();
				Console.ReadKey();
				launcher.Stop();
			}
		}
		private static ConnectionStrings CreateConnectionStrings(string address, string username, string password)
		{
			string feedConnectionString = FixAdviserLauncher.FeedConnectionString(address, username, password);
			string tradeConnectionSTring = FixAdviserLauncher.TradeConnectionString(address, username, password);
			ConnectionStrings result = new ConnectionStrings(feedConnectionString, tradeConnectionSTring);
			return result;
		}
		private static ConnectionStrings CreateConnectionStrings(int bankCode, int metaAccount)
		{
			string feedConnectionString = CreateFeedConnectionString(bankCode);
			string tradeConnectionSTring = CreateTradeConnectionString(bankCode, metaAccount);
			ConnectionStrings result = new ConnectionStrings(feedConnectionString, tradeConnectionSTring);
			return result;
		}
		private static string CreateFeedConnectionString(int bankCode)
		{
			string result = "[String]ProtocolType=Aggr;" +
			"[String]Type=Feed;" +
			"[String]FeederAddress=127.0.0.1;" +
			"[Int32]FeederPort=22060;" +
			"[String]FeederUsername=98B4;" +
			"[String]FeederPassword=083EA274;" +

			"[String]BridgeAddress=127.0.0.1;" +
			"[Int32]BridgePort=22090;" +
			"[String]BridgeUsername=username;" +
			"[String]BridgePassword=password;" +
			string.Format("[Int32]BankCode={0};", bankCode);
			return result;
		}
		private static string CreateTradeConnectionString(int bankCode, int metaAccount)
		{
			string result = "[String]ProtocolType=Aggr;" +
			"[String]Type=Trade;" +
			"[String]FeederAddress=127.0.0.1;" +
			"[Int32]FeederPort=22060;" +
			"[String]FeederUsername=98B4;" +
			"[String]FeederPassword=083EA274;" +

			"[String]BridgeAddress=127.0.0.1;" +
			"[Int32]BridgePort=22090;" +
			"[String]BridgeUsername=username;" +
			"[String]BridgePassword=password;" +
			string.Format("[Int32]BankCode={0};", bankCode) +
			string.Format("[Int32]MetaAccount={0};", metaAccount);
			return result;
		}
	}
}
