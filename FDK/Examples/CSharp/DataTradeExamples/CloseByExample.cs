namespace DataTradeExamples
{
    using System;
    using SoftFX.Extended;

    class CloseByExample : Example
    {
        public CloseByExample(string address, string username, string password)
            : base(address, username, password)
        {
        }
        protected override void RunExample()
        {
            var buy = this.Trade.Server.SendOrder("EURUSD", TradeCommand.Market, TradeRecordSide.Buy, 0, 1000000, null, null, null, null);
            var sell = this.Trade.Server.SendOrder("EURUSD", TradeCommand.Market, TradeRecordSide.Sell, 0, 1200000, null, null, null, null);
            var status = buy.CloseBy(sell);
            Console.WriteLine("Close by status: {0}", status);
        }
    }
}
