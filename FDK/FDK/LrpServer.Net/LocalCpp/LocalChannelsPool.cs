// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace LrpServer.Net.LocalCpp
{
	internal class LocalChannelsPoolRaw
	{
		private readonly IClient m_client;
		public LocalChannelsPoolRaw(IClient client)
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
		public bool Is_Constructor_Supported
		{
			get
			{
				return m_client.IsSupported(2, 0);
			}
		}
		public SoftFX.Lrp.LPtr Constructor(LrpServer.Net.LrpParams parameters)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLrpParams(parameters);

				int _status = m_client.Invoke(2, 0, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadLocalPointer();
				return _result;
			}
		}
		public bool Is_Destructor_Supported
		{
			get
			{
				return m_client.IsSupported(2, 1);
			}
		}
		public void Destructor(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(2, 1, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
	}
}

namespace LrpServer.Net.LocalCpp
{
	internal class LocalChannelsPoolProxy : System.IDisposable
	{
		public LocalChannelsPoolRaw Instance { get; private set; }
		public LPtr Handle { get; private set; }
		public bool IsSupported
		{
			get
			{
				return this.Instance.IsSupported;
			}
		}
		internal LocalChannelsPoolProxy(LocalClient client, LPtr handle)
		{
			this.Instance = new LocalChannelsPoolRaw(client);
			this.Handle = handle;
		}
		public bool Is_Constructor_Supported
		{
			get
			{
				return this.Instance.Is_Constructor_Supported;
			}
		}
		public LocalChannelsPoolProxy(LocalClient client, LrpServer.Net.LrpParams parameters)
		{
			this.Instance = new LocalChannelsPoolRaw(client);
			this.Handle = this.Instance.Constructor(parameters);
		}
		public bool Is_Destructor_Supported
		{
			get
			{
				return this.Instance.Is_Destructor_Supported;
			}
		}
		public void Dispose()
		{
			if(!this.Handle.IsZero)
			{
				this.Instance.Destructor(this.Handle);
				this.Handle.Clear();
			}
		}
	}
}
