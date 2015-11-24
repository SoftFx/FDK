#property show_inputs

#include <NamedPipeClient.mqh>
#include <NamedPipeServer.mqh>

#define MAX_AMOUNTSYMBOLS 150 // максимальное количество аггрегированных символов

#define FONT_SIZE 8

#define PAUSE 10

#define WITHOUT_BID 0
#define WITHOUT_ASK 1
#define WITHOUT_LP  2
#define WITH_ALL    3

extern string ServerNames_Comment = "Соответствующие названия Pipe-серверов, заданные через запятую.";
extern string ServerNames = "Arbitrage";
//extern string ServerNames = "Arbitrage, Arbitrage2";

extern string ShowLPMeta_Comment = "Показывать или нет данные LP-меты.";
extern bool ShowLPMeta = TRUE;
extern string LPOff_Comment = "Соответствующие названия внутренних LP (заданные через запятую), которые не будут учитываться.";
// extern string LPOff = "Renesource, Alfa";
extern string LPOff = "";
extern string LPNames_Comment = "Соответствующие названия внешних LP, заданные через запятую.";
extern string LPNames = "";
//extern string LPNames = "ATC, Pepper-RAZOR, Pepper-EDGE, RVD-Integral, RVD-Currenex, FXOPEN_Meta";
extern string FilterTime_Comment = "Максимальное время (в секундах) жизни котировки.";
extern int FilterTime = 180;  // Если котировка не обновлялась больше этого времени (в секунду), то она не учитывается
extern string XStep_Comment = "Расстояния в пикселах между стаканами по абциссе.";
extern int XStep = 310; // 1900x1200
extern string YStep_Comment = "Расстояния в пикселах между стаканами по ординате.";
extern int YStep = 180;
extern string AmountX_Comment = "Количество стаканов по абциссе.";
// extern int AmountX = 6; // 1280x768
extern int AmountX = 6; // 1900x1200

extern color ColorBid = Green;
extern color ColorAsk = Red;
extern color ColorPositiveSpread = Yellow;
extern color ColorNegativeSpread = Purple;
extern color ColorArbitragePrice = White;
extern color ColorLPOutBid = Lime;
extern color ColorLPOutAsk = Pink;

int NullTime; // для миллисекунд

string ServerName[MAX_AMOUNTSYMBOLS]; // Названия Pipe-серверов

string LP[MAX_AMOUNTSYMBOLS]; // Названия LP
string LP_Add[MAX_AMOUNTSYMBOLS]; // Названия внешних LP

string SymbolNames[]; // Названия аггрегированных символов
string Symbols[MAX_AMOUNTSYMBOLS][MAX_AMOUNTSYMBOLS]; // [i][j] названия символа для i-го LP j-го аггрегированного символа

int TimeAsks[][MAX_AMOUNTSYMBOLS];
int TimeBids[][MAX_AMOUNTSYMBOLS];

int TimeAsksMs[][MAX_AMOUNTSYMBOLS];
int TimeBidsMs[][MAX_AMOUNTSYMBOLS];

int NumSymbols[MAX_AMOUNTSYMBOLS][MAX_AMOUNTSYMBOLS]; // [i][j] индекс символа в SymbolsNames[] для i-го LP_Add j-го номера пришедшего символа
int TickCounts[MAX_AMOUNTSYMBOLS][MAX_AMOUNTSYMBOLS]; //  наибольший TickCount для i-го LP_Add j-го аггрегированного символа

double Bids[MAX_AMOUNTSYMBOLS][MAX_AMOUNTSYMBOLS], Asks[MAX_AMOUNTSYMBOLS][MAX_AMOUNTSYMBOLS];

int SumTimeBid[][MAX_AMOUNTSYMBOLS], SumTimeAsk[][MAX_AMOUNTSYMBOLS]; // сколько времени (в текущем часу) соответствующая цена была наилучшей
bool BestBid[][MAX_AMOUNTSYMBOLS], BestAsk[][MAX_AMOUNTSYMBOLS]; // TRUE - на предыдущей итерации соответствующая цена была наилучшей

int HourTime = 0; // Количество мс, прошедших с начала текущего часа.

int ProfitWithoutBid[][MAX_AMOUNTSYMBOLS], ProfitWithoutAsk[][MAX_AMOUNTSYMBOLS], ProfitWithoutLP[][MAX_AMOUNTSYMBOLS], ProfitAll[];
int AmountWithoutBid[][MAX_AMOUNTSYMBOLS], AmountWithoutAsk[][MAX_AMOUNTSYMBOLS], AmountWithoutLP[][MAX_AMOUNTSYMBOLS], AmountAll[];

void SendMessage( string Message )
{
  int Amount = ArraySize(ServerName);

  for (int i = 0; i < Amount; i++)
    SendPipeMessage(ServerName[i], Message, 0);

  return;
}

void SortStrings( string &Str[] )
{
  string Min;
  int jMin, Size = ArraySize(Str);

  for (int i = 0; i < Size - 1; i++)
  {
    Min = Str[i];
    jMin = i;

    for (int j = i + 1; j < Size; j++)
      if (Str[j] < Min)
      {
        Min = Str[j];
        jMin = j;
      }

    if (jMin > i)
    {
      Str[jMin] = Str[i];
      Str[i] = Min;
    }
  }

  return;
}

int SymbolsList( string &Symbols[] )
{
   int Offset, SymbolsNumber;

   int hFile = FileOpenHistory("symbols.sel", FILE_BIN|FILE_READ);
   SymbolsNumber = (FileSize(hFile) - 4) / 128;
   Offset = 116;

   ArrayResize(Symbols, SymbolsNumber);

   FileSeek(hFile, 4, SEEK_SET);

   for(int i = 0; i < SymbolsNumber; i++)
   {
      Symbols[i] = FileReadString(hFile, 12);
      FileSeek(hFile, Offset, SEEK_CUR);
   }

   FileClose(hFile);

   return(SymbolsNumber);
}

