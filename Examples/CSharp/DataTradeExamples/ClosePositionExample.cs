namespace DataTradeExamples
{
    using System;
    using SoftFX.Extended;

    class ClosePositionExample : Example
    {
        public ClosePositionExample(string address, string username, string password)
            : base(address, username, password)
        {
        }

        void CloseAll()
        {
            for (var records = this.Trade.Server.GetTradeRecords(); records.Length > 0; records = this.Trade.Server.GetTradeRecords())
            {
                foreach (var element in records)
                {
                    try
                    {
                        if (element.IsPendingOrder)
                        {
                            element.Delete();
                        }
                        else
                        {
                            element.Close();
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        protected override void RunExample()
        {
            this.CloseAll();

            try
            {
                this.Trade.SynchOperationTimeout = 30000;
                var position = this.Trade.Server.SendOrder("EURUSD", TradeCommand.Market, TradeRecordSide.Buy, 0, 1000000, null, null, null, null, null, null, null, null, null);
                Console.WriteLine("Opened position = {0}", position);

                //var position2 = position.Modify(null, null, 2.0, null);

                var result = position.Close();
 
                Console.WriteLine("Closing result = {0}", result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
