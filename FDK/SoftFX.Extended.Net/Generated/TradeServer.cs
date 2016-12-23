// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace SoftFX.Extended.Generated
{
	internal class TradeServer
	{
		private readonly IClient m_client;
		public TradeServer(IClient client)
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
				return m_client.IsSupported(7);
			}
		}
		public bool Is_Create_Supported
		{
			get
			{
				return m_client.IsSupported(7, 0);
			}
		}
		public SoftFX.Lrp.LPtr Create(string name, string connectionString)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteAString(name);
				buffer.WriteAString(connectionString);

				int _status = m_client.Invoke(7, 0, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadLocalPointer();
				return _result;
			}
		}
		public bool Is_GetTradeTransactionReportsAndSubscribe_Supported
		{
			get
			{
				return m_client.IsSupported(7, 1);
			}
		}
		public SoftFX.Lrp.LPtr GetTradeTransactionReportsAndSubscribe(SoftFX.Lrp.LPtr handle, int direction, bool subscribe, System.DateTime? from, System.DateTime? to, uint preferedBufferSize, uint timeoutInMilliseconds)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteInt32(direction);
				buffer.WriteBoolean(subscribe);
				buffer.WriteNullTime(from);
				buffer.WriteNullTime(to);
				buffer.WriteUInt32(preferedBufferSize);
				buffer.WriteUInt32(timeoutInMilliseconds);

				int _status = m_client.Invoke(7, 1, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadLocalPointer();
				return _result;
			}
		}
		public bool Is_GetTradeCaptureReports_Supported
		{
			get
			{
				return m_client.IsSupported(7, 2);
			}
		}
		public SoftFX.Lrp.LPtr GetTradeCaptureReports(SoftFX.Lrp.LPtr handle, System.DateTime? from, System.DateTime? to, uint timeoutInMilliseconds)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteNullTime(from);
				buffer.WriteNullTime(to);
				buffer.WriteUInt32(timeoutInMilliseconds);

				int _status = m_client.Invoke(7, 2, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadLocalPointer();
				return _result;
			}
		}
		public bool Is_UnsubscribeTradeTransactionReports_Supported
		{
			get
			{
				return m_client.IsSupported(7, 3);
			}
		}
		public void UnsubscribeTradeTransactionReports(SoftFX.Lrp.LPtr handle, uint timeoutInMilliseconds)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteUInt32(timeoutInMilliseconds);

				int _status = m_client.Invoke(7, 3, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_GetTradeServerInfo_Supported
		{
			get
			{
				return m_client.IsSupported(7, 4);
			}
		}
		public SoftFX.Extended.TradeServerInfo GetTradeServerInfo(SoftFX.Lrp.LPtr handle, uint timeoutInMilliseconds)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteUInt32(timeoutInMilliseconds);

				int _status = m_client.Invoke(7, 4, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadTradeServerInfo();
				return _result;
			}
		}
		public bool Is_GetAccountInfo_Supported
		{
			get
			{
				return m_client.IsSupported(7, 5);
			}
		}
		public SoftFX.Extended.AccountInfo GetAccountInfo(SoftFX.Lrp.LPtr handle, uint timeoutInMilliseconds)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteUInt32(timeoutInMilliseconds);

				int _status = m_client.Invoke(7, 5, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadAccountInfo();
				return _result;
			}
		}
		public bool Is_DeleteOrder_Supported
		{
			get
			{
				return m_client.IsSupported(7, 6);
			}
		}
		public void DeleteOrder(SoftFX.Lrp.LPtr handle, string operationId, string orderId, string clientOrderId, SoftFX.Extended.TradeRecordSide side, uint timeoutInMilliseconds)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteAString(operationId);
				buffer.WriteAString(orderId);
				buffer.WriteAString(clientOrderId);
				buffer.WriteSide(side);
				buffer.WriteUInt32(timeoutInMilliseconds);

				int _status = m_client.Invoke(7, 6, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_CloseAllPositions_Supported
		{
			get
			{
				return m_client.IsSupported(7, 7);
			}
		}
		public ulong CloseAllPositions(SoftFX.Lrp.LPtr handle, uint timeoutInMilliseconds)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteUInt32(timeoutInMilliseconds);

				int _status = m_client.Invoke(7, 7, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadUInt64();
				return _result;
			}
		}
		public bool Is_CloseByPositions_Supported
		{
			get
			{
				return m_client.IsSupported(7, 8);
			}
		}
		public bool CloseByPositions(SoftFX.Lrp.LPtr handle, string first, string second, uint timeoutInMilliseconds)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteAString(first);
				buffer.WriteAString(second);
				buffer.WriteUInt32(timeoutInMilliseconds);

				int _status = m_client.Invoke(7, 8, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadBoolean();
				return _result;
			}
		}
		public bool Is_GetRecords_Supported
		{
			get
			{
				return m_client.IsSupported(7, 9);
			}
		}
		public SoftFX.Extended.Data.FxOrder[] GetRecords(SoftFX.Lrp.LPtr handle, uint timeoutInMilliseconds)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteUInt32(timeoutInMilliseconds);

				int _status = m_client.Invoke(7, 9, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadFxOrderArray();
				return _result;
			}
		}
		public bool Is_OpenNewOrder_Supported
		{
			get
			{
				return m_client.IsSupported(7, 10);
			}
		}
		public SoftFX.Extended.Data.FxOrder OpenNewOrder(SoftFX.Lrp.LPtr handle, string operationId, SoftFX.Extended.Data.FxOrder order, uint timeoutInMilliseconds)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteAString(operationId);
				buffer.WriteFxOrder(order);
				buffer.WriteUInt32(timeoutInMilliseconds);

				int _status = m_client.Invoke(7, 10, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadFxOrder();
				return _result;
			}
		}
		public bool Is_ModifyOrder_Supported
		{
			get
			{
				return m_client.IsSupported(7, 11);
			}
		}
		public SoftFX.Extended.Data.FxOrder ModifyOrder(SoftFX.Lrp.LPtr handle, string operationId, SoftFX.Extended.Data.FxOrder order, uint timeoutInMilliseconds)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteAString(operationId);
				buffer.WriteFxOrder(order);
				buffer.WriteUInt32(timeoutInMilliseconds);

				int _status = m_client.Invoke(7, 11, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadFxOrder();
				return _result;
			}
		}
		public bool Is_CloseOrder_Supported
		{
			get
			{
				return m_client.IsSupported(7, 12);
			}
		}
		public SoftFX.Extended.ClosePositionResult CloseOrder(SoftFX.Lrp.LPtr handle, string operationId, string orderId, double? closingVolume, uint timeoutInMilliseconds)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteAString(operationId);
				buffer.WriteAString(orderId);
				buffer.WriteNullDouble(closingVolume);
				buffer.WriteUInt32(timeoutInMilliseconds);

				int _status = m_client.Invoke(7, 12, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadClosePositionResult();
				return _result;
			}
		}
	}
}
