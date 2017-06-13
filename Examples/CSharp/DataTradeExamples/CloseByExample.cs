namespace DataTradeExamples
{
    using System;
    using SoftFX.Extended;

    class CloseByExample : Example
    {
        public CloseByExample(string address, string username, string password)
            : base(address, username, password)
        {
            Trade.ExecutionReport += Trade_ExecutionReport;
        }

        private void Trade_ExecutionReport(object sender, SoftFX.Extended.Events.ExecutionReportEventArgs e)
        {
            Console.WriteLine(e.Report.TradeRequestId);
        }

        protected override void RunExample()
        {
            var buy = this.Trade.Server.SendOrder("EURUSD", TradeCommand.Market, TradeRecordSide.Buy, 1000000, null, 0, null, null, null, null, null, null, null);
            var sell = this.Trade.Server.SendOrder("EURUSD", TradeCommand.Market, TradeRecordSide.Sell, 1200000, null, 0, null, null, null, null, null, null, null);
            var status = buy.CloseByEx(sell, 10000);
            Console.WriteLine("Close by status: {0}", status);
        }
    }
}
