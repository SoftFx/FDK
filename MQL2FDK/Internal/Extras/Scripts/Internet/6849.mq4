//+------------------------------------------------------------------+
//|                                                          pp2.mq4 |
//|                      Copyright © 2010,                           |
//|                                                                  |
//+------------------------------------------------------------------+
#property copyright " www.Forexyangu.com"
#property link      "http://www.ForexYangu.com"

//+------------------------------------------------------------------+
//| expert initialization function                                   |
//+------------------------------------------------------------------+
//+------------------------------------------------------------------
extern string ToDonateContact ="admin@forexyangu.com";
extern int StopLoss   =90;     // SL for an opened order
extern int TakeProfit =20;     // ?? for an opened order
extern double Lots    =0.1;    // Strictly set amount of lots
extern int Slippage   =5;      // Price Slippage
extern string Caution  ="Advanced settings follow. Dont change if you dont know what you are doing.";
extern double Step    =0.001;   //Parabolic setting
extern double Maximum =0.2;    //Parabolic setting
extern string StopMultExplained  ="Improves broker compatibility";
extern string StopMultCaution  ="10 and 1 are the only known values for StopMult";
extern int StopMult   =10;      // Stop values will be multiplied by this
extern string ContactMe ="admin@forexyangu.com"; // support email

/*****************************************************-----READ THIS-------******************************************************
 *******************************************************************************************************************************/
 //-----------------------------------------------------------------------------------------------------------------------------
/*DONATE TO SUPPORT MY FREE PROJECTS AND TO RECEIVE NON OPEN PROJECTS AND ADVANCED VERSIONS OF EXISTING PROJECTS WHEN AVAILABLE: 
//------------------------------------------------------------------------------------------------------------------------------
__my moneybookers email is admin@forexyangu.com anyone can easily join moneybookers at www.moneybookers.com and pay people via their 
email through numerous payment methods__*/
//------------------------------------------------------------------------------------------------------------------------------
//SUPPORT AND INQUIRIES EMAIL:        admin@forexyangu.com
//------------------------------------------------------------------------------------------------------------------------------
/*******************************************************************************************************************************
 *************************************************--------END------*************************************************************/

/* Expert designed to open and close trades at first Parabolic SAR dots. Performance depends on your custom parameters 
Contact me at tonnyochieng@gmail.com */
//+------------------------------------------------------------------+
//| expert start function                                            |
//+------------------------------------------------------------------+
int start()
  {
  //trading criteria
    bool   result;
   double price;
   int    cmd,error,TP,SL;
   



TP=NormalizeDouble(TakeProfit*StopMult,Digits);
SL=NormalizeDouble(StopLoss*StopMult,Digits);

 //close
 if ((iSAR(NULL, 0,Step,Maximum, 0)<iClose(NULL,0,0))&&(iSAR(NULL, 0,Step,Maximum, 1)>iClose(NULL,0,1))) //Signal Buy
 {

   if(OrderSelect(0,SELECT_BY_POS,MODE_TRADES))
     {
      cmd=OrderType();
      //---- open order is sell
      if(cmd==OP_SELL)
        {
         while(true)
           {
            if(cmd==OP_BUY) price=Bid;
            else            price=Ask;
            result=OrderClose(OrderTicket(),OrderLots(),price,Slippage,CLR_NONE);
            if(result!=TRUE) { error=GetLastError(); Print("LastError = ",error); }
            else error=0;
            if(error==135) RefreshRates();
            else break;
           }
        }
     }
   else Print( "Error when order select ", GetLastError());

     // Closing Sell 
     //open buy
    if(OrdersTotal()==0)
    {
    int openbuy=OrderSend(Symbol(),OP_BUY,Lots,Ask,Slippage,NormalizeDouble(Ask-SL*Point,Digits),NormalizeDouble(Ask+TP*Point,Digits));//Opening Buy
     if(openbuy==TRUE) {Alert("Order opened !!!"); PlaySound("alert.wav"); }
    }
 
 }
 if ((iSAR(NULL, 0,Step,Maximum, 0)>iClose(NULL,0,0))&&(iSAR(NULL, 0,Step,Maximum, 1)<iClose(NULL,0,1))) //Signal Sell
 {

    if(OrderSelect(0,SELECT_BY_POS,MODE_TRADES))
     {
      cmd=OrderType();
      //---- open order is buy
      if(cmd==OP_BUY)
        {
         while(true)
           {
            if(cmd==OP_BUY) price=Bid;
            else            price=Ask;
            result=OrderClose(OrderTicket(),OrderLots(),price,Slippage,CLR_NONE);
            if(result!=TRUE) { error=GetLastError(); Print("LastError = ",error); }
            else error=0;
            if(error==135) RefreshRates();
            else break;
           }
        }
     }
   else Print( "Error when order select ", GetLastError());
 
      // Closing Buy 
      //open sell
    if(OrdersTotal()==0)
    {
     int opensell=OrderSend(Symbol(),OP_SELL,Lots,Bid,Slippage,NormalizeDouble(Bid+SL*Point,Digits),NormalizeDouble(Bid-TP*Point,Digits));//Opening Sell
     if(openbuy==TRUE) {Alert("Order opened !!!"); PlaySound("alert.wav"); }
    }
 

}




//----------
return(0);
  }
//------------------------------------------------------------------+