#property copyright "Copyright c 2006, Cyberia Decisions"
#property link      "http://cyberia.org.ru"


#define DECISION_BUY 1
#define DECISION_SELL 0
#define DECISION_UNKNOWN -1

//---- ���������� ����������
extern bool ExitMarket = false;
extern bool ShowSuitablePeriod = false;
extern bool ShowMarketInfo = false;
extern bool ShowAccountStatus = false;
extern bool ShowStat = false;
extern bool ShowDecision = false;
extern bool ShowDirection = false;
extern bool BlockSell = false;
extern bool BlockBuy = false;
extern bool ShowLots = false;
extern bool BlockStopLoss = false;
extern bool DisableShadowStopLoss = true;
extern bool DisableExitSell = false;
extern bool DisableExitBuy = false;
extern bool EnableMACD = false;
extern bool EnableMA = false;
extern bool EnableCyberiaLogic = true;
extern bool EnableLogicTrading = false;
extern bool EnableMoneyTrain = false;
extern bool EnableReverceDetector = false;
extern double ReverceIndex = 3;
extern double MoneyTrainLevel = 4;
extern int MACDLevel = 10;
extern bool AutoLots = True;
extern bool AutoDirection = True;
extern double ValuesPeriodCount = 23;
extern double ValuesPeriodCountMax = 23;
extern double SlipPage = 1; // ��������������� ������
extern double Lots = 0.1; // ���������� �����
extern double StopLoss = 0;
extern double TakeProfit = 0;
extern double SymbolsCount = 1;
extern double Risk = 0.5;
extern double StopLossIndex = 1.1;
extern bool AutoStopLossIndex = true;
extern double StopLevel;
bool DisableSell = false;
bool DisableBuy = false;
bool ExitSell = false;
bool ExitBuy = false;
double Disperce = 0;
double DisperceMax = 0;
bool DisableSellPipsator = false;
bool DisableBuyPipsator = false;
//----
double ValuePeriod = 1; // ��� ������� � �������
double ValuePeriodPrev = 1;
int FoundOpenedOrder = false;
bool DisablePipsator = false;
double BidPrev = 0;
double AskPrev = 0;
// ���������� ��� ������ �������� �������������
double BuyPossibilityQuality;
double SellPossibilityQuality;
double UndefinedPossibilityQuality;
//double BuyPossibilityQualityMid;
double PossibilityQuality;
double QualityMax = 0;
//----
double BuySucPossibilityQuality;
double SellSucPossibilityQuality;
double UndefinedSucPossibilityQuality;
double PossibilitySucQuality;
//----
double ModelingPeriod; // ������ ������������� � �������
double ModelingBars; // ���������� ����� � �������
//----
double Spread; // �����
double Decision;
double DecisionValue;
double PrevDecisionValue;
//----
int ticket, total, cnt;
//----
double BuyPossibility;
double SellPossibility;
double UndefinedPossibility;
double BuyPossibilityPrev;
double SellPossibilityPrev;
double UndefinedPossibilityPrev;
//----
double BuySucPossibilityMid; // ������� ����������� �������� �������
double SellSucPossibilityMid; // ������� ����������� �������� �������
double UndefinedSucPossibilityMid; // ������� �������� ����������� ��������������� ���������
//----
double SellSucPossibilityCount; // ���������� ������������ �������� �������
double BuySucPossibilityCount; // ���������� ������������ �������� �������
double UndefinedSucPossibilityCount; // ���������� ������������ ��������������� ���������
//----
double BuyPossibilityMid; // ������� ����������� �������
double SellPossibilityMid; // ������� ����������� �������
double UndefinedPossibilityMid; // ������� ����������� ��������������� ���������
//----
double SellPossibilityCount; // ���������� ������������ �������
double BuyPossibilityCount; // ���������� ������������ �������
double UndefinedPossibilityCount; // ���������� ������������ ��������������� ���������
//----
// ���������� ��� �������� ���������� � �����
double ModeLow;
double ModeHigh;
double ModeTime;
double ModeBid;
double ModeAsk;
double ModePoint;
double ModeDigits;
double ModeSpread;
double ModeStopLevel;
double ModeLotSize;
double ModeTickValue;
double ModeTickSize;
double ModeSwapLong;
double ModeSwapShort;
double ModeStarting;
double ModeExpiration;
double ModeTradeAllowed;
double ModeMinLot;
double ModeLotStep;
//+------------------------------------------------------------------+
//|��������� ���������� � �����                                                                  |
//+------------------------------------------------------------------+
int GetMarketInfo()
  {
   // ��������� ���������� � �����
   ModeLow = MarketInfo(Symbol(), MODE_LOW);
   ModeHigh = MarketInfo(Symbol(), MODE_HIGH);
   ModeTime = MarketInfo(Symbol(), MODE_TIME);
   ModeBid = MarketInfo(Symbol(), MODE_BID);
   ModeAsk = MarketInfo(Symbol(), MODE_ASK);
   ModePoint = MarketInfo(Symbol(), MODE_POINT);
   ModeDigits = MarketInfo(Symbol(), MODE_DIGITS);
   ModeSpread = MarketInfo(Symbol(), MODE_SPREAD);
   ModeStopLevel = MarketInfo(Symbol(), MODE_STOPLEVEL);
   ModeLotSize = MarketInfo(Symbol(), MODE_LOTSIZE);
   ModeTickValue = MarketInfo(Symbol(), MODE_TICKVALUE);
   ModeTickSize = MarketInfo(Symbol(), MODE_TICKSIZE);
   ModeSwapLong = MarketInfo(Symbol(), MODE_SWAPLONG);
   ModeSwapShort = MarketInfo(Symbol(), MODE_SWAPSHORT);
   ModeStarting = MarketInfo(Symbol(), MODE_STARTING);
   ModeExpiration = MarketInfo(Symbol(), MODE_EXPIRATION);
   ModeTradeAllowed = MarketInfo(Symbol(), MODE_TRADEALLOWED);
   ModeMinLot = MarketInfo(Symbol(), MODE_MINLOT);
   ModeLotStep = MarketInfo(Symbol(), MODE_LOTSTEP);
   // ������� ���������� � �����
   if ( ShowMarketInfo == True )
     {
       Print("ModeLow:",ModeLow);
       Print("ModeHigh:",ModeHigh);
       Print("ModeTime:",ModeTime);
       Print("ModeBid:",ModeBid);
       Print("ModeAsk:",ModeAsk);
       Print("ModePoint:",ModePoint);
       Print("ModeDigits:",ModeDigits);
       Print("ModeSpread:",ModeSpread);
       Print("ModeStopLevel:",ModeStopLevel);
       Print("ModeLotSize:",ModeLotSize);
       Print("ModeTickValue:",ModeTickValue);
       Print("ModeTickSize:",ModeTickSize);
       Print("ModeSwapLong:",ModeSwapLong);
       Print("ModeSwapShort:",ModeSwapShort);
       Print("ModeStarting:",ModeStarting);
       Print("ModeExpiration:",ModeExpiration);
       Print("ModeTradeAllowed:",ModeTradeAllowed);
       Print("ModeMinLot:",ModeMinLot);
       Print("ModeLotStep:",ModeLotStep);
     }
   return (0);
  }
