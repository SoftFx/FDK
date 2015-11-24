//+------------------------------------------------------------------+
//|                                                  Trader Test.mq4 |
//|                      Copyright © 2009, MetaQuotes Software Corp. |
//|                                        http://www.metaquotes.net |
//+------------------------------------------------------------------+


//-----

#define report      "[ROOT]\\experts\\files\\[ACCOUNT_NUM].html"
#define report_close_price      "[ROOT]\\experts\\files\\[ACCOUNT_NUM]rep.txt"
#define report_not_exec_orders      "[ROOT]\\experts\\files\\[ACCOUNT_NUM]not_exec_rep.txt"
#define current_folder     "[ROOT]\\experts\\files\\"

//-----

#import "shell32.dll"
  int ShellExecuteA(int hWnd, string lpVerb, string lpFile, string lpParameters, string lpDirectory, int nCmdShow);

#import "kernel32.dll"
  void ExitProcess(int uExitCode);
  bool TerminateProcess(int hProcess, int uExitCode);
  int GetCurrentProcess();



  int GetModuleFileNameA(int hModule, int& buf[], int len);

  int _lcreat(string path, int attr);
  int _lopen(string path, int mode);
  int _lclose(int hFile);  
  int _llseek(int hFile, int offset, int origin);   
  int _lread(int hFile, int& buf[], int len);
  int _lwrite(int hFile, string buf, int len);
  
  string lstrcat(string s1, string s2);
  //int lstrcat(int s1[], int s2[]);
  
  string LocalAlloc(int uFlags, int uBytes);
  
#import


#define OF_READ             0x00000000
#define OF_WRITE            0x00000001
#define OF_READWRITE        0x00000002

#define HFILE_ERROR         0xFFFFFFFF


#define FILE_BEGIN          0
#define FILE_CURRENT        1
#define FILE_END            2


#define SW_SHOW             5

#define OP_BALANCE          6



static int hFile;
static int hFile_close_price;
static int hFile_not_exec_orders;
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~



#property copyright "Copyright © 2009, MetaQuotes Software Corp."
#property link      "http://www.metaquotes.net"

#define StatSize 100

extern bool SLTP_Test_Market_Orders = true;
extern bool Test_Limit_Orders = true;
extern bool Test_Stop_Orders = true;
extern bool Marging_Test_Limit_Orders = false;
extern bool Marging_Test_Stop_Orders = false;
extern bool Test_For_All_Order_Types = true;
extern bool Create_And_Modify_All_Order = true;
extern bool Test_CloseBy = true;
extern bool Test_Magic = true;
extern double Order_Volume = 9.5;
extern int Amount_Diff_Order_in_one_Time = 6;
extern int Create_Order_Period = 30;//1 sec
extern int Duration = 6000;// 1 min
extern datetime Expiration_Date = 0;
extern int MaxSL = 100; 
extern int MaxTP = 100; 
extern int MagicNumber = 999; 
extern bool LocalFTPServer = true;
extern bool SendReport = true;
extern string FTPFolder = "/builds/reports/";
extern string InFolder = "C:\\inetpub\\ftproot\\builds\\reports\\outhouse\\";
//extern int MoreTradesTime = 300;//5 min


static int coeff = 1;
static int coeffSL = 1;
static int coeffTP = 1;
static int finish = 0;
static int digits = 5;//double to str
static datetime currtime;
static datetime DTStartTestTime;

static string path;
static string file_name;
static string path_close_price;
static string path_not_exec_orders;
static string path_to_current_folder;
static double firstexecutionprice = 0;
static int executiontime_start = 0;
static int executiontime_end = 0;


bool b_start = true;
int BuySL = 0;
int BuyTP = 0;
int SellSL = 0;
int SellTP = 0;
int BuySLworse = 0; 
int BuySLbetter = 0; 
int SellSLworse = 0; 
int SellSLbetter = 0; 
int BuyTPworse = 0; 
int BuyTPbetter = 0; 
int SellTPworse = 0; 
int SellTPbetter = 0; 

bool CreateLimitOrdersForMargingTest()
{
int ticket;
   RefreshRates();
   double price;
   
   price = Ask - Point * 50 * coeff;
   string str = "OP_BUYLIMIT price:" + DoubleToStr(price, digits);
   ticket = OrderSend(Symbol(), OP_BUYLIMIT, 1/*Order_Volume*/, price, 1, 0, 0, str, MagicNumber, Expiration_Date, CLR_NONE);
 
   price = Bid + Point * 50 * coeff;
   str = "OP_SELLLIMIT price:" + DoubleToStr(price, digits);
   ticket = OrderSend(Symbol(), OP_SELLLIMIT, 1/*Order_Volume*/, price, 1, 0, 0, str, MagicNumber, Expiration_Date, CLR_NONE);
   
   if(ticket<0)
   {
      int err = GetLastError();
      Print("OrderSend failed with error #",err);
      if (err != 130)
      {
         //Alert("Can`t add order");
      }
      if (err == 134)
      {
         //Alert("NOT_ENOUGH_MONEY, OrdersTotal #",OrdersTotal());
         finish = true;
      }
      return(false);
   }  
   else
   {
      Print("OrderSend started");
      Print("ticket# ",ticket," MagicNumber# ",MagicNumber);
      Print("AccountBalance# ",AccountBalance());

      return (true);
   }
}

bool CreateStopOrdersForMargingTest()
{
int ticket;
   RefreshRates();
   double price;
   
    price = Ask + Point * 50 * coeff;
    string str = "OP_BUYSTOP price:" + DoubleToStr(price, digits);
    ticket = OrderSend(Symbol(), OP_BUYSTOP, 1/*Order_Volume*/, price, 1, 0, 0, str, MagicNumber, Expiration_Date, CLR_NONE);
    price = Bid - Point * 50 * coeff;
    str = "OP_SELLSTOP price:" + DoubleToStr(price, digits);
    ticket = OrderSend(Symbol(), OP_SELLSTOP, 1/*Order_Volume*/, price, 1, 0, 0, str, MagicNumber, Expiration_Date, CLR_NONE);
   
   if(ticket<0)
   {
      int err = GetLastError();
      Print("OrderSend failed with error #",err);
      if (err != 130)
      {
         //Alert("Can`t add order");
      }
      if (err == 134)
      {
         //Alert("NOT_ENOUGH_MONEY, OrdersTotal #",OrdersTotal());
         finish = true;
      }
      return(false);
   }  
   else
   {
      Print("OrderSend started");
      Print("ticket# ",ticket," MagicNumber# ",MagicNumber);
      Print("AccountBalance# ",AccountBalance());

      return (true);
   }
}


bool CreateOrder( int Type/*OP_BUY,OP_SELL*/)
{

MathSrand(TimeLocal());

   int RandVol = Order_Volume + MathRand() / 3270;



int ticket;
string str;
double price, tp, sl;
string strType;
      RefreshRates();
   Print("AccountBalance#",AccountBalance());
   datetime CreateTime = LocalTime();
   if (Type == OP_BUY)
   {RandVol = Order_Volume + MathRand() / 3270;
    strType = "OP_BUY";
    price = Ask;
    str = "OP_BUY price:" + DoubleToStr(price, digits);
    sl = 0;
    tp = 0;
    ticket = OrderSend(Symbol(), OP_BUY, RandVol, price, 1, sl, tp, str, MagicNumber, Expiration_Date, CLR_NONE);
   } 
   else if (Type == OP_SELL)
   {RandVol = Order_Volume + MathRand() / 3270;
    strType = "OP_SELL";
    price = Bid;
    str = "OP_SELL price:" + DoubleToStr(price, digits);
    sl = 0;
    tp = 0;
    ticket = OrderSend(Symbol(), OP_SELL, RandVol, price, 1, sl, tp, str, MagicNumber, Expiration_Date, CLR_NONE);
   }
   else if (Type == OP_BUYLIMIT)
   {RandVol = Order_Volume + MathRand() / 3270;
    strType = "OP_BUYLIMIT";
    price = Ask - Point * 2 * coeff;
    str = "OP_BUYLIMIT price:" + DoubleToStr(price, digits);
    sl = Ask - Point * coeffSL*4;
    tp = Ask - Point * coeffTP;
    if (coeffSL == 0) sl = 0;
    if (coeffTP == 0) tp = 0;
    ticket = OrderSend(Symbol(), OP_BUYLIMIT, RandVol, price, 1, sl, tp, str, MagicNumber, Expiration_Date, CLR_NONE);
   }
   else if (Type == OP_SELLLIMIT)
   {RandVol = Order_Volume + MathRand() / 3270;
    strType = "OP_SELLLIMIT";
    price = Bid + Point * 2 * coeff;
    str = "OP_SELLLIMIT price:" + DoubleToStr(price, digits);
    sl = Bid + Point * coeffSL*4;
    tp = Bid + Point * coeffTP;
    if (coeffSL == 0) sl = 0;
    if (coeffTP == 0) tp = 0;
   ticket = OrderSend(Symbol(), OP_SELLLIMIT, RandVol, price, 1, sl, tp, str, MagicNumber, Expiration_Date, CLR_NONE);
   }
   else if (Type == OP_BUYSTOP)
   {RandVol = Order_Volume + MathRand() / 3270;
    strType = "OP_BUYSTOP";
    price = Ask + Point * coeff;
    str = "OP_BUYSTOP price:" + DoubleToStr(price, digits);
    sl = Ask - Point * coeffSL*8;
    tp = Ask + Point * coeffTP*40;
    if (coeffSL == 0) sl = 0;
    if (coeffTP == 0) tp = 0;
    ticket = OrderSend(Symbol(), OP_BUYSTOP, RandVol, price, 1, sl, tp, str, MagicNumber, Expiration_Date, CLR_NONE);
   }
   else if (Type == OP_SELLSTOP)
   {RandVol = Order_Volume + MathRand() / 3270;
    strType = "OP_SELLSTOP";
    price = Bid - Point * coeff;
    str = "OP_SELLSTOP price:" + DoubleToStr(price, digits);
    sl = Bid + Point * coeffSL*8;
    tp = Bid - Point * coeffTP*40;
    if (coeffSL == 0) sl = 0;
    if (coeffTP == 0) tp = 0;
    ticket = OrderSend(Symbol(), OP_SELLSTOP, RandVol, price, 1, sl, tp, str, MagicNumber, Expiration_Date, CLR_NONE);
   }
   
   if(ticket<0)
   {
      int err = GetLastError();
      Print("OrderSend failed with error #",err);
      if (err != 130 && err != 132 && err != 133 && err != 134 && err != 136 && err != 4051)
      {
         SetCreateOrderErrorToReport(CreateTime, strType, Symbol(), price, sl,  tp, err);
         //Alert("Can`t add order");
      }
      return(false);
   }  
   else
   {
      Print("OrderSend started");
      Print("ticket# ",ticket," MagicNumber# ",MagicNumber);
      Print("AccountBalance# ",AccountBalance());

      return (true);
   }
}

