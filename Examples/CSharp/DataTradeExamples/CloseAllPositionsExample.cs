namespace DataTradeExamples
{
    using System;
    using SoftFX.Extended;

    class CloseAllPositionsExample : Example
    {
        public CloseAllPositionsExample(string address, string username, string password)
            : base(address, username, password)
        {
        }

        protected override void RunExample()
        {
            this.Trade.Server.SendOrder("EURUSD", TradeCommand.Market, TradeRecordSide.Buy, 0, 1000000, null, null, null, null, null, null, null);
            this.Trade.Server.SendOrder("EURUSD", TradeCommand.Market, TradeRecordSide.Buy, 0, 1000000, null, null, null, null, null, null, null);
            var count = this.Trade.Server.CloseAllPositions();
            Console.WriteLine("Number of closed positions is {0}", count);
        }
    }
}
