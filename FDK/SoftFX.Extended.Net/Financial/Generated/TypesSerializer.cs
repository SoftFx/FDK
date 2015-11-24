// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace SoftFX.Extended.Financial.Generated
{
	internal static class TypesSerializer
	{
		public static SoftFX.Extended.Financial.MarginMode ReadMarginMode(this MemoryBuffer buffer)
		{
			var result = (SoftFX.Extended.Financial.MarginMode)buffer.ReadInt32();
			return result;
		}
		public static void WriteMarginMode(this MemoryBuffer buffer, SoftFX.Extended.Financial.MarginMode arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static SoftFX.Extended.TradeType ReadTradeType(this MemoryBuffer buffer)
		{
			var result = (SoftFX.Extended.TradeType)buffer.ReadInt32();
			return result;
		}
		public static void WriteTradeType(this MemoryBuffer buffer, SoftFX.Extended.TradeType arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static SoftFX.Extended.TradeRecordSide ReadTradeSide(this MemoryBuffer buffer)
		{
			var result = (SoftFX.Extended.TradeRecordSide)buffer.ReadInt32();
			return result;
		}
		public static void WriteTradeSide(this MemoryBuffer buffer, SoftFX.Extended.TradeRecordSide arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static SoftFX.Extended.Financial.TradeEntryStatus ReadTradeEntryStatus(this MemoryBuffer buffer)
		{
			var result = (SoftFX.Extended.Financial.TradeEntryStatus)buffer.ReadInt32();
			return result;
		}
		public static void WriteTradeEntryStatus(this MemoryBuffer buffer, SoftFX.Extended.Financial.TradeEntryStatus arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static SoftFX.Extended.Financial.AccountEntryStatus ReadAccountEntryStatus(this MemoryBuffer buffer)
		{
			var result = (SoftFX.Extended.Financial.AccountEntryStatus)buffer.ReadInt32();
			return result;
		}
		public static void WriteAccountEntryStatus(this MemoryBuffer buffer, SoftFX.Extended.Financial.AccountEntryStatus arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static SoftFX.Extended.AccountType ReadAccountType(this MemoryBuffer buffer)
		{
			var result = (SoftFX.Extended.AccountType)buffer.ReadInt32();
			return result;
		}
		public static void WriteAccountType(this MemoryBuffer buffer, SoftFX.Extended.AccountType arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static System.Collections.Generic.List<string> ReadAStringVector(this MemoryBuffer buffer)
		{
			int length = buffer.ReadCount();
			var result = new System.Collections.Generic.List<string>(length);
			for(int index = 0; index < length; ++index)
			{
				result.Add(buffer.ReadAString());
			}
			return result;
		}
		public static void WriteAStringVector(this MemoryBuffer buffer, System.Collections.Generic.List<string> arg)
		{
			buffer.WriteInt32(arg.Count);
			foreach(var element in arg)
			{
				buffer.WriteAString(element);
			}
		}
		public static SoftFX.Extended.Financial.Serialization.PriceData ReadPriceData(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Extended.Financial.Serialization.PriceData();
			result.Symbol = buffer.ReadAString();
			result.Bid = buffer.ReadDouble();
			result.Ask = buffer.ReadDouble();
			return result;
		}
		public static void WritePriceData(this MemoryBuffer buffer, SoftFX.Extended.Financial.Serialization.PriceData arg)
		{
			buffer.WriteAString(arg.Symbol);
			buffer.WriteDouble(arg.Bid);
			buffer.WriteDouble(arg.Ask);
		}
		public static System.Collections.Generic.List<SoftFX.Extended.Financial.Serialization.PriceData> ReadPriceDataVector(this MemoryBuffer buffer)
		{
			int length = buffer.ReadCount();
			var result = new System.Collections.Generic.List<SoftFX.Extended.Financial.Serialization.PriceData>(length);
			for(int index = 0; index < length; ++index)
			{
				result.Add(buffer.ReadPriceData());
			}
			return result;
		}
		public static void WritePriceDataVector(this MemoryBuffer buffer, System.Collections.Generic.List<SoftFX.Extended.Financial.Serialization.PriceData> arg)
		{
			buffer.WriteInt32(arg.Count);
			foreach(var element in arg)
			{
				buffer.WritePriceData(element);
			}
		}
		public static SoftFX.Extended.Financial.Serialization.SymbolData ReadSymbolData(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Extended.Financial.Serialization.SymbolData();
			result.Tag = buffer.ReadAString();
			result.Symbol = buffer.ReadAString();
			result.From = buffer.ReadAString();
			result.To = buffer.ReadAString();
			result.ContractSize = buffer.ReadDouble();
			result.Hedging = buffer.ReadDouble();
			result.MarginFactorOfPositions = buffer.ReadDouble();
			result.MarginFactorOfLimitOrders = buffer.ReadDouble();
			result.MarginFactorOfStopOrders = buffer.ReadDouble();
			return result;
		}
		public static void WriteSymbolData(this MemoryBuffer buffer, SoftFX.Extended.Financial.Serialization.SymbolData arg)
		{
			buffer.WriteAString(arg.Tag);
			buffer.WriteAString(arg.Symbol);
			buffer.WriteAString(arg.From);
			buffer.WriteAString(arg.To);
			buffer.WriteDouble(arg.ContractSize);
			buffer.WriteDouble(arg.Hedging);
			buffer.WriteDouble(arg.MarginFactorOfPositions);
			buffer.WriteDouble(arg.MarginFactorOfLimitOrders);
			buffer.WriteDouble(arg.MarginFactorOfStopOrders);
		}
		public static System.Collections.Generic.List<SoftFX.Extended.Financial.Serialization.SymbolData> ReadSymbolDataVector(this MemoryBuffer buffer)
		{
			int length = buffer.ReadCount();
			var result = new System.Collections.Generic.List<SoftFX.Extended.Financial.Serialization.SymbolData>(length);
			for(int index = 0; index < length; ++index)
			{
				result.Add(buffer.ReadSymbolData());
			}
			return result;
		}
		public static void WriteSymbolDataVector(this MemoryBuffer buffer, System.Collections.Generic.List<SoftFX.Extended.Financial.Serialization.SymbolData> arg)
		{
			buffer.WriteInt32(arg.Count);
			foreach(var element in arg)
			{
				buffer.WriteSymbolData(element);
			}
		}
		public static SoftFX.Extended.Financial.Serialization.TradeData ReadTradeData(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Extended.Financial.Serialization.TradeData();
			result.Tag = buffer.ReadAString();
			result.Type = buffer.ReadTradeType();
			result.Side = buffer.ReadTradeSide();
			result.Symbol = buffer.ReadAString();
			result.Price = buffer.ReadDouble();
			result.Volume = buffer.ReadDouble();
			result.Commission = buffer.ReadDouble();
			result.AgentCommission = buffer.ReadDouble();
			result.Swap = buffer.ReadDouble();
			result.Rate = buffer.ReadNullDouble();
			result.Profit = buffer.ReadNullDouble();
			result.ProfitStatus = buffer.ReadTradeEntryStatus();
			result.Margin = buffer.ReadNullDouble();
			result.MarginStatus = buffer.ReadTradeEntryStatus();
			return result;
		}
		public static void WriteTradeData(this MemoryBuffer buffer, SoftFX.Extended.Financial.Serialization.TradeData arg)
		{
			buffer.WriteAString(arg.Tag);
			buffer.WriteTradeType(arg.Type);
			buffer.WriteTradeSide(arg.Side);
			buffer.WriteAString(arg.Symbol);
			buffer.WriteDouble(arg.Price);
			buffer.WriteDouble(arg.Volume);
			buffer.WriteDouble(arg.Commission);
			buffer.WriteDouble(arg.AgentCommission);
			buffer.WriteDouble(arg.Swap);
			buffer.WriteNullDouble(arg.Rate);
			buffer.WriteNullDouble(arg.Profit);
			buffer.WriteTradeEntryStatus(arg.ProfitStatus);
			buffer.WriteNullDouble(arg.Margin);
			buffer.WriteTradeEntryStatus(arg.MarginStatus);
		}
		public static System.Collections.Generic.List<SoftFX.Extended.Financial.Serialization.TradeData> ReadTradeDataVector(this MemoryBuffer buffer)
		{
			int length = buffer.ReadCount();
			var result = new System.Collections.Generic.List<SoftFX.Extended.Financial.Serialization.TradeData>(length);
			for(int index = 0; index < length; ++index)
			{
				result.Add(buffer.ReadTradeData());
			}
			return result;
		}
		public static void WriteTradeDataVector(this MemoryBuffer buffer, System.Collections.Generic.List<SoftFX.Extended.Financial.Serialization.TradeData> arg)
		{
			buffer.WriteInt32(arg.Count);
			foreach(var element in arg)
			{
				buffer.WriteTradeData(element);
			}
		}
		public static SoftFX.Extended.Financial.Serialization.AccountData ReadAccountData(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Extended.Financial.Serialization.AccountData();
			result.Tag = buffer.ReadAString();
			result.Type = buffer.ReadAccountType();
			result.Leverage = buffer.ReadDouble();
			result.Balance = buffer.ReadDouble();
			result.Currency = buffer.ReadAString();
			result.Profit = buffer.ReadNullDouble();
			result.ProfitStatus = buffer.ReadAccountEntryStatus();
			result.Margin = buffer.ReadNullDouble();
			result.MarginStatus = buffer.ReadAccountEntryStatus();
			result.Trades = buffer.ReadTradeDataVector();
			return result;
		}
		public static void WriteAccountData(this MemoryBuffer buffer, SoftFX.Extended.Financial.Serialization.AccountData arg)
		{
			buffer.WriteAString(arg.Tag);
			buffer.WriteAccountType(arg.Type);
			buffer.WriteDouble(arg.Leverage);
			buffer.WriteDouble(arg.Balance);
			buffer.WriteAString(arg.Currency);
			buffer.WriteNullDouble(arg.Profit);
			buffer.WriteAccountEntryStatus(arg.ProfitStatus);
			buffer.WriteNullDouble(arg.Margin);
			buffer.WriteAccountEntryStatus(arg.MarginStatus);
			buffer.WriteTradeDataVector(arg.Trades);
		}
		public static System.Collections.Generic.List<SoftFX.Extended.Financial.Serialization.AccountData> ReadAccountDataVector(this MemoryBuffer buffer)
		{
			int length = buffer.ReadCount();
			var result = new System.Collections.Generic.List<SoftFX.Extended.Financial.Serialization.AccountData>(length);
			for(int index = 0; index < length; ++index)
			{
				result.Add(buffer.ReadAccountData());
			}
			return result;
		}
		public static void WriteAccountDataVector(this MemoryBuffer buffer, System.Collections.Generic.List<SoftFX.Extended.Financial.Serialization.AccountData> arg)
		{
			buffer.WriteInt32(arg.Count);
			foreach(var element in arg)
			{
				buffer.WriteAccountData(element);
			}
		}
		public static SoftFX.Extended.Financial.Serialization.CalculatorData ReadCalculatorData(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Extended.Financial.Serialization.CalculatorData();
			result.MarginMode = buffer.ReadMarginMode();
			result.Prices = buffer.ReadPriceDataVector();
			result.Symbols = buffer.ReadSymbolDataVector();
			result.Accounts = buffer.ReadAccountDataVector();
			result.Currencies = buffer.ReadAStringVector();
			return result;
		}
		public static void WriteCalculatorData(this MemoryBuffer buffer, SoftFX.Extended.Financial.Serialization.CalculatorData arg)
		{
			buffer.WriteMarginMode(arg.MarginMode);
			buffer.WritePriceDataVector(arg.Prices);
			buffer.WriteSymbolDataVector(arg.Symbols);
			buffer.WriteAccountDataVector(arg.Accounts);
			buffer.WriteAStringVector(arg.Currencies);
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
