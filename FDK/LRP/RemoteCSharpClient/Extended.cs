// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace RemoteCSharpClient
{
	internal class Extended
	{
		private readonly IClient m_client;
		public Extended(IClient client)
		{
			if(null == client)
			{
				throw new System.ArgumentNullException("client", "Client argument can not be null");
			}
			m_client = client;
		}
		public bool IsSupported
		{
			get
			{
				return m_client.IsSupported(1);
			}
		}
		public bool Is_Do_Supported
		{
			get
			{
				return m_client.IsSupported(1, 0);
			}
		}
		public RemoteCSharpClient.ReturnType Do(RemoteCSharpClient.InType inArg, ref RemoteCSharpClient.InOutType inOutArg, out RemoteCSharpClient.OutType outArg)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteInType(inArg);
				buffer.WriteInOutType(inOutArg);

				int _status = m_client.Invoke(1, 0, buffer);
				TypesSerializer.Throw(_status, buffer);

				inOutArg = buffer.ReadInOutType();
				outArg = buffer.ReadOutType();
				var _result = buffer.ReadReturnType();
				return _result;
			}
		}
		public bool Is_MarketBuy_Supported
		{
			get
			{
				return m_client.IsSupported(1, 1);
			}
		}
		public int MarketBuy(string symbol, double price, ref double volume, out double amount)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteAString(symbol);
				buffer.WriteDouble(price);
				buffer.WriteDouble(volume);

				int _status = m_client.Invoke(1, 1, buffer);
				TypesSerializer.Throw(_status, buffer);

				volume = buffer.ReadDouble();
				amount = buffer.ReadDouble();
				var _result = buffer.ReadInt32();
				return _result;
			}
		}
		public bool Is_Update_Supported
		{
			get
			{
				return m_client.IsSupported(1, 2);
			}
		}
		public void Update(ref System.Collections.Generic.SortedDictionary<string, double> reports, ref System.Collections.Generic.Dictionary<string, double> reports2)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WritePositionReports(reports);
				buffer.WritePositionReports2(reports2);

				int _status = m_client.Invoke(1, 2, buffer);
				TypesSerializer.Throw(_status, buffer);

				reports = buffer.ReadPositionReports();
				reports2 = buffer.ReadPositionReports2();
			}
		}
	}
}
