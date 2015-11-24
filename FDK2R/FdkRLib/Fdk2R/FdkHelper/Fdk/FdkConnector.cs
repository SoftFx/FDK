using log4net;
using SoftFX.Extended;
using SoftFX.Extended.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RHost.Shared
{
    public class FdkConnector
    {
        static readonly ILog Log = LogManager.GetLogger(typeof(FdkConnector));
        public string Address { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public FdkConnectLogic ConnectLogic { get; private set; }

        public bool IsConnected { get; set; }

        List<SymbolInfo> _symbols = new List<SymbolInfo>();

        public FdkConnector()
        {
        }

        public bool Connect()
        {
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
            while (!IsConnected && (DateTime.Now - start).Seconds < 5)
            {
                Thread.Sleep(100);
            }
            
            return IsConnected;
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
            Log.Debug(string.Format("Symbols information is received. Symbols count = {0}", _symbols.Count));
            
            // to us means also that symbols are already availiable
            IsConnected = true;
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

        public Bar GetClosestBar(string symbol, DateTime startTime)
        {
            var bars = new Bars(ConnectLogic.Feed, symbol, PriceType.Bid, BarPeriod.S1, startTime, 1);
            var bar = bars.FirstOrDefault();
            return bar;
        }

        public List<SymbolInfo> GetSymbols()
        {
            return _symbols;
        }

        public Bar GetHistorical(string symbol)
        {
            var bars = ConnectLogic.Storage.Online.GetBars(symbol, PriceType.Ask, BarPeriod.M1, DateTime.Now, -1000).ToArray();
            /*
            var bars = new Bars(_connectLogic.Storage.Online, symbol,
                PriceType.Bid, BarPeriod.S1, DateTime.Now, -1000);
             */
            var bar = bars.FirstOrDefault();
            return bar;
        }
    }
}