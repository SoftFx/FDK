using System;
using System.Collections.Generic;
using System.Text;
using SoftFX.C;

namespace AdviserExamples.MQL4
{
	/// <summary>
	/// This is a simple adviser example for a single feed/trade connection.
	/// </summary>
	class SingleAdviserExample : SingleAdviser
	{
		#region initialization and connection notifications
		/// <summary>
		/// The method will be called at the first time, be adviser framework.
		/// You should override the method to specify detailed adviser parameters
		/// and to do your custom initialization.
		/// You can comment/delete the method, if you like the default settings.
		/// </summary>
		protected override void OnInitialize()
		{
			Console.WriteLine("OnInitialize()");

			// uncomment the next line to override default market depth value = 1
			// this.Settings.MarketDepth = 1;
		}
		/// <summary>
		/// The method will be called at the last time, be adviser framework.
		/// You should override the method to do your custom finalization.
		/// You can comment/delete the method, if you do not need it.
		/// </summary>
		protected override void OnFinalize()
		{
			Console.WriteLine("OnFinalize()");
		}
		/// <summary>
		/// The method will be called by adviser framework every time, when feed and trade connections are establish.
		/// </summary>
		protected override void OnConnected()
		{
			Console.WriteLine("OnConnected()");
			// here you can access and use any account information
			// quotes information can be unavailable
			Console.WriteLine("user ID = {0}", this.Account.ID);
		}
		/// <summary>
		/// The method will be called by adviser framework every time, when feed or trade connections are lost.
		/// </summary>
		protected override void OnDisconnected(string message)
		{
			Console.WriteLine("OnDisconnected(): {0}", message);
		}
		#endregion
		/// <summary>
		/// Override th method to run code after any updates have been received.
		/// </summary>
		protected override void OnUpdate()
		{
			QuotesExample();
			TradesExample();
			SymbolsExample();
		}
		private void QuotesExample()
		{
			Level2 quote = GetLevel2("EURUSD");
			if (!quote.HasBeenChanged || !quote.Exist)
			{
				return;
			}
			Console.WriteLine("OnUpdate({0}): {1} - {2}", quote.Symbol, quote.Bid, quote.Ask);
			double averageBid = 0;
			double volume = 0;
			foreach (var element in quote.Bids)
			{
				averageBid += element.Price * element.Volume;
				volume += element.Volume;
			}
			averageBid /= volume;
			Console.WriteLine("average bid = {0}", averageBid);
		}
		private void TradesExample()
		{
			int count = this.TradeRecords.Count;
			if (count > 0)
			{
				return;
			}
			Console.WriteLine("trade records count = {0}", count);
			TradeRecord record = this.SendOrder("EURUSD", OrderType.Market, TradeSide.Buy, 1.0, 100000, null, null, null, null);
			Console.WriteLine("record = {0}", record);
		}
		private void SymbolsExample()
		{
			foreach (var element in this.Symbols)
			{
			}
		}
		/// <summary>
		/// The method will be called by adviser framework every time, when margin call notification will be received.
		/// </summary>
		protected override void OnMarginCall()
		{
			Console.WriteLine("OnMarginCall()");
		}
		/// <summary>
		/// The method will be called by adviser framework every time, when stop out notification will be received.
		/// </summary>
		protected override void OnStopOut()
		{
			Console.WriteLine("OnStopOut()");
		}
	}
}
