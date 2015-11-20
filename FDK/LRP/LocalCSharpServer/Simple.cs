using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftFX.Lrp;

namespace LocalCSharp
{
	internal class Simple
	{
		internal LPtr Constructor()
		{
			return LPtr.Zero;
		}
		internal string Inverse(string text)
		{
			string result = AppDomain.CurrentDomain.ToString();
			return result;
		}
		internal bool Factorial(int value, out int result)
		{
			result = 0;
			if (value < 0)
			{
				return false;
			}
			int answer = 1;
			while (value > 1)
			{
				answer *= value;
				--value;
				if (result < 0)
				{
					return false;
				}
			}
			result = answer;
			return true;
		}
	}
}
