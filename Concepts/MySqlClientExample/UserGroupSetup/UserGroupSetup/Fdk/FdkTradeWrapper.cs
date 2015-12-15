using System;
using System.IO;
using System.Threading;
using SoftFX.Extended;
using SoftFX.Extended.Events;

namespace RHost.Shared
{
    public class FdkTradeWrapper
    {
        public void Connect(string address, string username, string password, string logPath)
        {
            EnsureDirectoriesCreated(logPath);

            // Create builder
            SetupBuilder(address, username, password, logPath);
            Trade = new DataTrade
            {
                SynchOperationTimeout = 300000
            };
            var connectionString = Builder.ToString();
            Trade.Initialize(connectionString);
            Trade.Logon += OnLogon;
            Trade.Start();
            var timeoutInMilliseconds = Trade.SynchOperationTimeout;
            if (!_syncEvent.WaitOne(timeoutInMilliseconds))
            {
                throw new TimeoutException("Timeout of logon waiting has been reached");
            }
        }
 
        internal void SetupBuilder(string address, string username, string password, string logPath)
        {
            // Create builder
            var builder = new FixConnectionStringBuilder
            {
                TargetCompId = "EXECUTOR",
                ProtocolVersion = FixProtocolVersion.TheLatestVersion.ToString(),
                SecureConnection = true,
                Port = 5004,
                //ExcludeMessagesFromLogs = "W",
                DecodeLogFixMessages = true,
                Address = address,
                Username = username,
                Password = password,
                FixLogDirectory = logPath,
                FixEventsFileName = string.Format("{0}.trade.events.log", username),
                FixMessagesFileName = string.Format("{0}.trade.messages.log", username)
            };
            Builder = builder;
        }

        public DataTrade Trade { get; set; }

        internal FixConnectionStringBuilder Builder { get; private set; }
        readonly AutoResetEvent _syncEvent = new AutoResetEvent(false);

        private void OnLogon(object sender, LogonEventArgs e)
        {
            _syncEvent.Set();
        }

        static void EnsureDirectoriesCreated(string logPath)
        {
            if (!Directory.Exists(logPath))
                Directory.CreateDirectory(logPath);
        }
    }
}