//+------------------------------------------------------------------+
//| Функция возвращает расшифрованное название символа               |
//+------------------------------------------------------------------+
string SymbolDescription(string SymbolName)
{
   string SymbolDescription = "";

// Открываем файл с описанием символов

   int hFile = FileOpenHistory("symbols.raw", FILE_BIN|FILE_READ);
   if(hFile < 0) return("");

// Определяем количество символов, зарегистрированных в файле

   int SymbolsNumber = FileSize(hFile) / 1936;

// Ищем расшифровку символа в файле

   for(int i = 0; i < SymbolsNumber; i++)
   {
      if(FileReadString(hFile, 12) == SymbolName)
      {
         SymbolDescription = FileReadString(hFile, 64);

         break;
      }
      FileSeek(hFile, 1924, SEEK_CUR);
   }

   FileClose(hFile);

   return(SymbolDescription);
}

string GetLPName( string Symb )
{
  int Pos;
  string StrTmp = " quotes of ";

  Symb = SymbolDescription(Symb);
  Pos = StringFind(Symb, StrTmp);

  Symb = StringSubstr(Symb, Pos + StringLen(StrTmp));

  return(Symb);
}

string GetSymbolName( string Symb )
{
  int Pos;
  string StrTmp = " quotes of ";

  Symb = SymbolDescription(Symb);
  Pos = StringFind(Symb, StrTmp);

  Symb = StringSubstr(Symb, 0, Pos);

  return(Symb);
}

bool AskSymbol( string Symb )
{
   return(StringGetChar(Symb, StringLen(Symb) - 1) == 'A');
}

int SetSymbols()
{
  int j, k, AmountLP = 0, AmountSymbName = 0;
  string LPName, SymbName;
  int AmountSymbols = SymbolsList(SymbolNames);
  int AmountLP_Add = SetLP_Add();
  string LP_Off[MAX_AMOUNTSYMBOLS];
  int AmountLP_Off = StrToStringS(LPOff, ",", LP_Off);

  if (LPOff == "")
    AmountLP_Off = 0;

  ArrayResize(LP_Off, AmountLP_Off);

  SortStrings(SymbolNames);

  for (j = 0; j < MAX_AMOUNTSYMBOLS; j++)
    for (k = 0; k < MAX_AMOUNTSYMBOLS; k++)
    {
      Symbols[j][k] = "";

      Bids[j][k] = 0;
      Asks[j][k] = 0;

      NumSymbols[j][k] = -1;
      TickCounts[j][k] = 0;
    }

  for (int i = 0; i < AmountSymbols; i++)
    if (!AskSymbol(SymbolNames[i]))
    {
      LPName = GetLPName(SymbolNames[i]);
      SymbName = GetSymbolName(SymbolNames[i]);

      for (j = 0; j < AmountLP_Off; j++)
        if (LP_Off[j] == LPName)
          break;

      if (j < AmountLP_Off)
        continue;

      for (j = 0; j < AmountLP; j++)
        if (LP[j] == LPName)
          break;

      for (k = 0; k < AmountSymbName; k++)
        if (SymbolNames[k] == SymbName)
          break;

      Symbols[j][k] = SymbolNames[i];

      if (j == AmountLP)
      {
        LP[j] = LPName;
        AmountLP++;
      }

      if (k == AmountSymbName)
      {
        SymbolNames[k] = SymbName;
        AmountSymbName++;
      }
   }

  ArrayResize(Symbols, AmountLP);
  ArrayResize(NumSymbols, AmountLP_Add);
  ArrayResize(TickCounts, AmountLP_Add);

  for (j = 0; j < AmountLP_Add; j++)
  {
    LP[AmountLP] = LP_Add[j];

    AmountLP++;
  }

  ArrayResize(LP, AmountLP);
  ArrayResize(SymbolNames, AmountSymbName);

  ArrayResize(Bids, AmountLP);
  ArrayResize(Asks, AmountLP);

  ArrayResize(TimeBids, AmountLP);
  ArrayResize(TimeAsks, AmountLP);

  ArrayResize(TimeBidsMs, AmountLP);
  ArrayResize(TimeAsksMs, AmountLP);

  ArrayResize(SumTimeBid, AmountLP);
  ArrayResize(SumTimeAsk, AmountLP);

  ArrayResize(BestBid, AmountLP);
  ArrayResize(BestAsk, AmountLP);

  ArrayResize(ProfitWithoutBid, AmountLP);
  ArrayResize(ProfitWithoutAsk, AmountLP);
  ArrayResize(ProfitWithoutLP, AmountLP);
  ArrayResize(ProfitAll, AmountSymbName);

  ArrayResize(AmountWithoutBid, AmountLP);
  ArrayResize(AmountWithoutAsk, AmountLP);
  ArrayResize(AmountWithoutLP, AmountLP);
  ArrayResize(AmountAll, AmountSymbName);

  return(AmountSymbName);
}

string GetStr( string &Str, string Razdelitel )
{
  int Pos = StringFind(Str, ",");
  string StrRes = StringSubstr(Str, 0, Pos);

  Str = StringSubstr(Str, Pos + 1);

  return(StrRes);
}

int GetMessageData( string Message, int &Num, string &Symb, double &PriceBid, double &PriceAsk, int &TickCount )
{
  int i, AmountLP_Add = ArraySize(LP_Add);
  string LPName = GetStr(Message, ",");

  for (i = 0; i < AmountLP_Add; i++)
    if (LPName == LP_Add[i])
      break;

  if (i < AmountLP_Add)
  {
    Num = StrToInteger(GetStr(Message, ","));
    Symb = GetStr(Message, ",");
    PriceBid = StrToDouble(GetStr(Message, ","));
    PriceAsk = StrToDouble(GetStr(Message, ","));
    TickCount = StrToInteger(GetStr(Message, ","));
  }

  return(i);
}

