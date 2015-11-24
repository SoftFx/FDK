//+------------------------------------------------------------------+
//|                                        OneClickTradingLevel2.mq4 |
//|                                           Copyright 2013, SoftFX |
//|                                               http://soft-fx.com |
//+------------------------------------------------------------------+
#property copyright "Copyright 2013, SoftFX"
#property link      "http://soft-fx.com"

#import "SoftFX.OneClickTrading.dll"

// this function calls if we get hwnd of a chart window and can start the plugin
int OneClickTrader_Create(string symbol, int timeframe, int digits, string accountServer, int isDemo, double lotSize, int precision, int hwnd);

// this function calls if the expert adviser closed by the user so we need to close the plugin
int OneClickTrader_Delete(int handle);

// this function informs the plugin that adviser is ready to work
int OneClickTrader_IsStarted(int handle);

// this function calls each time when a new tick appear
void OneClickTrader_PostMSI(int handle, double bid, double ask, double low, double high, double buyPos, double sellPos, double buyAvg, double sellAvg, double pl, double swapBuy, double swapSell, string accCurrency, double mrgn, double mrgnLvl, double fEquity);

// this function listen if a new command come from the plugin side so we can process it this expert adviser
int OneClickTrader_AcquireCommand(int handle, int timeoutInMilliseconds);

// this function release a command that "OneClickTrader_AcquireCommand" sent
void OneClickTrader_ReleaseCommand(int handle, int command);

// this function calls after "OneClickTrader_AcquireCommand" and returns command type (see #define CommandType_... variables)
int Command_GetType(int handle);

// this function calls after "OneClickTrader_AcquireCommand" and returns additional information of the command in String type (it allows to send Symbol name or comment for example)
string Command_GetString(int handle, string key);

// this function calls after "OneClickTrader_AcquireCommand" and returns additional information of the command in Int type
int Command_GetInt(int handle, string key);

// this function calls after "OneClickTrader_AcquireCommand" and returns additional information of the command in Double type
double Command_GetDouble(int handle, string key);

// this function returns a result of an operation to the plugin in String type
void Command_SetString(int handle, string key, string value);

// this function returns a result of an operation to the plugin in Int type
void Command_SetInt(int handle, string key, int value);

// this function returns a result of an operation to the plugin in Double type
void Command_SetDouble(int handle, string key, double value);

// this function send info about opened Orders to plugin to calculate margin
void OneClickTrader_PostOrdersInfo(int handle, int serverTime, int nLeverage, double fEquity, string accCurrency, int orderCount,
      string& arOrderSymbol[], int arOrderType[], int arOrderMarginCalcMode[], double arOrderVolume[], 
      int arOrderOpenTime[], double arOrderOpenPrice[], double arOrderContractSize[], double arOrderInitialMargin[], double arOrderTickSize[]);
#import

#define CommandType_OrderSend 0
#define CommandType_CloseAll 1
#define CommandType_CloseProfit 2
#define CommandType_CloseLoss 3
#define CommandType_ClosePending 4
#define CommandType_CloseSymbByPercent 5

#define Key_Symbol "Symbol"
#define Key_Cmd "Cmd"
#define Key_Volume "Volume"
#define Key_Price "Price"
#define Key_Slippage "Slippage"
#define Key_StopLoss "StopLoss"
#define Key_TakeProfit "TakeProfit"
#define Key_Comment "Comment"
#define Key_Magic "Magic"
#define Key_Expiration "Expiration"
#define Key_ArrowColor "ArrowColor"
#define Key_Order "Order"
#define Key_Status "Status"

#define OP_CloseLimits 0
#define OP_CloseStops 1
#define OP_CloseSymbolPending 2
#define OP_CloseAllPending 3

#define AcquireCommandTimeoutInMs 100
#define TimeoutOfMetaSymbolInfoRefresh 1000


// >> TP/SL levels modification using chart lines variables
// Stop Loss line color and style
color sl_color = Orange;
int sl_style = STYLE_DASH;
// Take Profit line color and style
color tp_color = DarkGray;
int tp_style = STYLE_DASH;

string linePrefix = "octl2";
string tpLinePrefix = "octl2_tp_";
string slLinePrefix = "octl2_sl_";

int SlTp_tickCount = 0;
int refreshSlTpEvery = 500; //refresh Sl Tp lines Every 500ms
// << TP/SL levels modification using chart lines variables