//+------------------------------------------------------------------+
//| ������ ���������� �����                                          |
//+------------------------------------------------------------------+
int CyberiaLots()
  {
   GetMarketInfo();
   // ����� �����
   double S;
   // ��������� ����
   double L;
   // ���������� �����
   double k;
   // ��������� ������ ������
   if( AutoLots == true )
     {
       if(SymbolsCount != OrdersTotal())
         {
           S = (AccountBalance()* Risk - AccountMargin()) * AccountLeverage() / 
                (SymbolsCount - OrdersTotal());
         }
       else
         {
           S = 0;
         }
       // ���������, �������� �� ������ �� ����
       if(StringFind( Symbol(), "USD") == -1)
         {
           if(StringFind( Symbol(), "EUR") == -1)
             {
               S = 0;
             }
           else
             {
               S = S / iClose ("EURUSD", PERIOD_M1, 0);
               if(StringFind( Symbol(), "EUR") != 0)
                  {
                  S /= Bid;
                  }
             }
         }
       else
         {
           if(StringFind(Symbol(), "USD") != 0)
             {
               S /= Bid;
             }
         }
       S /= ModeLotSize;
       S -= ModeMinLot;
       S /= ModeLotStep;
       S = NormalizeDouble(S, 0);
       S *= ModeLotStep;
       S += ModeMinLot;
       Lots = S;
       if(ShowLots == True)
           Print ("Lots:", Lots);
     }
   return (0);
  }
//+------------------------------------------------------------------+
//|   �������������� ���������                                       |
//+------------------------------------------------------------------+
int init()
  {
   AccountStatus();   
   GetMarketInfo();
   ModelingPeriod = ValuePeriod * ValuesPeriodCount; // ������ ������������� � �������
   if (ValuePeriod != 0 )
       ModelingBars = ModelingPeriod / ValuePeriod; // ���������� ����� � �������
   CalculateSpread();
   return(0);
  }
//+------------------------------------------------------------------+
//| ��������� ����������� �������� ������ (������������ �������      |
//| � ����� ����� ������ �������� ����������� �������� ������ ����   |
//| ������ ��������� �������� ������                                 |
//+------------------------------------------------------------------+
int CalculateSpread()
  {
   Spread = Ask - Bid;
   return (0);
  }
//+------------------------------------------------------------------+
//| ��������� �������                                                |
//+------------------------------------------------------------------+
int CalculatePossibility (int shift)
  {
   DecisionValue = iClose( Symbol(), PERIOD_M1, ValuePeriod * shift) - 
                   iOpen( Symbol(), PERIOD_M1, ValuePeriod * shift);
   PrevDecisionValue = iClose( Symbol(), PERIOD_M1, ValuePeriod * (shift+1)) - 
                       iOpen( Symbol(), PERIOD_M1, ValuePeriod * (shift+1));
   SellPossibility = 0;
   BuyPossibility = 0;
   UndefinedPossibility = 0;
   if(DecisionValue != 0) // ���� ������� �� �����������
     {
       if(DecisionValue > 0) // ���� ������� � ������ �������
         {
           // ���������� �� ����������� �������
           if(PrevDecisionValue < 0) // ������������� ������� � ������ �������
             {
               Decision = DECISION_SELL;
               BuyPossibility = 0;
               SellPossibility = DecisionValue;
               UndefinedPossibility = 0;
             }
           else  // ����� ������� �� ����������
             {
               Decision = DECISION_UNKNOWN;
               UndefinedPossibility = DecisionValue;
               BuyPossibility = 0;
               SellPossibility = 0;
             }
         }
       else // ���� ������� � ������ �������
         {
           if(PrevDecisionValue > 0) // ������������� ������� � ������ �������
             {
               Decision = DECISION_BUY;
               SellPossibility = 0;
               UndefinedPossibility = 0;
               BuyPossibility = -1 * DecisionValue;
             }
           else  // ������� �� ����������
             {
               Decision = DECISION_UNKNOWN;
               UndefinedPossibility = -1 * DecisionValue;
               SellPossibility = 0;
               BuyPossibility = 0;
             }
         }
     }
   else
     {
       Decision = DECISION_UNKNOWN;
       UndefinedPossibility = 0;
       SellPossibility = 0;
       BuyPossibility = 0;
     }
   return (Decision);
  }
