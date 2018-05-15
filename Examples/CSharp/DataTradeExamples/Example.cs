namespace DataTradeExamples
{
    using SoftFX.Extended.Events;
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using SoftFX.Extended;

    abstract class Example : IDisposable
    {
        #region Construction

        protected Example(string address, string username, string password)
        {
            // Create folders
            EnsureDirectoriesCreated();

            // Create builder
            this.builder = new FixConnectionStringBuilder
            {
                TargetCompId = "EXECUTOR",
                ProtocolVersion = FixProtocolVersion.TheLatestVersion.ToString(),
                SecureConnection = false,
                Port = 5002,
                //ExcludeMessagesFromLogs = "W",
                DecodeLogFixMessages = true,

                Address = address,
                Username = username,
                Password = password,
/*
                ProxyType = "Socks5",
                ProxyAddress = "10.9.14.50",
                ProxyPort = 1080,
                ProxyUserName = "User",
                ProxyPassword = "Password",
*/
                FixLogDirectory = "Logs",
                FixEventsFileName = string.Format("{0}.trade.events.log", username),
                FixMessagesFileName = string.Format("{0}.trade.messages.log", username)
            };

            this.Trade = new DataTrade
            {
                SynchOperationTimeout = 30000
            };
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

        #endregion

        #region Control Methods

        internal void Run()
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
            var connectionString = this.builder.ToString();
            this.Trade.Initialize(connectionString);
            this.Trade.Logon += this.OnLogon;
            this.Trade.Logout += this.OnLogout;
            this.Trade.CacheInitialized += this.OnCacheInitialized;
            this.Trade.ExecutionReport += this.OnExecutionReport;
            this.Trade.PositionReport += this.OnPositionReport;
            this.Trade.AccountInfo += this.OnAccountInfo;
            this.Trade.SessionInfo += this.OnSessionInfo;
            this.Trade.Notify += this.OnNofity;
            this.Trade.BalanceOperation += this.OnBalanceOperaiton;
            this.Trade.Start();
            var timeoutInMilliseconds = this.Trade.SynchOperationTimeout;
            if (!syncEvent.WaitOne(timeoutInMilliseconds))
            {
                throw new TimeoutException("Timeout of logon waiting has been reached");
            }
            this.RunExample();
        }

        #endregion

        #region Event Handlers

        void OnLogon(object sender, LogonEventArgs e)
        {
            
        }

        void OnLogout(object sender, LogoutEventArgs e)
        {
            Console.WriteLine("OnLogout(): {0}", e);
        }

        void OnCacheInitialized(object sender, CacheEventArgs e)
        {
            this.syncEvent.Set();
            Console.WriteLine("OnCacheInitialized(): {0}", e);
        }

        void OnNofity(object sender, NotificationEventArgs e)
        {
            Console.WriteLine("OnNotify(): {0}", e);
        }

        void OnBalanceOperaiton(object sender, NotificationEventArgs<BalanceOperation> e)
        {
            Console.WriteLine("OnBalanceOperaiton(): {0}", e);
        }

        void OnExecutionReport(object sender, ExecutionReportEventArgs e)
        {
            Console.WriteLine("OnExecutionReport(): ExecutionType = {0}; OrderStatus = {1}; OrderType = {2}", e.Report.ExecutionType, e.Report.OrderStatus, e.Report.OrderType);
        }

        void OnPositionReport(object sender, PositionReportEventArgs e)
        {
            Console.WriteLine("OnPositionReport(): {0}", e);
        }

        void OnAccountInfo(object sender, AccountInfoEventArgs e)
        {
            Console.WriteLine("OnAccountInfo(): {0}", e);
        }

        void OnSessionInfo(object sender, SessionInfoEventArgs e)
        {
            Console.WriteLine("OnSessionInfo(): {0}", e);
        }

        #endregion

        #region Abstract Methods

        protected abstract void RunExample();

        #endregion

        #region IDisposable interface

        public void Dispose()
        {
            if (this.Trade == null)
                return;

            this.Trade.Stop();
            this.Trade.Dispose();
            this.Trade = null;
        }

        #endregion

        #region Members

        readonly FixConnectionStringBuilder builder;
        readonly AutoResetEvent syncEvent = new AutoResetEvent(false);

        #endregion
    }
}
