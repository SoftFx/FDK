// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace LrpServer.Net.LocalCSharp
{
	internal static class TypesSerializer
	{
		public static string[] ReadAStringArray(this MemoryBuffer buffer)
		{
			int length = buffer.ReadCount();
			var result = new string[length];
			for(int index = 0; index < length; ++index)
			{
				result[index] = buffer.ReadAString();
			}
			return result;
		}
		public static void WriteAStringArray(this MemoryBuffer buffer, string[] arg)
		{
			buffer.WriteInt32(arg.Length);
			foreach(var element in arg)
			{
				buffer.WriteAString(element);
			}
		}
		public static LrpServer.Net.LrpTwoFactorReason ReadTwoFactorReason(this MemoryBuffer buffer)
		{
			var result = (LrpServer.Net.LrpTwoFactorReason)buffer.ReadInt32();
			return result;
		}
		public static void WriteTwoFactorReason(this MemoryBuffer buffer, LrpServer.Net.LrpTwoFactorReason arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static LrpServer.Net.LrpDataHistoryRequest ReadDataHistoryRequest(this MemoryBuffer buffer)
		{
			var result = new LrpServer.Net.LrpDataHistoryRequest();
			result.Symbol = buffer.ReadAString();
			result.Time = buffer.ReadTime();
			result.BarsNumber = buffer.ReadInt32();
			result.PriceType = buffer.ReadInt32();
			result.GraphPeriod = buffer.ReadAString();
			result.ReportType = buffer.ReadInt32();
			result.GraphType = buffer.ReadInt32();
			return result;
		}
		public static void WriteDataHistoryRequest(this MemoryBuffer buffer, LrpServer.Net.LrpDataHistoryRequest arg)
		{
			buffer.WriteAString(arg.Symbol);
			buffer.WriteTime(arg.Time);
			buffer.WriteInt32(arg.BarsNumber);
			buffer.WriteInt32(arg.PriceType);
			buffer.WriteAString(arg.GraphPeriod);
			buffer.WriteInt32(arg.ReportType);
			buffer.WriteInt32(arg.GraphType);
		}
		public static LrpServer.Net.LrpBar ReadBar(this MemoryBuffer buffer)
		{
			var result = new LrpServer.Net.LrpBar();
			result.Open = buffer.ReadDouble();
			result.Close = buffer.ReadDouble();
			result.High = buffer.ReadDouble();
			result.Low = buffer.ReadDouble();
			result.Volume = buffer.ReadDouble();
			result.From = buffer.ReadTime();
			return result;
		}
		public static void WriteBar(this MemoryBuffer buffer, LrpServer.Net.LrpBar arg)
		{
			buffer.WriteDouble(arg.Open);
			buffer.WriteDouble(arg.Close);
			buffer.WriteDouble(arg.High);
			buffer.WriteDouble(arg.Low);
			buffer.WriteDouble(arg.Volume);
			buffer.WriteTime(arg.From);
		}
		public static LrpServer.Net.LrpBar[] ReadBarArray(this MemoryBuffer buffer)
		{
			int length = buffer.ReadCount();
			var result = new LrpServer.Net.LrpBar[length];
			for(int index = 0; index < length; ++index)
			{
				result[index] = buffer.ReadBar();
			}
			return result;
		}
		public static void WriteBarArray(this MemoryBuffer buffer, LrpServer.Net.LrpBar[] arg)
		{
			buffer.WriteInt32(arg.Length);
			foreach(var element in arg)
			{
				buffer.WriteBar(element);
			}
		}
		public static LrpServer.Net.LrpDataHistoryResponse ReadDataHistoryResponse(this MemoryBuffer buffer)
		{
			var result = new LrpServer.Net.LrpDataHistoryResponse();
			result.FromAll = buffer.ReadTime();
			result.ToAll = buffer.ReadTime();
			result.From = buffer.ReadTime();
			result.To = buffer.ReadTime();
			result.LastTickId = buffer.ReadAString();
			result.Bars = buffer.ReadBarArray();
			result.Files = buffer.ReadAStringArray();
			return result;
		}
		public static void WriteDataHistoryResponse(this MemoryBuffer buffer, LrpServer.Net.LrpDataHistoryResponse arg)
		{
			buffer.WriteTime(arg.FromAll);
			buffer.WriteTime(arg.ToAll);
			buffer.WriteTime(arg.From);
			buffer.WriteTime(arg.To);
			buffer.WriteAString(arg.LastTickId);
			buffer.WriteBarArray(arg.Bars);
			buffer.WriteAStringArray(arg.Files);
		}
		public static void Throw(System.Int32 status, MemoryBuffer buffer)
		{
			if(status >= 0)
			{
				return;
			}
			if(MagicNumbers.LRP_EXCEPTION != status)
			{
				throw new System.Exception("Unexpected exception has been encountered");
			}
			System.Int32 _id = buffer.ReadInt32();
			System.String _message = buffer.ReadAString();
			throw new System.Exception(_message);
		}
	}
}
