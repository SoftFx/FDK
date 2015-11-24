// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace LrpServer.Net.LocalCpp
{
	internal class LocalServerRaw
	{
		private readonly IClient m_client;
		public LocalServerRaw(IClient client)
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
		public SoftFX.Lrp.LPtr Constructor(SoftFX.Lrp.LPtr channels, int port, string sertificateFilename, string sertificatePassword, SoftFX.Lrp.LPtr handler)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(channels);
				buffer.WriteInt32(port);
				buffer.WriteAString(sertificateFilename);
				buffer.WriteAString(sertificatePassword);
				buffer.WriteLocalPointer(handler);

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
		public bool Is_Start_Supported
		{
			get
			{
				return m_client.IsSupported(1, 2);
			}
		}
		public void Start(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(1, 2, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_Stop_Supported
		{
			get
			{
				return m_client.IsSupported(1, 3);
			}
		}
		public void Stop(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(1, 3, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_EndConnection_Supported
		{
			get
			{
				return m_client.IsSupported(1, 4);
			}
		}
		public void EndConnection(SoftFX.Lrp.LPtr handle, long id, int status)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteInt64(id);
				buffer.WriteInt32(status);

				int _status = m_client.Invoke(1, 4, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_EndLogon_Supported
		{
			get
			{
				return m_client.IsSupported(1, 5);
			}
		}
		public void EndLogon(SoftFX.Lrp.LPtr handle, long id, int status, string message)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteInt64(id);
				buffer.WriteInt32(status);
				buffer.WriteAString(message);

				int _status = m_client.Invoke(1, 5, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_SendSessionInfo_Supported
		{
			get
			{
				return m_client.IsSupported(1, 6);
			}
		}
		public void SendSessionInfo(SoftFX.Lrp.LPtr handle, long id, string requestId, LrpServer.Net.LrpSessionInfo sessionInfo)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteInt64(id);
				buffer.WriteAString(requestId);
				buffer.WriteLrpSessionInfo(sessionInfo);

				int _status = m_client.Invoke(1, 6, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_SendCurrenciesInfo_Supported
		{
			get
			{
				return m_client.IsSupported(1, 7);
			}
		}
		public void SendCurrenciesInfo(SoftFX.Lrp.LPtr handle, long id, string requestId, LrpServer.Net.LrpCurrencyInfo[] currencies)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteInt64(id);
				buffer.WriteAString(requestId);
				buffer.WriteCurrencyInfoArray(currencies);

				int _status = m_client.Invoke(1, 7, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_SendSymbolsInfo_Supported
		{
			get
			{
				return m_client.IsSupported(1, 8);
			}
		}
		public void SendSymbolsInfo(SoftFX.Lrp.LPtr handle, long id, string requestId, LrpServer.Net.LrpSymbolInfo[] symbols)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteInt64(id);
				buffer.WriteAString(requestId);
				buffer.WriteSymbolInfoArray(symbols);

				int _status = m_client.Invoke(1, 8, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_SendQuotesSubscriptionConfirm_Supported
		{
			get
			{
				return m_client.IsSupported(1, 9);
			}
		}
		public void SendQuotesSubscriptionConfirm(SoftFX.Lrp.LPtr handle, long id, string requestId)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteInt64(id);
				buffer.WriteAString(requestId);

				int _status = m_client.Invoke(1, 9, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_SendQuotesSubscriptionReject_Supported
		{
			get
			{
				return m_client.IsSupported(1, 10);
			}
		}
		public void SendQuotesSubscriptionReject(SoftFX.Lrp.LPtr handle, long id, string requestId, string message)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteInt64(id);
				buffer.WriteAString(requestId);
				buffer.WriteAString(message);

				int _status = m_client.Invoke(1, 10, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_SendQuotesHistoryVersion_Supported
		{
			get
			{
				return m_client.IsSupported(1, 11);
			}
		}
		public void SendQuotesHistoryVersion(SoftFX.Lrp.LPtr handle, long id, string requestId, int version)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteInt64(id);
				buffer.WriteAString(requestId);
				buffer.WriteInt32(version);

				int _status = m_client.Invoke(1, 11, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_SendQuote_Supported
		{
			get
			{
				return m_client.IsSupported(1, 12);
			}
		}
		public void SendQuote(SoftFX.Lrp.LPtr handle, long id, LrpServer.Net.LrpQuote quote)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteInt64(id);
				buffer.WriteQuote(quote);

				int _status = m_client.Invoke(1, 12, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_SendMarketHistoryMetadataReport_Supported
		{
			get
			{
				return m_client.IsSupported(1, 13);
			}
		}
		public void SendMarketHistoryMetadataReport(SoftFX.Lrp.LPtr handle, long id, string requestId, int status, string field)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteInt64(id);
				buffer.WriteAString(requestId);
				buffer.WriteInt32(status);
				buffer.WriteAString(field);

				int _status = m_client.Invoke(1, 13, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_SendDataHistoryResponse_Supported
		{
			get
			{
				return m_client.IsSupported(1, 14);
			}
		}
		public void SendDataHistoryResponse(SoftFX.Lrp.LPtr handle, long id, string requestId, LrpServer.Net.LrpDataHistoryResponse response)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteInt64(id);
				buffer.WriteAString(requestId);
				buffer.WriteDataHistoryResponse(response);

				int _status = m_client.Invoke(1, 14, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_SendFileChunk_Supported
		{
			get
			{
				return m_client.IsSupported(1, 15);
			}
		}
		public void SendFileChunk(SoftFX.Lrp.LPtr handle, long id, string requestId, LrpServer.Net.LrpFileChunk chunk)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteInt64(id);
				buffer.WriteAString(requestId);
				buffer.WriteFileChunk(chunk);

				int _status = m_client.Invoke(1, 15, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_SendNotification_Supported
		{
			get
			{
				return m_client.IsSupported(1, 16);
			}
		}
		public void SendNotification(SoftFX.Lrp.LPtr handle, long id, LrpServer.Net.LrpNotification notification)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteInt64(id);
				buffer.WriteNotification(notification);

				int _status = m_client.Invoke(1, 16, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
	}
}

namespace LrpServer.Net.LocalCpp
{
	internal class LocalServerProxy : System.IDisposable
	{
		public LocalServerRaw Instance { get; private set; }
		public LPtr Handle { get; private set; }
		public bool IsSupported
		{
			get
			{
				return this.Instance.IsSupported;
			}
		}
		internal LocalServerProxy(LocalClient client, LPtr handle)
		{
			this.Instance = new LocalServerRaw(client);
			this.Handle = handle;
		}
		public bool Is_Constructor_Supported
		{
			get
			{
				return this.Instance.Is_Constructor_Supported;
			}
		}
		public LocalServerProxy(LocalClient client, SoftFX.Lrp.LPtr channels, int port, string sertificateFilename, string sertificatePassword, SoftFX.Lrp.LPtr handler)
		{
			this.Instance = new LocalServerRaw(client);
			this.Handle = this.Instance.Constructor(channels, port, sertificateFilename, sertificatePassword, handler);
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
		public bool Is_Start_Supported
		{
			get
			{
				return this.Instance.Is_Start_Supported;
			}
		}
		public void Start()
		{
			this.Instance.Start(this.Handle);
		}
		public bool Is_Stop_Supported
		{
			get
			{
				return this.Instance.Is_Stop_Supported;
			}
		}
		public void Stop()
		{
			this.Instance.Stop(this.Handle);
		}
		public bool Is_EndConnection_Supported
		{
			get
			{
				return this.Instance.Is_EndConnection_Supported;
			}
		}
		public void EndConnection(long id, int status)
		{
			this.Instance.EndConnection(this.Handle, id, status);
		}
		public bool Is_EndLogon_Supported
		{
			get
			{
				return this.Instance.Is_EndLogon_Supported;
			}
		}
		public void EndLogon(long id, int status, string message)
		{
			this.Instance.EndLogon(this.Handle, id, status, message);
		}
		public bool Is_SendSessionInfo_Supported
		{
			get
			{
				return this.Instance.Is_SendSessionInfo_Supported;
			}
		}
		public void SendSessionInfo(long id, string requestId, LrpServer.Net.LrpSessionInfo sessionInfo)
		{
			this.Instance.SendSessionInfo(this.Handle, id, requestId, sessionInfo);
		}
		public bool Is_SendCurrenciesInfo_Supported
		{
			get
			{
				return this.Instance.Is_SendCurrenciesInfo_Supported;
			}
		}
		public void SendCurrenciesInfo(long id, string requestId, LrpServer.Net.LrpCurrencyInfo[] currencies)
		{
			this.Instance.SendCurrenciesInfo(this.Handle, id, requestId, currencies);
		}
		public bool Is_SendSymbolsInfo_Supported
		{
			get
			{
				return this.Instance.Is_SendSymbolsInfo_Supported;
			}
		}
		public void SendSymbolsInfo(long id, string requestId, LrpServer.Net.LrpSymbolInfo[] symbols)
		{
			this.Instance.SendSymbolsInfo(this.Handle, id, requestId, symbols);
		}
		public bool Is_SendQuotesSubscriptionConfirm_Supported
		{
			get
			{
				return this.Instance.Is_SendQuotesSubscriptionConfirm_Supported;
			}
		}
		public void SendQuotesSubscriptionConfirm(long id, string requestId)
		{
			this.Instance.SendQuotesSubscriptionConfirm(this.Handle, id, requestId);
		}
		public bool Is_SendQuotesSubscriptionReject_Supported
		{
			get
			{
				return this.Instance.Is_SendQuotesSubscriptionReject_Supported;
			}
		}
		public void SendQuotesSubscriptionReject(long id, string requestId, string message)
		{
			this.Instance.SendQuotesSubscriptionReject(this.Handle, id, requestId, message);
		}
		public bool Is_SendQuotesHistoryVersion_Supported
		{
			get
			{
				return this.Instance.Is_SendQuotesHistoryVersion_Supported;
			}
		}
		public void SendQuotesHistoryVersion(long id, string requestId, int version)
		{
			this.Instance.SendQuotesHistoryVersion(this.Handle, id, requestId, version);
		}
		public bool Is_SendQuote_Supported
		{
			get
			{
				return this.Instance.Is_SendQuote_Supported;
			}
		}
		public void SendQuote(long id, LrpServer.Net.LrpQuote quote)
		{
			this.Instance.SendQuote(this.Handle, id, quote);
		}
		public bool Is_SendMarketHistoryMetadataReport_Supported
		{
			get
			{
				return this.Instance.Is_SendMarketHistoryMetadataReport_Supported;
			}
		}
		public void SendMarketHistoryMetadataReport(long id, string requestId, int status, string field)
		{
			this.Instance.SendMarketHistoryMetadataReport(this.Handle, id, requestId, status, field);
		}
		public bool Is_SendDataHistoryResponse_Supported
		{
			get
			{
				return this.Instance.Is_SendDataHistoryResponse_Supported;
			}
		}
		public void SendDataHistoryResponse(long id, string requestId, LrpServer.Net.LrpDataHistoryResponse response)
		{
			this.Instance.SendDataHistoryResponse(this.Handle, id, requestId, response);
		}
		public bool Is_SendFileChunk_Supported
		{
			get
			{
				return this.Instance.Is_SendFileChunk_Supported;
			}
		}
		public void SendFileChunk(long id, string requestId, LrpServer.Net.LrpFileChunk chunk)
		{
			this.Instance.SendFileChunk(this.Handle, id, requestId, chunk);
		}
		public bool Is_SendNotification_Supported
		{
			get
			{
				return this.Instance.Is_SendNotification_Supported;
			}
		}
		public void SendNotification(long id, LrpServer.Net.LrpNotification notification)
		{
			this.Instance.SendNotification(this.Handle, id, notification);
		}
	}
}