void GetPrices_Add()
{
  static bool ConnectedLP[MAX_AMOUNTSYMBOLS];
  string arrMessages[];
  int Num, TickCount, LPNum;
  string Symb;
  double PriceBid, PriceAsk;
  int i, j, TmpTime;
  string StrComment = WindowExpertName();
  int MessagesRetrieved = CheckForPipeMessages(arrMessages);
  int AmountSymbols = ArraySize(SymbolNames);
  int AmountLP_Add = ArraySize(LP_Add);
  int AmountLP = ArraySize(LP) - AmountLP_Add;

  for (i = 0; i < AmountLP_Add; i++)
    ConnectedLP[i] = FALSE;

  if (MessagesRetrieved > 0)
    for (int k = 0; k < MessagesRetrieved; k++)
    {
      i = GetMessageData(arrMessages[k], Num, Symb, PriceBid, PriceAsk, TickCount);

      if (i < AmountLP_Add)
      {
        ConnectedLP[i] = TRUE;

        j = NumSymbols[i][Num];

        if (j < 0)
        {
          for (j = 0; j < AmountSymbols; j++)
            if (Symb == SymbolNames[j])
              break;

          NumSymbols[i][Num] = j;
        }

        if (j < AmountSymbols)
          if (TickCounts[i][j] < TickCount)
          {
            TickCounts[i][j] = TickCount;

            TmpTime = TickCount - NullTime;

            i += AmountLP;

            if (Bids[i][j] != PriceBid)
            {
              Bids[i][j] = PriceBid;

              TimeBids[i][j] = TimeCurrent() + TmpTime / 1000;
              TimeBidsMs[i][j] = TmpTime % 1000;

              if (TmpTime < 0)
              {
                TimeBids[i][j]--;
                TimeBidsMs[i][j] += 1000;
              }
            }

            if (Asks[i][j] != PriceAsk)
            {
              Asks[i][j] = PriceAsk;

              TimeAsks[i][j] = TimeCurrent() + TmpTime / 1000;
              TimeAsksMs[i][j] = TmpTime % 1000;

              if (TmpTime < 0)
              {
                TimeAsks[i][j]--;
                TimeAsksMs[i][j] += 1000;
              }
            }
          }
      }
    }

  for (i = AmountLP; i < AmountLP + AmountLP_Add; i++)
    for (j = 0; j < AmountSymbols; j++)
    {
      if (TimeCurrent() - TimeBids[i][j] > FilterTime)
        Bids[i][j] = 0;

      if (TimeCurrent() - TimeAsks[i][j] > FilterTime)
        Asks[i][j] = 0;
    }


  for (i = 0; i < AmountLP_Add; i++)
    if (ConnectedLP[i])
      StrComment = StrComment + "\n" + LP_Add[i] + " is connected.";
    else
      StrComment = StrComment + "\n" + LP_Add[i] + " is not connected.";


  static int PrevTime = 0;
  int NewTime = GetTickCount();

  StrComment = StrComment + "\n" + DoubleToStr(NewTime - PrevTime, 0) + "ms.";
  PrevTime = NewTime;

  Comment(StrComment);

  return;
}

void GetPrices()
{
  static int PrevTime = 0;
  double Price;
  int AmountSymbols = ArraySize(SymbolNames);
  int AmountLP_Add = ArraySize(LP_Add);
  int AmountLP = ArraySize(LP) - AmountLP_Add;

  RefreshRates();

  if (PrevTime != TimeCurrent())
  {
    PrevTime = TimeCurrent();
    NullTime = GetTickCount();
  }

  for (int i = 0; i < AmountLP; i++)
    for (int j = 0; j < AmountSymbols; j++)
      if (Symbols[i][j] == "")
      {
        Bids[i][j] = 0;
        Asks[i][j] = 0;
      }
      else if (PrevTime - MarketInfo(Symbols[i][j], MODE_TIME) > FilterTime)
      {
        Bids[i][j] = 0;
        Asks[i][j] = 0;
      }
      else
      {
        if (ShowLPMeta)
          Price = MarketInfo(Symbols[i][j], MODE_BID);
        else
          Price = 0;

        if (Bids[i][j] != Price)
        {
          Bids[i][j] = Price;

          TimeBids[i][j] = MarketInfo(Symbols[i][j], MODE_TIME);
          TimeBidsMs[i][j] = (GetTickCount() - NullTime) % 1000;
        }

        if (ShowLPMeta)
          Price = MarketInfo(Symbols[i][j], MODE_ASK);
//          Price = MarketInfo(Symbols[i][j] + "A", MODE_BID);
        else
          Price = 0;

        if (Asks[i][j] != Price)
        {
          Asks[i][j] = Price;

          TimeAsks[i][j] = MarketInfo(Symbols[i][j], MODE_TIME);
          TimeAsksMs[i][j] = (GetTickCount() - NullTime) % 1000;
        }
      }

  if (AmountLP_Add > 0)
    GetPrices_Add();

  return;
}

void CreateLabel( string Name, int X, int Y )
{
  ObjectCreate(Name, OBJ_LABEL, 0, 0, 0);
  ObjectSet(Name, OBJPROP_XDISTANCE, X);
  ObjectSet(Name, OBJPROP_YDISTANCE, Y);

  return;
}

void CreateMarketDepth( int Num, int X, int Y )
{
  int i, AmountLP = ArraySize(LP);
  string Name = WindowExpertName() + "_" + SymbolNames[Num] + "_";

  for (i = 0; i < AmountLP; i++)
  {
    CreateLabel(Name + "Ask_" + i, X, Y);

    Y += FONT_SIZE * 1.5;
  }

  CreateLabel(Name + "Spread", X, Y);

  for (i = 0; i < AmountLP; i++)
  {
    Y += FONT_SIZE * 1.5;

    CreateLabel(Name + "Bid_" + i, X, Y);
  }

  return;
}

