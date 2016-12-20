namespace DataTradeExamples
{
    using System;
    using SoftFX.Extended;

    class SendStopLimitOrderExample : Example
    {
        public SendStopLimitOrderExample(string address, string username, string password)
            : base(address, username, password)
        {
        }

        protected override void RunExample()
        {
            Console.WriteLine("Sending stop limit order");
            var record = this.Trade.Server.SendOrder("EUR/USD", TradeCommand.StopLimit, TradeRecordSide.Buy, 2.0, 100000, 3.0, null, null, null, null, null, null, null);
            Console.WriteLine("Trade record: {0}", record);
            Console.ReadKey();
        }
    }
}
