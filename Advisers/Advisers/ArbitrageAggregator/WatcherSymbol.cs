using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using SoftFX.C;

namespace ArbitrageAggregator
{
	internal class WatcherSymbol
	{
		internal WatcherSymbol(string symbol)
		{
			Log.WriteLine("WatcherSymbol({0})", symbol);
			this.m_symbol = symbol;
		}
        
        private readonly string m_symbol;
        Dictionary<int, StrategyPairTransactionInfo> dictSPairs = new Dictionary<int, StrategyPairTransactionInfo>();
        public class StrategyPairTransactionInfo
        {
            //first LP has lower ID
            public Arbitrage m_firstBidSecondAskArbitrage = null;

            static public int GetKey(int first, int second)
            {
                    return (first << 16) | second;
            }
        }

		internal void Update(SingleAdviser<int> firstAdviser, SingleAdviser<int> secondAdviser)
		{
            Level2 f = firstAdviser.GetLevel2(m_symbol);
            Level2 s = secondAdviser.GetLevel2(m_symbol);
			if (!f.Exist || !s.Exist)
			{
				return;
			}
			if (!f.HasBeenChanged && !s.HasBeenChanged)
			{
				return;
			}
            int key = StrategyPairTransactionInfo.GetKey(firstAdviser.Tag, secondAdviser.Tag);
            StrategyPairTransactionInfo sPairs = null;
            if (!dictSPairs.TryGetValue(key, out sPairs))
            {
                sPairs = new StrategyPairTransactionInfo();
                {
                    dictSPairs.Add(key, sPairs);
                }
            }

            Run(firstAdviser, secondAdviser, s, f, ref sPairs.m_firstBidSecondAskArbitrage);
		}
		private void Run(SingleAdviser<int> first, SingleAdviser<int> second, Level2 f, Level2 s, ref  Arbitrage arb)
		{
			if ( f.Bids.Count == 0 || s.Asks.Count == 0 || s.Ask - f.Bid > -float.Epsilon)
			{
                if (null != arb)
				{
                    arb.EndDateTime = DateTime.UtcNow;
                    arb.Duration = (arb.EndDateTime - arb.StartDateTime).TotalMilliseconds;
                    ArbitrageDetail prevArbDetail = arb.ArbitrageDetails.Last();
                    prevArbDetail.EndTickTime = arb.EndDateTime;
                    prevArbDetail.duration = (prevArbDetail.EndTickTime - prevArbDetail.StartTickTime).TotalMilliseconds;

                    double test = Math.Abs(arb.ArbitrageDetails.Sum(p => p.duration));
                    if (test - arb.Duration > 0.0000000001)
                    {
                        Console.WriteLine("DurationError");
                    }

                    Arbitrage refArbitrage = arb;
                    //Task.Factory.StartNew(() =>
                    //{
                    //    using (Model.ArbitrageContext arbContext = new Model.ArbitrageContext())
                    //    {
                    //        arbContext.Arbitrages.Add(refArbitrage);
                    //        arbContext.SaveChanges();
                    //    }
                    //});

                    arb = null;

                    if (!dictSPairs.Any(p => p.Value.m_firstBidSecondAskArbitrage != null))
                    {
                        ArbitrageTimeStatistics ars = ArbitrageTimeStatistics.Instance(f.Symbol);
                        ars.TotalSecondsArbitradge += (DateTime.UtcNow - ars.StartArbitrageDateTime).TotalSeconds;
                        ars.StartArbitrageDateTime = DateTime.MinValue;

                        Console.WriteLine("Symbol={3}; ArbitrageTime={0} seconds; Percent={1}%; InHourSeconds={2}", ars.TotalSecondsArbitradge,
                            ars.TotalSecondsArbitradge * 100 / (DateTime.UtcNow - ArbitrageTimeStatistics.StartAppDateTime).TotalSeconds,
                            ars.TotalSecondsArbitradge * 3600 / (DateTime.UtcNow - ArbitrageTimeStatistics.StartAppDateTime).TotalSeconds,
                            f.Symbol);
                    }
				
                }
			}
			else
			{
                DateTime utcNow = DateTime.UtcNow;

                if (arb == null)
                    arb = new Arbitrage(utcNow, first.Tag, second.Tag, m_symbol);

                ArbitrageDetail arbDetail = new ArbitrageDetail(utcNow);
                if (f.Asks.Count > 0)
                {
                    arbDetail.B1Ask1 = f.Asks[0].Price;
                    arbDetail.B1Ask1Volume = f.Asks[0].Volume;
                }
                arbDetail.B1Bid1 = f.Bids[0].Price;
                arbDetail.B1Bid1Volume = f.Bids[0].Volume;
                arbDetail.B2Ask1 = s.Asks[0].Price;
                arbDetail.B2Ask1Volume = s.Asks[0].Volume;
                if (s.Bids.Count > 0)
                {
                    arbDetail.B2Bid1 = s.Bids[0].Price;
                    arbDetail.B2Bid1Volume = s.Bids[0].Volume;
                }
                if (arb.ArbitrageDetails.Count != 0)
                {
                    ArbitrageDetail previos = arb.ArbitrageDetails.Last();
                    previos.EndTickTime = arbDetail.StartTickTime;
                    previos.duration = (previos.EndTickTime - previos.StartTickTime).TotalMilliseconds;
                }
                arbDetail.spread = s.Ask - f.Bid;
                arb.ArbitrageDetails.Add(arbDetail);

                if (ArbitrageTimeStatistics.Instance(f.Symbol).StartArbitrageDateTime == DateTime.MinValue)
                    ArbitrageTimeStatistics.Instance(f.Symbol).StartArbitrageDateTime = DateTime.UtcNow;

                //CheckDoubleArbitrage(first.Tag, second.Tag);
			}
		}
        internal void CheckDoubleArbitrage(int ArbBidTag, int ArbAskTag)
        {
            int counter = 0;
            Arbitrage a1 = null, a2 = null;

            foreach (var item in dictSPairs)
            {
                if (item.Value.m_firstBidSecondAskArbitrage != null && item.Value.m_firstBidSecondAskArbitrage.BankBidGAsk == ArbBidTag)
                {
                    //continue if itself
                    if (ArbAskTag == item.Value.m_firstBidSecondAskArbitrage.BankAskLBid)
                    {
                        a1 = item.Value.m_firstBidSecondAskArbitrage;
                        continue;
                    }
                    counter++;
                    a2 = item.Value.m_firstBidSecondAskArbitrage;
                }
            }
            if (counter == 1 && a1 != null && a2 != null)
            {
                //run trade for 
                DoubleArbitrage1B2A da = new DoubleArbitrage1B2A();
                da.Symbol = m_symbol;
                da.FirstArbitrage = a1;
                da.SecondArbitrage = a2;
                TradeDoubleArbitrage.Instance.TradeBegin(da);
            }

            counter = 0;
            a1 = null;
            a2 = null;

            foreach (var item in dictSPairs)
            {
                if (item.Value.m_firstBidSecondAskArbitrage != null && item.Value.m_firstBidSecondAskArbitrage.BankAskLBid == ArbAskTag)
                {
                    //continue if itself
                    if (ArbBidTag == item.Value.m_firstBidSecondAskArbitrage.BankBidGAsk)
                    {
                        a1 = item.Value.m_firstBidSecondAskArbitrage;
                        continue;
                    }
                    counter++;
                    a2 = item.Value.m_firstBidSecondAskArbitrage;
                }
            }
            if (counter == 1 && a1 != null && a2 != null)
            {
                //run trade for 
                DoubleArbitrage2B1A da = new DoubleArbitrage2B1A();
                da.Symbol = m_symbol;
                da.FirstArbitrage = a1;
                da.SecondArbitrage = a2;
                TradeDoubleArbitrage.Instance.TradeBegin(da);
            }
        }

        public override int GetHashCode()
		{
			return m_symbol.GetHashCode();
		}
		public override bool Equals(object obj)
		{
			WatcherSymbol watcher = obj as WatcherSymbol;
			if (null == watcher)
			{
				return false;
			}
			return (m_symbol == watcher.m_symbol);
		}


	}
}
