// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace SoftFX.Extended.Generated
{
	internal class Params
	{
		private readonly IClient m_client;
		public Params(IClient client)
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
				return m_client.IsSupported(2);
			}
		}
		public bool Is_Create_Supported
		{
			get
			{
				return m_client.IsSupported(2, 0);
			}
		}
		public SoftFX.Lrp.LPtr Create()
		{
			using(MemoryBuffer buffer = m_client.Create())
			{

				int _status = m_client.Invoke(2, 0, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadLocalPointer();
				return _result;
			}
		}
		public bool Is_SetInt32_Supported
		{
			get
			{
				return m_client.IsSupported(2, 1);
			}
		}
		public void SetInt32(SoftFX.Lrp.LPtr handle, string key, int value)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteAString(key);
				buffer.WriteInt32(value);

				int _status = m_client.Invoke(2, 1, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_SetDouble_Supported
		{
			get
			{
				return m_client.IsSupported(2, 2);
			}
		}
		public void SetDouble(SoftFX.Lrp.LPtr handle, string key, double value)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteAString(key);
				buffer.WriteDouble(value);

				int _status = m_client.Invoke(2, 2, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_SetBoolean_Supported
		{
			get
			{
				return m_client.IsSupported(2, 3);
			}
		}
		public void SetBoolean(SoftFX.Lrp.LPtr handle, string key, bool value)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteAString(key);
				buffer.WriteBoolean(value);

				int _status = m_client.Invoke(2, 3, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_SetString_Supported
		{
			get
			{
				return m_client.IsSupported(2, 4);
			}
		}
		public void SetString(SoftFX.Lrp.LPtr handle, string key, string value)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteAString(key);
				buffer.WriteAString(value);

				int _status = m_client.Invoke(2, 4, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_ToText_Supported
		{
			get
			{
				return m_client.IsSupported(2, 5);
			}
		}
		public string ToText(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(2, 5, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadAString();
				return _result;
			}
		}
	}
}
