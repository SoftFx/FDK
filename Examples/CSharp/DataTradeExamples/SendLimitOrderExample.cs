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
//            var record = this.Trade.Server.SendOrder("EUR/USD", TradeCommand.StopLimit, TradeRecordSide.Buy, 2.0, 100000, 3.0, null, null, null, null, null, null, null);
            var record = this.Trade.Server.SendOrder("EUR/USD", TradeCommand.Limit, TradeRecordSide.Buy, 2.0, 100000, null, null, null, null, null, null, null, null);
            Console.WriteLine("Trade record: {0}", record);
            Console.ReadKey();
        }
    }
}
