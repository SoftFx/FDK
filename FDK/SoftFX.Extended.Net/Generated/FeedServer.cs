// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace SoftFX.Extended.Generated
{
	internal class FeedServer
	{
		private readonly IClient m_client;
		public FeedServer(IClient client)
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
				return m_client.IsSupported(5);
			}
		}
		public bool Is_Create_Supported
		{
			get
			{
				return m_client.IsSupported(5, 0);
			}
		}
		public SoftFX.Lrp.LPtr Create(string connectionString)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteAString(connectionString);

				int _status = m_client.Invoke(5, 0, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadLocalPointer();
				return _result;
			}
		}
		public bool Is_GetQuoteHistoryFiles_Supported
		{
			get
			{
				return m_client.IsSupported(5, 1);
			}
		}
		public SoftFX.Extended.DataHistoryInfo GetQuoteHistoryFiles(SoftFX.Lrp.LPtr handle, string symbol, bool includeLevel2, System.DateTime time, uint timeoutInMilliseconds)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteAString(symbol);
				buffer.WriteBoolean(includeLevel2);
				buffer.WriteTime(time);
				buffer.WriteUInt32(timeoutInMilliseconds);

				int _status = m_client.Invoke(5, 1, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadDataHistoryInfo();
				return _result;
			}
		}
		public bool Is_GetBarsHistoryFiles_Supported
		{
			get
			{
				return m_client.IsSupported(5, 2);
			}
		}
		public SoftFX.Extended.DataHistoryInfo GetBarsHistoryFiles(SoftFX.Lrp.LPtr handle, string symbol, SoftFX.Extended.PriceType priceType, string period, System.DateTime time, uint timeoutInMilliseconds)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteAString(symbol);
				buffer.WritePriceType(priceType);
				buffer.WriteAString(period);
				buffer.WriteTime(time);
				buffer.WriteUInt32(timeoutInMilliseconds);

				int _status = m_client.Invoke(5, 2, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadDataHistoryInfo();
				return _result;
			}
		}
		public bool Is_GetCurrencies_Supported
		{
			get
			{
				return m_client.IsSupported(5, 3);
			}
		}
		public SoftFX.Extended.CurrencyInfo[] GetCurrencies(SoftFX.Lrp.LPtr handle, uint timeoutInMilliseconds)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteUInt32(timeoutInMilliseconds);

				int _status = m_client.Invoke(5, 3, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadCurrencyInfoArray();
				return _result;
			}
		}
		public bool Is_GetSymbols_Supported
		{
			get
			{
				return m_client.IsSupported(5, 4);
			}
		}
		public SoftFX.Extended.SymbolInfo[] GetSymbols(SoftFX.Lrp.LPtr handle, uint timeoutInMilliseconds)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteUInt32(timeoutInMilliseconds);

				int _status = m_client.Invoke(5, 4, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadSymbolInfoArray();
				return _result;
			}
		}
		public bool Is_SubscribeToQuotes_Supported
		{
			get
			{
				return m_client.IsSupported(5, 5);
			}
		}
		public void SubscribeToQuotes(SoftFX.Lrp.LPtr handle, string[] symbols, int depth, uint timeoutInMilliseconds)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteStringArray(symbols);
				buffer.WriteInt32(depth);
				buffer.WriteUInt32(timeoutInMilliseconds);

				int _status = m_client.Invoke(5, 5, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_UnsubscribeQuotes_Supported
		{
			get
			{
				return m_client.IsSupported(5, 6);
			}
		}
		public void UnsubscribeQuotes(SoftFX.Lrp.LPtr handle, string[] symbols, uint timeoutInMilliseconds)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteStringArray(symbols);
				buffer.WriteUInt32(timeoutInMilliseconds);

				int _status = m_client.Invoke(5, 6, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_GetBarsHistoryMetaInfoFile_Supported
		{
			get
			{
				return m_client.IsSupported(5, 7);
			}
		}
		public string GetBarsHistoryMetaInfoFile(SoftFX.Lrp.LPtr handle, string symbol, SoftFX.Extended.PriceType priceType, string period, uint timeoutInMilliseconds)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteAString(symbol);
				buffer.WritePriceType(priceType);
				buffer.WriteAString(period);
				buffer.WriteUInt32(timeoutInMilliseconds);

				int _status = m_client.Invoke(5, 7, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadAString();
				return _result;
			}
		}
		public bool Is_GetQuotesHistoryMetaInfoFile_Supported
		{
			get
			{
				return m_client.IsSupported(5, 8);
			}
		}
		public string GetQuotesHistoryMetaInfoFile(SoftFX.Lrp.LPtr handle, string symbol, bool includeLevel2, uint timeoutInMilliseconds)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteAString(symbol);
				buffer.WriteBoolean(includeLevel2);
				buffer.WriteUInt32(timeoutInMilliseconds);

				int _status = m_client.Invoke(5, 8, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadAString();
				return _result;
			}
		}
		public bool Is_GetHistoryBars_Supported
		{
			get
			{
				return m_client.IsSupported(5, 9);
			}
		}
		public SoftFX.Extended.DataHistoryInfo GetHistoryBars(SoftFX.Lrp.LPtr handle, string symbol, System.DateTime time, int barsNumber, SoftFX.Extended.PriceType priceType, string period, uint timeoutInMilliseconds)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteAString(symbol);
				buffer.WriteTime(time);
				buffer.WriteInt32(barsNumber);
				buffer.WritePriceType(priceType);
				buffer.WriteAString(period);
				buffer.WriteUInt32(timeoutInMilliseconds);

				int _status = m_client.Invoke(5, 9, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadDataHistoryInfo();
				return _result;
			}
		}
		public bool Is_GetQueueThreshold_Supported
		{
			get
			{
				return m_client.IsSupported(5, 10);
			}
		}
		public int GetQueueThreshold(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(5, 10, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadInt32();
				return _result;
			}
		}
		public bool Is_SetQueueThreshold_Supported
		{
			get
			{
				return m_client.IsSupported(5, 11);
			}
		}
		public void SetQueueThreshold(SoftFX.Lrp.LPtr handle, int newSize)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteInt32(newSize);

				int _status = m_client.Invoke(5, 11, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_GetQuotesHistoryVersion_Supported
		{
			get
			{
				return m_client.IsSupported(5, 12);
			}
		}
		public int GetQuotesHistoryVersion(SoftFX.Lrp.LPtr handle, uint timeoutInMilliseconds)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteUInt32(timeoutInMilliseconds);

				int _status = m_client.Invoke(5, 12, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadInt32();
				return _result;
			}
		}
	}
}
