//+------------------------------------------------------------------+
//|                                                  time trader.mq4 |
//|                            Copyright © 2011, www.FxAutomated.com |
//|                                       http://www.FxAutomated.com |
//+------------------------------------------------------------------+
#property copyright "Copyright © 2011, www.FxAutomated.com"
#property link      "http://www.FxAutomated.com"
//---- input parameters
extern string    time_trader_v1.1="visit www.FxAutomated.com for more!!!";
extern double    Lots=0.1;
extern int       TakeProfit=20;
extern int       StopLoss=20;
extern int       Slip=5;
extern int BuyMagicNumber =10001;
extern int SellMagicNumber =10002;
extern string TradeSettings="Mt4 time(min-max): hours 0-23, minutes 0-59, seconds 0-59";
extern bool AllowBuy=true;
extern bool AllowSell=true;
extern int  TradeHour=0;
extern int  TradeMinutes=0;
extern int  TradeSeconds=0;
extern string OurSite="www.FxAutomated.com";
extern string SignalsAndManagedAccounts="www.TradingBug.com";

//+------------------------------------------------------------------+
//| expert starts                                  |
//+------------------------------------------------------------------+
int start()
  {
//----
int StopMultd,Sleeper=1;



int digits=MarketInfo("EURUSD",MODE_DIGITS);
if(digits==5){StopMultd=10;} else{StopMultd=1;}
double TP=NormalizeDouble(TakeProfit*StopMultd,Digits);
double SL=NormalizeDouble(StopLoss*StopMultd,Digits);
int Slippage=Slip*StopMultd;

// Calculate stop loss
double slb=NormalizeDouble(Ask-SL*Point,Digits);
double sls=NormalizeDouble(Bid+SL*Point,Digits);

// Calculate take profit
double tpb=NormalizeDouble(Ask+TP*Point,Digits);
double tps=NormalizeDouble(Bid-TP*Point,Digits);

//-------------------------------------------------------------------+
//Check open orders
//-------------------------------------------------------------------+
if(OrdersTotal()>0){
  for(int i=1; i<=OrdersTotal(); i++)          // Cycle searching in orders
     {
      if (OrderSelect(i-1,SELECT_BY_POS)==true) // If the next is available
        {
          if(OrderMagicNumber()==BuyMagicNumber) {int halt1=1;}
          if(OrderMagicNumber()==SellMagicNumber) {int halt2=1;}
        }
     }
}
//-------------------------------------------------------------------+


if((halt1!=1)&&(AllowBuy==true)){// halt1

// Buy criteria
if ((TradeHour==Hour())&&(TradeMinutes==Minute())&&(TradeSeconds>=Seconds())) //Signal Buy
 {
   int openbuy=OrderSend(Symbol(),OP_BUY,Lots,Ask,Slippage,slb,tpb,"time trader buy order ",BuyMagicNumber,0,Blue);
   if(openbuy<1){int buyfail=1;}
 }
 
}// halt1
 
if((halt2!=1)&&(AllowSell==true)){// halt2
RefreshRates();
 // Sell criteria
 if ((TradeHour==Hour())&&(TradeMinutes==Minute())&&(TradeSeconds>=Seconds())) //Signal Sell
 {
   int opensell=OrderSend(Symbol(),OP_SELL,Lots,Bid,Slippage,sls,tps,"time trader sell order ",SellMagicNumber,0,Green);
   if(opensell<1){int sellfail=1;}
 }
 
}// halt2


//-------------------------------------------------------------------+
// Error processing
//-------------------------------------------------------------------+
if(buyfail==1||sellfail==1){
int Error=GetLastError();
  if(Error==130){Alert("Wrong stops. Retrying."); RefreshRates();}
  if(Error==133){Alert("Trading prohibited.");}
  if(Error==2){Alert("Common error.");}
  if(Error==146){Alert("Trading subsystem is busy. Retrying."); Sleep(500); RefreshRates();}

}

// if(openbuy==true||opensell==true)Sleep(1*60*1000*Sleeper);
//-------------------------------------------------------------------
   return(0);
  }
//+-----------------------------------