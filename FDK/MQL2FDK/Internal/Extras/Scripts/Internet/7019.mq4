/*------------------------------------------------------------------+
 |                                              EA_PSar_002B_v1.mq4 |
 |                                                 Copyright © 2010 |
 |                                             basisforex@gmail.com |
 +------------------------------------------------------------------*/
#property copyright "Copyright © 2010, basisforex@gmail.com"
#property link      "basisforex@gmail.com"
//-----  
#define MagicNum 10001
//-----
extern bool     sar2                = true;
//-----
extern int      TP                  = 999;
extern int      SL                  = 399;
//-----
extern bool     UseMM               = false;
extern int      PercentMM           = 10;
extern double   Lots                = 0.1;
//+------------------------------------------------------------------+
double GetLots()
 { 
   if (UseMM)
    {
      double a;
      a = NormalizeDouble((PercentMM * AccountFreeMargin() / 100000), 1);      
      if(a > 49.9) return(49.9);
      else if(a < 0.1)
       {
         Print("Lots < 0.1");
         return(0);
       }
      else return(a);
    }    
   else return(Lots);
 }
//+------------------------------------------------------------------+ 
int CalculateCurrentOrders()
 {
   int orderT = OrdersTotal(), buys = 0, sells = 0;
   //----
   for(int i = 0; i < orderT; i++)
    {
      if(OrderSelect(i, SELECT_BY_POS, MODE_TRADES) == false) break;
      if(OrderSymbol() == Symbol() && OrderMagicNumber() == MagicNum)
       {
         if(OrderType() == OP_BUY)  buys++;
         if(OrderType() == OP_SELL) sells++;
       }
    }
   if(buys > 0) return(buys);
   else if(sells > 0) return(-sells);
   else return(0);
 }