void CreateAllMarketDepths( int XStep, int YStep, int AmountX )
{
  int AmountSymbols = ArraySize(SymbolNames);
  int X = 0, Y = 0, Count = 0;

  for (int i = 0; i < AmountSymbols; i++)
  {
    if (Count == AmountX)
    {
      Count = 0;
      X = 0;
      Y += YStep;
    }

    CreateMarketDepth(i, X, Y);

    X += XStep;
    Count++;
  }

  return;
}


bool ArbitrageBid( int Num )
{
  int i, AmountLP = ArraySize(LP);
  double Tmp, Prices[];

  ArrayResize(Prices, AmountLP);

  for (i = 0; i < AmountLP; i++)
    Prices[i] = Bids[i][Num];

  ArraySort(Prices);

  if (Prices[AmountLP - 2] == 0)
    return(FALSE);
  else
    Tmp = Prices[AmountLP - 1] - Prices[AmountLP - 2];

  for (i = 0; i < AmountLP; i++)
    Prices[i] = Asks[i][Num];

  ArraySort(Prices);

  for (i = 0; i < AmountLP; i++)
    if (Prices[i] > 0)
      break;

  if (i >= AmountLP - 1)
    return(TRUE);
  else
    Tmp -= Prices[i + 1] - Prices[i];

  return(Tmp > 0);
}

void SetLevel( int Num, string Type, string Value, color Color )
{
  Type = WindowExpertName() + "_" + SymbolNames[Num] + "_" + Type;

  ObjectSetText(Type, Value, FONT_SIZE, "Times New Roman", Color);

  return;
}

string StrNumToLen( string Num, int Len, bool Space = FALSE )
{
  string Str;

  if (Space)
    Str = " ";
  else
    Str = "0";

  Len -= StringLen(Num);

  while (Len > 0)
  {
    Num = Str + Num;
    Len--;
  }

  return(Num);
}

int GetPriceINT( int j )
{
  int Res, AmountLP = ArraySize(LP);
  double Price;

  for (int i = 0; i < AmountLP; i++)
  {
    Price = Bids[i][j];

    if (Price != 0)
      break;
  }

  Res = Price / GetMarketInfo(j, MODE_POINT) + 0.1;

  return(Res);
}

void SaveDataLPs()
{
  int AmountLP = ArraySize(LP);
  int AmountSymbols = ArraySize(SymbolNames);
  int handle, PrevTime = Time[1];
  int PrevHour = TimeHour(PrevTime); 
  string PrevDate = WindowExpertName() + "\\"
     + TimeToStr(PrevTime, TIME_DATE) + "\\";
  string FileName, StrBidTime, StrAskTime;
  string StrWithoutBidProfit, StrWithoutAskProfit, StrWithoutLPProfit, StrAllProfit;
  string StrWithoutBidAmount, StrWithoutAskAmount, StrWithoutLPAmount, StrAllAmount, StrPrice;

  for (int i = 0; i < AmountLP; i++)
  {
    FileName = PrevDate + LP[i] + ".prn";

    handle = FileOpen(FileName, FILE_CSV|FILE_READ|FILE_WRITE);
    FileSeek(handle, 0, SEEK_END);

    StrBidTime = PrevHour;
    StrAskTime = PrevHour;

    StrWithoutBidProfit = PrevHour;
    StrWithoutAskProfit = PrevHour;
    StrWithoutLPProfit = PrevHour;

    StrAllProfit = PrevHour;

    StrWithoutBidAmount = PrevHour;
    StrWithoutAskAmount = PrevHour;
    StrWithoutLPAmount = PrevHour;

    StrAllAmount = PrevHour;
    StrPrice = PrevHour;

    for (int j = 0; j < AmountSymbols; j++)
    {
      StrBidTime = StrBidTime + " " + DoubleToStr(100.0 * SumTimeBid[i][j] / HourTime, 8);
      StrAskTime = StrAskTime + " " + DoubleToStr(100.0 * SumTimeAsk[i][j] / HourTime, 8);

      StrWithoutBidProfit = StrWithoutBidProfit + " " + ProfitWithoutBid[i][j];
      StrWithoutAskProfit = StrWithoutAskProfit + " " + ProfitWithoutAsk[i][j];
      StrWithoutLPProfit = StrWithoutLPProfit + " " + ProfitWithoutLP[i][j];

      StrAllProfit = StrAllProfit + " " + ProfitAll[j];

      StrWithoutBidAmount = StrWithoutBidAmount + " " + AmountWithoutBid[i][j];
      StrWithoutAskAmount = StrWithoutAskAmount + " " + AmountWithoutAsk[i][j];
      StrWithoutLPAmount = StrWithoutLPAmount +  " " + AmountWithoutLP[i][j];

      StrAllAmount = StrAllAmount + " " + AmountAll[j];
      StrPrice = StrPrice + " " + GetPriceINT(j);
    }

    FileWrite(handle, StrBidTime);
    FileWrite(handle, StrAskTime);

    FileWrite(handle, StrWithoutBidProfit);
    FileWrite(handle, StrWithoutAskProfit);
    FileWrite(handle, StrWithoutLPProfit);

    FileWrite(handle, StrAllProfit);

    FileWrite(handle, StrWithoutBidAmount);
    FileWrite(handle, StrWithoutAskAmount);
    FileWrite(handle, StrWithoutLPAmount);

    FileWrite(handle, StrAllAmount);
    FileWrite(handle, StrPrice);

    FileClose(handle);
  }

  return;
}

