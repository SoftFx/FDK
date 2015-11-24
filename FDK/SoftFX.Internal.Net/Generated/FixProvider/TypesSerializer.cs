// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace SoftFX.Internal.Generated.FixProvider
{
	internal static class TypesSerializer
	{
		public static SoftFX.Extended.QuoteEntry ReadQuoteEntry(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Extended.QuoteEntry();
			result.Price = buffer.ReadDouble();
			result.Volume = buffer.ReadDouble();
			return result;
		}
		public static void WriteQuoteEntry(this MemoryBuffer buffer, SoftFX.Extended.QuoteEntry arg)
		{
			buffer.WriteDouble(arg.Price);
			buffer.WriteDouble(arg.Volume);
		}
		public static SoftFX.Extended.QuoteEntry[] ReadQuoteEntryArray(this MemoryBuffer buffer)
		{
			int length = buffer.ReadCount();
			var result = new SoftFX.Extended.QuoteEntry[length];
			for(int index = 0; index < length; ++index)
			{
				result[index] = buffer.ReadQuoteEntry();
			}
			return result;
		}
		public static void WriteQuoteEntryArray(this MemoryBuffer buffer, SoftFX.Extended.QuoteEntry[] arg)
		{
			buffer.WriteInt32(arg.Length);
			foreach(var element in arg)
			{
				buffer.WriteQuoteEntry(element);
			}
		}
		public static SoftFX.Extended.Quote ReadQuote(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Extended.Quote();
			result.Symbol = buffer.ReadAString();
			result.CreatingTime = buffer.ReadTime();
			result.Bids = buffer.ReadQuoteEntryArray();
			result.Asks = buffer.ReadQuoteEntryArray();
			return result;
		}
		public static void WriteQuote(this MemoryBuffer buffer, SoftFX.Extended.Quote arg)
		{
			buffer.WriteAString(arg.Symbol);
			buffer.WriteTime(arg.CreatingTime);
			buffer.WriteQuoteEntryArray(arg.Bids);
			buffer.WriteQuoteEntryArray(arg.Asks);
		}
		public static SoftFX.Extended.Quote[] ReadQuoteArray(this MemoryBuffer buffer)
		{
			int length = buffer.ReadCount();
			var result = new SoftFX.Extended.Quote[length];
			for(int index = 0; index < length; ++index)
			{
				result[index] = buffer.ReadQuote();
			}
			return result;
		}
		public static void WriteQuoteArray(this MemoryBuffer buffer, SoftFX.Extended.Quote[] arg)
		{
			buffer.WriteInt32(arg.Length);
			foreach(var element in arg)
			{
				buffer.WriteQuote(element);
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
