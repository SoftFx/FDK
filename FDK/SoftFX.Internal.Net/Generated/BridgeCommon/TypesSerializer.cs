// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace SoftFX.Internal.Generated.BridgeCommon
{
	internal static class TypesSerializer
	{
		public static SoftFX.Internal.FixSessionId ReadFFixSessionId(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Internal.FixSessionId();
			result.BeginString = buffer.ReadAString();
			result.SenderCompId = buffer.ReadAString();
			result.TargetCompId = buffer.ReadAString();
			return result;
		}
		public static void WriteFFixSessionId(this MemoryBuffer buffer, SoftFX.Internal.FixSessionId arg)
		{
			buffer.WriteAString(arg.BeginString);
			buffer.WriteAString(arg.SenderCompId);
			buffer.WriteAString(arg.TargetCompId);
		}
		public static SoftFX.Internal.FixSessionId[] ReadFFixSessionIdArray(this MemoryBuffer buffer)
		{
			int length = buffer.ReadCount();
			var result = new SoftFX.Internal.FixSessionId[length];
			for(int index = 0; index < length; ++index)
			{
				result[index] = buffer.ReadFFixSessionId();
			}
			return result;
		}
		public static void WriteFFixSessionIdArray(this MemoryBuffer buffer, SoftFX.Internal.FixSessionId[] arg)
		{
			buffer.WriteInt32(arg.Length);
			foreach(var element in arg)
			{
				buffer.WriteFFixSessionId(element);
			}
		}
		public static SoftFX.Extended.QuoteEntry ReadFQuoteEntry(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Extended.QuoteEntry();
			result.Price = buffer.ReadDouble();
			result.Volume = buffer.ReadDouble();
			return result;
		}
		public static void WriteFQuoteEntry(this MemoryBuffer buffer, SoftFX.Extended.QuoteEntry arg)
		{
			buffer.WriteDouble(arg.Price);
			buffer.WriteDouble(arg.Volume);
		}
		public static SoftFX.Extended.QuoteEntry[] ReadFQuoteEntryArray(this MemoryBuffer buffer)
		{
			int length = buffer.ReadCount();
			var result = new SoftFX.Extended.QuoteEntry[length];
			for(int index = 0; index < length; ++index)
			{
				result[index] = buffer.ReadFQuoteEntry();
			}
			return result;
		}
		public static void WriteFQuoteEntryArray(this MemoryBuffer buffer, SoftFX.Extended.QuoteEntry[] arg)
		{
			buffer.WriteInt32(arg.Length);
			foreach(var element in arg)
			{
				buffer.WriteFQuoteEntry(element);
			}
		}
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
		public static SoftFX.Extended.Quote ReadFQuote(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Extended.Quote();
			result.Symbol = buffer.ReadAString();
			result.CreatingTime = buffer.ReadTime();
			result.Bids = buffer.ReadFQuoteEntryArray();
			result.Asks = buffer.ReadFQuoteEntryArray();
			result.Id = buffer.ReadAString();
			return result;
		}
		public static void WriteFQuote(this MemoryBuffer buffer, SoftFX.Extended.Quote arg)
		{
			buffer.WriteAString(arg.Symbol);
			buffer.WriteTime(arg.CreatingTime);
			buffer.WriteFQuoteEntryArray(arg.Bids);
			buffer.WriteFQuoteEntryArray(arg.Asks);
			buffer.WriteAString(arg.Id);
		}
		public static SoftFX.Internal.FixParsingResult ReadFFixParsingResult(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Internal.FixParsingResult();
			result.Status = buffer.ReadBoolean();
			result.IsTimeDeviationCalculated = buffer.ReadBoolean();
			result.TimeDeviation = buffer.ReadInt64();
			result.SessionId = buffer.ReadFFixSessionId();
			result.Symbols = buffer.ReadAStringArray();
			return result;
		}
		public static void WriteFFixParsingResult(this MemoryBuffer buffer, SoftFX.Internal.FixParsingResult arg)
		{
			buffer.WriteBoolean(arg.Status);
			buffer.WriteBoolean(arg.IsTimeDeviationCalculated);
			buffer.WriteInt64(arg.TimeDeviation);
			buffer.WriteFFixSessionId(arg.SessionId);
			buffer.WriteAStringArray(arg.Symbols);
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
