// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace SoftFX.Extended.Generated
{
	internal class Handle
	{
		private readonly IClient m_client;
		public Handle(IClient client)
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
		public bool Is_Delete_Supported
		{
			get
			{
				return m_client.IsSupported(0, 0);
			}
		}
		public void Delete(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(0, 0, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_NotificationFromHandle_Supported
		{
			get
			{
				return m_client.IsSupported(0, 1);
			}
		}
		public SoftFX.Extended.Data.Notification NotificationFromHandle(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(0, 1, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadNotification();
				return _result;
			}
		}
	}
}