int _slippageToCLoseOrder = 10;

// this function creates an order (simple or pending)
void ProcessOrderSend(int command)
{
   // get command characteristics from the plugin side
   string symbol = Command_GetString(command, Key_Symbol);
   int cmd = Command_GetInt(command, Key_Cmd);
   double volume = Command_GetDouble(command, Key_Volume);
   double price = Command_GetDouble(command, Key_Price);
   int slippage = Command_GetInt(command, Key_Slippage);
   double stoploss = Command_GetDouble(command, Key_StopLoss);
   double takeprofit = Command_GetDouble(command, Key_TakeProfit);
   string comment = Command_GetString(command, Key_Comment);
   int magic = Command_GetInt(command, Key_Magic);
   datetime expiration = Command_GetInt(command, Key_Expiration);
   color arrow_color = Command_GetInt(command, Key_ArrowColor);
   
   // set StopLoss\TakeProfit only for pending orders 
   double orderStopLoss = 0;
   double orderTakeProfit = 0;
   if (cmd != OP_BUY && cmd != OP_SELL)
   {
      if (stoploss != 0)
         orderStopLoss = price + stoploss;

      if (takeprofit != 0)
         orderTakeProfit = price + takeprofit;
   }
   
   //--- Create an Order ---
   int order = OrderSend(symbol, cmd, volume, price, slippage, orderStopLoss, orderTakeProfit, comment, magic, 0, CLR_NONE);
   
   // send the result of order creation to the plugin
   Command_SetInt(command, Key_Order, order);
   if(order >= 0)
   {
      Command_SetInt(command, Key_Status, 0);
   }
   else
   {
      Command_SetInt(command, Key_Status, GetLastError());
   }
   
   // modify order if needs to set StopLoss\TakeProfit to BUY\SELL order
   if(order >= 0 && (cmd == OP_BUY || cmd == OP_SELL) && (stoploss != 0 || takeprofit != 0))
   {
      if (OrderSelect(order, SELECT_BY_TICKET))
      {
         double orderOpenPrice = OrderOpenPrice();
         if (stoploss == 0)
            orderStopLoss = 0;
         else
            orderStopLoss = orderOpenPrice + stoploss;
         
         if (takeprofit == 0)
            orderTakeProfit = 0;
         else
            orderTakeProfit = orderOpenPrice + takeprofit;
         
         bool orderModifyResult = OrderModify(OrderTicket(), OrderOpenPrice(), orderStopLoss, orderTakeProfit, OrderExpiration());
         if (orderModifyResult)
         {
            Command_SetInt(command, Key_Status, 0);
         }
         else
         {
            Command_SetInt(command, Key_Status, GetLastError());
         }
      }
   }
}

// if the plugin send a command we use this function to ask the plugin what is the command type
void Process(int command)
{
   if (IsExpertEnabled())
   {
      // ask the plugin what is the command type
      int type = Command_GetType(command);
      
      string operationName = "";
      int tickCount = GetTickCount();
      
      switch(type)
      {
         // it needs to create an order
         case CommandType_OrderSend:
            ProcessOrderSend(command);
            operationName = "Order Send";
            break;
      
         // it needs to close all orders
         case CommandType_CloseAll:
            CloseAll();
            operationName = "Close All";
            break;
      
         // it needs to close only Profit orders
         case CommandType_CloseProfit:
            CloseAllProfitLossOrders(true);
            operationName = "Close All Profit";
            break;
      
         // it needs to close only Loss orders
         case CommandType_CloseLoss:
            CloseAllProfitLossOrders(false);
            operationName = "Close All Loss";
            break;
         
         // it needs to close Pending orders
         case CommandType_ClosePending:
            ClosePending(command);
            operationName = "Close Pendings";
            break;
      
         // it needs to close orders by volume percentage
         case CommandType_CloseSymbByPercent:
            CloseSymbByPercent(command);
            operationName = "Close By Percentage";
            break;
         default:
            break;
      }
      if (operationName != "")
      {
         tickCount = GetTickCount() - tickCount;
         Print(operationName + " took " + tickCount + " ms");
      }
   }
}

