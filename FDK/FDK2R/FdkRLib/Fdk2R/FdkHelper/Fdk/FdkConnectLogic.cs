#region Uses

using log4net;
using SoftFX.Extended;
using SoftFX.Extended.Storage;
using System;
using System.IO;
using System.Reflection;

#endregion


namespace RHost.Shared
{
    public class FdkConnectLogic : IDisposable
    {
        static readonly ILog Log = LogManager.GetLogger(typeof(FdkConnectLogic));
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public FdkConnectLogic(string address, string username, string password)
        {
            Address = address;
            Username = username;
            Password = password;
            // create and initialize fix connection string builder
            Builder = new FixConnectionStringBuilder
            {
                TargetCompId = "EXECUTOR",
                ProtocolVersion = FixProtocolVersion.TheLatestVersion.ToString(),
                SecureConnection = false,
                Port = 5001,
                DecodeLogFixMessages = true,
                Address = address,
                Username = username,
                Password = password,
                FixEventsFileName = string.Format("{0}.trade.events.log", username),
                FixMessagesFileName = string.Format("{0}.trade.messages.log", username),
            };
            TradeWrapper = new FdkTradeWrapper();
            //this.Builder.ExcludeMessagesFromLogs = "W";
        }

        internal FixConnectionStringBuilder Builder { get; private set; }
        public DataFeed Feed { get; private set; }
        public DataFeedStorage Storage { get; set; }
        public FdkTradeWrapper TradeWrapper { get; set; }
        public string RootPath { get; set; }
        public bool Initialized { get; set; }

        public void Dispose()
        {
            if (null != Feed)
            {
                Feed.Stop();
                Feed.Dispose();
                Feed = null;
            }

            if (null == Storage) return;
            Storage.Dispose();
            Storage = null;
        }

        public void SetupPathsAndConnect(string rootPath)
        {
            if (Initialized)
            {
                throw new InvalidOperationException("Fdk seems to be initialized for second time");
            }
            Initialized = true;


            // create and specify log directory
            string root;
            if (string.IsNullOrEmpty(rootPath))
            {
                var assembly = Assembly.GetEntryAssembly();
                root = assembly == null ? Directory.GetCurrentDirectory() : assembly.Location;
                root = Path.GetDirectoryName(root);
                if (root == null)
                    throw new InvalidDataException("FDK assembly's directory seems to be invalid");
            }
            else
            {
                root = rootPath;
            }
            var logsPath = Path.Combine(root, "Logs\\Fix");
            Directory.CreateDirectory(logsPath);

            Builder.FixLogDirectory = logsPath;

            Feed = new DataFeed(Builder.ToString()) { SynchOperationTimeout = 18000 };

            var storagePath = Path.Combine(root, "Store");
            Directory.CreateDirectory(storagePath);

            Storage = new DataFeedStorage(storagePath, StorageProvider.Ntfs, Feed, true);
        }


        static void EnsureDirectoriesCreated(string logPath)
        {
            if (!Directory.Exists(logPath))
                Directory.CreateDirectory(logPath);
        }

        public bool DoConnect()
        {
            EnsureDirectoriesCreated(Builder.FixLogDirectory);
            var connectionString = Builder.ToString();

            SetupTradeConnection(Builder.FixLogDirectory);

            Feed.Initialize(connectionString);

            try
            {
                var result = Feed.Start(Feed.SynchOperationTimeout);
                return result;
            }
            catch (Exception ex)
            {
                Log.Warn(ex);
                throw;
            }
        }

        void SetupTradeConnection(string logPath)
        {
            TradeWrapper = new FdkTradeWrapper();
            TradeWrapper.Connect(Address, Username, Password, logPath);

        }

        public void Disconnect()
        {
            Dispose();
        }
    }
}