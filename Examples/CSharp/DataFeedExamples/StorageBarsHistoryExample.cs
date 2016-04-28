namespace DataFeedExamples
{
    using System;
    using SoftFX.Extended;

    class StorageBarsHistoryExample : Example
    {
        public StorageBarsHistoryExample(string address, string username, string password)
            : base(address, username, password)
        {
        }

        protected override void RunExample()
        {
            //var startTime = DateTime.Parse("01/01/2012 00:55:00", CultureInfo.InvariantCulture);
            //var endTime = DateTime.Parse("6/01/2012 23:01:00", CultureInfo.InvariantCulture);
            //var utcNow = DateTime.UtcNow;
            //var from = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day, utcNow.Hour, 0, 0, DateTimeKind.Utc);
            //var to = from.AddHours(1);

            //var symbols = new[] { "EURUSD" };
            //this.Feed.Server.SubscribeToQuotes(symbols, 1);

            //Console.ReadKey();

            //var bars = this.Storage.GetBars("EURUSD", PriceType.Bid, BarPeriod.S1, startTime, endTime, false);
            //Console.WriteLine("bars.Length = {0}", bars.Length);

            //bars = this.Storage.GetBars("EURUSD", PriceType.Bid, BarPeriod.S1, startTime, - 3000);
            //Console.WriteLine("bars.Length = {0}", bars.Length);


            var bars = this.Storage.Online.GetPairBars("AUDCAD", BarPeriod.H1, new DateTime(2016, 3, 4), -120);
            Console.WriteLine("bars.Length = {0}", bars.Length);
            Console.ReadKey();
        }
    }
}
