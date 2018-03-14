// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace SoftFX.Extended.Generated
{
	internal class DailySnapshotsIterator
	{
		private readonly IClient m_client;
		public DailySnapshotsIterator(IClient client)
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
				return m_client.IsSupported(10);
			}
		}
		public bool Is_TotalItems_Supported
		{
			get
			{
				return m_client.IsSupported(10, 0);
			}
		}
		public int TotalItems(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(10, 0, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadInt32();
				return _result;
			}
		}
		public bool Is_EndOfStream_Supported
		{
			get
			{
				return m_client.IsSupported(10, 1);
			}
		}
		public bool EndOfStream(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(10, 1, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadBoolean();
				return _result;
			}
		}
		public bool Is_Next_Supported
		{
			get
			{
				return m_client.IsSupported(10, 2);
			}
		}
		public void Next(SoftFX.Lrp.LPtr handle, uint timeoutInMilliseconds)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);
				buffer.WriteUInt32(timeoutInMilliseconds);

				int _status = m_client.Invoke(10, 2, buffer);
				TypesSerializer.Throw(_status, buffer);

			}
		}
		public bool Is_GetDailyAccountSnapshotReport_Supported
		{
			get
			{
				return m_client.IsSupported(10, 3);
			}
		}
		public SoftFX.Extended.Reports.DailyAccountSnapshotReport GetDailyAccountSnapshotReport(SoftFX.Lrp.LPtr handle)
		{
			using(MemoryBuffer buffer = m_client.Create())
			{
				buffer.WriteLocalPointer(handle);

				int _status = m_client.Invoke(10, 3, buffer);
				TypesSerializer.Throw(_status, buffer);

				var _result = buffer.ReadDailyAccountSnapshotReport();
				return _result;
			}
		}
	}
}