void SaveDataSymbols()
{
  int AmountLP = ArraySize(LP);
  int AmountSymbols = ArraySize(SymbolNames);
  int handle, PrevTime = Time[1];
  int PrevHour = TimeHour(PrevTime);
  string PrevDate = WindowExpertName() + "\\" + TimeToStr(PrevTime, TIME_DATE) + "\\";
  string FileName, StrBidTime, StrAskTime;
  string StrWithoutBidProfit, StrWithoutAskProfit, StrWithoutLPProfit, StrAllProfit;
  string StrWithoutBidAmount, StrWithoutAskAmount, StrWithoutLPAmount, StrAllAmount, StrPrice;

  for (int j = 0; j < AmountSymbols; j++)
  {
    FileName = PrevDate + StringSubstr(SymbolNames[j], 0, 3) + StringSubstr(SymbolNames[j], 4, 3) + ".prn";

    handle = FileOpen(FileName, FILE_CSV|FILE_READ|FILE_WRITE);
    FileSeek(handle, 0, SEEK_END);

    StrBidTime = PrevHour;
    StrAskTime = PrevHour;

    StrWithoutBidProfit = PrevHour;
    StrWithoutAskProfit = PrevHour;
    StrWithoutLPProfit = PrevHour;

    StrAllProfit = PrevHour;

    StrWithoutBidAmount = PrevHour;
    StrWithoutAskAmount = PrevHour;
    StrWithoutLPAmount = PrevHour;

    StrAllAmount = PrevHour;
    StrPrice = PrevHour;

    for (int i = 0; i < AmountLP; i++)
    {
      StrBidTime = StrBidTime + " " + DoubleToStr(100.0 * SumTimeBid[i][j] / HourTime, 8);
      StrAskTime = StrAskTime + " " + DoubleToStr(100.0 * SumTimeAsk[i][j] / HourTime, 8);

      StrWithoutBidProfit = StrWithoutBidProfit + " " + ProfitWithoutBid[i][j];
      StrWithoutAskProfit = StrWithoutAskProfit + " " + ProfitWithoutAsk[i][j];
      StrWithoutLPProfit = StrWithoutLPProfit + " " + ProfitWithoutLP[i][j];

      StrAllProfit = StrAllProfit + " " + ProfitAll[j];

      StrWithoutBidAmount = StrWithoutBidAmount + " " + AmountWithoutBid[i][j];
      StrWithoutAskAmount = StrWithoutAskAmount + " " + AmountWithoutAsk[i][j];
      StrWithoutLPAmount = StrWithoutLPAmount +  " " + AmountWithoutLP[i][j];

      StrAllAmount = StrAllAmount + " " + AmountAll[j];
      StrPrice = StrPrice + " " + GetPriceINT(j);
    }

    FileWrite(handle, StrBidTime);
    FileWrite(handle, StrAskTime);

    FileWrite(handle, StrWithoutBidProfit);
    FileWrite(handle, StrWithoutAskProfit);
    FileWrite(handle, StrWithoutLPProfit);

    FileWrite(handle, StrAllProfit);

    FileWrite(handle, StrWithoutBidAmount);
    FileWrite(handle, StrWithoutAskAmount);
    FileWrite(handle, StrWithoutLPAmount);

    FileWrite(handle, StrAllAmount);
    FileWrite(handle, StrPrice);

    FileClose(handle);
  }

  return;
}

void SaveData()
{
  static string PrevDay = "";
  string NewDay = TimeToStr(TimeCurrent(), TIME_DATE);

  if (PrevDay != NewDay)
  {
    SaveNames();

    PrevDay = NewDay;
  }

  SaveDataSymbols();
  SaveDataLPs();

  return;
}

void SetSumTime()
{
  static int PrevHour = -1;
  static int PrevTime = 0;
  int AmountLP = ArraySize(LP);
  int AmountSymbols = ArraySize(SymbolNames);
  int NewTime = GetTickCount();
  int Interval = 0;
  int i, j;

  if (PrevTime != 0)
    Interval = NewTime - PrevTime;

  PrevTime = NewTime;

  if (Hour() != PrevHour)
  {
    if (HourTime > 55 * PERIOD_H1 * 1000) // Если статистика собиралась более 55-и минут
      SaveData();

    PrevHour = Hour();
    HourTime = 0;

    for (j = 0; j < AmountSymbols; j++)
      for (i = 0; i < AmountLP; i++)
      {
        SumTimeBid[i][j] = 0;
        SumTimeAsk[i][j] = 0;
      }
  }
  else if (HourTime <= PERIOD_H1 * PERIOD_H1 * 1000)
  {
    HourTime += Interval;

    for (i = 0; i < AmountLP; i++)
      for (j = 0; j < AmountSymbols; j++)
      {
        if (BestBid[i][j])
          SumTimeBid[i][j] += Interval;

        if (BestAsk[i][j])
          SumTimeAsk[i][j] += Interval;
      }
  }

  return;
}

