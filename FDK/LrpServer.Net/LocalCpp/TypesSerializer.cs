// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace LrpServer.Net.LocalCpp
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
		public static byte[] ReadUInt8Array(this MemoryBuffer buffer)
		{
			int length = buffer.ReadCount();
			var result = new byte[length];
			for(int index = 0; index < length; ++index)
			{
				result[index] = buffer.ReadUInt8();
			}
			return result;
		}
		public static void WriteUInt8Array(this MemoryBuffer buffer, byte[] arg)
		{
			buffer.WriteInt32(arg.Length);
			foreach(var element in arg)
			{
				buffer.WriteUInt8(element);
			}
		}
		public static LrpServer.Net.LrpProfitCalcMode ReadProfitCalcMode(this MemoryBuffer buffer)
		{
			var result = (LrpServer.Net.LrpProfitCalcMode)buffer.ReadInt32();
			return result;
		}
		public static void WriteProfitCalcMode(this MemoryBuffer buffer, LrpServer.Net.LrpProfitCalcMode arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static LrpServer.Net.LrpMarginCalcMode ReadMarginCalcMode(this MemoryBuffer buffer)
		{
			var result = (LrpServer.Net.LrpMarginCalcMode)buffer.ReadInt32();
			return result;
		}
		public static void WriteMarginCalcMode(this MemoryBuffer buffer, LrpServer.Net.LrpMarginCalcMode arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static LrpServer.Net.LrpSessionStatus ReadSessionStatus(this MemoryBuffer buffer)
		{
			var result = (LrpServer.Net.LrpSessionStatus)buffer.ReadInt32();
			return result;
		}
		public static void WriteSessionStatus(this MemoryBuffer buffer, LrpServer.Net.LrpSessionStatus arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static LrpServer.Net.LrpCommissionType ReadCommissionType(this MemoryBuffer buffer)
		{
			var result = (LrpServer.Net.LrpCommissionType)buffer.ReadInt32();
			return result;
		}
		public static void WriteCommissionType(this MemoryBuffer buffer, LrpServer.Net.LrpCommissionType arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static LrpServer.Net.LrpCommissionChargeType ReadCommissionChargeType(this MemoryBuffer buffer)
		{
			var result = (LrpServer.Net.LrpCommissionChargeType)buffer.ReadInt32();
			return result;
		}
		public static void WriteCommissionChargeType(this MemoryBuffer buffer, LrpServer.Net.LrpCommissionChargeType arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static LrpServer.Net.LrpCommissionChargeMethod ReadCommissionChargeMethod(this MemoryBuffer buffer)
		{
			var result = (LrpServer.Net.LrpCommissionChargeMethod)buffer.ReadInt32();
			return result;
		}
		public static void WriteCommissionChargeMethod(this MemoryBuffer buffer, LrpServer.Net.LrpCommissionChargeMethod arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static LrpServer.Net.LrpMarketHistoryRejectType ReadMarketHistoryRejectType(this MemoryBuffer buffer)
		{
			var result = (LrpServer.Net.LrpMarketHistoryRejectType)buffer.ReadInt32();
			return result;
		}
		public static void WriteMarketHistoryRejectType(this MemoryBuffer buffer, LrpServer.Net.LrpMarketHistoryRejectType arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static LrpServer.Net.LrpNotificationType ReadNotificationType(this MemoryBuffer buffer)
		{
			var result = (LrpServer.Net.LrpNotificationType)buffer.ReadInt32();
			return result;
		}
		public static void WriteNotificationType(this MemoryBuffer buffer, LrpServer.Net.LrpNotificationType arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static LrpServer.Net.LrpSeverity ReadSeverity(this MemoryBuffer buffer)
		{
			var result = (LrpServer.Net.LrpSeverity)buffer.ReadInt32();
			return result;
		}
		public static void WriteSeverity(this MemoryBuffer buffer, LrpServer.Net.LrpSeverity arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static LrpServer.Net.LrpSessionInfo ReadLrpSessionInfo(this MemoryBuffer buffer)
		{
			var result = new LrpServer.Net.LrpSessionInfo();
			result.TradingSessionId = buffer.ReadAString();
			result.Status = buffer.ReadSessionStatus();
			result.ServerTimeZoneOffset = buffer.ReadInt32();
			result.PlatformName = buffer.ReadAString();
			result.PlatformCompany = buffer.ReadAString();
			result.StartTime = buffer.ReadTime();
			result.OpenTime = buffer.ReadTime();
			result.CloseTime = buffer.ReadTime();
			result.EndTime = buffer.ReadTime();
			return result;
		}
		public static void WriteLrpSessionInfo(this MemoryBuffer buffer, LrpServer.Net.LrpSessionInfo arg)
		{
			buffer.WriteAString(arg.TradingSessionId);
			buffer.WriteSessionStatus(arg.Status);
			buffer.WriteInt32(arg.ServerTimeZoneOffset);
			buffer.WriteAString(arg.PlatformName);
			buffer.WriteAString(arg.PlatformCompany);
			buffer.WriteTime(arg.StartTime);
			buffer.WriteTime(arg.OpenTime);
			buffer.WriteTime(arg.CloseTime);
			buffer.WriteTime(arg.EndTime);
		}
		public static LrpServer.Net.LrpSymbolInfo ReadSymbolInfo(this MemoryBuffer buffer)
		{
			var result = new LrpServer.Net.LrpSymbolInfo();
			result.Name = buffer.ReadAString();
			result.Currency = buffer.ReadAString();
			result.SettlementCurrency = buffer.ReadAString();
			result.ContractMultiplier = buffer.ReadDouble();
			result.Precision = buffer.ReadInt32();
			result.RoundLot = buffer.ReadDouble();
			result.MinTradeVolume = buffer.ReadDouble();
			result.MaxTradeVolume = buffer.ReadDouble();
			result.TradeVolumeStep = buffer.ReadDouble();
			result.ProfitCalcMode = buffer.ReadProfitCalcMode();
			result.MarginCalcMode = buffer.ReadMarginCalcMode();
			result.MarginHedge = buffer.ReadDouble();
			result.MarginFactor = buffer.ReadInt32();
			result.MarginFactorFractional = buffer.ReadNullDouble();
			result.Color = buffer.ReadInt32();
			result.CommissionType = buffer.ReadCommissionType();
			result.CommissionChargeType = buffer.ReadCommissionChargeType();
			result.CommissionChargeMethod = buffer.ReadCommissionChargeMethod();
			result.LimitsCommission = buffer.ReadDouble();
			result.Commission = buffer.ReadDouble();
			result.SwapSizeShort = buffer.ReadNullDouble();
			result.SwapSizeLong = buffer.ReadNullDouble();
			result.IsTradeEnabled = buffer.ReadBoolean();
			result.GroupSortOrder = buffer.ReadInt32();
			result.SortOrder = buffer.ReadInt32();
			result.CurrencySortOrder = buffer.ReadInt32();
			result.SettlementCurrencySortOrder = buffer.ReadInt32();
			result.CurrencyPrecision = buffer.ReadInt32();
			result.SettlementCurrencyPrecision = buffer.ReadInt32();
			return result;
		}
		public static void WriteSymbolInfo(this MemoryBuffer buffer, LrpServer.Net.LrpSymbolInfo arg)
		{
			buffer.WriteAString(arg.Name);
			buffer.WriteAString(arg.Currency);
			buffer.WriteAString(arg.SettlementCurrency);
			buffer.WriteDouble(arg.ContractMultiplier);
			buffer.WriteInt32(arg.Precision);
			buffer.WriteDouble(arg.RoundLot);
			buffer.WriteDouble(arg.MinTradeVolume);
			buffer.WriteDouble(arg.MaxTradeVolume);
			buffer.WriteDouble(arg.TradeVolumeStep);
			buffer.WriteProfitCalcMode(arg.ProfitCalcMode);
			buffer.WriteMarginCalcMode(arg.MarginCalcMode);
			buffer.WriteDouble(arg.MarginHedge);
			buffer.WriteInt32(arg.MarginFactor);
			buffer.WriteNullDouble(arg.MarginFactorFractional);
			buffer.WriteInt32(arg.Color);
			buffer.WriteCommissionType(arg.CommissionType);
			buffer.WriteCommissionChargeType(arg.CommissionChargeType);
			buffer.WriteCommissionChargeMethod(arg.CommissionChargeMethod);
			buffer.WriteDouble(arg.LimitsCommission);
			buffer.WriteDouble(arg.Commission);
			buffer.WriteNullDouble(arg.SwapSizeShort);
			buffer.WriteNullDouble(arg.SwapSizeLong);
			buffer.WriteBoolean(arg.IsTradeEnabled);
			buffer.WriteInt32(arg.GroupSortOrder);
			buffer.WriteInt32(arg.SortOrder);
			buffer.WriteInt32(arg.CurrencySortOrder);
			buffer.WriteInt32(arg.SettlementCurrencySortOrder);
			buffer.WriteInt32(arg.CurrencyPrecision);
			buffer.WriteInt32(arg.SettlementCurrencyPrecision);
		}
		public static LrpServer.Net.LrpSymbolInfo[] ReadSymbolInfoArray(this MemoryBuffer buffer)
		{
			int length = buffer.ReadCount();
			var result = new LrpServer.Net.LrpSymbolInfo[length];
			for(int index = 0; index < length; ++index)
			{
				result[index] = buffer.ReadSymbolInfo();
			}
			return result;
		}
		public static void WriteSymbolInfoArray(this MemoryBuffer buffer, LrpServer.Net.LrpSymbolInfo[] arg)
		{
			buffer.WriteInt32(arg.Length);
			foreach(var element in arg)
			{
				buffer.WriteSymbolInfo(element);
			}
		}
		public static LrpServer.Net.LrpCurrencyInfo ReadCurrencyInfo(this MemoryBuffer buffer)
		{
			var result = new LrpServer.Net.LrpCurrencyInfo();
			result.Name = buffer.ReadAString();
			result.Description = buffer.ReadAString();
			result.SortOrder = buffer.ReadInt32();
			result.Precision = buffer.ReadInt32();
			return result;
		}
		public static void WriteCurrencyInfo(this MemoryBuffer buffer, LrpServer.Net.LrpCurrencyInfo arg)
		{
			buffer.WriteAString(arg.Name);
			buffer.WriteAString(arg.Description);
			buffer.WriteInt32(arg.SortOrder);
			buffer.WriteInt32(arg.Precision);
		}
		public static LrpServer.Net.LrpCurrencyInfo[] ReadCurrencyInfoArray(this MemoryBuffer buffer)
		{
			int length = buffer.ReadCount();
			var result = new LrpServer.Net.LrpCurrencyInfo[length];
			for(int index = 0; index < length; ++index)
			{
				result[index] = buffer.ReadCurrencyInfo();
			}
			return result;
		}
		public static void WriteCurrencyInfoArray(this MemoryBuffer buffer, LrpServer.Net.LrpCurrencyInfo[] arg)
		{
			buffer.WriteInt32(arg.Length);
			foreach(var element in arg)
			{
				buffer.WriteCurrencyInfo(element);
			}
		}
		public static LrpServer.Net.LrpQuoteEntry ReadQuoteEntry(this MemoryBuffer buffer)
		{
			var result = new LrpServer.Net.LrpQuoteEntry();
			result.Price = buffer.ReadDouble();
			result.Volume = buffer.ReadDouble();
			return result;
		}
		public static void WriteQuoteEntry(this MemoryBuffer buffer, LrpServer.Net.LrpQuoteEntry arg)
		{
			buffer.WriteDouble(arg.Price);
			buffer.WriteDouble(arg.Volume);
		}
		public static LrpServer.Net.LrpQuoteEntry[] ReadQuoteEntryArray(this MemoryBuffer buffer)
		{
			int length = buffer.ReadCount();
			var result = new LrpServer.Net.LrpQuoteEntry[length];
			for(int index = 0; index < length; ++index)
			{
				result[index] = buffer.ReadQuoteEntry();
			}
			return result;
		}
		public static void WriteQuoteEntryArray(this MemoryBuffer buffer, LrpServer.Net.LrpQuoteEntry[] arg)
		{
			buffer.WriteInt32(arg.Length);
			foreach(var element in arg)
			{
				buffer.WriteQuoteEntry(element);
			}
		}
		public static LrpServer.Net.LrpQuote ReadQuote(this MemoryBuffer buffer)
		{
			var result = new LrpServer.Net.LrpQuote();
			result.Symbol = buffer.ReadAString();
			result.CreatingTime = buffer.ReadTime();
			result.Bids = buffer.ReadQuoteEntryArray();
			result.Asks = buffer.ReadQuoteEntryArray();
			result.Id = buffer.ReadAString();
			return result;
		}
		public static void WriteQuote(this MemoryBuffer buffer, LrpServer.Net.LrpQuote arg)
		{
			buffer.WriteAString(arg.Symbol);
			buffer.WriteTime(arg.CreatingTime);
			buffer.WriteQuoteEntryArray(arg.Bids);
			buffer.WriteQuoteEntryArray(arg.Asks);
			buffer.WriteAString(arg.Id);
		}
		public static LrpServer.Net.LrpQuote[] ReadQuoteArray(this MemoryBuffer buffer)
		{
			int length = buffer.ReadCount();
			var result = new LrpServer.Net.LrpQuote[length];
			for(int index = 0; index < length; ++index)
			{
				result[index] = buffer.ReadQuote();
			}
			return result;
		}
		public static void WriteQuoteArray(this MemoryBuffer buffer, LrpServer.Net.LrpQuote[] arg)
		{
			buffer.WriteInt32(arg.Length);
			foreach(var element in arg)
			{
				buffer.WriteQuote(element);
			}
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
		public static LrpServer.Net.LrpFileChunk ReadFileChunk(this MemoryBuffer buffer)
		{
			var result = new LrpServer.Net.LrpFileChunk();
			result.FileId = buffer.ReadAString();
			result.ChunkId = buffer.ReadInt32();
			result.TotalChunks = buffer.ReadInt32();
			result.FileSize = buffer.ReadInt32();
			result.Data = buffer.ReadUInt8Array();
			return result;
		}
		public static void WriteFileChunk(this MemoryBuffer buffer, LrpServer.Net.LrpFileChunk arg)
		{
			buffer.WriteAString(arg.FileId);
			buffer.WriteInt32(arg.ChunkId);
			buffer.WriteInt32(arg.TotalChunks);
			buffer.WriteInt32(arg.FileSize);
			buffer.WriteUInt8Array(arg.Data);
		}
		public static LrpServer.Net.LrpParams ReadLrpParams(this MemoryBuffer buffer)
		{
			var result = new LrpServer.Net.LrpParams();
			result.EnableCodec = buffer.ReadBoolean();
			result.ValidateCodec = buffer.ReadBoolean();
			result.ThreadsNumber = buffer.ReadInt32();
			result.MessagesNumberLimit = buffer.ReadInt32();
			result.MessagesSizeLimit = buffer.ReadInt32();
			result.HandshakeTimeout = buffer.ReadInt32();
			result.HeartbeatTimeout = buffer.ReadInt32();
			result.LogPath = buffer.ReadAString();
			return result;
		}
		public static void WriteLrpParams(this MemoryBuffer buffer, LrpServer.Net.LrpParams arg)
		{
			buffer.WriteBoolean(arg.EnableCodec);
			buffer.WriteBoolean(arg.ValidateCodec);
			buffer.WriteInt32(arg.ThreadsNumber);
			buffer.WriteInt32(arg.MessagesNumberLimit);
			buffer.WriteInt32(arg.MessagesSizeLimit);
			buffer.WriteInt32(arg.HandshakeTimeout);
			buffer.WriteInt32(arg.HeartbeatTimeout);
			buffer.WriteAString(arg.LogPath);
		}
		public static LrpServer.Net.LrpNotification ReadNotification(this MemoryBuffer buffer)
		{
			var result = new LrpServer.Net.LrpNotification();
			result.Severity = buffer.ReadSeverity();
			result.Type = buffer.ReadNotificationType();
			result.Text = buffer.ReadAString();
			result.Balance = buffer.ReadDouble();
			result.TransactionAmount = buffer.ReadDouble();
			result.TransactionCurrency = buffer.ReadAString();
			return result;
		}
		public static void WriteNotification(this MemoryBuffer buffer, LrpServer.Net.LrpNotification arg)
		{
			buffer.WriteSeverity(arg.Severity);
			buffer.WriteNotificationType(arg.Type);
			buffer.WriteAString(arg.Text);
			buffer.WriteDouble(arg.Balance);
			buffer.WriteDouble(arg.TransactionAmount);
			buffer.WriteAString(arg.TransactionCurrency);
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
