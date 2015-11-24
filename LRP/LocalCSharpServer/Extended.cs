using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalCSharp
{
	internal class Extended
	{
		internal ReturnType Do(InType inArg, ref InOutType inOutArg, out OutType outArg)
		{
			inOutArg.Used = inArg.Used;
			outArg = new OutType();
			outArg.Used = inArg.Used;
			inOutArg.Value2 = inOutArg.Value2 * inOutArg.Value2;
			outArg.Value3 = inArg.Value - 1;

			ReturnType result = new ReturnType();
			result.Used = inArg.Used;
			result.Value4 = inArg.Value * 3;
			return result;
		}
		internal int MarketBuy(string symbol, double price, ref double volume, out double amount)
		{
			amount = volume * price;
			volume = 0;
			return 0;
		}
	}
}
