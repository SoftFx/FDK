// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace RemoteCSharpClient
{
	internal static class TypesSerializer
	{
		public static RemoteCSharpClient.UsedType ReadUsedType(this MemoryBuffer buffer)
		{
			var result = new RemoteCSharpClient.UsedType();
			result.Code = buffer.ReadInt32();
			result.Description = buffer.ReadAString();
			return result;
		}
		public static void WriteUsedType(this MemoryBuffer buffer, RemoteCSharpClient.UsedType arg)
		{
			buffer.WriteInt32(arg.Code);
			buffer.WriteAString(arg.Description);
		}
		public static RemoteCSharpClient.InType ReadInType(this MemoryBuffer buffer)
		{
			var result = new RemoteCSharpClient.InType();
			result.Used = buffer.ReadUsedType();
			result.Value = buffer.ReadDouble();
			return result;
		}
		public static void WriteInType(this MemoryBuffer buffer, RemoteCSharpClient.InType arg)
		{
			buffer.WriteUsedType(arg.Used);
			buffer.WriteDouble(arg.Value);
		}
		public static RemoteCSharpClient.InOutType ReadInOutType(this MemoryBuffer buffer)
		{
			var result = new RemoteCSharpClient.InOutType();
			result.Used = buffer.ReadUsedType();
			result.Value2 = buffer.ReadDouble();
			return result;
		}
		public static void WriteInOutType(this MemoryBuffer buffer, RemoteCSharpClient.InOutType arg)
		{
			buffer.WriteUsedType(arg.Used);
			buffer.WriteDouble(arg.Value2);
		}
		public static RemoteCSharpClient.OutType ReadOutType(this MemoryBuffer buffer)
		{
			var result = new RemoteCSharpClient.OutType();
			result.Used = buffer.ReadUsedType();
			result.Value3 = buffer.ReadDouble();
			return result;
		}
		public static void WriteOutType(this MemoryBuffer buffer, RemoteCSharpClient.OutType arg)
		{
			buffer.WriteUsedType(arg.Used);
			buffer.WriteDouble(arg.Value3);
		}
		public static RemoteCSharpClient.ReturnType ReadReturnType(this MemoryBuffer buffer)
		{
			var result = new RemoteCSharpClient.ReturnType();
			result.Used = buffer.ReadUsedType();
			result.Value4 = buffer.ReadDouble();
			return result;
		}
		public static void WriteReturnType(this MemoryBuffer buffer, RemoteCSharpClient.ReturnType arg)
		{
			buffer.WriteUsedType(arg.Used);
			buffer.WriteDouble(arg.Value4);
		}
		public static System.Collections.Generic.SortedDictionary<string, double> ReadPositionReports(this MemoryBuffer buffer)
		{
			int length = buffer.ReadCount();
			var result = new System.Collections.Generic.SortedDictionary<string, double>();
			for(int index = 0; index < length; ++index)
			{
				result.Add(buffer.ReadAString(), buffer.ReadDouble());
			}
			return result;
		}
		public static void WritePositionReports(this MemoryBuffer buffer, System.Collections.Generic.SortedDictionary<string, double> arg)
		{
			buffer.WriteInt32(arg.Count);
			foreach(var element in arg)
			{
				buffer.WriteAString(element.Key);
				buffer.WriteDouble(element.Value);
			}
		}
		public static System.Collections.Generic.Dictionary<string, double> ReadPositionReports2(this MemoryBuffer buffer)
		{
			int length = buffer.ReadCount();
			var result = new System.Collections.Generic.Dictionary<string, double>();
			for(int index = 0; index < length; ++index)
			{
				result.Add(buffer.ReadAString(), buffer.ReadDouble());
			}
			return result;
		}
		public static void WritePositionReports2(this MemoryBuffer buffer, System.Collections.Generic.Dictionary<string, double> arg)
		{
			buffer.WriteInt32(arg.Count);
			foreach(var element in arg)
			{
				buffer.WriteAString(element.Key);
				buffer.WriteDouble(element.Value);
			}
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
