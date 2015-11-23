//+------------------------------------------------------------------+
//|                                                        Trade.mq4 |
//|                                 Copyright © 2010, Thomas Quester |
//|                                        http://www.olfolders.de   |
//+------------------------------------------------------------------+
#property copyright "Copyright © 2010, Thomas Quester"
#property link      "http://www.olfolders.de"
#include <stdlib.mqh>
extern bool Trade_________________=true;
extern double Lots=0.01;                   // amount of trade
extern double Slipage=20;
extern int StopLoss=40;
extern int TakeProfit=0;
extern bool TrailingStopLoss=true;
extern double MinMoney=20;
extern int    Magic=12345;                   // our magic number

int    numTickets;
int    tickets[];
int    commands[];
string comment;

double GetLots()             { return (Lots);         }
int    GetNumTickets()       { return (numTickets);   }
int    GetMagic()            { return (Magic);        }
void   SetMagic(int m)       { Magic = m;             }
int    GetStopLoss()         { return (StopLoss);     }
int    GetTakeProfit()       { return (TakeProfit);   }
bool   GetUseTrailingStopLoss() { return (TrailingStopLoss); }
int    GetTicket(int i)      { return (tickets[i]); }
int    GetCommand(int i)     { return (commands[i]);  }
void   SetComment(string s)  { comment = s;           }
// +----------------------------------------------------------------------------+
// | Eruzeuge ein Textfeld im Chart
// |  Input lblname        Name des Feldes (fьr SetText)
// |        x,y            Koordinaten
// |        txt            Text
// |        color          Farbe des Textes
// +----------------------------------------------------------------------------+
void makelabel(string lblname,int x,int y,
   string txt,color txtcolor){
   ObjectCreate(lblname, OBJ_LABEL,0, 0, 0);
   ObjectSet(lblname, OBJPROP_CORNER, 0);
   ObjectSetText(lblname,txt,7,"Verdana", txtcolor);
   ObjectSet(lblname, OBJPROP_XDISTANCE, x);
   ObjectSet(lblname, OBJPROP_YDISTANCE, y);
}

// +----------------------------------------------------------------------------+
// | Дndere ein Textfeld
// |  Input name           Name des Feldes 
// |        txt            Neuer Text
// +----------------------------------------------------------------------------+
void SetText(string name, string txt)
{
    ObjectSetText(name,txt,7,"Verdana", White);
}

// +----------------------------------------------------------------------------+
// | Findet alle Orders und zieht Stop Loss nach
//   Input  SetStopLoss   : true: StopLoss setzen
// |
// | Output in globale Variablen
// |        secureProfit  : Gewinn in Pips im Falles eines StopLoss
// |        allSecure     : bool: Alle Orders sind im Plus
// |        totalProfit   : Gewinn in Pips jetzt
// |        allProfit     : bool: Alle Orders sind im Plus
// |        numTickets    : Anzahl der Orders
// |        cTrades       : Anzahl der Orders
// |        cWin          : Anzahl Gewinn-Orders
// |        cLoss         : Anzahl Verlust-Orders
// +----------------------------------------------------------------------------+


void FindOrders(bool SetStopLoss)
{
    int typ,i,cnt,ticket;
    int _takeProfit;
    int id;
    cnt = OrdersTotal();

    id =0;
    ArrayResize(tickets,cnt);
    ArrayResize(commands,cnt);
    for (i=0;i<cnt;i++)
    {
       if (OrderSelect(i,SELECT_BY_POS,MODE_TRADES)==true)
       {
          if (OrderSymbol() == Symbol() && OrderMagicNumber() == Magic)
          {
             commands[id] = OrderType();
             tickets[id]  = OrderTicket();
             double profit,stop,open;
             typ = OrderType();
             if (typ == OP_BUY || typ == OP_SELL)
             {
                 numTickets ++;
                 profit = OrderProfit();
                 stop   = OrderStopLoss();
                 open   = OrderOpenPrice();
                 if (TrailingStopLoss) SetStopLoss(SetStopLoss, OrderTicket(),GetStopLoss(), _takeProfit);
             }
          }
       }
    }
    //Print("Profit = "+totalProfit);
}


// sets the stopp loss/take profit
// stoploss1 wenn noch nicht im profit, sonst stoploss2

