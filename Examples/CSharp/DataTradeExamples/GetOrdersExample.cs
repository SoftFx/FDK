namespace DataTradeExamples
{
    using System;
    using SoftFX.Extended;

    class GetOrdersExample : Example
    {
        public GetOrdersExample(string address, string username, string password)
            : base(address, username, password)
        {
        }

        protected override void RunExample()
        {
            //this.Trade.Server.SendOrder("EUR/USD", TradeCommand.Limit, TradeRecordSide.Buy, 1.1, 1000000, null, null, null, null, null, null);
            //this.Trade.Server.SendOrder("EUR/USD", TradeCommand.Stop, TradeRecordSide.Buy, 2.1, 1000000, null, null, null, null, null, null);
            this.Trade.Server.SendOrder("EURUSD", TradeCommand.Market, TradeRecordSide.Buy, 0, 1000000, null, null, null, null, null, null);
            //this.Trade.SynchOperationTimeout = 600000;
            var records = this.Trade.Server.GetTradeRecords();
            Console.WriteLine("Records number = {0}", records.Length);
            Console.ReadKey();
        }
    }
}