bool CreateAndModifyOrder( int Type/*OP_BUY,OP_SELL*/)
{
int ticket;
string str;
string strType;
double price, sl, tp;
bool m;
      datetime CreateTime = LocalTime();
      RefreshRates();
   Print("AccountBalance#",AccountBalance());
   if (Type == OP_BUY)
   {
     strType = "OP_BUY";
     price = Ask;
     str = "OP_BUY price:" + DoubleToStr(price, digits);
     sl = 0;
     tp = 0;
     ticket = OrderSend(Symbol(), OP_BUY, Order_Volume, price, 1, sl, tp, str, MagicNumber, Expiration_Date, CLR_NONE);
     m = OrderModify( ticket, 0, Ask-2*Point*20, Bid + 2*Point*20, 0);
   } 
   else if (Type == OP_SELL)
   {
     strType = "OP_SELL";
     price = Bid;
     str = "OP_SELL price:" + DoubleToStr(price, digits);
     sl = 0;
     tp = 0;
     ticket = OrderSend(Symbol(), OP_SELL, Order_Volume, price, 1, sl, tp, str, MagicNumber, Expiration_Date, CLR_NONE);
     m = OrderModify( ticket, 0, Bid + 2*Point*20, Ask - 2*Point*20, 0);
   }
   else if (Type == OP_BUYLIMIT)
   {
     strType = "OP_BUYLIMIT";
     price = Ask - Point * coeff;
     str = "OP_BUYLIMIT price:" + DoubleToStr(price, digits);
     sl = Ask - Point * coeffSL*4;
     tp = Ask - Point * coeffTP*2;
     if (coeffSL == 0) sl = 0;
     if (coeffTP == 0) tp = 0;
     ticket = OrderSend(Symbol(), OP_BUYLIMIT, Order_Volume, price, 1, sl, tp, str, MagicNumber, Expiration_Date, CLR_NONE);
     if (ticket!= -1 && OrderSelect(ticket,SELECT_BY_TICKET)) 
       m = OrderModify( ticket, OrderOpenPrice(), OrderOpenPrice() - Point*8*20, OrderOpenPrice() - Point*4*20, 0);
   }
   else if (Type == OP_SELLLIMIT)
   {
     strType = "OP_SELLLIMIT";
     price = Bid + Point * coeff;
     str = "OP_SELLLIMIT price:" + DoubleToStr(price, digits);
     sl = Bid + Point * coeffSL*4;
     tp = Bid + Point * coeffTP*2;
     if (coeffSL == 0) sl = 0;
     if (coeffTP == 0) tp = 0;
     ticket = OrderSend(Symbol(), OP_SELLLIMIT, Order_Volume, price, 1, sl, tp, str, MagicNumber, Expiration_Date, CLR_NONE);
     if (ticket!= -1 && OrderSelect(ticket,SELECT_BY_TICKET)) 
        m = OrderModify( ticket, OrderOpenPrice(), OrderOpenPrice() + Point*8*20, OrderOpenPrice() + Point*4*20, 0);
   }
   else if (Type == OP_BUYSTOP)
   {
     strType = "OP_BUYSTOP";
     price = Ask + Point * coeff;
     str = "OP_BUYSTOP price:" + DoubleToStr(price, digits);
     sl = Ask - Point * coeffSL*2;
     tp = Ask + Point * coeffTP*4;
     if (coeffSL == 0) sl = 0;
     if (coeffTP == 0) tp = 0;
     ticket = OrderSend(Symbol(), OP_BUYSTOP, Order_Volume, price, 1, sl, tp, str, MagicNumber, Expiration_Date, CLR_NONE);
     if (ticket!= -1 && OrderSelect(ticket,SELECT_BY_TICKET)) 
        m = OrderModify( ticket, OrderOpenPrice(), OrderOpenPrice() - Point*4*20, OrderOpenPrice() + Point*8*20, 0);
  }
   else if (Type == OP_SELLSTOP)
   {
      strType = "OP_SELLSTOP";
      price = Bid - Point * coeff;
      str = "OP_SELLSTOP price:" + DoubleToStr(price, digits);
      sl = Bid + Point * coeffSL*2;
      tp = Bid - Point * coeffTP*4;
      if (coeffSL == 0) sl = 0;
      if (coeffTP == 0) tp = 0;
      ticket = OrderSend(Symbol(), OP_SELLSTOP, Order_Volume, price, 1, sl, tp, str, MagicNumber, Expiration_Date, CLR_NONE);
      if (ticket!= -1 && OrderSelect(ticket,SELECT_BY_TICKET)) 
         m = OrderModify( ticket, OrderOpenPrice(), OrderOpenPrice() + Point*4*20, OrderOpenPrice() - Point*8*20, 0);
   }
   
   if(ticket<0)
   {
      int err = GetLastError();
      Print("OrderSend failed with error # ",err);
      if (err != 130 && err != 132 && err != 133 && err != 134 && err != 136 && err != 4051)
      {
         //Alert("Can`t add order");
         SetCreateOrderErrorToReport(CreateTime, strType, Symbol(), price, sl,  tp, err);
      }
      return(false);
   }  
   else
   {
         Print("OrderSend started");
      Print("ticket# ",ticket," MagicNumber# ",MagicNumber);
      if (!m)
      {
         Print("ticket# ",ticket," was not modified error # ",GetLastError());
         return(false);
      }
         Print("ticket# ",ticket," was modified");
         return (true);
   }
}



bool CloseBy()
{
   int ticket1 = 0;
   int ticket2 = 0;
   int Orders = OrdersTotal(); 
   string s1;
   for ( int j = 0; j < Orders; j++)
   {
      for (int i = OrdersTotal()-1; i >= 0; i--)
      {
 //        Print("OrderTotal #",OrdersTotal());
         OrderSelect( i, SELECT_BY_POS );
  //       Print("Order Type #",OrderType());
    /*     if (OrderSelect( i, SELECT_BY_POS ) && (OrderType() == OP_BUYLIMIT || OrderType() == OP_SELLLIMIT || OrderType() == OP_BUYSTOP || OrderType() == OP_SELLSTOP ))
         {
            if (OrderDelete(OrderTicket()))
            {
               Print("Delete order# ", OrderTicket());
            }
            else
            {
               Print("Delete order with error #",GetLastError());
               Alert("Can`t delete order");
               return(false);
            }
        }*/
        if (ticket1 == 0 && OrderSelect( i, SELECT_BY_POS ) && OrderType() == OP_BUY)
         {
            ticket1 = OrderTicket();
            Print("ticket1# ", ticket1);
            if (ticket1 == 0) continue;
            s1 = CloseByOrderErrorToReport1(OrderTicket(), OrderOpenTime(), OrderType(), OrderSymbol(), OrderOpenPrice(), OrderStopLoss(), OrderTakeProfit(), OrderClosePrice());

         }
      }//i
      if (ticket1 != 0)
      {
         for (i = OrdersTotal()-1; i >= 0; i--)
         {
            if (ticket2 == 0 && OrderSelect( i, SELECT_BY_POS ) && OrderType() == OP_SELL)
            {
               ticket2 = OrderTicket();
                  Print("ticket2# ", ticket2);
            }
            if (ticket2 == 0) continue;
   
            if (OrderCloseBy(ticket1, ticket2))
            {
               Print("# ", ticket1, "OrderCloseBy #", ticket2);
               break;
            }
            else
            {
            int err = GetLastError();
            if (err != 4108)
               CloseByOrderErrorToReport2(OrderTicket(), OrderOpenTime(), OrderType(), OrderSymbol(), OrderOpenPrice(), OrderStopLoss(), OrderTakeProfit(), OrderClosePrice(), err, s1);
               Print("CloseBy order with error #", err);
               //Alert("Can`t make CloseBy");
               return (false);
            }
         }//i
      }
   }//j
}

bool CloseAll()
{
   int ticket1 = 0;
   int ticket2 = 0;
   for (int i = OrdersTotal()-1; i >= 0; i--)
   {
      OrderSelect( i, SELECT_BY_POS );
      if (OrderSelect( i, SELECT_BY_POS ) && (OrderType() == OP_BUYLIMIT || OrderType() == OP_SELLLIMIT || OrderType() == OP_BUYSTOP || OrderType() == OP_SELLSTOP ))
      {
         if (OrderDelete(OrderTicket()))
         {
            Print("Delete limt order# ", OrderTicket());
         }
         else
         {
            int err = GetLastError();
            Print("Order # ",OrderTicket(),"  Delete order with error # ", err);
            SetCloseOrderErrorToReport(OrderTicket(), OrderOpenTime(), OrderType(), OrderSymbol(), OrderOpenPrice(), OrderStopLoss(), OrderTakeProfit(), OrderClosePrice(), err);
            //Alert("Can`t delete limit order");
            continue;
         }
      } else
      if (OrderSelect( i, SELECT_BY_POS ) && (OrderType() == OP_BUY ))
      {
      RefreshRates();
      double price = Bid;
         if (OrderClose(OrderTicket(), OrderLots(), price, 1, CLR_NONE))
         {
            //Alert("Order closed");
            string str = OrderTicket();
            for (int j = StringLen(str); j < 20; j++)
            {
               str = str + " ";
            }
            //Alert("Write to file ");
            _lwrite(hFile_close_price, str, StringLen(str));
            
            str = DoubleToStr(price, digits);
            for (j = StringLen(str); j < 20; j++)
            {
               str = str + " ";
            }
             //Alert("Write to file 2");
           _lwrite(hFile_close_price, str, StringLen(str));
            
            Print("Delete order# ", OrderTicket());
         }
         else
         {
            Print("Order # ",OrderTicket(),"  Delete order with error # ",GetLastError());
            //Alert("Can`t delete order");
            continue;
         }
      } else
      if (OrderSelect( i, SELECT_BY_POS ) && (OrderType() == OP_SELL ))
      {
      RefreshRates();
         price = Ask;
         if (OrderClose(OrderTicket(), OrderLots(), price, 1, CLR_NONE))
         {
             //Alert("Order closed");
           str = OrderTicket();
            for (j = StringLen(str); j < 20; j++)
            {
               str = str + " ";
            }
            _lwrite(hFile_close_price, str, StringLen(str));
            
            str = DoubleToStr(price, digits);
            for (j = StringLen(str); j < 20; j++)
            {
               str = str + " ";
            }
            _lwrite(hFile_close_price, str, StringLen(str));

            Print("Delete order# ", OrderTicket());
         }
         else
         {
            err = GetLastError();
            Print("Order # ",OrderTicket(),"  Delete order with error # ", err);
            SetCloseOrderErrorToReport(OrderTicket(), OrderOpenTime(), OrderType(), OrderSymbol(), OrderOpenPrice(), OrderStopLoss(), OrderTakeProfit(), OrderClosePrice(), err);
            //Alert("Can`t delete order");
            continue;
         }
      }
   }

}

