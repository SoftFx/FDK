namespace DataFeedExamples
{
    using System;
    using System.IO;
    using System.Reflection;
    using SoftFX.Extended;
    using SoftFX.Extended.Events;
    using SoftFX.Extended.Storage;

    abstract class Example : IDisposable
    {
        #region Construction

        protected Example(string address, string username, string password)
            : this(address, username, password, useFixProtocol: true)
        {
        }

        protected Example(string address, string username, string password, bool useFixProtocol)
        {
            // Create folders
            EnsureDirectoriesCreated();

            // Create builder
            this.builder = useFixProtocol ? CreateFixConnectionStringBuilder(address, username, password, LogPath)
                                          : CreateLrpConnectionStringBuilder(address, username, password, LogPath);

            this.Feed = new DataFeed
            {
                SynchOperationTimeout = 60000
            };

            this.Storage = new DataFeedStorage(StoragePath, StorageProvider.Ntfs, this.Feed, true);

        }

        static ConnectionStringBuilder CreateFixConnectionStringBuilder(string address, string username, string password, string logDirectory)
        {
            var result = new FixConnectionStringBuilder
            {
                SecureConnection = true,
                Port = 5003,
                //ExcludeMessagesFromLogs = "W"
                Address = address,
                FixLogDirectory = logDirectory,
                FixEventsFileName = string.Format("FIX_{0}.feed.events.log", username),
                FixMessagesFileName = string.Format("FIX_{0}.feed.messages.log", username),

                Username = username,
                Password = password
            };

            return result;
        }

        static ConnectionStringBuilder CreateLrpConnectionStringBuilder(string address, string username, string password, string logDirectory)
        {
            var result = new LrpConnectionStringBuilder
            {
                Address = address,
                EnableQuotesLogging = true,
                EventsLogFileName = Path.Combine(logDirectory, string.Format("LRP_{0}.feed.events.log", username)),
                MessagesLogFileName = Path.Combine(logDirectory, string.Format("LRP_{0}.feed.messages.log", username)),

                DeviceId = Guid.NewGuid().ToString(),
                Username = username,
                Password = password
            };

            return result;
        }


        static void EnsureDirectoriesCreated()
        {
            if (!Directory.Exists(LogPath))
                Directory.CreateDirectory(LogPath);

            if (!Directory.Exists(StoragePath))
                Directory.CreateDirectory(StoragePath);
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

        static string StoragePath
        {
            get
            {
                return Path.Combine(CommonPath, "Storage");
            }
        }

        protected DataFeed Feed { get; private set; }
        protected DataFeedStorage Storage { get; private set; }

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
                Console.WriteLine(ex.Message);
            }
        }

        void DoRun()
        {
            this.Feed.Initialize(this.builder.ToString());
            this.Feed.Logon += this.OnLogon;
            this.Feed.Logout += this.OnLogout;
            this.Feed.TwoFactorAuth += this.OnTwoFactorAuth;
            this.Feed.Notify += this.OnNotify;

            this.Feed.SessionInfo += this.OnSessionInfo;
            this.Feed.SymbolInfo += this.OnSymbolInfo;
            this.Feed.CurrencyInfo += this.OnCurrencyInfo;

            if (!this.Feed.Start(this.Feed.SynchOperationTimeout))
            {
                Console.ReadKey();
                throw new TimeoutException("Timeout of logon waiting has been reached");
            }

            this.RunExample();
        }

        #endregion

        #region Event Handlers

        void OnLogon(object sender, LogonEventArgs e)
        {
            Console.WriteLine("OnLogon(): {0}", e);
        }

        void OnLogout(object sender, LogoutEventArgs e)
        {
            Console.WriteLine("OnLogout(): {0}", e);
        }

        void OnTwoFactorAuth(object sender, TwoFactorAuthEventArgs e)
        {
            if (e.TwoFactorAuth.Reason == TwoFactorReason.ServerRequest)
            {
                Console.WriteLine("Two factor required! Please enter one time password: ");
                var otp = Console.ReadLine();
                Feed.Server.SendTwoFactorResponse(TwoFactorReason.ClientResponse, otp);
            }
            else if (e.TwoFactorAuth.Reason == TwoFactorReason.ServerSuccess)
                Console.WriteLine("Two factor success: {0}", e.TwoFactorAuth.Text);
            else if (e.TwoFactorAuth.Reason == TwoFactorReason.ServerError)
                Console.WriteLine("Two factor failed: {0}", e.TwoFactorAuth.Text);
            else
                Console.WriteLine("Invalid two factor server response: {0} - {1}", e.TwoFactorAuth.Reason, e.TwoFactorAuth.Text);
        }

        void OnSymbolInfo(object sender, SymbolInfoEventArgs e)
        {
        }

        void OnCurrencyInfo(object sender, CurrencyInfoEventArgs e)
        {
        }

        void OnSessionInfo(object sender, SessionInfoEventArgs e)
        {
        }

        void OnNotify(object sender, NotificationEventArgs e)
        {
            Console.WriteLine("OnNotify(): {0}", e);
        }

        #endregion

        #region Abstract Methods

        protected abstract void RunExample();

        #endregion

        #region IDisposable Interface

        public void Dispose()
        {
            if (this.Feed != null)
            {
                this.Feed.Stop();
                this.Feed.Dispose();
                this.Feed = null;
            }

            if (this.Storage != null)
            {
                this.Storage.Dispose();
                this.Storage = null;
            }
        }

        #endregion

        #region Members

        readonly ConnectionStringBuilder builder;

        #endregion
    }
}
