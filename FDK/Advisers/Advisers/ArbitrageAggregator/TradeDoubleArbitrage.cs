using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ArbitrageAggregator
{
    public class TradeDoubleArbitrage
    {
        #region Singleton
        private static readonly Lazy<TradeDoubleArbitrage> lazy =
            new Lazy<TradeDoubleArbitrage>(() => new TradeDoubleArbitrage());
        public static TradeDoubleArbitrage Instance { get { return lazy.Value; } }
        private TradeDoubleArbitrage()
        {
        }
        #endregion

        object syncObj = new object();
        List<string> listLockedQuotes = new List<string>();

        string GetKey(int bankTag, string symbol, bool isBid)
        {
            return bankTag.ToString() + symbol + ( isBid ? "0" : "1");
        }

        public void TradeBegin(DoubleArbitrage2B1A item)
        {
            Task.Factory.StartNew(() =>
                {
                    string key = GetKey(item.FirstArbitrage.BankAskLBid, item.FirstArbitrage.Symbol, false);
                    lock (syncObj)
                    {
                        if (listLockedQuotes.Contains(key))
                            return;
                        listLockedQuotes.Add(key);
                    }

                    try
                    {
                        System.Threading.Thread.Sleep(1000);
                        if (item.FirstArbitrage.IsFinished || item.SecondArbitrage.IsFinished)
                            return;

                        item.ExpectedAsk = item.FirstArbitrage.ArbitrageDetails.Last().B2Ask1;
                        item.ExecutedBid = item.FirstArbitrage.ArbitrageDetails.Last().B1Bid1;
                        item.ExecutedBid = Math.Max(item.ExecutedBid, item.SecondArbitrage.ArbitrageDetails.Last().B1Bid1);


                        item.TradeDate = DateTime.UtcNow;

                        using (Model.ArbitrageContext arbContext = new Model.ArbitrageContext())
                        {
                            arbContext.DoubleArbitrage2B1A.Add(item);
                            arbContext.SaveChanges();
                        }
                    }
                    finally
                    {
                        lock (syncObj)
                        {
                            listLockedQuotes.Remove(key);
                        }
                    }

                }
            , TaskCreationOptions.LongRunning);
        }

        public void TradeBegin(DoubleArbitrage1B2A item)
        {
            Task.Factory.StartNew(() =>
            {
                string key = GetKey(item.FirstArbitrage.BankBidGAsk, item.FirstArbitrage.Symbol, false);
                lock (syncObj)
                {
                    if (listLockedQuotes.Contains(key))
                        return;
                    listLockedQuotes.Add(key);
                }

                try
                {
                    System.Threading.Thread.Sleep(1000);
                    if (item.FirstArbitrage.IsFinished || item.SecondArbitrage.IsFinished)
                        return;

                    //item.ExpectedBid = item.FirstArbitrage.ArbitrageDetails.Last().B1Bid1;
                    //item.ExecutedAsk = item.FirstArbitrage.ArbitrageDetails.Last().B1Bid1;
                    //item.ExecutedBid = Math.Max(item.ExecutedBid, item.SecondArbitrage.ArbitrageDetails.Last().B1Bid1);


                    item.TradeDate = DateTime.UtcNow;

                    using (Model.ArbitrageContext arbContext = new Model.ArbitrageContext())
                    {
                        arbContext.DoubleArbitrage1B2A.Add(item);
                        arbContext.SaveChanges();
                    }
                }
                finally
                {
                    lock (syncObj)
                    {
                        listLockedQuotes.Remove(key);
                    }
                }

            }
            , TaskCreationOptions.LongRunning);
        }

    }
}
