namespace DataTradeExamples
{
    using System;
    using SoftFX.Extended;

    class DeletePendingOrderExample : Example
    {
        public DeletePendingOrderExample(string address, string username, string password)
            : base(address, username, password)
        {
        }

        protected override void RunExample()
        {
            var order = this.Trade.Server.SendOrder("EUR/USD", TradeCommand.Limit, TradeRecordSide.Buy, 1.1, 1000000, null, null, null, null, null, null, null, null);
            order.Delete();
            Console.WriteLine("Limit order has been deleted");
        }
    }
}
