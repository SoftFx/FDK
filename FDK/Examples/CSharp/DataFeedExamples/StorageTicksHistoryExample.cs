namespace DataFeedExamples
{
    using System;

    class StorageTicksHistoryExample : Example
    {
        public StorageTicksHistoryExample(string address, string username, string password)
            : base(address, username, password)
        {
        }

        protected override void RunExample()
        {
            //var startTime = DateTime.Parse("06/29/2012 00:00:00.000", CultureInfo.InvariantCulture);
            //var endTime = DateTime.Parse("06/29/2012 01:00:00.000", CultureInfo.InvariantCulture);
            var startTime = DateTime.UtcNow.AddHours(-1);
            var endTime = startTime.AddHours(1);

            var quotes = this.Storage.Online.GetQuotes("EURUSD", startTime, endTime, 1);

            var averageSpread = 0D;
            var count = 0;

            foreach (var element in quotes)
            {
                averageSpread += element.Spread;
                count++;
            }

            if (count > 0)
            {
                averageSpread /= count;
                Console.WriteLine("Averge spread = {0}", averageSpread);
            }
            else
            {
                Console.WriteLine("Off quotes");
            }

            Console.ReadKey();
        }
    }
}
