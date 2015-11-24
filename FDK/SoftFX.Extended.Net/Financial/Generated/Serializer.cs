// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace SoftFX.Extended.Financial.Generated
{
	internal class Serializer
	{
		private readonly IClient m_client;
		public Serializer(IClient client)
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
		public bool Is_Serialize_Supported
		{
			get
			{
				return m_client.IsSupported(0, 0);
			}
		}
		public string Serialize(SoftFX.Extended.Financial.Serialization.CalculatorData calc)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteCalculatorData(calc);

				int _status = m_client.Invoke(0, 0, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadAString();
				return _result;
			}
		}
		public bool Is_Deserialize_Supported
		{
			get
			{
				return m_client.IsSupported(0, 1);
			}
		}
		public SoftFX.Extended.Financial.Serialization.CalculatorData Deserialize(string text)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteAString(text);

				int _status = m_client.Invoke(0, 1, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadCalculatorData();
				return _result;
			}
		}
	}
}