bool Test(int orders)
{
   if(IsTradeContextBusy())
   {
      Alert("IsTradeContextBusy");
      return (false);
   }
   for (int i = 0; i < orders; i++)
   {
   coeff = 100 * MathRand()/32767;
   coeffSL = MaxSL * MathRand()/32767;
   coeffTP = MaxTP * MathRand()/32767;
  /*    CreateOrderWithMinCoeff(OP_BUYLIMIT);
      CreateOrderWithMinCoeff(OP_SELLLIMIT);
      CreateOrderWithMinCoeff(OP_BUYSTOP);
      CreateOrderWithMinCoeff(OP_SELLSTOP);
    */  
      if (SLTP_Test_Market_Orders)
      {
         CreateAndModifyOrder(OP_BUY);
         CreateAndModifyOrder(OP_SELL);
      }
      if (Test_Limit_Orders)
      {
         CreateOrder(OP_BUYLIMIT);
         CreateOrder(OP_SELLLIMIT);
      }
      if (Test_Stop_Orders)
      {
         CreateOrder(OP_BUYSTOP);
         CreateOrder(OP_SELLSTOP);
      }
      if (Marging_Test_Limit_Orders)
      {
         for (int j = 0; j < 100; j++)
            CreateLimitOrdersForMargingTest();
      }
      if (Marging_Test_Stop_Orders)
      {
         for (j = 0; j < 100; j++)
            CreateStopOrdersForMargingTest();
      }

      if (Test_For_All_Order_Types)
      {
         CreateOrder(OP_BUY);
         CreateOrder(OP_SELL);
         CreateOrder(OP_BUYLIMIT);
         CreateOrder(OP_SELLLIMIT);
         CreateOrder(OP_BUYSTOP);
         CreateOrder(OP_SELLSTOP);
      }
     /* CreateOrder(OP_BUYLIMIT);
      CreateOrder(OP_SELLLIMIT);
      CreateOrder(OP_BUYSTOP);
      CreateOrder(OP_SELLSTOP);*/
      
      if (Create_And_Modify_All_Order)
      {
         CreateAndModifyOrder(OP_BUY);
         CreateAndModifyOrder(OP_SELL);
         CreateAndModifyOrder(OP_BUYLIMIT);
         CreateAndModifyOrder(OP_SELLLIMIT);
         CreateAndModifyOrder(OP_BUYSTOP);
         CreateAndModifyOrder(OP_SELLSTOP);
      }      
    /*CreateStopOrder(OP_BUYSTOP);
    CreateStopOrder(OP_SELLSTOP);*/
   }
}

bool CheckMagic()
{
RefreshRates();
    string str = "<tr align=left bgcolor=#E9E9E9>";
    _lwrite(hFile, str, StringLen(str));
    str = "  <td colspan=\"15\">Test Magic:</td>";
    _lwrite(hFile, str, StringLen(str));
    str = "</tr>";
    _lwrite(hFile, str, StringLen(str));
   
      for (int i = OrdersTotal()-1; i >= 0; i--)
      {
         if (OrderSelect( i, SELECT_BY_POS ))
         {
            if (OrderMagicNumber() != MagicNumber)
            {
            
                str = "  <td colspan=\"15\">Order: " + OrderTicket() + " Magic: " + OrderMagicNumber() + "</td>";
                _lwrite(hFile, str, StringLen(str));
                str = "</tr>";
                _lwrite(hFile, str, StringLen(str));
             //Print("OrderMagicNumber was chaged!!! Order # ",OrderTicket());
               //Alert("OrderMagicNumber absent!!!");
            }
           
         }
      }
}

void StatisticsLimitOrders()
{
string comment;
  // retrieving info from trade history
  int i,accTotal=OrdersHistoryTotal();
  
  int BuyLimitLess = 0;
  int BuyLimitMore = 0;
  int BuyLimitCount = 0;
  int SellLimitLess = 0;
  int SellLimitMore = 0;
  int SellLimitCount = 0;
  for(i=0;i<accTotal;i++)
    {
     //---- check selection result
         if(OrderSelect(i,SELECT_BY_POS,MODE_HISTORY)==false )
           {
            //Print("Get history error (",GetLastError(),")");
            continue;
           }
         if ( currtime > OrderOpenTime()) continue;
 
   int char_codePrice;
   int char_codePriceComment;
     string strPrice;
     string strPriceComment;
 
        if ( OrderType() == OP_BUY )
         {
           comment = OrderComment();
           int pos = StringFind(comment,"OP_BUYLIMIT price:");
           if (-1 != pos)
           {
              strPrice = DoubleToStr(OrderOpenPrice(), 5);
              strPriceComment = StringSubstr(comment,pos+18);
              //Alert("OP_BUYSTOP price:", StringSubstr(comment,pos+17));
           }
           else continue;
              for (int ii = 0; ii < StringLen(strPrice); ii++)
              {
                  char_codePrice = StringGetChar(strPrice, ii);
                  char_codePriceComment = StringGetChar(strPriceComment, ii);
                  if (char_codePrice != char_codePriceComment)
                  {
                     if (char_codePrice < char_codePriceComment)
                     {
                       BuyLimitLess++;
                     }
                     if (char_codePrice > char_codePriceComment)
                     {
                       BuyLimitMore++;
                     }
                     break;
                  }
              }
                  BuyLimitCount++;
         }
         if ( OrderType() == OP_SELL )
         {
  //       Print("OP_SELL");
           comment = OrderComment();
           pos = StringFind(comment,"OP_SELLLIMIT price:");
           if (-1 != pos)
           {
              strPrice = DoubleToStr(OrderOpenPrice(), 5);
              strPriceComment = StringSubstr(comment,pos+19);
              //Alert("OP_SELLSTOP price:", StringSubstr(comment,pos+18));
           }
           else continue;
              for (ii = 0; ii < StringLen(strPrice); ii++)
              {
                  char_codePrice = StringGetChar(strPrice, ii);
                  char_codePriceComment = StringGetChar(strPriceComment, ii);
                  if (char_codePrice != char_codePriceComment)
                  {
                     if (char_codePrice < char_codePriceComment)
                     {
                       SellLimitLess++;
                     }
                     if (char_codePrice > char_codePriceComment)
                     {
                       SellLimitMore++;
                     }
                     break;
                  }
              }
                SellLimitCount++;
         }
    }
    
    
         string s[2];
         int amount = BuyLimitCount-BuyLimitMore-BuyLimitLess;
         int amount2 = SellLimitCount-SellLimitLess-SellLimitMore;
         s[0] = "<tr align=center bgcolor=#E9E9E9><td>"+"BUYLIMIT orders:"+
         "</td><td nowrap>" + BuyLimitCount + 
         "</td><td>" + amount + 
         "</td><td>" + BuyLimitMore +
         "</td><td>" + BuyLimitLess +
         "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td></tr>";
         s[1] = "<tr align=center bgcolor=#FFFFFF><td>"+"SELLLIMIT orders:"+
         "</td><td nowrap>" + SellLimitCount + 
         "</td><td>" + amount2 + 
         "</td><td>" + SellLimitLess +
         "</td><td>" + SellLimitMore +
         "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td></tr>";
   
      for (i = 0; i < 2; i++)
      {
       _lwrite(hFile, s[i], StringLen(s[i]));
      }

}

void StatisticsStopOrders()
{
string comment;
  // retrieving info from trade history
  int i,accTotal=OrdersHistoryTotal();
  
  int BuyStopLess = 0;
  int BuyStopMore = 0;
  int BuyStopCount = 0;
  int SellStopLess = 0;
  int SellStopMore = 0;
  int SellStopCount = 0;
  double dPrice;
  int pos;
  for(i=0;i<accTotal;i++)
    {
     //---- check selection result
         if(OrderSelect(i,SELECT_BY_POS,MODE_HISTORY)==false )
           {
            //Print("Get history error (",GetLastError(),")");
            continue;
           }
         if ( currtime > OrderOpenTime()) continue;
    int char_codePrice;
    int char_codePriceComment;
      string strPrice;
      string strPriceComment;
        if ( OrderType() == OP_BUY )
         {
           comment = OrderComment();
           pos = StringFind(comment,"OP_BUYSTOP price:");
           if (-1 != pos)
           {
            // dPrice = StrToDouble(StringSubstr(comment,pos+17));
              strPrice = DoubleToStr(OrderOpenPrice(), 5);
              strPriceComment = StringSubstr(comment,pos+17);
           }
           else 
               {
                  pos = StringFind(comment,"[sl]OP_BUYSTOP price:");
                  if (-1 != pos)
                  {
                      //dPrice = StrToDouble(StringSubstr(comment,pos+21));
                     strPrice = DoubleToStr(OrderOpenPrice(), 5);
                     strPriceComment = StringSubstr(comment,pos+21);
                  }
                  else
                  {
                     pos = StringFind(comment,"[tp]OP_BUYSTOP price:");
                     if (-1 != pos)
                     {
                        //dPrice = StrToDouble(StringSubstr(comment,pos+21));
                        strPrice = DoubleToStr(OrderOpenPrice(), 5);
                        strPriceComment = StringSubstr(comment,pos+21);
                     }
                     else continue;
                  }
                }
                
              for (int ii = 0; ii < StringLen(strPrice); ii++)
              {
                  char_codePrice = StringGetChar(strPrice, ii);
                  char_codePriceComment = StringGetChar(strPriceComment, ii);
                  if (char_codePrice != char_codePriceComment)
                  {
                     if (char_codePrice < char_codePriceComment)
                     {
                       BuyStopLess++;
                     }
                     if (char_codePrice > char_codePriceComment)
                     {
                       BuyStopMore++;
                     }
                     break;
                  }
              }
                  BuyStopCount++;

         }
         if ( OrderType() == OP_SELL )
         {
  //       Print("OP_SELL");
           comment = OrderComment();
           pos = StringFind(comment,"OP_SELLSTOP price:");
           if (-1 != pos)
           {
              //dPrice = StrToDouble(StringSubstr(comment,pos+18));
              strPrice = DoubleToStr(OrderOpenPrice(), 5);
              strPriceComment = StringSubstr(comment,pos+18);
           }
           else 
               {
                  pos = StringFind(comment,"[sl]OP_SELLSTOP price:");
                  if (-1 != pos)
                  {
                      //dPrice = StrToDouble(StringSubstr(comment,pos+22));
                     strPrice = DoubleToStr(OrderOpenPrice(), 5);
                     strPriceComment = StringSubstr(comment,pos+22);
                  }
                  else
                  {
                     pos = StringFind(comment,"[tp]OP_SELLSTOP price:");
                     if (-1 != pos)
                     {
                       // dPrice = StrToDouble(StringSubstr(comment,pos+22));
                        strPrice = DoubleToStr(OrderOpenPrice(), 5);
                        strPriceComment = StringSubstr(comment,pos+22);
                     }
                     else continue;
                  }
                }

              for (ii = 0; ii < StringLen(strPrice); ii++)
              {
                  char_codePrice = StringGetChar(strPrice, ii);
                  char_codePriceComment = StringGetChar(strPriceComment, ii);
                  if (char_codePrice != char_codePriceComment)
                  {
                     if (char_codePrice < char_codePriceComment)
                     {
                       SellStopLess++;
                     }
                     if (char_codePrice > char_codePriceComment)
                     {
                       SellStopMore++;
                     }
                     break;
                  }
              }
                  SellStopCount++;
         }
    }
    
    
         string s[2];
         int amount = BuyStopCount-BuyStopMore-BuyStopLess;
         int amount2 = SellStopCount-SellStopLess-SellStopMore;
         s[0] = "<tr align=center bgcolor=#E9E9E9><td>"+"BUYSTOP orders:"+
         "</td><td nowrap>" + BuyStopCount + 
         "</td><td>" + amount + 
         "</td><td>" + BuyStopMore +
         "</td><td>" + BuyStopLess +
         "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td></tr>";
         s[1] = "<tr align=center bgcolor=#FFFFFF><td>"+"SELLSTOP orders:"+
         "</td><td nowrap>" + SellStopCount + 
         "</td><td>" + amount2 + 
         "</td><td>" + SellStopLess +
         "</td><td>" + SellStopMore +
         "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td></tr>";
   
      for (i = 0; i < 2; i++)
      {
       _lwrite(hFile, s[i], StringLen(s[i]));
      }

}

