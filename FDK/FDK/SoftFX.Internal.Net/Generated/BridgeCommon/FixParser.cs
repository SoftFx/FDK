// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace SoftFX.Internal.Generated.BridgeCommon
{
	internal class FixParser
	{
		private readonly IClient m_client;
		public FixParser(IClient client)
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
				return m_client.IsSupported(0);
			}
		}
		public bool Is_Create_Supported
		{
			get
			{
				return m_client.IsSupported(0, 0);
			}
		}
		public SoftFX.Lrp.LPtr Create(string fixDictionaryPath)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteAString(fixDictionaryPath);

				int _status = m_client.Invoke(0, 0, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadLocalPointer();
				return _result;
			}
		}
		public bool Is_Delete_Supported
		{
			get
			{
				return m_client.IsSupported(0, 1);
			}
		}
		public void Delete(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(0, 1, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_Parse_Supported
		{
			get
			{
				return m_client.IsSupported(0, 2);
			}
		}
		public SoftFX.Internal.FixParsingResult Parse(SoftFX.Lrp.LPtr handle, string message)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteAString(message);

				int _status = m_client.Invoke(0, 2, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadFFixParsingResult();
				return _result;
			}
		}
		public bool Is_GetSymbols_Supported
		{
			get
			{
				return m_client.IsSupported(0, 3);
			}
		}
		public string[] GetSymbols(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(0, 3, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadAStringArray();
				return _result;
			}
		}
		public bool Is_GetSessions_Supported
		{
			get
			{
				return m_client.IsSupported(0, 4);
			}
		}
		public SoftFX.Internal.FixSessionId[] GetSessions(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(0, 4, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadFFixSessionIdArray();
				return _result;
			}
		}
		public bool Is_TryGetQuote_Supported
		{
			get
			{
				return m_client.IsSupported(0, 5);
			}
		}
		public bool TryGetQuote(SoftFX.Lrp.LPtr handle, string symbol, SoftFX.Internal.FixSessionId sessionId, out SoftFX.Extended.Quote quote)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteAString(symbol);
				buffer.WriteFFixSessionId(sessionId);

				int _status = m_client.Invoke(0, 5, buffer);
				TypesSerializer.Throw(_status, buffer);

				quote = buffer.ReadFQuote();
				var _result = buffer.ReadBoolean();
				return _result;
			}
		}
	}
}
