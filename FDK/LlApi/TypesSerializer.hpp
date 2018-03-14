// This is always generated file. Do not change anything.

namespace
{
	void WriteArgumentNullException(const CArgumentNullException& arg, MemoryBuffer& buffer);
	CArgumentNullException ReadArgumentNullException(MemoryBuffer& buffer);
	void WriteArgumentException(const CArgumentException& arg, MemoryBuffer& buffer);
	CArgumentException ReadArgumentException(MemoryBuffer& buffer);
	void WriteInvalidHandleException(const CInvalidHandleException& arg, MemoryBuffer& buffer);
	CInvalidHandleException ReadInvalidHandleException(MemoryBuffer& buffer);
	void WriteRejectException(const CRejectException& arg, MemoryBuffer& buffer);
	CRejectException ReadRejectException(MemoryBuffer& buffer);
	void WriteTimeoutException(const CTimeoutException& arg, MemoryBuffer& buffer);
	CTimeoutException ReadTimeoutException(MemoryBuffer& buffer);
	void WriteSendException(const CSendException& arg, MemoryBuffer& buffer);
	CSendException ReadSendException(MemoryBuffer& buffer);
	void WriteLogoutException(const CLogoutException& arg, MemoryBuffer& buffer);
	CLogoutException ReadLogoutException(MemoryBuffer& buffer);
	void WriteUnsupportedFeatureException(const CUnsupportedFeatureException& arg, MemoryBuffer& buffer);
	CUnsupportedFeatureException ReadUnsupportedFeatureException(MemoryBuffer& buffer);
	void WriteRuntimeException(const std::runtime_error& arg, MemoryBuffer& buffer);
	std::runtime_error ReadRuntimeException(MemoryBuffer& buffer);
	void WriteAccountType(const FxAccountType& arg, MemoryBuffer& buffer);
	FxAccountType ReadAccountType(MemoryBuffer& buffer);
	void WriteSeverity(const Severity& arg, MemoryBuffer& buffer);
	Severity ReadSeverity(MemoryBuffer& buffer);
	void WriteNotificationType(const NotificationType& arg, MemoryBuffer& buffer);
	NotificationType ReadNotificationType(MemoryBuffer& buffer);
	void WriteProfitCalcMode(const ProfitCalcMode& arg, MemoryBuffer& buffer);
	ProfitCalcMode ReadProfitCalcMode(MemoryBuffer& buffer);
	void WriteMarginCalcMode(const MarginCalcMode& arg, MemoryBuffer& buffer);
	MarginCalcMode ReadMarginCalcMode(MemoryBuffer& buffer);
	void WriteSessionStatus(const SessionStatus& arg, MemoryBuffer& buffer);
	SessionStatus ReadSessionStatus(MemoryBuffer& buffer);
	void WriteSide(const FxTradeRecordSide& arg, MemoryBuffer& buffer);
	FxTradeRecordSide ReadSide(MemoryBuffer& buffer);
	void WritePriceType(const FxPriceType& arg, MemoryBuffer& buffer);
	FxPriceType ReadPriceType(MemoryBuffer& buffer);
	void WriteTradeRecordSide(const FxTradeRecordSide& arg, MemoryBuffer& buffer);
	FxTradeRecordSide ReadTradeRecordSide(MemoryBuffer& buffer);
	void WriteTradeRecordType(const FxOrderType& arg, MemoryBuffer& buffer);
	FxOrderType ReadTradeRecordType(MemoryBuffer& buffer);
	void WriteFxOrderType(const FxTradeRecordType& arg, MemoryBuffer& buffer);
	FxTradeRecordType ReadFxOrderType(MemoryBuffer& buffer);
	void WriteOrderType(const FxOrderType& arg, MemoryBuffer& buffer);
	FxOrderType ReadOrderType(MemoryBuffer& buffer);
	void WriteLogoutReason(const FxLogoutReason& arg, MemoryBuffer& buffer);
	FxLogoutReason ReadLogoutReason(MemoryBuffer& buffer);
	void WriteTwoFactorReason(const FxTwoFactorReason& arg, MemoryBuffer& buffer);
	FxTwoFactorReason ReadTwoFactorReason(MemoryBuffer& buffer);
	void WriteOrderStatus(const FxOrderStatus& arg, MemoryBuffer& buffer);
	FxOrderStatus ReadOrderStatus(MemoryBuffer& buffer);
	void WriteExecutionType(const FxExecutionType& arg, MemoryBuffer& buffer);
	FxExecutionType ReadExecutionType(MemoryBuffer& buffer);
	void WriteRejectReason(const FxRejectReason& arg, MemoryBuffer& buffer);
	FxRejectReason ReadRejectReason(MemoryBuffer& buffer);
	void WriteCommissionType(const FxCommissionType& arg, MemoryBuffer& buffer);
	FxCommissionType ReadCommissionType(MemoryBuffer& buffer);
	void WriteCommissionChargeType(const FxCommissionChargeType& arg, MemoryBuffer& buffer);
	FxCommissionChargeType ReadCommissionChargeType(MemoryBuffer& buffer);
	void WriteCommissionChargeMethod(const FxCommissionChargeMethod& arg, MemoryBuffer& buffer);
	FxCommissionChargeMethod ReadCommissionChargeMethod(MemoryBuffer& buffer);
	void WriteSwapType(const SwapType& arg, MemoryBuffer& buffer);
	SwapType ReadSwapType(MemoryBuffer& buffer);
	void WriteNotification(const CNotification& arg, MemoryBuffer& buffer);
	CNotification ReadNotification(MemoryBuffer& buffer);
	void WriteDataHistoryInfo(const CDataHistoryInfo& arg, MemoryBuffer& buffer);
	CDataHistoryInfo ReadDataHistoryInfo(MemoryBuffer& buffer);
	void WriteSymbolInfo(const CFxSymbolInfo& arg, MemoryBuffer& buffer);
	CFxSymbolInfo ReadSymbolInfo(MemoryBuffer& buffer);
	void WriteTwoFactorAuth(const CFxTwoFactorAuth& arg, MemoryBuffer& buffer);
	CFxTwoFactorAuth ReadTwoFactorAuth(MemoryBuffer& buffer);
	void WriteStatusGroupInfo(const CFxStatusGroupInfo& arg, MemoryBuffer& buffer);
	CFxStatusGroupInfo ReadStatusGroupInfo(MemoryBuffer& buffer);
	void WriteStatusGroupInfoArray(const std::vector<CFxStatusGroupInfo>& arg, MemoryBuffer& buffer);
	std::vector<CFxStatusGroupInfo> ReadStatusGroupInfoArray(MemoryBuffer& buffer);
	void WriteSessionInfo(const CFxSessionInfo& arg, MemoryBuffer& buffer);
	CFxSessionInfo ReadSessionInfo(MemoryBuffer& buffer);
	void WriteSymbolInfoArray(const std::vector<CFxSymbolInfo>& arg, MemoryBuffer& buffer);
	std::vector<CFxSymbolInfo> ReadSymbolInfoArray(MemoryBuffer& buffer);
	void WriteStringArray(const std::vector<std::string>& arg, MemoryBuffer& buffer);
	std::vector<std::string> ReadStringArray(MemoryBuffer& buffer);
	void WriteByteArray(const std::vector<unsigned __int8>& arg, MemoryBuffer& buffer);
	std::vector<unsigned __int8> ReadByteArray(MemoryBuffer& buffer);
	void WriteAssetInfo(const CAssetInfo& arg, MemoryBuffer& buffer);
	CAssetInfo ReadAssetInfo(MemoryBuffer& buffer);
	void WriteAssetInfoArray(const std::vector<CAssetInfo>& arg, MemoryBuffer& buffer);
	std::vector<CAssetInfo> ReadAssetInfoArray(MemoryBuffer& buffer);
	void WriteTradeServerInfo(const CFxTradeServerInfo& arg, MemoryBuffer& buffer);
	CFxTradeServerInfo ReadTradeServerInfo(MemoryBuffer& buffer);
	void WriteAccountInfo(const CFxAccountInfo& arg, MemoryBuffer& buffer);
	CFxAccountInfo ReadAccountInfo(MemoryBuffer& buffer);
	void WriteFileChunk(const CFxFileChunk& arg, MemoryBuffer& buffer);
	CFxFileChunk ReadFileChunk(MemoryBuffer& buffer);
	void WriteFxOrder(const CFxOrder& arg, MemoryBuffer& buffer);
	CFxOrder ReadFxOrder(MemoryBuffer& buffer);
	void WriteFxOrderArray(const std::vector<CFxOrder>& arg, MemoryBuffer& buffer);
	std::vector<CFxOrder> ReadFxOrderArray(MemoryBuffer& buffer);
	void WriteBar(const CFxBar& arg, MemoryBuffer& buffer);
	CFxBar ReadBar(MemoryBuffer& buffer);
	void WriteQuoteEntry(const CFxQuoteEntry& arg, MemoryBuffer& buffer);
	CFxQuoteEntry ReadQuoteEntry(MemoryBuffer& buffer);
	void WriteQuoteEntryArray(const std::vector<CFxQuoteEntry>& arg, MemoryBuffer& buffer);
	std::vector<CFxQuoteEntry> ReadQuoteEntryArray(MemoryBuffer& buffer);
	void WriteQuote(const CFxQuote& arg, MemoryBuffer& buffer);
	CFxQuote ReadQuote(MemoryBuffer& buffer);
	void WriteQuoteArray(const std::vector<CFxQuote>& arg, MemoryBuffer& buffer);
	std::vector<CFxQuote> ReadQuoteArray(MemoryBuffer& buffer);
	void WriteBarArray(const std::vector<CFxBar>& arg, MemoryBuffer& buffer);
	std::vector<CFxBar> ReadBarArray(MemoryBuffer& buffer);
	void WriteMessage(const CFxMessage& arg, MemoryBuffer& buffer);
	CFxMessage ReadMessage(MemoryBuffer& buffer);
	void WritePosition(const CFxPositionReport& arg, MemoryBuffer& buffer);
	CFxPositionReport ReadPosition(MemoryBuffer& buffer);
	void WritePositionArray(const std::vector<CFxPositionReport>& arg, MemoryBuffer& buffer);
	std::vector<CFxPositionReport> ReadPositionArray(MemoryBuffer& buffer);
	void WriteTradeTransactionReportType(const FxTradeTransactionReportType& arg, MemoryBuffer& buffer);
	FxTradeTransactionReportType ReadTradeTransactionReportType(MemoryBuffer& buffer);
	void WriteTradeTransactionReason(const FxTradeTransactionReason& arg, MemoryBuffer& buffer);
	FxTradeTransactionReason ReadTradeTransactionReason(MemoryBuffer& buffer);
	void WriteTradeTransactionReport(const CFxTradeTransactionReport& arg, MemoryBuffer& buffer);
	CFxTradeTransactionReport ReadTradeTransactionReport(MemoryBuffer& buffer);
	void WriteDailyAccountSnapshotReport(const CFxDailyAccountSnapshotReport& arg, MemoryBuffer& buffer);
	CFxDailyAccountSnapshotReport ReadDailyAccountSnapshotReport(MemoryBuffer& buffer);
	void WriteClosePositionResult(const CFxClosePositionResult& arg, MemoryBuffer& buffer);
	CFxClosePositionResult ReadClosePositionResult(MemoryBuffer& buffer);
	void WriteExecutionReport(const CFxExecutionReport& arg, MemoryBuffer& buffer);
	CFxExecutionReport ReadExecutionReport(MemoryBuffer& buffer);
	void WriteCurrencyInfo(const CFxCurrencyInfo& arg, MemoryBuffer& buffer);
	CFxCurrencyInfo ReadCurrencyInfo(MemoryBuffer& buffer);
	void WriteCurrencyInfoArray(const std::vector<CFxCurrencyInfo>& arg, MemoryBuffer& buffer);
	std::vector<CFxCurrencyInfo> ReadCurrencyInfoArray(MemoryBuffer& buffer);
}

