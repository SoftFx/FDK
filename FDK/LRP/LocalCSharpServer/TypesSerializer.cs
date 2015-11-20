// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace LocalCSharp
{
	internal static class TypesSerializer
	{
		public static LocalCSharp.UsedType ReadUsedType(this MemoryBuffer buffer)
		{
			var result = new LocalCSharp.UsedType();
			result.Code = buffer.ReadInt32();
			result.Description = buffer.ReadAString();
			return result;
		}
		public static void WriteUsedType(this MemoryBuffer buffer, LocalCSharp.UsedType arg)
		{
			buffer.WriteInt32(arg.Code);
			buffer.WriteAString(arg.Description);
		}
		public static LocalCSharp.InType ReadInType(this MemoryBuffer buffer)
		{
			var result = new LocalCSharp.InType();
			result.Used = buffer.ReadUsedType();
			result.Value = buffer.ReadDouble();
			return result;
		}
		public static void WriteInType(this MemoryBuffer buffer, LocalCSharp.InType arg)
		{
			buffer.WriteUsedType(arg.Used);
			buffer.WriteDouble(arg.Value);
		}
		public static LocalCSharp.InOutType ReadInOutType(this MemoryBuffer buffer)
		{
			var result = new LocalCSharp.InOutType();
			result.Used = buffer.ReadUsedType();
			result.Value2 = buffer.ReadDouble();
			return result;
		}
		public static void WriteInOutType(this MemoryBuffer buffer, LocalCSharp.InOutType arg)
		{
			buffer.WriteUsedType(arg.Used);
			buffer.WriteDouble(arg.Value2);
		}
		public static LocalCSharp.OutType ReadOutType(this MemoryBuffer buffer)
		{
			var result = new LocalCSharp.OutType();
			result.Used = buffer.ReadUsedType();
			result.Value3 = buffer.ReadDouble();
			return result;
		}
		public static void WriteOutType(this MemoryBuffer buffer, LocalCSharp.OutType arg)
		{
			buffer.WriteUsedType(arg.Used);
			buffer.WriteDouble(arg.Value3);
		}
		public static LocalCSharp.ReturnType ReadReturnType(this MemoryBuffer buffer)
		{
			var result = new LocalCSharp.ReturnType();
			result.Used = buffer.ReadUsedType();
			result.Value4 = buffer.ReadDouble();
			return result;
		}
		public static void WriteReturnType(this MemoryBuffer buffer, LocalCSharp.ReturnType arg)
		{
			buffer.WriteUsedType(arg.Used);
			buffer.WriteDouble(arg.Value4);
		}
		public static LocalCSharp.QuoteEntry ReadQuoteEntry(this MemoryBuffer buffer)
		{
			var result = new LocalCSharp.QuoteEntry();
			result.Price = buffer.ReadDouble();
			result.Volume = buffer.ReadDouble();
			return result;
		}
		public static void WriteQuoteEntry(this MemoryBuffer buffer, LocalCSharp.QuoteEntry arg)
		{
			buffer.WriteDouble(arg.Price);
			buffer.WriteDouble(arg.Volume);
		}
		public static LocalCSharp.QuoteEntry[] ReadQuoteEntryArray(this MemoryBuffer buffer)
		{
			int length = buffer.ReadCount();
			var result = new LocalCSharp.QuoteEntry[length];
			for(int index = 0; index < length; ++index)
			{
				result[index] = buffer.ReadQuoteEntry();
			}
			return result;
		}
		public static void WriteQuoteEntryArray(this MemoryBuffer buffer, LocalCSharp.QuoteEntry[] arg)
		{
			buffer.WriteInt32(arg.Length);
			foreach(var element in arg)
			{
				buffer.WriteQuoteEntry(element);
			}
		}
		public static LocalCSharp.Level2 ReadLevel2(this MemoryBuffer buffer)
		{
			var result = new LocalCSharp.Level2();
			result.CreatingTime = buffer.ReadTime();
			result.Bids = buffer.ReadQuoteEntryArray();
			result.Asks = buffer.ReadQuoteEntryArray();
			result.Symbol = buffer.ReadAString();
			return result;
		}
		public static void WriteLevel2(this MemoryBuffer buffer, LocalCSharp.Level2 arg)
		{
			buffer.WriteTime(arg.CreatingTime);
			buffer.WriteQuoteEntryArray(arg.Bids);
			buffer.WriteQuoteEntryArray(arg.Asks);
			buffer.WriteAString(arg.Symbol);
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
