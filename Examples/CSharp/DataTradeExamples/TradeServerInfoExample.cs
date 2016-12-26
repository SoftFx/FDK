namespace DataTradeExamples
{
    using System;
    using SoftFX.Extended;

    class TradeServerInfoExample : Example
    {
        public TradeServerInfoExample(string address, string username, string password)
            : base(address, username, password)
        {
        }

        protected override void RunExample()
        {
            TradeServerInfo tradeServerInfo = Trade.Server.GetTradeServerInfo();
            Console.WriteLine(tradeServerInfo);
        }
    }
}
