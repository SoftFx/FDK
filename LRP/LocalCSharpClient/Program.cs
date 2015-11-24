using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using SoftFX.Lrp;

namespace LocalCSharp
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				TimeSpan interval = new TimeSpan(0, 0, 1);
				using (LocalClient client = new LocalClient("LocalCppServer.dll", Signature.Value, Mode.OutProcess))
				{
					SimpleTest(client);
					//ExtendedTest(client);
					//SpeedTest(client);
					//PInvokeSpeedTest();
				}
			}
			catch (System.Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		private static void SimpleTest(IClient client)
		{
			Console.WriteLine(AppDomain.CurrentDomain);

			Simple simple = new Simple(client);
			LPtr x = simple.Constructor();

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
			//UsedType usedType = new UsedType();
			//usedType.Code = 1;
			//usedType.Description = "test";

			//InType inType = new InType();
			//inType.Used = usedType;
			//inType.Value = 2;

			//InOutType inOutType = new InOutType();
			//inOutType.Used = usedType;
			//inOutType.Value2 = 3;

			//OutType outType = null;

			//ReturnType returnType = extended.Do(inType, ref inOutType, out outType);
			//return;
		}
		private static void SpeedTest(LocalClient client)
		{
			Extended extended = new Extended(client);

			DateTime start = DateTime.UtcNow;
			Int32 count = 16 * 1024 * 1024;
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
			Console.WriteLine("Speed = {0}", speed / 1000);
		}
		private static void PInvokeSpeedTest()
		{
			{
				Double volume = 100000;
				Double amount = 0;
				Int32 status = MarketBuy("EUR/USD", 1.3, ref volume, out amount);
			}

			DateTime start = DateTime.UtcNow;
			Int32 count = 16 * 1024 * 1024;
			for (Int32 index = 0; index < count; ++index)
			{
				Double amount = 0;
				Double volume = 100000;
				MarketBuy("EUR/USD", 1.3, ref volume, out amount);
			}
			DateTime finish = DateTime.UtcNow;
			Double interval = (finish - start).TotalSeconds;
			Double speed = (count / interval);
			Console.WriteLine("P/Invoke speed", interval);
			Console.WriteLine("Interval = {0}", interval);
			Console.WriteLine("Speed = {0}", speed / 1000);

		}
		[DllImport("LocalCppServer.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		private static extern Int32 MarketBuy(String symbol, Double price, ref Double volume, out Double amount);
	}
}
