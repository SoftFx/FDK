namespace SoftFX.Extended.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TickTrader.BusinessObjects;
    using TickTrader.Common.Business;

	sealed class StorageConvert
	{
		public StorageConvert()
		{
		    this.records = new List<FeedLevel2Record>();
			this.lastUsedTimeStamp = DateTime.MinValue;
		}

        public static BarPeriod ToBarPeriod(Periodicity periodicity)
		{
			var st = periodicity.ToString();
			if (st == "S1")
			{
				return BarPeriod.S1;
			}
			if (st == "S10")
			{
				return BarPeriod.S10;
			}
			if (st == "M1")
			{
				return BarPeriod.M1;
			}
			if (st == "M5")
			{
				return BarPeriod.M5;
			}
			if (st == "M15")
			{
				return BarPeriod.M15;
			}
			if (st == "H1")
			{
				return BarPeriod.H1;
			}
			if (st == "H4")
			{
				return BarPeriod.H4;
			}
			if (st == "D1")
			{
				return BarPeriod.D1;
			}
			if (st == "MN1")
			{
				return BarPeriod.MN1;
			}

			throw new ArgumentException("Unknown periodicity = " + st);
		}

        public static Periodicity ToPeriodicity(BarPeriod period)
		{
			var st = period.ToString();
			var result = Periodicity.Parse(st);
			return result;
		}

        public static PriceType ToPriceType(FxPriceType type)
		{
			if (type == FxPriceType.Bid)
				return PriceType.Bid;
			if (type == FxPriceType.Ask)
				return PriceType.Ask;

			var message = string.Format("Incorrect price type: expected Bid or Ask, but received = {0}", type);
			throw new ArgumentException("type", message);
		}

        public static FxPriceType ToFxPriceType(PriceType type)
		{
			if (type == PriceType.Bid)
				return FxPriceType.Bid;
			else if (type == PriceType.Ask)
				return FxPriceType.Ask;

			var message = string.Format("Incorrect price type: expected Bid or Ask, but received = {0}", type);
			throw new ArgumentException("type", message);
		}

        /// <summary>
        /// Comparator two bid quote entries.
        /// </summary>
        /// <param name="first">A first quote entry.</param>
        /// <param name="second">A second quote entry.</param>
        /// <returns>0, -1, 1</returns>
        static int BidComparator(QuoteEntry first, QuoteEntry second)
        {
            return second.Price.CompareTo(first.Price);
        }

        /// <summary>
        /// Comparator for two ask quote entries.
        /// </summary>
        /// <param name="first">A first quote entry.</param>
        /// <param name="second">A second quote entry.</param>
        /// <returns>0, -1, 1</returns>
        static int AskComparator(QuoteEntry first, QuoteEntry second)
        {
            return first.Price.CompareTo(second.Price);
        }

        public Quote ToQuote(string symbol, TickValue tick, int depth)
		{
			var creatingTime = tick.Time;

            var bids = tick.Level2
                           .Where(o => o.Type == FxPriceType.Bid)
                           .Select(o => new QuoteEntry((double)o.Price, (double)o.Volume))
                           .ToArray();

            var asks = tick.Level2
                           .Where(o => o.Type == FxPriceType.Ask)
                           .Select(o => new QuoteEntry((double)o.Price, (double)o.Volume))
                           .ToArray();

			Array.Sort(bids, BidComparator);
			Array.Sort(asks, AskComparator);

			return new Quote(symbol, creatingTime, bids.Take(depth).ToArray(), asks.Take(depth).ToArray());
		}

        public FeedTick ToFeedTick(Quote quote)
		{
			this.records.Clear();

			this.FillRecords(FxPriceType.Bid, quote.Bids);
			this.FillRecords(FxPriceType.Ask, quote.Asks);

			if (!string.IsNullOrEmpty(quote.Id))
			{
				var id = FeedTickId.Parse(quote.Id);
				this.index = id.Index;
			}
			else if (this.lastUsedTimeStamp < quote.CreatingTime)
			{
				this.index = 0;
			}
			this.lastUsedTimeStamp = quote.CreatingTime;
			var result = new FeedTick(quote.Symbol, quote.CreatingTime, this.index, this.records);
			++this.index;
			return result;
		}

		void FillRecords(FxPriceType type, QuoteEntry[] entries)
		{
			for (var index = 0; index < entries.Length; ++index)
			{
				var entry = entries[index];
				var record = new FeedLevel2Record
                {
				    Price = RoundPrice(entry.Price),
				    Volume = entry.Volume,
				    Type = type
                };
				this.records.Add(record);
			}
		}

		static decimal RoundPrice(double price)
		{
			var result = (decimal)price * PrecisionFactor;
			result = Math.Round(result);
			result /= PrecisionFactor;
			return result;
		}

        public static Bar ToBar(HistoryBar bar, BarPeriod period)
		{
			var from = bar.Time;
			var to = from + period;
			var open = (double)bar.Open;
			var close = (double)bar.Close;
			var low = (double)bar.Low;
			var high = (double)bar.Hi;
			var volume = bar.Volume;
			var result = new Bar(from, to, open, close, low, high, volume);

			return result;
		}

        public static HistoryBar ToHistoryBar(Bar bar)
		{
            var result = new HistoryBar
            {
                Low = (decimal)bar.Low,
                Hi = (decimal)bar.High,
                Open = (decimal)bar.Open,
                Close = (decimal)bar.Close,
                Time = bar.From,
                Volume = (uint)bar.Volume
            };

			return result;
		}

        const decimal PrecisionFactor = 1000000M;

		readonly List<FeedLevel2Record> records;
		DateTime lastUsedTimeStamp;
		byte index;
	}
}

