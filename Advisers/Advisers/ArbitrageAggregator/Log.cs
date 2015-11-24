using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace ArbitrageAggregator
{
	internal class Log : IDisposable
	{
		#region construction
		private Log()
		{
			Settings s = Settings.Default;
			m_stream = new StreamWriter(s.LogPath);
			m_thread = new Thread(ThreadMethod);
			m_thread.Start();
		}
		#endregion
		#region methods
		internal static void WriteLine(string message)
		{
			m_log.DoWriteLine(message);
		}
		internal static void WriteLine(string format, params object[] args)
		{
			string message = string.Format(format, args);
			WriteLine(message);
		}
		internal static void Shutdown()
		{
			m_log.Dispose();
		}
		#endregion
		#region private methods
		private void DoWriteLine(string message)
		{
			lock (m_synchronizer)
			{
				m_first.Add(message);
			}
			m_event.Set();
		}
		private void ThreadMethod()
		{
			try
			{
				ThreadLoop();
			}
			catch (System.Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
		private void ThreadLoop()
		{
			for (m_event.WaitOne(); m_continue; m_event.WaitOne())
			{
				ThreadStep();
			}
		}
		private void ThreadStep()
		{
			lock (m_synchronizer)
			{
				List<string> temp = m_first;
				m_first = m_second;
				m_second = temp;
			}
			foreach (var element in m_second)
			{
				m_stream.WriteLine(element);
			}
			m_stream.Flush();
			m_second.Clear();
		}
		#endregion
		#region IDisposable interface implementation
		public void Dispose()
		{
			m_continue = false;
			m_event.Set();
			m_thread.Join();
			m_event.Dispose();
			m_stream.Dispose();
		}
		#endregion
		#region members
		private static Log m_log = new Log();
		private readonly object m_synchronizer = new object();
		private List<string> m_first = new List<string>();
		private List<string> m_second = new List<string>();
		private volatile bool m_continue = true;
		private readonly AutoResetEvent m_event = new AutoResetEvent(false);
		private readonly Thread m_thread;
		private StreamWriter m_stream;
		#endregion
	}
}
