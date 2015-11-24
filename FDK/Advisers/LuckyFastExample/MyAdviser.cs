using System;
using SoftFX;
using SoftFX.Adapters.C;
using System.Runtime.InteropServices;

public class MyAdviser : BaseCAdapter
{
    //+------------------------------------------------------------------+
    //|                                                    LuckyFast.mq4 |
    //|                                                                  |
    //|                                                                  |
    //+------------------------------------------------------------------+
    public readonly static object copyright = "";
    public readonly static object link = "";

    public int RateTrailingDistance = 13;
    public double MaximumOpenVolumeLots = 0.2;
    public int IsAutoCloseBy = 0;

    int lastBuyLimit = 0;
    int lastSellLimit = 0;
    double PipsCoef = 0;

    //+------------------------------------------------------------------+
    //| expert initialization function                                   |
    //+------------------------------------------------------------------+
    int init()
    {
        //----
        PipsCoef = MathPow(10, -Digits);

        for (int i =OrdersTotal()-1; i >= 0; i--)
        {
            OrderSelect(i, SELECT_BY_POS);
            int cmd = OrderType();

            if (cmd !=OP_BUY && cmd != OP_SELL)
            {
                OrderDelete(OrderTicket());
            }
        }

        //----
        return (0);
    }
    //+------------------------------------------------------------------+
    //| expert deinitialization function                                 |
    //+------------------------------------------------------------------+
    int deinit()
    {
        //----

        //----
        return (0);
    }
    //+------------------------------------------------------------------+
    //| expert start function                                            |
    //+------------------------------------------------------------------+
    int start()
    {
        if (IsAutoCloseBy != 0)
            AutoCloseBy();
        //----
        if (!IsVolumeExcedeed(OP_BUY))
        {
            double openBuyPrice = Ask - PipsCoef * RateTrailingDistance;
            if (!IsLimitOrderSelect(lastBuyLimit))
            {
                Print("Adding new Buy order");
                lastBuyLimit = OrderSend(Symbol(), OP_BUYLIMIT, 0.11, openBuyPrice, 0, 0, 0, "buy", 1);
            }
            else
            {
                OrderModify(lastBuyLimit, openBuyPrice, 0, 0, 0);
            }
        }

        if (!IsVolumeExcedeed(OP_SELL))
        {
            double openSellPrice = Bid + PipsCoef * RateTrailingDistance;
            if (!IsLimitOrderSelect(lastSellLimit))
            {
                Print("Adding new Sell order");
                lastSellLimit = OrderSend(Symbol(), OP_SELLLIMIT, 0.11, openSellPrice, 0, 0, 0, "sell", 2);
            }
            else
            {
                OrderModify(lastSellLimit, openSellPrice, 0, 0, 0);
            }
        }
        //----
        return (0);
    }
    //+------------------------------------------------------------------+

    void AutoCloseBy()
    {
        if (OrdersTotal() <= 1)
            return;

        int posFirstOrder = -1;
        int ticket1 = 0;
        int ticket2 = 0;


        int cmd = 0;
        int i = OrdersTotal() - 1;
        for (; i >= 0; i--)
        {
            OrderSelect(i, SELECT_BY_POS);
            cmd = OrderType();
            if (cmd == OP_BUY || cmd == OP_SELL)
            {
                posFirstOrder = cmd;
                ticket1 = OrderTicket();
                break;
            }
        }
        if (posFirstOrder == -1)
            return;

        for (; i >= 0; i--)
        {
            OrderSelect(i, SELECT_BY_POS);
            cmd = OrderType();

            if (cmd != posFirstOrder && (cmd == OP_BUY || cmd == OP_SELL))
            {
                ticket2 = OrderTicket();
                break;
            }
        }

        if (ticket1 != 0 && ticket2 != 0)
        {
            OrderCloseBy(ticket1, ticket2);
            AutoCloseBy();
            return;
        }
    }

    bool IsVolumeExcedeed(int cmd)
    {
        double sumOpenVolume = 0;

        int i = OrdersTotal() - 1;
        for (; i >= 0; i--)
        {
            OrderSelect(i, SELECT_BY_POS);
            if (OrderType() == OP_BUY)
                sumOpenVolume += OrderLots();
            if (OrderType() == OP_SELL)
                sumOpenVolume -= OrderLots();
        }
        if (sumOpenVolume > MaximumOpenVolumeLots && cmd == OP_BUY)
            return (true);
        if (sumOpenVolume < -MaximumOpenVolumeLots && cmd == OP_SELL)
            return (true);
        return (false);

    }

    bool IsLimitOrderSelect(int order)
    {
        if (false == OrderSelect(order, SELECT_BY_TICKET))
            return (false);
        return (OrderType() == OP_BUYLIMIT || OrderType() == OP_SELLLIMIT);
    }
}
