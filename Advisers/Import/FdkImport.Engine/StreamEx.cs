namespace FdkImport.Engine
{
    using System;
    using System.IO;

    class StreamEx
	{
		#region Construction

		public StreamEx(StreamReader input, StreamWriter output)
		{
			if (input == null)
				throw new ArgumentNullException("input");

			if (output == null)
				throw new ArgumentNullException("output");

			this.input = input;
			this.output = output;
		}

		#endregion

		#region Methods

		public string ReadLine()
		{
			return this.input.ReadLine();
		}

		public void WriteLine()
		{
			this.output.WriteLine();
		}

		public void WriteLine(string message)
		{
			this.WriteTabs();
			this.output.WriteLine(message);
		}

		public void WriteLine(string format, params object[] args)
		{
			var message = string.Format(format, args);
			this.WriteLine(message);
		}

		public void BeginLine()
		{
			this.WriteTabs();
		}

		public void Write(string message)
		{
			this.output.Write(message);
		}

		public void Write(string format, params object[] args)
		{
			var message = string.Format(format, args);
			this.Write(message);
		}

		public void EndLine()
		{
			this.output.WriteLine();
		}

		#endregion

		#region Private Methods

		void WriteTabs()
		{
			for (var index = 0; index < this.indent; ++index)
			{
				this.output.Write("    ");
			}
		}

		#endregion

		#region Properties

		public int Indent
		{
			get
			{
				return this.indent;
			}
			set
			{
				if (value < 0)
					throw new ArgumentOutOfRangeException("value", value, "Indent value must be positive or zero");

				this.indent = value;
			}
		}

		#endregion

		#region Members

		readonly StreamReader input;
		readonly StreamWriter output;
		int indent;

		#endregion
	}
}
