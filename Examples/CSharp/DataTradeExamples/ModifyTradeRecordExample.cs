namespace DataTradeExamples
{
    using System;
    using SoftFX.Extended;

    class ModifyTradeRecordExample : Example
    {
        public ModifyTradeRecordExample(string address, string username, string password)
            : base(address, username, password)
        {
        }

        protected override void RunExample()
        {
            var trade = this.Trade.Server.SendOrderEx("EURUSD", TradeCommand.Market, TradeRecordSide.Buy, 100000, null, null, null, null, null, null, null, null, null, 10000);
            /*
            var position0 = this.Trade.Server.SendOrder("EURUSD", TradeCommand.Market, TradeRecordSide.Buy, 0, 1000000, null, null, null, null, null, null, null, null);
            var position1 = position0.Modify(null, null, null, 2, null, null, null, null);
            Console.WriteLine("Opened position = {0}", position0);
            Console.WriteLine("Modified position = {0}", position1);

            var limit0 = this.Trade.Server.SendOrder("EURUSD", TradeCommand.Market, TradeRecordSide.Buy, 1, 1000000, null, null, null, null, null, null, null, null);
            var limit1 = limit0.Modify(null, null, null, 2, null, null, null, null);
            Console.WriteLine("Opened limit order = {0}", limit0);
            Console.WriteLine("Modified limit order = {0}", limit1);

            var stop0 = this.Trade.Server.SendOrder("EURUSD", TradeCommand.Market, TradeRecordSide.Buy, 1.8, 1000000, null, null, null, null, null, null, null, null);
            var stop1 = stop0.Modify(null, null, null, 2, null, null, null, null);
            Console.WriteLine("Opened stop order = {0}", stop0);
            Console.WriteLine("Modified stop order = {0}", stop1);
            */
        }
    }
}