namespace
{
	void WriteArgumentNullException(const CArgumentNullException& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.what(), buffer);
	}
	CArgumentNullException ReadArgumentNullException(MemoryBuffer& buffer)
	{
		auto _message = ReadAString(buffer);
		CArgumentNullException result(_message);
		return result;
	}
	void WriteArgumentException(const CArgumentException& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.what(), buffer);
	}
	CArgumentException ReadArgumentException(MemoryBuffer& buffer)
	{
		auto _message = ReadAString(buffer);
		CArgumentException result(_message);
		return result;
	}
	void WriteInvalidHandleException(const CInvalidHandleException& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.what(), buffer);
	}
	CInvalidHandleException ReadInvalidHandleException(MemoryBuffer& buffer)
	{
		auto _message = ReadAString(buffer);
		CInvalidHandleException result(_message);
		return result;
	}
	void WriteRejectException(const CRejectException& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.what(), buffer);
		WriteInt32(arg.Code, buffer);
	}
	CRejectException ReadRejectException(MemoryBuffer& buffer)
	{
		auto _message = ReadAString(buffer);
		CRejectException result(_message);
		result.Code = ReadInt32(buffer);
		return result;
	}
	void WriteTimeoutException(const CTimeoutException& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.what(), buffer);
		WriteInt32(arg.WaitingInterval, buffer);
		WriteAString(arg.OperationId, buffer);
	}
	CTimeoutException ReadTimeoutException(MemoryBuffer& buffer)
	{
		auto _message = ReadAString(buffer);
		CTimeoutException result(_message);
		result.WaitingInterval = ReadInt32(buffer);
		result.OperationId = ReadAString(buffer);
		return result;
	}
	void WriteSendException(const CSendException& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.what(), buffer);
	}
	CSendException ReadSendException(MemoryBuffer& buffer)
	{
		auto _message = ReadAString(buffer);
		CSendException result(_message);
		return result;
	}
	void WriteLogoutException(const CLogoutException& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.what(), buffer);
	}
	CLogoutException ReadLogoutException(MemoryBuffer& buffer)
	{
		auto _message = ReadAString(buffer);
		CLogoutException result(_message);
		return result;
	}
	void WriteUnsupportedFeatureException(const CUnsupportedFeatureException& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.what(), buffer);
		WriteAString(arg.Feature, buffer);
	}
	CUnsupportedFeatureException ReadUnsupportedFeatureException(MemoryBuffer& buffer)
	{
		auto _message = ReadAString(buffer);
		CUnsupportedFeatureException result(_message);
		result.Feature = ReadAString(buffer);
		return result;
	}
	void WriteRuntimeException(const std::runtime_error& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.what(), buffer);
	}
	std::runtime_error ReadRuntimeException(MemoryBuffer& buffer)
	{
		auto _message = ReadAString(buffer);
		std::runtime_error result(_message);
		return result;
	}
	void WriteAccountType(const FxAccountType& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	FxAccountType ReadAccountType(MemoryBuffer& buffer)
	{
		auto result = (FxAccountType)ReadInt32(buffer);
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
	void WriteNotificationType(const NotificationType& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	NotificationType ReadNotificationType(MemoryBuffer& buffer)
	{
		auto result = (NotificationType)ReadInt32(buffer);
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
	void WriteSessionStatus(const SessionStatus& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	SessionStatus ReadSessionStatus(MemoryBuffer& buffer)
	{
		auto result = (SessionStatus)ReadInt32(buffer);
		return result;
	}
	void WriteSide(const FxTradeRecordSide& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	FxTradeRecordSide ReadSide(MemoryBuffer& buffer)
	{
		auto result = (FxTradeRecordSide)ReadInt32(buffer);
		return result;
	}
	void WritePriceType(const FxPriceType& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	FxPriceType ReadPriceType(MemoryBuffer& buffer)
	{
		auto result = (FxPriceType)ReadInt32(buffer);
		return result;
	}
	void WriteTradeRecordSide(const FxTradeRecordSide& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	FxTradeRecordSide ReadTradeRecordSide(MemoryBuffer& buffer)
	{
		auto result = (FxTradeRecordSide)ReadInt32(buffer);
		return result;
	}
	void WriteTradeRecordType(const FxOrderType& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	FxOrderType ReadTradeRecordType(MemoryBuffer& buffer)
	{
		auto result = (FxOrderType)ReadInt32(buffer);
		return result;
	}
	void WriteFxOrderType(const FxTradeRecordType& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	FxTradeRecordType ReadFxOrderType(MemoryBuffer& buffer)
	{
		auto result = (FxTradeRecordType)ReadInt32(buffer);
		return result;
	}
	void WriteOrderType(const FxOrderType& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	FxOrderType ReadOrderType(MemoryBuffer& buffer)
	{
		auto result = (FxOrderType)ReadInt32(buffer);
		return result;
	}
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
	void WriteOrderStatus(const FxOrderStatus& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	FxOrderStatus ReadOrderStatus(MemoryBuffer& buffer)
	{
		auto result = (FxOrderStatus)ReadInt32(buffer);
		return result;
	}
	void WriteExecutionType(const FxExecutionType& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	FxExecutionType ReadExecutionType(MemoryBuffer& buffer)
	{
		auto result = (FxExecutionType)ReadInt32(buffer);
		return result;
	}
	void WriteRejectReason(const FxRejectReason& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	FxRejectReason ReadRejectReason(MemoryBuffer& buffer)
	{
		auto result = (FxRejectReason)ReadInt32(buffer);
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
	void WriteSwapType(const SwapType& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	SwapType ReadSwapType(MemoryBuffer& buffer)
	{
		auto result = (SwapType)ReadInt32(buffer);
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
	void WriteDataHistoryInfo(const CDataHistoryInfo& arg, MemoryBuffer& buffer)
	{
		WriteTime(arg.FromAll, buffer);
		WriteTime(arg.ToAll, buffer);
		WriteNullTime(arg.From, buffer);
		WriteNullTime(arg.To, buffer);
		WriteAString(arg.LastTickId, buffer);
		WriteStringArray(arg.Files, buffer);
		WriteBarArray(arg.Bars, buffer);
	}
	CDataHistoryInfo ReadDataHistoryInfo(MemoryBuffer& buffer)
	{
		CDataHistoryInfo result = CDataHistoryInfo();
		result.FromAll = ReadTime(buffer);
		result.ToAll = ReadTime(buffer);
		result.From = ReadNullTime(buffer);
		result.To = ReadNullTime(buffer);
		result.LastTickId = ReadAString(buffer);
		result.Files = ReadStringArray(buffer);
		result.Bars = ReadBarArray(buffer);
		return result;
	}
	void WriteSymbolInfo(const CFxSymbolInfo& arg, MemoryBuffer& buffer)
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
		WriteDouble(arg.MinCommission, buffer);
		WriteWString(arg.MinCommissionCurrency, buffer);
		WriteSwapType(arg.SwapType, buffer);
		WriteInt32(arg.TripleSwapDay, buffer);
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
	CFxSymbolInfo ReadSymbolInfo(MemoryBuffer& buffer)
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
		result.MinCommission = ReadDouble(buffer);
		result.MinCommissionCurrency = ReadWString(buffer);
		result.SwapType = ReadSwapType(buffer);
		result.TripleSwapDay = ReadInt32(buffer);
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
	void WriteTwoFactorAuth(const CFxTwoFactorAuth& arg, MemoryBuffer& buffer)
	{
		WriteTwoFactorReason(arg.Reason, buffer);
		WriteAString(arg.Text, buffer);
		WriteTime(arg.Expire, buffer);
	}
	CFxTwoFactorAuth ReadTwoFactorAuth(MemoryBuffer& buffer)
	{
		CFxTwoFactorAuth result = CFxTwoFactorAuth();
		result.Reason = ReadTwoFactorReason(buffer);
		result.Text = ReadAString(buffer);
		result.Expire = ReadTime(buffer);
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
	void WriteSessionInfo(const CFxSessionInfo& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.TradingSessionId, buffer);
		WriteSessionStatus(arg.Status, buffer);
		WriteInt32(arg.ServerTimeZoneOffset, buffer);
		WriteTime(arg.StartTime, buffer);
		WriteTime(arg.OpenTime, buffer);
		WriteTime(arg.CloseTime, buffer);
		WriteTime(arg.EndTime, buffer);
		WriteAString(arg.PlatformName, buffer);
		WriteAString(arg.PlatformCompany, buffer);
		WriteStatusGroupInfoArray(arg.StatusGroups, buffer);
	}
	CFxSessionInfo ReadSessionInfo(MemoryBuffer& buffer)
	{
		CFxSessionInfo result = CFxSessionInfo();
		result.TradingSessionId = ReadAString(buffer);
		result.Status = ReadSessionStatus(buffer);
		result.ServerTimeZoneOffset = ReadInt32(buffer);
		result.StartTime = ReadTime(buffer);
		result.OpenTime = ReadTime(buffer);
		result.CloseTime = ReadTime(buffer);
		result.EndTime = ReadTime(buffer);
		result.PlatformName = ReadAString(buffer);
		result.PlatformCompany = ReadAString(buffer);
		result.StatusGroups = ReadStatusGroupInfoArray(buffer);
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
	void WriteStringArray(const std::vector<std::string>& arg, MemoryBuffer& buffer)
	{
		WriteUInt32((unsigned __int32)arg.size(), buffer);
		for each(const auto element in arg)
		{
			WriteAString(element, buffer);
		}
	}
	std::vector<std::string> ReadStringArray(MemoryBuffer& buffer)
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
	void WriteByteArray(const std::vector<unsigned __int8>& arg, MemoryBuffer& buffer)
	{
		WriteUInt32((unsigned __int32)arg.size(), buffer);
		for each(const auto element in arg)
		{
			WriteUInt8(element, buffer);
		}
	}
	std::vector<unsigned __int8> ReadByteArray(MemoryBuffer& buffer)
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
	void WriteAssetInfo(const CAssetInfo& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.Currency, buffer);
		WriteDouble(arg.Balance, buffer);
		WriteDouble(arg.LockedAmount, buffer);
		WriteDouble(arg.TradeAmount, buffer);
	}
	CAssetInfo ReadAssetInfo(MemoryBuffer& buffer)
	{
		CAssetInfo result = CAssetInfo();
		result.Currency = ReadAString(buffer);
		result.Balance = ReadDouble(buffer);
		result.LockedAmount = ReadDouble(buffer);
		result.TradeAmount = ReadDouble(buffer);
		return result;
	}
	void WriteAssetInfoArray(const std::vector<CAssetInfo>& arg, MemoryBuffer& buffer)
	{
		WriteUInt32((unsigned __int32)arg.size(), buffer);
		for each(const auto element in arg)
		{
			WriteAssetInfo(element, buffer);
		}
	}
	std::vector<CAssetInfo> ReadAssetInfoArray(MemoryBuffer& buffer)
	{
		const size_t count = buffer.ReadCount();
		std::vector<CAssetInfo> result;
		result.reserve(count);
		for(size_t index = 0; index < count; ++index)
		{
			result.push_back(ReadAssetInfo(buffer));
		}
		return result;
	}
	void WriteTradeServerInfo(const CFxTradeServerInfo& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.CompanyName, buffer);
		WriteAString(arg.CompanyFullName, buffer);
		WriteWString(arg.CompanyDescription, buffer);
		WriteAString(arg.CompanyAddress, buffer);
		WriteAString(arg.CompanyEmail, buffer);
		WriteAString(arg.CompanyPhone, buffer);
		WriteAString(arg.CompanyWebSite, buffer);
		WriteAString(arg.ServerName, buffer);
		WriteAString(arg.ServerFullName, buffer);
		WriteWString(arg.ServerDescription, buffer);
		WriteAString(arg.ServerAddress, buffer);
		WriteNullInt32(arg.ServerFixFeedSslPort, buffer);
		WriteNullInt32(arg.ServerFixTradeSslPort, buffer);
		WriteNullInt32(arg.ServerWebSocketFeedPort, buffer);
		WriteNullInt32(arg.ServerWebSocketTradePort, buffer);
		WriteNullInt32(arg.ServerRestPort, buffer);
	}
	CFxTradeServerInfo ReadTradeServerInfo(MemoryBuffer& buffer)
	{
		CFxTradeServerInfo result = CFxTradeServerInfo();
		result.CompanyName = ReadAString(buffer);
		result.CompanyFullName = ReadAString(buffer);
		result.CompanyDescription = ReadWString(buffer);
		result.CompanyAddress = ReadAString(buffer);
		result.CompanyEmail = ReadAString(buffer);
		result.CompanyPhone = ReadAString(buffer);
		result.CompanyWebSite = ReadAString(buffer);
		result.ServerName = ReadAString(buffer);
		result.ServerFullName = ReadAString(buffer);
		result.ServerDescription = ReadWString(buffer);
		result.ServerAddress = ReadAString(buffer);
		result.ServerFixFeedSslPort = ReadNullInt32(buffer);
		result.ServerFixTradeSslPort = ReadNullInt32(buffer);
		result.ServerWebSocketFeedPort = ReadNullInt32(buffer);
		result.ServerWebSocketTradePort = ReadNullInt32(buffer);
		result.ServerRestPort = ReadNullInt32(buffer);
		return result;
	}
	void WriteAccountInfo(const CFxAccountInfo& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.AccountId, buffer);
		WriteAccountType(arg.Type, buffer);
		WriteAString(arg.Name, buffer);
		WriteAString(arg.Email, buffer);
		WriteWString(arg.Comment, buffer);
		WriteAString(arg.Currency, buffer);
		WriteNullTime(arg.RegistredDate, buffer);
		WriteNullTime(arg.ModifiedTime, buffer);
		WriteInt32(arg.Leverage, buffer);
		WriteDouble(arg.Balance, buffer);
		WriteDouble(arg.Margin, buffer);
		WriteDouble(arg.Equity, buffer);
		WriteDouble(arg.MarginCallLevel, buffer);
		WriteDouble(arg.StopOutLevel, buffer);
		WriteBoolean(arg.IsValid, buffer);
		WriteBoolean(arg.IsReadOnly, buffer);
		WriteBoolean(arg.IsBlocked, buffer);
		WriteAssetInfoArray(arg.Assets, buffer);
	}
	CFxAccountInfo ReadAccountInfo(MemoryBuffer& buffer)
	{
		CFxAccountInfo result = CFxAccountInfo();
		result.AccountId = ReadAString(buffer);
		result.Type = ReadAccountType(buffer);
		result.Name = ReadAString(buffer);
		result.Email = ReadAString(buffer);
		result.Comment = ReadWString(buffer);
		result.Currency = ReadAString(buffer);
		result.RegistredDate = ReadNullTime(buffer);
		result.ModifiedTime = ReadNullTime(buffer);
		result.Leverage = ReadInt32(buffer);
		result.Balance = ReadDouble(buffer);
		result.Margin = ReadDouble(buffer);
		result.Equity = ReadDouble(buffer);
		result.MarginCallLevel = ReadDouble(buffer);
		result.StopOutLevel = ReadDouble(buffer);
		result.IsValid = ReadBoolean(buffer);
		result.IsReadOnly = ReadBoolean(buffer);
		result.IsBlocked = ReadBoolean(buffer);
		result.Assets = ReadAssetInfoArray(buffer);
		return result;
	}
	void WriteFileChunk(const CFxFileChunk& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.FileId, buffer);
		WriteAString(arg.FileName, buffer);
		WriteInt32(arg.FileSize, buffer);
		WriteInt32(arg.ChunkId, buffer);
		WriteInt32(arg.TotalChunks, buffer);
		WriteByteArray(arg.Data, buffer);
	}
	CFxFileChunk ReadFileChunk(MemoryBuffer& buffer)
	{
		CFxFileChunk result = CFxFileChunk();
		result.FileId = ReadAString(buffer);
		result.FileName = ReadAString(buffer);
		result.FileSize = ReadInt32(buffer);
		result.ChunkId = ReadInt32(buffer);
		result.TotalChunks = ReadInt32(buffer);
		result.Data = ReadByteArray(buffer);
		return result;
	}
	void WriteFxOrder(const CFxOrder& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.OrderId, buffer);
		WriteAString(arg.ClientOrderId, buffer);
		WriteAString(arg.Symbol, buffer);
		WriteDouble(arg.InitialVolume, buffer);
		WriteNullDouble(arg.Volume, buffer);
		WriteNullDouble(arg.MaxVisibleVolume, buffer);
		WriteNullDouble(arg.Price, buffer);
		WriteNullDouble(arg.StopPrice, buffer);
		WriteNullDouble(arg.TakeProfit, buffer);
		WriteNullDouble(arg.StopLoss, buffer);
		WriteDouble(arg.Commission, buffer);
		WriteDouble(arg.AgentCommission, buffer);
		WriteDouble(arg.Swap, buffer);
		WriteNullDouble(arg.Profit, buffer);
		WriteFxOrderType(arg.InitialType, buffer);
		WriteFxOrderType(arg.Type, buffer);
		WriteTradeRecordSide(arg.Side, buffer);
		WriteNullTime(arg.Expiration, buffer);
		WriteNullTime(arg.Created, buffer);
		WriteNullTime(arg.Modified, buffer);
		WriteWString(arg.Comment, buffer);
		WriteWString(arg.Tag, buffer);
		WriteNullInt32(arg.Magic, buffer);
		WriteBoolean(arg.IsReducedOpenCommission, buffer);
		WriteBoolean(arg.IsReducedCloseCommission, buffer);
		WriteBoolean(arg.ImmediateOrCancel, buffer);
		WriteBoolean(arg.MarketWithSlippage, buffer);
		WriteNullBoolean(arg.IOCOverride, buffer);
		WriteNullBoolean(arg.IFMOverride, buffer);
		WriteNullDouble(arg.PrevVolume, buffer);
	}
	CFxOrder ReadFxOrder(MemoryBuffer& buffer)
	{
		CFxOrder result = CFxOrder();
		result.OrderId = ReadAString(buffer);
		result.ClientOrderId = ReadAString(buffer);
		result.Symbol = ReadAString(buffer);
		result.InitialVolume = ReadDouble(buffer);
		result.Volume = ReadNullDouble(buffer);
		result.MaxVisibleVolume = ReadNullDouble(buffer);
		result.Price = ReadNullDouble(buffer);
		result.StopPrice = ReadNullDouble(buffer);
		result.TakeProfit = ReadNullDouble(buffer);
		result.StopLoss = ReadNullDouble(buffer);
		result.Commission = ReadDouble(buffer);
		result.AgentCommission = ReadDouble(buffer);
		result.Swap = ReadDouble(buffer);
		result.Profit = ReadNullDouble(buffer);
		result.InitialType = ReadFxOrderType(buffer);
		result.Type = ReadFxOrderType(buffer);
		result.Side = ReadTradeRecordSide(buffer);
		result.Expiration = ReadNullTime(buffer);
		result.Created = ReadNullTime(buffer);
		result.Modified = ReadNullTime(buffer);
		result.Comment = ReadWString(buffer);
		result.Tag = ReadWString(buffer);
		result.Magic = ReadNullInt32(buffer);
		result.IsReducedOpenCommission = ReadBoolean(buffer);
		result.IsReducedCloseCommission = ReadBoolean(buffer);
		result.ImmediateOrCancel = ReadBoolean(buffer);
		result.MarketWithSlippage = ReadBoolean(buffer);
		result.IOCOverride = ReadNullBoolean(buffer);
		result.IFMOverride = ReadNullBoolean(buffer);
		result.PrevVolume = ReadNullDouble(buffer);
		return result;
	}
	void WriteFxOrderArray(const std::vector<CFxOrder>& arg, MemoryBuffer& buffer)
	{
		WriteUInt32((unsigned __int32)arg.size(), buffer);
		for each(const auto element in arg)
		{
			WriteFxOrder(element, buffer);
		}
	}
	std::vector<CFxOrder> ReadFxOrderArray(MemoryBuffer& buffer)
	{
		const size_t count = buffer.ReadCount();
		std::vector<CFxOrder> result;
		result.reserve(count);
		for(size_t index = 0; index < count; ++index)
		{
			result.push_back(ReadFxOrder(buffer));
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
	void WriteMessage(const CFxMessage& arg, MemoryBuffer& buffer)
	{
		WriteInt32(arg.Type, buffer);
		WriteNullTime(arg.SendingTime, buffer);
		WriteNullTime(arg.ReceivingTime, buffer);
		WriteLocalPointer(arg.Data, buffer);
	}
	CFxMessage ReadMessage(MemoryBuffer& buffer)
	{
		CFxMessage result = CFxMessage();
		result.Type = ReadInt32(buffer);
		result.SendingTime = ReadNullTime(buffer);
		result.ReceivingTime = ReadNullTime(buffer);
		result.Data = ReadLocalPointer(buffer);
		return result;
	}
	void WritePosition(const CFxPositionReport& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.Symbol, buffer);
		WriteDouble(arg.SettlementPrice, buffer);
		WriteDouble(arg.BuyAmount, buffer);
		WriteDouble(arg.SellAmount, buffer);
		WriteDouble(arg.Commission, buffer);
		WriteDouble(arg.AgentCommission, buffer);
		WriteDouble(arg.Swap, buffer);
		WriteNullDouble(arg.Profit, buffer);
		WriteNullDouble(arg.BuyPrice, buffer);
		WriteNullDouble(arg.SellPrice, buffer);
	}
	CFxPositionReport ReadPosition(MemoryBuffer& buffer)
	{
		CFxPositionReport result = CFxPositionReport();
		result.Symbol = ReadAString(buffer);
		result.SettlementPrice = ReadDouble(buffer);
		result.BuyAmount = ReadDouble(buffer);
		result.SellAmount = ReadDouble(buffer);
		result.Commission = ReadDouble(buffer);
		result.AgentCommission = ReadDouble(buffer);
		result.Swap = ReadDouble(buffer);
		result.Profit = ReadNullDouble(buffer);
		result.BuyPrice = ReadNullDouble(buffer);
		result.SellPrice = ReadNullDouble(buffer);
		return result;
	}
	void WritePositionArray(const std::vector<CFxPositionReport>& arg, MemoryBuffer& buffer)
	{
		WriteUInt32((unsigned __int32)arg.size(), buffer);
		for each(const auto element in arg)
		{
			WritePosition(element, buffer);
		}
	}
	std::vector<CFxPositionReport> ReadPositionArray(MemoryBuffer& buffer)
	{
		const size_t count = buffer.ReadCount();
		std::vector<CFxPositionReport> result;
		result.reserve(count);
		for(size_t index = 0; index < count; ++index)
		{
			result.push_back(ReadPosition(buffer));
		}
		return result;
	}
	void WriteTradeTransactionReportType(const FxTradeTransactionReportType& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	FxTradeTransactionReportType ReadTradeTransactionReportType(MemoryBuffer& buffer)
	{
		auto result = (FxTradeTransactionReportType)ReadInt32(buffer);
		return result;
	}
	void WriteTradeTransactionReason(const FxTradeTransactionReason& arg, MemoryBuffer& buffer)
	{
		WriteInt32((__int32)arg, buffer);
	}
	FxTradeTransactionReason ReadTradeTransactionReason(MemoryBuffer& buffer)
	{
		auto result = (FxTradeTransactionReason)ReadInt32(buffer);
		return result;
	}
	void WriteTradeTransactionReport(const CFxTradeTransactionReport& arg, MemoryBuffer& buffer)
	{
		WriteTradeTransactionReportType(arg.TradeTransactionReportType, buffer);
		WriteTradeTransactionReason(arg.TradeTransactionReason, buffer);
		WriteDouble(arg.AccountBalance, buffer);
		WriteDouble(arg.TransactionAmount, buffer);
		WriteAString(arg.TransactionCurrency, buffer);
		WriteAString(arg.Id, buffer);
		WriteAString(arg.ClientId, buffer);
		WriteDouble(arg.Quantity, buffer);
		WriteNullDouble(arg.MaxVisibleQuantity, buffer);
		WriteDouble(arg.LeavesQuantity, buffer);
		WriteDouble(arg.Price, buffer);
		WriteDouble(arg.StopPrice, buffer);
		WriteTradeRecordType(arg.InitialTradeRecordType, buffer);
		WriteTradeRecordType(arg.TradeRecordType, buffer);
		WriteTradeRecordSide(arg.TradeRecordSide, buffer);
		WriteAString(arg.Symbol, buffer);
		WriteWString(arg.Comment, buffer);
		WriteWString(arg.Tag, buffer);
		WriteNullInt32(arg.Magic, buffer);
		WriteBoolean(arg.IsReducedOpenCommission, buffer);
		WriteBoolean(arg.IsReducedCloseCommission, buffer);
		WriteBoolean(arg.ImmediateOrCancel, buffer);
		WriteBoolean(arg.MarketWithSlippage, buffer);
		WriteNullDouble(arg.ReqOpenPrice, buffer);
		WriteNullDouble(arg.ReqOpenQuantity, buffer);
		WriteNullDouble(arg.ReqClosePrice, buffer);
		WriteNullDouble(arg.ReqCloseQuantity, buffer);
		WriteTime(arg.OrderCreated, buffer);
		WriteTime(arg.OrderModified, buffer);
		WriteAString(arg.PositionId, buffer);
		WriteAString(arg.PositionById, buffer);
		WriteTime(arg.PositionOpened, buffer);
		WriteDouble(arg.PosOpenReqPrice, buffer);
		WriteDouble(arg.PosOpenPrice, buffer);
		WriteDouble(arg.PositionQuantity, buffer);
		WriteDouble(arg.PositionLastQuantity, buffer);
		WriteDouble(arg.PositionLeavesQuantity, buffer);
		WriteDouble(arg.PositionCloseRequestedPrice, buffer);
		WriteDouble(arg.PositionClosePrice, buffer);
		WriteTime(arg.PositionClosed, buffer);
		WriteTime(arg.PositionModified, buffer);
		WriteTradeRecordSide(arg.PosRemainingSide, buffer);
		WriteNullDouble(arg.PosRemainingPrice, buffer);
		WriteDouble(arg.Commission, buffer);
		WriteDouble(arg.AgentCommission, buffer);
		WriteDouble(arg.Swap, buffer);
		WriteAString(arg.CommCurrency, buffer);
		WriteDouble(arg.StopLoss, buffer);
		WriteDouble(arg.TakeProfit, buffer);
		WriteAString(arg.NextStreamPositionId, buffer);
		WriteTime(arg.TransactionTime, buffer);
		WriteNullDouble(arg.OrderFillPrice, buffer);
		WriteNullDouble(arg.OrderLastFillAmount, buffer);
		WriteNullDouble(arg.OpenConversionRate, buffer);
		WriteNullDouble(arg.CloseConversionRate, buffer);
		WriteInt32(arg.ActionId, buffer);
		WriteNullTime(arg.Expiration, buffer);
		WriteAString(arg.SrcAssetCurrency, buffer);
		WriteNullDouble(arg.SrcAssetAmount, buffer);
		WriteNullDouble(arg.SrcAssetMovement, buffer);
		WriteAString(arg.DstAssetCurrency, buffer);
		WriteNullDouble(arg.DstAssetAmount, buffer);
		WriteNullDouble(arg.DstAssetMovement, buffer);
		WriteNullDouble(arg.MarginCurrencyToUsdConversionRate, buffer);
		WriteNullDouble(arg.UsdToMarginCurrencyConversionRate, buffer);
		WriteAString(arg.MarginCurrency, buffer);
		WriteNullDouble(arg.ProfitCurrencyToUsdConversionRate, buffer);
		WriteNullDouble(arg.UsdToProfitCurrencyConversionRate, buffer);
		WriteAString(arg.ProfitCurrency, buffer);
		WriteNullDouble(arg.SrcAssetToUsdConversionRate, buffer);
		WriteNullDouble(arg.UsdToSrcAssetConversionRate, buffer);
		WriteNullDouble(arg.DstAssetToUsdConversionRate, buffer);
		WriteNullDouble(arg.UsdToDstAssetConversionRate, buffer);
		WriteAString(arg.MinCommissionCurrency, buffer);
		WriteNullDouble(arg.MinCommissionConversionRate, buffer);
	}
	CFxTradeTransactionReport ReadTradeTransactionReport(MemoryBuffer& buffer)
	{
		CFxTradeTransactionReport result = CFxTradeTransactionReport();
		result.TradeTransactionReportType = ReadTradeTransactionReportType(buffer);
		result.TradeTransactionReason = ReadTradeTransactionReason(buffer);
		result.AccountBalance = ReadDouble(buffer);
		result.TransactionAmount = ReadDouble(buffer);
		result.TransactionCurrency = ReadAString(buffer);
		result.Id = ReadAString(buffer);
		result.ClientId = ReadAString(buffer);
		result.Quantity = ReadDouble(buffer);
		result.MaxVisibleQuantity = ReadNullDouble(buffer);
		result.LeavesQuantity = ReadDouble(buffer);
		result.Price = ReadDouble(buffer);
		result.StopPrice = ReadDouble(buffer);
		result.InitialTradeRecordType = ReadTradeRecordType(buffer);
		result.TradeRecordType = ReadTradeRecordType(buffer);
		result.TradeRecordSide = ReadTradeRecordSide(buffer);
		result.Symbol = ReadAString(buffer);
		result.Comment = ReadWString(buffer);
		result.Tag = ReadWString(buffer);
		result.Magic = ReadNullInt32(buffer);
		result.IsReducedOpenCommission = ReadBoolean(buffer);
		result.IsReducedCloseCommission = ReadBoolean(buffer);
		result.ImmediateOrCancel = ReadBoolean(buffer);
		result.MarketWithSlippage = ReadBoolean(buffer);
		result.ReqOpenPrice = ReadNullDouble(buffer);
		result.ReqOpenQuantity = ReadNullDouble(buffer);
		result.ReqClosePrice = ReadNullDouble(buffer);
		result.ReqCloseQuantity = ReadNullDouble(buffer);
		result.OrderCreated = ReadTime(buffer);
		result.OrderModified = ReadTime(buffer);
		result.PositionId = ReadAString(buffer);
		result.PositionById = ReadAString(buffer);
		result.PositionOpened = ReadTime(buffer);
		result.PosOpenReqPrice = ReadDouble(buffer);
		result.PosOpenPrice = ReadDouble(buffer);
		result.PositionQuantity = ReadDouble(buffer);
		result.PositionLastQuantity = ReadDouble(buffer);
		result.PositionLeavesQuantity = ReadDouble(buffer);
		result.PositionCloseRequestedPrice = ReadDouble(buffer);
		result.PositionClosePrice = ReadDouble(buffer);
		result.PositionClosed = ReadTime(buffer);
		result.PositionModified = ReadTime(buffer);
		result.PosRemainingSide = ReadTradeRecordSide(buffer);
		result.PosRemainingPrice = ReadNullDouble(buffer);
		result.Commission = ReadDouble(buffer);
		result.AgentCommission = ReadDouble(buffer);
		result.Swap = ReadDouble(buffer);
		result.CommCurrency = ReadAString(buffer);
		result.StopLoss = ReadDouble(buffer);
		result.TakeProfit = ReadDouble(buffer);
		result.NextStreamPositionId = ReadAString(buffer);
		result.TransactionTime = ReadTime(buffer);
		result.OrderFillPrice = ReadNullDouble(buffer);
		result.OrderLastFillAmount = ReadNullDouble(buffer);
		result.OpenConversionRate = ReadNullDouble(buffer);
		result.CloseConversionRate = ReadNullDouble(buffer);
		result.ActionId = ReadInt32(buffer);
		result.Expiration = ReadNullTime(buffer);
		result.SrcAssetCurrency = ReadAString(buffer);
		result.SrcAssetAmount = ReadNullDouble(buffer);
		result.SrcAssetMovement = ReadNullDouble(buffer);
		result.DstAssetCurrency = ReadAString(buffer);
		result.DstAssetAmount = ReadNullDouble(buffer);
		result.DstAssetMovement = ReadNullDouble(buffer);
		result.MarginCurrencyToUsdConversionRate = ReadNullDouble(buffer);
		result.UsdToMarginCurrencyConversionRate = ReadNullDouble(buffer);
		result.MarginCurrency = ReadAString(buffer);
		result.ProfitCurrencyToUsdConversionRate = ReadNullDouble(buffer);
		result.UsdToProfitCurrencyConversionRate = ReadNullDouble(buffer);
		result.ProfitCurrency = ReadAString(buffer);
		result.SrcAssetToUsdConversionRate = ReadNullDouble(buffer);
		result.UsdToSrcAssetConversionRate = ReadNullDouble(buffer);
		result.DstAssetToUsdConversionRate = ReadNullDouble(buffer);
		result.UsdToDstAssetConversionRate = ReadNullDouble(buffer);
		result.MinCommissionCurrency = ReadAString(buffer);
		result.MinCommissionConversionRate = ReadNullDouble(buffer);
		return result;
	}
	void WriteDailyAccountSnapshotReport(const CFxDailyAccountSnapshotReport& arg, MemoryBuffer& buffer)
	{
		WriteTime(arg.Timestamp, buffer);
		WriteAString(arg.AccountId, buffer);
		WriteAccountType(arg.Type, buffer);
		WriteAString(arg.BalanceCurrency, buffer);
		WriteInt32(arg.Leverage, buffer);
		WriteDouble(arg.Balance, buffer);
		WriteDouble(arg.Margin, buffer);
		WriteDouble(arg.MarginLevel, buffer);
		WriteDouble(arg.Equity, buffer);
		WriteDouble(arg.Swap, buffer);
		WriteDouble(arg.Profit, buffer);
		WriteDouble(arg.Commission, buffer);
		WriteDouble(arg.AgentCommission, buffer);
		WriteBoolean(arg.IsValid, buffer);
		WriteBoolean(arg.IsReadOnly, buffer);
		WriteBoolean(arg.IsBlocked, buffer);
		WriteNullDouble(arg.BalanceCurrencyToUsdConversionRate, buffer);
		WriteNullDouble(arg.UsdToBalanceCurrencyConversionRate, buffer);
		WriteNullDouble(arg.ProfitCurrencyToUsdConversionRate, buffer);
		WriteNullDouble(arg.UsdToProfitCurrencyConversionRate, buffer);
		WriteAssetInfoArray(arg.Assets, buffer);
	}
	CFxDailyAccountSnapshotReport ReadDailyAccountSnapshotReport(MemoryBuffer& buffer)
	{
		CFxDailyAccountSnapshotReport result = CFxDailyAccountSnapshotReport();
		result.Timestamp = ReadTime(buffer);
		result.AccountId = ReadAString(buffer);
		result.Type = ReadAccountType(buffer);
		result.BalanceCurrency = ReadAString(buffer);
		result.Leverage = ReadInt32(buffer);
		result.Balance = ReadDouble(buffer);
		result.Margin = ReadDouble(buffer);
		result.MarginLevel = ReadDouble(buffer);
		result.Equity = ReadDouble(buffer);
		result.Swap = ReadDouble(buffer);
		result.Profit = ReadDouble(buffer);
		result.Commission = ReadDouble(buffer);
		result.AgentCommission = ReadDouble(buffer);
		result.IsValid = ReadBoolean(buffer);
		result.IsReadOnly = ReadBoolean(buffer);
		result.IsBlocked = ReadBoolean(buffer);
		result.BalanceCurrencyToUsdConversionRate = ReadNullDouble(buffer);
		result.UsdToBalanceCurrencyConversionRate = ReadNullDouble(buffer);
		result.ProfitCurrencyToUsdConversionRate = ReadNullDouble(buffer);
		result.UsdToProfitCurrencyConversionRate = ReadNullDouble(buffer);
		result.Assets = ReadAssetInfoArray(buffer);
		return result;
	}
	void WriteClosePositionResult(const CFxClosePositionResult& arg, MemoryBuffer& buffer)
	{
		WriteDouble(arg.ExecutedVolume, buffer);
		WriteDouble(arg.ExecutedPrice, buffer);
		WriteBoolean(arg.Sucess, buffer);
	}
	CFxClosePositionResult ReadClosePositionResult(MemoryBuffer& buffer)
	{
		CFxClosePositionResult result = CFxClosePositionResult();
		result.ExecutedVolume = ReadDouble(buffer);
		result.ExecutedPrice = ReadDouble(buffer);
		result.Sucess = ReadBoolean(buffer);
		return result;
	}
	void WriteExecutionReport(const CFxExecutionReport& arg, MemoryBuffer& buffer)
	{
		WriteAString(arg.OrderId, buffer);
		WriteAString(arg.ClientOrderId, buffer);
		WriteAString(arg.TradeRequestId, buffer);
		WriteOrderStatus(arg.OrderStatus, buffer);
		WriteExecutionType(arg.ExecutionType, buffer);
		WriteAString(arg.Symbol, buffer);
		WriteDouble(arg.ExecutedVolume, buffer);
		WriteNullDouble(arg.InitialVolume, buffer);
		WriteDouble(arg.LeavesVolume, buffer);
		WriteNullDouble(arg.MaxVisibleVolume, buffer);
		WriteNullDouble(arg.TradeAmount, buffer);
		WriteDouble(arg.Commission, buffer);
		WriteDouble(arg.AgentCommission, buffer);
		WriteDouble(arg.Swap, buffer);
		WriteTradeRecordType(arg.InitialOrderType, buffer);
		WriteTradeRecordType(arg.OrderType, buffer);
		WriteTradeRecordSide(arg.OrderSide, buffer);
		WriteNullDouble(arg.AveragePrice, buffer);
		WriteNullDouble(arg.Price, buffer);
		WriteNullDouble(arg.StopPrice, buffer);
		WriteDouble(arg.TradePrice, buffer);
		WriteNullTime(arg.Expiration, buffer);
		WriteNullTime(arg.Created, buffer);
		WriteNullTime(arg.Modified, buffer);
		WriteRejectReason(arg.RejectReason, buffer);
		WriteNullDouble(arg.TakeProfit, buffer);
		WriteNullDouble(arg.StopLoss, buffer);
		WriteAString(arg.Text, buffer);
		WriteWString(arg.Comment, buffer);
		WriteWString(arg.Tag, buffer);
		WriteNullInt32(arg.Magic, buffer);
		WriteAString(arg.ClosePositionRequestId, buffer);
		WriteAssetInfoArray(arg.Assets, buffer);
		WriteDouble(arg.Balance, buffer);
		WriteBoolean(arg.IsReducedOpenCommission, buffer);
		WriteBoolean(arg.IsReducedCloseCommission, buffer);
		WriteBoolean(arg.ImmediateOrCancel, buffer);
		WriteBoolean(arg.MarketWithSlippage, buffer);
		WriteNullDouble(arg.ReqOpenPrice, buffer);
		WriteNullDouble(arg.ReqOpenVolume, buffer);
	}
	CFxExecutionReport ReadExecutionReport(MemoryBuffer& buffer)
	{
		CFxExecutionReport result = CFxExecutionReport();
		result.OrderId = ReadAString(buffer);
		result.ClientOrderId = ReadAString(buffer);
		result.TradeRequestId = ReadAString(buffer);
		result.OrderStatus = ReadOrderStatus(buffer);
		result.ExecutionType = ReadExecutionType(buffer);
		result.Symbol = ReadAString(buffer);
		result.ExecutedVolume = ReadDouble(buffer);
		result.InitialVolume = ReadNullDouble(buffer);
		result.LeavesVolume = ReadDouble(buffer);
		result.MaxVisibleVolume = ReadNullDouble(buffer);
		result.TradeAmount = ReadNullDouble(buffer);
		result.Commission = ReadDouble(buffer);
		result.AgentCommission = ReadDouble(buffer);
		result.Swap = ReadDouble(buffer);
		result.InitialOrderType = ReadTradeRecordType(buffer);
		result.OrderType = ReadTradeRecordType(buffer);
		result.OrderSide = ReadTradeRecordSide(buffer);
		result.AveragePrice = ReadNullDouble(buffer);
		result.Price = ReadNullDouble(buffer);
		result.StopPrice = ReadNullDouble(buffer);
		result.TradePrice = ReadDouble(buffer);
		result.Expiration = ReadNullTime(buffer);
		result.Created = ReadNullTime(buffer);
		result.Modified = ReadNullTime(buffer);
		result.RejectReason = ReadRejectReason(buffer);
		result.TakeProfit = ReadNullDouble(buffer);
		result.StopLoss = ReadNullDouble(buffer);
		result.Text = ReadAString(buffer);
		result.Comment = ReadWString(buffer);
		result.Tag = ReadWString(buffer);
		result.Magic = ReadNullInt32(buffer);
		result.ClosePositionRequestId = ReadAString(buffer);
		result.Assets = ReadAssetInfoArray(buffer);
		result.Balance = ReadDouble(buffer);
		result.IsReducedOpenCommission = ReadBoolean(buffer);
		result.IsReducedCloseCommission = ReadBoolean(buffer);
		result.ImmediateOrCancel = ReadBoolean(buffer);
		result.MarketWithSlippage = ReadBoolean(buffer);
		result.ReqOpenPrice = ReadNullDouble(buffer);
		result.ReqOpenVolume = ReadNullDouble(buffer);
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
		string _message = ReadAString(buffer);
		throw std::exception(_message.c_str());
	}
}
