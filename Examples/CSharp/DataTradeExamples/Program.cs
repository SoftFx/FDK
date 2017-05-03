namespace DataTradeExamples
{
    class Program
    {
        static void Main()
        {
            string address = "tp.dev.soft-fx.eu";
            string username = "9";
            string password = "123qwe!";

            //var example = new TradeServerInfoExample(address, username, password);
            var example = new AccountInfoExample(address, username, password);
            //var example = new SendLimitOrderExample(address, username, password);
            //var example = new SendMarketOrderExample(address, username, password);
            //var example = new SendStopOrderExample(address, username, password);
            //var example = new CloseAllPositionsExample(address, username, password);
            //var example = new ClosePositionExample(address, username, password);
            //var example = new ClosePartiallyPositionExample(address, username, password);
            //var example = new DeletePendingOrderExample(address, username, password);
            //var example = new GetOrdersExample(address, username, password);
            //var example = new GetTradeTransactionReportsExample(address, username, password);
            //var example = new ModifyTradeRecordExample(address, username, password);
            //var example = new CloseByExample(address, username, password);

            using (example)
            {
                example.Run();
            }
        }
    }
}
