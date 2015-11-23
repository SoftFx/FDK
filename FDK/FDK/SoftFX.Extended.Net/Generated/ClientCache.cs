// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace SoftFX.Extended.Generated
{
	internal class ClientCache
	{
		private readonly IClient m_client;
		public ClientCache(IClient client)
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
				return m_client.IsSupported(4);
			}
		}
		public bool Is_GetSessionInfo_Supported
		{
			get
			{
				return m_client.IsSupported(4, 0);
			}
		}
		public SoftFX.Extended.SessionInfo GetSessionInfo(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(4, 0, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadSessionInfo();
				return _result;
			}
		}
	}
}
