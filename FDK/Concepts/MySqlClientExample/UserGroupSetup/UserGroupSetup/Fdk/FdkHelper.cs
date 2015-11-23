using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using NLog;
using RHost.Shared;
using System.Collections.Generic;
using TickTrader.Common.Business;
using System.Linq;

namespace RHost
{
	public class FdkHelper
	{
		static readonly Logger Log = LogManager.GetCurrentClassLogger();
        static FdkHelper()
        {
            Wrapper = new FdkWrapper();
        }


        public static HistoryBar GetTTQuotesHistory(string symbol, DateTime dateTime, List<string> quoteSymbols, string address, long login)
        {
            QuotesHistoryConnector.Instance.Address = address;
            QuotesHistoryConnector.Instance.StorageFolderName = address + "_" + login;
            QuotesHistoryConnector.Instance.SetSymbols(quoteSymbols);
            QuotesHistoryConnector.Instance.Reconnect();

            return QuotesHistoryConnector.Instance.GetHistory(symbol, dateTime);
        }

        static decimal QSConvertFactorToUSD (List<string>  qsSymbolNames, DateTime dateTime, string symbolName, decimal price, string address, long login) 
        {
            if (string.IsNullOrEmpty(symbolName))
            {
                return 0;
            }

            symbolName = symbolName.Replace("\\", "").Replace("/", "");
            symbolName = symbolName.Replace("GOLD", "XAUUSD");
            symbolName = symbolName.Replace("SILVER", "XAGUSD");

            if (symbolName.IndexOf("USD") == 0)
            {
                Log.Debug("QSConvertFactorToUSD({0}, {1}, {2}) returns {3}", dateTime.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), symbolName, price, 1);
                return 1;
            }

            if (symbolName.IndexOf("USD") > 0 && price != 0)
            {
                Log.Debug("QSConvertFactorToUSD({0}, {1}, {2}) returns {3}", dateTime.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), symbolName, price, price);
                return price;
            }

            decimal result = 0;

            var qsSymbolName = qsSymbolNames.Where(i => i.IndexOf("USD") >= 0 && i.IndexOf(symbolName.Substring(0, 3)) >= 0).FirstOrDefault();

            if (qsSymbolName == null)
            {
                Log.Debug("QSConvertFactorToUSD() Can not find cross quote for symbol {0} to USD", symbolName);
                return 0;
            }

            var bar = GetTTQuotesHistory(qsSymbolName, dateTime, qsSymbolNames, address, login);

            if (bar != null)
            {
                result = (decimal)((bar.Low + bar.Hi) / (decimal)2);

                Log.Debug("QSConvertFactorToUSD() using bar {0} {1} {2} for symbol {3}", bar.Time.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), qsSymbolName, result, symbolName);

                if (qsSymbolName.IndexOf("USD") == 0 && result != 0)
                {
                    result = 1 / result;
                }
            }

            Log.Debug("QSConvertFactorToUSD({0}, {1}, {2}) returns {3}", dateTime.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), symbolName, price, result);
            return result;
        }

        public static int ConnectToFdk(string address, string login, string password, string path)
		{
			#if DEBUG
			//Library.Path = @"C:\Users\ciprian.khlud\Documents\R\win-library\3.2\FdkRLib\data";
			#endif

			Log.Info("FdkHelper.ConnectToFdk( address: {0}, login: {1}, password: {2}, path: {3})",
				address, login, password, path);
            //Debugger.Launch();

            var addr = String.IsNullOrEmpty(address)
			? "tpdemo.fxopen.com"
                : address;
			var loginStr = String.IsNullOrEmpty(login)
			? "59932"
                : login;
			var passwordString = String.IsNullOrEmpty(login)
			? "8mEx7zZ2"
                : password;

			try
			{
				Wrapper.Address = addr;
				Wrapper.Login = loginStr;
				Wrapper.Password = passwordString;
				var localPath = String.Empty;

				if (!String.IsNullOrEmpty(path))
				{
					var localPathInfo = new DirectoryInfo(path);
					localPath = localPathInfo.FullName;
				}

                Wrapper.Path = localPath;

                Wrapper.SetupBuilder();

				return Wrapper.Connect() ? 0 : -1;
			}
			catch (Exception ex)
			{
				Log.Error(ex);
				throw;
			}
        }

        public static FdkWrapper Wrapper { get; set; }

        public static void Disconnect()
        {
            Wrapper.Disconnect();
        }
        public static void WriteMessage(string message)
        {
			Console.WriteLine("FdkRLib: {0}", message);
        }

        public static Double GetCreatedEpoch(DateTime created)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
            var span = (created.ToLocalTime() - epoch);
            return span.TotalSeconds;
        }

        public static Double GetCreatedEpochFromText(string createdTimeStr)
        {
            var created = DateTime.Parse(createdTimeStr, CultureInfo.InvariantCulture);
            return GetCreatedEpoch(created);
        }

        public static void DisplayDate(DateTime time)
        {
            MessageBox.Show(time.ToString());
        }


        public static DateTime GetCreatedEpoch(Double value)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
            var created = epoch.AddSeconds(value);
            return created;
        }


        public static bool IsTimeZero(DateTime startTime)
        {
            return startTime.Year == 1970 && startTime.Month == 1;
        }

        #region Accessors

        public static T? ParseEnumStr<T>(string text) where T : struct
        {
            T result;
			if (Enum.TryParse(text, out result))
				return result;
			else 
				return null;
        }

        public static T GetFieldByName<T>(string fieldName)
        {
            var barPeriodField = typeof(T).GetField(fieldName);
            if (barPeriodField == null)
                return default(T);

            var result = (T)barPeriodField.GetValue(null);

            return result;
        }
        #endregion
    }

}
 