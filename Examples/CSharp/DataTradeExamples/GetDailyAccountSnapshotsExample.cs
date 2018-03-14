namespace DataTradeExamples
{
    using System;
    using System.Threading;
    using SoftFX.Extended;
    using SoftFX.Extended.Events;

    class GetDailyAccountSnapshotsExample : Example
    {
        public GetDailyAccountSnapshotsExample(string address, string username, string password)
            : base(address, username, password)
        {
        }

        protected override void RunExample()
        {
            var to = DateTime.UtcNow;
            var from = to.AddDays(-10);

            var reportsNumber = 0;
            var it = this.Trade.Server.GetDailyAccountSnapshots(TimeDirection.Forward, from, to, 10);
            for (; !it.EndOfStream; it.Next())
            {
                var s = it.Item;
                Console.WriteLine(s);
                reportsNumber++;
            }
            it.Dispose();
        }
    }
}
