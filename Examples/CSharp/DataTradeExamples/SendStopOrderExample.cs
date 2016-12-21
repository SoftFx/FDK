namespace DataTradeExamples
{
    using System;
    using SoftFX.Extended;

    class SendStopOrderExample : Example
    {
        public SendStopOrderExample(string address, string username, string password)
            : base(address, username, password)
        {
        }

        protected override void RunExample()
        {
            var record = this.Trade.Server.SendOrder("EURUSD", TradeCommand.Stop, TradeRecordSide.Sell, 1.0, 1000000, null, null, null, null, null, null, null, null);
            Console.WriteLine("Stop order: {0}", record);
            record.Delete();
            Console.WriteLine("Order has been deleted");
        }
    }
}
