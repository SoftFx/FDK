// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace SoftFX.Internal.Generated.LrpProvider
{
	internal class LrpCodecRaw
	{
		private readonly IClient m_client;
		public LrpCodecRaw(IClient client)
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
		public bool Is_Destructor_Supported
		{
			get
			{
				return m_client.IsSupported(0, 1);
			}
		}
		public void Destructor(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(0, 1, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_GetCount_Supported
		{
			get
			{
				return m_client.IsSupported(0, 2);
			}
		}
		public long GetCount(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(0, 2, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadInt64();
				return _result;
			}
		}
		public bool Is_GetSize_Supported
		{
			get
			{
				return m_client.IsSupported(0, 3);
			}
		}
		public long GetSize(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(0, 3, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadInt64();
				return _result;
			}
		}
		public bool Is_GetTime_Supported
		{
			get
			{
				return m_client.IsSupported(0, 4);
			}
		}
		public double GetTime(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(0, 4, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadDouble();
				return _result;
			}
		}
		public bool Is_EncodeRaw_Supported
		{
			get
			{
				return m_client.IsSupported(0, 5);
			}
		}
		public void EncodeRaw(SoftFX.Lrp.LPtr handle, SoftFX.Extended.Quote[] quotes)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteQuoteArray(quotes);

				int _status = m_client.Invoke(0, 5, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_EncodeSlow_Supported
		{
			get
			{
				return m_client.IsSupported(0, 6);
			}
		}
		public void EncodeSlow(SoftFX.Lrp.LPtr handle, SoftFX.Extended.Quote[] quotes)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteQuoteArray(quotes);

				int _status = m_client.Invoke(0, 6, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_EncodeFast_Supported
		{
			get
			{
				return m_client.IsSupported(0, 7);
			}
		}
		public void EncodeFast(SoftFX.Lrp.LPtr handle, uint precision, double volumeStep, SoftFX.Extended.Quote[] quotes)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteUInt32(precision);
				buffer.WriteDouble(volumeStep);
				buffer.WriteQuoteArray(quotes);

				int _status = m_client.Invoke(0, 7, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_Clear_Supported
		{
			get
			{
				return m_client.IsSupported(0, 8);
			}
		}
		public void Clear(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(0, 8, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
	}
}

namespace SoftFX.Internal.Generated.LrpProvider
{
	internal class LrpCodecProxy : System.IDisposable
	{
		public LrpCodecRaw Instance { get; private set; }
		public LPtr Handle { get; private set; }
		public bool IsSupported
		{
			get
			{
				return this.Instance.IsSupported;
			}
		}
		internal LrpCodecProxy(LocalClient client, LPtr handle)
		{
			this.Instance = new LrpCodecRaw(client);
			this.Handle = handle;
		}
		public bool Is_Constructor_Supported
		{
			get
			{
				return this.Instance.Is_Constructor_Supported;
			}
		}
		public LrpCodecProxy(LocalClient client)
		{
			this.Instance = new LrpCodecRaw(client);
			this.Handle = this.Instance.Constructor();
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
		public bool Is_GetCount_Supported
		{
			get
			{
				return this.Instance.Is_GetCount_Supported;
			}
		}
		public long GetCount()
		{
			return this.Instance.GetCount(this.Handle);
		}
		public bool Is_GetSize_Supported
		{
			get
			{
				return this.Instance.Is_GetSize_Supported;
			}
		}
		public long GetSize()
		{
			return this.Instance.GetSize(this.Handle);
		}
		public bool Is_GetTime_Supported
		{
			get
			{
				return this.Instance.Is_GetTime_Supported;
			}
		}
		public double GetTime()
		{
			return this.Instance.GetTime(this.Handle);
		}
		public bool Is_EncodeRaw_Supported
		{
			get
			{
				return this.Instance.Is_EncodeRaw_Supported;
			}
		}
		public void EncodeRaw(SoftFX.Extended.Quote[] quotes)
		{
			this.Instance.EncodeRaw(this.Handle, quotes);
		}
		public bool Is_EncodeSlow_Supported
		{
			get
			{
				return this.Instance.Is_EncodeSlow_Supported;
			}
		}
		public void EncodeSlow(SoftFX.Extended.Quote[] quotes)
		{
			this.Instance.EncodeSlow(this.Handle, quotes);
		}
		public bool Is_EncodeFast_Supported
		{
			get
			{
				return this.Instance.Is_EncodeFast_Supported;
			}
		}
		public void EncodeFast(uint precision, double volumeStep, SoftFX.Extended.Quote[] quotes)
		{
			this.Instance.EncodeFast(this.Handle, precision, volumeStep, quotes);
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
	}
}
