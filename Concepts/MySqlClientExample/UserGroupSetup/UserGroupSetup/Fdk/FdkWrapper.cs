using System;
using System.Collections.Generic;
using System.Threading;
using NLog;
using SoftFX.Extended.Events;

namespace RHost.Shared
{
    public class FdkWrapper
    {
        public FdkWrapper()
        {
        }
        public void SetupBuilder()
        {
            if (IsConnected)
            {
                ConnectLogic.Disconnect();
                IsConnected = false;
            }
            ConnectLogic = new FdkConnectLogic(Address, Login, Password)
            {
                RootPath = Path
            };

            ConnectLogic.TradeWrapper.SetupBuilder(Address, Login, Password, this.Path);
        }

        public bool Connect()
        {
            ConnectLogic.SetupPathsAndConnect(Path);
            ConnectLogic.Feed.CacheInitialized += OnCacheInitialize;
            ConnectLogic.Feed.SessionInfo += OnSessionInfo;
            ConnectLogic.Feed.SymbolInfo += OnSymbolInfo;
            ConnectLogic.Feed.Logon += OnLogon;
            ConnectLogic.Feed.Logout += OnLogout;
            
            var connectionSuccessful = ConnectLogic.DoConnect();
            if (!connectionSuccessful)
            {
                Log.Warn("");
                return false;
            }
            var start = DateTime.Now;
            
            while (!IsConnected && (DateTime.Now - start).Seconds < 15)
            {
                Thread.Sleep(100);
            }

            return IsConnected;
        }

        private void OnCacheInitialize(object sender, CacheEventArgs e)
        {
            IsConnected = true;
        }

        public void Disconnect()
        {
            if (!IsConnected)
                return;
            //_logger.Warn("FdkConnector.Disconnecting");
            ConnectLogic.Dispose();
            IsConnected = false;
        }
        private void OnSymbolInfo(object sender, SymbolInfoEventArgs e)
        {
            _symbols = e.Information.ToList();
            Log.Debug("Symbols information is received. Symbols count = {0}", _symbols.Count);

            // to us means also that symbols are already availiable
            IsConnected = true;
        }

        private void OnSessionInfo(object sender, SessionInfoEventArgs e)
        {
            Log.Debug(e.Information);
        }
        private void OnLogon(object sender, LogonEventArgs e)
        {
            Log.Debug(string.Format("OnLogon(): {0}", e));
            
        }
        private void OnLogout(Object sender, LogoutEventArgs e)
        {
            //_logger.InfoFormat("OnLogout() ");
            IsConnected = false;
        }

        private List<SoftFX.Extended.SymbolInfo> _symbols = new List<SoftFX.Extended.SymbolInfo>();


        public string Address
        {
            get;
            set;
        }
        public string Login
        {
            get;
            set;
        }
		public string Password
		{
            get;
            set;
        }
		public string Path {
            get;
            set;
        }


        public FdkConnectLogic ConnectLogic { get; private set; }

        static readonly Logger Log = LogManager.GetCurrentClassLogger();
        public bool IsConnected { get; set; }

        public List<SoftFX.Extended.SymbolInfo> Symbols
        {
            get { return _symbols; }
            set { _symbols = value; }
        }
    }
}