void StatisticsOpenMarketOrders()
{
  // retrieving info from trade history
  int i,accTotal=OrdersHistoryTotal();
  
  int ByLess = 0;
  int ByMore = 0;
  int ByCount = 0;
  int SellLess = 0;
  int SellMore = 0;
  int SellCount = 0;
  for(i=0;i<accTotal;i++)
    {
     //---- check selection result
         if(OrderSelect(i,SELECT_BY_POS,MODE_HISTORY)==false)
           {
            Print("Get history error (",GetLastError(),")");
            continue;
           }
 //         Print("currtime", currtime,"OrderOpenTime", OrderOpenTime());
         if ( currtime > OrderOpenTime()) continue;
    int char_codePrice;
    int char_codePriceComment;
      string strPrice;
      string strPriceComment;

               string comment = OrderComment();
               double dPrice;
         if ( OrderType() == OP_BUY)
         {
               int pos = StringFind(comment,"OP_BUY price:");
               if (-1 != pos)
               {
                  //dPrice = StrToDouble(StringSubstr(comment,pos+13));
                     strPrice = DoubleToStr(OrderOpenPrice(), 5);
                     strPriceComment = StringSubstr(comment,pos+13);
               }
               else continue;
              for (int ii = 0; ii < StringLen(strPrice); ii++)
              {
                  char_codePrice = StringGetChar(strPrice, ii);
                  char_codePriceComment = StringGetChar(strPriceComment, ii);
                  if (char_codePrice != char_codePriceComment)
                  {
                     if (char_codePrice < char_codePriceComment)
                     {
                       ByLess++;
                     }
                     if (char_codePrice > char_codePriceComment)
                     {
                       ByMore++;
                     }
                     break;
                  }
              }
                ByCount++;
         }
         if ( OrderType() == OP_SELL)
         {
               pos = StringFind(comment,"OP_SELL price:");
               if (-1 != pos)
               {
                  //dPrice = StrToDouble(StringSubstr(comment,pos+14));
                     strPrice = DoubleToStr(OrderOpenPrice(), 5);
                     strPriceComment = StringSubstr(comment,pos+21);
               }
               else continue;
              for (ii = 0; ii < StringLen(strPrice); ii++)
              {
                  char_codePrice = StringGetChar(strPrice, ii);
                  char_codePriceComment = StringGetChar(strPriceComment, ii);
                  if (char_codePrice != char_codePriceComment)
                  {
                     if (char_codePrice < char_codePriceComment)
                     {
                       SellLess++;
                     }
                     if (char_codePrice > char_codePriceComment)
                     {
                       SellMore++;
                     }
                     break;
                  }
              }
                SellCount++;
         }
    }
    
         string s[2];
         int amount = ByCount-ByLess-ByMore;
         int amount2 = SellCount-SellMore-SellLess;
         s[0] = "<tr align=center bgcolor=#E9E9E9><td>"+"OPEN BUY orders:"+
         "</td><td nowrap>" + ByCount + 
         "</td><td>" + amount + 
         "</td><td>" + ByLess +
         "</td><td>" + ByMore +
         "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td></tr>";
         s[1] = "<tr align=center bgcolor=#FFFFFF><td>"+"OPEN SELL orders:"+
         "</td><td nowrap>" + SellCount + 
         "</td><td>" + amount2 + 
         "</td><td>" + SellLess +
         "</td><td>" + SellMore +
         "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td></tr>";


      for (i = 0; i < 2; i++)
      {
       _lwrite(hFile, s[i], StringLen(s[i]));
      }
    
}

void StatisticsCloseMarketOrders()
{
  // retrieving info from trade history
  int i,accTotal=OrdersHistoryTotal();
  
  int ByLess = 0;
  int ByMore = 0;
  int ByCount = 0;
  int SellLess = 0;
  int SellMore = 0;
  int SellCount = 0;
  for(i=0;i<accTotal;i++)
    {
     //---- check selection result
         if(OrderSelect(i,SELECT_BY_POS,MODE_HISTORY)==false)
           {
            Print("Get history error (",GetLastError(),")");
            continue;
           }
 //         Print("currtime", currtime,"OrderOpenTime", OrderOpenTime());
         if ( currtime > OrderOpenTime()) continue;

    int char_codePrice;
    int char_codePriceComment;
      string strPrice;
      string strPriceComment;

         string comment = OrderComment();
         double dPrice = FindPriceFromFile(OrderTicket());
         if (DoubleToStr(dPrice, 0) == "99999999")continue;
         if ( OrderType() == OP_BUY)
         {
                if (OrderClosePrice() <  dPrice)
                {
                  ByLess++;
                }
                if (OrderClosePrice() > dPrice)
                {
                  ByMore++;
                }
             ByCount++;
         }
         if ( OrderType() == OP_SELL)
         {
                if (OrderClosePrice() < dPrice)
                {
                  SellLess++;
                }
                if (OrderClosePrice() > dPrice)
                {
                  SellMore++;
                }
             SellCount++;
         }
    }
    
    
         string s[2];
         int amount = ByCount-ByLess-ByMore;
         int amount2 = SellCount-SellMore-SellLess;
         s[0] = "<tr align=center bgcolor=#E9E9E9><td>"+"CLOSE BUY orders:"+
         "</td><td nowrap>" + ByCount + 
         "</td><td>" + amount + 
         "</td><td>" + ByLess +
         "</td><td>" + ByMore +
         "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td></tr>";
         s[1] = "<tr align=center bgcolor=#FFFFFF><td>"+"CLOSE SELL orders:"+
         "</td><td nowrap>" + SellCount + 
         "</td><td>" + amount2 + 
         "</td><td>" + SellMore +
         "</td><td>" + SellLess +
         "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td></tr>";


      for (i = 0; i < 2; i++)
      {
       _lwrite(hFile, s[i], StringLen(s[i]));
      }
    
}

void StatisticsSLTPOrders()
{
    
         string s[4];
         int amount = BuySL-BuySLbetter-BuySLworse;
         int amount2 = BuyTP-BuyTPbetter-BuyTPworse;
         int amount3 = SellSL-SellSLbetter-SellSLworse;
         int amount4 = SellTP-SellTPworse-SellTPbetter;
         s[0] = "<tr align=center bgcolor=#E9E9E9><td>"+"SL BUY orders:"+
         "</td><td nowrap>" + BuySL + 
         "</td><td>" + amount + 
         "</td><td>" + BuySLworse +
         "</td><td>" + BuySLbetter +
         "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td></tr>";
         s[1] = "<tr align=center bgcolor=#FFFFFF><td>"+"TP BUY orders:"+
         "</td><td nowrap>" + BuyTP + 
         "</td><td>" + amount2 + 
         "</td><td>" + BuyTPworse +
         "</td><td>" + BuyTPbetter +
         "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td></tr>";
         s[2] = "<tr align=center bgcolor=#E9E9E9><td>"+"SL SELL orders:"+
         "</td><td nowrap>" + SellSL + 
         "</td><td>" + amount3 + 
         "</td><td>" + SellSLworse +
         "</td><td>" + SellSLbetter +
         "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td></tr>";
         s[3] = "<tr align=center bgcolor=#FFFFFF><td>"+"TP SELL orders:"+
         "</td><td nowrap>" + SellTP + 
         "</td><td>" + amount4 + 
         "</td><td>" + SellTPworse +
         "</td><td>" + SellTPbetter +
         "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td></tr>";

      for (int i = 0; i < 4; i++)
      {
       _lwrite(hFile, s[i], StringLen(s[i]));
      }
    
}

//+------------------------------------------------------------------+
//| expert initialization function                                   |
//+------------------------------------------------------------------+
  static int StartTime;
  static int StartMoreTradesTime;
  static int StartTestTime;
int init()
  {
  Expiration_Date *= 1000;
  Create_Order_Period *= 1000;
  Duration *= 1000;

  b_start = true;
        DTStartTestTime = TimeCurrent();

        currtime = TimeCurrent();
        StartTime = GetTickCountEx();
        //StartTestTime = GetTickCountEx();
        StartMoreTradesTime = GetTickCountEx();
   return(0);
  }
//+------------------------------------------------------------------+
//| expert deinitialization function                                 |
//+------------------------------------------------------------------+
int deinit()
  {
//----
 if (finish == 0)
 {
           EndFile();
 }
//----
   return(0);
  }
  
  
//+------------------------------------------------------------------+
//| expert start function                                            |
//+------------------------------------------------------------------+
int start()
  {
 //           Alert("initReport");
 if (b_start)
 { 
   CloseAll();
   string root;
   //Alert("string root");
   int res = GetRoot(root);
   if (res == FALSE) return;

   SetHeader(root);

   b_start = false;
 }

//----
   if(GetTickCountEx() - StartTime > Duration)
   {
      if (finish == 0)
      {
         if (Test_Magic)
            CheckMagic();   
      
         if (!Marging_Test_Limit_Orders || !Marging_Test_Stop_Orders)
         {
            CloseAll();
         }
            //Alert("initReport finish");
         EndFile();
         if (SendReport)
         {
            if (!LocalFTPServer)
            {
               if(!SendFTP(file_name + ".html", FTPFolder))// + TimeToStr(TimeCurrent( ), TIME_DATE|TIME_MINUTES)); 
               {
          //        int lasterror=0;
          //        lasterror=GetLastError();
          //        Alert("FTP not sended");
          //        Alert(lasterror);
               }
            }
            else
            {
               ShellExecuteA(0, "open", "cmd", "/c copy \"" + path + "\" " + "\"" + InFolder + "\"", "", SW_SHOW);  
               ShellExecuteA(0, "open", "cmd", "/c copy \"" + path + "\" " + "\"" + InFolder + "report1.html" + "\"", "", SW_SHOW);  
            }
              ShellExecuteA(0, "open", "cmd", "/c copy \"" + path + "\" " + "\"" + path_to_current_folder + "report1.html" + "\"", "", SW_SHOW);  
              TerminateProcess(GetCurrentProcess(), 0);
         }
         else
         {
            ShellExecuteA(0, "open", "cmd", "/c copy \"" + path + "\" " + "\"" + path_to_current_folder + "report1.html" + "\"", "", SW_SHOW);  
            Print("ShellExecuteA ",path);
            ShellExecuteA(0, 0, path, 0, "", SW_SHOW);  
         }
         //Alert("Test Finished");
         finish = 1;
      }
      return(0);
   }
   Print("GetTickCountEx() = ",GetTickCountEx());
   if (GetTickCountEx() - StartTestTime > Create_Order_Period)
   {
 /*     if (GetTickCountEx() - StartMoreTradesTime > MoreTradesTime)
      {
         CloseAll();
         Num_orders+=5;
         StartMoreTradesTime = GetTickCountEx();
      }*/
      //coeff+=3;
      if (Test_CloseBy) CloseBy();
      
      if (Marging_Test_Limit_Orders || Marging_Test_Stop_Orders)
      {
         Test(1);
         Duration = 1;
      }
      else
      {
         Test(Amount_Diff_Order_in_one_Time);
      }
  //    CloseMarketOrder();
      StartTestTime = GetTickCountEx();
   }
  
//----
CheckOrderExecution();
   return(0);
  }

