using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalCSharp
{
	internal class Channel
	{
		internal Channel()
		{
			this.Simple = new Simple();
			this.Extended = new Extended();
		}
		internal Simple Simple { get; private set; }
		internal Extended Extended { get; private set; }
	}
}
