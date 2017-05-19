// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace SoftFX.Extended.Generated
{
	internal class Converter
	{
		private readonly IClient m_client;
		public Converter(IClient client)
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
		public bool Is_CurrenciesFromHandle_Supported
		{
			get
			{
				return m_client.IsSupported(1, 0);
			}
		}
		public SoftFX.Extended.CurrencyInfo[] CurrenciesFromHandle(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(1, 0, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadCurrencyInfoArray();
				return _result;
			}
		}
		public bool Is_SymbolFromHandle_Supported
		{
			get
			{
				return m_client.IsSupported(1, 1);
			}
		}
		public string SymbolFromHandle(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(1, 1, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadAString();
				return _result;
			}
		}
		public bool Is_SymbolsFromHandle_Supported
		{
			get
			{
				return m_client.IsSupported(1, 2);
			}
		}
		public SoftFX.Extended.SymbolInfo[] SymbolsFromHandle(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(1, 2, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadSymbolInfoArray();
				return _result;
			}
		}
		public bool Is_TwoFactorAuthFromHandle_Supported
		{
			get
			{
				return m_client.IsSupported(1, 3);
			}
		}
		public SoftFX.Extended.TwoFactorAuth TwoFactorAuthFromHandle(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(1, 3, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadTwoFactorAuth();
				return _result;
			}
		}
		public bool Is_SessionInfoFromHandle_Supported
		{
			get
			{
				return m_client.IsSupported(1, 4);
			}
		}
		public SoftFX.Extended.SessionInfo SessionInfoFromHandle(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(1, 4, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadSessionInfo();
				return _result;
			}
		}
		public bool Is_NotificationFromHandle_Supported
		{
			get
			{
				return m_client.IsSupported(1, 5);
			}
		}
		public SoftFX.Extended.Data.Notification NotificationFromHandle(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(1, 5, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadNotification();
				return _result;
			}
		}
		public bool Is_QuoteFromHandle_Supported
		{
			get
			{
				return m_client.IsSupported(1, 6);
			}
		}
		public SoftFX.Extended.Quote QuoteFromHandle(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(1, 6, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadQuote();
				return _result;
			}
		}
		public bool Is_ProtocolVersionFromHandle_Supported
		{
			get
			{
				return m_client.IsSupported(1, 7);
			}
		}
		public string ProtocolVersionFromHandle(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(1, 7, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadAString();
				return _result;
			}
		}
		public bool Is_AccountInfoFromHandle_Supported
		{
			get
			{
				return m_client.IsSupported(1, 8);
			}
		}
		public SoftFX.Extended.AccountInfo AccountInfoFromHandle(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(1, 8, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadAccountInfo();
				return _result;
			}
		}
		public bool Is_PositionFromHandle_Supported
		{
			get
			{
				return m_client.IsSupported(1, 9);
			}
		}
		public SoftFX.Extended.Position PositionFromHandle(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(1, 9, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadPosition();
				return _result;
			}
		}
		public bool Is_TradeTransactionReportFromHandle_Supported
		{
			get
			{
				return m_client.IsSupported(1, 10);
			}
		}
		public SoftFX.Extended.Reports.TradeTransactionReport TradeTransactionReportFromHandle(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(1, 10, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadTradeTransactionReport();
				return _result;
			}
		}
		public bool Is_ExecutionReportFromHandle_Supported
		{
			get
			{
				return m_client.IsSupported(1, 11);
			}
		}
		public SoftFX.Extended.ExecutionReport ExecutionReportFromHandle(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(1, 11, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadExecutionReport();
				return _result;
			}
		}
		public bool Is_GetLogoutInfoFromHandle_Supported
		{
			get
			{
				return m_client.IsSupported(1, 12);
			}
		}
		public void GetLogoutInfoFromHandle(SoftFX.Lrp.LPtr handle, out string text, out SoftFX.Extended.LogoutReason reason, out int code)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(1, 12, buffer);
				TypesSerializer.Throw(_status, buffer);

				text = buffer.ReadAString();
				reason = buffer.ReadLogoutReason();
				code = buffer.ReadInt32();
			}
		}
	}
}
