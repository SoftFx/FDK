// This is always generated file. Do not change anything.

namespace
{
	void WriteLogoutReason(const FxLogoutReason& arg, MemoryBuffer& buffer);
	FxLogoutReason ReadLogoutReason(MemoryBuffer& buffer);
	void WriteTwoFactorReason(const FxTwoFactorReason& arg, MemoryBuffer& buffer);
	FxTwoFactorReason ReadTwoFactorReason(MemoryBuffer& buffer);
	void WriteSessionStatus(const SessionStatus& arg, MemoryBuffer& buffer);
	SessionStatus ReadSessionStatus(MemoryBuffer& buffer);
	void WriteProfitCalcMode(const ProfitCalcMode& arg, MemoryBuffer& buffer);
	ProfitCalcMode ReadProfitCalcMode(MemoryBuffer& buffer);
	void WriteMarginCalcMode(const MarginCalcMode& arg, MemoryBuffer& buffer);
	MarginCalcMode ReadMarginCalcMode(MemoryBuffer& buffer);
	void WriteCommissionType(const FxCommissionType& arg, MemoryBuffer& buffer);
	FxCommissionType ReadCommissionType(MemoryBuffer& buffer);
	void WriteCommissionChargeType(const FxCommissionChargeType& arg, MemoryBuffer& buffer);
	FxCommissionChargeType ReadCommissionChargeType(MemoryBuffer& buffer);
	void WriteCommissionChargeMethod(const FxCommissionChargeMethod& arg, MemoryBuffer& buffer);
	FxCommissionChargeMethod ReadCommissionChargeMethod(MemoryBuffer& buffer);
	void WriteMarketHistoryRejectType(const FxMarketHistoryRejectType& arg, MemoryBuffer& buffer);
	FxMarketHistoryRejectType ReadMarketHistoryRejectType(MemoryBuffer& buffer);
	void WriteNotificationType(const NotificationType& arg, MemoryBuffer& buffer);
	NotificationType ReadNotificationType(MemoryBuffer& buffer);
	void WriteSeverity(const Severity& arg, MemoryBuffer& buffer);
	Severity ReadSeverity(MemoryBuffer& buffer);
	void WriteLrpSessionInfo(const CFxSessionInfo& arg, MemoryBuffer& buffer);
	CFxSessionInfo ReadLrpSessionInfo(MemoryBuffer& buffer);
	void WriteStatusGroupInfo(const CFxStatusGroupInfo& arg, MemoryBuffer& buffer);
	CFxStatusGroupInfo ReadStatusGroupInfo(MemoryBuffer& buffer);
	void WriteStatusGroupInfoArray(const std::vector<CFxStatusGroupInfo>& arg, MemoryBuffer& buffer);
	std::vector<CFxStatusGroupInfo> ReadStatusGroupInfoArray(MemoryBuffer& buffer);
	void WriteLrpSessionInfo2(const CFxSessionInfo& arg, MemoryBuffer& buffer);
	CFxSessionInfo ReadLrpSessionInfo2(MemoryBuffer& buffer);
	void WriteCurrencyInfo(const CFxCurrencyInfo& arg, MemoryBuffer& buffer);
	CFxCurrencyInfo ReadCurrencyInfo(MemoryBuffer& buffer);
	void WriteSymbolInfo(const CFxSymbolInfo& arg, MemoryBuffer& buffer);
	CFxSymbolInfo ReadSymbolInfo(MemoryBuffer& buffer);
	void WriteSymbolInfo2(const CFxSymbolInfo& arg, MemoryBuffer& buffer);
	CFxSymbolInfo ReadSymbolInfo2(MemoryBuffer& buffer);
	void WriteSymbolInfo3(const CFxSymbolInfo& arg, MemoryBuffer& buffer);
	CFxSymbolInfo ReadSymbolInfo3(MemoryBuffer& buffer);
	void WriteSymbolInfo4(const CFxSymbolInfo& arg, MemoryBuffer& buffer);
	CFxSymbolInfo ReadSymbolInfo4(MemoryBuffer& buffer);
	void WriteSymbolInfo5(const CFxSymbolInfo& arg, MemoryBuffer& buffer);
	CFxSymbolInfo ReadSymbolInfo5(MemoryBuffer& buffer);
	void WriteSymbolInfo6(const CFxSymbolInfo& arg, MemoryBuffer& buffer);
	CFxSymbolInfo ReadSymbolInfo6(MemoryBuffer& buffer);
	void WriteSymbolInfo7(const CFxSymbolInfo& arg, MemoryBuffer& buffer);
	CFxSymbolInfo ReadSymbolInfo7(MemoryBuffer& buffer);
	void WriteAStringArray(const std::vector<std::string>& arg, MemoryBuffer& buffer);
	std::vector<std::string> ReadAStringArray(MemoryBuffer& buffer);
	void WriteUInt8Array(const std::vector<unsigned __int8>& arg, MemoryBuffer& buffer);
	std::vector<unsigned __int8> ReadUInt8Array(MemoryBuffer& buffer);
	void WriteSymbolInfoArray(const std::vector<CFxSymbolInfo>& arg, MemoryBuffer& buffer);
	std::vector<CFxSymbolInfo> ReadSymbolInfoArray(MemoryBuffer& buffer);
	void WriteCurrencyInfoArray(const std::vector<CFxCurrencyInfo>& arg, MemoryBuffer& buffer);
	std::vector<CFxCurrencyInfo> ReadCurrencyInfoArray(MemoryBuffer& buffer);
	void WriteSymbolInfoArray2(const std::vector<CFxSymbolInfo>& arg, MemoryBuffer& buffer);
	std::vector<CFxSymbolInfo> ReadSymbolInfoArray2(MemoryBuffer& buffer);
	void WriteSymbolInfoArray3(const std::vector<CFxSymbolInfo>& arg, MemoryBuffer& buffer);
	std::vector<CFxSymbolInfo> ReadSymbolInfoArray3(MemoryBuffer& buffer);
	void WriteSymbolInfoArray4(const std::vector<CFxSymbolInfo>& arg, MemoryBuffer& buffer);
	std::vector<CFxSymbolInfo> ReadSymbolInfoArray4(MemoryBuffer& buffer);
	void WriteSymbolInfoArray5(const std::vector<CFxSymbolInfo>& arg, MemoryBuffer& buffer);
	std::vector<CFxSymbolInfo> ReadSymbolInfoArray5(MemoryBuffer& buffer);
	void WriteSymbolInfoArray6(const std::vector<CFxSymbolInfo>& arg, MemoryBuffer& buffer);
	std::vector<CFxSymbolInfo> ReadSymbolInfoArray6(MemoryBuffer& buffer);
	void WriteSymbolInfoArray7(const std::vector<CFxSymbolInfo>& arg, MemoryBuffer& buffer);
	std::vector<CFxSymbolInfo> ReadSymbolInfoArray7(MemoryBuffer& buffer);
	void WriteQuoteEntry(const CFxQuoteEntry& arg, MemoryBuffer& buffer);
	CFxQuoteEntry ReadQuoteEntry(MemoryBuffer& buffer);
	void WriteQuoteEntryArray(const std::vector<CFxQuoteEntry>& arg, MemoryBuffer& buffer);
	std::vector<CFxQuoteEntry> ReadQuoteEntryArray(MemoryBuffer& buffer);
	void WriteQuote(const CFxQuote& arg, MemoryBuffer& buffer);
	CFxQuote ReadQuote(MemoryBuffer& buffer);
	void WriteQuoteArray(const std::vector<CFxQuote>& arg, MemoryBuffer& buffer);
	std::vector<CFxQuote> ReadQuoteArray(MemoryBuffer& buffer);
	void WriteBar(const CFxBar& arg, MemoryBuffer& buffer);
	CFxBar ReadBar(MemoryBuffer& buffer);
	void WriteBarArray(const std::vector<CFxBar>& arg, MemoryBuffer& buffer);
	std::vector<CFxBar> ReadBarArray(MemoryBuffer& buffer);
	void WriteDataHistoryResponse(const CFxDataHistoryResponse& arg, MemoryBuffer& buffer);
	CFxDataHistoryResponse ReadDataHistoryResponse(MemoryBuffer& buffer);
	void WriteFileChunk(const CFxFileChunk& arg, MemoryBuffer& buffer);
	CFxFileChunk ReadFileChunk(MemoryBuffer& buffer);
	void WriteNotification(const CNotification& arg, MemoryBuffer& buffer);
	CNotification ReadNotification(MemoryBuffer& buffer);
}