//+------------------------------------------------------------------+
//| ��������� ���������� ������������                                |
//+------------------------------------------------------------------+
int CalculatePossibilityStat()
  {
   int i;
   BuySucPossibilityCount = 0;
   SellSucPossibilityCount = 0;
   UndefinedSucPossibilityCount = 0;
//----
   BuyPossibilityCount = 0;
   SellPossibilityCount = 0;
   UndefinedPossibilityCount = 0;
   // ��������� ������� �������� �����������
   BuySucPossibilityMid = 0;
   SellSucPossibilityMid = 0;
   UndefinedSucPossibilityMid = 0;
   BuyPossibilityQuality = 0;
   SellPossibilityQuality = 0;
   UndefinedPossibilityQuality = 0;
   PossibilityQuality = 0;
//----
   BuySucPossibilityQuality = 0;
   SellSucPossibilityQuality = 0;
   UndefinedSucPossibilityQuality = 0;
   PossibilitySucQuality = 0;
   for( i = 0 ; i < ModelingBars ; i ++ )
     {
       // ��������� ������� ��� ������� ���������
       CalculatePossibility (i);
       // ���� ������� ��� �������� i - ���������         
       if(Decision == DECISION_SELL )
           SellPossibilityQuality ++;           
       // ���� ������� ��� �������� i - ��������
       if(Decision == DECISION_BUY )
           BuyPossibilityQuality ++;           
       // ���� ������� ��� �������� i - �� ����������
       if(Decision == DECISION_UNKNOWN )
           UndefinedPossibilityQuality ++;           
       // �� �� ������ ��� �������� ��������                 
         //
       if((BuyPossibility > Spread) || (SellPossibility > Spread) || 
          (UndefinedPossibility > Spread))
         {
           if(Decision == DECISION_SELL)
               SellSucPossibilityQuality ++;                     
           if(Decision == DECISION_BUY)
               BuySucPossibilityQuality ++;
           if(Decision == DECISION_UNKNOWN )
               UndefinedSucPossibilityQuality ++;                   
         }  
       // ��������� ������� ����������� �������
       // ����������� �������
       BuyPossibilityMid *= BuyPossibilityCount;
       BuyPossibilityCount ++;
       BuyPossibilityMid += BuyPossibility;
       if(BuyPossibilityCount != 0 )
           BuyPossibilityMid /= BuyPossibilityCount;
       else
           BuyPossibilityMid = 0;
       // ����������� �������
       SellPossibilityMid *= SellPossibilityCount;
       SellPossibilityCount ++;
       SellPossibilityMid += SellPossibility;
       if(SellPossibilityCount != 0 )
           SellPossibilityMid /= SellPossibilityCount;
       else
           SellPossibilityMid = 0;
       // ����������� ��������������� ���������
       UndefinedPossibilityMid *= UndefinedPossibilityCount;
       UndefinedPossibilityCount ++;
       UndefinedPossibilityMid += UndefinedPossibility;
       if(UndefinedPossibilityCount != 0)
           UndefinedPossibilityMid /= UndefinedPossibilityCount;
       else
           UndefinedPossibilityMid = 0;
       // ��������� ������� ����������� �������� �������
       if(BuyPossibility > Spread)
         {
           BuySucPossibilityMid *= BuySucPossibilityCount;
           BuySucPossibilityCount ++;
           BuySucPossibilityMid += BuyPossibility;
           if(BuySucPossibilityCount != 0)
               BuySucPossibilityMid /= BuySucPossibilityCount;
           else
               BuySucPossibilityMid = 0;
         }
       if(SellPossibility > Spread)
         {
           SellSucPossibilityMid *= SellSucPossibilityCount;
           SellSucPossibilityCount ++;                 
           SellSucPossibilityMid += SellPossibility;
           if (SellSucPossibilityCount != 0)
              SellSucPossibilityMid /= SellSucPossibilityCount;
              else
                 SellSucPossibilityMid = 0;
         }
       if(UndefinedPossibility > Spread)
         {
           UndefinedSucPossibilityMid *= UndefinedSucPossibilityCount;
           UndefinedSucPossibilityCount ++;                 
           UndefinedSucPossibilityMid += UndefinedPossibility;
           if(UndefinedSucPossibilityCount != 0)
               UndefinedSucPossibilityMid /= UndefinedSucPossibilityCount;
           else
               UndefinedSucPossibilityMid = 0;
         }
     }
   if((UndefinedPossibilityQuality + SellPossibilityQuality + BuyPossibilityQuality)!= 0)
       PossibilityQuality = (SellPossibilityQuality + BuyPossibilityQuality) / 
       (UndefinedPossibilityQuality + SellPossibilityQuality + BuyPossibilityQuality);
   else             
       PossibilityQuality = 0;
   // �������� ��� �������� ��������
   if((UndefinedSucPossibilityQuality + SellSucPossibilityQuality + 
      BuySucPossibilityQuality)!= 0)          
       PossibilitySucQuality = (SellSucPossibilityQuality + BuySucPossibilityQuality) / 
                                (UndefinedSucPossibilityQuality + SellSucPossibilityQuality + 
                                BuySucPossibilityQuality);
   else             
       PossibilitySucQuality = 0;
   return (0);
  }
//+------------------------------------------------------------------+
//| ���������� ����������                                            |
//+------------------------------------------------------------------+
int DisplayStat()
  {
   if(ShowStat == true)
     {
       Print ("SellPossibilityMid*SellPossibilityQuality:", SellPossibilityMid*SellPossibilityQuality);
       Print ("BuyPossibilityMid*BuyPossibilityQuality:", BuyPossibilityMid*BuyPossibilityQuality);
       Print ("UndefinedPossibilityMid*UndefinedPossibilityQuality:", UndefinedPossibilityMid*UndefinedPossibilityQuality);
       Print ("UndefinedSucPossibilityQuality:", UndefinedSucPossibilityQuality);
       Print ("SellSucPossibilityQuality:", SellSucPossibilityQuality);
       Print ("BuySucPossibilityQuality:", BuySucPossibilityQuality);
       Print ("UndefinedPossibilityQuality:", UndefinedPossibilityQuality);
       Print ("SellPossibilityQuality:", SellPossibilityQuality);
       Print ("BuyPossibilityQuality:", BuyPossibilityQuality);
       Print ("UndefinedSucPossibilityMid:", UndefinedSucPossibilityMid);
       Print ("SellSucPossibilityMid:", SellSucPossibilityMid);
       Print ("BuySucPossibilityMid:", BuySucPossibilityMid);
       Print ("UndefinedPossibilityMid:", UndefinedPossibilityMid);
       Print ("SellPossibilityMid:", SellPossibilityMid);
       Print ("BuyPossibilityMid:", BuyPossibilityMid);
     }
   return (0);
  }   // 
//+------------------------------------------------------------------+
//|  ����������� ��������� ��� �������� �������                      |
//+------------------------------------------------------------------+
int CyberiaDecision()
  {
// ��������� ���������� �������
   CalculatePossibilityStat();
// ��������� ����������� ���������� ������
   CalculatePossibility(0);
   DisplayStat();
   return(Decision);     
  }
//+------------------------------------------------------------------+
//| ��������� ����������� �������� �����                             |
//+------------------------------------------------------------------+
int CalculateDirection()
  {
   DisableSellPipsator = false;
   DisableBuyPipsator = false;
   DisablePipsator = false;
   DisableSell = false;
   DisableBuy = false;
//----
   if(EnableCyberiaLogic == true)           
     {
       AskCyberiaLogic();
     }
   if(EnableMACD == true)
       AskMACD();
   if(EnableMA == true)
       AskMA();
   if(EnableReverceDetector == true)
       ReverceDetector();
   return (0);
  }
