// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace SoftFX.Extended.Generated
{
	internal class FeedCache
	{
		private readonly IClient m_client;
		public FeedCache(IClient client)
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
				return m_client.IsSupported(6);
			}
		}
		public bool Is_GetSymbols_Supported
		{
			get
			{
				return m_client.IsSupported(6, 0);
			}
		}
		public SoftFX.Extended.SymbolInfo[] GetSymbols(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(6, 0, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadSymbolInfoArray();
				return _result;
			}
		}
		public bool Is_TryGetBid_Supported
		{
			get
			{
				return m_client.IsSupported(6, 1);
			}
		}
		public bool TryGetBid(SoftFX.Lrp.LPtr handle, string symbol, out double price, out double volume, out System.DateTime creationTime)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteAString(symbol);

				int _status = m_client.Invoke(6, 1, buffer);
				TypesSerializer.Throw(_status, buffer);

				price = buffer.ReadDouble();
				volume = buffer.ReadDouble();
				creationTime = buffer.ReadTime();
				var _result = buffer.ReadBoolean();
				return _result;
			}
		}
		public bool Is_TryGetAsk_Supported
		{
			get
			{
				return m_client.IsSupported(6, 2);
			}
		}
		public bool TryGetAsk(SoftFX.Lrp.LPtr handle, string symbol, out double price, out double volume, out System.DateTime creationTime)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteAString(symbol);

				int _status = m_client.Invoke(6, 2, buffer);
				TypesSerializer.Throw(_status, buffer);

				price = buffer.ReadDouble();
				volume = buffer.ReadDouble();
				creationTime = buffer.ReadTime();
				var _result = buffer.ReadBoolean();
				return _result;
			}
		}
		public bool Is_TryGetQuote_Supported
		{
			get
			{
				return m_client.IsSupported(6, 3);
			}
		}
		public bool TryGetQuote(SoftFX.Lrp.LPtr handle, string symbol, out SoftFX.Extended.Quote quote)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteAString(symbol);

				int _status = m_client.Invoke(6, 3, buffer);
				TypesSerializer.Throw(_status, buffer);

				quote = buffer.ReadQuote();
				var _result = buffer.ReadBoolean();
				return _result;
			}
		}
		public bool Is_GetCurrencies_Supported
		{
			get
			{
				return m_client.IsSupported(6, 4);
			}
		}
		public SoftFX.Extended.CurrencyInfo[] GetCurrencies(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(6, 4, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadCurrencyInfoArray();
				return _result;
			}
		}
	}
}
