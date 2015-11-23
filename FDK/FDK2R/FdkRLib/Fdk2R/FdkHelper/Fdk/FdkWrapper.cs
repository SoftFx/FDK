using log4net;
using SoftFX.Extended;
using SoftFX.Extended.Events;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace RHost.Shared
{
    public class FdkWrapper
    {
        static readonly ILog Log = LogManager.GetLogger(typeof(FdkWrapper));

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
        void OnSymbolInfo(object sender, SymbolInfoEventArgs e)
        {
            _symbols = e.Information.ToList();
            Log.DebugFormat("Symbols information is received. Symbols count = {0}", _symbols.Count);

            // to us means also that symbols are already availiable
            IsConnected = true;
        }

        public Dictionary<string, SymbolInfo> GetSymbolsDict()
        {
            var result = new Dictionary<string, SymbolInfo>();
            foreach(var sym in _symbols)
            {
                result[sym.Name] = sym;
            }
            return result;
        }

        void OnSessionInfo(object sender, SessionInfoEventArgs e)
        {
            Log.Debug(e.Information);
        }
        void OnLogon(object sender, LogonEventArgs e)
        {
            Log.Debug(string.Format("OnLogon(): {0}", e));

        }
        void OnLogout(Object sender, LogoutEventArgs e)
        {
            //_logger.InfoFormat("OnLogout() ");
            IsConnected = false;
        }

        List<SymbolInfo> _symbols = new List<SymbolInfo>();


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

        public bool IsConnected { get; set; }

        public List<SoftFX.Extended.SymbolInfo> Symbols
        {
            get { return _symbols; }
            set { _symbols = value; }
        }
    }
}