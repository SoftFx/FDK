using System.IO;
using System.Text;
using Mql2Fdk.Translator.Lexer.Preprocessor;

namespace Mq4RewriterTesting.Common
{
    internal static class ScriptUtils
    {
        public const string ComplexCode = @"
#property copyright asd
#property link      dfa

extern int RateTrailingDistance = 6;

double PipsCoef=0;

//+------------------------------------------------------------------+
//| expert initialization function                                   |
//+------------------------------------------------------------------+
int start()
  {
//----
   PipsCoef = MathPow(10, -Digits);
//----
   return(0);
  }
";


        public const string CodeWithIfs = @"
int start()
  {
   if(Bars<100)
     {
      Print(1);
      return(0);  
     }  
    if(Bars<5);
}
";

        public const string CodeWithWhiles = @"
int start()
  {
   while(Bars<100)
     {
      Print(1);
      return(0);  
     }  
    if(Bars<5);
}
";


        public const string CodeWithFunctionCall = @"
int start()
  {
//----
   PipsCoef = MathPow(10, -Digits);
//----
   return(0);
  }
";

        public const string ComplexIfsCode = @"
int OrderScan( int MagicNumber, string Symb, int Type1, int Type2 = -1 )
{
  int Types[MAX_TYPES];

  Types[0] = Type1;
  Types[1] = Type2;

  for (int i = 0; i < MAX_TYPES; i++)
  {
    if (Types[i] == -1)
      break;

    RefreshRates();

    for (int j = OrdersTotal() - 1; j >= 0; j--)
    {
      if (OrderSelect(j, SELECT_BY_POS))
        if ((OrderType() == Types[i])&& (OrderSymbol() == Symb) && (OrderMagicNumber() == MagicNumber))
          return(Types[i]);
    }
  }

  return(-1);
}
";

        public const string GetMarginFunctionWithComplexComments = @"

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
";

        public const string SimpleArray = @"
int OrderScan(  )
{
  int Types[2];
}
";

        public const string ComplexFunctionWrongFolding = @"

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


  return;
}
";
        public const string PathToScripts = @"..\..\..\Extras\Scripts\";

        public static string CodeFromFile(this string fileName, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.Default;
            return File.ReadAllText(fileName, encoding);
        }

        public static string CodeFromExtras(this string fileName, Encoding encoding = null)
        {
            var filePath = Path.Combine(PathToScripts, fileName);
            IncludePaths.AddIncludeDirectoryOfFile(filePath);
            return CodeFromFile(filePath, encoding);
        }
    }
}