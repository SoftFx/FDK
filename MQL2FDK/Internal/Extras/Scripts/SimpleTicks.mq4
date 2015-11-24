#define ALPHA 0.000001
#define MAX_TYPES 2

extern int Tral = 3;
extern double Lots = 0.1;
extern double MinLot = 0.1;
extern int MagicNumber = 0;

int LotDigits;

int GetDigits( double Num )
{
  int Mult = 1, Res = 0;
  int Tmp = Num * Mult + ALPHA;
  while (MathAbs(Tmp - Num * Mult) > ALPHA)
  {
    Mult *= 10;
    Tmp = Num * Mult + ALPHA;

    Res++;
  }

  return(Res);
}

void init()
{
  LotDigits = GetDigits(MarketInfo(Symbol(), MODE_MINLOT));

  return;
}

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

void MyOrderModify( int Type, double Lots, double Price )
{
  int a = OrderScan(MagicNumber, Symbol(), Type);

  if (Type == OP_BUYLIMIT)
    Price = MathMin(Price, Ask);
  else if (Type == OP_SELLLIMIT)
    Price = MathMax(Price, Bid);

  Price = NormalizeDouble(Price, Digits);
  Lots = NormalizeDouble(Lots, LotDigits);

  if (a == Type)
  {
    if (MathAbs(Price - OrderOpenPrice()) > ALPHA)
      OrderModify(OrderTicket(), Price, OrderStopLoss(), OrderTakeProfit(), 0);
  }
  else if (Lots >= MinLot)
    OrderSend(Symbol(), Type, Lots, Price, 0, 0, 0, "", MagicNumber);

  return;
}

void SetOrders( double Lots, double PriceLow, double PriceHigh )
{
  double LotsBuyLimit = 0, LotsSellLimit = 0;

  if (Lots > 0)
    LotsBuyLimit = Lots;
  else
    LotsSellLimit = -Lots;

  MyOrderModify(OP_BUYLIMIT, LotsBuyLimit, PriceLow);
  MyOrderModify(OP_SELLLIMIT, LotsSellLimit, PriceHigh);

  return;
}

void CloseBy()
{
  int SellTicket, OrderTime;
  int a = OrderScan(MagicNumber, Symbol(), OP_SELL);

  if (a == OP_SELL)
  {
    SellTicket = OrderTicket();
    OrderTime = OrderOpenTime();

    a = OrderScan(MagicNumber, Symbol(), OP_BUY);

    if (a == OP_BUY)
    {
      if (OrderOpenTime() < OrderTime)
        OrderCloseBy(OrderTicket(), SellTicket);
      else
        OrderCloseBy(SellTicket, OrderTicket());
    }
  }

  return;
}

double GetLot( int MagicNumber, double Lots )
{
  RefreshRates();

  double Netto = 0, Limits = 0;
  int Pos = OrdersTotal() - 1;

  while (Pos >= 0)
  {
    if (OrderSelect(Pos, SELECT_BY_POS))
      if ((OrderSymbol() == Symbol()) && (OrderMagicNumber() == MagicNumber))
      {
        if (OrderType() == OP_BUY)
          Netto -= OrderLots();
        else if (OrderType() == OP_SELL)
          Netto += OrderLots();
        else if (OrderType() == OP_BUYLIMIT)
          Limits -= OrderLots();
        else if (OrderType() == OP_SELLLIMIT)
          Limits += OrderLots();
    }

    Pos--;
  }

  Limits += Netto;

  if (Netto < 0)
    Limits -= Lots;
  else
    Limits += Lots;

  return(Limits);

}

void GetPrices( double &PriceBid, double &PriceAsk )
{
  RefreshRates();

  static int PrevTime = 0;
  static double PrevPriceHigh = 0, PrevPriceLow = 0;
  double CurrPriceHigh = iHigh(Symbol(), PERIOD_M1, 0);
  double CurrPriceLow = iLow(Symbol() + "_Ask", PERIOD_M1, 0);
  int CurrTime = iTime(Symbol(), PERIOD_M1, 0);

  if (CurrTime != PrevTime)
  {
    PriceBid = CurrPriceHigh;
    PriceAsk = CurrPriceLow;

    PrevTime = CurrTime;
  }
  else
  {
    if (CurrPriceHigh != PrevPriceHigh)
      PriceBid = CurrPriceHigh;
    else
      PriceBid = Bid;

    if (CurrPriceLow != PrevPriceLow)
      PriceAsk = CurrPriceLow;
    else
      PriceAsk = Ask;
  }

  PrevPriceHigh = CurrPriceHigh;
  PrevPriceLow = CurrPriceLow;

  return;
}

void GetMinMax( int &Min, int &Max, int Pips )
{
  static bool FlagUP = TRUE;
  double PriceBid, PriceAsk;
  int PriceHigh, PriceLow;

  GetPrices(PriceBid, PriceAsk);

  PriceHigh = PriceBid / Point + ALPHA;
  PriceLow = PriceAsk / Point + ALPHA;

  if (FlagUP)
  {
    if (PriceHigh > Max)
      Max = PriceHigh;
    else if (Max - PriceLow >= Pips)
    {
      Min = PriceLow;

      FlagUP = !FlagUP;
    }
  }
  else
  {
    if (PriceLow < Min)
      Min = PriceLow;
    else if (PriceHigh - Min >= Pips)
    {
      Max = PriceHigh;

      FlagUP = !FlagUP;
    }
  }

  return;
}

void start()
{
  static int Min = 0, Max = 0;

  GetMinMax(Min, Max, Tral);

  SetOrders(GetLot(MagicNumber, Lots), (Max - 1 * Tral) * Point, (Min + 1 * Tral) * Point);

  CloseBy();

  return;
}