int GetRoot(string& root) 
{
  int buf[255];
  int res = GetModuleFileNameA(0, buf, 4*255);
  if (res == 0) {
    return (FALSE);
  }

  root = "";
  int ch = -1;
  
  for (int i=0; i<res; i++) {
    if (i%4 == 0) ch = buf[i/4] & 0xFF;
    if (i%4 == 1) ch = (buf[i/4] >> 8) & 0xFF;
    if (i%4 == 2) ch = (buf[i/4] >> 16) & 0xFF;
    if (i%4 == 3) ch = (buf[i/4] >> 24) & 0xFF;

    root = root + CharToStr(ch);
  }
  
  root = str_replace(root, "\\terminal.exe", "");

  return (TRUE);
}

string str_replace(string subject, string search, string replace) 
{
  string left_part = "";
  string right_part = "";

  for (;;) {
    int pos = StringFind(subject, search);
    if (pos == -1) break;
    
    left_part = "";
    right_part = "";
    
    if (pos > 0) left_part = StringSubstr(subject, 0, pos);
    if (pos+1 < StringLen(subject)) right_part = StringSubstr(subject, pos+StringLen(search));

    subject = left_part + replace + right_part;
  }

  return (subject);
}


void SetHeader( string root)
{
   path = str_replace(report, "[ROOT]", root);
   file_name = DoubleToStr(AccountNumber(), 0) + "_" + time_format(LocalTime(), "d_mon_yyyy_hh_mm");
   path = str_replace(path, "[ACCOUNT_NUM]", file_name);
   path_close_price = str_replace(report_close_price, "[ROOT]", root);
   path_close_price = str_replace(path_close_price, "[ACCOUNT_NUM]", DoubleToStr(AccountNumber(), 0) + "_" + time_format(LocalTime(), "d_mon_yyyy_hh_mm"));
   path_not_exec_orders = str_replace(report_not_exec_orders, "[ROOT]", root);
   path_not_exec_orders = str_replace(path_not_exec_orders, "[ACCOUNT_NUM]", DoubleToStr(AccountNumber(), 0) + "_" + time_format(LocalTime(), "d_mon_yyyy_hh_mm"));
   path_to_current_folder = str_replace(current_folder, "[ROOT]", root);
   Print("Final path", path);

  hFile = _lcreat(path, 0);
  Print("create file ",path);
  if (hFile == HFILE_ERROR) {
    Alert("WriteRes: _lcreat(" + path + ", 0) failed!");
    return (FALSE);
  }
  
  hFile_close_price = _lcreat(path_close_price, 0);
  Print("create file ",path_close_price);
  if (hFile_close_price == HFILE_ERROR) {
    Alert("WriteRes: _lcreat(" + path_close_price + ", 0) failed!");
    return (FALSE);
  }
  
  hFile_not_exec_orders = _lcreat(path_not_exec_orders, 0);
  Print("create file ",path_not_exec_orders);
  if (hFile_not_exec_orders == HFILE_ERROR) {
    Alert("WriteRes: _lcreat(" + hFile_not_exec_orders + ", 0) failed!");
    return (FALSE);
  }

   string str[255];
   str[0] = "<!DOCTYPE HTML PUBLIC \"-//W3C//TD HTML 4.01//EN\" \"http://www.w3.org/TR/html4/strict.dtd\">";
   str[1] = "<html>";
   str[2] = "  <head>";
   str[3] = "    <title>Statement: <!--ACCOUNT--> - <!--NAME--></title>";
   str[4] = "    <meta name=\"generator\" content=\"MetaQuotes Software Corp.\">";
   str[5] = "    <link rel=\"help\" href=\"http://www.metaquotes.net\">";
   str[6] = "    <style type=\"text/css\" media=\"screen\">";
   str[7] = "    <!--";
   str[8] = "    td { font: 8pt Tahoma,Arial; }";
   str[10] = "    //-->";
   str[11] = "    </style>";
   str[12] = "    <style type=\"text/css\" media=\"print\">";
   str[13] = "    <!--";
   str[14] = "    td { font: 7pt Tahoma,Arial; }";
   str[15] = "    //-->";
   str[16] = "    </style>";
   str[17] = "    <style type=\"text/css\">";
   str[18] = "    <!--";
   str[19] = "    .msdate { mso-number-format:\"General Date\"; }";
   str[20] = "    .mspt   { mso-number-format:\#\,\#\#0\.00;  }";
   str[21] = "    //-->";
   str[22] = "    </style>";
   str[23] = "  </head>";
   str[24] = "<body topmargin=1 marginheight=1>";
   str[25] = "<div align=center>";
   str[26] = "<div style=\"font: 20pt Times New Roman\"><b><!--COMPANY--></b></div><br>";
   str[27] = "<!--NOTRANS=No transactions-->";
   str[28] = "<!--NOCOMMENTS-->";
   str[29] = "<table cellspacing=1 cellpadding=3 border=0>";
   str[30] = "<tr align=left>";
   str[31] = "    <td colspan=2><b>Account: "+DoubleToStr(AccountNumber(), 0)+/*<!--ACCOUNT-->*/"</b></td>";
   str[32] = "    <td colspan=6><b>Name: "+CompanyName()+/*<!--NAME-->*/"</b></td>";
   str[33] = "    <td colspan=2><b>Currency: "+AccountCurrency()+/*<!--CURRENCY-->*/"</b></td>";
   str[34] = "    <td colspan=4 align=right><b>"+time_format(LocalTime(), "d mon yyyy hh:mm")+/*<!--FULLTIME-->*/"</b></td></tr>";
   str[35] = "<tr align=left><td colspan=14><b>Closed Transactions:</b></td></tr>";
   str[36] = "<tr align=center bgcolor=\"#C0C0C0\">";
   str[37] = "   <td>Ticket</td><td nowrap>Open Time</td><td>Type</td><td>Symbol</td><td>Size</td>";
   str[38] = "   <td>Price</td><td>Comment & price set for open</td><td>S / L</td><td>T / P</td><td nowrap>Close Time</td>";
   str[39] = "   <td>Price</td><td>Comment & price set for close</td><td>Commission</td><td>Swap</td><td>Profit</td></tr>";
   for (int i = 0; i < 40; i++)
  {
   _lwrite(hFile, str[i], StringLen(str[i]));
  }
 
}

void InsertStr()
{

  int i,accTotal=OrdersHistoryTotal();
  //Alert("accTotal",accTotal);
   string scolor;
   string scolortpsl;
   string s;
   string comment;
   string comment2;
  for(i=0;i<accTotal;i++)
    {
      scolortpsl = "";
      string sltp;
      sltp = "";
     //---- check selection result
     if(OrderSelect(i,SELECT_BY_POS,MODE_HISTORY)==false)
       {
        Print("Error (",GetLastError(),")");
        break;
       }
       if (DTStartTestTime > OrderOpenTime())continue;
        if (i%2 == 0)
         scolor = "#FFFFFF";
        else scolor = "#E0E0E0";
     // working ...
    
          
         s = "<tr align=center bgcolor=" + scolor + "><td>"+OrderTicket( ) +
         "</td><td nowrap>" + TimeToStr(OrderOpenTime()) + 
         "</td><td>" + OrderTypeToStr(OrderType()) + 
         "</td><td>" + OrderSymbol() +
         "</td><td>" + DoubleToStr(OrderLots(), 2) +
         "</td><td>" + DoubleToStr(OrderOpenPrice(), digits) +
         "</td><td>" + Comment1() +
         "</td><td>" + DoubleToStr(OrderStopLoss(), digits) +
         "</td><td>" + DoubleToStr(OrderTakeProfit(), digits) +
         "</td><td>" + TimeToStr(OrderCloseTime()) +
         "</td><td>" + DoubleToStr(OrderClosePrice(), digits) +
         "</td><td>" + Comment2(true) +
         "</td><td>" + number_format(OrderCommission(), 2, ".", " ") +
         "</td><td>" + number_format(OrderSwap(), 2, ".", " ") +
         "</td><td>" + OrderProfit( ) +
         "</td></tr>";
  //       Alert("s",s);
         _lwrite(hFile, s, StringLen(s));
    }


}

void EndFile()
{
   //Alert("InsertStr");
   InsertStr();

   NotExecutedOrders();   
   string scolor; 
   scolor = "#FFFFFF";     
   string s;
         s = "<tr align=center bgcolor=" + scolor + "><td>"+""+
         "</td><td nowrap>" + "Amount orders" + 
         "</td><td>" + "Order Price" + 
         "</td><td>" + "With Worse Price" +
         "</td><td>" + "With Better Price" +
         "</td><td>" + "" +
         "</td><td>" + "" +
         "</td><td>" + "" +
         "</td><td>" + "" +
         "</td><td>" + "" +
         "</td><td>" + "" +
         "</td><td>" + "" +
         "</td><td>" + "" +
         "</td><td>" + "" +
         "</td><td>" + "" +
         "</td></tr>";

   _lwrite(hFile, s, StringLen(s));

      if (SLTP_Test_Market_Orders)
      {
         StatisticsOpenMarketOrders();
         StatisticsCloseMarketOrders();
         StatisticsSLTPOrders();
      }
      if (Test_Limit_Orders)
      {
         StatisticsLimitOrders();
      }
      if (Test_Stop_Orders)
      {
         StatisticsStopOrders();
      }
      if (Marging_Test_Limit_Orders)
      {
      }
      if (Marging_Test_Stop_Orders)
      {
      }
      if (Test_For_All_Order_Types)
      {
      }
      if (Create_And_Modify_All_Order)
      {
      }
      //StatisticsTPSmallVolume();

   
   
   string str[2];
   str[0] = "</table>";
   _lwrite(hFile, str[0], StringLen(str[0]));
   
   str[1] = "</div></body></html>";
   _lwrite(hFile, str[1], StringLen(str[1]));

  _lclose(hFile);
  _lclose(hFile_close_price);
  _lclose(hFile_not_exec_orders);

}

void SetCreateOrderErrorToReport(datetime CreateTime, string strType, string symbol, double price, double sl, double tp, int err)
{
         string s = "<tr align=center bgcolor=\"#FFFFFF\" ><td>"+""+
         "</td><td nowrap>" + TimeToStr(CreateTime) + 
         "</td><td>" + strType + 
         "</td><td>" + OrderSymbol() +
         "</td><td>" + DoubleToStr(OrderLots(), 2) +
         "</td><td>" + DoubleToStr(price, digits) +
         "</td><td>" + "" +
         "</td><td>" + DoubleToStr(sl, digits) +
         "</td><td>" + DoubleToStr(tp, digits) +
         "</td><td>" + "" +
         "</td><td>" + "" +
         "</td><td>" + "" +
         "</td><td>" + "" +
         "</td><td>" + "Order was not created" +
         "</td><td>" + "Error #"+ err +
         "</td></tr>";
  //       Alert("s",s);
         _lwrite(hFile, s, StringLen(s));

}

