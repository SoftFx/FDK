namespace PerformanceTest
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using SoftFX.Extended;
    using SoftFX.Extended.Events;
    using SoftFX.Extended.Financial;

    public class Test : IDisposable
    {
        public Test(string address, string username, string password)
        {
            string LogPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Logs");

            if (! Directory.Exists(LogPath))
                Directory.CreateDirectory(LogPath);

            Feed = new DataFeed();           

            FixConnectionStringBuilder dataFeedBuilder = new FixConnectionStringBuilder();
            dataFeedBuilder.TargetCompId = "EXECUTOR";
            dataFeedBuilder.ProtocolVersion = FixProtocolVersion.TheLatestVersion.ToString();
            dataFeedBuilder.SecureConnection = false;
            dataFeedBuilder.Address = address;
            dataFeedBuilder.Port = 5001;
            dataFeedBuilder.Username = username;
            dataFeedBuilder.Password = password;
            dataFeedBuilder.DecodeLogFixMessages = true;
            dataFeedBuilder.FixLogDirectory = LogPath;
            dataFeedBuilder.FixEventsFileName = string.Format("{0}.feed.events.log", username);
            dataFeedBuilder.FixMessagesFileName = string.Format("{0}.feed.messages.log", username);

            Feed.Initialize(dataFeedBuilder.ToString());
            Feed.SynchOperationTimeout = 60000;

            dataFeedEvent = new AutoResetEvent(false);
            Feed.Logon += this.OnLogon;
            Feed.Tick += this.OnTick;

            Trade = new DataTrade();

            FixConnectionStringBuilder dataTradeBuilder = new FixConnectionStringBuilder();
            dataTradeBuilder.TargetCompId = "EXECUTOR";
            dataTradeBuilder.ProtocolVersion = FixProtocolVersion.TheLatestVersion.ToString();
            dataTradeBuilder.SecureConnection = false;
            dataTradeBuilder.Address = address;
            dataTradeBuilder.Port = 5002;
            dataTradeBuilder.Username = username;
            dataTradeBuilder.Password = password;
            dataTradeBuilder.DecodeLogFixMessages = true;
            dataTradeBuilder.FixLogDirectory = LogPath;
            dataTradeBuilder.FixEventsFileName = string.Format("{0}.trade.events.log", username);
            dataTradeBuilder.FixMessagesFileName = string.Format("{0}.trade.messages.log", username);

            Trade.Initialize(dataTradeBuilder.ToString());
            Trade.SynchOperationTimeout = 60000;

            dataTradeEvent = new AutoResetEvent(false);
            Trade.Logon += this.OnLogon;
            Trade.ExecutionReport += this.OnExecutionReport;
        }

        public void Start()
        {
            lastTickCount = Environment.TickCount;

            Trade.Start();
            Feed.Start();

            if (! dataTradeEvent.WaitOne(this.Trade.SynchOperationTimeout))
                throw new TimeoutException("Timeout of data trade logon waiting has been reached");

            if (! dataFeedEvent.WaitOne(this.Trade.SynchOperationTimeout))
                throw new TimeoutException("Timeout of data feed logon waiting has been reached");

            var info = this.Feed.Server.GetSymbols();
            var count = info.Length;
            var symbols = new string[count];

            for (var index = 0; index < count; ++index)
                symbols[index] = info[index].Name;

            Feed.Server.SubscribeToQuotes(symbols, 1);
        }

        public void Stop()
        {
            Feed.Stop();
            Trade.Stop();
        }

        void OnLogon(object sender, LogonEventArgs e)
        {
            if (this.Trade == sender)
            {
                this.dataTradeEvent.Set();
            }
            else if (this.Feed == sender)
            {
                this.dataFeedEvent.Set();
            }
        }

        void OnTick(object sender, TickEventArgs e)
        {
            try
            {
                Console.WriteLine(e.Tick);

                int tickCount = Environment.TickCount;

                if (tickCount - lastTickCount >= 1000)
                {
                    if (e.Tick.HasAsk)
                    {
                        string symbol = e.Tick.Symbol;
                        double volume = e.Tick.Asks[0].Volume;

                        Console.WriteLine("Buying {0}: {1}", symbol, volume);

                        Trade.Server.SendOrder
                        (
                                symbol,
                                TradeCommand.Market,
                                TradeRecordSide.Buy,
                                0,
                                volume,
                                null,
                                null,
                                null,
                                null,
                                null,
                                null
                        );

                        lastTickCount = tickCount;
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error: {0}", exception.Message);
            }
        }

        void OnExecutionReport(object sender, ExecutionReportEventArgs e)
        {
            try
            {
                if (e.Report.ExecutionType == ExecutionType.Trade)
                {
                    string symbol = e.Report.Symbol;
                    double volume = e.Report.ExecutedVolume;

                    if (e.Report.OrderSide == TradeRecordSide.Buy)
                    {
                        Console.WriteLine("Bought {0}: {1}", symbol, volume);
                        Console.WriteLine("Selling {0}: {1}", symbol, volume);

                        Trade.Server.SendOrder
                        (
                                symbol,
                                TradeCommand.Market,
                                TradeRecordSide.Sell,
                                0,
                                volume,
                                null,
                                null,
                                null,
                                null,
                                null,
                                null
                        );
                    }
                    else
                    {
                        Console.WriteLine("Sold {0}: {1}", symbol, volume);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error: {0}", exception.Message);
            }
        }

        public void Dispose()
        {
            if (this.Trade != null)
            {                
                this.Trade.Dispose();
                this.Trade = null;
            }
            if (this.Feed != null)
            {                
                this.Feed.Dispose();
                this.Feed = null;
            }
        }

        DataTrade Trade;
        DataFeed Feed;

        AutoResetEvent dataFeedEvent;
        AutoResetEvent dataTradeEvent;

        int lastTickCount;
    }
}
