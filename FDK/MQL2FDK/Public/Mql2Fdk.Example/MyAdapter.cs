using System;
using Mql2Fdk;
using Mql2Fdk.Attributes;
using System.Runtime.InteropServices;
public class MyAdviser : MqlAdapter
{
	//+------------------------------------------------------------------+
	//|                                               Moving Average.mq4 |
	//|                      Copyright © 2005, MetaQuotes Software Corp. |
	//|                                       http://www.metaquotes.net/ |
	//+------------------------------------------------------------------+
	const int MAGICMA = 20050610;
	public double Lots = 0.1;
	public double MaximumRisk = 0.02;
	public double DecreaseFactor = 3;
	public double MovingPeriod = 12;
	public double MovingShift = 6;
	//+------------------------------------------------------------------+
	//| Calculate open positions                                         |
	//+------------------------------------------------------------------+
	int CalculateCurrentOrders(string symbol)
	{
		int i = 0;
		int buys = 0, sells = 0;
		//----
		for (i = 0; i < OrdersTotal(); i++)
		{
			if (OrderSelect(i, SELECT_BY_POS, MODE_TRADES) == false)
				break;
			if (OrderSymbol() == Symbol() && OrderMagicNumber() == MAGICMA)
			{
				if (OrderType() == OP_BUY)
					buys++;
				if (OrderType() == OP_SELL)
					sells++;
			}
		}
		//---- return orders volume
		if (buys > 0)
			return (buys);
		else
			return (0);
		// this is added by the translator. Consider it adding in your script 
		return (0);
	}
	//+------------------------------------------------------------------+
	//| Calculate optimal lot size                                       |
	//+------------------------------------------------------------------+
	double LotsOptimized()
	{
		int i = 0;
		double lot = Lots;
		int orders = HistoryTotal();
		// history orders total
		int losses = 0;
		// number of losses orders without a break
		//---- select lot size
		lot = NormalizeDouble(AccountFreeMargin() * MaximumRisk / 1000.0, 1);
		//---- calcuulate number of losses orders without a break
		if (DecreaseFactor > 0)
		{
			for (i = orders - 1; i >= 0; i--)
			{
				if (OrderSelect(i, SELECT_BY_POS, MODE_HISTORY) == false)
				{
					Print("Error in history!");
					break;
				}
				if (OrderSymbol() != Symbol() || OrderType() > OP_SELL)
					continue;
				//----
				if (OrderProfit() > 0)
					break;
				if (OrderProfit() < 0)
					losses++;
			}
			if (losses > 1)
				lot = NormalizeDouble(lot - lot * losses / DecreaseFactor, 1);
		}
		//---- return lot size
		if (lot < 0.1)
			lot = 0.1;
		return (lot);
	}
	//+------------------------------------------------------------------+
	//| Check for open order conditions                                  |
	//+------------------------------------------------------------------+
	void CheckForOpen()
	{
		double ma = 0.0;
		int res = 0;
		//---- go trading only for first tiks of new bar
		if (Volume[0] > 1)
			return;
		//---- get Moving Average 
		ma = iMA(null, 0, (int)MovingPeriod, (int)MovingShift, MODE_SMA, PRICE_CLOSE, 0);
		//---- sell conditions
		if (Open[1] > ma && Close[1] < ma)
		{
			res = OrderSend(Symbol(), OP_SELL, LotsOptimized(), Bid, 3, 0, 0, "", MAGICMA, 0, Red);
			return;
		}
		//---- buy conditions
		if (Open[1] < ma && Close[1] > ma)
		{
			res = OrderSend(Symbol(), OP_BUY, LotsOptimized(), Ask, 3, 0, 0, "", MAGICMA, 0, Blue);
			return;
		}
		//----
	}
	//+------------------------------------------------------------------+
	//| Check for close order conditions                                 |
	//+------------------------------------------------------------------+
	void CheckForClose()
	{
		int i = 0;
		double ma = 0.0;
		//---- go trading only for first tiks of new bar
		if (Volume[0] > 1)
			return;
		//---- get Moving Average 
		ma = iMA(null, 0, (int)MovingPeriod, (int)MovingShift, MODE_SMA, PRICE_CLOSE, 0);
		//----
		for (i = 0; i < OrdersTotal(); i++)
		{
			if (OrderSelect(i, SELECT_BY_POS, MODE_TRADES) == false)
				break;
			if (OrderMagicNumber() != MAGICMA || OrderSymbol() != Symbol())
				continue;
			//---- check order type 
			if (OrderType() == OP_BUY)
			{
				if (Open[1] > ma && Close[1] < ma)
					OrderClose(OrderTicket(), OrderLots(), Bid, 3, White);
				break;
			}
			if (OrderType() == OP_SELL)
			{
				if (Open[1] < ma && Close[1] > ma)
					OrderClose(OrderTicket(), OrderLots(), Ask, 3, White);
				break;
			}
		}
		//----
	}
	//+------------------------------------------------------------------+
	//| Start function                                                   |
	//+------------------------------------------------------------------+
	void start()
	{
		//---- check for history and trading
		if (Bars < 100 || IsTradeAllowed() == false)
			return;
		//---- calculate open orders by current symbol
		if (CalculateCurrentOrders(Symbol()) == 0)
			CheckForOpen();
		else
			CheckForClose();
		//----
	}
	//+------------------------------------------------------------------+
}
