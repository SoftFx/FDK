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
            var record = this.Trade.Server.SendOrderEx("Operation1", "EURUSD", TradeCommand.Limit, TradeRecordSide.Buy, 100000, null, 1.0, null, null, null, null, null, null, null);
            Console.WriteLine("Trade record: {0}", record);
            Console.ReadKey();
        }
    }
}
