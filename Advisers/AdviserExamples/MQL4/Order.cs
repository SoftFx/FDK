using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftFX.C;
using System.Threading;

namespace AdviserExamples.MQL4
{
	internal class Order
	{
		internal Order(SingleAdviser<int> adviser, string symbol, double requestedPrice, double requestedVolume, OrderType type, TradeSide side)
		{
			m_volume = requestedVolume;
			m_adviser = adviser;
			m_symbol = symbol;
			m_requestedPrice = requestedPrice;
			m_type = type;
			m_side = side;
		}
		public bool Run()
		{
			return ThreadPool.QueueUserWorkItem(DoRun);
		}
		public void Join()
		{
			m_event.WaitOne();
		}
		public double? ExecutedPrice
		{
			get
			{
				return m_executedPrice;
			}
		}
		private void DoRun(object state)
		{
			try
			{
				TradeRecord record = m_adviser.SendOrder(m_symbol, m_type, m_side, m_requestedPrice, m_volume, null, null, null, null);
				if ((null != record) && (record.Volume > 0))
				{
					m_executedPrice = record.Price;
				}
			}
			catch (System.Exception ex)
			{
				Console.WriteLine(ex);
			}
			finally
			{
				m_event.Set();
			}
		}
		#region members
		private SingleAdviser<int> m_adviser;
		private string m_symbol;
		private double m_requestedPrice;
		private double m_volume;
		private OrderType m_type;
		private TradeSide m_side;
		private double? m_executedPrice;
		private AutoResetEvent m_event = new AutoResetEvent(false);
		#endregion
	}
}