void SetMarketDepth( int Num )
{
  static int Count = 0;
  string Value, Message = SymbolNames[Num];
  double MinMax, Prices[][2];
  int i, j, Tmp, AmountLP = ArraySize(LP);
  int AmountLP_Add = ArraySize(LP_Add);
  int AmountLP2 = AmountLP - AmountLP_Add;
  int digits = GetMarketInfo(Num, MODE_DIGITS);
  double point = GetMarketInfo(Num, MODE_POINT);
  bool Flag = ArbitrageBid(Num);
  double BidBest0, AskBest0;
  double BidBest1, AskBest1;
  int Spread;
  color Color;

//  AmountLP--; // Temp

  ArrayResize(Prices, AmountLP);

  for (i = 0; i < AmountLP; i++)
  {
    Prices[i][0] = Bids[i][Num];
    Prices[i][1] = i;
  }

  ArraySort(Prices);


  BidBest0 = Prices[AmountLP - 1][0];
  BidBest1 = Prices[AmountLP - 2][0];

  MinMax = BidBest0;

  if (MinMax == 0)
    for (i = AmountLP - 1; i >= 0; i--)
      BestBid[i][Num] = FALSE;
  else
    for (i = AmountLP - 1; i >= 0; i--)
    {
      Tmp = Prices[i][1];
      BestBid[Tmp][Num] = (Prices[i][0] == MinMax);
    }

  if (Flag)
    Color = ColorArbitragePrice;
  else
    Color = ColorBid;

  j = 0;

  for (i = AmountLP - 1; i >= 0; i--)
  {
    if (Prices[i][0] == 0)
      Value = "";
    else
    {
      Tmp = Prices[i][1];
      Value = DoubleToStr(Prices[i][0], digits) + " " + TimeToStr(TimeBids[Tmp][Num], TIME_SECONDS) + "." + StrNumToLen(TimeBidsMs[Tmp][Num], 3)+ " " + LP[Tmp];

      if (HourTime != 0)
        Value = Value + " " + DoubleToStr(100.0 * SumTimeBid[Tmp][Num] / HourTime, 1) + "% ("+ TimeToStr(SumTimeBid[Tmp][Num] * PERIOD_H1 / 1000, TIME_MINUTES) + ")";

      if (ProfitAll[Num] != 0)
        Value = Value + " " + DoubleToStr(100 - 100.0 * ProfitWithoutBid[Tmp][Num] / ProfitAll[Num], 2) + "%";

      if (j == 0)
        Message = Message + ", Bid = " + Value;
      else if (Tmp >= AmountLP2)
        Color = ColorLPOutBid;
    }

    SetLevel(Num, "Bid_" + j, Value, Color);

    Color = ColorBid;

    j++;
  }

  Spread = Prices[AmountLP - 1][0] / point + 0.1;

  for (i = 0; i < AmountLP; i++)
  {
    Prices[i][0] = Asks[i][Num];
    Prices[i][1] = i;
  }

  ArraySort(Prices);

  MinMax = Prices[0][0];

  for (i = 0; i < AmountLP; i++)
  {
    Tmp = Prices[i][1];

    if (MinMax > 0)
      BestAsk[Tmp][Num] = (Prices[i][0] == MinMax);
    else
    {
      MinMax = Prices[i][0];
      BestAsk[Tmp][Num] = (MinMax > 0);

      j = i;
    }
  }

  AskBest0 = MinMax;

  if (j < AmountLP - 1)
    AskBest1 = Prices[j + 1][0];
  else
    AskBest1 = 0;

  for (i = 0; i < AmountLP; i++)
  {
    if (Prices[i][0] != 0)
      break;

    SetLevel(Num, "Ask_" + i, "", Red);
  }

  if (i < AmountLP)
  {
    Tmp = Prices[i][0] / point + 0.1;
    Spread = Tmp - Spread;
  }
  else
    Spread = 0;

  if (Flag)
    Color = ColorAsk;
  else
    Color = ColorArbitragePrice;

  j = AmountLP - 1;

  while (i < AmountLP)
  {
    Tmp = Prices[i][1];
    Value = DoubleToStr(Prices[i][0], digits) + " " + TimeToStr(TimeAsks[Tmp][Num], TIME_SECONDS) + "." + StrNumToLen(TimeAsksMs[Tmp][Num], 3)+ " " + LP[Tmp];

    if (HourTime != 0)
      Value = Value + " " + DoubleToStr(100.0 * SumTimeAsk[Tmp][Num] / HourTime, 1) + "% ("+ TimeToStr(SumTimeAsk[Tmp][Num] * PERIOD_H1 / 1000, TIME_MINUTES) + ")";

    if (ProfitAll[Num] != 0)
      Value = Value + " " + DoubleToStr(100 - 100.0 * ProfitWithoutAsk[Tmp][Num] / ProfitAll[Num], 2) + "%";

    if (j == AmountLP - 1)
      Message = Message + ", Ask = " + Value;
    else if (Tmp >= AmountLP2)
      Color = ColorLPOutAsk;

    SetLevel(Num, "Ask_" + j, Value, Color);

    Color = ColorAsk;

    j--;
    i++;
  }

  Value = "Spread = " + Spread + ", " + TimeToStr(TimeCurrent(), TIME_SECONDS) +
          "." + StrNumToLen((GetTickCount() - NullTime) % 1000, 3);

  Value = Value + ",All=" + ProfitAll[Num] + ",Am=" + AmountAll[Num];

  if (Spread < 0)
  {
    SetLevel(Num, "Spread", SymbolNames[Num] + ", " + Value, ColorNegativeSpread);

    Message = Message + ", " + Value;

    if (Flag)
      Message = "SELL " + Message;
    else
      Message = "BUY " + Message;

//    Message = Message + "... " + LP[AmountLP] + ", Bid = " + DoubleToStr(Bids[AmountLP][Num], digits) + " " + TimeToStr(TimeBids[AmountLP][Num], TIME_SECONDS) + "." + StrNumToLen(TimeBidsMs[AmountLP][Num], 3) +
//                                                ", Ask = " + DoubleToStr(Asks[AmountLP][Num], digits) + " " + TimeToStr(TimeAsks[AmountLP][Num], TIME_SECONDS) + "." + StrNumToLen(TimeAsksMs[AmountLP][Num], 3) +
//                                                ", Spread = " + DoubleToStr((Asks[AmountLP][Num] - Bids[AmountLP][Num]) / point + 0.1, 0); // Temp

//    Print(Count + ": " + Message);

    if (AmountLP_Add == 0)
      SendMessage(Message);

    Count++;
  }
  else
    SetLevel(Num, "Spread", SymbolNames[Num] + ", " + Value, ColorPositiveSpread);

  SetProfit(Num, BidBest0, AskBest0, BidBest1, AskBest1);

  return;
}