// close orders partly by volume percentage
void CloseSymbByPercent(int command)
{
   // get percentage value from the plugin and exit if it equals zero
   int percentage = Command_GetInt(command, Key_Cmd);
   if (percentage == 0)
      return;
   
   // adjust percentage between 1 and 100
   if (percentage < 1)
      percentage = 1;
      
   if (percentage > 100)
      percentage = 100;
      
   int total=OrdersTotal(); // get orders count
   int orderType; // order type  
   int ticket; // ticket number
   double orderLots;
   double priceClose; // price to close orders;
   
   // go over all orders 
   for(int i = total - 1; i >= 0; i--)
   {  
      // Exit if the adviser is stopped
      if (IsStopped())
         break;

      if (OrderSelect(i, SELECT_BY_POS))
      {
         // process only orders that are created by current symbol
         if (OrderSymbol() != Symbol())
            continue;
      
         orderType = OrderType();
         ticket = OrderTicket();
         
         // calculate volume to close based on percentage   
         orderLots = OrderLots();
         if (percentage != 100)
         {
            orderLots = orderLots * percentage / 100;
         }
         
         // close orders partly
         switch(orderType)
         {
            case OP_BUY:
               // close buy
               priceClose=MarketInfo(OrderSymbol(), MODE_BID);
               OrderClose(ticket, orderLots, priceClose, _slippageToCLoseOrder, CLR_NONE);
               break;
            case OP_SELL:
               // close sell
               priceClose=MarketInfo(OrderSymbol(),MODE_ASK);
               OrderClose(ticket, orderLots, priceClose, _slippageToCLoseOrder, CLR_NONE);
               break;
            default:
               break;
         }
      }
   }
}

// close pending orders
void ClosePending(int command)
{
   // select wich type of pending orders shall be closed
   int cmd = Command_GetInt(command, Key_Cmd);

   int total=OrdersTotal();
   int orderType; // order type  
   int ticket; // ticket number
   bool deleteOrder;
   
   for(int i = total - 1; i >= 0; i--)
   {
      // Exit if the adviser is stopped
      if (IsStopped())
         break;
        
      if (OrderSelect(i, SELECT_BY_POS))
      {
         if (OrderSymbol() != Symbol())
            continue;

         orderType = OrderType();
         ticket = OrderTicket();
         
         // define if it needs to close current order
         deleteOrder = false;
         
         switch(cmd)
         {
            // this works if the user presses on "Limits" button
            case OP_CloseLimits:
               deleteOrder = (orderType == OP_BUYLIMIT || orderType == OP_SELLLIMIT);
               break;
            // this works if the user presses on "Stops" button
            case OP_CloseStops:
               deleteOrder = (orderType == OP_BUYSTOP || orderType == OP_SELLSTOP);
               break;
            // this is obsolete functionality becase "Symb" button duplicate "All" button functionality and is hidden for now
            case OP_CloseSymbolPending:
               //deleteOrder = (orderType == OP_BUYLIMIT || orderType == OP_BUYSTOP || orderType == OP_SELLLIMIT || orderType == OP_SELLSTOP) && OrderSymbol() == Symbol();
               deleteOrder = (orderType == OP_BUYLIMIT || orderType == OP_BUYSTOP || orderType == OP_SELLLIMIT || orderType == OP_SELLSTOP);
               break;
            // this works if the user presses on "All" button
            case OP_CloseAllPending:
               deleteOrder = (orderType == OP_BUYLIMIT || orderType == OP_BUYSTOP || orderType == OP_SELLLIMIT || orderType == OP_SELLSTOP);
               break;
            default:
               break;
         }

         if (deleteOrder)
         {
            OrderDelete(ticket);
         }
      }
   }
}

// Close ALL orders
void CloseAll()
{
   int total = OrdersTotal();
   int orderType; // order type  
   int ticket; // ticket number
   double priceClose; // price to close orders;
   
   for(int i = total - 1; i >= 0; i--)
   {
      // Exit if the adviser is stopped
      if (IsStopped())
         break;

      if (OrderSelect(i, SELECT_BY_POS))
      {
         if (OrderSymbol() != Symbol())
            continue;
      
         orderType = OrderType();
         ticket = OrderTicket();
         
         switch(orderType)
         {
            case OP_BUYLIMIT:
               OrderDelete(ticket);
               break;
            case OP_BUYSTOP:
               OrderDelete(ticket);
               break;
            case OP_BUY:
               // close buy                
               priceClose=MarketInfo(OrderSymbol(), MODE_BID);
               OrderClose(ticket, OrderLots(), priceClose, _slippageToCLoseOrder, CLR_NONE);
               break;
            case OP_SELLLIMIT:
               OrderDelete(ticket);
               break;
            case OP_SELLSTOP:
               OrderDelete(ticket);
               break;
            case OP_SELL:
               // close sell
               priceClose=MarketInfo(OrderSymbol(),MODE_ASK);
               OrderClose(ticket, OrderLots(), priceClose, _slippageToCLoseOrder, CLR_NONE);
               break;
            default:
               break;
         }
      }
   }
}


