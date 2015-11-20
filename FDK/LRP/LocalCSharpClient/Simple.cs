// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace LocalCSharp
{
	internal class Simple
	{
		private readonly IClient m_client;
		public Simple(IClient client)
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
		public bool Is_Constructor_Supported
		{
			get
			{
				return m_client.IsSupported(0, 0);
			}
		}
		public SoftFX.Lrp.LPtr Constructor()
		{
			using(MemoryBuffer buffer = m_client.Create())
			{

				int _status = m_client.Invoke(0, 0, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadLocalPointer();
				return _result;
			}
		}
		public bool Is_Inverse_Supported
		{
			get
			{
				return m_client.IsSupported(0, 1);
			}
		}
		public string Inverse(string text)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteAString(text);

				int _status = m_client.Invoke(0, 1, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadAString();
				return _result;
			}
		}
		public bool Is_Factorial_Supported
		{
			get
			{
				return m_client.IsSupported(0, 2);
			}
		}
		public bool Factorial(int value, out int result)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteInt32(value);

				int _status = m_client.Invoke(0, 2, buffer);
				TypesSerializer.Throw(_status, buffer);

				result = buffer.ReadInt32();
				var _result = buffer.ReadBoolean();
				return _result;
			}
		}
	}
}