namespace
{
	void WriteLogoutReason(const FxLogoutReason& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	FxLogoutReason ReadLogoutReason(MemoryBuffer& buffer)
	{
		auto result = (FxLogoutReason)ReadInt32(buffer);
		return result;
	}
	void WriteTwoFactorReason(const FxTwoFactorReason& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	FxTwoFactorReason ReadTwoFactorReason(MemoryBuffer& buffer)
	{
		auto result = (FxTwoFactorReason)ReadInt32(buffer);
		return result;
	}
	void WriteSessionStatus(const SessionStatus& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	SessionStatus ReadSessionStatus(MemoryBuffer& buffer)
	{
		auto result = (SessionStatus)ReadInt32(buffer);
		return result;
	}
	void WriteProfitCalcMode(const ProfitCalcMode& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	ProfitCalcMode ReadProfitCalcMode(MemoryBuffer& buffer)
	{
		auto result = (ProfitCalcMode)ReadInt32(buffer);
		return result;
	}
	void WriteMarginCalcMode(const MarginCalcMode& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	MarginCalcMode ReadMarginCalcMode(MemoryBuffer& buffer)
	{
		auto result = (MarginCalcMode)ReadInt32(buffer);
		return result;
	}
	void WriteCommissionType(const FxCommissionType& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	FxCommissionType ReadCommissionType(MemoryBuffer& buffer)
	{
		auto result = (FxCommissionType)ReadInt32(buffer);
		return result;
	}
	void WriteCommissionChargeType(const FxCommissionChargeType& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	FxCommissionChargeType ReadCommissionChargeType(MemoryBuffer& buffer)
	{
		auto result = (FxCommissionChargeType)ReadInt32(buffer);
		return result;
	}
	void WriteCommissionChargeMethod(const FxCommissionChargeMethod& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	FxCommissionChargeMethod ReadCommissionChargeMethod(MemoryBuffer& buffer)
	{
		auto result = (FxCommissionChargeMethod)ReadInt32(buffer);
		return result;
	}
	void WriteMarketHistoryRejectType(const FxMarketHistoryRejectType& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	FxMarketHistoryRejectType ReadMarketHistoryRejectType(MemoryBuffer& buffer)
	{
		auto result = (FxMarketHistoryRejectType)ReadInt32(buffer);
		return result;
	}
	void WriteNotificationType(const NotificationType& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	NotificationType ReadNotificationType(MemoryBuffer& buffer)
	{
		auto result = (NotificationType)ReadInt32(buffer);
		return result;
	}
	void WriteSeverity(const Severity& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	Severity ReadSeverity(MemoryBuffer& buffer)
	{
		auto result = (Severity)ReadInt32(buffer);
		return result;
	}
	void WriteLrpSessionInfo(const CFxSessionInfo& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.TradingSessionId, buffer);
		WriteSessionStatus(arg.Status, buffer);
		WriteInt32(arg.ServerTimeZoneOffset, buffer);
		WriteTime(arg.StartTime, buffer);
		WriteTime(arg.OpenTime, buffer);
		WriteTime(arg.CloseTime, buffer);
		WriteTime(arg.EndTime, buffer);
	}
	CFxSessionInfo ReadLrpSessionInfo(MemoryBuffer& buffer)
	{
		CFxSessionInfo result = CFxSessionInfo();
		result.TradingSessionId = ReadAString(buffer);
		result.Status = ReadSessionStatus(buffer);
		result.ServerTimeZoneOffset = ReadInt32(buffer);
		result.StartTime = ReadTime(buffer);
		result.OpenTime = ReadTime(buffer);
		result.CloseTime = ReadTime(buffer);
		result.EndTime = ReadTime(buffer);
		return result;
	}
	void WriteStatusGroupInfo(const CFxStatusGroupInfo& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.StatusGroupId, buffer);
		WriteSessionStatus(arg.Status, buffer);
		WriteTime(arg.StartTime, buffer);
		WriteTime(arg.EndTime, buffer);
		WriteTime(arg.OpenTime, buffer);
		WriteTime(arg.CloseTime, buffer);
	}
	CFxStatusGroupInfo ReadStatusGroupInfo(MemoryBuffer& buffer)
	{
		CFxStatusGroupInfo result = CFxStatusGroupInfo();
		result.StatusGroupId = ReadAString(buffer);
		result.Status = ReadSessionStatus(buffer);
		result.StartTime = ReadTime(buffer);
		result.EndTime = ReadTime(buffer);
		result.OpenTime = ReadTime(buffer);
		result.CloseTime = ReadTime(buffer);
		return result;
	}
	void WriteStatusGroupInfoArray(const std::vector<CFxStatusGroupInfo>& arg, MemoryBuffer& buffer)
	{
		WriteUInt32((unsigned __int32)arg.size(), buffer);
		for each(const auto element in arg)
		{
			WriteStatusGroupInfo(element, buffer);
		}
	}
	std::vector<CFxStatusGroupInfo> ReadStatusGroupInfoArray(MemoryBuffer& buffer)
	{
		const size_t count = buffer.ReadCount();
		std::vector<CFxStatusGroupInfo> result;
		result.reserve(count);
		for(size_t index = 0; index < count; ++index)
		{
			result.push_back(ReadStatusGroupInfo(buffer));
		}
		return result;
	}
	void WriteLrpSessionInfo2(const CFxSessionInfo& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.TradingSessionId, buffer);
		WriteSessionStatus(arg.Status, buffer);
		WriteInt32(arg.ServerTimeZoneOffset, buffer);
		WriteAString(arg.PlatformName, buffer);
		WriteAString(arg.PlatformCompany, buffer);
		WriteTime(arg.StartTime, buffer);
		WriteTime(arg.OpenTime, buffer);
		WriteTime(arg.CloseTime, buffer);
		WriteTime(arg.EndTime, buffer);
		WriteStatusGroupInfoArray(arg.StatusGroups, buffer);
	}
	CFxSessionInfo ReadLrpSessionInfo2(MemoryBuffer& buffer)
	{
		CFxSessionInfo result = CFxSessionInfo();
		result.TradingSessionId = ReadAString(buffer);
		result.Status = ReadSessionStatus(buffer);
		result.ServerTimeZoneOffset = ReadInt32(buffer);
		result.PlatformName = ReadAString(buffer);
		result.PlatformCompany = ReadAString(buffer);
		result.StartTime = ReadTime(buffer);
		result.OpenTime = ReadTime(buffer);
		result.CloseTime = ReadTime(buffer);
		result.EndTime = ReadTime(buffer);
		result.StatusGroups = ReadStatusGroupInfoArray(buffer);
		return result;
	}
	void WriteCurrencyInfo(const CFxCurrencyInfo& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.Name, buffer);
		WriteWString(arg.Description, buffer);
		WriteInt32(arg.SortOrder, buffer);
		WriteInt32(arg.Precision, buffer);
	}
	CFxCurrencyInfo ReadCurrencyInfo(MemoryBuffer& buffer)
	{
		CFxCurrencyInfo result = CFxCurrencyInfo();
		result.Name = ReadAString(buffer);
		result.Description = ReadWString(buffer);
		result.SortOrder = ReadInt32(buffer);
		result.Precision = ReadInt32(buffer);
		return result;
	}
	void WriteSymbolInfo(const CFxSymbolInfo& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.Name, buffer);
		WriteAString(arg.Currency, buffer);
		WriteAString(arg.SettlementCurrency, buffer);
		WriteDouble(arg.ContractMultiplier, buffer);
		WriteInt32(arg.Precision, buffer);
		WriteDouble(arg.RoundLot, buffer);
		WriteDouble(arg.MinTradeVolume, buffer);
		WriteDouble(arg.MaxTradeVolume, buffer);
		WriteDouble(arg.TradeVolumeStep, buffer);
		WriteProfitCalcMode(arg.ProfitCalcMode, buffer);
		WriteMarginCalcMode(arg.MarginCalcMode, buffer);
		WriteDouble(arg.MarginHedge, buffer);
		WriteInt32(arg.MarginFactor, buffer);
		WriteInt32(arg.Color, buffer);
	}
	CFxSymbolInfo ReadSymbolInfo(MemoryBuffer& buffer)
	{
		CFxSymbolInfo result = CFxSymbolInfo();
		result.Name = ReadAString(buffer);
		result.Currency = ReadAString(buffer);
		result.SettlementCurrency = ReadAString(buffer);
		result.ContractMultiplier = ReadDouble(buffer);
		result.Precision = ReadInt32(buffer);
		result.RoundLot = ReadDouble(buffer);
		result.MinTradeVolume = ReadDouble(buffer);
		result.MaxTradeVolume = ReadDouble(buffer);
		result.TradeVolumeStep = ReadDouble(buffer);
		result.ProfitCalcMode = ReadProfitCalcMode(buffer);
		result.MarginCalcMode = ReadMarginCalcMode(buffer);
		result.MarginHedge = ReadDouble(buffer);
		result.MarginFactor = ReadInt32(buffer);
		result.Color = ReadInt32(buffer);
		return result;
	}
	void WriteSymbolInfo2(const CFxSymbolInfo& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.Name, buffer);
		WriteAString(arg.Currency, buffer);
		WriteAString(arg.SettlementCurrency, buffer);
		WriteDouble(arg.ContractMultiplier, buffer);
		WriteInt32(arg.Precision, buffer);
		WriteDouble(arg.RoundLot, buffer);
		WriteDouble(arg.MinTradeVolume, buffer);
		WriteDouble(arg.MaxTradeVolume, buffer);
		WriteDouble(arg.TradeVolumeStep, buffer);
		WriteProfitCalcMode(arg.ProfitCalcMode, buffer);
		WriteMarginCalcMode(arg.MarginCalcMode, buffer);
		WriteDouble(arg.MarginHedge, buffer);
		WriteInt32(arg.MarginFactor, buffer);
		WriteInt32(arg.Color, buffer);
		WriteCommissionType(arg.CommissionType, buffer);
		WriteCommissionChargeType(arg.CommissionChargeType, buffer);
		WriteDouble(arg.LimitsCommission, buffer);
		WriteDouble(arg.Commission, buffer);
		WriteNullDouble(arg.SwapSizeShort, buffer);
		WriteNullDouble(arg.SwapSizeLong, buffer);
	}
	CFxSymbolInfo ReadSymbolInfo2(MemoryBuffer& buffer)
	{
		CFxSymbolInfo result = CFxSymbolInfo();
		result.Name = ReadAString(buffer);
		result.Currency = ReadAString(buffer);
		result.SettlementCurrency = ReadAString(buffer);
		result.ContractMultiplier = ReadDouble(buffer);
		result.Precision = ReadInt32(buffer);
		result.RoundLot = ReadDouble(buffer);
		result.MinTradeVolume = ReadDouble(buffer);
		result.MaxTradeVolume = ReadDouble(buffer);
		result.TradeVolumeStep = ReadDouble(buffer);
		result.ProfitCalcMode = ReadProfitCalcMode(buffer);
		result.MarginCalcMode = ReadMarginCalcMode(buffer);
		result.MarginHedge = ReadDouble(buffer);
		result.MarginFactor = ReadInt32(buffer);
		result.Color = ReadInt32(buffer);
		result.CommissionType = ReadCommissionType(buffer);
		result.CommissionChargeType = ReadCommissionChargeType(buffer);
		result.LimitsCommission = ReadDouble(buffer);
		result.Commission = ReadDouble(buffer);
		result.SwapSizeShort = ReadNullDouble(buffer);
		result.SwapSizeLong = ReadNullDouble(buffer);
		return result;
	}
	void WriteSymbolInfo3(const CFxSymbolInfo& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.Name, buffer);
		WriteAString(arg.Currency, buffer);
		WriteAString(arg.SettlementCurrency, buffer);
		WriteDouble(arg.ContractMultiplier, buffer);
		WriteInt32(arg.Precision, buffer);
		WriteDouble(arg.RoundLot, buffer);
		WriteDouble(arg.MinTradeVolume, buffer);
		WriteDouble(arg.MaxTradeVolume, buffer);
		WriteDouble(arg.TradeVolumeStep, buffer);
		WriteProfitCalcMode(arg.ProfitCalcMode, buffer);
		WriteMarginCalcMode(arg.MarginCalcMode, buffer);
		WriteDouble(arg.MarginHedge, buffer);
		WriteInt32(arg.MarginFactor, buffer);
		WriteInt32(arg.Color, buffer);
		WriteCommissionType(arg.CommissionType, buffer);
		WriteCommissionChargeType(arg.CommissionChargeType, buffer);
		WriteDouble(arg.LimitsCommission, buffer);
		WriteDouble(arg.Commission, buffer);
		WriteNullDouble(arg.SwapSizeShort, buffer);
		WriteNullDouble(arg.SwapSizeLong, buffer);
		WriteNullDouble(arg.DefaultSlippage, buffer);
		WriteBoolean(arg.IsTradeEnabled, buffer);
	}
	CFxSymbolInfo ReadSymbolInfo3(MemoryBuffer& buffer)
	{
		CFxSymbolInfo result = CFxSymbolInfo();
		result.Name = ReadAString(buffer);
		result.Currency = ReadAString(buffer);
		result.SettlementCurrency = ReadAString(buffer);
		result.ContractMultiplier = ReadDouble(buffer);
		result.Precision = ReadInt32(buffer);
		result.RoundLot = ReadDouble(buffer);
		result.MinTradeVolume = ReadDouble(buffer);
		result.MaxTradeVolume = ReadDouble(buffer);
		result.TradeVolumeStep = ReadDouble(buffer);
		result.ProfitCalcMode = ReadProfitCalcMode(buffer);
		result.MarginCalcMode = ReadMarginCalcMode(buffer);
		result.MarginHedge = ReadDouble(buffer);
		result.MarginFactor = ReadInt32(buffer);
		result.Color = ReadInt32(buffer);
		result.CommissionType = ReadCommissionType(buffer);
		result.CommissionChargeType = ReadCommissionChargeType(buffer);
		result.LimitsCommission = ReadDouble(buffer);
		result.Commission = ReadDouble(buffer);
		result.SwapSizeShort = ReadNullDouble(buffer);
		result.SwapSizeLong = ReadNullDouble(buffer);
		result.DefaultSlippage = ReadNullDouble(buffer);
		result.IsTradeEnabled = ReadBoolean(buffer);
		return result;
	}
	void WriteSymbolInfo4(const CFxSymbolInfo& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.Name, buffer);
		WriteAString(arg.Currency, buffer);
		WriteAString(arg.SettlementCurrency, buffer);
		WriteDouble(arg.ContractMultiplier, buffer);
		WriteInt32(arg.Precision, buffer);
		WriteDouble(arg.RoundLot, buffer);
		WriteDouble(arg.MinTradeVolume, buffer);
		WriteDouble(arg.MaxTradeVolume, buffer);
		WriteDouble(arg.TradeVolumeStep, buffer);
		WriteProfitCalcMode(arg.ProfitCalcMode, buffer);
		WriteMarginCalcMode(arg.MarginCalcMode, buffer);
		WriteDouble(arg.MarginHedge, buffer);
		WriteInt32(arg.MarginFactor, buffer);
		WriteInt32(arg.Color, buffer);
		WriteCommissionType(arg.CommissionType, buffer);
		WriteCommissionChargeType(arg.CommissionChargeType, buffer);
		WriteDouble(arg.LimitsCommission, buffer);
		WriteDouble(arg.Commission, buffer);
		WriteNullDouble(arg.SwapSizeShort, buffer);
		WriteNullDouble(arg.SwapSizeLong, buffer);
		WriteNullDouble(arg.DefaultSlippage, buffer);
		WriteBoolean(arg.IsTradeEnabled, buffer);
		WriteInt32(arg.GroupSortOrder, buffer);
		WriteInt32(arg.SortOrder, buffer);
	}
	CFxSymbolInfo ReadSymbolInfo4(MemoryBuffer& buffer)
	{
		CFxSymbolInfo result = CFxSymbolInfo();
		result.Name = ReadAString(buffer);
		result.Currency = ReadAString(buffer);
		result.SettlementCurrency = ReadAString(buffer);
		result.ContractMultiplier = ReadDouble(buffer);
		result.Precision = ReadInt32(buffer);
		result.RoundLot = ReadDouble(buffer);
		result.MinTradeVolume = ReadDouble(buffer);
		result.MaxTradeVolume = ReadDouble(buffer);
		result.TradeVolumeStep = ReadDouble(buffer);
		result.ProfitCalcMode = ReadProfitCalcMode(buffer);
		result.MarginCalcMode = ReadMarginCalcMode(buffer);
		result.MarginHedge = ReadDouble(buffer);
		result.MarginFactor = ReadInt32(buffer);
		result.Color = ReadInt32(buffer);
		result.CommissionType = ReadCommissionType(buffer);
		result.CommissionChargeType = ReadCommissionChargeType(buffer);
		result.LimitsCommission = ReadDouble(buffer);
		result.Commission = ReadDouble(buffer);
		result.SwapSizeShort = ReadNullDouble(buffer);
		result.SwapSizeLong = ReadNullDouble(buffer);
		result.DefaultSlippage = ReadNullDouble(buffer);
		result.IsTradeEnabled = ReadBoolean(buffer);
		result.GroupSortOrder = ReadInt32(buffer);
		result.SortOrder = ReadInt32(buffer);
		return result;
	}
	void WriteSymbolInfo5(const CFxSymbolInfo& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.Name, buffer);
		WriteAString(arg.Currency, buffer);
		WriteAString(arg.SettlementCurrency, buffer);
		WriteDouble(arg.ContractMultiplier, buffer);
		WriteInt32(arg.Precision, buffer);
		WriteDouble(arg.RoundLot, buffer);
		WriteDouble(arg.MinTradeVolume, buffer);
		WriteDouble(arg.MaxTradeVolume, buffer);
		WriteDouble(arg.TradeVolumeStep, buffer);
		WriteProfitCalcMode(arg.ProfitCalcMode, buffer);
		WriteMarginCalcMode(arg.MarginCalcMode, buffer);
		WriteDouble(arg.MarginHedge, buffer);
		WriteInt32(arg.MarginFactor, buffer);
		WriteNullDouble(arg.MarginFactorFractional, buffer);
		WriteInt32(arg.Color, buffer);
		WriteCommissionType(arg.CommissionType, buffer);
		WriteCommissionChargeType(arg.CommissionChargeType, buffer);
		WriteDouble(arg.LimitsCommission, buffer);
		WriteDouble(arg.Commission, buffer);
		WriteNullDouble(arg.SwapSizeShort, buffer);
		WriteNullDouble(arg.SwapSizeLong, buffer);
		WriteNullDouble(arg.DefaultSlippage, buffer);
		WriteBoolean(arg.IsTradeEnabled, buffer);
		WriteInt32(arg.GroupSortOrder, buffer);
		WriteInt32(arg.SortOrder, buffer);
		WriteInt32(arg.CurrencySortOrder, buffer);
		WriteInt32(arg.SettlementCurrencySortOrder, buffer);
	}
	CFxSymbolInfo ReadSymbolInfo5(MemoryBuffer& buffer)
	{
		CFxSymbolInfo result = CFxSymbolInfo();
		result.Name = ReadAString(buffer);
		result.Currency = ReadAString(buffer);
		result.SettlementCurrency = ReadAString(buffer);
		result.ContractMultiplier = ReadDouble(buffer);
		result.Precision = ReadInt32(buffer);
		result.RoundLot = ReadDouble(buffer);
		result.MinTradeVolume = ReadDouble(buffer);
		result.MaxTradeVolume = ReadDouble(buffer);
		result.TradeVolumeStep = ReadDouble(buffer);
		result.ProfitCalcMode = ReadProfitCalcMode(buffer);
		result.MarginCalcMode = ReadMarginCalcMode(buffer);
		result.MarginHedge = ReadDouble(buffer);
		result.MarginFactor = ReadInt32(buffer);
		result.MarginFactorFractional = ReadNullDouble(buffer);
		result.Color = ReadInt32(buffer);
		result.CommissionType = ReadCommissionType(buffer);
		result.CommissionChargeType = ReadCommissionChargeType(buffer);
		result.LimitsCommission = ReadDouble(buffer);
		result.Commission = ReadDouble(buffer);
		result.SwapSizeShort = ReadNullDouble(buffer);
		result.SwapSizeLong = ReadNullDouble(buffer);
		result.DefaultSlippage = ReadNullDouble(buffer);
		result.IsTradeEnabled = ReadBoolean(buffer);
		result.GroupSortOrder = ReadInt32(buffer);
		result.SortOrder = ReadInt32(buffer);
		result.CurrencySortOrder = ReadInt32(buffer);
		result.SettlementCurrencySortOrder = ReadInt32(buffer);
		return result;
	}
	void WriteSymbolInfo6(const CFxSymbolInfo& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.Name, buffer);
		WriteAString(arg.Currency, buffer);
		WriteAString(arg.SettlementCurrency, buffer);
		WriteDouble(arg.ContractMultiplier, buffer);
		WriteInt32(arg.Precision, buffer);
		WriteDouble(arg.RoundLot, buffer);
		WriteDouble(arg.MinTradeVolume, buffer);
		WriteDouble(arg.MaxTradeVolume, buffer);
		WriteDouble(arg.TradeVolumeStep, buffer);
		WriteProfitCalcMode(arg.ProfitCalcMode, buffer);
		WriteMarginCalcMode(arg.MarginCalcMode, buffer);
		WriteDouble(arg.MarginHedge, buffer);
		WriteInt32(arg.MarginFactor, buffer);
		WriteNullDouble(arg.MarginFactorFractional, buffer);
		WriteInt32(arg.Color, buffer);
		WriteCommissionType(arg.CommissionType, buffer);
		WriteCommissionChargeType(arg.CommissionChargeType, buffer);
		WriteDouble(arg.LimitsCommission, buffer);
		WriteDouble(arg.Commission, buffer);
		WriteNullDouble(arg.SwapSizeShort, buffer);
		WriteNullDouble(arg.SwapSizeLong, buffer);
		WriteNullDouble(arg.DefaultSlippage, buffer);
		WriteBoolean(arg.IsTradeEnabled, buffer);
		WriteInt32(arg.GroupSortOrder, buffer);
		WriteInt32(arg.SortOrder, buffer);
		WriteInt32(arg.CurrencySortOrder, buffer);
		WriteInt32(arg.SettlementCurrencySortOrder, buffer);
		WriteInt32(arg.CurrencyPrecision, buffer);
		WriteInt32(arg.SettlementCurrencyPrecision, buffer);
	}
	CFxSymbolInfo ReadSymbolInfo6(MemoryBuffer& buffer)
	{
		CFxSymbolInfo result = CFxSymbolInfo();
		result.Name = ReadAString(buffer);
		result.Currency = ReadAString(buffer);
		result.SettlementCurrency = ReadAString(buffer);
		result.ContractMultiplier = ReadDouble(buffer);
		result.Precision = ReadInt32(buffer);
		result.RoundLot = ReadDouble(buffer);
		result.MinTradeVolume = ReadDouble(buffer);
		result.MaxTradeVolume = ReadDouble(buffer);
		result.TradeVolumeStep = ReadDouble(buffer);
		result.ProfitCalcMode = ReadProfitCalcMode(buffer);
		result.MarginCalcMode = ReadMarginCalcMode(buffer);
		result.MarginHedge = ReadDouble(buffer);
		result.MarginFactor = ReadInt32(buffer);
		result.MarginFactorFractional = ReadNullDouble(buffer);
		result.Color = ReadInt32(buffer);
		result.CommissionType = ReadCommissionType(buffer);
		result.CommissionChargeType = ReadCommissionChargeType(buffer);
		result.LimitsCommission = ReadDouble(buffer);
		result.Commission = ReadDouble(buffer);
		result.SwapSizeShort = ReadNullDouble(buffer);
		result.SwapSizeLong = ReadNullDouble(buffer);
		result.DefaultSlippage = ReadNullDouble(buffer);
		result.IsTradeEnabled = ReadBoolean(buffer);
		result.GroupSortOrder = ReadInt32(buffer);
		result.SortOrder = ReadInt32(buffer);
		result.CurrencySortOrder = ReadInt32(buffer);
		result.SettlementCurrencySortOrder = ReadInt32(buffer);
		result.CurrencyPrecision = ReadInt32(buffer);
		result.SettlementCurrencyPrecision = ReadInt32(buffer);
		return result;
	}
	void WriteSymbolInfo7(const CFxSymbolInfo& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.Name, buffer);
		WriteAString(arg.Currency, buffer);
		WriteAString(arg.SettlementCurrency, buffer);
		WriteDouble(arg.ContractMultiplier, buffer);
		WriteWString(arg.Description, buffer);
		WriteInt32(arg.Precision, buffer);
		WriteDouble(arg.RoundLot, buffer);
		WriteDouble(arg.MinTradeVolume, buffer);
		WriteDouble(arg.MaxTradeVolume, buffer);
		WriteDouble(arg.TradeVolumeStep, buffer);
		WriteProfitCalcMode(arg.ProfitCalcMode, buffer);
		WriteMarginCalcMode(arg.MarginCalcMode, buffer);
		WriteDouble(arg.MarginHedge, buffer);
		WriteInt32(arg.MarginFactor, buffer);
		WriteNullDouble(arg.MarginFactorFractional, buffer);
		WriteInt32(arg.Color, buffer);
		WriteCommissionType(arg.CommissionType, buffer);
		WriteCommissionChargeType(arg.CommissionChargeType, buffer);
		WriteCommissionChargeMethod(arg.CommissionChargeMethod, buffer);
		WriteDouble(arg.LimitsCommission, buffer);
		WriteDouble(arg.Commission, buffer);
		WriteNullDouble(arg.SwapSizeShort, buffer);
		WriteNullDouble(arg.SwapSizeLong, buffer);
		WriteNullDouble(arg.DefaultSlippage, buffer);
		WriteBoolean(arg.IsTradeEnabled, buffer);
		WriteInt32(arg.GroupSortOrder, buffer);
		WriteInt32(arg.SortOrder, buffer);
		WriteInt32(arg.CurrencySortOrder, buffer);
		WriteInt32(arg.SettlementCurrencySortOrder, buffer);
		WriteInt32(arg.CurrencyPrecision, buffer);
		WriteInt32(arg.SettlementCurrencyPrecision, buffer);
		WriteAString(arg.StatusGroupId, buffer);
		WriteAString(arg.SecurityName, buffer);
		WriteWString(arg.SecurityDescription, buffer);
		WriteNullDouble(arg.StopOrderMarginReduction, buffer);
		WriteNullDouble(arg.HiddenLimitOrderMarginReduction, buffer);
	}
	CFxSymbolInfo ReadSymbolInfo7(MemoryBuffer& buffer)
	{
		CFxSymbolInfo result = CFxSymbolInfo();
		result.Name = ReadAString(buffer);
		result.Currency = ReadAString(buffer);
		result.SettlementCurrency = ReadAString(buffer);
		result.ContractMultiplier = ReadDouble(buffer);
		result.Description = ReadWString(buffer);
		result.Precision = ReadInt32(buffer);
		result.RoundLot = ReadDouble(buffer);
		result.MinTradeVolume = ReadDouble(buffer);
		result.MaxTradeVolume = ReadDouble(buffer);
		result.TradeVolumeStep = ReadDouble(buffer);
		result.ProfitCalcMode = ReadProfitCalcMode(buffer);
		result.MarginCalcMode = ReadMarginCalcMode(buffer);
		result.MarginHedge = ReadDouble(buffer);
		result.MarginFactor = ReadInt32(buffer);
		result.MarginFactorFractional = ReadNullDouble(buffer);
		result.Color = ReadInt32(buffer);
		result.CommissionType = ReadCommissionType(buffer);
		result.CommissionChargeType = ReadCommissionChargeType(buffer);
		result.CommissionChargeMethod = ReadCommissionChargeMethod(buffer);
		result.LimitsCommission = ReadDouble(buffer);
		result.Commission = ReadDouble(buffer);
		result.SwapSizeShort = ReadNullDouble(buffer);
		result.SwapSizeLong = ReadNullDouble(buffer);
		result.DefaultSlippage = ReadNullDouble(buffer);
		result.IsTradeEnabled = ReadBoolean(buffer);
		result.GroupSortOrder = ReadInt32(buffer);
		result.SortOrder = ReadInt32(buffer);
		result.CurrencySortOrder = ReadInt32(buffer);
		result.SettlementCurrencySortOrder = ReadInt32(buffer);
		result.CurrencyPrecision = ReadInt32(buffer);
		result.SettlementCurrencyPrecision = ReadInt32(buffer);
		result.StatusGroupId = ReadAString(buffer);
		result.SecurityName = ReadAString(buffer);
		result.SecurityDescription = ReadWString(buffer);
		result.StopOrderMarginReduction = ReadNullDouble(buffer);
		result.HiddenLimitOrderMarginReduction = ReadNullDouble(buffer);
		return result;
	}
	void WriteAStringArray(const std::vector<std::string>& arg, MemoryBuffer& buffer)
	{
		WriteUInt32((unsigned __int32)arg.size(), buffer);
		for each(const auto element in arg)
		{
			WriteAString(element, buffer);
		}
	}
	std::vector<std::string> ReadAStringArray(MemoryBuffer& buffer)
	{
		const size_t count = buffer.ReadCount();
		std::vector<std::string> result;
		result.reserve(count);
		for(size_t index = 0; index < count; ++index)
		{
			result.push_back(ReadAString(buffer));
		}
		return result;
	}
	void WriteUInt8Array(const std::vector<unsigned __int8>& arg, MemoryBuffer& buffer)
	{
		WriteUInt32((unsigned __int32)arg.size(), buffer);
		for each(const auto element in arg)
		{
			WriteUInt8(element, buffer);
		}
	}
	std::vector<unsigned __int8> ReadUInt8Array(MemoryBuffer& buffer)
	{
		const size_t count = buffer.ReadCount();
		std::vector<unsigned __int8> result;
		result.reserve(count);
		for(size_t index = 0; index < count; ++index)
		{
			result.push_back(ReadUInt8(buffer));
		}
		return result;
	}
	void WriteSymbolInfoArray(const std::vector<CFxSymbolInfo>& arg, MemoryBuffer& buffer)
	{
		WriteUInt32((unsigned __int32)arg.size(), buffer);
		for each(const auto element in arg)
		{
			WriteSymbolInfo(element, buffer);
		}
	}
	std::vector<CFxSymbolInfo> ReadSymbolInfoArray(MemoryBuffer& buffer)
	{
		const size_t count = buffer.ReadCount();
		std::vector<CFxSymbolInfo> result;
		result.reserve(count);
		for(size_t index = 0; index < count; ++index)
		{
			result.push_back(ReadSymbolInfo(buffer));
		}
		return result;
	}
	void WriteCurrencyInfoArray(const std::vector<CFxCurrencyInfo>& arg, MemoryBuffer& buffer)
	{
		WriteUInt32((unsigned __int32)arg.size(), buffer);
		for each(const auto element in arg)
		{
			WriteCurrencyInfo(element, buffer);
		}
	}
	std::vector<CFxCurrencyInfo> ReadCurrencyInfoArray(MemoryBuffer& buffer)
	{
		const size_t count = buffer.ReadCount();
		std::vector<CFxCurrencyInfo> result;
		result.reserve(count);
		for(size_t index = 0; index < count; ++index)
		{
			result.push_back(ReadCurrencyInfo(buffer));
		}
		return result;
	}
	void WriteSymbolInfoArray2(const std::vector<CFxSymbolInfo>& arg, MemoryBuffer& buffer)
	{
		WriteUInt32((unsigned __int32)arg.size(), buffer);
		for each(const auto element in arg)
		{
			WriteSymbolInfo2(element, buffer);
		}
	}
	std::vector<CFxSymbolInfo> ReadSymbolInfoArray2(MemoryBuffer& buffer)
	{
		const size_t count = buffer.ReadCount();
		std::vector<CFxSymbolInfo> result;
		result.reserve(count);
		for(size_t index = 0; index < count; ++index)
		{
			result.push_back(ReadSymbolInfo2(buffer));
		}
		return result;
	}
	void WriteSymbolInfoArray3(const std::vector<CFxSymbolInfo>& arg, MemoryBuffer& buffer)
	{
		WriteUInt32((unsigned __int32)arg.size(), buffer);
		for each(const auto element in arg)
		{
			WriteSymbolInfo3(element, buffer);
		}
	}
	std::vector<CFxSymbolInfo> ReadSymbolInfoArray3(MemoryBuffer& buffer)
	{
		const size_t count = buffer.ReadCount();
		std::vector<CFxSymbolInfo> result;
		result.reserve(count);
		for(size_t index = 0; index < count; ++index)
		{
			result.push_back(ReadSymbolInfo3(buffer));
		}
		return result;
	}
	void WriteSymbolInfoArray4(const std::vector<CFxSymbolInfo>& arg, MemoryBuffer& buffer)
	{
		WriteUInt32((unsigned __int32)arg.size(), buffer);
		for each(const auto element in arg)
		{
			WriteSymbolInfo4(element, buffer);
		}
	}
	std::vector<CFxSymbolInfo> ReadSymbolInfoArray4(MemoryBuffer& buffer)
	{
		const size_t count = buffer.ReadCount();
		std::vector<CFxSymbolInfo> result;
		result.reserve(count);
		for(size_t index = 0; index < count; ++index)
		{
			result.push_back(ReadSymbolInfo4(buffer));
		}
		return result;
	}
	void WriteSymbolInfoArray5(const std::vector<CFxSymbolInfo>& arg, MemoryBuffer& buffer)
	{
		WriteUInt32((unsigned __int32)arg.size(), buffer);
		for each(const auto element in arg)
		{
			WriteSymbolInfo5(element, buffer);
		}
	}
	std::vector<CFxSymbolInfo> ReadSymbolInfoArray5(MemoryBuffer& buffer)
	{
		const size_t count = buffer.ReadCount();
		std::vector<CFxSymbolInfo> result;
		result.reserve(count);
		for(size_t index = 0; index < count; ++index)
		{
			result.push_back(ReadSymbolInfo5(buffer));
		}
		return result;
	}
	void WriteSymbolInfoArray6(const std::vector<CFxSymbolInfo>& arg, MemoryBuffer& buffer)
	{
		WriteUInt32((unsigned __int32)arg.size(), buffer);
		for each(const auto element in arg)
		{
			WriteSymbolInfo6(element, buffer);
		}
	}
	std::vector<CFxSymbolInfo> ReadSymbolInfoArray6(MemoryBuffer& buffer)
	{
		const size_t count = buffer.ReadCount();
		std::vector<CFxSymbolInfo> result;
		result.reserve(count);
		for(size_t index = 0; index < count; ++index)
		{
			result.push_back(ReadSymbolInfo6(buffer));
		}
		return result;
	}
	void WriteSymbolInfoArray7(const std::vector<CFxSymbolInfo>& arg, MemoryBuffer& buffer)
	{
		WriteUInt32((unsigned __int32)arg.size(), buffer);
		for each(const auto element in arg)
		{
			WriteSymbolInfo7(element, buffer);
		}
	}
	std::vector<CFxSymbolInfo> ReadSymbolInfoArray7(MemoryBuffer& buffer)
	{
		const size_t count = buffer.ReadCount();
		std::vector<CFxSymbolInfo> result;
		result.reserve(count);
		for(size_t index = 0; index < count; ++index)
		{
			result.push_back(ReadSymbolInfo7(buffer));
		}
		return result;
	}
	void WriteQuoteEntry(const CFxQuoteEntry& arg, MemoryBuffer& buffer)
	{
		WriteDouble(arg.Price, buffer);
		WriteDouble(arg.Volume, buffer);
	}
	CFxQuoteEntry ReadQuoteEntry(MemoryBuffer& buffer)
	{
		CFxQuoteEntry result = CFxQuoteEntry();
		result.Price = ReadDouble(buffer);
		result.Volume = ReadDouble(buffer);
		return result;
	}
	void WriteQuoteEntryArray(const std::vector<CFxQuoteEntry>& arg, MemoryBuffer& buffer)
	{
		WriteUInt32((unsigned __int32)arg.size(), buffer);
		for each(const auto element in arg)
		{
			WriteQuoteEntry(element, buffer);
		}
	}
	std::vector<CFxQuoteEntry> ReadQuoteEntryArray(MemoryBuffer& buffer)
	{
		const size_t count = buffer.ReadCount();
		std::vector<CFxQuoteEntry> result;
		result.reserve(count);
		for(size_t index = 0; index < count; ++index)
		{
			result.push_back(ReadQuoteEntry(buffer));
		}
		return result;
	}
	void WriteQuote(const CFxQuote& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.Symbol, buffer);
		WriteTime(arg.CreatingTime, buffer);
		WriteQuoteEntryArray(arg.Bids, buffer);
		WriteQuoteEntryArray(arg.Asks, buffer);
		WriteAString(arg.Id, buffer);
	}
	CFxQuote ReadQuote(MemoryBuffer& buffer)
	{
		CFxQuote result = CFxQuote();
		result.Symbol = ReadAString(buffer);
		result.CreatingTime = ReadTime(buffer);
		result.Bids = ReadQuoteEntryArray(buffer);
		result.Asks = ReadQuoteEntryArray(buffer);
		result.Id = ReadAString(buffer);
		return result;
	}
	void WriteQuoteArray(const std::vector<CFxQuote>& arg, MemoryBuffer& buffer)
	{
		WriteUInt32((unsigned __int32)arg.size(), buffer);
		for each(const auto element in arg)
		{
			WriteQuote(element, buffer);
		}
	}
	std::vector<CFxQuote> ReadQuoteArray(MemoryBuffer& buffer)
	{
		const size_t count = buffer.ReadCount();
		std::vector<CFxQuote> result;
		result.reserve(count);
		for(size_t index = 0; index < count; ++index)
		{
			result.push_back(ReadQuote(buffer));
		}
		return result;
	}
	void WriteBar(const CFxBar& arg, MemoryBuffer& buffer)
	{
		WriteDouble(arg.Open, buffer);
		WriteDouble(arg.Close, buffer);
		WriteDouble(arg.High, buffer);
		WriteDouble(arg.Low, buffer);
		WriteDouble(arg.Volume, buffer);
		WriteTime(arg.From, buffer);
	}
	CFxBar ReadBar(MemoryBuffer& buffer)
	{
		CFxBar result = CFxBar();
		result.Open = ReadDouble(buffer);
		result.Close = ReadDouble(buffer);
		result.High = ReadDouble(buffer);
		result.Low = ReadDouble(buffer);
		result.Volume = ReadDouble(buffer);
		result.From = ReadTime(buffer);
		return result;
	}
	void WriteBarArray(const std::vector<CFxBar>& arg, MemoryBuffer& buffer)
	{
		WriteUInt32((unsigned __int32)arg.size(), buffer);
		for each(const auto element in arg)
		{
			WriteBar(element, buffer);
		}
	}
	std::vector<CFxBar> ReadBarArray(MemoryBuffer& buffer)
	{
		const size_t count = buffer.ReadCount();
		std::vector<CFxBar> result;
		result.reserve(count);
		for(size_t index = 0; index < count; ++index)
		{
			result.push_back(ReadBar(buffer));
		}
		return result;
	}
	void WriteDataHistoryResponse(const CFxDataHistoryResponse& arg, MemoryBuffer& buffer)
	{
		WriteTime(arg.FromAll, buffer);
		WriteTime(arg.ToAll, buffer);
		WriteTime(arg.From, buffer);
		WriteTime(arg.To, buffer);
		WriteAString(arg.LastTickId, buffer);
		WriteBarArray(arg.Bars, buffer);
		WriteAStringArray(arg.Files, buffer);
	}
	CFxDataHistoryResponse ReadDataHistoryResponse(MemoryBuffer& buffer)
	{
		CFxDataHistoryResponse result = CFxDataHistoryResponse();
		result.FromAll = ReadTime(buffer);
		result.ToAll = ReadTime(buffer);
		result.From = ReadTime(buffer);
		result.To = ReadTime(buffer);
		result.LastTickId = ReadAString(buffer);
		result.Bars = ReadBarArray(buffer);
		result.Files = ReadAStringArray(buffer);
		return result;
	}
	void WriteFileChunk(const CFxFileChunk& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.FileId, buffer);
		WriteAString(arg.FileName, buffer);
		WriteInt32(arg.FileSize, buffer);
		WriteInt32(arg.ChunkId, buffer);
		WriteInt32(arg.TotalChunks, buffer);
		WriteUInt8Array(arg.Data, buffer);
	}
	CFxFileChunk ReadFileChunk(MemoryBuffer& buffer)
	{
		CFxFileChunk result = CFxFileChunk();
		result.FileId = ReadAString(buffer);
		result.FileName = ReadAString(buffer);
		result.FileSize = ReadInt32(buffer);
		result.ChunkId = ReadInt32(buffer);
		result.TotalChunks = ReadInt32(buffer);
		result.Data = ReadUInt8Array(buffer);
		return result;
	}
	void WriteNotification(const CNotification& arg, MemoryBuffer& buffer)
	{
		WriteSeverity(arg.Severity, buffer);
		WriteNotificationType(arg.Type, buffer);
		WriteAString(arg.Text, buffer);
		WriteDouble(arg.Balance, buffer);
		WriteDouble(arg.TransactionAmount, buffer);
		WriteAString(arg.TransactionCurrency, buffer);
	}
	CNotification ReadNotification(MemoryBuffer& buffer)
	{
		CNotification result = CNotification();
		result.Severity = ReadSeverity(buffer);
		result.Type = ReadNotificationType(buffer);
		result.Text = ReadAString(buffer);
		result.Balance = ReadDouble(buffer);
		result.TransactionAmount = ReadDouble(buffer);
		result.TransactionCurrency = ReadAString(buffer);
		return result;
	}
	void Throw(HRESULT status, MemoryBuffer& buffer)
	{
		if(status >= 0)
		{
			return;
		}
		if(LRP_EXCEPTION != status)
		{
			throw std::exception("Unexpected exception has been encountered");
		}
		const int _id = ReadInt32(buffer);
		_id;
		string _message = ReadAString(buffer);
		throw std::exception(_message.c_str());
	}
}
