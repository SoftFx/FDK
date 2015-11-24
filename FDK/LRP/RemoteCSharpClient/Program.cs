using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using SoftFX.Lrp;

namespace RemoteCSharpClient
{
	class Program
	{
		static void Main(string[] args)
		{
			if (1 != args.Length)
			{
				Console.WriteLine("Usage:");
				Console.WriteLine("\tRemoteCSharpClient <port>");
			}
			else
			{
				Start(args[0]);
			}
		}
		static void Start(String st)
		{
			//try
			{
				Thread.Sleep(5000);
				Int32 port = Convert.ToInt32(st);
				using (StClient client = new StClient(Signature.Value, "localhost", port, "username", "password", "c:\\Temporary3\\csharp.log"))
				{
					client.Connect(60000);

					Console.ReadKey();
					client.Ping(10000);
					SimpleTest(client);
					ExtendedTest(client);
					//SpeedTest(client);
				}
			}
			//catch (System.Exception ex)
			{
				//Console.WriteLine(ex);
			}
		}
		private static void SimpleTest(IClient client)
		{
			Simple simple = new Simple(client);

			String st = simple.Inverse("test");
			Console.WriteLine(st);

			Int32 value = 4;
			Int32 result = 0;
			Boolean status = simple.Factorial(value, out result);

			Console.WriteLine("{0}! = {1}; result = {2}", value, result, status);

			value = 100;
			result = 0;

			status = simple.Factorial(value, out result);

			Console.WriteLine("{0}! = {1}; result = {2}", value, result, status);
		}
		private static void ExtendedTest(IClient client)
		{
			Extended extended = new Extended(client);

			UsedType usedType = new UsedType();
			usedType.Code = 1;
			usedType.Description = "test";

			InType inType = new InType();
			inType.Used = usedType;
			inType.Value = 2;

			InOutType inOutType = new InOutType();
			inOutType.Used = usedType;
			inOutType.Value2 = 3;

			OutType outType = null;

			ReturnType returnType = extended.Do(inType, ref inOutType, out outType);

			SortedDictionary<string, double> reports = new SortedDictionary<string, double>();
			reports["EUR/USD"] = 1000;
			Dictionary<string, double> reports2 = new Dictionary<string, double>();
			reports["EUR/JPY"] = 333;
			extended.Update(ref reports, ref reports2);


			return;
		}
		private static void SpeedTest(IClient client)
		{
			Extended extended = new Extended(client);

			DateTime start = DateTime.UtcNow;
			Int32 count = 320 * 1024;
			for (Int32 index = 0; index < count; ++index)
			{
				Double amount = 0;
				Double volume = 100000;
				extended.MarketBuy("EUR/USD", 1.3, ref volume, out amount);
			}
			DateTime finish = DateTime.UtcNow;
			Double interval = (finish - start).TotalSeconds;
			Double speed = (count / interval);
			Console.WriteLine("LRP speed");
			Console.WriteLine("Interval = {0}", interval);
			Console.WriteLine("Speed = {0}", speed);
		}
	}
}
