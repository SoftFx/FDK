namespace DataFeedExamples
{
    using System;
    using SoftFX.Extended.Events;

    class TicksExample : Example
    {
        public TicksExample(string address, string username, string password)
            : this(address, username, password, true)
        {
        }

        public TicksExample(string address, string username, string password, bool useFixProtocol)
            : base(address, username, password, useFixProtocol)
        {
            this.Feed.Logon += this.OnLogon;
            this.Feed.Tick += this.OnTick;
        }

        void OnLogon(object sender, LogonEventArgs e)
        {
            var symbols = new[]
            {
                "EURUSD",
                "EURJPY",
            };
                    
            // we should subscribe to quotes every time after logon event
            this.Feed.Server.SubscribeToQuotes(symbols, 3);
        }

        protected override void RunExample()
        {
            Console.WriteLine("Press any key to stop");
            Console.ReadKey();
        }

        void OnTick(object sender, TickEventArgs e)
        {
            Console.WriteLine("OnTick(): {0}", e);
        }
        
    }
}
