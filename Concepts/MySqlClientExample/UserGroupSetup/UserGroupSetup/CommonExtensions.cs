using System;
using System.Collections.Generic;

namespace UserGroupSetup
{
	public static class CommonExtensions
{	
		public static void Each<T>(this IEnumerable<T> items, Action<T> action)
		{
			foreach (var it in items)
				action (it);
		}
	}
}

