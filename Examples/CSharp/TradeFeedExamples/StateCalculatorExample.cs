using SoftFX.Extended;

namespace TradeFeedExamples
{
    using System;
    using SoftFX.Extended.Financial;

    class StateCalculatorExample : Example
    {
        public StateCalculatorExample(string address, string username, string password)
            : base(address, username, password)
        {
        }

        private string id = "";

        protected override void RunExample()
        {
            this.Subscribe();
            this.Calculator.StateInfoChanged += StateInfoChanged;

            var position = this.Trade.Server.SendOrder("EURUSD", TradeCommand.Market, TradeRecordSide.Buy, 0, 100000, null, null, null, null, null, null, null, null);
            var result = position.ClosePartially(40000);

            Console.ReadKey();

            CloseAll();

            this.Calculator.StateInfoChanged -= StateInfoChanged;
        }

        void StateInfoChanged(object sender, StateInfoEventArgs e)
        {
            Console.WriteLine("Generation = {0}; Margin = {1}", e.Information.Generation, e.Information.Margin);
        }

        void CloseAll()
        {
            for (var records = this.Trade.Server.GetTradeRecords(); records.Length > 0; records = this.Trade.Server.GetTradeRecords())
            {
                try
                {
                    foreach (var element in records)
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
                }
                catch
                {

                }
            }
        }

        void Subscribe()
        {
            var info = this.Feed.Server.GetSymbols();
            var count = info.Length;
            var symbols = new string[count];

            for (var index = 0; index < count; ++index)
            {
                symbols[index] = info[index].Name;
            }

            this.Feed.Server.SubscribeToQuotes(symbols, 1);
        }
    }
}
