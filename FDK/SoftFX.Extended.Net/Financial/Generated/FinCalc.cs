// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace SoftFX.Extended.Financial.Generated
{
	internal class FinCalcRaw
	{
		private readonly IClient m_client;
		public FinCalcRaw(IClient client)
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
				return m_client.IsSupported(1);
			}
		}
		public bool Is_Constructor_Supported
		{
			get
			{
				return m_client.IsSupported(1, 0);
			}
		}
		public SoftFX.Lrp.LPtr Constructor(string text)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteAString(text);

				int _status = m_client.Invoke(1, 0, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadLocalPointer();
				return _result;
			}
		}
		public bool Is_Destructor_Supported
		{
			get
			{
				return m_client.IsSupported(1, 1);
			}
		}
		public void Destructor(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(1, 1, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_Calculate_Supported
		{
			get
			{
				return m_client.IsSupported(1, 2);
			}
		}
		public void Calculate(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(1, 2, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_Clear_Supported
		{
			get
			{
				return m_client.IsSupported(1, 3);
			}
		}
		public void Clear(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(1, 3, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_Format_Supported
		{
			get
			{
				return m_client.IsSupported(1, 4);
			}
		}
		public string Format(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(1, 4, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadAString();
				return _result;
			}
		}
	}
}

namespace SoftFX.Extended.Financial.Generated
{
	internal class FinCalcProxy : System.IDisposable
	{
		public FinCalcRaw Instance { get; private set; }
		public LPtr Handle { get; private set; }
		public bool IsSupported
		{
			get
			{
				return this.Instance.IsSupported;
			}
		}
		internal FinCalcProxy(LocalClient client, LPtr handle)
		{
			this.Instance = new FinCalcRaw(client);
			this.Handle = handle;
		}
		public bool Is_Constructor_Supported
		{
			get
			{
				return this.Instance.Is_Constructor_Supported;
			}
		}
		public FinCalcProxy(LocalClient client, string text)
		{
			this.Instance = new FinCalcRaw(client);
			this.Handle = this.Instance.Constructor(text);
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
		public bool Is_Calculate_Supported
		{
			get
			{
				return this.Instance.Is_Calculate_Supported;
			}
		}
		public void Calculate()
		{
			this.Instance.Calculate(this.Handle);
		}
		public bool Is_Clear_Supported
		{
			get
			{
				return this.Instance.Is_Clear_Supported;
			}
		}
		public void Clear()
		{
			this.Instance.Clear(this.Handle);
		}
		public bool Is_Format_Supported
		{
			get
			{
				return this.Instance.Is_Format_Supported;
			}
		}
		public string Format()
		{
			return this.Instance.Format(this.Handle);
		}
	}
}
