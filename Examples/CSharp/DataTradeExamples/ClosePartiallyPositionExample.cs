namespace DataTradeExamples
{
    using System;
    using SoftFX.Extended;

    class ClosePartiallyPositionExample : Example
    {
        public ClosePartiallyPositionExample(string address, string username, string password)
            : base(address, username, password)
        {
        }

        protected override void RunExample()
        {
            var position = this.Trade.Server.SendOrder("EURUSD", TradeCommand.Market, TradeRecordSide.Buy, 0, 1000000, null, null, null, null, null, null);
            var result = position.ClosePartially(40000);
            Console.WriteLine("Closing result: {0}", result);
        }
    }
}