//+------------------------------------------------------------------+
//| ���� ����������� ��������� ������ �������������� �������         |
//+------------------------------------------------------------------+
int ReverceDetector ()
  {
   if((BuyPossibility > BuyPossibilityMid * ReverceIndex && BuyPossibility != 0 && 
      BuyPossibilityMid != 0) ||(SellPossibility > SellPossibilityMid * ReverceIndex && 
      SellPossibility != 0 && SellPossibilityMid != 0))
     {
       if(DisableSell == true)
           DisableSell = false;
       else
           DisableSell = true;
       if(DisableBuy == true)
           DisableBuy = false;
       else
           DisableBuy = true;
       //----
       if(DisableSellPipsator == true)
           DisableSellPipsator = false;
       else
           DisableSellPipsator = true;
       if(DisableBuyPipsator == true)
           DisableBuyPipsator = false;
       else
           DisableBuyPipsator = true;
     }
   return (0);
  }
//+------------------------------------------------------------------+
//| ���������� ������ �������� CyberiaLogic(C)                       |
//+------------------------------------------------------------------+
int AskCyberiaLogic()
  {
   //������������� ���������� ��� �������� �����
   /*DisableBuy = true;
   DisableSell = true;
   DisablePipsator = false;*/
   // ���� ����� ���������� �������� � �������� �����������
   if(ValuePeriod > ValuePeriodPrev)
     {
       if(SellPossibilityMid*SellPossibilityQuality > BuyPossibilityMid*BuyPossibilityQuality)
         {
           DisableSell = false;
           DisableBuy = true;
           DisableBuyPipsator = true;
           if(SellSucPossibilityMid*SellSucPossibilityQuality > 
              BuySucPossibilityMid*BuySucPossibilityQuality)
             {
               DisableSell = true;  
             }
         }
       if(SellPossibilityMid*SellPossibilityQuality < BuyPossibilityMid*BuyPossibilityQuality)
         {
           DisableSell = true;
           DisableBuy = false;
           DisableSellPipsator = true;
           if(SellSucPossibilityMid*SellSucPossibilityQuality < 
              BuySucPossibilityMid*BuySucPossibilityQuality)
             {
               DisableBuy = true;
             }
         }
     }
   // ���� ����� ������ ����������� - ������� �� ������ ������ ������!!!
   if(ValuePeriod < ValuePeriodPrev)
     {
      if(SellPossibilityMid*SellPossibilityQuality > BuyPossibilityMid*BuyPossibilityQuality)
         {
           DisableSell = true;
           DisableBuy = true;
         }
      if(SellPossibilityMid*SellPossibilityQuality < BuyPossibilityMid*BuyPossibilityQuality)
        {
          DisableSell = true;
          DisableBuy = true;
        }
     }
   // ���� ����� ��������������
   if(SellPossibilityMid*SellPossibilityQuality == BuyPossibilityMid*BuyPossibilityQuality)
     {
       DisableSell = true;
       DisableBuy = true;
       DisablePipsator=false;
     }
   // ��������� ����������� ������ �� �����
   if(SellPossibility > SellSucPossibilityMid * 2 && SellSucPossibilityMid > 0)
     {
       DisableSell = true;
       DisableSellPipsator = true;
     }
   // ��������� ����������� ������ �� �����
   if(BuyPossibility > BuySucPossibilityMid * 2 && BuySucPossibilityMid > 0 )
     {
       DisableBuy = true;
       DisableBuyPipsator = true;
     }
   if(ShowDirection == true)
     {
       if(DisableSell == true )
         {
           Print("������� �������������:", SellPossibilityMid*SellPossibilityQuality);
         }
       else
         {
           Print ("������� ���������:", SellPossibilityMid*SellPossibilityQuality);
         }
       //----
       if(DisableBuy == true )
         {
           Print ("������� �������������:", BuyPossibilityMid*BuyPossibilityQuality);
         }
       else
         {
           Print ("������� ���������:", BuyPossibilityMid*BuyPossibilityQuality);
         }
     }
   if(ShowDecision == true)
     {
       if(Decision == DECISION_SELL)
           Print("������� - ���������: ", DecisionValue);
       if(Decision == DECISION_BUY)
           Print("������� - ��������: ", DecisionValue);
       if(Decision == DECISION_UNKNOWN)
           Print("������� - ����������������: ", DecisionValue);
     }
   return (0);
  }
//+------------------------------------------------------------------+
//| ���������� ��������� MA                                          |
//+------------------------------------------------------------------+
int AskMA()
  {
   if(iMA(Symbol(), PERIOD_M1, ValuePeriod, 0 , MODE_EMA, PRICE_CLOSE, 0) > 
      iMA(Symbol(), PERIOD_M1, ValuePeriod, 0 , MODE_EMA, PRICE_CLOSE, 1))        
     {
       DisableSell = true;
       DisableSellPipsator = true;
     }
   if(iMA(Symbol(), PERIOD_M1, ValuePeriod, 0 , MODE_EMA, PRICE_CLOSE, 0) < 
      iMA(Symbol(), PERIOD_M1, ValuePeriod, 0 , MODE_EMA, PRICE_CLOSE, 1))        
     {
       DisableBuy = true;
       DisableBuyPipsator = true;
     }
   return (0);
  }
//+------------------------------------------------------------------+
//| ���������� ��������� MACD                                        |
//+------------------------------------------------------------------+
int AskMACD()
  {
   double DecisionIndex = 0;
   double SellIndex = 0;
   double BuyIndex = 0;
   double BuyVector = 0;
   double SellVector = 0;
   double BuyResult = 0;
   double SellResult = 0;
   DisablePipsator = false;
   DisableSellPipsator = false;
   DisableBuyPipsator = false;
   DisableBuy = false;
   DisableSell = false;
   DisableExitSell = false;
   DisableExitBuy = false;
   // ��������� ������
   for(int i = 0 ; i < MACDLevel ; i ++)
     {
       if(iMACD(Symbol(), MathPow( 2, i) , 2, 4, 1, PRICE_CLOSE, MODE_MAIN, 0) < 
          iMACD(Symbol(), MathPow( 2, i), 2, 4, 1, PRICE_CLOSE, MODE_MAIN, 1) )
         {
           SellIndex += iMACD(Symbol(), MathPow( 2, i), 2, 4, 1, PRICE_CLOSE, MODE_MAIN, 0);
         }
       if(iMACD(Symbol(), MathPow( 2, i), 2, 4, 1, PRICE_CLOSE, MODE_MAIN, 0) > 
          iMACD(Symbol(), MathPow( 2, i), 2, 4, 1, PRICE_CLOSE, MODE_MAIN, 1) )
         {
           BuyIndex += iMACD(Symbol(), MathPow( 2, i), 2, 4, 1, PRICE_CLOSE, MODE_MAIN, 0);
         }

     }
   if(SellIndex> BuyIndex)
     {
       DisableBuy = true;
       DisableBuyPipsator = true;
     }
   if(SellIndex < BuyIndex)
     {
       DisableSell = true;
       DisableSellPipsator = true;
     }
   return (0);
  }