void SetCloseOrderErrorToReport(int order, datetime CreateTime, int Type, string symbol, double OpenPrice, double sl, double tp, double ClosePrice, int err)
{
string strType;

   if (Type == OP_BUY)
   {
    strType = "OP_BUY";
   } 
   else if (Type == OP_SELL)
   {
     strType = "OP_SELL";
   }
   else if (Type == OP_BUYLIMIT)
   {
    strType = "OP_BUYLIMIT";
   }
   else if (Type == OP_SELLLIMIT)
   {
    strType = "OP_SELLLIMIT";
   }
   else if (Type == OP_BUYSTOP)
   {
    strType = "OP_BUYSTOP";
   }
   else if (Type == OP_SELLSTOP)
   {
    strType = "OP_SELLSTOP";
   }

         string s = "<tr align=center bgcolor=\"#FFFFFF\" ><td>" + order +
         "</td><td nowrap>" + TimeToStr(CreateTime) + 
         "</td><td>" + strType + 
         "</td><td>" + OrderSymbol() +
         "</td><td>" + DoubleToStr(OrderLots(), 2) +
         "</td><td>" + DoubleToStr(OpenPrice, digits) +
         "</td><td>" + "" +
         "</td><td>" + DoubleToStr(sl, digits) +
         "</td><td>" + DoubleToStr(tp, digits) +
         "</td><td>" + "" +
         "</td><td>" + DoubleToStr(ClosePrice, digits) +
         "</td><td>" + "" +
         "</td><td>" + "" +
         "</td><td>" + "Order was not closed" +
         "</td><td>" + "Error #" + err +
         "</td></tr>";
  //       Alert("s",s);
         _lwrite(hFile, s, StringLen(s));

}

string CloseByOrderErrorToReport1(int order, datetime CreateTime, int Type, string symbol, double OpenPrice, double sl, double tp, double ClosePrice)
{
string strType;

   if (Type == OP_BUY)
   {
    strType = "OP_BUY";
   } 
   else if (Type == OP_SELL)
   {
     strType = "OP_SELL";
   }
   else if (Type == OP_BUYLIMIT)
   {
    strType = "OP_BUYLIMIT";
   }
   else if (Type == OP_SELLLIMIT)
   {
    strType = "OP_SELLLIMIT";
   }
   else if (Type == OP_BUYSTOP)
   {
    strType = "OP_BUYSTOP";
   }
   else if (Type == OP_SELLSTOP)
   {
    strType = "OP_SELLSTOP";
   }

         string CloseBy1 = "<tr align=center bgcolor=\"#FFFFFF\" ><td>" + order +
         "</td><td nowrap>" + TimeToStr(CreateTime) + 
         "</td><td>" + strType + 
         "</td><td>" + OrderSymbol() +
         "</td><td>" + DoubleToStr(OrderLots(), 2) +
         "</td><td>" + DoubleToStr(OpenPrice, digits) +
         "</td><td>" + "" +
         "</td><td>" + DoubleToStr(sl, digits) +
         "</td><td>" + DoubleToStr(tp, digits) +
         "</td><td>" + "" +
         "</td><td>" + DoubleToStr(ClosePrice, digits) +
         "</td><td>" + "" +
         "</td><td>" + "" +
         "</td><td>" + "Order was not closed" +
         "</td><td>" + "Close by" +
         "</td></tr>";
  //       Alert("s",s);
        // _lwrite(hFile, s, StringLen(s));
         return (CloseBy1);
}

void CloseByOrderErrorToReport2(int order, datetime CreateTime, int Type, string symbol, double OpenPrice, double sl, double tp, double ClosePrice, int err, string s1)
{
string strType;

   if (Type == OP_BUY)
   {
    strType = "OP_BUY";
   } 
   else if (Type == OP_SELL)
   {
     strType = "OP_SELL";
   }
   else if (Type == OP_BUYLIMIT)
   {
    strType = "OP_BUYLIMIT";
   }
   else if (Type == OP_SELLLIMIT)
   {
    strType = "OP_SELLLIMIT";
   }
   else if (Type == OP_BUYSTOP)
   {
    strType = "OP_BUYSTOP";
   }
   else if (Type == OP_SELLSTOP)
   {
    strType = "OP_SELLSTOP";
   }

         string s = "<tr align=center bgcolor=\"#FFFFFF\" ><td>" + order +
         "</td><td nowrap>" + TimeToStr(CreateTime) + 
         "</td><td>" + strType + 
         "</td><td>" + OrderSymbol() +
         "</td><td>" + DoubleToStr(OrderLots(), 2) +
         "</td><td>" + DoubleToStr(OpenPrice, digits) +
         "</td><td>" + "" +
         "</td><td>" + DoubleToStr(sl, digits) +
         "</td><td>" + DoubleToStr(tp, digits) +
         "</td><td>" + "" +
         "</td><td>" + DoubleToStr(ClosePrice, digits) +
         "</td><td>" + "" +
         "</td><td>" + "" +
         "</td><td>" + "Order was not closed" +
         "</td><td>" + "Error #" + err +
         "</td></tr>";
  //       Alert("s",s);
         _lwrite(hFile, s1, StringLen(s1));
         _lwrite(hFile, s, StringLen(s));

}

string time_format(datetime tm, string format) 
{
  string month[12] = {"Jan", "Feb", "March", "April", "May", "June", "July", "Aug",
    "Sept", "Oct", "Nov", "Dec"};
  
  string res = format;  
  res = str_replace(res, "d", TimeDay(tm));
  res = str_replace(res, "mon", month[TimeMonth(tm)-1]);
  res = str_replace(res, "yyyy", TimeYear(tm));
  
  int hh = TimeHour(tm);
  int mm = TimeMinute(tm);
  
  res = str_replace(res, "hh", str_if(hh < 10, "0" + hh, hh));
  res = str_replace(res, "mm", str_if(mm < 10, "0" + mm, mm));

  return (res);
}

string str_if(bool cond, string val1, string val2) 
{
  if (cond) return (val1);
  return (val2);
}

string number_format(double number, int decimals, string dec_point, string thousands_sep)
{
  string str = DoubleToStr(number, decimals);
  string res = "";

  int len = StringLen(str);
  int pos = StringFind(str, dec_point);
  if (pos > 0) {
    len = pos;
    res = StringSubstr(str, pos);
  }
  
  for (int i = len-1; i >= 0; i--) {
    res = StringSubstr(str, i, 1) + res;
    
    if (i > 0 && (len-i)%3 == 0) {
      if (StringSubstr(str, i-1, 1) != "-") res = thousands_sep + res;
    }
  }

  return (res);
}

string OrderTypeToStr(int type) 
{
  if (type == OP_BUY) return ("buy");
  if (type == OP_SELL) return ("sell");
  if (type == OP_BUYLIMIT) return ("buy limit");
  if (type == OP_SELLLIMIT) return ("sell limit");
  if (type == OP_BUYSTOP) return ("buy stop");
  if (type == OP_SELLSTOP) return ("sell stop");
  if (type == OP_BALANCE) return ("balance");

  return ("?");
}

string Comment1()
{
bool b_sltp = false;
double dPrice;

          string fontcolor;


         string comment = OrderComment();
         int pos = StringFind(comment,"[sl]");
         if (-1 != pos)
         {
            b_sltp = true;
         }
         pos = StringFind(comment,"[tp]");
         if (-1 != pos)
         {
            b_sltp = true;
         }
         if (OrderType() == OP_BUY || OrderType() == OP_SELL)
         {
            pos = StringFind(comment,"price:");
            if (-1 != pos)
            {
               dPrice = StrToDouble(StringSubstr(comment,pos+6));
               dPrice = OrderOpenPrice() - dPrice;
            
                if (OrderType() == OP_BUY && dPrice > 0)
               {
                  fontcolor = "<font color=\"#FF0000\">";//red
               }
               else 
                  if (OrderType() == OP_BUY && dPrice < 0)
                  {
                     fontcolor = "<font color=\"#006600\">";//green
                  }
               if (OrderType() == OP_SELL && dPrice > 0)
               {
                  fontcolor = "<font color=\"#006600\">";
               }
               else 
                  if (OrderType() == OP_SELL && dPrice < 0)
                  {
                     fontcolor = "<font color=\"#FF0000\">";
                  }
              if (DoubleToStr(dPrice, digits) == "0.00000")
                  fontcolor = "<font color=\"#000000\">";
            
              if (b_sltp)
               {
                  comment = StringSubstr(comment,4) + " (" + DoubleToStr(dPrice, digits) + ")";
               }
               else
               {
                  comment = comment + " (" + DoubleToStr(dPrice, digits) + ")";
               }
            }
         }
         else comment = "cancelled"; 
         return (fontcolor + comment);
}

string Comment2(bool bCountingOrders)
{
string scolortpsl;
         string comment = OrderComment();
         string comment2;
         if (OrderType() == OP_BUY || OrderType() == OP_SELL)
         {
            int pos = StringFind(comment,"[sl]");
            if (-1 != pos)
            {
            if (bCountingOrders && OrderType() == OP_BUY)
                  BuySL++;
            if (bCountingOrders && OrderType() == OP_SELL)
                  SellSL++;
               if ( DoubleToStr(OrderClosePrice(), digits) != DoubleToStr(OrderStopLoss(), digits))   
               {
               if (bCountingOrders && OrderType() == OP_BUY && OrderClosePrice() - OrderStopLoss() > 0)
                  {
                     scolortpsl = "<font color=\"#006600\">";
                     BuySLbetter++;
                  }
                  else 
                  if (bCountingOrders && OrderType() == OP_BUY && OrderClosePrice() - OrderStopLoss() < 0)
                     {
                        scolortpsl = "<font color=\"#FF0000\">";
                        BuySLworse++;
                     }
               if (bCountingOrders && OrderType() == OP_SELL && OrderClosePrice() - OrderStopLoss() > 0)
                  {
                     scolortpsl = "<font color=\"#FF0000\">";
                     SellSLworse++;
                  }
                  else 
                  if (bCountingOrders && OrderType() == OP_SELL && OrderClosePrice() - OrderStopLoss() < 0)
                     {
                        scolortpsl = "<font color=\"#006600\">";
                        SellSLbetter++;
                     }
                 }
               comment2 = scolortpsl + "[sl]" + " (" + DoubleToStr(OrderClosePrice() - OrderStopLoss(), digits) + ")";
            }else
            {
               pos = StringFind(comment,"[tp]");
               if (-1 != pos)
               {
             if (bCountingOrders && OrderType() == OP_BUY)
                  BuyTP++;
             if (bCountingOrders && OrderType() == OP_SELL)
                  SellTP++;
                if ( DoubleToStr(OrderClosePrice(), digits) != DoubleToStr(OrderTakeProfit(), digits))   
                {
               if (bCountingOrders && OrderType() == OP_BUY && OrderClosePrice() - OrderTakeProfit() > 0)
                    {
                       scolortpsl = "<font color=\"#006600\">";
                       BuyTPbetter++;
                    }
                    else 
                    if (bCountingOrders && OrderType() == OP_BUY && OrderClosePrice() - OrderTakeProfit() < 0)
                       {
                          scolortpsl = "<font color=\"#FF0000\">";
                          BuyTPworse++;
                       }
               if (bCountingOrders && OrderType() == OP_SELL && OrderClosePrice() - OrderTakeProfit() > 0)
                    {
                       scolortpsl = "<font color=\"#FF0000\">";
                       SellTPworse++;
                    }
                    else 
                    if (bCountingOrders && OrderType() == OP_SELL && OrderClosePrice() - OrderTakeProfit() < 0)
                       {
                          scolortpsl = "<font color=\"#006600\">";
                          SellTPbetter++;
                       }
                   }
                  comment2 = scolortpsl + "[tp]" + " (" + DoubleToStr(OrderClosePrice() - OrderTakeProfit(), digits) + ")";
               }
               else
               { // process market orders
                  double MarketOrderClosePrice = FindPriceFromFile(OrderTicket());
                  if (DoubleToStr(MarketOrderClosePrice, 0) != "99999999")
                  {
                     //Alert("MarketOrderClosePrice = ", MarketOrderClosePrice);
                     if (OrderType() == OP_BUY && OrderClosePrice() - MarketOrderClosePrice > 0)
                     {
                        scolortpsl = "<font color=\"#006600\">";
                     }
                     else 
                        if (OrderType() == OP_BUY && OrderClosePrice() - MarketOrderClosePrice < 0)
                        {
                           scolortpsl = "<font color=\"#FF0000\">";
                        }
                     if (OrderType() == OP_SELL && OrderClosePrice() - MarketOrderClosePrice > 0)
                     {
                        scolortpsl = "<font color=\"#FF0000\">";
                     }
                     else 
                        if (OrderType() == OP_SELL && OrderClosePrice() - MarketOrderClosePrice < 0)
                        {
                           scolortpsl = "<font color=\"#006600\">";
                        }

                     comment2 = scolortpsl + "Close price:" + DoubleToStr(MarketOrderClosePrice, digits) + " (" + DoubleToStr(OrderClosePrice() - MarketOrderClosePrice, digits) + ")";
               
                  } 
                     else   comment2 = scolortpsl + "Close price:" + 0 + " (" + DoubleToStr(OrderClosePrice(), digits) + ")";
               }
            }
         }
   return (comment2);
}