void SetProfit( int j, double BidBest0, double AskBest0, double BidBest1, double AskBest1 )
{
  int AmountLP = ArraySize(LP);
  double point = GetMarketInfo(j, MODE_POINT);
  int PriceBidBest0 = BidBest0 / point + 0.1;
  int PriceAskBest0 = AskBest0 / point + 0.1;
  int PriceBidBest1 = BidBest1 / point + 0.1;
  int PriceAskBest1 = AskBest1 / point + 0.1;
  int PriceBid, PriceAsk;

  if ((PriceBidBest0 == 0) || (PriceAskBest0 == 0) ||
      (PriceBidBest1 == 0) || (PriceAskBest1 == 0) ||
      (PriceBidBest0 > PriceAskBest0))
  {

//    if ((PriceBidBest0 <= PriceAskBest0))
//      Print(SymbolNames[j] + " - Bid = " + PriceBidBest0 + ", Ask = " + PriceAskBest0);

    return;
  }

  int Profit = GetProfit(-1, j, PriceBidBest0, PriceAskBest0, WITH_ALL);

  if (Profit > 0)
  {
    ProfitAll[j] += Profit;
    AmountAll[j]++;
  }

  for (int i = 0; i < AmountLP; i++)
  {
    PriceBid = Bids[i][j] / point + 0.1;
    PriceAsk = Asks[i][j] / point + 0.1;

    if (PriceBid == PriceBidBest0)
      PriceBid = PriceBidBest1;
    else
      PriceBid = PriceBidBest0;

    Profit = GetProfit(i, j, PriceBid, PriceAskBest0, WITHOUT_BID);

    if (Profit > 0)
    {
      ProfitWithoutBid[i][j] += Profit;
      AmountWithoutBid[i][j]++;
    }

    if (PriceAsk == PriceAskBest0)
      PriceAsk = PriceAskBest1;
    else
      PriceAsk = PriceAskBest0;

    Profit = GetProfit(i, j, PriceBidBest0, PriceAsk, WITHOUT_ASK);

    if (Profit > 0)
    {
      ProfitWithoutAsk[i][j] += Profit;
      AmountWithoutAsk[i][j]++;
    }

    Profit = GetProfit(i, j, PriceBid, PriceAsk, WITHOUT_LP);

    if (Profit > 0)
    {
      ProfitWithoutLP[i][j] += Profit;
      AmountWithoutLP[i][j]++;
    }
  }

  return;
}

void DeleteAllObject()
{
  string Name, StrTmp = WindowExpertName();
  int Pos = ObjectsTotal() - 1;

  while (Pos >= 0)
  {
    Name = ObjectName(Pos);

    if (StringFind(Name, StrTmp) >= 0)
      ObjectDelete(Name);

    Pos--;
  }

  return;
}

void deinit()
{
  Comment("");

  DeleteAllObject();

  WindowRedraw();

  return;
}

string StrDelSpaces( string Str )
{
  int Pos, Length;

  Str = StringTrimLeft(Str);
  Str = StringTrimRight(Str);

  Length = StringLen(Str) - 1;
  Pos = 1;

  while (Pos < Length)
    if (StringGetChar(Str, Pos) == ' ')
    {
      Str = StringSubstr(Str, 0, Pos) + StringSubstr(Str, Pos + 1, 0);
      Length--;
    }
    else
      Pos++;

  return(Str);
}

int StrToStringS( string Str, string Razdelitel, string &Output[] )
{
  int Pos, LengthSh;
  int Count = 0;

  Str = StrDelSpaces(Str);
  Razdelitel = StrDelSpaces(Razdelitel);

  LengthSh = StringLen(Razdelitel);

  while (TRUE)
  {
    Pos = StringFind(Str, Razdelitel);
    Output[Count] = StringSubstr(Str, 0, Pos);
    Count++;

    if (Pos == -1)
      break;

    Pos += LengthSh;
    Str = StringSubstr(Str, Pos);
  }

  return(Count);
}

int SetLP_Add()
{
  int AmountLP_Add = StrToStringS(LPNames, ",", LP_Add);

  if (LPNames == "")
    AmountLP_Add = 0;

  ArrayResize(LP_Add, AmountLP_Add);

  return(AmountLP_Add);
}

int SetServerName()
{
  int Amount = StrToStringS(ServerNames, ",", ServerName);

  if (ServerNames == "")
    Amount = 0;

  ArrayResize(ServerName, Amount);

  return(Amount);
}

void SaveNames()
{
  int AmountLP = ArraySize(LP);
  int AmountSymbols = ArraySize(SymbolNames);
  int i, handle = FileOpen(WindowExpertName() + "\\" + TimeToStr(TimeCurrent(), TIME_DATE) + "\\LPs.txt", FILE_CSV|FILE_WRITE);

  for (i = 0; i < AmountLP; i++)
    FileWrite(handle, i + " " + LP[i]);

  FileClose(handle);

  handle = FileOpen(WindowExpertName() + "\\" + TimeToStr(TimeCurrent(), TIME_DATE) + "\\Symbols.txt", FILE_CSV|FILE_WRITE);

  for (i = 0; i < AmountSymbols; i++)
    FileWrite(handle, i + " " + SymbolNames[i]);

  FileClose(handle);

  return;
}

