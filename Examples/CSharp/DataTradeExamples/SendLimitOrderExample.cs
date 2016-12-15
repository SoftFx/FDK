namespace DataTradeExamples
{
    using System;
    using SoftFX.Extended;

    class SendLimitOrderExample : Example
    {
        public SendLimitOrderExample(string address, string username, string password)
            : base(address, username, password)
        {
        }

        protected override void RunExample()
        {
            //var expiration = DateTime.UtcNow.AddMinutes(10);
            var record = this.Trade.Server.SendOrder("EURUSD", TradeCommand.Limit, TradeRecordSide.Buy, 2.0, 100000, null, null, null, null, null, null, null);
            Console.WriteLine("Limit order: {0}", record);
            Console.ReadKey();
            Console.WriteLine(this.Trade.Cache.TradeRecords[0].Type);
            Console.ReadKey();
        }
    }
}
