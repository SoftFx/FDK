using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftFX.C;

namespace ArbitrageAggregator
{
	class Transaction
	{
		internal Transaction(string symbol, SingleAdviser<int> first, SingleAdviser<int> second)
		{
			m_start = DateTime.UtcNow;
			m_first = first.Tag;
			m_second = second.Tag;
			m_precision = Math.Min(first.Symbols[symbol].Precision, second.Symbols[symbol].Precision);
			m_builder.AppendFormat("{0}> symbol = {1}; first bank = {2}; second bank = {3}", UtcNow(m_start), symbol, first.Tag, second.Tag);
			m_builder.AppendLine();
		}
		internal void AddPrices(Level2 first, Level2 second)
		{
            if (first.Asks.Count == 0 || second.Bids.Count == 0)
            {
                m_builder.AppendLine("No quotes for level2");
                return;
            }
			int spread = PipsFromPrice(first.Ask - second.Bid);
			m_builder.AppendFormat("{0}> [{1}].Ask = {2}; [{3}].Bid = {4}; spread = {5} pip(s)", UtcNow(), m_first, first.Ask, m_second, second.Bid, spread);
			m_builder.AppendLine();
		}
		internal void Finish()
		{
			DateTime finish = DateTime.UtcNow;
			double interval = Math.Round((finish - m_start).TotalMilliseconds);
			interval /= 1000;
			m_builder.AppendFormat("{0}> duration = {1}", UtcNow(finish), interval);
			m_builder.AppendLine();
		}
		public override string ToString()
		{
			return m_builder.ToString();
		}
		private int PipsFromPrice(double price)
		{
			price *= Math.Pow(10, m_precision);
			price = Math.Round(price);
			int result = (int)price;
			return result;
		}
		private static string UtcNow()
		{
			return UtcNow(DateTime.UtcNow);
		}
		private static string UtcNow(DateTime now)
		{
			string result = now.ToString("yyyy.MM.dd - HH.mm.ss.fff");
			return result;
		}
		#region members
		private readonly DateTime m_start;
		private readonly int m_precision;
		private readonly int m_first;
		private readonly int m_second;
		private readonly StringBuilder m_builder = new StringBuilder();
		#endregion
	}
}