//+------------------------------------------------------------------+ 
int start()
 {
   if (Symbol() != "EURUSD")
    {
		Comment("Not a right Symbol: ", Symbol(), " <>  EURUSD");
		return(0);
	 }  
   if (Period() != 1)
    {
		Comment("Not a right Period!!! It should be M1");
		return(0);	
	 }             
   if (AccountFreeMargin() < 20)
    {
		Comment("AccountFreeMargin < 20");
		return(0);
	 }
   //======================================================================
   double sa15_0, sa15_1, sa30_0, sa30_1, sa60_0, sa60_1, sa240_0, sa240_1;
   double saUp, saDn;
   int saDif;
   int cnt, ticket, total;
   //---
   if(CalculateCurrentOrders() == 0) 
    {      
      if(sar2 == true)
       {            
         sa15_0  = iSAR(NULL, PERIOD_M15, 0.06, 0.1, 0);
         sa15_1  = iSAR(NULL, PERIOD_M15, 0.06, 0.1, 1);
         sa30_0  = iSAR(NULL, PERIOD_M30, 0.06, 0.1, 0);
         sa30_1  = iSAR(NULL, PERIOD_M30, 0.06, 0.1, 1);
         sa60_0  = iSAR(NULL, PERIOD_H1, 0.06, 0.1, 0);
         sa60_1  = iSAR(NULL, PERIOD_H1, 0.06, 0.1, 1);
         //---------------------------------------------   
         if(sa15_0 > sa30_0) saUp = sa15_0;
         else saUp = sa30_0;
         if(saUp > sa60_0) saUp = saUp;
         else saUp = sa60_0;
         //---
         if(sa15_0 < sa30_0) saDn = sa15_0;
         else saDn = sa30_0;
         if(saDn < sa60_0) saDn = saDn;
         else saDn = sa60_0;
         //---
         saDif = (saUp - saDn) / Point;
         //-------------------------------         
         if(saDif <= 19)
          {
            if((sa15_0 < iLow(NULL, PERIOD_M15, 0) && sa30_0 < iLow(NULL, PERIOD_M30, 0) && sa60_1 > iHigh(NULL, PERIOD_H1, 1) && sa60_0 < iLow(NULL, PERIOD_H1, 0)) ||
               (sa15_0 < iLow(NULL, PERIOD_M15, 0) && sa60_0 < iLow(NULL, PERIOD_H1, 0) && sa30_1 > iHigh(NULL, PERIOD_M30, 1) && sa30_0 < iLow(NULL, PERIOD_M30, 0)) ||
               (sa30_0 < iLow(NULL, PERIOD_M30, 0) && sa60_0 < iLow(NULL, PERIOD_H1, 0) && sa15_1 > iHigh(NULL, PERIOD_M15, 1) && sa15_0 < iLow(NULL, PERIOD_M15, 0)))
             {
               ticket = OrderSend(Symbol(), OP_BUY, GetLots(), Ask, 3, Ask - SL * Point, Ask + TP * Point, "Nik-Psar", MagicNum, 0, Green);
               if(ticket > 0)
                {            
                  return(0);
                }   
             }
            if((sa15_0 > iHigh(NULL, PERIOD_M15, 0) && sa30_0 > iHigh(NULL, PERIOD_M30, 0) && sa60_1 < iLow(NULL, PERIOD_H1, 1) && sa60_0 > iHigh(NULL, PERIOD_H1, 0)) ||
               (sa15_0 > iHigh(NULL, PERIOD_M15, 0) && sa60_0 > iHigh(NULL, PERIOD_H1, 0) && sa30_1 < iLow(NULL, PERIOD_M30, 1) && sa30_0 > iHigh(NULL, PERIOD_M30, 0)) ||
               (sa30_0 > iHigh(NULL, PERIOD_M30, 0) && sa60_0 > iHigh(NULL, PERIOD_H1, 0) && sa15_1 < iLow(NULL, PERIOD_M15, 1) && sa15_0 > iHigh(NULL, PERIOD_M15, 0)))
             {
               ticket = OrderSend(Symbol(), OP_SELL, GetLots(), Bid, 3, Bid + SL * Point, Bid - TP * Point, "Nik-Psar", MagicNum, 0, Red);
               if(ticket > 0)
                {
                  return(0); 
                }  
             }
          }   
       }         
    }
   //==================================== 
   double saCloMod_1 = 0, saCloMod_0 = 0;
   total = OrdersTotal();
   for(cnt = 0; cnt < total; cnt++)
    {
      OrderSelect(cnt, SELECT_BY_POS, MODE_TRADES);
      if(OrderType() <= OP_SELL && OrderSymbol() == Symbol() && OrderMagicNumber() == MagicNum)
       {
         saCloMod_1 = iSAR(NULL, PERIOD_M30, 0.06, 0.1, 1);
         saCloMod_0 = iSAR(NULL, PERIOD_M30, 0.06, 0.1, 0);
         //-----------------------         
         if(OrderType() == OP_BUY)
          {                
            if(saCloMod_1 < Low[1] && saCloMod_0 > High[0])
             {
               OrderClose(OrderTicket(), OrderLots(), Bid, 3, Violet); 
               return(0); 
             }
            if(saCloMod_0 >= OrderOpenPrice() && saCloMod_0 > OrderStopLoss())
             {               
               OrderModify(OrderTicket(), OrderOpenPrice(), saCloMod_0, OrderTakeProfit(), 0, Green);
               return(0);
             }
          }
         else if(OrderType() == OP_SELL)
          {                
            if(saCloMod_1 > High[1] && saCloMod_0 < Low[0])
             {
               OrderClose(OrderTicket(), OrderLots(), Ask, 3, Violet); 
               return(0); 
             }
            if(saCloMod_0 <= OrderOpenPrice() && saCloMod_0 < OrderStopLoss())
             {               
               OrderModify(OrderTicket(), OrderOpenPrice(), saCloMod_0, OrderTakeProfit(), 0, Red);
               return(0);
             }
          }
       }
    }   
   return(0);
 }