// +----------------------------------------------------------------------------+
// | Setze Stop Loss und Take Profit fьr eine Order
// | Input: SetStopLoss: true : Orders werden modifiziert (bei False nur Statistik)
// |        ticket     : 0: Aktuelle Order
// |                     anderes: Ticket der Order
// |        stopLoss   : StopLoss der Order in Pips oder 0 fьr keine Дnderung
// |        takeProfit : Take Profit der Order in Pips oder 0 fьr keine Дnderung
// | Output in globale Variablen
// |        secureProfit  : Gewinn in Pips im Falles eines StopLoss
// |        allSecure     : bool: Alle Orders sind im Plus
// |        totalProfit   : Gewinn in Pips jetzt
// |        allProfit     : bool: Alle Orders sind im Plus
// +----------------------------------------------------------------------------+
void SetStopLoss(bool SetStopLoss, int ticket, int stopLoss, int takeProfit)
{


   double newStop,newStop2,stop,tp,newtp,profit;
   int typ;
   int stopPips;
   double win;
   if (ticket != 0)
       if (!OrderSelect(ticket,SELECT_BY_TICKET,MODE_TRADES)) return;
       
  
    tp = OrderTakeProfit();
    stop = OrderStopLoss();
    profit = OrderProfit();
    
    stopPips = stopLoss;
    newStop2 = 0;
    newStop= stop;
    newtp = tp;
    typ = OrderType();
    if (typ ==OP_BUY)
    {
       if (takeProfit != 0)  newtp = Ask+Point*takeProfit;
       newStop = Ask-Point*stopPips;
       if (newStop < stop) newStop = stop;
    }      
    if (typ == OP_SELL)
    {
       if (takeProfit != 0) newtp = Ask-Point*takeProfit;
       newStop = Ask+Point*stopPips;
       if (newStop > stop) newStop = stop;
    }
     
    if (tp != newtp || stop != newStop)
    {
        if (SetStopLoss)
        {
        
            Print("Orrder Modify ticket=",ticket, " typ=",CmdName(OrderType())," newStop=",newStop, " newTp=",newtp,  " StopLoss=",StopLoss);
            OrderModify(ticket,OrderOpenPrice(),newStop,newtp,OrderExpiration(),White);
        }
    }
       
  
}

// +----------------------------------------------------------------------------+
// | Buy Lots lots
// +----------------------------------------------------------------------------+
void Order(double lots)
{
   if (lots < 0) Sell(-lots);
         else    Buy(lots);
}
         
void Buy(double lots)
{

   if (AccountEquity() < MinMoney)
   {
      Print("You have no money to trade!");
      return;
   }
   double stop,profit;
   int    magic1;
   int    ticket;
   int    i;
   double limit;
   
   stop = Ask-GetStopLoss()*Point;
   if (TakeProfit != 0) 
         profit = Ask+TakeProfit*Point;
   else  
         profit = 0;
   
   ticket = OrderSend(Symbol(),OP_BUY,lots,Ask,Slipage,stop,profit,comment,Magic,0,Green);
   if (ticket < 0)
       Print("Error ",ErrorDescription(GetLastError()));
   
}

// +----------------------------------------------------------------------------+
// | Sell Lots lots
// +----------------------------------------------------------------------------+
void Sell(double lots)
{
   double stop,profit;
   int    magic1;
   int    ticket;
   int    i;
   double limit;
   limit = Bid;
   if (AccountEquity() < MinMoney)
   {
      Print("You have no money to trade!");
      return;
   }
   
   stop = Bid+GetStopLoss()*Point;
   if (TakeProfit != 0)
      profit = Bid-TakeProfit*Point;
   else  
         profit = 0;
   ticket = OrderSend(Symbol(),OP_SELL,lots,Bid,Slipage,stop,profit,comment,Magic,0,Green);
   if (ticket < 0)
      Print("Error ",ErrorDescription(GetLastError()));
   
} 

void CloseOrder(int ticket)
{
   double price;
   Print("Closing order ticket=",ticket);
    if (OrderSelect(ticket,SELECT_BY_TICKET,MODE_TRADES))
    {
       if (OrderType() == OP_BUY) price = Ask; else price=Bid;
       
         if (OrderClose(ticket,OrderLots(),price,Slipage) == false)
            Alert("Problem closing order");
    }
}    
// +----------------------------------------------------------------------------+
// | Returns the name of BUY/SELL Constant
// +----------------------------------------------------------------------------+

string CmdName(int cmd)
{
   string r;
   switch(cmd)
   {
      case OP_BUY: r = "buy"; break;
      case OP_SELL: r = "sell"; break;
      case OP_BUYSTOP: r = "buy stop"; break;
      case OP_SELLSTOP: r = "sell stop"; break;
      case OP_BUYLIMIT: r = "buy limit"; break;
      case OP_SELLLIMIT: r = "sell limit"; break;
    }
    return (r);
}



void SaveStockParameters(int handle)
{
	FileWrite(handle,"Lots",Lots);
	FileWrite(handle,"StopLoss",StopLoss);
	FileWrite(handle,"TakeProfit",TakeProfit);
	FileWrite(handle,"MinMoney",MinMoney);
	FileWrite(handle,"Magic",Magic);
}