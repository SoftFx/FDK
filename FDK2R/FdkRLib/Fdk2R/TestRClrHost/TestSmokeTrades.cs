using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using NUnit.Framework;
using RHost;
using SoftFX.Extended;
using SoftFX.Extended.Events;

namespace TestRClrHost
{
    [TestFixture]
    public class TestSmokeTrades
	{		
		static DataTrade Trade
		{
			get { return FdkHelper.Wrapper.ConnectLogic.TradeWrapper.Trade; }
		}
        [Test]
        public void TestGetTradeRecords()
        {
            Assert.AreEqual(0, FdkHelper.ConnectToFdk("tp.st.soft-fx.eu", "100065", "123qwe!", ""));
            //Assert.AreEqual(0, FdkHelper.ConnectToFdk("", "", "", ""));
            TradeRecord[] tradeRecords = Trade.Server.GetTradeRecords()
                 .ToArray();
            var bars = FdkTrade.GetTradeRecords(new DateTime(1970, 1, 2), DateTime.UtcNow);
            var comission = FdkTrade.GetTradeAgentCommission(bars);
            var profit = FdkTrade.GetTradeTakeProfit(bars);
            FdkVars.Unregister(bars);
		}     
		[Test]
		public void TestTradeRecordsForPips()
		{
            //Assert.AreEqual(0, FdkHelper.ConnectToFdk("tp.dev.soft-fx.eu", "100106", "123qwe123", ""));
            //Assert.AreEqual(0, FdkHelper.ConnectToFdk("", "", "", ""));
            Assert.AreEqual(0, FdkStatic.ConnectToFdk("tp.st.soft-fx.eu", "100065", "123qwe!", ""));
            var calculator = FdkStatic.Calculator;
            var symbols = FdkSymbolInfo.Symbols;
            var symFirst = symbols.First();

            FdkSymbolInfo.RegisterToFeed(FdkSymbolInfo.Feed, calculator);
            Thread.Sleep(1000);
            double volumeByHand = FdkSymbolInfo.CalculatePipsValue(symFirst);
		}
 	
        [Test]
        public void TestSelectSpeed()
        {
        	var sz = 10000000;
			var randData = new int[sz];
			var random = new Random();
			for(var i = 0; i<sz; i++)
			{
				randData[i] = random.Next(1, 10);
			}
			var sw = Stopwatch.StartNew();
			for(int t = 0; t<5;t++){
				var arr = randData.SelectToArray(i => i);
			}
			var time1 = sw.ElapsedMilliseconds;
			sw.Stop();
			sw.Restart();
			for(int t = 0; t<5;t++){
				var arr = randData.Select(i => i).ToArray();
			}
			var time2 = sw.ElapsedMilliseconds;
			Console.WriteLine("Time 1: {0}", time1);
			Console.WriteLine("Time 2: {0}", time2);
			Assert.IsTrue(time1 < time2, "Time " + time1 + "should be smaller than: " + time2);
        }
        [Test]
        public void TestGetTradeRecordsFromStaging()
        {
            Assert.AreEqual(0, FdkHelper.ConnectToFdk("tp.st.soft-fx.eu", "100000", "123321", ""));
            //Assert.AreEqual(0, FdkHelper.ConnectToFdk("", "", "", ""));
            var bars = FdkTrade.GetTradeRecords(new DateTime(1970, 1, 2), DateTime.UtcNow);
            var comission = FdkTrade.GetTradeAgentCommission(bars);
            FdkVars.Unregister(bars);
        }
        [Test]
        public void TestGetTradeReportsAll()
        {
            //Assert.AreEqual(0, FdkHelper.ConnectToFdk("tp.st.soft-fx.eu", "100000", "123321", ""));
			Assert.AreEqual(0, FdkHelper.ConnectToFdk("tp.st.soft-fx.eu", "100131", "123qwe!", ""));
            var bars = FdkTradeReports.GetTradeTransactionReportAll();
            var comission = FdkTradeReports.GetTradeComment(bars);
            FdkVars.Unregister(bars);
        }

        [Test]
        public void TestDataTradeIsolation()
        {
            const string address = "tpdemo.fxopen.com";
            const string username = "59932";
            const string password = "8mEx7zZ2";

            EnsureDirectoriesCreated();

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

                FixLogDirectory = LogPath,
                FixEventsFileName = string.Format("{0}.trade.events.log", username),
                FixMessagesFileName = string.Format("{0}.trade.messages.log", username)
            };
            var trade = new DataTrade
            {
                SynchOperationTimeout = 30000
            };
            var connectionString = builder.ToString();
            trade.Initialize(connectionString);
            trade.Logon += OnLogon;
            trade.Start();
            var timeoutInMilliseconds = trade.SynchOperationTimeout;
            if (!_syncEvent.WaitOne(timeoutInMilliseconds))
            {
                throw new TimeoutException("Timeout of logon waiting has been reached");
            }
            RunExample(trade);

            trade.Dispose();
        }

        private static void RunExample(DataTrade trade)
        {
            var records = trade.Server.GetTradeRecords();
        }

        static void EnsureDirectoriesCreated()
        {
            if (!Directory.Exists(LogPath))
                Directory.CreateDirectory(LogPath);
        }

        readonly AutoResetEvent _syncEvent = new AutoResetEvent(false);
        private void OnLogon(object sender, LogonEventArgs e)
        {
            _syncEvent.Set();
        }

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

    }
}