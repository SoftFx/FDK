// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace SoftFX.Extended.Generated
{
	internal class ClientServer
	{
		private readonly IClient m_client;
		public ClientServer(IClient client)
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
				return m_client.IsSupported(3);
			}
		}
		public bool Is_Start_Supported
		{
			get
			{
				return m_client.IsSupported(3, 0);
			}
		}
		public bool Start(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(3, 0, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadBoolean();
				return _result;
			}
		}
		public bool Is_WaitForLogon_Supported
		{
			get
			{
				return m_client.IsSupported(3, 1);
			}
		}
		public bool WaitForLogon(SoftFX.Lrp.LPtr handle, uint timeoutInMilliseconds)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteUInt32(timeoutInMilliseconds);

				int _status = m_client.Invoke(3, 1, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadBoolean();
				return _result;
			}
		}
		public bool Is_Shutdown_Supported
		{
			get
			{
				return m_client.IsSupported(3, 2);
			}
		}
		public int Shutdown(SoftFX.Lrp.LPtr instance)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(instance);

				int _status = m_client.Invoke(3, 2, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadInt32();
				return _result;
			}
		}
		public bool Is_Stop_Supported
		{
			get
			{
				return m_client.IsSupported(3, 3);
			}
		}
		public int Stop(SoftFX.Lrp.LPtr instance)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(instance);

				int _status = m_client.Invoke(3, 3, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadInt32();
				return _result;
			}
		}
		public bool Is_NextId_Supported
		{
			get
			{
				return m_client.IsSupported(3, 4);
			}
		}
		public string NextId(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(3, 4, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadAString();
				return _result;
			}
		}
		public bool Is_GetProtocolVersion_Supported
		{
			get
			{
				return m_client.IsSupported(3, 5);
			}
		}
		public string GetProtocolVersion(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(3, 5, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadAString();
				return _result;
			}
		}
		public bool Is_GetNextMessage_Supported
		{
			get
			{
				return m_client.IsSupported(3, 6);
			}
		}
		public bool GetNextMessage(SoftFX.Lrp.LPtr handle, out SoftFX.Extended.Core.FxMessage meessage)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(3, 6, buffer);
				TypesSerializer.Throw(_status, buffer);

				meessage = buffer.ReadMessage();
				var _result = buffer.ReadBoolean();
				return _result;
			}
		}
		public bool Is_DispatchMessage_Supported
		{
			get
			{
				return m_client.IsSupported(3, 7);
			}
		}
		public void DispatchMessage(SoftFX.Lrp.LPtr handle, SoftFX.Extended.Core.FxMessage message)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteMessage(message);

				int _status = m_client.Invoke(3, 7, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_GetNetworkActivity_Supported
		{
			get
			{
				return m_client.IsSupported(3, 8);
			}
		}
		public void GetNetworkActivity(SoftFX.Lrp.LPtr handle, out ulong dataBytesSent, out ulong sslBytesSent, out ulong dataBytesReceived, out ulong sslBytesReceived)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(3, 8, buffer);
				TypesSerializer.Throw(_status, buffer);

				dataBytesSent = buffer.ReadUInt64();
				sslBytesSent = buffer.ReadUInt64();
				dataBytesReceived = buffer.ReadUInt64();
				sslBytesReceived = buffer.ReadUInt64();
			}
		}
		public bool Is_GetSessionInfo_Supported
		{
			get
			{
				return m_client.IsSupported(3, 9);
			}
		}
		public SoftFX.Extended.SessionInfo GetSessionInfo(SoftFX.Lrp.LPtr handle, uint timeoutInMilliseconds)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteUInt32(timeoutInMilliseconds);

				int _status = m_client.Invoke(3, 9, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadSessionInfo();
				return _result;
			}
		}
		public bool Is_GetFileChunk_Supported
		{
			get
			{
				return m_client.IsSupported(3, 10);
			}
		}
		public SoftFX.Extended.FxFileChunk GetFileChunk(SoftFX.Lrp.LPtr handle, string fileId, int chunkId, uint timeoutInMilliseconds)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteAString(fileId);
				buffer.WriteInt32(chunkId);
				buffer.WriteUInt32(timeoutInMilliseconds);

				int _status = m_client.Invoke(3, 10, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadFileChunk();
				return _result;
			}
		}
	}
}