//+------------------------------------------------------------------------+
//| ����� �������� ��� - (���������� ��������������� ����� ������� ��������|
//+------------------------------------------------------------------------+
int MoneyTrain()
  {
   if(FoundOpenedOrder == False)
     {
       // ������� ���������
       Disperce = (iHigh ( Symbol(), PERIOD_M1, 0) - iLow ( Symbol(), PERIOD_M1, 0));
       if(Decision == DECISION_SELL)
         {
           // *** ���������� � ������� �� ����������� �������� ����� ����� ***
           if((iClose( Symbol(), PERIOD_M1, 0) - iClose( Symbol(), PERIOD_M1, ValuePeriod)) / 
               MoneyTrainLevel >= SellSucPossibilityMid && SellSucPossibilityMid != 0 && 
               EnableMoneyTrain == true)
             {
               ModeSpread = ModeSpread + 1;
               // ������ ����-����
               if((Bid - SellSucPossibilityMid*StopLossIndex- ModeSpread * Point) > 
                  (Bid - ModeStopLevel* ModePoint- ModeSpread * Point))
                 {
                   StopLoss = Bid - ModeStopLevel* ModePoint- ModeSpread * Point - Disperce;
                 }
               else
                 {
                   if(SellSucPossibilityMid != 0)
                       StopLoss = Bid - SellSucPossibilityMid*StopLossIndex- 
                       ModeSpread * Point - Disperce;
                   else
                       StopLoss = Bid - ModeStopLevel* ModePoint- ModeSpread * Point - Disperce;
                 }

               if(BlockBuy == true)
                 {
                   return(0);
                 }
               StopLevel = StopLoss;
               Print ("StopLevel:", StopLevel);
               // ���������� ���������
               if(BlockStopLoss == true)
                   StopLoss = 0;                                                                            
               ticket = OrderSend(Symbol(), OP_BUY, Lots, Ask, SlipPage, StopLoss, 
                                  TakeProfit,"CyberiaTrader-AI-HB1",0,0,Blue);
               if(ticket > 0)
                 {
                   if(OrderSelect(ticket,SELECT_BY_TICKET,MODE_TRADES)) 
                       Print("������ ����� �� �������: ",OrderOpenPrice());
                 }
               else
                 {
                   Print("���� � �����: ������ �������� ������ �� �������: ",GetLastError());
                   PrintErrorValues();
                 }
               return (0);
             }
         }              
       if(Decision == DECISION_BUY)
         {
           // *** ���������� � ������� �� ����������� �������� ����� ����� ***
           if((iClose( Symbol(), PERIOD_M1, ValuePeriod) - iClose( Symbol(), PERIOD_M1, 0)) / 
               MoneyTrainLevel >= BuySucPossibilityMid && BuySucPossibilityMid != 0 && 
               EnableMoneyTrain == true)
             {
               ModeSpread = ModeSpread + 1;
               // ������ ����-����
               if((Ask + BuySucPossibilityMid*StopLossIndex+ ModeSpread* Point) < 
                  (Ask + ModeStopLevel* ModePoint+ ModeSpread * Point))
                 {
                   StopLoss = Ask + ModeStopLevel* ModePoint+ ModeSpread * Point+ Disperce;
                 }
               else
                 {
               if(BuySucPossibilityMid != 0)
                   StopLoss = Ask + BuySucPossibilityMid*StopLossIndex+ ModeSpread*Point + 
                              Disperce;
               else
                   StopLoss = Ask + ModeStopLevel* ModePoint+ ModeSpread * Point+ Disperce;
                 }
               // ���� �������� ������ ���������� ������
               if(BlockSell == true)
                 {
                   return(0);
                 }
               StopLevel = StopLoss;
               Print ("StopLevel:", StopLevel);
               // ���������� ���������
               if(BlockStopLoss == true)
                   StopLoss = 0;                                                                      
               ticket = OrderSend(Symbol(), OP_SELL, Lots, Bid, SlipPage, StopLoss, 
                                  TakeProfit, "CyberiaTrader-AI-HS1", 0, 0, Green);
               if(ticket > 0)
                 {
                   if(OrderSelect(ticket, SELECT_BY_TICKET, MODE_TRADES)) 
                       Print("������ ����� �� �������: ", OrderOpenPrice());
                 }
               else
                 {
                   Print("���� � �����: ������ �������� ������ �� �������: ",GetLastError());
                   PrintErrorValues();
                 }
               return (0);
             }   
         }            
     }
   return (0);
  }
