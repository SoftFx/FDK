namespace DataTradeExamples
{
    using System;
    using SoftFX.Extended;

    class SendMarketOrderExample : Example
    {
        public SendMarketOrderExample(string address, string username, string password)
            : base(address, username, password)
        {
        }

        protected override void RunExample()
        {
            var record = this.Trade.Server.SendOrder("EURUSD", TradeCommand.Market, TradeRecordSide.Buy, 0, 1000000, null, null, null, null);
            Console.WriteLine("Position: {0}", record);
            //record.Close();
            //Console.WriteLine("Position has been closed");
        }
        
    }
}
