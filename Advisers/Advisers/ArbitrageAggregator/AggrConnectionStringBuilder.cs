using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftFX;

namespace ArbitrageAggregator
{
	internal static class AggrConnectionStringBuilder
	{
		internal static Dictionary<int, ConnectionStrings> CreateConnectionsStrings()
		{
			Settings s = Settings.Default;
            int[] arrayBankCodes = s.BankCodes.Split(';').Select(p => Int32.Parse(p)).ToArray();
            Dictionary<int, ConnectionStrings> result = new Dictionary<int, ConnectionStrings>();
            foreach (int currBankCode in arrayBankCodes)
            {
                result[currBankCode] = CreateConnectionStrings(currBankCode);
            }
			return result;
		}


		private static string CreateConnectionString(string type, int bankCode)
		{
			Settings s = Settings.Default;
			StringBuilder builder = new StringBuilder("[String]ProtocolType=Aggr;");
			builder.AppendFormat("[String]Type={0};", type);
			builder.AppendFormat("[String]FeederAddress={0};", s.FeederAddress);
			builder.AppendFormat("[Int32]FeederPort={0};", s.FeederPort);
			builder.AppendFormat("[String]FeederUsername={0};", s.FeederUsername);
			builder.AppendFormat("[String]FeederPassword={0};", s.FeederPassword);

			builder.AppendFormat("[String]BridgeAddress={0};", s.BridgeAddress);
			builder.AppendFormat("[Int32]BridgePort={0};", s.BridgePort);
			builder.AppendFormat("[String]BridgeUsername={0};", s.BridgeUsername);
			builder.AppendFormat("[String]BridgePassword={0};", s.BridgePassword);
            builder.AppendFormat("[Int32]BankCode={0};", bankCode);
            builder.AppendFormat("[Int32]MetaAccount=0;");
			string result = builder.ToString();
			return result;
		}
		private static ConnectionStrings CreateConnectionStrings(int bankCode)
		{
			string feed = CreateConnectionString("Feed", bankCode);
			string trade = CreateConnectionString("Trade", bankCode);
			ConnectionStrings result = new ConnectionStrings(feed, trade);
			return result;
		}
	}
}