int GetProfit( int i, int j, int PriceBid, int PriceAsk, int Type )
{
  static bool FirstRun = TRUE;
  static int PrevHour = -1;
  static bool FlagUPWithoutBid[][MAX_AMOUNTSYMBOLS], FlagUPWithoutAsk[][MAX_AMOUNTSYMBOLS], FlagUPWithoutLP[][MAX_AMOUNTSYMBOLS], FlagUPAll[];
  static int MinWithoutBid[][MAX_AMOUNTSYMBOLS], MinWithoutAsk[][MAX_AMOUNTSYMBOLS], MinWithoutLP[][MAX_AMOUNTSYMBOLS], MinAll[];
  static int MaxWithoutBid[][MAX_AMOUNTSYMBOLS], MaxWithoutAsk[][MAX_AMOUNTSYMBOLS], MaxWithoutLP[][MAX_AMOUNTSYMBOLS], MaxAll[];
  int Min, Max;
  bool FlagUP;
  int Profit;

  if ((Hour() != PrevHour) && (HourTime == 0))
  {
    int AmountLP = ArraySize(LP);
    int AmountSymbols = ArraySize(SymbolNames);

    if (FirstRun)
    {
      ArrayResize(FlagUPWithoutBid, AmountLP);
      ArrayResize(FlagUPWithoutAsk, AmountLP);
      ArrayResize(FlagUPWithoutLP, AmountLP);
      ArrayResize(FlagUPAll, AmountSymbols);

      ArrayResize(MinWithoutBid, AmountLP);
      ArrayResize(MinWithoutAsk, AmountLP);
      ArrayResize(MinWithoutLP, AmountLP);
      ArrayResize(MinAll, AmountSymbols);

      ArrayResize(MaxWithoutBid, AmountLP);
      ArrayResize(MaxWithoutAsk, AmountLP);
      ArrayResize(MaxWithoutLP, AmountLP);
      ArrayResize(MaxAll, AmountSymbols);

      FirstRun = FALSE;
    }


    for (int m = 0; m < AmountSymbols; m++)
    {
      for (int k = 0; k < AmountLP; k++)
      {
        FlagUPWithoutBid[k][m] = TRUE;
        FlagUPWithoutAsk[k][m] = TRUE;
        FlagUPWithoutLP[k][m] = TRUE;

        MaxWithoutBid[k][m] = 0;
        MaxWithoutAsk[k][m] = 0;
        MaxWithoutLP[k][m] = 0;

        MinWithoutBid[k][m] = 999999999;
        MinWithoutAsk[k][m] = 999999999;
        MinWithoutLP[k][m] = 999999999;

        ProfitWithoutBid[k][m] = 0;
        ProfitWithoutAsk[k][m] = 0;
        ProfitWithoutLP[k][m] = 0;

        AmountWithoutBid[k][m] = 0;
        AmountWithoutAsk[k][m] = 0;
        AmountWithoutLP[k][m] = 0;
      }

      FlagUPAll[m] = TRUE;
      MaxAll[m] = 0;
      MinAll[m] = 999999999;

      ProfitAll[m] = 0;
      AmountAll[m] = 0;
    }

    PrevHour = Hour();
  }

//  Print("PriceBid = " + PriceBid + ", PriceAsk = " + PriceAsk);

  if (Type == WITHOUT_BID)
  {
    Min = MinWithoutBid[i][j];
    Max = MaxWithoutBid[i][j];
    FlagUP = FlagUPWithoutBid[i][j];
  }
  else if (Type == WITHOUT_ASK)
  {
    Min = MinWithoutAsk[i][j];
    Max = MaxWithoutAsk[i][j];
    FlagUP = FlagUPWithoutAsk[i][j];
  }
  else if (Type == WITHOUT_LP)
  {
    Min = MinWithoutLP[i][j];
    Max = MaxWithoutLP[i][j];
    FlagUP = FlagUPWithoutLP[i][j];
  }
  else // Type == WITH_ALL
  {
    Min = MinAll[j];
    Max = MaxAll[j];
    FlagUP = FlagUPAll[j];
  }
/*
  static int Tmp = 0;

  if (Type == WITH_ALL)
  {
    if ((Tmp % 100) == 0)
      Print("PriceBid = " + PriceBid + ", PriceAsk = " + PriceAsk + ", Min = " + Min + ", Max = " + Max);

    Tmp++;
  }
*/
  Profit = GetProfit2(PriceBid, PriceAsk, Min, Max, FlagUP);

  if (Type == WITHOUT_BID)
  {
    MinWithoutBid[i][j] = Min;
    MaxWithoutBid[i][j] = Max;
    FlagUPWithoutBid[i][j] = FlagUP;
  }
  else if (Type == WITHOUT_ASK)
  {
    MinWithoutAsk[i][j] = Min;
    MaxWithoutAsk[i][j] = Max;
    FlagUPWithoutAsk[i][j] = FlagUP;
  }
  else if (Type == WITHOUT_LP)
  {
    MinWithoutLP[i][j] = Min;
    MaxWithoutLP[i][j] = Max;
    FlagUPWithoutLP[i][j] = FlagUP;
  }
  else // Type == WITH_ALL
  {
    MinAll[j] = Min;
    MaxAll[j] = Max;
    FlagUPAll[j] = FlagUP;
  }

  return(Profit);
}

int GetProfit2( int PriceHigh, int PriceLow, int &Min, int& Max, bool &FlagUP )
{
  int Profit = 0;

  if (FlagUP)
  {
    if (PriceHigh > Max)
      Max = PriceHigh;
    else if (Max > PriceLow)
    {
      Profit = Max - Min;

      Min = PriceLow;
      FlagUP = FALSE;
    }
  }
  else
  {
    if (PriceLow < Min)
      Min = PriceLow;
    else if (PriceHigh > Min)
    {
      Profit = Max - Min;

      Max = PriceHigh;
      FlagUP = TRUE;
    }
  }

  return(Profit);
}

double GetMarketInfo( int Num, int Type )
{
  double Res = 0;
  int AmountLP = ArraySize(LP) - ArraySize(LP_Add);

  for (int i = 0; i < AmountLP; i++)
    if (Symbols[i][Num] != "")
    {
      Res = MarketInfo(Symbols[i][Num], Type);

      break;
    }

  return(Res);
}

void start()
{
  int AmountSymbols = SetSymbols();
  int AmountLP_Add = ArraySize(LP_Add);
  int AmountServers = SetServerName();
  int Temp;

  if ((AmountLP_Add > 0) && (AmountServers > 0))
    CreatePipeServer(ServerName[0]);

  CreateAllMarketDepths(XStep, YStep, AmountX);

  while (!IsStopped())
  {
    Temp = GetTickCount();

    GetPrices();

    SetSumTime();

    for (int i = 0; i < AmountSymbols; i++)
      SetMarketDepth(i);

    WindowRedraw();

    Temp = GetTickCount() - Temp;
    Comment("Cycle = " + Temp + " ms.");

    Sleep(PAUSE);
  }

  if ((AmountLP_Add > 0) && (AmountServers > 0))
    DestroyPipeServer();

  return;
}

