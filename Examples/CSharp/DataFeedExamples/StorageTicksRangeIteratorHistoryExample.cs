namespace DataFeedExamples
{
    using System;
    using System.Globalization;
    using SoftFX.Extended.Storage.Sequences;

    class StorageTicksRangeIteratorHistoryExample : Example
    {
        public StorageTicksRangeIteratorHistoryExample(string address, string username, string password)
            : base(address, username, password)
        {
        }

        protected override void RunExample()
        {
            var startTime = DateTime.Parse("06/01/2012 00:00:00.000", CultureInfo.InvariantCulture);
            var endTime = DateTime.Parse("08/01/2012 00:00:00.000", CultureInfo.InvariantCulture);

            var symbols = this.Feed.Server.GetSymbols();
            Console.WriteLine("symbols.Length = {0}", symbols.Length);

            foreach (var symbol in symbols)
            {
                if (!this.Run(symbol.Name, startTime, endTime))
                {
                    break;
                }
            }
        }

        bool Run(string symbol, DateTime startTime, DateTime endTime)
        {
            Console.WriteLine("Getting quotes for {0}", symbol);

            var sequence = new QuotesSingleSequence(this.Storage.Online, symbol, startTime, endTime, 1);
            var hour = 0;

            foreach (var element in sequence)
            {
                if (hour != element.CreatingTime.Hour)
                {
                    Console.WriteLine(element.CreatingTime);
                    hour = element.CreatingTime.Hour;
                }
            }

            return true;
        }
    }
}
