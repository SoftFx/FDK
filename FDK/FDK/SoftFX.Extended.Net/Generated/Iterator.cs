// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace SoftFX.Extended.Generated
{
	internal class Iterator
	{
		private readonly IClient m_client;
		public Iterator(IClient client)
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
				return m_client.IsSupported(9);
			}
		}
		public bool Is_EndOfStream_Supported
		{
			get
			{
				return m_client.IsSupported(9, 0);
			}
		}
		public bool EndOfStream(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(9, 0, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadBoolean();
				return _result;
			}
		}
		public bool Is_Next_Supported
		{
			get
			{
				return m_client.IsSupported(9, 1);
			}
		}
		public void Next(SoftFX.Lrp.LPtr handle, uint timeoutInMilliseconds)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteUInt32(timeoutInMilliseconds);

				int _status = m_client.Invoke(9, 1, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_GetTradeTransactionReport_Supported
		{
			get
			{
				return m_client.IsSupported(9, 2);
			}
		}
		public SoftFX.Extended.Reports.TradeTransactionReport GetTradeTransactionReport(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(9, 2, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadTradeTransactionReport();
				return _result;
			}
		}
	}
}
