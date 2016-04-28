namespace DataFeedExamples
{
    using System;
    using SoftFX.Extended;
    using System.Globalization;

    class BarsHistoryExample : Example
    {
        public BarsHistoryExample(string address, string username, string password)
            : this(address, username, password, true)
        {
        }

        public BarsHistoryExample(string address, string username, string password, bool useFixProtocol)
            : base(address, username, password, useFixProtocol)
        {
        }

        protected override void RunExample()
        {
            this.BarsEnumeration();
            this.PairBarsEnumeration();
        }

        void BarsEnumeration()
        {
            var startTime = DateTime.Parse("3/3/2016 13:30:00", CultureInfo.InvariantCulture);
            var endTime = DateTime.Parse("3/3/2016 14:00:00", CultureInfo.InvariantCulture);

            var bars = new Bars(this.Feed, "EURUSD", PriceType.Bid, BarPeriod.S1, startTime, endTime, 1024);

            var averageDeviation = 0D;
            var count = 0;

            foreach (var element in bars)
            {
                averageDeviation += (element.High - element.Low);
                count++;
            }

            averageDeviation /= count;
            Console.WriteLine("BarsEnumeration(): average deviation = {0}", averageDeviation);
        }

        void PairBarsEnumeration()
        {
            var startTime = DateTime.Parse("3/3/2016 13:30:00", CultureInfo.InvariantCulture);
            var endTime = DateTime.Parse("3/3/2016 14:00:00", CultureInfo.InvariantCulture);

            var bars = new PairBars(this.Feed, "EURUSD", BarPeriod.S1, startTime, endTime, 1024);

            var bidsVolume = 0D;
            var asksVolume = 0D;

            foreach (var element in bars)
            {
                if (element.Bid != null)
                    bidsVolume += element.Bid.Volume;

                if (element.Ask != null)
                    asksVolume += element.Ask.Volume;
            }

            Console.WriteLine("PairBarsEnumeration(): bids volume = {0}, asks volume = {1}", bidsVolume, asksVolume);
        }
    }
}
