namespace DataTradeExamples
{
    using System;
    using SoftFX.Extended;

    class DeletePendingOrderExample : Example
    {
        public DeletePendingOrderExample(string address, string username, string password)
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
            try
            {
                var order = this.Trade.Server.SendOrder("EURUSD", TradeCommand.Limit, TradeRecordSide.Buy, 1000000, null, 1.0, null, null, null, null, null, null, null, null);
                order.Delete();
                Console.WriteLine("Limit order has been deleted");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex);
            }
        }
    }
}