// Close only Profit or Loss orders. THis works if the user presses on "CP" or "CL" button
// isCloseProfit == true then Close all Profit orders (the user presses on "CP" button)
// isCloseProfit == false then Close all Loss orders (the user presses on "CL" button)
void CloseAllProfitLossOrders(bool isCloseProfit)
{
   int total=OrdersTotal();
   int orderType; // order type  
   int ticket; // ticket number
   double priceClose; // price to close orders;
   double profit;
   bool closeOrder;
   
   for(int i = total - 1; i >= 0; i--)
   {
      // Exit if the adviser is stopped
      if (IsStopped())
         break;
         
      if (OrderSelect(i, SELECT_BY_POS))
      {
         if (OrderSymbol() != Symbol())
            continue;

         orderType = OrderType();
         if (orderType != OP_BUY && orderType != OP_SELL)
            continue;
            
         ticket = OrderTicket();
         
         // calculate profit
         profit = OrderProfit() + OrderCommission() + OrderSwap();
         
         // define if it needs to close an order
         closeOrder = (profit > 0 && isCloseProfit) || (profit < 0 && !isCloseProfit);
         if (!closeOrder)
            continue;
         
         switch(orderType)
         {
            case OP_BUY:
               // close buy                
               priceClose=MarketInfo(OrderSymbol(), MODE_BID);
               OrderClose(ticket, OrderLots(), priceClose, _slippageToCLoseOrder, CLR_NONE);
               break;
            case OP_SELL:
               // close sell
               priceClose=MarketInfo(OrderSymbol(),MODE_ASK);
               OrderClose(ticket, OrderLots(), priceClose, _slippageToCLoseOrder, CLR_NONE);
               break;
            default:
               break;
         }
      }
   }
}

// if this variable not equal to zero then plugin stared
int gTrader = 0;

// this function checks 3 flags:
// IsTradeAllowed(), IsDllsAllowed(), IsExpertEnabled()
// before plugin stars and call Alert if on of them is false
void CheckAllowExpertFlags()
{
   if (!IsTradeAllowed())
      Alert("Please turn on Allow live trading");
   if (!IsDllsAllowed())
      Alert("Please turn on Allow DLL imports");
   if (!IsExpertEnabled())
      Alert("Please enable Expert Advisors");
}

int init()
{
   string symbol = Symbol();
   string accountServer = AccountServer();
   bool isDemo = IsDemo();
   double lotSize = MarketInfo(symbol, MODE_LOTSIZE);
   int precision = MarketInfo(symbol, MODE_DIGITS);
   int timeframe = Period();
   int hwnd = WindowHandle(symbol, timeframe);
   
   // try to get hwnd of a chart and run the plugin one time
   if(hwnd > 0)
   {
      CheckAllowExpertFlags();
      gTrader = OneClickTrader_Create(symbol, timeframe, Digits, accountServer, isDemo, lotSize, precision, hwnd);
   }
   return (0);
}

int deinit()
{
   // close the plugin
   if(gTrader > 0)
   {   
      OneClickTrader_Delete(gTrader);
      gTrader = 0;
   }
   return(0);
}