double FindPriceFromFile(int ticket)
{
int size = 20;
     string str_ticket = "";  
     if (HFILE_ERROR != _llseek(hFile_close_price, 0, FILE_BEGIN))
     {
         int buf[];
         ArrayResize(buf, size);

         int res = _lread(hFile_close_price, buf, size);
         while (res > 0)
         {
            Print("_lread res = ",res);
            if (res == HFILE_ERROR) 
            {
               //Alert("GetTemplate: _lread failed!");
               return (FALSE);
            }
            Print("size",size);
            str_ticket = "";  
            int ch = -1;
    
            for (int i=0; i<res; i++) 
            {
               //Print("buf[i]",buf[i]);
               if (i%4 == 0) {ch = buf[i/4] & 0xFF;}
               if (i%4 == 1) {ch = (buf[i/4] >> 8) & 0xFF;}
               if (i%4 == 2) {ch = (buf[i/4] >> 16) & 0xFF;}
               if (i%4 == 3) {ch = (buf[i/4] >> 24) & 0xFF;}
               str_ticket = str_ticket + CharToStr(ch);
            }
            if (ticket ==  StrToInteger(str_ticket))
            {
                  //Alert("Ticket was founded", str_ticket);
                  res = _lread(hFile_close_price, buf, size);
                  string str_price="";
                  for (i=0; i<res; i++) 
                  {
                     //Print("buf[i]",buf[i]);
                     if (i%4 == 0) {ch = buf[i/4] & 0xFF;}
                     if (i%4 == 1) {ch = (buf[i/4] >> 8) & 0xFF;}
                     if (i%4 == 2) {ch = (buf[i/4] >> 16) & 0xFF;}
                     if (i%4 == 3) {ch = (buf[i/4] >> 24) & 0xFF;}
                     str_price = str_price + CharToStr(ch);
                  }

                  return (StrToDouble(str_price));
                 // break; 
            }
            res = _lread(hFile_close_price, buf, size);    
         }     
         //Alert("Ticket from file#", str_ticket);
     }
     return (99999999);
}

void SetExecutionOrderErrorToReport(int order, double ClosePrice, int time)
{
/*string strType;

   if (Type == OP_BUY)
   {
    strType = "OP_BUY";
   } 
   else if (Type == OP_SELL)
   {
     strType = "OP_SELL";
   }
   else if (Type == OP_BUYLIMIT)
   {
    strType = "OP_BUYLIMIT";
   }
   else if (Type == OP_SELLLIMIT)
   {
    strType = "OP_SELLLIMIT";
   }
   else if (Type == OP_BUYSTOP)
   {
    strType = "OP_BUYSTOP";
   }
   else if (Type == OP_SELLSTOP)
   {
    strType = "OP_SELLSTOP";
   }

         string s = "<tr align=center bgcolor=\"#FFFFFF\" ><td>" + order +
         "</td><td nowrap>" + TimeToStr(CreateTime) + 
         "</td><td>" + strType + 
         "</td><td>" + OrderSymbol() +
         "</td><td>" + DoubleToStr(OrderLots(), 2) +
         "</td><td>" + DoubleToStr(OpenPrice, digits) +
         "</td><td>" + "" +
         "</td><td>" + DoubleToStr(sl, digits) +
         "</td><td>" + DoubleToStr(tp, digits) +
         "</td><td>" + "" +
         "</td><td>" + DoubleToStr(ClosePrice, digits) +
         "</td><td>" + "" +
         "</td><td>" + "" +
         "</td><td>" + "Order was not executed" +
         "</td><td>" + "Error #" + err +
         "</td></tr>";
  //       Alert("s",s);*/
  string ord = order;
            for (int j = StringLen(ord); j < 20; j++)
            {
               ord = ord + " ";
            }
         
            ord = ord + DoubleToStr(ClosePrice, digits);
            
            for (j = StringLen(ord); j < 40; j++)
            {
               ord = ord + " ";
            }
   
            ord = ord + time;
            
            for (j = StringLen(ord); j < 60; j++)
            {
               ord = ord + " ";
            }
   
         _lwrite(hFile_not_exec_orders, ord, StringLen(ord));

}

void CheckOrderExecution()
{
   for (int i = OrdersTotal()-1; i >= 0; i--)
   {
      if (OrderSelect( i, SELECT_BY_POS ))
      {
         RefreshRates();
         double ask = MarketInfo(Symbol(),MODE_ASK);
         double bid = MarketInfo(Symbol(),MODE_BID);
         if (OrderType() == OP_BUY && (OrderStopLoss() != 0) && (bid < OrderStopLoss( )))
         {
               SetExecutionOrderErrorToReport(OrderTicket(), bid, GetTickCountEx());
         }
         else
         if (OrderType() == OP_BUY && (OrderTakeProfit() != 0) && (ask > OrderTakeProfit( )))
         {
               SetExecutionOrderErrorToReport(OrderTicket(), ask, GetTickCountEx());
         }
         else
         if (OrderType() == OP_SELL && (OrderStopLoss() != 0) && (ask > OrderStopLoss( )))
         {
               SetExecutionOrderErrorToReport(OrderTicket(), ask, GetTickCountEx());
         }
         else
         if (OrderType() == OP_SELL && (OrderTakeProfit() != 0) && (bid < OrderTakeProfit( )))
         {
               SetExecutionOrderErrorToReport(OrderTicket(), bid, GetTickCountEx());
         }
         else
         if (OrderType() == OP_BUYLIMIT && (ask < OrderOpenPrice( )))
         {
               SetExecutionOrderErrorToReport(OrderTicket(), ask, GetTickCountEx());
         }
         else
         if (OrderType() == OP_SELLLIMIT && (bid > OrderOpenPrice( )))
         {
               SetExecutionOrderErrorToReport(OrderTicket(), bid, GetTickCountEx());
         }
         else
         if (OrderType() == OP_BUYSTOP && (ask > OrderOpenPrice( )))
         {
               SetExecutionOrderErrorToReport(OrderTicket(), ask, GetTickCountEx());
         }
         else
         if (OrderType() == OP_SELLSTOP && (bid < OrderOpenPrice( )))
         {
               SetExecutionOrderErrorToReport(OrderTicket(), bid, GetTickCountEx());
         }
      }
   }

}

void NotExecutedOrders()
{

  int i,accTotal=OrdersHistoryTotal();
  //Alert("accTotal",accTotal);
   string scolor;
   string scolortpsl;
   string s;
   string comment;
   string comment2;
   
  for(i=0;i<accTotal;i++)
    {
      scolortpsl = "";
      string sltp;
      sltp = "";
     //---- check selection result
     if(OrderSelect(i,SELECT_BY_POS,MODE_HISTORY)==false)
       {
        Print("Error (",GetLastError(),")");
        break;
       }
       if (DTStartTestTime > OrderOpenTime())continue;
        if (i%2 == 0)
         scolor = "#FFFFFF";
        else scolor = "#E0E0E0";
     // working ...
    
      int summ = NumOrdersFromFile(OrderTicket());
      if (summ != 0)
      {
      string result = "was not executed " + summ + " times. " + DoubleToStr((executiontime_end - executiontime_start)/1000.,3) + " sec.";
      string execres = "first exec price = " + firstexecutionprice;
         s = "<tr align=center bgcolor=" + scolor + "><td>"+OrderTicket( ) +
         "</td><td nowrap>" + TimeToStr(OrderOpenTime()) + 
         "</td><td>" + OrderTypeToStr(OrderType()) + 
         "</td><td>" + OrderSymbol() +
         "</td><td>" + DoubleToStr(OrderLots(), 2) +
         "</td><td>" + DoubleToStr(OrderOpenPrice(), digits) +
         "</td><td>" + Comment1() +
         "</td><td>" + DoubleToStr(OrderStopLoss(), digits) +
         "</td><td>" + DoubleToStr(OrderTakeProfit(), digits) +
         "</td><td>" + TimeToStr(OrderCloseTime()) +
         "</td><td>" + DoubleToStr(OrderClosePrice(), digits) +
         "</td><td>" + Comment2(false) +
         "</td><td>" + number_format(OrderCommission(), 2, ".", " ") +
         "</td><td>" + execres +
         "</td><td>" + result +
         "</td></tr>";
  //       Alert("s",s);
         _lwrite(hFile, s, StringLen(s));
      }
    }


}

int NumOrdersFromFile(int ticket)
{
int summ = 0;
int size = 20;
bool fp = false;
executiontime_start = 0;
executiontime_end = 0;
     string str_ticket = "";  
     string str_price = "";  
     string str_time = "";  
     if (HFILE_ERROR != _llseek(hFile_not_exec_orders, 0, FILE_BEGIN))
     {
         int buf[];
         ArrayResize(buf, size);

         int res = _lread(hFile_not_exec_orders, buf, size);
         while (res > 0)
         {
            Print("_lread res = ",res);
            if (res == HFILE_ERROR) 
            {
               //Alert("GetTemplate: _lread failed!");
               return (FALSE);
            }
 //           Print("size",size);
            str_ticket = "";  
            int ch = -1;
    
            for (int i=0; i<res; i++) 
            {
   //            Print("buf[i]",buf[i]);
               if (i%4 == 0) {ch = buf[i/4] & 0xFF;}
               if (i%4 == 1) {ch = (buf[i/4] >> 8) & 0xFF;}
               if (i%4 == 2) {ch = (buf[i/4] >> 16) & 0xFF;}
               if (i%4 == 3) {ch = (buf[i/4] >> 24) & 0xFF;}
               str_ticket = str_ticket + CharToStr(ch);
            }
   //      Alert("Ticket from file#", str_ticket);
            if (ticket == StrToInteger(str_ticket))
            {
                  summ++;
            }
            
            res = _lread(hFile_not_exec_orders, buf, size);  
            
            if (!fp && ticket == StrToInteger(str_ticket))
            {
               str_price = "";  
               ch = -1;
               for (i=0; i<res; i++) 
               {
                  if (i%4 == 0) {ch = buf[i/4] & 0xFF;}
                  if (i%4 == 1) {ch = (buf[i/4] >> 8) & 0xFF;}
                  if (i%4 == 2) {ch = (buf[i/4] >> 16) & 0xFF;}
                  if (i%4 == 3) {ch = (buf[i/4] >> 24) & 0xFF;}
                  str_price = str_price + CharToStr(ch);
               }
               firstexecutionprice = StrToDouble(str_price);
               fp = true;
            }
              
            res = _lread(hFile_not_exec_orders, buf, size);    
            
            if (ticket == StrToInteger(str_ticket))
            {
            ch = -1;
               str_time = "";
            for (i=0; i<res; i++) 
            {
               if (i%4 == 0) {ch = buf[i/4] & 0xFF;}
               if (i%4 == 1) {ch = (buf[i/4] >> 8) & 0xFF;}
               if (i%4 == 2) {ch = (buf[i/4] >> 16) & 0xFF;}
               if (i%4 == 3) {ch = (buf[i/4] >> 24) & 0xFF;}
                  str_time = str_time + CharToStr(ch);
            }
            if (executiontime_start == 0)
            {
                  executiontime_start = StrToInteger(str_time);
                  executiontime_end = StrToInteger(str_time);
            }
            else
                  executiontime_end = StrToInteger(str_time);
            }
              
            res = _lread(hFile_not_exec_orders, buf, size);    
         }     
     }
     return (summ);
}