//+------------------------------------------------------------------+
//| ���� � �����                                                     |
//+------------------------------------------------------------------+
int EnterMarket()
  {
// ���� ��� �������, �������
   if(Lots == 0)
     {
       return (0);
     }
// ������ � ����� ���� ��� ������� ������ �� �����
   if(ExitMarket == False)
     {
       // ------- ���� ��� �������� ������� - ������ � ����� ------------
       if(FoundOpenedOrder == False)
         {
           // ������� ���������
           Disperce = (iHigh(Symbol(), PERIOD_M1, 0) - iLow(Symbol(), PERIOD_M1, 0));
           if(Decision == DECISION_SELL)
             {
               // ���� ���� ������� ������ ������� �������� ������� �� ������������ ���������
               if(SellPossibility >= SellSucPossibilityMid)
                 {
                   // ������ ����-����
                   if((Ask + BuySucPossibilityMid*StopLossIndex + ModeSpread * Point) < 
                      (Ask + ModeStopLevel* ModePoint+ ModeSpread * Point))
                     {
                       StopLoss = Ask + ModeStopLevel* ModePoint+ ModeSpread * Point + Disperce;
                     }
                   else
                     {
                       if(BuySucPossibilityMid != 0)
                           StopLoss = Ask + BuySucPossibilityMid*StopLossIndex + 
                                      ModeSpread * Point+ Disperce;
                       else
                           StopLoss = Ask + ModeStopLevel* ModePoint+ ModeSpread * Point + 
                                      Disperce;
                     }
                   // ���� �������� ������ ���������� ������
                   if(DisableSell == true)
                     {
                       return(0);
                     }
                   if(BlockSell == true)
                     {
                       return(0);
                     }
                   StopLevel = StopLoss;
                   Print ("StopLevel:", StopLevel);
                   // ���������� ���������
                   if(BlockStopLoss == true)
                       StopLoss = 0;                                                                      
                   ticket = OrderSend(Symbol(), OP_SELL, Lots, Bid, SlipPage, StopLoss, 
                            TakeProfit, "CyberiaTrader-AI-LS1", 0, 0, Green);
                   if(ticket > 0)
                     {
                       if(OrderSelect(ticket, SELECT_BY_TICKET, MODE_TRADES)) 
                           Print("������ ����� �� �������: ",OrderOpenPrice());
                     }
                   else
                     {
                       Print("���� � �����: ������ �������� ������ �� �������: ",GetLastError());
                       PrintErrorValues();
                     }
                   // ��������� ���������� �������� �������
                   return (0);
                 }
             }
           if(Decision == DECISION_BUY)
             {
               // ���� ���� ������� ������ ������� �������� ������� �� ������������ ���������
               if(BuyPossibility >= BuySucPossibilityMid)
                 {
                   // ������ ����-����
                   if((Bid - SellSucPossibilityMid*StopLossIndex- ModeSpread* Point) > 
                      (Bid - ModeStopLevel* ModePoint- ModeSpread* Point))
                     {
                       StopLoss = Bid - ModeStopLevel* ModePoint- ModeSpread* Point - Disperce;
                     }
                   else
                     {
                       if(SellSucPossibilityMid != 0)
                           StopLoss = Bid - SellSucPossibilityMid*StopLossIndex- 
                                      ModeSpread* Point- Disperce;
                       else
                           StopLoss = Bid - ModeStopLevel* ModePoint- ModeSpread* Point- 
                                      Disperce;
                     }
                   // ���� �������� ������ ���������� �������
                   if(DisableBuy == true)
                     {
                       return(0);
                     }
                   if(BlockBuy == true)
                     {
                       return(0);
                     }
                   StopLevel = StopLoss;
                   Print("StopLevel:", StopLevel);
                   // ���������� ���������
                   if(BlockStopLoss == true)
                       StopLoss = 0;                                                                      
                   ticket = OrderSend(Symbol(), OP_BUY, Lots, Ask, SlipPage, StopLoss, 
                            TakeProfit, "CyberiaTrader-AI-LB1", 0, 0, Blue);
                   if(ticket > 0)
                     {
                      if(OrderSelect(ticket, SELECT_BY_TICKET, MODE_TRADES)) 
                          Print("������ ����� �� �������: ",OrderOpenPrice());
                     }
                   else
                     {
                       Print("���� � �����: ������ �������� ������ �� �������: ",GetLastError());
                       PrintErrorValues();
                     }
                   return (0);
                 }
             }
         }
// ---------------- ����� ����� � ����� ----------------------        
     }     
   return (0);
  }   
//+------------------------------------------------------------------+
//| ����� �������� �������                                           |
//+------------------------------------------------------------------+
int FindSymbolOrder()
  {
   FoundOpenedOrder = false;
   total = OrdersTotal();
   for(cnt = 0; cnt < total; cnt++)
     {
       OrderSelect(cnt, SELECT_BY_POS, MODE_TRADES);
       // ���� ����� �� ����� ������
       if(OrderSymbol() == Symbol())
         {
           FoundOpenedOrder = True;
           break;
         }
       else
         {
           StopLevel = 0;
           StopLoss = 0;
         }
     }
   return (0);
  }
//+------------------------------------------------------------------+
//| �������� �� �������� ����������                                  |
//+------------------------------------------------------------------+
int RunPipsator()
  {
   int i = 0;
   FindSymbolOrder();
   // ������ � ����� ���� ��� ������� ������ �� �����
   // ������� ���������
   if(Lots == 0)
       return (0);
   Disperce = 0;
   if(ExitMarket == False)
     {
       // ---------- ���� ��� �������� ������� - ������ � ����� ----------
       if(FoundOpenedOrder == False)
         {
           Disperce = 0;
           DisperceMax = 0;
           // ������� ������������ ���������
           for(i = 0 ; i < ValuePeriod ; i ++)
             {
               Disperce = (iHigh( Symbol(), PERIOD_M1, i + 1) - 
                           iLow( Symbol(), PERIOD_M1, i + 1));                                
               if(Disperce > DisperceMax)
                   DisperceMax = Disperce;                             
             }
           Disperce = DisperceMax  * StopLossIndex;
           if( Disperce == 0 )
             {
               Disperce = ModeStopLevel * Point;
             }
           for(i = 0 ; i < ValuePeriod ; i ++)
             {
               // �������� ��������� ��������� �� �������
               if((Bid - iClose( Symbol(), PERIOD_M1, i + 1)) > 
                  SellSucPossibilityMid * (i + 1) && 
                  SellSucPossibilityMid != 0 && DisablePipsator == false && 
                  DisableSellPipsator == false)
                 {
                   // ������ ����-����
                   if((Ask + ModeSpread * Point + Disperce) < 
                      (Ask + ModeStopLevel* ModePoint + ModeSpread * Point))
                     {
                       StopLoss = Ask + ModeStopLevel* ModePoint+ ModeSpread * Point + Point;
                     }
                   else
                     {
                       if(BuySucPossibilityMid != 0)
                           StopLoss = Ask + ModeSpread * Point+ Disperce + Point;
                       else
                         StopLoss = Ask + ModeStopLevel* ModePoint+ ModeSpread * Point + Point;
                     }
                   // ���� �������� ������ ���������� ������
                   if(BlockSell == true)
                     {
                       return(0);
                     }
                   // ���� �������� ������ ���������� ������
                   if(DisableSell == true)
                     {
                       return(0);
                     }
                   StopLevel = StopLoss;
                   Print("StopLevel:", StopLevel);
                                      // ���������� ���������
                   if(BlockStopLoss == true)
                       StopLoss = 0;
                   ticket = OrderSend(Symbol(), OP_SELL, Lots, Bid, SlipPage, StopLoss, 
                            TakeProfit, "CyberiaTrader-AI-PS1", 0, 0, Green);
                   if(ticket > 0)
                     {
                       if(OrderSelect(ticket, SELECT_BY_TICKET, MODE_TRADES)) 
                           Print("������ ����� �� �������: ",OrderOpenPrice());
                     }
                   else
                     {
                       Print("���� � �����: ������ �������� ������ �� �������: ",GetLastError());
                       PrintErrorValues();
                     }
                   return (0);
                 }
               // �������� ��������� ��������� �� �������
               if((iClose(Symbol(), PERIOD_M1, i + 1) - Bid) > BuySucPossibilityMid *(i + 1) && 
                   BuySucPossibilityMid != 0 && DisablePipsator == False && 
                   DisableBuyPipsator == false)
                 {
                   // ������ ����-����
                   if((Bid -  ModeSpread * Point - Disperce) > 
                      (Bid - ModeStopLevel* ModePoint- ModeSpread * Point))
                     {
                       StopLoss = Bid - ModeStopLevel* ModePoint- ModeSpread * Point - Point;
                     }
                   else
                     {
                       if(SellSucPossibilityMid != 0)
                           StopLoss = Bid - ModeSpread * Point- Disperce- Point;
                       else
                           StopLoss = Bid - ModeStopLevel* ModePoint- ModeSpread * Point - Point;
                     }
                   // ���� �������� ������ ���������� 
                   if(DisableBuy == true)
                     {
                       return(0);
                     }
                   if(BlockBuy == true)
                     {
                       return(0);
                     }
                   StopLevel = StopLoss;
                   Print("StopLevel:", StopLevel);
                   // ���������� ���������
                   if(BlockStopLoss == true)
                       StopLoss = 0;                                                                            
                   ticket = OrderSend(Symbol(), OP_BUY, Lots, Ask, SlipPage, StopLoss, 
                            TakeProfit, "CyberiaTrader-AI-PB1", 0, 0, Blue);
                   if(ticket > 0)
                     {
                       if(OrderSelect(ticket, SELECT_BY_TICKET, MODE_TRADES)) 
                           Print("������ ����� �� �������: ",OrderOpenPrice());
                     }
                   else
                     {
                       Print("���� � �����: ������ �������� ������ �� �������: ",GetLastError());
                       PrintErrorValues();
                     }
                   return (0);
                 }   
             }// ����� ������������ �����           
         }
     }
   return (0);
  }
