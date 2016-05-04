// This is always generated file. Do not change anything.

namespace
{
	void LrpWriteLogoutReason(const char* name, const FxLogoutReason& arg, std::ostream& _stream);
	void LrpWriteSessionStatus(const char* name, const SessionStatus& arg, std::ostream& _stream);
	void LrpWriteProfitCalcMode(const char* name, const ProfitCalcMode& arg, std::ostream& _stream);
	void LrpWriteMarginCalcMode(const char* name, const MarginCalcMode& arg, std::ostream& _stream);
	void LrpWriteCommissionType(const char* name, const FxCommissionType& arg, std::ostream& _stream);
	void LrpWriteCommissionChargeType(const char* name, const FxCommissionChargeType& arg, std::ostream& _stream);
	void LrpWriteCommissionChargeMethod(const char* name, const FxCommissionChargeMethod& arg, std::ostream& _stream);
	void LrpWriteMarketHistoryRejectType(const char* name, const FxMarketHistoryRejectType& arg, std::ostream& _stream);
	void LrpWriteNotificationType(const char* name, const NotificationType& arg, std::ostream& _stream);
	void LrpWriteSeverity(const char* name, const Severity& arg, std::ostream& _stream);
	void LrpWriteLrpSessionInfo(const char* name, const CFxSessionInfo& arg, std::ostream& _stream);
	void LrpWriteLrpSessionInfo2(const char* name, const CFxSessionInfo& arg, std::ostream& _stream);
	void LrpWriteCurrencyInfo(const char* name, const CFxCurrencyInfo& arg, std::ostream& _stream);
	void LrpWriteSymbolInfo(const char* name, const CFxSymbolInfo& arg, std::ostream& _stream);
	void LrpWriteSymbolInfo2(const char* name, const CFxSymbolInfo& arg, std::ostream& _stream);
	void LrpWriteSymbolInfo3(const char* name, const CFxSymbolInfo& arg, std::ostream& _stream);
	void LrpWriteSymbolInfo4(const char* name, const CFxSymbolInfo& arg, std::ostream& _stream);
	void LrpWriteSymbolInfo5(const char* name, const CFxSymbolInfo& arg, std::ostream& _stream);
	void LrpWriteSymbolInfo6(const char* name, const CFxSymbolInfo& arg, std::ostream& _stream);
	void LrpWriteSymbolInfo7(const char* name, const CFxSymbolInfo& arg, std::ostream& _stream);
	void LrpWriteAStringArray(const char* name, const std::vector<std::string>& arg, std::ostream& _stream);
	template<size_t count> void LrpWriteAStringArray(const char* name, const std::string(&arg)[count], std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream << "[" << count << "]{";
		const std::string* it = arg;
		const std::string* end = it + count;
		if (it != end)
		{
			LrpWriteAString(nullptr, *it, _stream);
			_stream << ";";
			++it;
		}
		for (; it != end; ++it)
		{
			_stream << " ";
			LrpWriteAString(nullptr, *it, _stream);
			_stream << ";";
		}
		_stream << "}";
	}
	void LrpWriteUInt8Array(const char* name, const std::vector<unsigned __int8>& arg, std::ostream& _stream);
	template<size_t count> void LrpWriteUInt8Array(const char* name, const unsigned __int8(&arg)[count], std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream << "[" << count << "]{";
		const unsigned __int8* it = arg;
		const unsigned __int8* end = it + count;
		if (it != end)
		{
			LrpWriteUInt8(nullptr, *it, _stream);
			_stream << ";";
			++it;
		}
		for (; it != end; ++it)
		{
			_stream << " ";
			LrpWriteUInt8(nullptr, *it, _stream);
			_stream << ";";
		}
		_stream << "}";
	}
	void LrpWriteSymbolInfoArray(const char* name, const std::vector<CFxSymbolInfo>& arg, std::ostream& _stream);
	template<size_t count> void LrpWriteSymbolInfoArray(const char* name, const CFxSymbolInfo(&arg)[count], std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream << "[" << count << "]{";
		const CFxSymbolInfo* it = arg;
		const CFxSymbolInfo* end = it + count;
		if (it != end)
		{
			LrpWriteSymbolInfo(nullptr, *it, _stream);
			_stream << ";";
			++it;
		}
		for (; it != end; ++it)
		{
			_stream << " ";
			LrpWriteSymbolInfo(nullptr, *it, _stream);
			_stream << ";";
		}
		_stream << "}";
	}
	void LrpWriteCurrencyInfoArray(const char* name, const std::vector<CFxCurrencyInfo>& arg, std::ostream& _stream);
	template<size_t count> void LrpWriteCurrencyInfoArray(const char* name, const CFxCurrencyInfo(&arg)[count], std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream << "[" << count << "]{";
		const CFxCurrencyInfo* it = arg;
		const CFxCurrencyInfo* end = it + count;
		if (it != end)
		{
			LrpWriteCurrencyInfo(nullptr, *it, _stream);
			_stream << ";";
			++it;
		}
		for (; it != end; ++it)
		{
			_stream << " ";
			LrpWriteCurrencyInfo(nullptr, *it, _stream);
			_stream << ";";
		}
		_stream << "}";
	}
	void LrpWriteSymbolInfoArray2(const char* name, const std::vector<CFxSymbolInfo>& arg, std::ostream& _stream);
	template<size_t count> void LrpWriteSymbolInfoArray2(const char* name, const CFxSymbolInfo(&arg)[count], std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream << "[" << count << "]{";
		const CFxSymbolInfo* it = arg;
		const CFxSymbolInfo* end = it + count;
		if (it != end)
		{
			LrpWriteSymbolInfo2(nullptr, *it, _stream);
			_stream << ";";
			++it;
		}
		for (; it != end; ++it)
		{
			_stream << " ";
			LrpWriteSymbolInfo2(nullptr, *it, _stream);
			_stream << ";";
		}
		_stream << "}";
	}
	void LrpWriteSymbolInfoArray3(const char* name, const std::vector<CFxSymbolInfo>& arg, std::ostream& _stream);
	template<size_t count> void LrpWriteSymbolInfoArray3(const char* name, const CFxSymbolInfo(&arg)[count], std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream << "[" << count << "]{";
		const CFxSymbolInfo* it = arg;
		const CFxSymbolInfo* end = it + count;
		if (it != end)
		{
			LrpWriteSymbolInfo3(nullptr, *it, _stream);
			_stream << ";";
			++it;
		}
		for (; it != end; ++it)
		{
			_stream << " ";
			LrpWriteSymbolInfo3(nullptr, *it, _stream);
			_stream << ";";
		}
		_stream << "}";
	}
	void LrpWriteSymbolInfoArray4(const char* name, const std::vector<CFxSymbolInfo>& arg, std::ostream& _stream);
	template<size_t count> void LrpWriteSymbolInfoArray4(const char* name, const CFxSymbolInfo(&arg)[count], std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream << "[" << count << "]{";
		const CFxSymbolInfo* it = arg;
		const CFxSymbolInfo* end = it + count;
		if (it != end)
		{
			LrpWriteSymbolInfo4(nullptr, *it, _stream);
			_stream << ";";
			++it;
		}
		for (; it != end; ++it)
		{
			_stream << " ";
			LrpWriteSymbolInfo4(nullptr, *it, _stream);
			_stream << ";";
		}
		_stream << "}";
	}
	void LrpWriteSymbolInfoArray5(const char* name, const std::vector<CFxSymbolInfo>& arg, std::ostream& _stream);
	template<size_t count> void LrpWriteSymbolInfoArray5(const char* name, const CFxSymbolInfo(&arg)[count], std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream << "[" << count << "]{";
		const CFxSymbolInfo* it = arg;
		const CFxSymbolInfo* end = it + count;
		if (it != end)
		{
			LrpWriteSymbolInfo5(nullptr, *it, _stream);
			_stream << ";";
			++it;
		}
		for (; it != end; ++it)
		{
			_stream << " ";
			LrpWriteSymbolInfo5(nullptr, *it, _stream);
			_stream << ";";
		}
		_stream << "}";
	}
	void LrpWriteSymbolInfoArray6(const char* name, const std::vector<CFxSymbolInfo>& arg, std::ostream& _stream);
	template<size_t count> void LrpWriteSymbolInfoArray6(const char* name, const CFxSymbolInfo(&arg)[count], std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream << "[" << count << "]{";
		const CFxSymbolInfo* it = arg;
		const CFxSymbolInfo* end = it + count;
		if (it != end)
		{
			LrpWriteSymbolInfo6(nullptr, *it, _stream);
			_stream << ";";
			++it;
		}
		for (; it != end; ++it)
		{
			_stream << " ";
			LrpWriteSymbolInfo6(nullptr, *it, _stream);
			_stream << ";";
		}
		_stream << "}";
	}
	void LrpWriteSymbolInfoArray7(const char* name, const std::vector<CFxSymbolInfo>& arg, std::ostream& _stream);
	template<size_t count> void LrpWriteSymbolInfoArray7(const char* name, const CFxSymbolInfo(&arg)[count], std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream << "[" << count << "]{";
		const CFxSymbolInfo* it = arg;
		const CFxSymbolInfo* end = it + count;
		if (it != end)
		{
			LrpWriteSymbolInfo7(nullptr, *it, _stream);
			_stream << ";";
			++it;
		}
		for (; it != end; ++it)
		{
			_stream << " ";
			LrpWriteSymbolInfo7(nullptr, *it, _stream);
			_stream << ";";
		}
		_stream << "}";
	}
	void LrpWriteQuoteEntry(const char* name, const CFxQuoteEntry& arg, std::ostream& _stream);
	void LrpWriteQuoteEntryArray(const char* name, const std::vector<CFxQuoteEntry>& arg, std::ostream& _stream);
	template<size_t count> void LrpWriteQuoteEntryArray(const char* name, const CFxQuoteEntry(&arg)[count], std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream << "[" << count << "]{";
		const CFxQuoteEntry* it = arg;
		const CFxQuoteEntry* end = it + count;
		if (it != end)
		{
			LrpWriteQuoteEntry(nullptr, *it, _stream);
			_stream << ";";
			++it;
		}
		for (; it != end; ++it)
		{
			_stream << " ";
			LrpWriteQuoteEntry(nullptr, *it, _stream);
			_stream << ";";
		}
		_stream << "}";
	}
	void LrpWriteQuote(const char* name, const CFxQuote& arg, std::ostream& _stream);
	void LrpWriteQuoteArray(const char* name, const std::vector<CFxQuote>& arg, std::ostream& _stream);
	template<size_t count> void LrpWriteQuoteArray(const char* name, const CFxQuote(&arg)[count], std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream << "[" << count << "]{";
		const CFxQuote* it = arg;
		const CFxQuote* end = it + count;
		if (it != end)
		{
			LrpWriteQuote(nullptr, *it, _stream);
			_stream << ";";
			++it;
		}
		for (; it != end; ++it)
		{
			_stream << " ";
			LrpWriteQuote(nullptr, *it, _stream);
			_stream << ";";
		}
		_stream << "}";
	}
	void LrpWriteBar(const char* name, const CFxBar& arg, std::ostream& _stream);
	void LrpWriteBarArray(const char* name, const std::vector<CFxBar>& arg, std::ostream& _stream);
	template<size_t count> void LrpWriteBarArray(const char* name, const CFxBar(&arg)[count], std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream << "[" << count << "]{";
		const CFxBar* it = arg;
		const CFxBar* end = it + count;
		if (it != end)
		{
			LrpWriteBar(nullptr, *it, _stream);
			_stream << ";";
			++it;
		}
		for (; it != end; ++it)
		{
			_stream << " ";
			LrpWriteBar(nullptr, *it, _stream);
			_stream << ";";
		}
		_stream << "}";
	}
	void LrpWriteDataHistoryResponse(const char* name, const CFxDataHistoryResponse& arg, std::ostream& _stream);
	void LrpWriteFileChunk(const char* name, const CFxFileChunk& arg, std::ostream& _stream);
	void LrpWriteNotification(const char* name, const CNotification& arg, std::ostream& _stream);
}
namespace
{
	void LrpWriteLogoutReason(const char* name, const FxLogoutReason& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream<<static_cast<__int32>(arg);
	}
	void LrpWriteSessionStatus(const char* name, const SessionStatus& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream<<static_cast<__int32>(arg);
	}
	void LrpWriteProfitCalcMode(const char* name, const ProfitCalcMode& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream<<static_cast<__int32>(arg);
	}
	void LrpWriteMarginCalcMode(const char* name, const MarginCalcMode& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream<<static_cast<__int32>(arg);
	}
	void LrpWriteCommissionType(const char* name, const FxCommissionType& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream<<static_cast<__int32>(arg);
	}
	void LrpWriteCommissionChargeType(const char* name, const FxCommissionChargeType& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream<<static_cast<__int32>(arg);
	}
	void LrpWriteCommissionChargeMethod(const char* name, const FxCommissionChargeMethod& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream<<static_cast<__int32>(arg);
	}
	void LrpWriteMarketHistoryRejectType(const char* name, const FxMarketHistoryRejectType& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream<<static_cast<__int32>(arg);
	}
	void LrpWriteNotificationType(const char* name, const NotificationType& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream<<static_cast<__int32>(arg);
	}
	void LrpWriteSeverity(const char* name, const Severity& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream<<static_cast<__int32>(arg);
	}
	void LrpWriteLrpSessionInfo(const char* name, const CFxSessionInfo& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream<<"{";
		LrpWriteAString("TradingSessionId", arg.TradingSessionId, _stream);
		_stream << ';';
		LrpWriteSessionStatus("Status", arg.Status, _stream);
		_stream << ';';
		LrpWriteInt32("ServerTimeZoneOffset", arg.ServerTimeZoneOffset, _stream);
		_stream << ';';
		LrpWriteTime("StartTime", arg.StartTime, _stream);
		_stream << ';';
		LrpWriteTime("OpenTime", arg.OpenTime, _stream);
		_stream << ';';
		LrpWriteTime("CloseTime", arg.CloseTime, _stream);
		_stream << ';';
		LrpWriteTime("EndTime", arg.EndTime, _stream);
		_stream << ';';
		_stream<<"}";
	}
	void LrpWriteLrpSessionInfo2(const char* name, const CFxSessionInfo& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream<<"{";
		LrpWriteAString("TradingSessionId", arg.TradingSessionId, _stream);
		_stream << ';';
		LrpWriteSessionStatus("Status", arg.Status, _stream);
		_stream << ';';
		LrpWriteInt32("ServerTimeZoneOffset", arg.ServerTimeZoneOffset, _stream);
		_stream << ';';
		LrpWriteAString("PlatformName", arg.PlatformName, _stream);
		_stream << ';';
		LrpWriteAString("PlatformCompany", arg.PlatformCompany, _stream);
		_stream << ';';
		LrpWriteTime("StartTime", arg.StartTime, _stream);
		_stream << ';';
		LrpWriteTime("OpenTime", arg.OpenTime, _stream);
		_stream << ';';
		LrpWriteTime("CloseTime", arg.CloseTime, _stream);
		_stream << ';';
		LrpWriteTime("EndTime", arg.EndTime, _stream);
		_stream << ';';
		_stream<<"}";
	}
	void LrpWriteCurrencyInfo(const char* name, const CFxCurrencyInfo& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream<<"{";
		LrpWriteAString("Name", arg.Name, _stream);
		_stream << ';';
		LrpWriteAString("Description", arg.Description, _stream);
		_stream << ';';
		LrpWriteInt32("SortOrder", arg.SortOrder, _stream);
		_stream << ';';
		LrpWriteInt32("Precision", arg.Precision, _stream);
		_stream << ';';
		_stream<<"}";
	}
	void LrpWriteSymbolInfo(const char* name, const CFxSymbolInfo& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream<<"{";
		LrpWriteAString("Name", arg.Name, _stream);
		_stream << ';';
		LrpWriteAString("Currency", arg.Currency, _stream);
		_stream << ';';
		LrpWriteAString("SettlementCurrency", arg.SettlementCurrency, _stream);
		_stream << ';';
		LrpWriteDouble("ContractMultiplier", arg.ContractMultiplier, _stream);
		_stream << ';';
		LrpWriteInt32("Precision", arg.Precision, _stream);
		_stream << ';';
		LrpWriteDouble("RoundLot", arg.RoundLot, _stream);
		_stream << ';';
		LrpWriteDouble("MinTradeVolume", arg.MinTradeVolume, _stream);
		_stream << ';';
		LrpWriteDouble("MaxTradeVolume", arg.MaxTradeVolume, _stream);
		_stream << ';';
		LrpWriteDouble("TradeVolumeStep", arg.TradeVolumeStep, _stream);
		_stream << ';';
		LrpWriteProfitCalcMode("ProfitCalcMode", arg.ProfitCalcMode, _stream);
		_stream << ';';
		LrpWriteMarginCalcMode("MarginCalcMode", arg.MarginCalcMode, _stream);
		_stream << ';';
		LrpWriteDouble("MarginHedge", arg.MarginHedge, _stream);
		_stream << ';';
		LrpWriteInt32("MarginFactor", arg.MarginFactor, _stream);
		_stream << ';';
		LrpWriteInt32("Color", arg.Color, _stream);
		_stream << ';';
		_stream<<"}";
	}
	void LrpWriteSymbolInfo2(const char* name, const CFxSymbolInfo& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream<<"{";
		LrpWriteAString("Name", arg.Name, _stream);
		_stream << ';';
		LrpWriteAString("Currency", arg.Currency, _stream);
		_stream << ';';
		LrpWriteAString("SettlementCurrency", arg.SettlementCurrency, _stream);
		_stream << ';';
		LrpWriteDouble("ContractMultiplier", arg.ContractMultiplier, _stream);
		_stream << ';';
		LrpWriteInt32("Precision", arg.Precision, _stream);
		_stream << ';';
		LrpWriteDouble("RoundLot", arg.RoundLot, _stream);
		_stream << ';';
		LrpWriteDouble("MinTradeVolume", arg.MinTradeVolume, _stream);
		_stream << ';';
		LrpWriteDouble("MaxTradeVolume", arg.MaxTradeVolume, _stream);
		_stream << ';';
		LrpWriteDouble("TradeVolumeStep", arg.TradeVolumeStep, _stream);
		_stream << ';';
		LrpWriteProfitCalcMode("ProfitCalcMode", arg.ProfitCalcMode, _stream);
		_stream << ';';
		LrpWriteMarginCalcMode("MarginCalcMode", arg.MarginCalcMode, _stream);
		_stream << ';';
		LrpWriteDouble("MarginHedge", arg.MarginHedge, _stream);
		_stream << ';';
		LrpWriteInt32("MarginFactor", arg.MarginFactor, _stream);
		_stream << ';';
		LrpWriteInt32("Color", arg.Color, _stream);
		_stream << ';';
		LrpWriteCommissionType("CommissionType", arg.CommissionType, _stream);
		_stream << ';';
		LrpWriteCommissionChargeType("CommissionChargeType", arg.CommissionChargeType, _stream);
		_stream << ';';
		LrpWriteDouble("LimitsCommission", arg.LimitsCommission, _stream);
		_stream << ';';
		LrpWriteDouble("Commission", arg.Commission, _stream);
		_stream << ';';
		LrpWriteNullDouble("SwapSizeShort", arg.SwapSizeShort, _stream);
		_stream << ';';
		LrpWriteNullDouble("SwapSizeLong", arg.SwapSizeLong, _stream);
		_stream << ';';
		_stream<<"}";
	}
	void LrpWriteSymbolInfo3(const char* name, const CFxSymbolInfo& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream<<"{";
		LrpWriteAString("Name", arg.Name, _stream);
		_stream << ';';
		LrpWriteAString("Currency", arg.Currency, _stream);
		_stream << ';';
		LrpWriteAString("SettlementCurrency", arg.SettlementCurrency, _stream);
		_stream << ';';
		LrpWriteDouble("ContractMultiplier", arg.ContractMultiplier, _stream);
		_stream << ';';
		LrpWriteInt32("Precision", arg.Precision, _stream);
		_stream << ';';
		LrpWriteDouble("RoundLot", arg.RoundLot, _stream);
		_stream << ';';
		LrpWriteDouble("MinTradeVolume", arg.MinTradeVolume, _stream);
		_stream << ';';
		LrpWriteDouble("MaxTradeVolume", arg.MaxTradeVolume, _stream);
		_stream << ';';
		LrpWriteDouble("TradeVolumeStep", arg.TradeVolumeStep, _stream);
		_stream << ';';
		LrpWriteProfitCalcMode("ProfitCalcMode", arg.ProfitCalcMode, _stream);
		_stream << ';';
		LrpWriteMarginCalcMode("MarginCalcMode", arg.MarginCalcMode, _stream);
		_stream << ';';
		LrpWriteDouble("MarginHedge", arg.MarginHedge, _stream);
		_stream << ';';
		LrpWriteInt32("MarginFactor", arg.MarginFactor, _stream);
		_stream << ';';
		LrpWriteInt32("Color", arg.Color, _stream);
		_stream << ';';
		LrpWriteCommissionType("CommissionType", arg.CommissionType, _stream);
		_stream << ';';
		LrpWriteCommissionChargeType("CommissionChargeType", arg.CommissionChargeType, _stream);
		_stream << ';';
		LrpWriteDouble("LimitsCommission", arg.LimitsCommission, _stream);
		_stream << ';';
		LrpWriteDouble("Commission", arg.Commission, _stream);
		_stream << ';';
		LrpWriteNullDouble("SwapSizeShort", arg.SwapSizeShort, _stream);
		_stream << ';';
		LrpWriteNullDouble("SwapSizeLong", arg.SwapSizeLong, _stream);
		_stream << ';';
		LrpWriteBoolean("IsTradeEnabled", arg.IsTradeEnabled, _stream);
		_stream << ';';
		_stream<<"}";
	}
	void LrpWriteSymbolInfo4(const char* name, const CFxSymbolInfo& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream<<"{";
		LrpWriteAString("Name", arg.Name, _stream);
		_stream << ';';
		LrpWriteAString("Currency", arg.Currency, _stream);
		_stream << ';';
		LrpWriteAString("SettlementCurrency", arg.SettlementCurrency, _stream);
		_stream << ';';
		LrpWriteDouble("ContractMultiplier", arg.ContractMultiplier, _stream);
		_stream << ';';
		LrpWriteInt32("Precision", arg.Precision, _stream);
		_stream << ';';
		LrpWriteDouble("RoundLot", arg.RoundLot, _stream);
		_stream << ';';
		LrpWriteDouble("MinTradeVolume", arg.MinTradeVolume, _stream);
		_stream << ';';
		LrpWriteDouble("MaxTradeVolume", arg.MaxTradeVolume, _stream);
		_stream << ';';
		LrpWriteDouble("TradeVolumeStep", arg.TradeVolumeStep, _stream);
		_stream << ';';
		LrpWriteProfitCalcMode("ProfitCalcMode", arg.ProfitCalcMode, _stream);
		_stream << ';';
		LrpWriteMarginCalcMode("MarginCalcMode", arg.MarginCalcMode, _stream);
		_stream << ';';
		LrpWriteDouble("MarginHedge", arg.MarginHedge, _stream);
		_stream << ';';
		LrpWriteInt32("MarginFactor", arg.MarginFactor, _stream);
		_stream << ';';
		LrpWriteInt32("Color", arg.Color, _stream);
		_stream << ';';
		LrpWriteCommissionType("CommissionType", arg.CommissionType, _stream);
		_stream << ';';
		LrpWriteCommissionChargeType("CommissionChargeType", arg.CommissionChargeType, _stream);
		_stream << ';';
		LrpWriteDouble("LimitsCommission", arg.LimitsCommission, _stream);
		_stream << ';';
		LrpWriteDouble("Commission", arg.Commission, _stream);
		_stream << ';';
		LrpWriteNullDouble("SwapSizeShort", arg.SwapSizeShort, _stream);
		_stream << ';';
		LrpWriteNullDouble("SwapSizeLong", arg.SwapSizeLong, _stream);
		_stream << ';';
		LrpWriteBoolean("IsTradeEnabled", arg.IsTradeEnabled, _stream);
		_stream << ';';
		LrpWriteInt32("GroupSortOrder", arg.GroupSortOrder, _stream);
		_stream << ';';
		LrpWriteInt32("SortOrder", arg.SortOrder, _stream);
		_stream << ';';
		_stream<<"}";
	}
	void LrpWriteSymbolInfo5(const char* name, const CFxSymbolInfo& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream<<"{";
		LrpWriteAString("Name", arg.Name, _stream);
		_stream << ';';
		LrpWriteAString("Currency", arg.Currency, _stream);
		_stream << ';';
		LrpWriteAString("SettlementCurrency", arg.SettlementCurrency, _stream);
		_stream << ';';
		LrpWriteDouble("ContractMultiplier", arg.ContractMultiplier, _stream);
		_stream << ';';
		LrpWriteInt32("Precision", arg.Precision, _stream);
		_stream << ';';
		LrpWriteDouble("RoundLot", arg.RoundLot, _stream);
		_stream << ';';
		LrpWriteDouble("MinTradeVolume", arg.MinTradeVolume, _stream);
		_stream << ';';
		LrpWriteDouble("MaxTradeVolume", arg.MaxTradeVolume, _stream);
		_stream << ';';
		LrpWriteDouble("TradeVolumeStep", arg.TradeVolumeStep, _stream);
		_stream << ';';
		LrpWriteProfitCalcMode("ProfitCalcMode", arg.ProfitCalcMode, _stream);
		_stream << ';';
		LrpWriteMarginCalcMode("MarginCalcMode", arg.MarginCalcMode, _stream);
		_stream << ';';
		LrpWriteDouble("MarginHedge", arg.MarginHedge, _stream);
		_stream << ';';
		LrpWriteInt32("MarginFactor", arg.MarginFactor, _stream);
		_stream << ';';
		LrpWriteNullDouble("MarginFactorFractional", arg.MarginFactorFractional, _stream);
		_stream << ';';
		LrpWriteInt32("Color", arg.Color, _stream);
		_stream << ';';
		LrpWriteCommissionType("CommissionType", arg.CommissionType, _stream);
		_stream << ';';
		LrpWriteCommissionChargeType("CommissionChargeType", arg.CommissionChargeType, _stream);
		_stream << ';';
		LrpWriteDouble("LimitsCommission", arg.LimitsCommission, _stream);
		_stream << ';';
		LrpWriteDouble("Commission", arg.Commission, _stream);
		_stream << ';';
		LrpWriteNullDouble("SwapSizeShort", arg.SwapSizeShort, _stream);
		_stream << ';';
		LrpWriteNullDouble("SwapSizeLong", arg.SwapSizeLong, _stream);
		_stream << ';';
		LrpWriteBoolean("IsTradeEnabled", arg.IsTradeEnabled, _stream);
		_stream << ';';
		LrpWriteInt32("GroupSortOrder", arg.GroupSortOrder, _stream);
		_stream << ';';
		LrpWriteInt32("SortOrder", arg.SortOrder, _stream);
		_stream << ';';
		LrpWriteInt32("CurrencySortOrder", arg.CurrencySortOrder, _stream);
		_stream << ';';
		LrpWriteInt32("SettlementCurrencySortOrder", arg.SettlementCurrencySortOrder, _stream);
		_stream << ';';
		_stream<<"}";
	}
	void LrpWriteSymbolInfo6(const char* name, const CFxSymbolInfo& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream<<"{";
		LrpWriteAString("Name", arg.Name, _stream);
		_stream << ';';
		LrpWriteAString("Currency", arg.Currency, _stream);
		_stream << ';';
		LrpWriteAString("SettlementCurrency", arg.SettlementCurrency, _stream);
		_stream << ';';
		LrpWriteDouble("ContractMultiplier", arg.ContractMultiplier, _stream);
		_stream << ';';
		LrpWriteInt32("Precision", arg.Precision, _stream);
		_stream << ';';
		LrpWriteDouble("RoundLot", arg.RoundLot, _stream);
		_stream << ';';
		LrpWriteDouble("MinTradeVolume", arg.MinTradeVolume, _stream);
		_stream << ';';
		LrpWriteDouble("MaxTradeVolume", arg.MaxTradeVolume, _stream);
		_stream << ';';
		LrpWriteDouble("TradeVolumeStep", arg.TradeVolumeStep, _stream);
		_stream << ';';
		LrpWriteProfitCalcMode("ProfitCalcMode", arg.ProfitCalcMode, _stream);
		_stream << ';';
		LrpWriteMarginCalcMode("MarginCalcMode", arg.MarginCalcMode, _stream);
		_stream << ';';
		LrpWriteDouble("MarginHedge", arg.MarginHedge, _stream);
		_stream << ';';
		LrpWriteInt32("MarginFactor", arg.MarginFactor, _stream);
		_stream << ';';
		LrpWriteNullDouble("MarginFactorFractional", arg.MarginFactorFractional, _stream);
		_stream << ';';
		LrpWriteInt32("Color", arg.Color, _stream);
		_stream << ';';
		LrpWriteCommissionType("CommissionType", arg.CommissionType, _stream);
		_stream << ';';
		LrpWriteCommissionChargeType("CommissionChargeType", arg.CommissionChargeType, _stream);
		_stream << ';';
		LrpWriteDouble("LimitsCommission", arg.LimitsCommission, _stream);
		_stream << ';';
		LrpWriteDouble("Commission", arg.Commission, _stream);
		_stream << ';';
		LrpWriteNullDouble("SwapSizeShort", arg.SwapSizeShort, _stream);
		_stream << ';';
		LrpWriteNullDouble("SwapSizeLong", arg.SwapSizeLong, _stream);
		_stream << ';';
		LrpWriteBoolean("IsTradeEnabled", arg.IsTradeEnabled, _stream);
		_stream << ';';
		LrpWriteInt32("GroupSortOrder", arg.GroupSortOrder, _stream);
		_stream << ';';
		LrpWriteInt32("SortOrder", arg.SortOrder, _stream);
		_stream << ';';
		LrpWriteInt32("CurrencySortOrder", arg.CurrencySortOrder, _stream);
		_stream << ';';
		LrpWriteInt32("SettlementCurrencySortOrder", arg.SettlementCurrencySortOrder, _stream);
		_stream << ';';
		LrpWriteInt32("CurrencyPrecision", arg.CurrencyPrecision, _stream);
		_stream << ';';
		LrpWriteInt32("SettlementCurrencyPrecision", arg.SettlementCurrencyPrecision, _stream);
		_stream << ';';
		_stream<<"}";
	}
	void LrpWriteSymbolInfo7(const char* name, const CFxSymbolInfo& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream<<"{";
		LrpWriteAString("Name", arg.Name, _stream);
		_stream << ';';
		LrpWriteAString("Currency", arg.Currency, _stream);
		_stream << ';';
		LrpWriteAString("SettlementCurrency", arg.SettlementCurrency, _stream);
		_stream << ';';
		LrpWriteDouble("ContractMultiplier", arg.ContractMultiplier, _stream);
		_stream << ';';
		LrpWriteInt32("Precision", arg.Precision, _stream);
		_stream << ';';
		LrpWriteDouble("RoundLot", arg.RoundLot, _stream);
		_stream << ';';
		LrpWriteDouble("MinTradeVolume", arg.MinTradeVolume, _stream);
		_stream << ';';
		LrpWriteDouble("MaxTradeVolume", arg.MaxTradeVolume, _stream);
		_stream << ';';
		LrpWriteDouble("TradeVolumeStep", arg.TradeVolumeStep, _stream);
		_stream << ';';
		LrpWriteProfitCalcMode("ProfitCalcMode", arg.ProfitCalcMode, _stream);
		_stream << ';';
		LrpWriteMarginCalcMode("MarginCalcMode", arg.MarginCalcMode, _stream);
		_stream << ';';
		LrpWriteDouble("MarginHedge", arg.MarginHedge, _stream);
		_stream << ';';
		LrpWriteInt32("MarginFactor", arg.MarginFactor, _stream);
		_stream << ';';
		LrpWriteNullDouble("MarginFactorFractional", arg.MarginFactorFractional, _stream);
		_stream << ';';
		LrpWriteInt32("Color", arg.Color, _stream);
		_stream << ';';
		LrpWriteCommissionType("CommissionType", arg.CommissionType, _stream);
		_stream << ';';
		LrpWriteCommissionChargeType("CommissionChargeType", arg.CommissionChargeType, _stream);
		_stream << ';';
		LrpWriteCommissionChargeMethod("CommissionChargeMethod", arg.CommissionChargeMethod, _stream);
		_stream << ';';
		LrpWriteDouble("LimitsCommission", arg.LimitsCommission, _stream);
		_stream << ';';
		LrpWriteDouble("Commission", arg.Commission, _stream);
		_stream << ';';
		LrpWriteNullDouble("SwapSizeShort", arg.SwapSizeShort, _stream);
		_stream << ';';
		LrpWriteNullDouble("SwapSizeLong", arg.SwapSizeLong, _stream);
		_stream << ';';
		LrpWriteBoolean("IsTradeEnabled", arg.IsTradeEnabled, _stream);
		_stream << ';';
		LrpWriteInt32("GroupSortOrder", arg.GroupSortOrder, _stream);
		_stream << ';';
		LrpWriteInt32("SortOrder", arg.SortOrder, _stream);
		_stream << ';';
		LrpWriteInt32("CurrencySortOrder", arg.CurrencySortOrder, _stream);
		_stream << ';';
		LrpWriteInt32("SettlementCurrencySortOrder", arg.SettlementCurrencySortOrder, _stream);
		_stream << ';';
		LrpWriteInt32("CurrencyPrecision", arg.CurrencyPrecision, _stream);
		_stream << ';';
		LrpWriteInt32("SettlementCurrencyPrecision", arg.SettlementCurrencyPrecision, _stream);
		_stream << ';';
		_stream<<"}";
	}
	void LrpWriteAStringArray(const char* name, const std::vector<std::string>& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream << "[" << arg.size() << "]{";
		auto it = LrpBeginIterator(arg);
		auto end = LrpEndIterator(arg);
		if (it != end)
		{
			LrpWriteAString(nullptr, *it, _stream);
			_stream << ";";
			++it;
		}
		for (; it != end; ++it)
		{
			_stream << " ";
			LrpWriteAString(nullptr, *it, _stream);
			_stream << ";";
		}
		_stream << "}";
	}
	void LrpWriteUInt8Array(const char* name, const std::vector<unsigned __int8>& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream << "[" << arg.size() << "]{";
		auto it = LrpBeginIterator(arg);
		auto end = LrpEndIterator(arg);
		if (it != end)
		{
			LrpWriteUInt8(nullptr, *it, _stream);
			_stream << ";";
			++it;
		}
		for (; it != end; ++it)
		{
			_stream << " ";
			LrpWriteUInt8(nullptr, *it, _stream);
			_stream << ";";
		}
		_stream << "}";
	}
	void LrpWriteSymbolInfoArray(const char* name, const std::vector<CFxSymbolInfo>& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream << "[" << arg.size() << "]{";
		auto it = LrpBeginIterator(arg);
		auto end = LrpEndIterator(arg);
		if (it != end)
		{
			LrpWriteSymbolInfo(nullptr, *it, _stream);
			_stream << ";";
			++it;
		}
		for (; it != end; ++it)
		{
			_stream << " ";
			LrpWriteSymbolInfo(nullptr, *it, _stream);
			_stream << ";";
		}
		_stream << "}";
	}
	void LrpWriteCurrencyInfoArray(const char* name, const std::vector<CFxCurrencyInfo>& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream << "[" << arg.size() << "]{";
		auto it = LrpBeginIterator(arg);
		auto end = LrpEndIterator(arg);
		if (it != end)
		{
			LrpWriteCurrencyInfo(nullptr, *it, _stream);
			_stream << ";";
			++it;
		}
		for (; it != end; ++it)
		{
			_stream << " ";
			LrpWriteCurrencyInfo(nullptr, *it, _stream);
			_stream << ";";
		}
		_stream << "}";
	}
	void LrpWriteSymbolInfoArray2(const char* name, const std::vector<CFxSymbolInfo>& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream << "[" << arg.size() << "]{";
		auto it = LrpBeginIterator(arg);
		auto end = LrpEndIterator(arg);
		if (it != end)
		{
			LrpWriteSymbolInfo2(nullptr, *it, _stream);
			_stream << ";";
			++it;
		}
		for (; it != end; ++it)
		{
			_stream << " ";
			LrpWriteSymbolInfo2(nullptr, *it, _stream);
			_stream << ";";
		}
		_stream << "}";
	}
	void LrpWriteSymbolInfoArray3(const char* name, const std::vector<CFxSymbolInfo>& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream << "[" << arg.size() << "]{";
		auto it = LrpBeginIterator(arg);
		auto end = LrpEndIterator(arg);
		if (it != end)
		{
			LrpWriteSymbolInfo3(nullptr, *it, _stream);
			_stream << ";";
			++it;
		}
		for (; it != end; ++it)
		{
			_stream << " ";
			LrpWriteSymbolInfo3(nullptr, *it, _stream);
			_stream << ";";
		}
		_stream << "}";
	}
	void LrpWriteSymbolInfoArray4(const char* name, const std::vector<CFxSymbolInfo>& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream << "[" << arg.size() << "]{";
		auto it = LrpBeginIterator(arg);
		auto end = LrpEndIterator(arg);
		if (it != end)
		{
			LrpWriteSymbolInfo4(nullptr, *it, _stream);
			_stream << ";";
			++it;
		}
		for (; it != end; ++it)
		{
			_stream << " ";
			LrpWriteSymbolInfo4(nullptr, *it, _stream);
			_stream << ";";
		}
		_stream << "}";
	}
	void LrpWriteSymbolInfoArray5(const char* name, const std::vector<CFxSymbolInfo>& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream << "[" << arg.size() << "]{";
		auto it = LrpBeginIterator(arg);
		auto end = LrpEndIterator(arg);
		if (it != end)
		{
			LrpWriteSymbolInfo5(nullptr, *it, _stream);
			_stream << ";";
			++it;
		}
		for (; it != end; ++it)
		{
			_stream << " ";
			LrpWriteSymbolInfo5(nullptr, *it, _stream);
			_stream << ";";
		}
		_stream << "}";
	}
	void LrpWriteSymbolInfoArray6(const char* name, const std::vector<CFxSymbolInfo>& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream << "[" << arg.size() << "]{";
		auto it = LrpBeginIterator(arg);
		auto end = LrpEndIterator(arg);
		if (it != end)
		{
			LrpWriteSymbolInfo6(nullptr, *it, _stream);
			_stream << ";";
			++it;
		}
		for (; it != end; ++it)
		{
			_stream << " ";
			LrpWriteSymbolInfo6(nullptr, *it, _stream);
			_stream << ";";
		}
		_stream << "}";
	}
	void LrpWriteSymbolInfoArray7(const char* name, const std::vector<CFxSymbolInfo>& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream << "[" << arg.size() << "]{";
		auto it = LrpBeginIterator(arg);
		auto end = LrpEndIterator(arg);
		if (it != end)
		{
			LrpWriteSymbolInfo7(nullptr, *it, _stream);
			_stream << ";";
			++it;
		}
		for (; it != end; ++it)
		{
			_stream << " ";
			LrpWriteSymbolInfo7(nullptr, *it, _stream);
			_stream << ";";
		}
		_stream << "}";
	}
	void LrpWriteQuoteEntry(const char* name, const CFxQuoteEntry& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream<<"{";
		LrpWriteDouble("Price", arg.Price, _stream);
		_stream << ';';
		LrpWriteDouble("Volume", arg.Volume, _stream);
		_stream << ';';
		_stream<<"}";
	}
	void LrpWriteQuoteEntryArray(const char* name, const std::vector<CFxQuoteEntry>& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream << "[" << arg.size() << "]{";
		auto it = LrpBeginIterator(arg);
		auto end = LrpEndIterator(arg);
		if (it != end)
		{
			LrpWriteQuoteEntry(nullptr, *it, _stream);
			_stream << ";";
			++it;
		}
		for (; it != end; ++it)
		{
			_stream << " ";
			LrpWriteQuoteEntry(nullptr, *it, _stream);
			_stream << ";";
		}
		_stream << "}";
	}
	void LrpWriteQuote(const char* name, const CFxQuote& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream<<"{";
		LrpWriteAString("Symbol", arg.Symbol, _stream);
		_stream << ';';
		LrpWriteTime("CreatingTime", arg.CreatingTime, _stream);
		_stream << ';';
		LrpWriteQuoteEntryArray("Bids", arg.Bids, _stream);
		_stream << ';';
		LrpWriteQuoteEntryArray("Asks", arg.Asks, _stream);
		_stream << ';';
		LrpWriteAString("Id", arg.Id, _stream);
		_stream << ';';
		_stream<<"}";
	}
	void LrpWriteQuoteArray(const char* name, const std::vector<CFxQuote>& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream << "[" << arg.size() << "]{";
		auto it = LrpBeginIterator(arg);
		auto end = LrpEndIterator(arg);
		if (it != end)
		{
			LrpWriteQuote(nullptr, *it, _stream);
			_stream << ";";
			++it;
		}
		for (; it != end; ++it)
		{
			_stream << " ";
			LrpWriteQuote(nullptr, *it, _stream);
			_stream << ";";
		}
		_stream << "}";
	}
	void LrpWriteBar(const char* name, const CFxBar& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream<<"{";
		LrpWriteDouble("Open", arg.Open, _stream);
		_stream << ';';
		LrpWriteDouble("Close", arg.Close, _stream);
		_stream << ';';
		LrpWriteDouble("High", arg.High, _stream);
		_stream << ';';
		LrpWriteDouble("Low", arg.Low, _stream);
		_stream << ';';
		LrpWriteDouble("Volume", arg.Volume, _stream);
		_stream << ';';
		LrpWriteTime("From", arg.From, _stream);
		_stream << ';';
		_stream<<"}";
	}
	void LrpWriteBarArray(const char* name, const std::vector<CFxBar>& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream << "[" << arg.size() << "]{";
		auto it = LrpBeginIterator(arg);
		auto end = LrpEndIterator(arg);
		if (it != end)
		{
			LrpWriteBar(nullptr, *it, _stream);
			_stream << ";";
			++it;
		}
		for (; it != end; ++it)
		{
			_stream << " ";
			LrpWriteBar(nullptr, *it, _stream);
			_stream << ";";
		}
		_stream << "}";
	}
	void LrpWriteDataHistoryResponse(const char* name, const CFxDataHistoryResponse& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream<<"{";
		LrpWriteTime("FromAll", arg.FromAll, _stream);
		_stream << ';';
		LrpWriteTime("ToAll", arg.ToAll, _stream);
		_stream << ';';
		LrpWriteTime("From", arg.From, _stream);
		_stream << ';';
		LrpWriteTime("To", arg.To, _stream);
		_stream << ';';
		LrpWriteAString("LastTickId", arg.LastTickId, _stream);
		_stream << ';';
		LrpWriteBarArray("Bars", arg.Bars, _stream);
		_stream << ';';
		LrpWriteAStringArray("Files", arg.Files, _stream);
		_stream << ';';
		_stream<<"}";
	}
	void LrpWriteFileChunk(const char* name, const CFxFileChunk& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream<<"{";
		LrpWriteAString("FileId", arg.FileId, _stream);
		_stream << ';';
		LrpWriteInt32("ChunkId", arg.ChunkId, _stream);
		_stream << ';';
		LrpWriteInt32("TotalChunks", arg.TotalChunks, _stream);
		_stream << ';';
		LrpWriteInt32("FileSize", arg.FileSize, _stream);
		_stream << ';';
		LrpWriteUInt8Array("Data", arg.Data, _stream);
		_stream << ';';
		_stream<<"}";
	}
	void LrpWriteNotification(const char* name, const CNotification& arg, std::ostream& _stream)
	{
		if (nullptr != name)
		{
			_stream << name << " = ";
		}
		_stream<<"{";
		LrpWriteSeverity("Severity", arg.Severity, _stream);
		_stream << ';';
		LrpWriteNotificationType("Type", arg.Type, _stream);
		_stream << ';';
		LrpWriteAString("Text", arg.Text, _stream);
		_stream << ';';
		LrpWriteDouble("Balance", arg.Balance, _stream);
		_stream << ';';
		LrpWriteDouble("TransactionAmount", arg.TransactionAmount, _stream);
		_stream << ';';
		LrpWriteAString("TransactionCurrency", arg.TransactionCurrency, _stream);
		_stream << ';';
		_stream<<"}";
	}
}
Client::Client(ILrpTextStream* pStream) : m_stream(pStream)
{
}
void Client::OnHeartBeatRequest()
{
	std::stringstream _stream;
	_stream << "[0]Client[0]OnHeartBeatRequest(";
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Client::OnHeartBeatResponse()
{
	std::stringstream _stream;
	_stream << "[0]Client[1]OnHeartBeatResponse(";
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Client::OnLogonMsg(const std::string& protocolVersion)
{
	std::stringstream _stream;
	_stream << "[0]Client[2]OnLogonMsg(";
	LrpWriteAString("protocolVersion", protocolVersion, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Client::OnLogoutMsg(const FxLogoutReason& reason, const std::string& description)
{
	std::stringstream _stream;
	_stream << "[0]Client[3]OnLogoutMsg(";
	LrpWriteLogoutReason("reason", reason, _stream);
	_stream<<", ";
	LrpWriteAString("description", description, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Client::OnSessionInfoMsg(const std::string& requestId, const CFxSessionInfo& sessionInfo)
{
	std::stringstream _stream;
	_stream << "[0]Client[4]OnSessionInfoMsg(";
	LrpWriteAString("requestId", requestId, _stream);
	_stream<<", ";
	LrpWriteLrpSessionInfo("sessionInfo", sessionInfo, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Client::OnSessionInfoMsg2(const std::string& requestId, const CFxSessionInfo& sessionInfo)
{
	std::stringstream _stream;
	_stream << "[0]Client[5]OnSessionInfoMsg2(";
	LrpWriteAString("requestId", requestId, _stream);
	_stream<<", ";
	LrpWriteLrpSessionInfo2("sessionInfo", sessionInfo, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Client::OnCurrenciesInfoMsg(const std::string& requestId, const std::vector<CFxCurrencyInfo>& currencies)
{
	std::stringstream _stream;
	_stream << "[0]Client[6]OnCurrenciesInfoMsg(";
	LrpWriteAString("requestId", requestId, _stream);
	_stream<<", ";
	LrpWriteCurrencyInfoArray("currencies", currencies, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Client::OnSymbolsInfoMsg(const std::string& requestId, const std::vector<CFxSymbolInfo>& symbols)
{
	std::stringstream _stream;
	_stream << "[0]Client[7]OnSymbolsInfoMsg(";
	LrpWriteAString("requestId", requestId, _stream);
	_stream<<", ";
	LrpWriteSymbolInfoArray("symbols", symbols, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Client::OnSymbolsInfoMsg2(const std::string& requestId, const std::vector<CFxSymbolInfo>& symbols)
{
	std::stringstream _stream;
	_stream << "[0]Client[8]OnSymbolsInfoMsg2(";
	LrpWriteAString("requestId", requestId, _stream);
	_stream<<", ";
	LrpWriteSymbolInfoArray2("symbols", symbols, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Client::OnSymbolsInfoMsg3(const std::string& requestId, const std::vector<CFxSymbolInfo>& symbols)
{
	std::stringstream _stream;
	_stream << "[0]Client[9]OnSymbolsInfoMsg3(";
	LrpWriteAString("requestId", requestId, _stream);
	_stream<<", ";
	LrpWriteSymbolInfoArray3("symbols", symbols, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Client::OnSymbolsInfoMsg4(const std::string& requestId, const std::vector<CFxSymbolInfo>& symbols)
{
	std::stringstream _stream;
	_stream << "[0]Client[10]OnSymbolsInfoMsg4(";
	LrpWriteAString("requestId", requestId, _stream);
	_stream<<", ";
	LrpWriteSymbolInfoArray4("symbols", symbols, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Client::OnSymbolsInfoMsg5(const std::string& requestId, const std::vector<CFxSymbolInfo>& symbols)
{
	std::stringstream _stream;
	_stream << "[0]Client[11]OnSymbolsInfoMsg5(";
	LrpWriteAString("requestId", requestId, _stream);
	_stream<<", ";
	LrpWriteSymbolInfoArray5("symbols", symbols, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Client::OnSymbolsInfoMsg6(const std::string& requestId, const std::vector<CFxSymbolInfo>& symbols)
{
	std::stringstream _stream;
	_stream << "[0]Client[12]OnSymbolsInfoMsg6(";
	LrpWriteAString("requestId", requestId, _stream);
	_stream<<", ";
	LrpWriteSymbolInfoArray6("symbols", symbols, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Client::OnSymbolsInfoMsg7(const std::string& requestId, const std::vector<CFxSymbolInfo>& symbols)
{
	std::stringstream _stream;
	_stream << "[0]Client[13]OnSymbolsInfoMsg7(";
	LrpWriteAString("requestId", requestId, _stream);
	_stream<<", ";
	LrpWriteSymbolInfoArray7("symbols", symbols, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Client::OnComponentsInfoMsg(const std::string& requestId, const __int32& quotesStorageVersion)
{
	std::stringstream _stream;
	_stream << "[0]Client[14]OnComponentsInfoMsg(";
	LrpWriteAString("requestId", requestId, _stream);
	_stream<<", ";
	LrpWriteInt32("quotesStorageVersion", quotesStorageVersion, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Client::OnQuotesSubscriptionMsg(const std::string& requestId, const __int32& status, const std::string& message)
{
	std::stringstream _stream;
	_stream << "[0]Client[15]OnQuotesSubscriptionMsg(";
	LrpWriteAString("requestId", requestId, _stream);
	_stream<<", ";
	LrpWriteInt32("status", status, _stream);
	_stream<<", ";
	LrpWriteAString("message", message, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Client::OnFileChunkMsg(const std::string& requestId, const CFxFileChunk& chunk)
{
	std::stringstream _stream;
	_stream << "[0]Client[16]OnFileChunkMsg(";
	LrpWriteAString("requestId", requestId, _stream);
	_stream<<", ";
	LrpWriteFileChunk("chunk", chunk, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Client::OnDataHistoryMetaInfoResponseMsg(const std::string& requestId, const __int32& status, const std::string& field)
{
	std::stringstream _stream;
	_stream << "[0]Client[17]OnDataHistoryMetaInfoResponseMsg(";
	LrpWriteAString("requestId", requestId, _stream);
	_stream<<", ";
	LrpWriteInt32("status", status, _stream);
	_stream<<", ";
	LrpWriteAString("field", field, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Client::OnDataHistoryMetaInfoRejectMsg(const std::string& requestId, const __int32& status, const std::string& field)
{
	std::stringstream _stream;
	_stream << "[0]Client[18]OnDataHistoryMetaInfoRejectMsg(";
	LrpWriteAString("requestId", requestId, _stream);
	_stream<<", ";
	LrpWriteInt32("status", status, _stream);
	_stream<<", ";
	LrpWriteAString("field", field, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Client::OnDataHistoryResponseMsg(const std::string& requestId, const CFxDataHistoryResponse& response)
{
	std::stringstream _stream;
	_stream << "[0]Client[19]OnDataHistoryResponseMsg(";
	LrpWriteAString("requestId", requestId, _stream);
	_stream<<", ";
	LrpWriteDataHistoryResponse("response", response, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Client::OnDataHistoryRejectMsg(const std::string& requestId, const FxMarketHistoryRejectType& rejectType, const std::string& rejectReason)
{
	std::stringstream _stream;
	_stream << "[0]Client[20]OnDataHistoryRejectMsg(";
	LrpWriteAString("requestId", requestId, _stream);
	_stream<<", ";
	LrpWriteMarketHistoryRejectType("rejectType", rejectType, _stream);
	_stream<<", ";
	LrpWriteAString("rejectReason", rejectReason, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Client::OnQuoteRawMsg(const MemoryBuffer& data)
{
	std::stringstream _stream;
	_stream << "[0]Client[21]OnQuoteRawMsg(";
	LrpWriteRaw("data", data, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Client::OnNotificationMsg(const CNotification& notification)
{
	std::stringstream _stream;
	_stream << "[0]Client[22]OnNotificationMsg(";
	LrpWriteNotification("notification", notification, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void Client::OnBusinessRejectMsg(const std::string& rejectReason, const std::string& rejectTag)
{
	std::stringstream _stream;
	_stream << "[0]Client[23]OnBusinessRejectMsg(";
	LrpWriteAString("rejectReason", rejectReason, _stream);
	_stream<<", ";
	LrpWriteAString("rejectTag", rejectTag, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
SimpleCodec::SimpleCodec(ILrpTextStream* pStream) : m_stream(pStream)
{
}
void SimpleCodec::OnSymbolIndexMsg(const std::string& symbol, const unsigned __int32& index)
{
	std::stringstream _stream;
	_stream << "[1]SimpleCodec[0]OnSymbolIndexMsg(";
	LrpWriteAString("symbol", symbol, _stream);
	_stream<<", ";
	LrpWriteUInt32("index", index, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void SimpleCodec::OnTimeSynchMsg(const CDateTime& time)
{
	std::stringstream _stream;
	_stream << "[1]SimpleCodec[1]OnTimeSynchMsg(";
	LrpWriteTime("time", time, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void SimpleCodec::OnSymbol8Time32Level2Msg(const unsigned __int8& symbolIndex, const __int32& timeOffset, const MemoryBuffer& data)
{
	std::stringstream _stream;
	_stream << "[1]SimpleCodec[2]OnSymbol8Time32Level2Msg(";
	LrpWriteUInt8("symbolIndex", symbolIndex, _stream);
	_stream<<", ";
	LrpWriteInt32("timeOffset", timeOffset, _stream);
	_stream<<", ";
	LrpWriteRaw("data", data, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
void SimpleCodec::OnQuoteZipRawMsg(const MemoryBuffer& data)
{
	std::stringstream _stream;
	_stream << "[1]SimpleCodec[3]OnQuoteZipRawMsg(";
	LrpWriteRaw("data", data, _stream);
	_stream << ");";
	m_stream->Write(_stream.str());
}
