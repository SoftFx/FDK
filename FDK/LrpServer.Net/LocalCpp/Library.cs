// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace LrpServer.Net.LocalCpp
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
				return m_client.IsSupported(0);
			}
		}
		public bool Is_SetDotNetDllPath_Supported
		{
			get
			{
				return m_client.IsSupported(0, 0);
			}
		}
		public void SetDotNetDllPath(string path)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteAString(path);

				int _status = m_client.Invoke(0, 0, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
	}
}
