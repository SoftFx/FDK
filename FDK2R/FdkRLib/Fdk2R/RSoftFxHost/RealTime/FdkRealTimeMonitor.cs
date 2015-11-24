using System;
using System.Collections.Generic;
using SoftFX.Extended;
using SoftFX.Extended.Events;

namespace RHost
{
    public class FdkRealTimeMonitor
    {
        public int Id { get; private set; }

        public FdkRealTimeMonitor(string symbol, int level, DateTime utcNow, int id)
        {
            Id = id;
            Symbol = symbol;
            this.Level = level;
            TimeToMonitor = utcNow;
        }

        public string Symbol { get; set; }

        public int Level { get; private set; }

        public DateTime TimeToMonitor { get; set; }

        public TickEventArgs LastEventData { get; set; }

        public FdkRealTimeMonitor Clone()
        {
            var result = new FdkRealTimeMonitor(Symbol, 0, TimeToMonitor, Id);
            result.LastEventData = LastEventData;
            return result;
        }

        public FdkRealTimeQuote[] BuildSnapshot()
        {
			if (LastEventData == null) 
			{
				return new FdkRealTimeQuote[0];
			}
            Quote lastTick = LastEventData.Tick;
            var maxBidAsks = Math.Max(lastTick.Bids.Length, lastTick.Asks.Length);
            var resultList = new List<FdkRealTimeQuote>();
            for (var i = 0; i < maxBidAsks; i++)
            {
                var newItem = new FdkRealTimeQuote()
                {
                    ReceivingTime = LastEventData.ReceivingTime,
                    SendingTime = LastEventData.SendingTime
                };
                if (i < lastTick.Bids.Length)
                {
                    newItem.BidPrice = lastTick.Bids[i].Price;
                    newItem.BidVolume = lastTick.Bids[i].Volume;
                }
                if (i < lastTick.Asks.Length)
                {
                    newItem.AskPrice = lastTick.Asks[i].Price;
                    newItem.AskVolume = lastTick.Asks[i].Volume;
                }
                resultList.Add(newItem);
            }

            return resultList.ToArray();
        }
    }
}