int GetTickCountEx()
{
static int gCurrentTimeInMilliseconds = 0;
static int gLastUpdatedTime = 0;
static int cGetTickCountMaximum = 2147483647;

	int currentTime = GetTickCount();
	if (gLastUpdatedTime < 0)
	  gLastUpdatedTime = gLastUpdatedTime - cGetTickCountMaximum + 1;
	int lastUpdateTime = gLastUpdatedTime;
	Print("currentTime = ", currentTime);
	Print("lastUpdateTime = ", lastUpdateTime);
	if (currentTime >= lastUpdateTime)
	{
		gCurrentTimeInMilliseconds += (currentTime - lastUpdateTime);
	  Print("gCurrentTimeInMilliseconds1 = ", gCurrentTimeInMilliseconds);
	}
	else
	{
		gCurrentTimeInMilliseconds += currentTime + (cGetTickCountMaximum - lastUpdateTime) + 1;
	  Print("gCurrentTimeInMilliseconds2 = ", gCurrentTimeInMilliseconds);
	}
	gLastUpdatedTime = currentTime;
	int result = gCurrentTimeInMilliseconds;
	return (result);
}



/*void StatisticsTPSmallVolume()
{
  // retrieving info from trade history
  int i,accTotal=OrdersHistoryTotal();
  
  int ByLess = 0;
  int ByMore = 0;
  int ByCount = 0;
  int SellLess = 0;
  int SellMore = 0;
  int SellCount = 0;
  for(i=0;i<accTotal;i++)
    {
     //---- check selection result
         if(OrderSelect(i,SELECT_BY_POS,MODE_HISTORY)==false)
           {
            Print("Get history error (",GetLastError(),")");
            continue;
           }
 //         Print("currtime", currtime,"OrderOpenTime", OrderOpenTime());
         if ( currtime > OrderOpenTime()) continue;
         if ( OrderType() == OP_BUY && OrderLots() < 1 && OrderTakeProfit( ) > 0)
         {
                if (OrderClosePrice() <  OrderTakeProfit( ))
                {
                  ByLess++;
                }
                if (OrderClosePrice() > OrderTakeProfit( ))
                {
                  ByMore++;
                }
             ByCount++;
         }
         if ( OrderType() == OP_SELL && OrderLots() < 1 && OrderTakeProfit( ) > 0)
         {
                if (OrderClosePrice() < OrderTakeProfit( ))
                {
                  SellLess++;
                }
                if (OrderClosePrice() > OrderTakeProfit( ))
                {
                  SellMore++;
                }
             SellCount++;
         }
    }
    
   /* string str[24];
    str[0] = "<tr align=left bgcolor=#E9E9E9>";
    str[1] = "  <td colspan=\"15\">BUY orders with volume < 1:</td>";
    str[2] = "</tr>";
    str[3] = "<tr align=left bgcolor=#FFFFFF>";
    str[4] = "  <td colspan=\"15\">amount BUY orders  " + ByCount + "</td>";
    str[5] = "</tr>";
    str[6] = "<tr align=left bgcolor=#E9E9E9>";
    str[7] = "  <td colspan=\"15\">amount BUY orders with open price > place " + ByMore + "</td>";
    str[8] = "</tr>";
    str[9] = "<tr align=left bgcolor=#E9E9E9>";
    str[10] = "  <td colspan=\"15\">amount BUY orders with open price < place " + ByLess + "</td>";
    str[11] = "</tr>";

    str[12] = "<tr align=left bgcolor=#E9E9E9>";
    str[13] = "  <td colspan=\"15\">SELL orders with volume < 1:</td>";
    str[14] = "</tr>";
    str[15] = "<tr align=left bgcolor=#FFFFFF>";
    str[16] = "  <td colspan=\"15\">amount SELL orders " + SellCount + "</td>";
    str[17] = "</tr>";
    str[18] = "<tr align=left bgcolor=#E9E9E9>";
    str[19] = "  <td colspan=\"15\">amount SELL orders with open price > place " + SellMore + "</td>";
    str[20] = "</tr>";
    str[21] = "<tr align=left bgcolor=#E9E9E9>";
    str[22] = "  <td colspan=\"15\">amount SELL orders with open price < place " + SellLess + "</td>";
    str[23] = "</tr>";

      for (i = 0; i < 24; i++)
      {
       _lwrite(hFile, str[i], StringLen(str[i]));
      }
    */
//}

/*bool CreateStopOrder( int Type/*OP_BUY,OP_SELL*///)
/*{
int ticket;
      RefreshRates();
      double price;
 if (Type == OP_BUYSTOP)
   {
          price = Ask + Point * coeff;
          Print("OP_BUYSTOP price = ",price);
          string str = "OP_BUYSTOP price:" + DoubleToStr(price,digits);
          ticket = OrderSend(Symbol(), OP_BUYSTOP, Order_Volume, price, 1, 0, 0, str, MagicNumber, Expiration_Date, CLR_NONE);
   }
   else if (Type == OP_SELLSTOP)
   {
            price = Bid - Point * coeff;
            Print("OP_SELLSTOP price = ",price);
            str = "OP_SELLSTOP price:" + DoubleToStr(price,digits);
            ticket = OrderSend(Symbol(), OP_SELLSTOP, Order_Volume, price, 1, 0, 0, str, MagicNumber, Expiration_Date, CLR_NONE);
   }
   
  return (ticket);
}*/

/*bool CreateOrderWithMinCoeff( int Type/*OP_BUY,OP_SELL*///)
/*{
int ticket;
string str;
double price, st, tp;
MathSrand(TimeLocal());

   int MinCoeff = 5 * MathRand() / 32767;
      RefreshRates();
   Print("AccountBalance#",AccountBalance());
   if (Type == OP_BUY)
   {
    Print("OP_BUY");
    price = Ask;
    st = 0;
    tp = 0;
     ticket = OrderSend(Symbol(), OP_BUY, Order_Volume, price, 1, st, tp, "", MagicNumber, Expiration_Date, CLR_NONE);
   } 
   else if (Type == OP_SELL)
   {
    Print("OP_SELL");
    price = Bid;
    st = 0;
    tp = 0;
      ticket = OrderSend(Symbol(), OP_SELL, Order_Volume, price, 1, st, tp, "", MagicNumber, Expiration_Date, CLR_NONE);
   }
   else if (Type == OP_BUYLIMIT)
   {
      Print("OP_BUYLIMIT");
           price = Ask - Point * MinCoeff;
      str = "OP_BUYLIMIT price:" + DoubleToStr(price, digits);
      st = Ask - Point * MinCoeff*4;
      tp = Ask - Point * MinCoeff*2;
      ticket = OrderSend(Symbol(), OP_BUYLIMIT, Order_Volume, price, 1, st, tp, str, MagicNumber, Expiration_Date, CLR_NONE);
   }
   else if (Type == OP_SELLLIMIT)
   {
     Print("OP_SELLLIMIT");
      price = Bid + Point * MinCoeff;
      str = "OP_SELLLIMIT price:" + DoubleToStr(price, digits);
      st = Bid + Point * MinCoeff*4;
      tp = Bid + Point * MinCoeff*2;
      ticket = OrderSend(Symbol(), OP_SELLLIMIT, Order_Volume, price, 1, st, tp, str, MagicNumber, Expiration_Date, CLR_NONE);
   }
   else if (Type == OP_BUYSTOP)
   {
     Print("OP_BUYSTOP");
      price = Ask + Point * MinCoeff;
      str = "OP_BUYSTOP price:" + DoubleToStr(price, digits);
      st = Ask - Point * MinCoeff*2;
      tp = Ask + Point * MinCoeff*4;
      ticket = OrderSend(Symbol(), OP_BUYSTOP, Order_Volume, price, 1, st, tp, str, MagicNumber, Expiration_Date, CLR_NONE);
   }
   else if (Type == OP_SELLSTOP)
   {
    Print("OP_SELLSTOP");
      price = Bid - Point * MinCoeff;
      str = "OP_SELLSTOP price:" + DoubleToStr(price, digits);
      st = Bid + Point * MinCoeff*2;
      tp = Bid - Point * MinCoeff*4;
      ticket = OrderSend(Symbol(), OP_SELLSTOP, Order_Volume, price, 1, st, tp, str, MagicNumber, Expiration_Date, CLR_NONE);
   }
   
   if(ticket<0)
   {
      int err = GetLastError();
      Print("OrderSend failed with error # ",err);
      if (err != 130)
      {
         //Alert("Can`t add order");
      }
      return(false);
   }  
   else
   {
      Print("OrderSend started");
      Print("ticket# ",ticket," MagicNumber# ",MagicNumber);
      Print("AccountBalance# ",AccountBalance());

      return (true);
   }
}*/

/*bool CloseMarketOrder()
{
   for (int i = OrdersTotal()-1; i >= 0; i--)
   {
      if (OrderSelect( i, SELECT_BY_POS ) && (OrderType() == OP_BUY || OrderType() == OP_SELL))
      {
         if (OrderClose(OrderTicket(), OrderLots(), Bid, 1, CLR_NONE))
         {
            Print("Delete order# ", OrderTicket());
         }
         else
         {
            int err = GetLastError();
            Print("Order # ",OrderTicket(),"  Delete order with error # ", err);
            SetCloseOrderErrorToReport(OrderTicket(), OrderOpenTime(), OrderType(), OrderSymbol(), OrderOpenPrice(), OrderStopLoss(), OrderTakeProfit(), OrderClosePrice(), err);
            //Alert("Can`t delete order");
            continue;
         }
      } 
   }
}*/

/*bool CheckExpiration()
{
RefreshRates();
      for (int i = OrdersTotal()-1; i >= 0; i--)
      {
         if (OrderSelect( i, SELECT_BY_POS ))
         {
            if ( OrderType() != OP_BUY && OrderType() != OP_SELL && OrderExpiration() < 1)
            {
               Print("Order Expiration Date absent!!! Order # ",OrderTicket());
               //Alert("Order Expiration Date absent!!!"); 
            }
            else if ( OrderType() != OP_BUY && OrderType() != OP_SELL)
               Print("Order Expiration Date = ",OrderExpiration() ," Order # ",OrderTicket());
           
         }
      }
}
*/