int start()
{  
   string symbol = Symbol();
   string accountServer = AccountServer();
   bool isDemo = IsDemo();
   double lotSize = MarketInfo(symbol, MODE_LOTSIZE);
   int precision = MarketInfo(symbol, MODE_DIGITS);
   int timeframe = Period();
   
   double high;
   double low;
   
   double buyPos;
   double sellPos;
   double buyAvg;
   double sellAvg;
   double profit;
   
   double swapBuy;
   double swapSell;
   
   double mrgn;
   double mrgnLvl;
   
   // this cycle works until we get hwnd of a chart thus we can run the plugin
   for(; (0 == gTrader) && !IsStopped();) 
   {
      int hwnd = WindowHandle(symbol, timeframe);
      if(hwnd > 0)
      {
         CheckAllowExpertFlags();
         // run the plugin
         gTrader = OneClickTrader_Create(symbol, timeframe, Digits, accountServer, isDemo, lotSize, precision, hwnd);
         break;
      }  
      Sleep(1);  
   }
   
   bool forceMetaSymbolInfoRefresh = false;
   int timeout = 0;
   
   OneClickTrader_IsStarted(gTrader);
   
   // this cycle works until the expert adviser is not closed
   for(;!IsStopped();)
   {
      if(RefreshRates() || forceMetaSymbolInfoRefresh)
      { // new tick come
      
         //high = iHigh(Symbol(), PERIOD_D1, 0);
         //low = iLow(Symbol(), PERIOD_D1, 0);
         high = iHigh(NULL, PERIOD_D1, 0);
         low = iLow(NULL, PERIOD_D1, 0);
         
         // calculate position, average, PL at once
         CalculatePositionAveragePL(buyPos, sellPos, buyAvg, sellAvg, profit);
         
         // get SWAPs
         GetSwapInfo(swapBuy, swapSell);
         
         // get margin & marginLevel 
         GetMargin(mrgn, mrgnLvl);
         
         // update the plugin
         OneClickTrader_PostMSI(gTrader, Bid, Ask, low, high, buyPos, sellPos, buyAvg, sellAvg, profit, swapBuy, swapSell, AccountCurrency(), mrgn, mrgnLvl, AccountEquity());
         timeout = 0;
         forceMetaSymbolInfoRefresh = false;
      }
      
      // check if the plugin send a command
      int command = OneClickTrader_AcquireCommand(gTrader, AcquireCommandTimeoutInMs);
      if(command > 0)
      {
         // process a command
         Process(command);
         OneClickTrader_ReleaseCommand(gTrader, command);               
         forceMetaSymbolInfoRefresh = true;
      }
      else
      {
         timeout += AcquireCommandTimeoutInMs;
      }
      if(timeout >= TimeoutOfMetaSymbolInfoRefresh)
      {
         forceMetaSymbolInfoRefresh = true;
      }
      
      UpdateOrders();
   }
   return(0);
}

// calculate position by Buy and by Sell separately and return buyPos, sellPos;
// calculate average by Buy and by Sell separately and return buyAvg, sellAvg;
// calculate PL and return profit
void CalculatePositionAveragePL(double &buyPos, double &sellPos, double &buyAvg, double &sellAvg, double &profit)
{
   int total = OrdersTotal();
   int orderType; // order type  
 
   buyPos = 0;
   sellPos = 0;
   buyAvg = 0;
   sellAvg = 0;
   profit = 0;

   double buyPriceVolumes = 0;
   double buyVolumes = 0;
   double sellPriceVolumes = 0;
   double sellVolumes = 0;
   
   double orderLots;
   double orderPrice;
   
   for(int i = total - 1; i >= 0; i--)
   {
      if (OrderSelect(i, SELECT_BY_POS))
      {
         if (OrderSymbol() != Symbol())
            continue;

         orderType=OrderType();
         
         if (orderType == OP_BUY || orderType == OP_SELL)
         {
            orderLots = OrderLots();
            orderPrice = OrderOpenPrice();
            
            switch(orderType)
            {
               case OP_BUY:
                  buyPos = buyPos + orderLots;
                  buyPriceVolumes = buyPriceVolumes + orderPrice * orderLots;
                  buyVolumes = buyVolumes + orderLots;
                  break;
               case OP_SELL:
                  sellPos = sellPos + orderLots;
                  sellPriceVolumes = sellPriceVolumes + orderPrice * orderLots;
                  sellVolumes = sellVolumes + orderLots;
                  break;
               default:
                  break;
            }
            profit = profit + OrderProfit() + OrderCommission() + OrderSwap();
         }
      }
   }
   if (buyVolumes != 0)
      buyAvg = buyPriceVolumes / buyVolumes;   
   if (sellVolumes != 0)
      sellAvg = sellPriceVolumes / sellVolumes;   
}


void GetSwapInfo(double& swapBuy, double& swapSell)
{
   swapBuy = MarketInfo(Symbol(), MODE_SWAPLONG);
   swapSell = MarketInfo(Symbol(), MODE_SWAPSHORT);
   
   // if today is Wednesday then multiply them by 3
   if (DayOfWeek() == 3)
   {
      swapBuy *= 3;
      swapSell *= 3; 
   }
}

