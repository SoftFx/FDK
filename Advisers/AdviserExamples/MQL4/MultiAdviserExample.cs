using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftFX.C;
using System.Threading;

namespace AdviserExamples.MQL4
{
	class MultiAdviserExample : MultiAdviser<int>
	{
		protected override void OnInitialize()
		{
			Console.ForegroundColor = ConsoleColor.White;
			foreach (var element in this)
			{
 				if (null == m_first)
 				{
					m_first = this[element];
 				}
				else if (null == m_second)
				{
					m_second = this[element];
				}
				else
				{
					break;
				}
			}
			string now = DateTime.Now.ToString("HHHH:mm:ss.fff");
			Console.WriteLine("{0}: OnInitialize(EUR/USD)", now);
		}
		protected override void OnConnected(SingleAdviser<int> adviser)
		{
			string now = DateTime.Now.ToString("HHHH:mm:ss.fff");
			Console.WriteLine("{0}: OnConnected({1})", now, adviser.Tag);
		}
		protected override void OnDisconnected(SingleAdviser<int> adviser, string message)
		{
			string now = DateTime.Now.ToString("HHHH:mm:ss.fff");
			Console.WriteLine("{0}: OnDisconnected({1})", now, adviser.Tag);
		}
		protected override void OnUpdate(SingleAdviser<int> adviser)
		{
			string symbol = "EUR/USD";
			Level2 first = m_first.GetLevel2(symbol);
			Order sell = new Order(m_first, symbol, first.Bid, cVolume, OrderType.Market, TradeSide.Sell);
			sell.Run();
			sell.Join();
		}
		private void RunFirstBidSecondAsk(string symbol, Level2 first, Level2 second)
		{
			DateTime when = DateTime.Now;
			string now = when.ToString("HHHH:mm:ss.fff");
			int arbitrage = (int)Math.Round((first.Bid - second.Ask) * 100000);
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("{0}: [{1}].Bid = {2}, [{3}].Ask = {4}, diff = {5}; start", now, m_first.Tag, first.Bid, m_second.Tag, second.Ask, arbitrage);

			Order sell = new Order(m_first, symbol, first.Bid, cVolume, OrderType.Market, TradeSide.Sell);
			Order buy = new Order(m_second, symbol, second.Ask, cVolume, OrderType.Market, TradeSide.Buy);
			sell.Run();
			buy.Run();
			sell.Join();
			buy.Join();

			PrintProfit(cVolume, buy, sell);
			Console.WriteLine();
		}
		private void RunFirstAskSecondBid(string symbol, Level2 first, Level2 second)
		{
			DateTime when = DateTime.Now;
			string now = when.ToString("HHHH:mm:ss.fff");
			int arbitrage = (int)Math.Round((second.Bid - first.Ask) * 100000);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("{0}: [{1}].Ask = {2}, [{3}].Bid = {4}, diff = {5}; start", now, m_first.Tag, first.Ask, m_second.Tag, second.Bid, arbitrage);


			Order buy = new Order(m_first, symbol, first.Ask, cVolume, OrderType.Market, TradeSide.Buy);
			Order sell = new Order(m_second, symbol, second.Bid, cVolume, OrderType.Market, TradeSide.Sell);
			sell.Run();
			buy.Run();
			sell.Join();
			buy.Join();

			PrintProfit(cVolume, buy, sell);
			Console.WriteLine();
		}
		private void PrintProfit(double volume, Order buy, Order sell)
		{
			DateTime when = DateTime.Now;
			string now = when.ToString("HHHH:mm:ss.fff");
			double? sellPrice = sell.ExecutedPrice;
			double? buyPrice = buy.ExecutedPrice;
			if (buyPrice.HasValue && sellPrice.HasValue)
			{
				Console.WriteLine("{0}: buy price = {1}, sell price = {2}", now, buyPrice.Value, sellPrice.Value);
				double profit = volume * (sellPrice.Value - buyPrice.Value);
				Console.WriteLine("{0}: profit = {1}", now, profit);
			}
			else if (buyPrice.HasValue)
			{
				Console.WriteLine("{0}: only buy: price = {1}", now, buyPrice.Value);
			}
			else if (sellPrice.HasValue)
			{
				Console.WriteLine("{0}: only sell: price = {1}", sellPrice.Value);
			}
			else
			{
				Console.WriteLine("{0}: nothing executed", now);
			}
		}
		#region members
		private SingleAdviser<int> m_first;
		private SingleAdviser<int> m_second;
		private const double cVolume = 1000000;
		#endregion
	}
}