//+------------------------------------------------------------------+
//| ����� �� �����                                                   |
//+------------------------------------------------------------------+
int ExitMarket ()
  {
   //FindSymbolOrder();
   // -------------------- ��������� �������� ������� ----------------
   if(FoundOpenedOrder == True) // ���� ���� �������� ����� �� ���� ������
     {
       if(OrderType()==OP_BUY) // ���� ��������� ����� �� ������������ ������
         {
           // �������� ������, ���� �� ������ ������ ����-����
           if(Bid <= StopLevel && DisableShadowStopLoss == false && StopLevel != 0)
             {
               OrderClose(OrderTicket(),OrderLots(),Bid ,SlipPage,Violet); // ��������� �����
               return(0);
             }
           if(DisableExitBuy == true)
               return (0);
           // �������� ���������
           if((OrderOpenPrice() < Bid))
             {
               // ��������� �����
               OrderClose(OrderTicket(), OrderLots(), Bid , SlipPage, Violet); // ��������� �����
               return(0);
             }
           // �� ������� �� �����, ���� ����� ����, ���������� �� �������
           if((iClose( Symbol(), PERIOD_M1, 0) - iClose( Symbol(), PERIOD_M1, 1)) >= 
               SellSucPossibilityMid * 4 && SellSucPossibilityMid > 0)
               return(0);

           // �������� ������ �� ���������� ����������� �������� �������
           if((OrderOpenPrice() < Bid) && (Bid - OrderOpenPrice() >= 
              SellSucPossibilityMid) && (SellSucPossibilityMid > 0) )
             {
               // ��������� �����
               OrderClose(OrderTicket(), OrderLots(), Bid , SlipPage, Violet); // ��������� �����
               return(0);
             }
           // �������� ������ �� ���������� ����������� �������� �������
           if((OrderOpenPrice() < Bid) && (Bid - OrderOpenPrice() >= 
              BuySucPossibilityMid) && (BuySucPossibilityMid > 0) )
             {
               // ��������� �����
               OrderClose(OrderTicket(), OrderLots(), Bid , SlipPage, Violet); // ��������� �����
               return(0);
             }

           if(Decision == DECISION_SELL)
             {
               // ���� ���� ������� ������ ���� ������� - ������ ����� ������� (� ������ ����� ������)
               //if ( OrderOpenPrice() < Bid - SlipPage * Point )
               if( OrderOpenPrice() < Bid)
                 {
                   // ���� ���� ������� ������ ������� �������� ������� �� ������������ ���������
                   if(SellPossibility >= SellPossibilityMid - Point)
                     {
                       OrderClose(OrderTicket(), OrderLots(), Bid , SlipPage, Violet); // ��������� �����
                       return(0);
                     }
                 }
             }
           // �������� ������ �� ���������� ��������������� ��������
           if((OrderOpenPrice() < Bid) && (Bid - OrderOpenPrice() >= UndefinedPossibilityMid) )
             {
               // ��������� �����
               OrderClose(OrderTicket(), OrderLots(), Bid , SlipPage, Violet); // ��������� �����
               return(0);
             }

           //��������� ����� �� ��� ������� � ��������� ������
           if(Decision == DECISION_BUY)
             {
               return(0);
             }
         }
       if(OrderType() == OP_SELL) // ���� ��������� ����� �� ������������ ������
         {
           // �������� ������, ���� �� ������ ������ ����-����
           if(Ask >= StopLevel && DisableShadowStopLoss == false && StopLevel != 0)
             {
               OrderClose(OrderTicket(), OrderLots(), Ask , SlipPage, Violet); // ��������� �����
               return(0);
             }
           if(DisableExitSell == true)
               return (0);
           // �������� ���������
           if((OrderOpenPrice() > Ask))
             {
               OrderClose(OrderTicket(), OrderLots(), Ask, SlipPage, Violet); // ��������� �����
               return(0);
             }


           // �� ������� �� �����, ���� ����� ����, ���������� �� �������
           if((iClose( Symbol(), PERIOD_M1, 1) - iClose( Symbol(), PERIOD_M1, 0)) >= BuySucPossibilityMid * 4 && BuySucPossibilityMid > 0)
            return (0);
           // �������� ������ �� ����� ���������� ����������� �������� �������
           if((OrderOpenPrice() > Ask) && (OrderOpenPrice() - Ask) >= 
               BuySucPossibilityMid && BuySucPossibilityMid > 0)
             {
               // ��������� �����
               OrderClose(OrderTicket(), OrderLots(), Ask, SlipPage, Violet); // ��������� �����
               return(0);
             }

           // �������� ������ �� ����� ���������� ����������� �������� �������
           if((OrderOpenPrice() > Ask) && (OrderOpenPrice() - Ask) >= 
              SellSucPossibilityMid && SellSucPossibilityMid > 0)
             {
               // ��������� �����
               OrderClose(OrderTicket(), OrderLots(), Ask, SlipPage, Violet); // ��������� �����
               return(0);
             }

           if (Decision == DECISION_BUY )
             {
               // ���� ���� ������� ������ ���� ������� - ������ ����� ������� (� ������ ����� ������)
               if(OrderOpenPrice() > Ask)
                 {
                   // ���� ���� ������� ������ ������� �������� ������� �� ������������ ���������
                   if(BuyPossibility >= BuyPossibilityMid - Point)
                     {
                       OrderClose(OrderTicket(), OrderLots(), Ask, SlipPage, Violet); // ��������� �����
                       return(0);
                     }
                 }

             }
           // �������� ������ �� ���������� ��������������� ��������
           if((OrderOpenPrice() > Ask) && (OrderOpenPrice() - Ask) >= UndefinedPossibilityMid)
             {
               // ��������� �����
               OrderClose(OrderTicket(), OrderLots(), Ask, SlipPage, Violet); // ��������� �����
               return(0);
             }
           //��������� ����� �� ��� ������� � ��������� ������
           if(Decision == DECISION_SELL)
             {
               return (0);
             }

         }
     }
 // --------------------- ����� ��������� �������� ������� -----------
 //  ValuePeriodPrev = ValuePeriod;
   return (0);
  }   
