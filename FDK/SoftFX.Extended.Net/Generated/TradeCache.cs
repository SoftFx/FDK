// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace SoftFX.Extended.Generated
{
	internal class TradeCache
	{
		private readonly IClient m_client;
		public TradeCache(IClient client)
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
				return m_client.IsSupported(8);
			}
		}
		public bool Is_GetAccountInfo_Supported
		{
			get
			{
				return m_client.IsSupported(8, 0);
			}
		}
		public SoftFX.Extended.AccountInfo GetAccountInfo(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(8, 0, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadAccountInfo();
				return _result;
			}
		}
		public bool Is_GetRecords_Supported
		{
			get
			{
				return m_client.IsSupported(8, 1);
			}
		}
		public SoftFX.Extended.Data.FxOrder[] GetRecords(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(8, 1, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadFxOrderArray();
				return _result;
			}
		}
		public bool Is_GetPositions_Supported
		{
			get
			{
				return m_client.IsSupported(8, 2);
			}
		}
		public SoftFX.Extended.Position[] GetPositions(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(8, 2, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadPositionArray();
				return _result;
			}
		}
	}
}
