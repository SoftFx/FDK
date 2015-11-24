namespace TradeFeedExamples
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using SoftFX.Extended;
    using SoftFX.Extended.Events;
    using SoftFX.Extended.Financial;

    abstract class Example : IDisposable
    {
        #region Construction

        Example()
        {
            // Create folders
            EnsureDirectoriesCreated();

            // create and initialize fix connection string builder
            this.dataTradeBuilder = new FixConnectionStringBuilder
            {
                TargetCompId = "EXECUTOR",
                ProtocolVersion = FixProtocolVersion.TheLatestVersion.ToString(),
                SecureConnection = true,
                Port = 5004,
                DecodeLogFixMessages = true
            };

            this.dataFeedBuilder = new FixConnectionStringBuilder
            {
                TargetCompId = "EXECUTOR",
                ProtocolVersion = FixProtocolVersion.TheLatestVersion.ToString(),
                SecureConnection = true,
                Port = 5003,
                DecodeLogFixMessages = true
            };

            this.dataTradeBuilder.FixLogDirectory = LogPath;
            this.dataFeedBuilder.FixLogDirectory = LogPath;

            this.Trade = new DataTrade();
            this.Feed = new DataFeed();

            this.Trade.Logon += this.OnLogon;
            this.Feed.Logon += this.OnLogon;
            this.Trade.SynchOperationTimeout = 60000;
            this.Feed.SynchOperationTimeout = 60000;
        }

        protected Example(string address, string username, string password)
            : this()
        {
            this.dataTradeBuilder.Address = address;
            this.dataTradeBuilder.Username = username;
            this.dataTradeBuilder.Password = password;
            this.dataTradeBuilder.FixEventsFileName = string.Format("{0}.trade.events.log", username);
            this.dataTradeBuilder.FixMessagesFileName = string.Format("{0}.trade.messages.log", username);


            this.dataFeedBuilder.Address = address;
            this.dataFeedBuilder.Username = username;
            this.dataFeedBuilder.Password = password;
            this.dataFeedBuilder.FixEventsFileName = string.Format("{0}.feed.events.log", username);
            this.dataFeedBuilder.FixMessagesFileName = string.Format("{0}.feed.messages.log", username);
        }

        static void EnsureDirectoriesCreated()
        {
            if (!Directory.Exists(LogPath))
                Directory.CreateDirectory(LogPath);
        }

        #endregion

        #region Properties

        static string CommonPath
        {
            get
            {
                var assembly = Assembly.GetEntryAssembly();
                return assembly != null ? Path.GetDirectoryName(assembly.Location) : string.Empty;
            }
        }

        static string LogPath
        {
            get
            {
                return Path.Combine(CommonPath, "Logs");
            }
        }

        protected DataTrade Trade { get; private set; }

        protected DataFeed Feed { get; private set; }

        protected StateCalculator Calculator { get; private set; }

        #endregion

        #region Control Methods

        public void Run()
        {
            try
            {
                this.DoRun();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        void DoRun()
        {
            this.Trade.Initialize(this.dataTradeBuilder.ToString());
            this.Feed.Initialize(this.dataFeedBuilder.ToString());

            this.Calculator = new StateCalculator(this.Trade, this.Feed);

            this.Trade.Start();
            this.Feed.Start();

            if (!dataTradeEvent.WaitOne(this.Trade.SynchOperationTimeout))
                throw new TimeoutException("Timeout of data trade logon waiting has been reached");

            if (!dataFeedEvent.WaitOne(this.Trade.SynchOperationTimeout))
                throw new TimeoutException("Timeout of data feed logon waiting has been reached");

            
            this.RunExample();
        }

        void OnLogon(object sender, LogonEventArgs e)
        {
            if (this.Trade == sender)
            {
                this.dataTradeEvent.Set();
            }
            if (this.Feed == sender)
            {
                this.dataFeedEvent.Set();
            }
        }
        #endregion

        #region Abstract Methods

        protected abstract void RunExample();

        #endregion

        #region IDisposable Interface

        public void Dispose()
        {
            if (this.Trade != null)
            {
                this.Trade.Stop();
                this.Trade.Dispose();
                this.Trade = null;
            }
            if (this.Feed != null)
            {
                this.Feed.Stop();
                this.Feed.Dispose();
                this.Feed = null;
            }
        }

        #endregion

        #region Members

        readonly AutoResetEvent dataFeedEvent = new AutoResetEvent(false);
        readonly AutoResetEvent dataTradeEvent = new AutoResetEvent(false);
        FixConnectionStringBuilder dataFeedBuilder;
        FixConnectionStringBuilder dataTradeBuilder;

        #endregion
    }
}
