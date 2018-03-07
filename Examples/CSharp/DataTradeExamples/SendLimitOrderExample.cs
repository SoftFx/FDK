namespace DataTradeExamples
{
    using System;
    using SoftFX.Extended;

    class SendLimitOrderExample : Example
    {
        public SendLimitOrderExample(string address, string username, string password)
            : base(address, username, password)
        {
            Trade.ExecutionReport += Trade_ExecutionReport;
        }

        private void Trade_ExecutionReport(object sender, SoftFX.Extended.Events.ExecutionReportEventArgs e)
        {
            Console.WriteLine($"ExecutionReport: {e.Report.ExecutionType} {e.Report.OrderStatus}");
        }

        protected override void RunExample()
        {
            try
            {
                var record = this.Trade.Server.SendOrderEx("Operation1", "EURUSD", TradeCommand.Market, TradeRecordSide.Sell, 4000, null, 1.24068, null, null, null, null, "Open Market Bot 2018-03-07 14:13:09", "{\"Key\":\"T Open Market Script 1\",\"Tag\":null}", 0);
                Console.WriteLine("Trade record: {0}", record);
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