void GetMargin(double& mrgn, double& mrgnLvl)
{
/*   if (IsMM_Account())
   { // Market Maker account */
      mrgn = AccountMargin();
      if (mrgn == 0)
         mrgnLvl = 0;
      else
         mrgnLvl = AccountEquity() / AccountMargin() * 100;
      
/*   }
   else
   { // ECN account
      mrgn = AccountMargin();
      mrgnLvl = mrgn / AccountEquity() * 100;
   }*/
}

// returns if the accunt is Market Maker. otherwise - ECN
bool IsMM_Account()
{
/* Competition - ecn
   ECN Demo - ecn
   ECN Live - ecn
   Real1 - mm/stp
   Real3 - mm/stp
   Real2 - mm/stp */

   int pos = StringFind(AccountServer(), "Real");
   return (pos >= 0);
}

// --------------- Margin Calculation Variables ------------------------
// --------------- Store orders info to send it to Plugin if something changed ---
int checkOrders_tickCount = 0;
int checkOrdersEvery = 1000;

int orderCount = 0;
string arOrderSymbol[];
int arOrderType[];
int arOrderMarginCalcMode[];
double arOrderVolume[];
int arOrderOpenTime[];
double arOrderOpenPrice[];
double arOrderContractSize[];
double arOrderInitialMargin[];
double arOrderTickSize[];

void UpdateOrders()
{
   //check orders every 'checkOrdersEvery' ms
   int ticksDiff = GetTickCount() - checkOrders_tickCount;
   if (ticksDiff >= 0 && ticksDiff <= checkOrdersEvery)
      return;
   checkOrders_tickCount = GetTickCount();
   
   bool isSendOrdersInfo = false;
   int total = OrdersTotal();
   if (orderCount == total)
   {
      for(int i = 0; i < total; i++)
      {
         if(OrderSelect(i,SELECT_BY_POS))
         {
            double dt1 = (double)OrderOpenTime();
            
            if (arOrderSymbol[i] != OrderSymbol() || 
                arOrderType[i] != OrderType() || 
                arOrderOpenTime[i] != dt1 ||
                arOrderOpenPrice[i] != OrderOpenPrice() ||
                arOrderVolume[i] != OrderLots())
            {
               isSendOrdersInfo = true;
               break;
            }
         }
      }
   }
   else
   {
      isSendOrdersInfo = true;
   }
   
   if (isSendOrdersInfo)
   {
      // 1) update orders info
      orderCount = total;
      ArrayResize(arOrderSymbol, orderCount);
      ArrayResize(arOrderType, orderCount);
      ArrayResize(arOrderMarginCalcMode, orderCount);
      ArrayResize(arOrderVolume, orderCount);
      ArrayResize(arOrderOpenTime, orderCount);
      ArrayResize(arOrderOpenPrice, orderCount);
      ArrayResize(arOrderContractSize, orderCount);
      ArrayResize(arOrderInitialMargin, orderCount);
      ArrayResize(arOrderTickSize, orderCount);
      
      for(i = 0; i < total; i++)
      {
         if(OrderSelect(i, SELECT_BY_POS))
         {
            arOrderSymbol[i] = OrderSymbol();
            arOrderType[i] = OrderType();
            arOrderMarginCalcMode[i] = MarketInfo(arOrderSymbol[i], MODE_MARGINCALCMODE);
            arOrderVolume[i] = OrderLots();
            arOrderOpenTime[i] = OrderOpenTime();
            arOrderOpenPrice[i] = OrderOpenPrice();
            arOrderContractSize[i] = MarketInfo(arOrderSymbol[i], MODE_LOTSIZE);
            arOrderInitialMargin[i] = MarketInfo(arOrderSymbol[i], MODE_MARGININIT);
            arOrderTickSize[i] = MarketInfo(arOrderSymbol[i], MODE_TICKSIZE);
         }
      }
      
      // 2) send event
      int serverTime = TimeCurrent();
      int nLeverage = AccountLeverage();
      double fEquity = AccountEquity();
      string accCurrency = AccountCurrency();
      
      OneClickTrader_PostOrdersInfo(gTrader, serverTime, nLeverage, fEquity, accCurrency, orderCount,
                                    arOrderSymbol, arOrderType, arOrderMarginCalcMode, arOrderVolume, 
                                    arOrderOpenTime, arOrderOpenPrice, arOrderContractSize, arOrderInitialMargin, arOrderTickSize);
   }
}

