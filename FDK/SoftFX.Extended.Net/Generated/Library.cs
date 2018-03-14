// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace SoftFX.Extended.Generated
{
	internal class Library
	{
		private readonly IClient m_client;
		public Library(IClient client)
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
				return m_client.IsSupported(11);
			}
		}
		public bool Is_WriteNormalDumpOnError_Supported
		{
			get
			{
				return m_client.IsSupported(11, 0);
			}
		}
		public void WriteNormalDumpOnError(string path)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteWString(path);

				int _status = m_client.Invoke(11, 0, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_WriteFullDumpOnError_Supported
		{
			get
			{
				return m_client.IsSupported(11, 1);
			}
		}
		public void WriteFullDumpOnError(string path)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteWString(path);

				int _status = m_client.Invoke(11, 1, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_WriteNormalDump_Supported
		{
			get
			{
				return m_client.IsSupported(11, 2);
			}
		}
		public void WriteNormalDump(string path)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteWString(path);

				int _status = m_client.Invoke(11, 2, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_WriteFullDump_Supported
		{
			get
			{
				return m_client.IsSupported(11, 3);
			}
		}
		public void WriteFullDump(string path)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteWString(path);

				int _status = m_client.Invoke(11, 3, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
	}
}
