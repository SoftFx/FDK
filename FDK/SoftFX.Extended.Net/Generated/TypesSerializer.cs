// This is always generated file. Do not change anything.

using SoftFX.Lrp;

namespace SoftFX.Extended.Generated
{
	internal static class TypesSerializer
	{
		public static System.ArgumentNullException ReadArgumentNullException(this MemoryBuffer buffer)
		{
			System.String _message = buffer.ReadAString();
			var result = new System.ArgumentNullException(_message);
			return result;
		}
		public static System.ArgumentException ReadArgumentException(this MemoryBuffer buffer)
		{
			System.String _message = buffer.ReadAString();
			var result = new System.ArgumentException(_message);
			return result;
		}
		public static SoftFX.Extended.Errors.InvalidHandleException ReadInvalidHandleException(this MemoryBuffer buffer)
		{
			System.String _message = buffer.ReadAString();
			var result = new SoftFX.Extended.Errors.InvalidHandleException(_message);
			return result;
		}
		public static SoftFX.Extended.Errors.RejectException ReadRejectException(this MemoryBuffer buffer)
		{
			System.String _message = buffer.ReadAString();
			var result = new SoftFX.Extended.Errors.RejectException(_message);
			result.Code = buffer.ReadInt32();
			return result;
		}
		public static SoftFX.Extended.Errors.TimeoutException ReadTimeoutException(this MemoryBuffer buffer)
		{
			System.String _message = buffer.ReadAString();
			var result = new SoftFX.Extended.Errors.TimeoutException(_message);
			result.WaitingInterval = buffer.ReadInt32();
			result.OperationId = buffer.ReadAString();
			return result;
		}
		public static SoftFX.Extended.Errors.SendException ReadSendException(this MemoryBuffer buffer)
		{
			System.String _message = buffer.ReadAString();
			var result = new SoftFX.Extended.Errors.SendException(_message);
			return result;
		}
		public static SoftFX.Extended.Errors.LogoutException ReadLogoutException(this MemoryBuffer buffer)
		{
			System.String _message = buffer.ReadAString();
			var result = new SoftFX.Extended.Errors.LogoutException(_message);
			return result;
		}
		public static SoftFX.Extended.Errors.UnsupportedFeatureException ReadUnsupportedFeatureException(this MemoryBuffer buffer)
		{
			System.String _message = buffer.ReadAString();
			var result = new SoftFX.Extended.Errors.UnsupportedFeatureException(_message);
			result.Feature = buffer.ReadAString();
			return result;
		}
		public static SoftFX.Extended.Errors.RuntimeException ReadRuntimeException(this MemoryBuffer buffer)
		{
			System.String _message = buffer.ReadAString();
			var result = new SoftFX.Extended.Errors.RuntimeException(_message);
			return result;
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
		public static SoftFX.Extended.Severity ReadSeverity(this MemoryBuffer buffer)
		{
			var result = (SoftFX.Extended.Severity)buffer.ReadInt32();
			return result;
		}
		public static void WriteSeverity(this MemoryBuffer buffer, SoftFX.Extended.Severity arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static SoftFX.Extended.NotificationType ReadNotificationType(this MemoryBuffer buffer)
		{
			var result = (SoftFX.Extended.NotificationType)buffer.ReadInt32();
			return result;
		}
		public static void WriteNotificationType(this MemoryBuffer buffer, SoftFX.Extended.NotificationType arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static SoftFX.Extended.ProfitCalcMode ReadProfitCalcMode(this MemoryBuffer buffer)
		{
			var result = (SoftFX.Extended.ProfitCalcMode)buffer.ReadInt32();
			return result;
		}
		public static void WriteProfitCalcMode(this MemoryBuffer buffer, SoftFX.Extended.ProfitCalcMode arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static SoftFX.Extended.MarginCalcMode ReadMarginCalcMode(this MemoryBuffer buffer)
		{
			var result = (SoftFX.Extended.MarginCalcMode)buffer.ReadInt32();
			return result;
		}
		public static void WriteMarginCalcMode(this MemoryBuffer buffer, SoftFX.Extended.MarginCalcMode arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static SoftFX.Extended.SessionStatus ReadSessionStatus(this MemoryBuffer buffer)
		{
			var result = (SoftFX.Extended.SessionStatus)buffer.ReadInt32();
			return result;
		}
		public static void WriteSessionStatus(this MemoryBuffer buffer, SoftFX.Extended.SessionStatus arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static SoftFX.Extended.TradeRecordSide ReadSide(this MemoryBuffer buffer)
		{
			var result = (SoftFX.Extended.TradeRecordSide)buffer.ReadInt32();
			return result;
		}
		public static void WriteSide(this MemoryBuffer buffer, SoftFX.Extended.TradeRecordSide arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static SoftFX.Extended.PriceType ReadPriceType(this MemoryBuffer buffer)
		{
			var result = (SoftFX.Extended.PriceType)buffer.ReadInt32();
			return result;
		}
		public static void WritePriceType(this MemoryBuffer buffer, SoftFX.Extended.PriceType arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static SoftFX.Extended.TradeRecordSide ReadTradeRecordSide(this MemoryBuffer buffer)
		{
			var result = (SoftFX.Extended.TradeRecordSide)buffer.ReadInt32();
			return result;
		}
		public static void WriteTradeRecordSide(this MemoryBuffer buffer, SoftFX.Extended.TradeRecordSide arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static SoftFX.Extended.TradeRecordType ReadTradeRecordType(this MemoryBuffer buffer)
		{
			var result = (SoftFX.Extended.TradeRecordType)buffer.ReadInt32();
			return result;
		}
		public static void WriteTradeRecordType(this MemoryBuffer buffer, SoftFX.Extended.TradeRecordType arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static System.Int32 ReadFxOrderType(this MemoryBuffer buffer)
		{
			var result = (System.Int32)buffer.ReadInt32();
			return result;
		}
		public static void WriteFxOrderType(this MemoryBuffer buffer, System.Int32 arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static SoftFX.Extended.OrderType ReadOrderType(this MemoryBuffer buffer)
		{
			var result = (SoftFX.Extended.OrderType)buffer.ReadInt32();
			return result;
		}
		public static void WriteOrderType(this MemoryBuffer buffer, SoftFX.Extended.OrderType arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static SoftFX.Extended.LogoutReason ReadLogoutReason(this MemoryBuffer buffer)
		{
			var result = (SoftFX.Extended.LogoutReason)buffer.ReadInt32();
			return result;
		}
		public static void WriteLogoutReason(this MemoryBuffer buffer, SoftFX.Extended.LogoutReason arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static SoftFX.Extended.TwoFactorReason ReadTwoFactorReason(this MemoryBuffer buffer)
		{
			var result = (SoftFX.Extended.TwoFactorReason)buffer.ReadInt32();
			return result;
		}
		public static void WriteTwoFactorReason(this MemoryBuffer buffer, SoftFX.Extended.TwoFactorReason arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static SoftFX.Extended.OrderStatus ReadOrderStatus(this MemoryBuffer buffer)
		{
			var result = (SoftFX.Extended.OrderStatus)buffer.ReadInt32();
			return result;
		}
		public static void WriteOrderStatus(this MemoryBuffer buffer, SoftFX.Extended.OrderStatus arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static SoftFX.Extended.ExecutionType ReadExecutionType(this MemoryBuffer buffer)
		{
			var result = (SoftFX.Extended.ExecutionType)buffer.ReadInt32();
			return result;
		}
		public static void WriteExecutionType(this MemoryBuffer buffer, SoftFX.Extended.ExecutionType arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static SoftFX.Extended.RejectReason ReadRejectReason(this MemoryBuffer buffer)
		{
			var result = (SoftFX.Extended.RejectReason)buffer.ReadInt32();
			return result;
		}
		public static void WriteRejectReason(this MemoryBuffer buffer, SoftFX.Extended.RejectReason arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static SoftFX.Extended.CommissionType ReadCommissionType(this MemoryBuffer buffer)
		{
			var result = (SoftFX.Extended.CommissionType)buffer.ReadInt32();
			return result;
		}
		public static void WriteCommissionType(this MemoryBuffer buffer, SoftFX.Extended.CommissionType arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static SoftFX.Extended.CommissionChargeType ReadCommissionChargeType(this MemoryBuffer buffer)
		{
			var result = (SoftFX.Extended.CommissionChargeType)buffer.ReadInt32();
			return result;
		}
		public static void WriteCommissionChargeType(this MemoryBuffer buffer, SoftFX.Extended.CommissionChargeType arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static SoftFX.Extended.CommissionChargeMethod ReadCommissionChargeMethod(this MemoryBuffer buffer)
		{
			var result = (SoftFX.Extended.CommissionChargeMethod)buffer.ReadInt32();
			return result;
		}
		public static void WriteCommissionChargeMethod(this MemoryBuffer buffer, SoftFX.Extended.CommissionChargeMethod arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static SoftFX.Extended.SwapType ReadSwapType(this MemoryBuffer buffer)
		{
			var result = (SoftFX.Extended.SwapType)buffer.ReadInt32();
			return result;
		}
		public static void WriteSwapType(this MemoryBuffer buffer, SoftFX.Extended.SwapType arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static SoftFX.Extended.Data.Notification ReadNotification(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Extended.Data.Notification();
			result.Severity = buffer.ReadSeverity();
			result.Type = buffer.ReadNotificationType();
			result.Text = buffer.ReadAString();
			result.Balance = buffer.ReadDouble();
			result.TransactionAmount = buffer.ReadDouble();
			result.TransactionCurrency = buffer.ReadAString();
			return result;
		}
		public static void WriteNotification(this MemoryBuffer buffer, SoftFX.Extended.Data.Notification arg)
		{
			buffer.WriteSeverity(arg.Severity);
			buffer.WriteNotificationType(arg.Type);
			buffer.WriteAString(arg.Text);
			buffer.WriteDouble(arg.Balance);
			buffer.WriteDouble(arg.TransactionAmount);
			buffer.WriteAString(arg.TransactionCurrency);
		}
		public static SoftFX.Extended.DataHistoryInfo ReadDataHistoryInfo(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Extended.DataHistoryInfo();
			result.FromAll = buffer.ReadTime();
			result.ToAll = buffer.ReadTime();
			result.From = buffer.ReadNullTime();
			result.To = buffer.ReadNullTime();
			result.LastTickId = buffer.ReadAString();
			result.Files = buffer.ReadStringArray();
			result.Bars = buffer.ReadBarArray();
			return result;
		}
		public static void WriteDataHistoryInfo(this MemoryBuffer buffer, SoftFX.Extended.DataHistoryInfo arg)
		{
			buffer.WriteTime(arg.FromAll);
			buffer.WriteTime(arg.ToAll);
			buffer.WriteNullTime(arg.From);
			buffer.WriteNullTime(arg.To);
			buffer.WriteAString(arg.LastTickId);
			buffer.WriteStringArray(arg.Files);
			buffer.WriteBarArray(arg.Bars);
		}
		public static SoftFX.Extended.SymbolInfo ReadSymbolInfo(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Extended.SymbolInfo();
			result.Name = buffer.ReadAString();
			result.Currency = buffer.ReadAString();
			result.SettlementCurrency = buffer.ReadAString();
			result.ContractMultiplier = buffer.ReadDouble();
			result.Description = buffer.ReadWString();
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
			result.MinCommission = buffer.ReadDouble();
			result.MinCommissionCurrency = buffer.ReadWString();
			result.SwapType = buffer.ReadSwapType();
			result.TripleSwapDay = buffer.ReadInt32();
			result.SwapSizeShort = buffer.ReadNullDouble();
			result.SwapSizeLong = buffer.ReadNullDouble();
			result.DefaultSlippage = buffer.ReadNullDouble();
			result.IsTradeEnabled = buffer.ReadBoolean();
			result.GroupSortOrder = buffer.ReadInt32();
			result.SortOrder = buffer.ReadInt32();
			result.CurrencySortOrder = buffer.ReadInt32();
			result.SettlementCurrencySortOrder = buffer.ReadInt32();
			result.CurrencyPrecision = buffer.ReadInt32();
			result.SettlementCurrencyPrecision = buffer.ReadInt32();
			result.StatusGroupId = buffer.ReadAString();
			result.SecurityName = buffer.ReadAString();
			result.SecurityDescription = buffer.ReadWString();
			result.StopOrderMarginReduction = buffer.ReadNullDouble();
			result.HiddenLimitOrderMarginReduction = buffer.ReadNullDouble();
			result.IsCloseOnly = buffer.ReadBoolean();
			return result;
		}
		public static void WriteSymbolInfo(this MemoryBuffer buffer, SoftFX.Extended.SymbolInfo arg)
		{
			buffer.WriteAString(arg.Name);
			buffer.WriteAString(arg.Currency);
			buffer.WriteAString(arg.SettlementCurrency);
			buffer.WriteDouble(arg.ContractMultiplier);
			buffer.WriteWString(arg.Description);
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
			buffer.WriteDouble(arg.MinCommission);
			buffer.WriteWString(arg.MinCommissionCurrency);
			buffer.WriteSwapType(arg.SwapType);
			buffer.WriteInt32(arg.TripleSwapDay);
			buffer.WriteNullDouble(arg.SwapSizeShort);
			buffer.WriteNullDouble(arg.SwapSizeLong);
			buffer.WriteNullDouble(arg.DefaultSlippage);
			buffer.WriteBoolean(arg.IsTradeEnabled);
			buffer.WriteInt32(arg.GroupSortOrder);
			buffer.WriteInt32(arg.SortOrder);
			buffer.WriteInt32(arg.CurrencySortOrder);
			buffer.WriteInt32(arg.SettlementCurrencySortOrder);
			buffer.WriteInt32(arg.CurrencyPrecision);
			buffer.WriteInt32(arg.SettlementCurrencyPrecision);
			buffer.WriteAString(arg.StatusGroupId);
			buffer.WriteAString(arg.SecurityName);
			buffer.WriteWString(arg.SecurityDescription);
			buffer.WriteNullDouble(arg.StopOrderMarginReduction);
			buffer.WriteNullDouble(arg.HiddenLimitOrderMarginReduction);
			buffer.WriteBoolean(arg.IsCloseOnly);
		}
		public static SoftFX.Extended.TwoFactorAuth ReadTwoFactorAuth(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Extended.TwoFactorAuth();
			result.Reason = buffer.ReadTwoFactorReason();
			result.Text = buffer.ReadAString();
			result.Expire = buffer.ReadTime();
			return result;
		}
		public static void WriteTwoFactorAuth(this MemoryBuffer buffer, SoftFX.Extended.TwoFactorAuth arg)
		{
			buffer.WriteTwoFactorReason(arg.Reason);
			buffer.WriteAString(arg.Text);
			buffer.WriteTime(arg.Expire);
		}
		public static SoftFX.Extended.StatusGroupInfo ReadStatusGroupInfo(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Extended.StatusGroupInfo();
			result.StatusGroupId = buffer.ReadAString();
			result.Status = buffer.ReadSessionStatus();
			result.StartTime = buffer.ReadTime();
			result.EndTime = buffer.ReadTime();
			result.OpenTime = buffer.ReadTime();
			result.CloseTime = buffer.ReadTime();
			return result;
		}
		public static void WriteStatusGroupInfo(this MemoryBuffer buffer, SoftFX.Extended.StatusGroupInfo arg)
		{
			buffer.WriteAString(arg.StatusGroupId);
			buffer.WriteSessionStatus(arg.Status);
			buffer.WriteTime(arg.StartTime);
			buffer.WriteTime(arg.EndTime);
			buffer.WriteTime(arg.OpenTime);
			buffer.WriteTime(arg.CloseTime);
		}
		public static SoftFX.Extended.StatusGroupInfo[] ReadStatusGroupInfoArray(this MemoryBuffer buffer)
		{
			int length = buffer.ReadCount();
			var result = new SoftFX.Extended.StatusGroupInfo[length];
			for(int index = 0; index < length; ++index)
			{
				result[index] = buffer.ReadStatusGroupInfo();
			}
			return result;
		}
		public static void WriteStatusGroupInfoArray(this MemoryBuffer buffer, SoftFX.Extended.StatusGroupInfo[] arg)
		{
			buffer.WriteInt32(arg.Length);
			foreach(var element in arg)
			{
				buffer.WriteStatusGroupInfo(element);
			}
		}
		public static SoftFX.Extended.SessionInfo ReadSessionInfo(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Extended.SessionInfo();
			result.TradingSessionId = buffer.ReadAString();
			result.Status = buffer.ReadSessionStatus();
			result.ServerTimeZoneOffset = buffer.ReadInt32();
			result.StartTime = buffer.ReadTime();
			result.OpenTime = buffer.ReadTime();
			result.CloseTime = buffer.ReadTime();
			result.EndTime = buffer.ReadTime();
			result.PlatformName = buffer.ReadWString();
			result.PlatformCompany = buffer.ReadWString();
			result.StatusGroups = buffer.ReadStatusGroupInfoArray();
			return result;
		}
		public static void WriteSessionInfo(this MemoryBuffer buffer, SoftFX.Extended.SessionInfo arg)
		{
			buffer.WriteAString(arg.TradingSessionId);
			buffer.WriteSessionStatus(arg.Status);
			buffer.WriteInt32(arg.ServerTimeZoneOffset);
			buffer.WriteTime(arg.StartTime);
			buffer.WriteTime(arg.OpenTime);
			buffer.WriteTime(arg.CloseTime);
			buffer.WriteTime(arg.EndTime);
			buffer.WriteWString(arg.PlatformName);
			buffer.WriteWString(arg.PlatformCompany);
			buffer.WriteStatusGroupInfoArray(arg.StatusGroups);
		}
		public static SoftFX.Extended.SymbolInfo[] ReadSymbolInfoArray(this MemoryBuffer buffer)
		{
			int length = buffer.ReadCount();
			var result = new SoftFX.Extended.SymbolInfo[length];
			for(int index = 0; index < length; ++index)
			{
				result[index] = buffer.ReadSymbolInfo();
			}
			return result;
		}
		public static void WriteSymbolInfoArray(this MemoryBuffer buffer, SoftFX.Extended.SymbolInfo[] arg)
		{
			buffer.WriteInt32(arg.Length);
			foreach(var element in arg)
			{
				buffer.WriteSymbolInfo(element);
			}
		}
		public static string[] ReadStringArray(this MemoryBuffer buffer)
		{
			int length = buffer.ReadCount();
			var result = new string[length];
			for(int index = 0; index < length; ++index)
			{
				result[index] = buffer.ReadAString();
			}
			return result;
		}
		public static void WriteStringArray(this MemoryBuffer buffer, string[] arg)
		{
			buffer.WriteInt32(arg.Length);
			foreach(var element in arg)
			{
				buffer.WriteAString(element);
			}
		}
		public static byte[] ReadByteArray(this MemoryBuffer buffer)
		{
			int length = buffer.ReadCount();
			var result = new byte[length];
			for(int index = 0; index < length; ++index)
			{
				result[index] = buffer.ReadUInt8();
			}
			return result;
		}
		public static void WriteByteArray(this MemoryBuffer buffer, byte[] arg)
		{
			buffer.WriteInt32(arg.Length);
			foreach(var element in arg)
			{
				buffer.WriteUInt8(element);
			}
		}
		public static SoftFX.Extended.AssetInfo ReadAssetInfo(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Extended.AssetInfo();
			result.Currency = buffer.ReadAString();
			result.Balance = buffer.ReadDouble();
			result.LockedAmount = buffer.ReadDouble();
			result.TradeAmount = buffer.ReadDouble();
			result.CurrencyToUsdConversionRate = buffer.ReadNullDouble();
			result.UsdToCurrencyConversionRate = buffer.ReadNullDouble();
			result.CurrencyToReportConversionRate = buffer.ReadNullDouble();
			result.ReportToCurrencyConversionRate = buffer.ReadNullDouble();
			return result;
		}
		public static void WriteAssetInfo(this MemoryBuffer buffer, SoftFX.Extended.AssetInfo arg)
		{
			buffer.WriteAString(arg.Currency);
			buffer.WriteDouble(arg.Balance);
			buffer.WriteDouble(arg.LockedAmount);
			buffer.WriteDouble(arg.TradeAmount);
			buffer.WriteNullDouble(arg.CurrencyToUsdConversionRate);
			buffer.WriteNullDouble(arg.UsdToCurrencyConversionRate);
			buffer.WriteNullDouble(arg.CurrencyToReportConversionRate);
			buffer.WriteNullDouble(arg.ReportToCurrencyConversionRate);
		}
		public static SoftFX.Extended.AssetInfo[] ReadAssetInfoArray(this MemoryBuffer buffer)
		{
			int length = buffer.ReadCount();
			var result = new SoftFX.Extended.AssetInfo[length];
			for(int index = 0; index < length; ++index)
			{
				result[index] = buffer.ReadAssetInfo();
			}
			return result;
		}
		public static void WriteAssetInfoArray(this MemoryBuffer buffer, SoftFX.Extended.AssetInfo[] arg)
		{
			buffer.WriteInt32(arg.Length);
			foreach(var element in arg)
			{
				buffer.WriteAssetInfo(element);
			}
		}
		public static SoftFX.Extended.TradeServerInfo ReadTradeServerInfo(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Extended.TradeServerInfo();
			result.CompanyName = buffer.ReadWString();
			result.CompanyFullName = buffer.ReadWString();
			result.CompanyDescription = buffer.ReadWString();
			result.CompanyAddress = buffer.ReadWString();
			result.CompanyEmail = buffer.ReadAString();
			result.CompanyPhone = buffer.ReadAString();
			result.CompanyWebSite = buffer.ReadAString();
			result.ServerName = buffer.ReadWString();
			result.ServerFullName = buffer.ReadWString();
			result.ServerDescription = buffer.ReadWString();
			result.ServerAddress = buffer.ReadAString();
			result.ServerFixFeedSslPort = buffer.ReadNullInt32();
			result.ServerFixTradeSslPort = buffer.ReadNullInt32();
			result.ServerWebSocketFeedPort = buffer.ReadNullInt32();
			result.ServerWebSocketTradePort = buffer.ReadNullInt32();
			result.ServerRestPort = buffer.ReadNullInt32();
			return result;
		}
		public static void WriteTradeServerInfo(this MemoryBuffer buffer, SoftFX.Extended.TradeServerInfo arg)
		{
			buffer.WriteWString(arg.CompanyName);
			buffer.WriteWString(arg.CompanyFullName);
			buffer.WriteWString(arg.CompanyDescription);
			buffer.WriteWString(arg.CompanyAddress);
			buffer.WriteAString(arg.CompanyEmail);
			buffer.WriteAString(arg.CompanyPhone);
			buffer.WriteAString(arg.CompanyWebSite);
			buffer.WriteWString(arg.ServerName);
			buffer.WriteWString(arg.ServerFullName);
			buffer.WriteWString(arg.ServerDescription);
			buffer.WriteAString(arg.ServerAddress);
			buffer.WriteNullInt32(arg.ServerFixFeedSslPort);
			buffer.WriteNullInt32(arg.ServerFixTradeSslPort);
			buffer.WriteNullInt32(arg.ServerWebSocketFeedPort);
			buffer.WriteNullInt32(arg.ServerWebSocketTradePort);
			buffer.WriteNullInt32(arg.ServerRestPort);
		}
		public static SoftFX.Extended.AccountInfo ReadAccountInfo(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Extended.AccountInfo();
			result.AccountId = buffer.ReadAString();
			result.Type = buffer.ReadAccountType();
			result.Name = buffer.ReadWString();
			result.Email = buffer.ReadAString();
			result.Comment = buffer.ReadWString();
			result.Currency = buffer.ReadAString();
			result.RegistredDate = buffer.ReadNullTime();
			result.ModifiedTime = buffer.ReadNullTime();
			result.Leverage = buffer.ReadInt32();
			result.Balance = buffer.ReadDouble();
			result.Margin = buffer.ReadDouble();
			result.Equity = buffer.ReadDouble();
			result.MarginCallLevel = buffer.ReadDouble();
			result.StopOutLevel = buffer.ReadDouble();
			result.IsValid = buffer.ReadBoolean();
			result.IsReadOnly = buffer.ReadBoolean();
			result.IsBlocked = buffer.ReadBoolean();
			result.Assets = buffer.ReadAssetInfoArray();
			result.SessionsPerAccount = buffer.ReadInt32();
			result.RequestsPerSecond = buffer.ReadInt32();
			result.ThrottlingMethods = buffer.ReadThrottlingMethodInfoArray();
			result.ReportCurrency = buffer.ReadAString();
			result.TokenCommissionCurrency = buffer.ReadAString();
			result.TokenCommissionCurrencyDiscount = buffer.ReadNullDouble();
			result.IsTokenCommissionEnabled = buffer.ReadBoolean();
			return result;
		}
		public static void WriteAccountInfo(this MemoryBuffer buffer, SoftFX.Extended.AccountInfo arg)
		{
			buffer.WriteAString(arg.AccountId);
			buffer.WriteAccountType(arg.Type);
			buffer.WriteWString(arg.Name);
			buffer.WriteAString(arg.Email);
			buffer.WriteWString(arg.Comment);
			buffer.WriteAString(arg.Currency);
			buffer.WriteNullTime(arg.RegistredDate);
			buffer.WriteNullTime(arg.ModifiedTime);
			buffer.WriteInt32(arg.Leverage);
			buffer.WriteDouble(arg.Balance);
			buffer.WriteDouble(arg.Margin);
			buffer.WriteDouble(arg.Equity);
			buffer.WriteDouble(arg.MarginCallLevel);
			buffer.WriteDouble(arg.StopOutLevel);
			buffer.WriteBoolean(arg.IsValid);
			buffer.WriteBoolean(arg.IsReadOnly);
			buffer.WriteBoolean(arg.IsBlocked);
			buffer.WriteAssetInfoArray(arg.Assets);
			buffer.WriteInt32(arg.SessionsPerAccount);
			buffer.WriteInt32(arg.RequestsPerSecond);
			buffer.WriteThrottlingMethodInfoArray(arg.ThrottlingMethods);
			buffer.WriteAString(arg.ReportCurrency);
			buffer.WriteAString(arg.TokenCommissionCurrency);
			buffer.WriteNullDouble(arg.TokenCommissionCurrencyDiscount);
			buffer.WriteBoolean(arg.IsTokenCommissionEnabled);
		}
		public static SoftFX.Extended.FxFileChunk ReadFileChunk(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Extended.FxFileChunk();
			result.FileId = buffer.ReadAString();
			result.FileName = buffer.ReadAString();
			result.FileSize = buffer.ReadInt32();
			result.ChunkId = buffer.ReadInt32();
			result.TotalChunks = buffer.ReadInt32();
			result.Data = buffer.ReadByteArray();
			return result;
		}
		public static void WriteFileChunk(this MemoryBuffer buffer, SoftFX.Extended.FxFileChunk arg)
		{
			buffer.WriteAString(arg.FileId);
			buffer.WriteAString(arg.FileName);
			buffer.WriteInt32(arg.FileSize);
			buffer.WriteInt32(arg.ChunkId);
			buffer.WriteInt32(arg.TotalChunks);
			buffer.WriteByteArray(arg.Data);
		}
		public static SoftFX.Extended.Data.FxOrder ReadFxOrder(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Extended.Data.FxOrder();
			result.OrderId = buffer.ReadAString();
			result.ClientOrderId = buffer.ReadAString();
			result.Symbol = buffer.ReadAString();
			result.InitialVolume = buffer.ReadDouble();
			result.Volume = buffer.ReadNullDouble();
			result.MaxVisibleVolume = buffer.ReadNullDouble();
			result.Price = buffer.ReadNullDouble();
			result.StopPrice = buffer.ReadNullDouble();
			result.TakeProfit = buffer.ReadNullDouble();
			result.StopLoss = buffer.ReadNullDouble();
			result.Commission = buffer.ReadDouble();
			result.AgentCommission = buffer.ReadDouble();
			result.Swap = buffer.ReadDouble();
			result.Profit = buffer.ReadNullDouble();
			result.InitialType = buffer.ReadFxOrderType();
			result.Type = buffer.ReadFxOrderType();
			result.Side = buffer.ReadTradeRecordSide();
			result.Expiration = buffer.ReadNullTime();
			result.Created = buffer.ReadNullTime();
			result.Modified = buffer.ReadNullTime();
			result.Comment = buffer.ReadWString();
			result.Tag = buffer.ReadWString();
			result.Magic = buffer.ReadNullInt32();
			result.IsReducedOpenCommission = buffer.ReadBoolean();
			result.IsReducedCloseCommission = buffer.ReadBoolean();
			result.ImmediateOrCancel = buffer.ReadBoolean();
			result.MarketWithSlippage = buffer.ReadBoolean();
			result.IOCOverride = buffer.ReadNullBoolean();
			result.IFMOverride = buffer.ReadNullBoolean();
			result.PrevVolume = buffer.ReadNullDouble();
			result.Slippage = buffer.ReadNullDouble();
			return result;
		}
		public static void WriteFxOrder(this MemoryBuffer buffer, SoftFX.Extended.Data.FxOrder arg)
		{
			buffer.WriteAString(arg.OrderId);
			buffer.WriteAString(arg.ClientOrderId);
			buffer.WriteAString(arg.Symbol);
			buffer.WriteDouble(arg.InitialVolume);
			buffer.WriteNullDouble(arg.Volume);
			buffer.WriteNullDouble(arg.MaxVisibleVolume);
			buffer.WriteNullDouble(arg.Price);
			buffer.WriteNullDouble(arg.StopPrice);
			buffer.WriteNullDouble(arg.TakeProfit);
			buffer.WriteNullDouble(arg.StopLoss);
			buffer.WriteDouble(arg.Commission);
			buffer.WriteDouble(arg.AgentCommission);
			buffer.WriteDouble(arg.Swap);
			buffer.WriteNullDouble(arg.Profit);
			buffer.WriteFxOrderType(arg.InitialType);
			buffer.WriteFxOrderType(arg.Type);
			buffer.WriteTradeRecordSide(arg.Side);
			buffer.WriteNullTime(arg.Expiration);
			buffer.WriteNullTime(arg.Created);
			buffer.WriteNullTime(arg.Modified);
			buffer.WriteWString(arg.Comment);
			buffer.WriteWString(arg.Tag);
			buffer.WriteNullInt32(arg.Magic);
			buffer.WriteBoolean(arg.IsReducedOpenCommission);
			buffer.WriteBoolean(arg.IsReducedCloseCommission);
			buffer.WriteBoolean(arg.ImmediateOrCancel);
			buffer.WriteBoolean(arg.MarketWithSlippage);
			buffer.WriteNullBoolean(arg.IOCOverride);
			buffer.WriteNullBoolean(arg.IFMOverride);
			buffer.WriteNullDouble(arg.PrevVolume);
			buffer.WriteNullDouble(arg.Slippage);
		}
		public static SoftFX.Extended.Data.FxOrder[] ReadFxOrderArray(this MemoryBuffer buffer)
		{
			int length = buffer.ReadCount();
			var result = new SoftFX.Extended.Data.FxOrder[length];
			for(int index = 0; index < length; ++index)
			{
				result[index] = buffer.ReadFxOrder();
			}
			return result;
		}
		public static void WriteFxOrderArray(this MemoryBuffer buffer, SoftFX.Extended.Data.FxOrder[] arg)
		{
			buffer.WriteInt32(arg.Length);
			foreach(var element in arg)
			{
				buffer.WriteFxOrder(element);
			}
		}
		public static SoftFX.Extended.Bar ReadBar(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Extended.Bar();
			result.Open = buffer.ReadDouble();
			result.Close = buffer.ReadDouble();
			result.High = buffer.ReadDouble();
			result.Low = buffer.ReadDouble();
			result.Volume = buffer.ReadDouble();
			result.From = buffer.ReadTime();
			return result;
		}
		public static void WriteBar(this MemoryBuffer buffer, SoftFX.Extended.Bar arg)
		{
			buffer.WriteDouble(arg.Open);
			buffer.WriteDouble(arg.Close);
			buffer.WriteDouble(arg.High);
			buffer.WriteDouble(arg.Low);
			buffer.WriteDouble(arg.Volume);
			buffer.WriteTime(arg.From);
		}
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
			result.Id = buffer.ReadAString();
			result.IndicativeTick = buffer.ReadBoolean();
			return result;
		}
		public static void WriteQuote(this MemoryBuffer buffer, SoftFX.Extended.Quote arg)
		{
			buffer.WriteAString(arg.Symbol);
			buffer.WriteTime(arg.CreatingTime);
			buffer.WriteQuoteEntryArray(arg.Bids);
			buffer.WriteQuoteEntryArray(arg.Asks);
			buffer.WriteAString(arg.Id);
			buffer.WriteBoolean(arg.IndicativeTick);
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
		public static SoftFX.Extended.Bar[] ReadBarArray(this MemoryBuffer buffer)
		{
			int length = buffer.ReadCount();
			var result = new SoftFX.Extended.Bar[length];
			for(int index = 0; index < length; ++index)
			{
				result[index] = buffer.ReadBar();
			}
			return result;
		}
		public static void WriteBarArray(this MemoryBuffer buffer, SoftFX.Extended.Bar[] arg)
		{
			buffer.WriteInt32(arg.Length);
			foreach(var element in arg)
			{
				buffer.WriteBar(element);
			}
		}
		public static SoftFX.Extended.Core.FxMessage ReadMessage(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Extended.Core.FxMessage();
			result.Type = buffer.ReadInt32();
			result.SendingTime = buffer.ReadNullTime();
			result.ReceivingTime = buffer.ReadNullTime();
			result.Data = buffer.ReadLocalPointer();
			return result;
		}
		public static void WriteMessage(this MemoryBuffer buffer, SoftFX.Extended.Core.FxMessage arg)
		{
			buffer.WriteInt32(arg.Type);
			buffer.WriteNullTime(arg.SendingTime);
			buffer.WriteNullTime(arg.ReceivingTime);
			buffer.WriteLocalPointer(arg.Data);
		}
		public static SoftFX.Extended.PosReportType ReadPositionReportType(this MemoryBuffer buffer)
		{
			var result = (SoftFX.Extended.PosReportType)buffer.ReadInt32();
			return result;
		}
		public static void WritePositionReportType(this MemoryBuffer buffer, SoftFX.Extended.PosReportType arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static SoftFX.Extended.Position ReadPosition(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Extended.Position();
			result.Symbol = buffer.ReadAString();
			result.SettlementPrice = buffer.ReadDouble();
			result.BuyAmount = buffer.ReadDouble();
			result.SellAmount = buffer.ReadDouble();
			result.Commission = buffer.ReadDouble();
			result.AgentCommission = buffer.ReadDouble();
			result.Swap = buffer.ReadDouble();
			result.Profit = buffer.ReadNullDouble();
			result.BuyPrice = buffer.ReadNullDouble();
			result.SellPrice = buffer.ReadNullDouble();
			result.PosModified = buffer.ReadNullTime();
			result.PosID = buffer.ReadAString();
			result.Margin = buffer.ReadNullDouble();
			result.CurrentBestAsk = buffer.ReadNullDouble();
			result.CurrentBestBid = buffer.ReadNullDouble();
			result.PosReportType = buffer.ReadPositionReportType();
			return result;
		}
		public static void WritePosition(this MemoryBuffer buffer, SoftFX.Extended.Position arg)
		{
			buffer.WriteAString(arg.Symbol);
			buffer.WriteDouble(arg.SettlementPrice);
			buffer.WriteDouble(arg.BuyAmount);
			buffer.WriteDouble(arg.SellAmount);
			buffer.WriteDouble(arg.Commission);
			buffer.WriteDouble(arg.AgentCommission);
			buffer.WriteDouble(arg.Swap);
			buffer.WriteNullDouble(arg.Profit);
			buffer.WriteNullDouble(arg.BuyPrice);
			buffer.WriteNullDouble(arg.SellPrice);
			buffer.WriteNullTime(arg.PosModified);
			buffer.WriteAString(arg.PosID);
			buffer.WriteNullDouble(arg.Margin);
			buffer.WriteNullDouble(arg.CurrentBestAsk);
			buffer.WriteNullDouble(arg.CurrentBestBid);
			buffer.WritePositionReportType(arg.PosReportType);
		}
		public static SoftFX.Extended.Position[] ReadPositionArray(this MemoryBuffer buffer)
		{
			int length = buffer.ReadCount();
			var result = new SoftFX.Extended.Position[length];
			for(int index = 0; index < length; ++index)
			{
				result[index] = buffer.ReadPosition();
			}
			return result;
		}
		public static void WritePositionArray(this MemoryBuffer buffer, SoftFX.Extended.Position[] arg)
		{
			buffer.WriteInt32(arg.Length);
			foreach(var element in arg)
			{
				buffer.WritePosition(element);
			}
		}
		public static SoftFX.Extended.Reports.TradeTransactionReportType ReadTradeTransactionReportType(this MemoryBuffer buffer)
		{
			var result = (SoftFX.Extended.Reports.TradeTransactionReportType)buffer.ReadInt32();
			return result;
		}
		public static void WriteTradeTransactionReportType(this MemoryBuffer buffer, SoftFX.Extended.Reports.TradeTransactionReportType arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static SoftFX.Extended.Reports.TradeTransactionReason ReadTradeTransactionReason(this MemoryBuffer buffer)
		{
			var result = (SoftFX.Extended.Reports.TradeTransactionReason)buffer.ReadInt32();
			return result;
		}
		public static void WriteTradeTransactionReason(this MemoryBuffer buffer, SoftFX.Extended.Reports.TradeTransactionReason arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static SoftFX.Extended.Reports.TradeTransactionReport ReadTradeTransactionReport(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Extended.Reports.TradeTransactionReport();
			result.TradeTransactionReportType = buffer.ReadTradeTransactionReportType();
			result.TradeTransactionReason = buffer.ReadTradeTransactionReason();
			result.AccountBalance = buffer.ReadDouble();
			result.TransactionAmount = buffer.ReadDouble();
			result.TransactionCurrency = buffer.ReadAString();
			result.Id = buffer.ReadAString();
			result.ClientId = buffer.ReadAString();
			result.Quantity = buffer.ReadDouble();
			result.MaxVisibleQuantity = buffer.ReadNullDouble();
			result.LeavesQuantity = buffer.ReadDouble();
			result.Price = buffer.ReadDouble();
			result.StopPrice = buffer.ReadDouble();
			result.InitialTradeRecordType = buffer.ReadTradeRecordType();
			result.TradeRecordType = buffer.ReadTradeRecordType();
			result.TradeRecordSide = buffer.ReadTradeRecordSide();
			result.Symbol = buffer.ReadAString();
			result.Comment = buffer.ReadWString();
			result.Tag = buffer.ReadWString();
			result.Magic = buffer.ReadNullInt32();
			result.IsReducedOpenCommission = buffer.ReadBoolean();
			result.IsReducedCloseCommission = buffer.ReadBoolean();
			result.ImmediateOrCancel = buffer.ReadBoolean();
			result.MarketWithSlippage = buffer.ReadBoolean();
			result.ReqOpenPrice = buffer.ReadNullDouble();
			result.ReqOpenQuantity = buffer.ReadNullDouble();
			result.ReqClosePrice = buffer.ReadNullDouble();
			result.ReqCloseQuantity = buffer.ReadNullDouble();
			result.OrderCreated = buffer.ReadTime();
			result.OrderModified = buffer.ReadTime();
			result.PositionId = buffer.ReadAString();
			result.PositionById = buffer.ReadAString();
			result.PositionOpened = buffer.ReadTime();
			result.PosOpenReqPrice = buffer.ReadDouble();
			result.PosOpenPrice = buffer.ReadDouble();
			result.PositionQuantity = buffer.ReadDouble();
			result.PositionLastQuantity = buffer.ReadDouble();
			result.PositionLeavesQuantity = buffer.ReadDouble();
			result.PositionCloseRequestedPrice = buffer.ReadDouble();
			result.PositionClosePrice = buffer.ReadDouble();
			result.PositionClosed = buffer.ReadTime();
			result.PositionModified = buffer.ReadTime();
			result.PosRemainingSide = buffer.ReadTradeRecordSide();
			result.PosRemainingPrice = buffer.ReadNullDouble();
			result.Commission = buffer.ReadDouble();
			result.AgentCommission = buffer.ReadDouble();
			result.Swap = buffer.ReadDouble();
			result.CommCurrency = buffer.ReadAString();
			result.StopLoss = buffer.ReadDouble();
			result.TakeProfit = buffer.ReadDouble();
			result.NextStreamPositionId = buffer.ReadAString();
			result.TransactionTime = buffer.ReadTime();
			result.OrderFillPrice = buffer.ReadNullDouble();
			result.OrderLastFillAmount = buffer.ReadNullDouble();
			result.OpenConversionRate = buffer.ReadNullDouble();
			result.CloseConversionRate = buffer.ReadNullDouble();
			result.ActionId = buffer.ReadInt32();
			result.Expiration = buffer.ReadNullTime();
			result.SrcAssetCurrency = buffer.ReadAString();
			result.SrcAssetAmount = buffer.ReadNullDouble();
			result.SrcAssetMovement = buffer.ReadNullDouble();
			result.DstAssetCurrency = buffer.ReadAString();
			result.DstAssetAmount = buffer.ReadNullDouble();
			result.DstAssetMovement = buffer.ReadNullDouble();
			result.MarginCurrencyToUsdConversionRate = buffer.ReadNullDouble();
			result.UsdToMarginCurrencyConversionRate = buffer.ReadNullDouble();
			result.MarginCurrency = buffer.ReadAString();
			result.ProfitCurrencyToUsdConversionRate = buffer.ReadNullDouble();
			result.UsdToProfitCurrencyConversionRate = buffer.ReadNullDouble();
			result.ProfitCurrency = buffer.ReadAString();
			result.SrcAssetToUsdConversionRate = buffer.ReadNullDouble();
			result.UsdToSrcAssetConversionRate = buffer.ReadNullDouble();
			result.DstAssetToUsdConversionRate = buffer.ReadNullDouble();
			result.UsdToDstAssetConversionRate = buffer.ReadNullDouble();
			result.MinCommissionCurrency = buffer.ReadAString();
			result.MinCommissionConversionRate = buffer.ReadNullDouble();
			result.Slippage = buffer.ReadNullDouble();
			result.MarginCurrencyToReportConversionRate = buffer.ReadNullDouble();
			result.ReportToMarginCurrencyConversionRate = buffer.ReadNullDouble();
			result.ProfitCurrencyToReportConversionRate = buffer.ReadNullDouble();
			result.ReportToProfitCurrencyConversionRate = buffer.ReadNullDouble();
			result.SrcAssetToReportConversionRate = buffer.ReadNullDouble();
			result.ReportToSrcAssetConversionRate = buffer.ReadNullDouble();
			result.DstAssetToReportConversionRate = buffer.ReadNullDouble();
			result.ReportToDstAssetConversionRate = buffer.ReadNullDouble();
			result.ReportCurrency = buffer.ReadAString();
			result.TokenCommissionCurrency = buffer.ReadAString();
			result.TokenCommissionCurrencyDiscount = buffer.ReadNullDouble();
			result.TokenCommissionConversionRate = buffer.ReadNullDouble();
			return result;
		}
		public static void WriteTradeTransactionReport(this MemoryBuffer buffer, SoftFX.Extended.Reports.TradeTransactionReport arg)
		{
			buffer.WriteTradeTransactionReportType(arg.TradeTransactionReportType);
			buffer.WriteTradeTransactionReason(arg.TradeTransactionReason);
			buffer.WriteDouble(arg.AccountBalance);
			buffer.WriteDouble(arg.TransactionAmount);
			buffer.WriteAString(arg.TransactionCurrency);
			buffer.WriteAString(arg.Id);
			buffer.WriteAString(arg.ClientId);
			buffer.WriteDouble(arg.Quantity);
			buffer.WriteNullDouble(arg.MaxVisibleQuantity);
			buffer.WriteDouble(arg.LeavesQuantity);
			buffer.WriteDouble(arg.Price);
			buffer.WriteDouble(arg.StopPrice);
			buffer.WriteTradeRecordType(arg.InitialTradeRecordType);
			buffer.WriteTradeRecordType(arg.TradeRecordType);
			buffer.WriteTradeRecordSide(arg.TradeRecordSide);
			buffer.WriteAString(arg.Symbol);
			buffer.WriteWString(arg.Comment);
			buffer.WriteWString(arg.Tag);
			buffer.WriteNullInt32(arg.Magic);
			buffer.WriteBoolean(arg.IsReducedOpenCommission);
			buffer.WriteBoolean(arg.IsReducedCloseCommission);
			buffer.WriteBoolean(arg.ImmediateOrCancel);
			buffer.WriteBoolean(arg.MarketWithSlippage);
			buffer.WriteNullDouble(arg.ReqOpenPrice);
			buffer.WriteNullDouble(arg.ReqOpenQuantity);
			buffer.WriteNullDouble(arg.ReqClosePrice);
			buffer.WriteNullDouble(arg.ReqCloseQuantity);
			buffer.WriteTime(arg.OrderCreated);
			buffer.WriteTime(arg.OrderModified);
			buffer.WriteAString(arg.PositionId);
			buffer.WriteAString(arg.PositionById);
			buffer.WriteTime(arg.PositionOpened);
			buffer.WriteDouble(arg.PosOpenReqPrice);
			buffer.WriteDouble(arg.PosOpenPrice);
			buffer.WriteDouble(arg.PositionQuantity);
			buffer.WriteDouble(arg.PositionLastQuantity);
			buffer.WriteDouble(arg.PositionLeavesQuantity);
			buffer.WriteDouble(arg.PositionCloseRequestedPrice);
			buffer.WriteDouble(arg.PositionClosePrice);
			buffer.WriteTime(arg.PositionClosed);
			buffer.WriteTime(arg.PositionModified);
			buffer.WriteTradeRecordSide(arg.PosRemainingSide);
			buffer.WriteNullDouble(arg.PosRemainingPrice);
			buffer.WriteDouble(arg.Commission);
			buffer.WriteDouble(arg.AgentCommission);
			buffer.WriteDouble(arg.Swap);
			buffer.WriteAString(arg.CommCurrency);
			buffer.WriteDouble(arg.StopLoss);
			buffer.WriteDouble(arg.TakeProfit);
			buffer.WriteAString(arg.NextStreamPositionId);
			buffer.WriteTime(arg.TransactionTime);
			buffer.WriteNullDouble(arg.OrderFillPrice);
			buffer.WriteNullDouble(arg.OrderLastFillAmount);
			buffer.WriteNullDouble(arg.OpenConversionRate);
			buffer.WriteNullDouble(arg.CloseConversionRate);
			buffer.WriteInt32(arg.ActionId);
			buffer.WriteNullTime(arg.Expiration);
			buffer.WriteAString(arg.SrcAssetCurrency);
			buffer.WriteNullDouble(arg.SrcAssetAmount);
			buffer.WriteNullDouble(arg.SrcAssetMovement);
			buffer.WriteAString(arg.DstAssetCurrency);
			buffer.WriteNullDouble(arg.DstAssetAmount);
			buffer.WriteNullDouble(arg.DstAssetMovement);
			buffer.WriteNullDouble(arg.MarginCurrencyToUsdConversionRate);
			buffer.WriteNullDouble(arg.UsdToMarginCurrencyConversionRate);
			buffer.WriteAString(arg.MarginCurrency);
			buffer.WriteNullDouble(arg.ProfitCurrencyToUsdConversionRate);
			buffer.WriteNullDouble(arg.UsdToProfitCurrencyConversionRate);
			buffer.WriteAString(arg.ProfitCurrency);
			buffer.WriteNullDouble(arg.SrcAssetToUsdConversionRate);
			buffer.WriteNullDouble(arg.UsdToSrcAssetConversionRate);
			buffer.WriteNullDouble(arg.DstAssetToUsdConversionRate);
			buffer.WriteNullDouble(arg.UsdToDstAssetConversionRate);
			buffer.WriteAString(arg.MinCommissionCurrency);
			buffer.WriteNullDouble(arg.MinCommissionConversionRate);
			buffer.WriteNullDouble(arg.Slippage);
			buffer.WriteNullDouble(arg.MarginCurrencyToReportConversionRate);
			buffer.WriteNullDouble(arg.ReportToMarginCurrencyConversionRate);
			buffer.WriteNullDouble(arg.ProfitCurrencyToReportConversionRate);
			buffer.WriteNullDouble(arg.ReportToProfitCurrencyConversionRate);
			buffer.WriteNullDouble(arg.SrcAssetToReportConversionRate);
			buffer.WriteNullDouble(arg.ReportToSrcAssetConversionRate);
			buffer.WriteNullDouble(arg.DstAssetToReportConversionRate);
			buffer.WriteNullDouble(arg.ReportToDstAssetConversionRate);
			buffer.WriteAString(arg.ReportCurrency);
			buffer.WriteAString(arg.TokenCommissionCurrency);
			buffer.WriteNullDouble(arg.TokenCommissionCurrencyDiscount);
			buffer.WriteNullDouble(arg.TokenCommissionConversionRate);
		}
		public static SoftFX.Extended.Reports.DailyAccountSnapshotReport ReadDailyAccountSnapshotReport(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Extended.Reports.DailyAccountSnapshotReport();
			result.Timestamp = buffer.ReadTime();
			result.AccountId = buffer.ReadAString();
			result.Type = buffer.ReadAccountType();
			result.BalanceCurrency = buffer.ReadAString();
			result.Leverage = buffer.ReadInt32();
			result.Balance = buffer.ReadDouble();
			result.Margin = buffer.ReadDouble();
			result.MarginLevel = buffer.ReadDouble();
			result.Equity = buffer.ReadDouble();
			result.Swap = buffer.ReadDouble();
			result.Profit = buffer.ReadDouble();
			result.Commission = buffer.ReadDouble();
			result.AgentCommission = buffer.ReadDouble();
			result.IsValid = buffer.ReadBoolean();
			result.IsReadOnly = buffer.ReadBoolean();
			result.IsBlocked = buffer.ReadBoolean();
			result.BalanceCurrencyToUsdConversionRate = buffer.ReadNullDouble();
			result.UsdToBalanceCurrencyConversionRate = buffer.ReadNullDouble();
			result.ProfitCurrencyToUsdConversionRate = buffer.ReadNullDouble();
			result.UsdToProfitCurrencyConversionRate = buffer.ReadNullDouble();
			result.BalanceCurrencyToReportConversionRate = buffer.ReadNullDouble();
			result.ReportToBalanceCurrencyConversionRate = buffer.ReadNullDouble();
			result.ProfitCurrencyToReportConversionRate = buffer.ReadNullDouble();
			result.ReportToProfitCurrencyConversionRate = buffer.ReadNullDouble();
			result.Assets = buffer.ReadAssetInfoArray();
			result.Positions = buffer.ReadPositionArray();
			result.ReportCurrency = buffer.ReadAString();
			result.TokenCommissionCurrency = buffer.ReadAString();
			result.TokenCommissionCurrencyDiscount = buffer.ReadNullDouble();
			result.IsTokenCommissionEnabled = buffer.ReadBoolean();
			return result;
		}
		public static void WriteDailyAccountSnapshotReport(this MemoryBuffer buffer, SoftFX.Extended.Reports.DailyAccountSnapshotReport arg)
		{
			buffer.WriteTime(arg.Timestamp);
			buffer.WriteAString(arg.AccountId);
			buffer.WriteAccountType(arg.Type);
			buffer.WriteAString(arg.BalanceCurrency);
			buffer.WriteInt32(arg.Leverage);
			buffer.WriteDouble(arg.Balance);
			buffer.WriteDouble(arg.Margin);
			buffer.WriteDouble(arg.MarginLevel);
			buffer.WriteDouble(arg.Equity);
			buffer.WriteDouble(arg.Swap);
			buffer.WriteDouble(arg.Profit);
			buffer.WriteDouble(arg.Commission);
			buffer.WriteDouble(arg.AgentCommission);
			buffer.WriteBoolean(arg.IsValid);
			buffer.WriteBoolean(arg.IsReadOnly);
			buffer.WriteBoolean(arg.IsBlocked);
			buffer.WriteNullDouble(arg.BalanceCurrencyToUsdConversionRate);
			buffer.WriteNullDouble(arg.UsdToBalanceCurrencyConversionRate);
			buffer.WriteNullDouble(arg.ProfitCurrencyToUsdConversionRate);
			buffer.WriteNullDouble(arg.UsdToProfitCurrencyConversionRate);
			buffer.WriteNullDouble(arg.BalanceCurrencyToReportConversionRate);
			buffer.WriteNullDouble(arg.ReportToBalanceCurrencyConversionRate);
			buffer.WriteNullDouble(arg.ProfitCurrencyToReportConversionRate);
			buffer.WriteNullDouble(arg.ReportToProfitCurrencyConversionRate);
			buffer.WriteAssetInfoArray(arg.Assets);
			buffer.WritePositionArray(arg.Positions);
			buffer.WriteAString(arg.ReportCurrency);
			buffer.WriteAString(arg.TokenCommissionCurrency);
			buffer.WriteNullDouble(arg.TokenCommissionCurrencyDiscount);
			buffer.WriteBoolean(arg.IsTokenCommissionEnabled);
		}
		public static SoftFX.Extended.ClosePositionResult ReadClosePositionResult(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Extended.ClosePositionResult();
			result.ExecutedVolume = buffer.ReadDouble();
			result.ExecutedPrice = buffer.ReadDouble();
			result.Sucess = buffer.ReadBoolean();
			return result;
		}
		public static void WriteClosePositionResult(this MemoryBuffer buffer, SoftFX.Extended.ClosePositionResult arg)
		{
			buffer.WriteDouble(arg.ExecutedVolume);
			buffer.WriteDouble(arg.ExecutedPrice);
			buffer.WriteBoolean(arg.Sucess);
		}
		public static SoftFX.Extended.ExecutionReport ReadExecutionReport(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Extended.ExecutionReport();
			result.OrderId = buffer.ReadAString();
			result.ClientOrderId = buffer.ReadAString();
			result.TradeRequestId = buffer.ReadAString();
			result.OrderStatus = buffer.ReadOrderStatus();
			result.ExecutionType = buffer.ReadExecutionType();
			result.Symbol = buffer.ReadAString();
			result.ExecutedVolume = buffer.ReadDouble();
			result.InitialVolume = buffer.ReadNullDouble();
			result.LeavesVolume = buffer.ReadDouble();
			result.MaxVisibleVolume = buffer.ReadNullDouble();
			result.TradeAmount = buffer.ReadNullDouble();
			result.Commission = buffer.ReadDouble();
			result.AgentCommission = buffer.ReadDouble();
			result.Swap = buffer.ReadDouble();
			result.InitialOrderType = buffer.ReadTradeRecordType();
			result.OrderType = buffer.ReadTradeRecordType();
			result.OrderSide = buffer.ReadTradeRecordSide();
			result.AveragePrice = buffer.ReadNullDouble();
			result.Price = buffer.ReadNullDouble();
			result.StopPrice = buffer.ReadNullDouble();
			result.TradePrice = buffer.ReadDouble();
			result.Expiration = buffer.ReadNullTime();
			result.Created = buffer.ReadNullTime();
			result.Modified = buffer.ReadNullTime();
			result.RejectReason = buffer.ReadRejectReason();
			result.TakeProfit = buffer.ReadNullDouble();
			result.StopLoss = buffer.ReadNullDouble();
			result.Text = buffer.ReadAString();
			result.Comment = buffer.ReadWString();
			result.Tag = buffer.ReadWString();
			result.Magic = buffer.ReadNullInt32();
			result.ClosePositionRequestId = buffer.ReadAString();
			result.Assets = buffer.ReadAssetInfoArray();
			result.Balance = buffer.ReadDouble();
			result.IsReducedOpenCommission = buffer.ReadBoolean();
			result.IsReducedCloseCommission = buffer.ReadBoolean();
			result.ImmediateOrCancel = buffer.ReadBoolean();
			result.MarketWithSlippage = buffer.ReadBoolean();
			result.ReqOpenPrice = buffer.ReadNullDouble();
			result.ReqOpenVolume = buffer.ReadNullDouble();
			result.Slippage = buffer.ReadNullDouble();
			return result;
		}
		public static void WriteExecutionReport(this MemoryBuffer buffer, SoftFX.Extended.ExecutionReport arg)
		{
			buffer.WriteAString(arg.OrderId);
			buffer.WriteAString(arg.ClientOrderId);
			buffer.WriteAString(arg.TradeRequestId);
			buffer.WriteOrderStatus(arg.OrderStatus);
			buffer.WriteExecutionType(arg.ExecutionType);
			buffer.WriteAString(arg.Symbol);
			buffer.WriteDouble(arg.ExecutedVolume);
			buffer.WriteNullDouble(arg.InitialVolume);
			buffer.WriteDouble(arg.LeavesVolume);
			buffer.WriteNullDouble(arg.MaxVisibleVolume);
			buffer.WriteNullDouble(arg.TradeAmount);
			buffer.WriteDouble(arg.Commission);
			buffer.WriteDouble(arg.AgentCommission);
			buffer.WriteDouble(arg.Swap);
			buffer.WriteTradeRecordType(arg.InitialOrderType);
			buffer.WriteTradeRecordType(arg.OrderType);
			buffer.WriteTradeRecordSide(arg.OrderSide);
			buffer.WriteNullDouble(arg.AveragePrice);
			buffer.WriteNullDouble(arg.Price);
			buffer.WriteNullDouble(arg.StopPrice);
			buffer.WriteDouble(arg.TradePrice);
			buffer.WriteNullTime(arg.Expiration);
			buffer.WriteNullTime(arg.Created);
			buffer.WriteNullTime(arg.Modified);
			buffer.WriteRejectReason(arg.RejectReason);
			buffer.WriteNullDouble(arg.TakeProfit);
			buffer.WriteNullDouble(arg.StopLoss);
			buffer.WriteAString(arg.Text);
			buffer.WriteWString(arg.Comment);
			buffer.WriteWString(arg.Tag);
			buffer.WriteNullInt32(arg.Magic);
			buffer.WriteAString(arg.ClosePositionRequestId);
			buffer.WriteAssetInfoArray(arg.Assets);
			buffer.WriteDouble(arg.Balance);
			buffer.WriteBoolean(arg.IsReducedOpenCommission);
			buffer.WriteBoolean(arg.IsReducedCloseCommission);
			buffer.WriteBoolean(arg.ImmediateOrCancel);
			buffer.WriteBoolean(arg.MarketWithSlippage);
			buffer.WriteNullDouble(arg.ReqOpenPrice);
			buffer.WriteNullDouble(arg.ReqOpenVolume);
			buffer.WriteNullDouble(arg.Slippage);
		}
		public static SoftFX.Extended.CurrencyInfo ReadCurrencyInfo(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Extended.CurrencyInfo();
			result.Name = buffer.ReadAString();
			result.Description = buffer.ReadWString();
			result.SortOrder = buffer.ReadInt32();
			result.Precision = buffer.ReadInt32();
			return result;
		}
		public static void WriteCurrencyInfo(this MemoryBuffer buffer, SoftFX.Extended.CurrencyInfo arg)
		{
			buffer.WriteAString(arg.Name);
			buffer.WriteWString(arg.Description);
			buffer.WriteInt32(arg.SortOrder);
			buffer.WriteInt32(arg.Precision);
		}
		public static SoftFX.Extended.CurrencyInfo[] ReadCurrencyInfoArray(this MemoryBuffer buffer)
		{
			int length = buffer.ReadCount();
			var result = new SoftFX.Extended.CurrencyInfo[length];
			for(int index = 0; index < length; ++index)
			{
				result[index] = buffer.ReadCurrencyInfo();
			}
			return result;
		}
		public static void WriteCurrencyInfoArray(this MemoryBuffer buffer, SoftFX.Extended.CurrencyInfo[] arg)
		{
			buffer.WriteInt32(arg.Length);
			foreach(var element in arg)
			{
				buffer.WriteCurrencyInfo(element);
			}
		}
		public static SoftFX.Extended.ThrottlingMethod ReadFxThrottlingMethod(this MemoryBuffer buffer)
		{
			var result = (SoftFX.Extended.ThrottlingMethod)buffer.ReadInt32();
			return result;
		}
		public static void WriteFxThrottlingMethod(this MemoryBuffer buffer, SoftFX.Extended.ThrottlingMethod arg)
		{
			buffer.WriteInt32((int)arg);
		}
		public static SoftFX.Extended.ThrottlingMethodInfo ReadThrottlingMethodInfo(this MemoryBuffer buffer)
		{
			var result = new SoftFX.Extended.ThrottlingMethodInfo();
			result.Method = buffer.ReadFxThrottlingMethod();
			result.RequestsPerSecond = buffer.ReadInt32();
			return result;
		}
		public static void WriteThrottlingMethodInfo(this MemoryBuffer buffer, SoftFX.Extended.ThrottlingMethodInfo arg)
		{
			buffer.WriteFxThrottlingMethod(arg.Method);
			buffer.WriteInt32(arg.RequestsPerSecond);
		}
		public static SoftFX.Extended.ThrottlingMethodInfo[] ReadThrottlingMethodInfoArray(this MemoryBuffer buffer)
		{
			int length = buffer.ReadCount();
			var result = new SoftFX.Extended.ThrottlingMethodInfo[length];
			for(int index = 0; index < length; ++index)
			{
				result[index] = buffer.ReadThrottlingMethodInfo();
			}
			return result;
		}
		public static void WriteThrottlingMethodInfoArray(this MemoryBuffer buffer, SoftFX.Extended.ThrottlingMethodInfo[] arg)
		{
			buffer.WriteInt32(arg.Length);
			foreach(var element in arg)
			{
				buffer.WriteThrottlingMethodInfo(element);
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
			if(0 == _id)
			{
				throw ReadArgumentNullException(buffer);
			}
			if(1 == _id)
			{
				throw ReadArgumentException(buffer);
			}
			if(2 == _id)
			{
				throw ReadInvalidHandleException(buffer);
			}
			if(3 == _id)
			{
				throw ReadRejectException(buffer);
			}
			if(4 == _id)
			{
				throw ReadTimeoutException(buffer);
			}
			if(5 == _id)
			{
				throw ReadSendException(buffer);
			}
			if(6 == _id)
			{
				throw ReadLogoutException(buffer);
			}
			if(7 == _id)
			{
				throw ReadUnsupportedFeatureException(buffer);
			}
			if(8 == _id)
			{
				throw ReadRuntimeException(buffer);
			}
			System.String _message = buffer.ReadAString();
			throw new System.Exception(_message);
		}
	}
}