//+--------------------------------------------------------------------------+
//| ��������� �������� ������ � ������� ������������� ��� ��������� ���������|
//+--------------------------------------------------------------------------+
int SaveStat()
  {
   BidPrev = Bid;
   AskPrev = Ask;
   ValuePeriodPrev = ValuePeriod;
   return (0);
  }
//+------------------------------------------------------------------+
//| ��������                                                         |
//+------------------------------------------------------------------+
int Trade ()
  {
   // �������� ���������
   // ���� �������� ������
   FindSymbolOrder();
   CalculateDirection();
   AutoStopLossIndex();
//---- ���� �������� ������� �� ������� ���, �������� ���� � �����
//---- �������� - ����� ������ ���� ������� ������������ ���������� ����� � ����� (MoneyTrain, LogicTrading, Pipsator)
   if(FoundOpenedOrder == false)
     {
       if(EnableMoneyTrain == true)
           MoneyTrain();
       if(EnableLogicTrading == true)
           EnterMarket();
       if(DisablePipsator == false)
           RunPipsator();           
     }
   else
     {
       ExitMarket();
     }
//---- ����� ��������� �����/������ �� �����
   return(0);
  }
//+------------------------------------------------------------------+
//| �������� � ����� ������ �����                                    |
//+------------------------------------------------------------------+
int AccountStatus()
  {
   if(ShowAccountStatus == True )
     {
       Print ("AccountBalance:", AccountBalance());
       Print ("AccountCompany:", AccountCompany());
       Print ("AccountCredit:", AccountCredit());
       Print ("AccountCurrency:", AccountCurrency());
       Print ("AccountEquity:", AccountEquity());
       Print ("AccountFreeMargin:", AccountFreeMargin());
       Print ("AccountLeverage:", AccountLeverage());
       Print ("AccountMargin:", AccountMargin());
       Print ("AccountName:", AccountName());
       Print ("AccountNumber:", AccountNumber());
       Print ("AccountProfit:", AccountProfit());
     }    
   return ( 0 );
  }
//+------------------------------------------------------------------+
//| ����� ������ ������� - ����� ������� �������������               |
//+------------------------------------------------------------------+
int FindSuitablePeriod()
  {
   double SuitablePeriodQuality = -1 *ValuesPeriodCountMax*ValuesPeriodCountMax;
   double SuitablePeriod = 0;
   int i; // ���������� ��� ������� ��������
// ���������� ������������� ��������. i - ������ �������
   for(i = 0 ; i < ValuesPeriodCountMax ; i ++ )
     {
       ValuePeriod = i + 1;
      // �������� ��������� ������� ����� � ��� �� ������� ��� ������� � ������ � ������ �������
       ValuesPeriodCount = ValuePeriod * 5; 
       init();           
       CalculatePossibilityStat ();
       if(PossibilitySucQuality > SuitablePeriodQuality)
         {
           SuitablePeriodQuality = PossibilitySucQuality;
           //Print ("PossibilitySucQuality:", PossibilitySucQuality:);
           SuitablePeriod = i + 1;
         }
     }
   ValuePeriod = SuitablePeriod;
   init();
   // �������� ������ �������������
   if(ShowSuitablePeriod == True)
     {
       Print("������ �������������:", SuitablePeriod, " ����� � ������������:", 
       SuitablePeriodQuality );
     }
   return(SuitablePeriod);
  }
//+------------------------------------------------------------------+
//|�������������� ��������� ������ ����-����                         |
//+------------------------------------------------------------------+
int AutoStopLossIndex()
  {
   if(AutoStopLossIndex == true)
     {
       StopLossIndex = ModeSpread;
     }
   return(0);
  }
//+------------------------------------------------------------------+
//|����� ������ ��� ����� � �����                                    |
//+------------------------------------------------------------------+
int PrintErrorValues()
  {
   Print("ErrorValues:Symbol=", Symbol(),",Lots=",Lots, ",Bid=", Bid, ",Ask=", Ask,
         ",SlipPage=", SlipPage, "StopLoss=",StopLoss,",TakeProfit=", TakeProfit);
   return (0);
  }   
//+------------------------------------------------------------------+
//| expert start function (��������)                                 |
//+------------------------------------------------------------------+
int start()
  {
   GetMarketInfo();
   CyberiaLots();
   CalculateSpread();
   FindSuitablePeriod();
   CyberiaDecision();
   Trade();
   SaveStat();
   return(0);
  }


