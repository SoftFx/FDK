// This is always generated file. Do not change anything.


// handlers of Handle component
namespace
{
	const unsigned short LrpComponent_Handle_Id = 0;
	const unsigned short LrpMethod_Handle_Delete_Id = 0;
	const unsigned short LrpMethod_Handle_NotificationFromHandle_Id = 1;

	typedef void (*LrpInvoke_Handle_Method_Handler)(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel);
	void LrpInvoke_Handle_Delete(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetHandle();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		component.Delete(arg0);
		buffer.Reset(offset);
	}
	void LrpInvoke_Handle_NotificationFromHandle(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetHandle();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.NotificationFromHandle(arg0);
		buffer.Reset(offset);
		WriteNotification(result, buffer);
	}

	LrpInvoke_Handle_Method_Handler gHandleHandlers[] = 
	{
		LrpInvoke_Handle_Delete,
		LrpInvoke_Handle_NotificationFromHandle,
	};

	HRESULT LrpInvoke_Handle(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		if(methodId >= 2)
		{
			return LRP_INVALID_METHOD_ID;
		}
		gHandleHandlers[methodId](offset, buffer, pChannel); 
		return S_OK;
	}
}


// handlers of Converter component
namespace
{
	const unsigned short LrpComponent_Converter_Id = 1;
	const unsigned short LrpMethod_Converter_CurrenciesFromHandle_Id = 0;
	const unsigned short LrpMethod_Converter_SymbolFromHandle_Id = 1;
	const unsigned short LrpMethod_Converter_SymbolsFromHandle_Id = 2;
	const unsigned short LrpMethod_Converter_TwoFactorAuthFromHandle_Id = 3;
	const unsigned short LrpMethod_Converter_SessionInfoFromHandle_Id = 4;
	const unsigned short LrpMethod_Converter_NotificationFromHandle_Id = 5;
	const unsigned short LrpMethod_Converter_QuoteFromHandle_Id = 6;
	const unsigned short LrpMethod_Converter_ProtocolVersionFromHandle_Id = 7;
	const unsigned short LrpMethod_Converter_AccountInfoFromHandle_Id = 8;
	const unsigned short LrpMethod_Converter_PositionFromHandle_Id = 9;
	const unsigned short LrpMethod_Converter_TradeTransactionReportFromHandle_Id = 10;
	const unsigned short LrpMethod_Converter_ExecutionReportFromHandle_Id = 11;
	const unsigned short LrpMethod_Converter_GetLogoutInfoFromHandle_Id = 12;

	typedef void (*LrpInvoke_Converter_Method_Handler)(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel);
	void LrpInvoke_Converter_CurrenciesFromHandle(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetConverter();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.CurrenciesFromHandle(arg0);
		buffer.Reset(offset);
		WriteCurrencyInfoArray(result, buffer);
	}
	void LrpInvoke_Converter_SymbolFromHandle(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetConverter();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.SymbolFromHandle(arg0);
		buffer.Reset(offset);
		WriteAString(result, buffer);
	}
	void LrpInvoke_Converter_SymbolsFromHandle(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetConverter();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.SymbolsFromHandle(arg0);
		buffer.Reset(offset);
		WriteSymbolInfoArray(result, buffer);
	}
	void LrpInvoke_Converter_TwoFactorAuthFromHandle(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetConverter();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.TwoFactorAuthFromHandle(arg0);
		buffer.Reset(offset);
		WriteTwoFactorAuth(result, buffer);
	}
	void LrpInvoke_Converter_SessionInfoFromHandle(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetConverter();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.SessionInfoFromHandle(arg0);
		buffer.Reset(offset);
		WriteSessionInfo(result, buffer);
	}
	void LrpInvoke_Converter_NotificationFromHandle(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetConverter();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.NotificationFromHandle(arg0);
		buffer.Reset(offset);
		WriteNotification(result, buffer);
	}
	void LrpInvoke_Converter_QuoteFromHandle(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetConverter();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.QuoteFromHandle(arg0);
		buffer.Reset(offset);
		WriteQuote(result, buffer);
	}
	void LrpInvoke_Converter_ProtocolVersionFromHandle(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetConverter();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.ProtocolVersionFromHandle(arg0);
		buffer.Reset(offset);
		WriteAString(result, buffer);
	}
	void LrpInvoke_Converter_AccountInfoFromHandle(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetConverter();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.AccountInfoFromHandle(arg0);
		buffer.Reset(offset);
		WriteAccountInfo(result, buffer);
	}
	void LrpInvoke_Converter_PositionFromHandle(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetConverter();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.PositionFromHandle(arg0);
		buffer.Reset(offset);
		WritePosition(result, buffer);
	}
	void LrpInvoke_Converter_TradeTransactionReportFromHandle(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetConverter();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.TradeTransactionReportFromHandle(arg0);
		buffer.Reset(offset);
		WriteTradeTransactionReport(result, buffer);
	}
	void LrpInvoke_Converter_ExecutionReportFromHandle(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetConverter();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.ExecutionReportFromHandle(arg0);
		buffer.Reset(offset);
		WriteExecutionReport(result, buffer);
	}
	void LrpInvoke_Converter_GetLogoutInfoFromHandle(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetConverter();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = std::string();
		auto arg2 = FxLogoutReason();
		auto arg3 = __int32();
		component.GetLogoutInfoFromHandle(arg0, arg1, arg2, arg3);
		buffer.Reset(offset);
		WriteAString(arg1, buffer);
		WriteLogoutReason(arg2, buffer);
		WriteInt32(arg3, buffer);
	}

	LrpInvoke_Converter_Method_Handler gConverterHandlers[] = 
	{
		LrpInvoke_Converter_CurrenciesFromHandle,
		LrpInvoke_Converter_SymbolFromHandle,
		LrpInvoke_Converter_SymbolsFromHandle,
		LrpInvoke_Converter_TwoFactorAuthFromHandle,
		LrpInvoke_Converter_SessionInfoFromHandle,
		LrpInvoke_Converter_NotificationFromHandle,
		LrpInvoke_Converter_QuoteFromHandle,
		LrpInvoke_Converter_ProtocolVersionFromHandle,
		LrpInvoke_Converter_AccountInfoFromHandle,
		LrpInvoke_Converter_PositionFromHandle,
		LrpInvoke_Converter_TradeTransactionReportFromHandle,
		LrpInvoke_Converter_ExecutionReportFromHandle,
		LrpInvoke_Converter_GetLogoutInfoFromHandle,
	};

	HRESULT LrpInvoke_Converter(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		if(methodId >= 13)
		{
			return LRP_INVALID_METHOD_ID;
		}
		gConverterHandlers[methodId](offset, buffer, pChannel); 
		return S_OK;
	}
}


// handlers of Params component
namespace
{
	const unsigned short LrpComponent_Params_Id = 2;
	const unsigned short LrpMethod_Params_Create_Id = 0;
	const unsigned short LrpMethod_Params_SetInt32_Id = 1;
	const unsigned short LrpMethod_Params_SetDouble_Id = 2;
	const unsigned short LrpMethod_Params_SetBoolean_Id = 3;
	const unsigned short LrpMethod_Params_SetString_Id = 4;
	const unsigned short LrpMethod_Params_ToText_Id = 5;

	typedef void (*LrpInvoke_Params_Method_Handler)(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel);
	void LrpInvoke_Params_Create(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetParams();
		component; // if all methods of component are static then the next line generates warning #4189
		auto result = component.Create();
		buffer.Reset(offset);
		WriteLocalPointer(result, buffer);
	}
	void LrpInvoke_Params_SetInt32(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetParams();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadAString(buffer);
		auto arg2 = ReadInt32(buffer);
		component.SetInt32(arg0, arg1, arg2);
		buffer.Reset(offset);
	}
	void LrpInvoke_Params_SetDouble(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetParams();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadAString(buffer);
		auto arg2 = ReadDouble(buffer);
		component.SetDouble(arg0, arg1, arg2);
		buffer.Reset(offset);
	}
	void LrpInvoke_Params_SetBoolean(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetParams();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadAString(buffer);
		auto arg2 = ReadBoolean(buffer);
		component.SetBoolean(arg0, arg1, arg2);
		buffer.Reset(offset);
	}
	void LrpInvoke_Params_SetString(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetParams();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadAString(buffer);
		auto arg2 = ReadAString(buffer);
		component.SetString(arg0, arg1, arg2);
		buffer.Reset(offset);
	}
	void LrpInvoke_Params_ToText(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetParams();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.ToText(arg0);
		buffer.Reset(offset);
		WriteAString(result, buffer);
	}

	LrpInvoke_Params_Method_Handler gParamsHandlers[] = 
	{
		LrpInvoke_Params_Create,
		LrpInvoke_Params_SetInt32,
		LrpInvoke_Params_SetDouble,
		LrpInvoke_Params_SetBoolean,
		LrpInvoke_Params_SetString,
		LrpInvoke_Params_ToText,
	};

	HRESULT LrpInvoke_Params(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		if(methodId >= 6)
		{
			return LRP_INVALID_METHOD_ID;
		}
		gParamsHandlers[methodId](offset, buffer, pChannel); 
		return S_OK;
	}
}


// handlers of ClientServer component
namespace
{
	const unsigned short LrpComponent_ClientServer_Id = 3;
	const unsigned short LrpMethod_ClientServer_Start_Id = 0;
	const unsigned short LrpMethod_ClientServer_WaitForLogon_Id = 1;
	const unsigned short LrpMethod_ClientServer_Dispose_Id = 2;
	const unsigned short LrpMethod_ClientServer_Shutdown_Id = 3;
	const unsigned short LrpMethod_ClientServer_Stop_Id = 4;
	const unsigned short LrpMethod_ClientServer_NextId_Id = 5;
	const unsigned short LrpMethod_ClientServer_GetProtocolVersion_Id = 6;
	const unsigned short LrpMethod_ClientServer_GetNextMessage_Id = 7;
	const unsigned short LrpMethod_ClientServer_DispatchMessage_Id = 8;
	const unsigned short LrpMethod_ClientServer_GetNetworkActivity_Id = 9;
	const unsigned short LrpMethod_ClientServer_SendTwoFactorResponse_Id = 10;
	const unsigned short LrpMethod_ClientServer_GetSessionInfo_Id = 11;
	const unsigned short LrpMethod_ClientServer_GetFileChunk_Id = 12;

	typedef void (*LrpInvoke_ClientServer_Method_Handler)(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel);
	void LrpInvoke_ClientServer_Start(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClientServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.Start(arg0);
		buffer.Reset(offset);
		WriteBoolean(result, buffer);
	}
	void LrpInvoke_ClientServer_WaitForLogon(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClientServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadUInt32(buffer);
		auto result = component.WaitForLogon(arg0, arg1);
		buffer.Reset(offset);
		WriteBoolean(result, buffer);
	}
	void LrpInvoke_ClientServer_Dispose(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClientServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.Dispose(arg0);
		buffer.Reset(offset);
		WriteInt32(result, buffer);
	}
	void LrpInvoke_ClientServer_Shutdown(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClientServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.Shutdown(arg0);
		buffer.Reset(offset);
		WriteInt32(result, buffer);
	}
	void LrpInvoke_ClientServer_Stop(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClientServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.Stop(arg0);
		buffer.Reset(offset);
		WriteInt32(result, buffer);
	}
	void LrpInvoke_ClientServer_NextId(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClientServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.NextId(arg0);
		buffer.Reset(offset);
		WriteAString(result, buffer);
	}
	void LrpInvoke_ClientServer_GetProtocolVersion(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClientServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.GetProtocolVersion(arg0);
		buffer.Reset(offset);
		WriteAString(result, buffer);
	}
	void LrpInvoke_ClientServer_GetNextMessage(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClientServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = CFxMessage();
		auto result = component.GetNextMessage(arg0, arg1);
		buffer.Reset(offset);
		WriteMessage(arg1, buffer);
		WriteBoolean(result, buffer);
	}
	void LrpInvoke_ClientServer_DispatchMessage(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClientServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadMessage(buffer);
		component.DispatchMessage(arg0, arg1);
		buffer.Reset(offset);
	}
	void LrpInvoke_ClientServer_GetNetworkActivity(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClientServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = unsigned __int64();
		auto arg2 = unsigned __int64();
		auto arg3 = unsigned __int64();
		auto arg4 = unsigned __int64();
		component.GetNetworkActivity(arg0, arg1, arg2, arg3, arg4);
		buffer.Reset(offset);
		WriteUInt64(arg1, buffer);
		WriteUInt64(arg2, buffer);
		WriteUInt64(arg3, buffer);
		WriteUInt64(arg4, buffer);
	}
	void LrpInvoke_ClientServer_SendTwoFactorResponse(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClientServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadTwoFactorReason(buffer);
		auto arg2 = ReadAString(buffer);
		component.SendTwoFactorResponse(arg0, arg1, arg2);
		buffer.Reset(offset);
	}
	void LrpInvoke_ClientServer_GetSessionInfo(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClientServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadUInt32(buffer);
		auto result = component.GetSessionInfo(arg0, arg1);
		buffer.Reset(offset);
		WriteSessionInfo(result, buffer);
	}
	void LrpInvoke_ClientServer_GetFileChunk(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClientServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadAString(buffer);
		auto arg2 = ReadInt32(buffer);
		auto arg3 = ReadUInt32(buffer);
		auto result = component.GetFileChunk(arg0, arg1, arg2, arg3);
		buffer.Reset(offset);
		WriteFileChunk(result, buffer);
	}

	LrpInvoke_ClientServer_Method_Handler gClientServerHandlers[] = 
	{
		LrpInvoke_ClientServer_Start,
		LrpInvoke_ClientServer_WaitForLogon,
		LrpInvoke_ClientServer_Dispose,
		LrpInvoke_ClientServer_Shutdown,
		LrpInvoke_ClientServer_Stop,
		LrpInvoke_ClientServer_NextId,
		LrpInvoke_ClientServer_GetProtocolVersion,
		LrpInvoke_ClientServer_GetNextMessage,
		LrpInvoke_ClientServer_DispatchMessage,
		LrpInvoke_ClientServer_GetNetworkActivity,
		LrpInvoke_ClientServer_SendTwoFactorResponse,
		LrpInvoke_ClientServer_GetSessionInfo,
		LrpInvoke_ClientServer_GetFileChunk,
	};

	HRESULT LrpInvoke_ClientServer(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		if(methodId >= 13)
		{
			return LRP_INVALID_METHOD_ID;
		}
		gClientServerHandlers[methodId](offset, buffer, pChannel); 
		return S_OK;
	}
}


// handlers of ClientCache component
namespace
{
	const unsigned short LrpComponent_ClientCache_Id = 4;
	const unsigned short LrpMethod_ClientCache_GetSessionInfo_Id = 0;

	typedef void (*LrpInvoke_ClientCache_Method_Handler)(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel);
	void LrpInvoke_ClientCache_GetSessionInfo(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClientCache();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.GetSessionInfo(arg0);
		buffer.Reset(offset);
		WriteSessionInfo(result, buffer);
	}

	LrpInvoke_ClientCache_Method_Handler gClientCacheHandlers[] = 
	{
		LrpInvoke_ClientCache_GetSessionInfo,
	};

	HRESULT LrpInvoke_ClientCache(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		if(methodId >= 1)
		{
			return LRP_INVALID_METHOD_ID;
		}
		gClientCacheHandlers[methodId](offset, buffer, pChannel); 
		return S_OK;
	}
}


// handlers of FeedServer component
namespace
{
	const unsigned short LrpComponent_FeedServer_Id = 5;
	const unsigned short LrpMethod_FeedServer_Create_Id = 0;
	const unsigned short LrpMethod_FeedServer_GetQuoteHistoryFiles_Id = 1;
	const unsigned short LrpMethod_FeedServer_GetBarsHistoryFiles_Id = 2;
	const unsigned short LrpMethod_FeedServer_GetCurrencies_Id = 3;
	const unsigned short LrpMethod_FeedServer_GetSymbols_Id = 4;
	const unsigned short LrpMethod_FeedServer_SubscribeToQuotes_Id = 5;
	const unsigned short LrpMethod_FeedServer_UnsubscribeQuotes_Id = 6;
	const unsigned short LrpMethod_FeedServer_GetBarsHistoryMetaInfoFile_Id = 7;
	const unsigned short LrpMethod_FeedServer_GetQuotesHistoryMetaInfoFile_Id = 8;
	const unsigned short LrpMethod_FeedServer_GetHistoryBars_Id = 9;
	const unsigned short LrpMethod_FeedServer_GetQueueThreshold_Id = 10;
	const unsigned short LrpMethod_FeedServer_SetQueueThreshold_Id = 11;
	const unsigned short LrpMethod_FeedServer_GetQuotesHistoryVersion_Id = 12;

	typedef void (*LrpInvoke_FeedServer_Method_Handler)(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel);
	void LrpInvoke_FeedServer_Create(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFeedServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto arg1 = ReadAString(buffer);
		auto result = component.Create(arg0, arg1);
		buffer.Reset(offset);
		WriteLocalPointer(result, buffer);
	}
	void LrpInvoke_FeedServer_GetQuoteHistoryFiles(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFeedServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadAString(buffer);
		auto arg2 = ReadBoolean(buffer);
		auto arg3 = ReadTime(buffer);
		auto arg4 = ReadUInt32(buffer);
		auto result = component.GetQuoteHistoryFiles(arg0, arg1, arg2, arg3, arg4);
		buffer.Reset(offset);
		WriteDataHistoryInfo(result, buffer);
	}
	void LrpInvoke_FeedServer_GetBarsHistoryFiles(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFeedServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadAString(buffer);
		auto arg2 = ReadPriceType(buffer);
		auto arg3 = ReadAString(buffer);
		auto arg4 = ReadTime(buffer);
		auto arg5 = ReadUInt32(buffer);
		auto result = component.GetBarsHistoryFiles(arg0, arg1, arg2, arg3, arg4, arg5);
		buffer.Reset(offset);
		WriteDataHistoryInfo(result, buffer);
	}
	void LrpInvoke_FeedServer_GetCurrencies(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFeedServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadUInt32(buffer);
		auto result = component.GetCurrencies(arg0, arg1);
		buffer.Reset(offset);
		WriteCurrencyInfoArray(result, buffer);
	}
	void LrpInvoke_FeedServer_GetSymbols(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFeedServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadUInt32(buffer);
		auto result = component.GetSymbols(arg0, arg1);
		buffer.Reset(offset);
		WriteSymbolInfoArray(result, buffer);
	}
	void LrpInvoke_FeedServer_SubscribeToQuotes(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFeedServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadStringArray(buffer);
		auto arg2 = ReadInt32(buffer);
		auto arg3 = ReadUInt32(buffer);
		component.SubscribeToQuotes(arg0, arg1, arg2, arg3);
		buffer.Reset(offset);
	}
	void LrpInvoke_FeedServer_UnsubscribeQuotes(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFeedServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadStringArray(buffer);
		auto arg2 = ReadUInt32(buffer);
		component.UnsubscribeQuotes(arg0, arg1, arg2);
		buffer.Reset(offset);
	}
	void LrpInvoke_FeedServer_GetBarsHistoryMetaInfoFile(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFeedServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadAString(buffer);
		auto arg2 = ReadPriceType(buffer);
		auto arg3 = ReadAString(buffer);
		auto arg4 = ReadUInt32(buffer);
		auto result = component.GetBarsHistoryMetaInfoFile(arg0, arg1, arg2, arg3, arg4);
		buffer.Reset(offset);
		WriteAString(result, buffer);
	}
	void LrpInvoke_FeedServer_GetQuotesHistoryMetaInfoFile(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFeedServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadAString(buffer);
		auto arg2 = ReadBoolean(buffer);
		auto arg3 = ReadUInt32(buffer);
		auto result = component.GetQuotesHistoryMetaInfoFile(arg0, arg1, arg2, arg3);
		buffer.Reset(offset);
		WriteAString(result, buffer);
	}
	void LrpInvoke_FeedServer_GetHistoryBars(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFeedServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadAString(buffer);
		auto arg2 = ReadTime(buffer);
		auto arg3 = ReadInt32(buffer);
		auto arg4 = ReadPriceType(buffer);
		auto arg5 = ReadAString(buffer);
		auto arg6 = ReadUInt32(buffer);
		auto result = component.GetHistoryBars(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
		buffer.Reset(offset);
		WriteDataHistoryInfo(result, buffer);
	}
	void LrpInvoke_FeedServer_GetQueueThreshold(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFeedServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.GetQueueThreshold(arg0);
		buffer.Reset(offset);
		WriteInt32(result, buffer);
	}
	void LrpInvoke_FeedServer_SetQueueThreshold(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFeedServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadInt32(buffer);
		component.SetQueueThreshold(arg0, arg1);
		buffer.Reset(offset);
	}
	void LrpInvoke_FeedServer_GetQuotesHistoryVersion(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFeedServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadUInt32(buffer);
		auto result = component.GetQuotesHistoryVersion(arg0, arg1);
		buffer.Reset(offset);
		WriteInt32(result, buffer);
	}

	LrpInvoke_FeedServer_Method_Handler gFeedServerHandlers[] = 
	{
		LrpInvoke_FeedServer_Create,
		LrpInvoke_FeedServer_GetQuoteHistoryFiles,
		LrpInvoke_FeedServer_GetBarsHistoryFiles,
		LrpInvoke_FeedServer_GetCurrencies,
		LrpInvoke_FeedServer_GetSymbols,
		LrpInvoke_FeedServer_SubscribeToQuotes,
		LrpInvoke_FeedServer_UnsubscribeQuotes,
		LrpInvoke_FeedServer_GetBarsHistoryMetaInfoFile,
		LrpInvoke_FeedServer_GetQuotesHistoryMetaInfoFile,
		LrpInvoke_FeedServer_GetHistoryBars,
		LrpInvoke_FeedServer_GetQueueThreshold,
		LrpInvoke_FeedServer_SetQueueThreshold,
		LrpInvoke_FeedServer_GetQuotesHistoryVersion,
	};

	HRESULT LrpInvoke_FeedServer(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		if(methodId >= 13)
		{
			return LRP_INVALID_METHOD_ID;
		}
		gFeedServerHandlers[methodId](offset, buffer, pChannel); 
		return S_OK;
	}
}


// handlers of FeedCache component
namespace
{
	const unsigned short LrpComponent_FeedCache_Id = 6;
	const unsigned short LrpMethod_FeedCache_GetSymbols_Id = 0;
	const unsigned short LrpMethod_FeedCache_TryGetBid_Id = 1;
	const unsigned short LrpMethod_FeedCache_TryGetAsk_Id = 2;
	const unsigned short LrpMethod_FeedCache_TryGetQuote_Id = 3;
	const unsigned short LrpMethod_FeedCache_GetCurrencies_Id = 4;

	typedef void (*LrpInvoke_FeedCache_Method_Handler)(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel);
	void LrpInvoke_FeedCache_GetSymbols(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFeedCache();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.GetSymbols(arg0);
		buffer.Reset(offset);
		WriteSymbolInfoArray(result, buffer);
	}
	void LrpInvoke_FeedCache_TryGetBid(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFeedCache();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadAString(buffer);
		auto arg2 = double();
		auto arg3 = double();
		auto arg4 = CDateTime();
		auto result = component.TryGetBid(arg0, arg1, arg2, arg3, arg4);
		buffer.Reset(offset);
		WriteDouble(arg2, buffer);
		WriteDouble(arg3, buffer);
		WriteTime(arg4, buffer);
		WriteBoolean(result, buffer);
	}
	void LrpInvoke_FeedCache_TryGetAsk(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFeedCache();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadAString(buffer);
		auto arg2 = double();
		auto arg3 = double();
		auto arg4 = CDateTime();
		auto result = component.TryGetAsk(arg0, arg1, arg2, arg3, arg4);
		buffer.Reset(offset);
		WriteDouble(arg2, buffer);
		WriteDouble(arg3, buffer);
		WriteTime(arg4, buffer);
		WriteBoolean(result, buffer);
	}
	void LrpInvoke_FeedCache_TryGetQuote(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFeedCache();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadAString(buffer);
		auto arg2 = CFxQuote();
		auto result = component.TryGetQuote(arg0, arg1, arg2);
		buffer.Reset(offset);
		WriteQuote(arg2, buffer);
		WriteBoolean(result, buffer);
	}
	void LrpInvoke_FeedCache_GetCurrencies(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFeedCache();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.GetCurrencies(arg0);
		buffer.Reset(offset);
		WriteCurrencyInfoArray(result, buffer);
	}

	LrpInvoke_FeedCache_Method_Handler gFeedCacheHandlers[] = 
	{
		LrpInvoke_FeedCache_GetSymbols,
		LrpInvoke_FeedCache_TryGetBid,
		LrpInvoke_FeedCache_TryGetAsk,
		LrpInvoke_FeedCache_TryGetQuote,
		LrpInvoke_FeedCache_GetCurrencies,
	};

	HRESULT LrpInvoke_FeedCache(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		if(methodId >= 5)
		{
			return LRP_INVALID_METHOD_ID;
		}
		gFeedCacheHandlers[methodId](offset, buffer, pChannel); 
		return S_OK;
	}
}


// handlers of TradeServer component
namespace
{
	const unsigned short LrpComponent_TradeServer_Id = 7;
	const unsigned short LrpMethod_TradeServer_Create_Id = 0;
	const unsigned short LrpMethod_TradeServer_GetTradeTransactionReportsAndSubscribe_Id = 1;
	const unsigned short LrpMethod_TradeServer_GetTradeCaptureReports_Id = 2;
	const unsigned short LrpMethod_TradeServer_UnsubscribeTradeTransactionReports_Id = 3;
	const unsigned short LrpMethod_TradeServer_GetTradeServerInfo_Id = 4;
	const unsigned short LrpMethod_TradeServer_GetAccountInfo_Id = 5;
	const unsigned short LrpMethod_TradeServer_DeleteOrder_Id = 6;
	const unsigned short LrpMethod_TradeServer_CloseAllPositions_Id = 7;
	const unsigned short LrpMethod_TradeServer_CloseByPositions_Id = 8;
	const unsigned short LrpMethod_TradeServer_GetRecords_Id = 9;
	const unsigned short LrpMethod_TradeServer_OpenNewOrder_Id = 10;
	const unsigned short LrpMethod_TradeServer_ModifyOrder_Id = 11;
	const unsigned short LrpMethod_TradeServer_CloseOrder_Id = 12;

	typedef void (*LrpInvoke_TradeServer_Method_Handler)(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel);
	void LrpInvoke_TradeServer_Create(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetTradeServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto arg1 = ReadAString(buffer);
		auto result = component.Create(arg0, arg1);
		buffer.Reset(offset);
		WriteLocalPointer(result, buffer);
	}
	void LrpInvoke_TradeServer_GetTradeTransactionReportsAndSubscribe(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetTradeServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadInt32(buffer);
		auto arg2 = ReadBoolean(buffer);
		auto arg3 = ReadNullTime(buffer);
		auto arg4 = ReadNullTime(buffer);
		auto arg5 = ReadUInt32(buffer);
		auto arg6 = ReadNullInt32(buffer);
		auto arg7 = ReadUInt32(buffer);
		auto result = component.GetTradeTransactionReportsAndSubscribe(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
		buffer.Reset(offset);
		WriteLocalPointer(result, buffer);
	}
	void LrpInvoke_TradeServer_GetTradeCaptureReports(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetTradeServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadNullTime(buffer);
		auto arg2 = ReadNullTime(buffer);
		auto arg3 = ReadUInt32(buffer);
		auto result = component.GetTradeCaptureReports(arg0, arg1, arg2, arg3);
		buffer.Reset(offset);
		WriteLocalPointer(result, buffer);
	}
	void LrpInvoke_TradeServer_UnsubscribeTradeTransactionReports(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetTradeServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadUInt32(buffer);
		component.UnsubscribeTradeTransactionReports(arg0, arg1);
		buffer.Reset(offset);
	}
	void LrpInvoke_TradeServer_GetTradeServerInfo(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetTradeServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadUInt32(buffer);
		auto result = component.GetTradeServerInfo(arg0, arg1);
		buffer.Reset(offset);
		WriteTradeServerInfo(result, buffer);
	}
	void LrpInvoke_TradeServer_GetAccountInfo(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetTradeServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadUInt32(buffer);
		auto result = component.GetAccountInfo(arg0, arg1);
		buffer.Reset(offset);
		WriteAccountInfo(result, buffer);
	}
	void LrpInvoke_TradeServer_DeleteOrder(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetTradeServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadAString(buffer);
		auto arg2 = ReadAString(buffer);
		auto arg3 = ReadAString(buffer);
		auto arg4 = ReadSide(buffer);
		auto arg5 = ReadUInt32(buffer);
		component.DeleteOrder(arg0, arg1, arg2, arg3, arg4, arg5);
		buffer.Reset(offset);
	}
	void LrpInvoke_TradeServer_CloseAllPositions(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetTradeServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadAString(buffer);
		auto arg2 = ReadUInt32(buffer);
		auto result = component.CloseAllPositions(arg0, arg1, arg2);
		buffer.Reset(offset);
		WriteUInt64(result, buffer);
	}
	void LrpInvoke_TradeServer_CloseByPositions(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetTradeServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadAString(buffer);
		auto arg2 = ReadAString(buffer);
		auto arg3 = ReadAString(buffer);
		auto arg4 = ReadUInt32(buffer);
		auto result = component.CloseByPositions(arg0, arg1, arg2, arg3, arg4);
		buffer.Reset(offset);
		WriteBoolean(result, buffer);
	}
	void LrpInvoke_TradeServer_GetRecords(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetTradeServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadUInt32(buffer);
		auto result = component.GetRecords(arg0, arg1);
		buffer.Reset(offset);
		WriteFxOrderArray(result, buffer);
	}
	void LrpInvoke_TradeServer_OpenNewOrder(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetTradeServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadAString(buffer);
		auto arg2 = ReadFxOrder(buffer);
		auto arg3 = ReadUInt32(buffer);
		auto result = component.OpenNewOrder(arg0, arg1, arg2, arg3);
		buffer.Reset(offset);
		WriteFxOrder(result, buffer);
	}
	void LrpInvoke_TradeServer_ModifyOrder(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetTradeServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadAString(buffer);
		auto arg2 = ReadFxOrder(buffer);
		auto arg3 = ReadUInt32(buffer);
		auto result = component.ModifyOrder(arg0, arg1, arg2, arg3);
		buffer.Reset(offset);
		WriteFxOrder(result, buffer);
	}
	void LrpInvoke_TradeServer_CloseOrder(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetTradeServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadAString(buffer);
		auto arg2 = ReadAString(buffer);
		auto arg3 = ReadNullDouble(buffer);
		auto arg4 = ReadUInt32(buffer);
		auto result = component.CloseOrder(arg0, arg1, arg2, arg3, arg4);
		buffer.Reset(offset);
		WriteClosePositionResult(result, buffer);
	}

	LrpInvoke_TradeServer_Method_Handler gTradeServerHandlers[] = 
	{
		LrpInvoke_TradeServer_Create,
		LrpInvoke_TradeServer_GetTradeTransactionReportsAndSubscribe,
		LrpInvoke_TradeServer_GetTradeCaptureReports,
		LrpInvoke_TradeServer_UnsubscribeTradeTransactionReports,
		LrpInvoke_TradeServer_GetTradeServerInfo,
		LrpInvoke_TradeServer_GetAccountInfo,
		LrpInvoke_TradeServer_DeleteOrder,
		LrpInvoke_TradeServer_CloseAllPositions,
		LrpInvoke_TradeServer_CloseByPositions,
		LrpInvoke_TradeServer_GetRecords,
		LrpInvoke_TradeServer_OpenNewOrder,
		LrpInvoke_TradeServer_ModifyOrder,
		LrpInvoke_TradeServer_CloseOrder,
	};

	HRESULT LrpInvoke_TradeServer(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		if(methodId >= 13)
		{
			return LRP_INVALID_METHOD_ID;
		}
		gTradeServerHandlers[methodId](offset, buffer, pChannel); 
		return S_OK;
	}
}


// handlers of TradeCache component
namespace
{
	const unsigned short LrpComponent_TradeCache_Id = 8;
	const unsigned short LrpMethod_TradeCache_GetAccountInfo_Id = 0;
	const unsigned short LrpMethod_TradeCache_GetRecords_Id = 1;
	const unsigned short LrpMethod_TradeCache_GetPositions_Id = 2;

	typedef void (*LrpInvoke_TradeCache_Method_Handler)(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel);
	void LrpInvoke_TradeCache_GetAccountInfo(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetTradeCache();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.GetAccountInfo(arg0);
		buffer.Reset(offset);
		WriteAccountInfo(result, buffer);
	}
	void LrpInvoke_TradeCache_GetRecords(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetTradeCache();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.GetRecords(arg0);
		buffer.Reset(offset);
		WriteFxOrderArray(result, buffer);
	}
	void LrpInvoke_TradeCache_GetPositions(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetTradeCache();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.GetPositions(arg0);
		buffer.Reset(offset);
		WritePositionArray(result, buffer);
	}

	LrpInvoke_TradeCache_Method_Handler gTradeCacheHandlers[] = 
	{
		LrpInvoke_TradeCache_GetAccountInfo,
		LrpInvoke_TradeCache_GetRecords,
		LrpInvoke_TradeCache_GetPositions,
	};

	HRESULT LrpInvoke_TradeCache(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		if(methodId >= 3)
		{
			return LRP_INVALID_METHOD_ID;
		}
		gTradeCacheHandlers[methodId](offset, buffer, pChannel); 
		return S_OK;
	}
}


// handlers of TradeHistoryIterator component
namespace
{
	const unsigned short LrpComponent_TradeHistoryIterator_Id = 9;
	const unsigned short LrpMethod_TradeHistoryIterator_TotalItems_Id = 0;
	const unsigned short LrpMethod_TradeHistoryIterator_EndOfStream_Id = 1;
	const unsigned short LrpMethod_TradeHistoryIterator_Next_Id = 2;
	const unsigned short LrpMethod_TradeHistoryIterator_GetTradeTransactionReport_Id = 3;

	typedef void (*LrpInvoke_TradeHistoryIterator_Method_Handler)(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel);
	void LrpInvoke_TradeHistoryIterator_TotalItems(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetTradeHistoryIterator();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.TotalItems(arg0);
		buffer.Reset(offset);
		WriteInt32(result, buffer);
	}
	void LrpInvoke_TradeHistoryIterator_EndOfStream(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetTradeHistoryIterator();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.EndOfStream(arg0);
		buffer.Reset(offset);
		WriteBoolean(result, buffer);
	}
	void LrpInvoke_TradeHistoryIterator_Next(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetTradeHistoryIterator();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadUInt32(buffer);
		component.Next(arg0, arg1);
		buffer.Reset(offset);
	}
	void LrpInvoke_TradeHistoryIterator_GetTradeTransactionReport(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetTradeHistoryIterator();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.GetTradeTransactionReport(arg0);
		buffer.Reset(offset);
		WriteTradeTransactionReport(result, buffer);
	}

	LrpInvoke_TradeHistoryIterator_Method_Handler gTradeHistoryIteratorHandlers[] = 
	{
		LrpInvoke_TradeHistoryIterator_TotalItems,
		LrpInvoke_TradeHistoryIterator_EndOfStream,
		LrpInvoke_TradeHistoryIterator_Next,
		LrpInvoke_TradeHistoryIterator_GetTradeTransactionReport,
	};

	HRESULT LrpInvoke_TradeHistoryIterator(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		if(methodId >= 4)
		{
			return LRP_INVALID_METHOD_ID;
		}
		gTradeHistoryIteratorHandlers[methodId](offset, buffer, pChannel); 
		return S_OK;
	}
}


// handlers of DailySnapshotsIterator component
namespace
{
	const unsigned short LrpComponent_DailySnapshotsIterator_Id = 10;
	const unsigned short LrpMethod_DailySnapshotsIterator_TotalItems_Id = 0;
	const unsigned short LrpMethod_DailySnapshotsIterator_EndOfStream_Id = 1;
	const unsigned short LrpMethod_DailySnapshotsIterator_Next_Id = 2;
	const unsigned short LrpMethod_DailySnapshotsIterator_GetDailyAccountSnapshotReport_Id = 3;

	typedef void (*LrpInvoke_DailySnapshotsIterator_Method_Handler)(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel);
	void LrpInvoke_DailySnapshotsIterator_TotalItems(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetDailySnapshotsIterator();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.TotalItems(arg0);
		buffer.Reset(offset);
		WriteInt32(result, buffer);
	}
	void LrpInvoke_DailySnapshotsIterator_EndOfStream(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetDailySnapshotsIterator();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.EndOfStream(arg0);
		buffer.Reset(offset);
		WriteBoolean(result, buffer);
	}
	void LrpInvoke_DailySnapshotsIterator_Next(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetDailySnapshotsIterator();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadUInt32(buffer);
		component.Next(arg0, arg1);
		buffer.Reset(offset);
	}
	void LrpInvoke_DailySnapshotsIterator_GetDailyAccountSnapshotReport(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetDailySnapshotsIterator();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.GetDailyAccountSnapshotReport(arg0);
		buffer.Reset(offset);
		WriteDailyAccountSnapshotReport(result, buffer);
	}

	LrpInvoke_DailySnapshotsIterator_Method_Handler gDailySnapshotsIteratorHandlers[] = 
	{
		LrpInvoke_DailySnapshotsIterator_TotalItems,
		LrpInvoke_DailySnapshotsIterator_EndOfStream,
		LrpInvoke_DailySnapshotsIterator_Next,
		LrpInvoke_DailySnapshotsIterator_GetDailyAccountSnapshotReport,
	};

	HRESULT LrpInvoke_DailySnapshotsIterator(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		if(methodId >= 4)
		{
			return LRP_INVALID_METHOD_ID;
		}
		gDailySnapshotsIteratorHandlers[methodId](offset, buffer, pChannel); 
		return S_OK;
	}
}


// handlers of Library component
namespace
{
	const unsigned short LrpComponent_Library_Id = 11;
	const unsigned short LrpMethod_Library_WriteNormalDumpOnError_Id = 0;
	const unsigned short LrpMethod_Library_WriteFullDumpOnError_Id = 1;
	const unsigned short LrpMethod_Library_WriteNormalDump_Id = 2;
	const unsigned short LrpMethod_Library_WriteFullDump_Id = 3;

	typedef void (*LrpInvoke_Library_Method_Handler)(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel);
	void LrpInvoke_Library_WriteNormalDumpOnError(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLibrary();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadWString(buffer);
		component.WriteNormalDumpOnError(arg0);
		buffer.Reset(offset);
	}
	void LrpInvoke_Library_WriteFullDumpOnError(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLibrary();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadWString(buffer);
		component.WriteFullDumpOnError(arg0);
		buffer.Reset(offset);
	}
	void LrpInvoke_Library_WriteNormalDump(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLibrary();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadWString(buffer);
		component.WriteNormalDump(arg0);
		buffer.Reset(offset);
	}
	void LrpInvoke_Library_WriteFullDump(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLibrary();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadWString(buffer);
		component.WriteFullDump(arg0);
		buffer.Reset(offset);
	}

	LrpInvoke_Library_Method_Handler gLibraryHandlers[] = 
	{
		LrpInvoke_Library_WriteNormalDumpOnError,
		LrpInvoke_Library_WriteFullDumpOnError,
		LrpInvoke_Library_WriteNormalDump,
		LrpInvoke_Library_WriteFullDump,
	};

	HRESULT LrpInvoke_Library(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		if(methodId >= 4)
		{
			return LRP_INVALID_METHOD_ID;
		}
		gLibraryHandlers[methodId](offset, buffer, pChannel); 
		return S_OK;
	}
}


// components handler
namespace
{
	typedef HRESULT (*LrpInvoke_Component_Handler)(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel);
	LrpInvoke_Component_Handler gHandlers[] = 
	{
		LrpInvoke_Handle,
		LrpInvoke_Converter,
		LrpInvoke_Params,
		LrpInvoke_ClientServer,
		LrpInvoke_ClientCache,
		LrpInvoke_FeedServer,
		LrpInvoke_FeedCache,
		LrpInvoke_TradeServer,
		LrpInvoke_TradeCache,
		LrpInvoke_TradeHistoryIterator,
		LrpInvoke_DailySnapshotsIterator,
		LrpInvoke_Library,
	};
}

namespace
{
	HRESULT LrpInvokeEx(size_t offset, size_t componentId, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		if(componentId >= 12)
		{
			return LRP_INVALID_COMPONENT_ID;
		}
		HRESULT result = LRP_EXCEPTION;
		try
		{
			try
			{
				result = gHandlers[componentId](offset, methodId, buffer, pChannel);
				return result;
			}
			catch(const CArgumentNullException& e)
			{
				buffer.Reset(offset);
				WriteInt32(0, buffer);
				WriteArgumentNullException(e, buffer);
			}
			catch(const CArgumentException& e)
			{
				buffer.Reset(offset);
				WriteInt32(1, buffer);
				WriteArgumentException(e, buffer);
			}
			catch(const CInvalidHandleException& e)
			{
				buffer.Reset(offset);
				WriteInt32(2, buffer);
				WriteInvalidHandleException(e, buffer);
			}
			catch(const CRejectException& e)
			{
				buffer.Reset(offset);
				WriteInt32(3, buffer);
				WriteRejectException(e, buffer);
			}
			catch(const CTimeoutException& e)
			{
				buffer.Reset(offset);
				WriteInt32(4, buffer);
				WriteTimeoutException(e, buffer);
			}
			catch(const CSendException& e)
			{
				buffer.Reset(offset);
				WriteInt32(5, buffer);
				WriteSendException(e, buffer);
			}
			catch(const CLogoutException& e)
			{
				buffer.Reset(offset);
				WriteInt32(6, buffer);
				WriteLogoutException(e, buffer);
			}
			catch(const CUnsupportedFeatureException& e)
			{
				buffer.Reset(offset);
				WriteInt32(7, buffer);
				WriteUnsupportedFeatureException(e, buffer);
			}
			catch(const std::runtime_error& e)
			{
				buffer.Reset(offset);
				WriteInt32(8, buffer);
				WriteRuntimeException(e, buffer);
			}
			catch(const std::exception& e)
			{
				buffer.Reset(offset);
				WriteInt32(-1, buffer);
				WriteAString(e.what(), buffer);
			}
			catch(...)
			{
				result = E_FAIL;
			}
		}
		catch(...)
		{
			result = E_FAIL;
		}
		return result;
	}
}

extern "C" HRESULT __stdcall LrpInvoke(unsigned __int16 componentId, unsigned __int16 methodId, void* heap, unsigned __int32* pSize, void** ppData, unsigned __int32* pCapacity)
{
	MemoryBuffer buffer(heap, *ppData, *pSize, *pCapacity);
	HRESULT result = LrpInvokeEx(0, componentId, methodId, buffer, nullptr);
	*pSize = static_cast<unsigned __int32>(buffer.GetSize());
	*ppData = buffer.GetData();
	*pCapacity = static_cast<unsigned __int32>(buffer.GetCapacity());
	buffer = MemoryBuffer();
	return result;
}
extern "C" const char* __stdcall LrpSignature()
{
	return 
	"$Exceptions;"
		"ArgumentNullException@AE18B6F7E69F5B3ABDC2892D4864129D;"
		"ArgumentException@CE622A67EAB717D1B9CFEE1E4D6D4797;"
		"InvalidHandleException@4A6CED109E43FC88EC931999B761319D;"
		"RejectException@9C48126618BDEE837B309D5063835DA7;"
		"TimeoutException@2C9077D83F7A58B117A84740623F01A2;"
		"SendException@C528E01C37EEAF2F365C337F2597F2D2;"
		"LogoutException@1A54490EBC094FD9CFF023FDAD01D8E2;"
		"UnsupportedFeatureException@018EAD1E9AA467694B917E75F10D685E;"
		"RuntimeException@7A4A429D0306E8180F8DE687CFA9F61A;"
	"$Handle;"
		"Delete@630FBC483782B4782AEE60308AE81F65;"
		"NotificationFromHandle@BEB02BF3BB04C8EB7209F50A97731858;"
	"$Converter;"
		"CurrenciesFromHandle@A8CC264990787ED33862B4A6E0838363;"
		"SymbolFromHandle@73F5BE94DE15128BA7988BF2FE336B72;"
		"SymbolsFromHandle@3A0B4BD9066B71BA8403FE0C34B3D6C2;"
		"TwoFactorAuthFromHandle@FB915A5C7CB1D480C3EE3A2B05FEC71F;"
		"SessionInfoFromHandle@7C15EA0A8059440A0600AB96ACC75455;"
		"NotificationFromHandle@BEB02BF3BB04C8EB7209F50A97731858;"
		"QuoteFromHandle@148B292E98A210C7468E99BF8E63167A;"
		"ProtocolVersionFromHandle@E662C4365958A9CB3726120271AA2C4D;"
		"AccountInfoFromHandle@0A1486D74FCFDED014425323774F0857;"
		"PositionFromHandle@D313E7E49EA90F54A0DA20FC1D771E59;"
		"TradeTransactionReportFromHandle@79817376F702AB723FF7865214C5C87B;"
		"ExecutionReportFromHandle@2AF9F466D2C16A5E8FB232BF6EBC3E04;"
		"GetLogoutInfoFromHandle@E25C01C7A37B6BA68326FFCD8EA00126;"
	"$Params;"
		"Create@F8AD2DE8C002053CC9CD614AED5A7294;"
		"SetInt32@22263A8BFAF7369A9C2121F3A84207C7;"
		"SetDouble@2DD4BB413D92C9B1E14244CE84A7D11C;"
		"SetBoolean@FD0384DEDD2EEE3FA9F33AFD3045FB9C;"
		"SetString@4BF58FA27BD380F27211AD6CD17F4D37;"
		"ToText@11BC4C39FB9519A3CB8B3E607B114C9B;"
	"$ClientServer;"
		"Start@D6D8EA3E5548AA39324649E62568D973;"
		"WaitForLogon@66A89EC21889604B2016EDFA6EAC691A;"
		"Dispose@7189470EB16C52D4B54C79280AC338C0;"
		"Shutdown@515DE9A60D3AE51097A9C8B5D33C776D;"
		"Stop@B9A940F484C4A3F91B7251CAE563D7A5;"
		"NextId@33F915FA304E2F5151757D7DE561DF0D;"
		"GetProtocolVersion@39FB33514864AF8661E600500D2C249D;"
		"GetNextMessage@7DA6E4C15870B40BB320FAD8AE769592;"
		"DispatchMessage@A392D063EB2B6FBD41C9E9D8792FBB77;"
		"GetNetworkActivity@F971B04D8C74F97079D5E754A167C543;"
		"SendTwoFactorResponse@B9E034A987F3756B7B4CC10C66D42D2A;"
		"GetSessionInfo@03529F85905897AC1E150E4015871224;"
		"GetFileChunk@384153BF0B70B44D5CE2E25522BD5B05;"
	"$ClientCache;"
		"GetSessionInfo@D22D8007F3C9148ACF7F056F1F86B2A5;"
	"$FeedServer;"
		"Create@3B600297C4910456A888F1A45C27B07F;"
		"GetQuoteHistoryFiles@06324080EDE3B81326C9328F86125C5B;"
		"GetBarsHistoryFiles@7507193C2AEA59209885B65DCCCF100A;"
		"GetCurrencies@110F4541D32D9EE443AEF3A770447495;"
		"GetSymbols@C713C877758BB5601A2708CF61F3CB52;"
		"SubscribeToQuotes@B71983B311AF56C499834F55258DF673;"
		"UnsubscribeQuotes@1B9021898D7DDAD87936E2114700DFD6;"
		"GetBarsHistoryMetaInfoFile@61DC766C893BB76C538E27DAD4E867A8;"
		"GetQuotesHistoryMetaInfoFile@931AF1B3B7C018C830672195403A8F54;"
		"GetHistoryBars@02F6C1C757437A467E99ADF5B007BD90;"
		"GetQueueThreshold@B75A89199CB0CC23E5DE5098B01023BB;"
		"SetQueueThreshold@72B3CC6174B9A7291864A84F7C02DFBD;"
		"GetQuotesHistoryVersion@38A679A1C49DFC5A38DDB95D68CE0F9E;"
	"$FeedCache;"
		"GetSymbols@C9B3B0092C8AD66879723F3D2C632812;"
		"TryGetBid@B3C9F0B172094660F089F9E8D16813BF;"
		"TryGetAsk@547D8C897425B67113EE609D5D054BB9;"
		"TryGetQuote@1CC660CAA93E845F3E00780845CA86C4;"
		"GetCurrencies@3AA598E434D8DA6D35A1FA3F9C7AF48C;"
	"$TradeServer;"
		"Create@3B600297C4910456A888F1A45C27B07F;"
		"GetTradeTransactionReportsAndSubscribe@DD8DB0FB21C7F331B2C86C95B0BC9905;"
		"GetTradeCaptureReports@874C22E243BFC4DED82F08A53E84EF80;"
		"UnsubscribeTradeTransactionReports@42E7909824399BBE11FEC757FAB1BBD7;"
		"GetTradeServerInfo@A34A6F98C30D420EA60766CCA907ADD3;"
		"GetAccountInfo@3DD7B947BC728B9BBF8C79BABC986BE1;"
		"DeleteOrder@DE7E6163B4AE4C044E27D27E5F8318A7;"
		"CloseAllPositions@5AE89441E884CCB9BC0C4E87373FF1EB;"
		"CloseByPositions@0AB581529A7E25D55918AC623D40C891;"
		"GetRecords@B77F233E8C6A54AEB9D17BF880AA4FD8;"
		"OpenNewOrder@45DB72365BEB98148B76B42E823E1F63;"
		"ModifyOrder@BDFD8E6DC0F8B5D81B195872FDA3A9A4;"
		"CloseOrder@0E234736BAA3CA34BFD4E7C636702DCC;"
	"$TradeCache;"
		"GetAccountInfo@446EF938D132F905C2149D7E29252571;"
		"GetRecords@3AEE8D77B680849D7049629BE81F5F03;"
		"GetPositions@66EEB769D2FA814DE7C674BB9336063B;"
	"$TradeHistoryIterator;"
		"TotalItems@2D823D88E5F1A41CD2E0D38B83A2FF81;"
		"EndOfStream@2603242C4BD05C699776EDE9A3BE7095;"
		"Next@B11EB7E43C20423C945B2C435640CBB7;"
		"GetTradeTransactionReport@CD7F098DB444A50152ECABCCA865F6FA;"
	"$DailySnapshotsIterator;"
		"TotalItems@2D823D88E5F1A41CD2E0D38B83A2FF81;"
		"EndOfStream@2603242C4BD05C699776EDE9A3BE7095;"
		"Next@B11EB7E43C20423C945B2C435640CBB7;"
		"GetDailyAccountSnapshotReport@21B93921B88BC70340D6B3E6B1FD56FF;"
	"$Library;"
		"WriteNormalDumpOnError@0B96772D78BDDF0BED91111591B97E95;"
		"WriteFullDumpOnError@B174D794F550A48C009BEC31D8DF5256;"
		"WriteNormalDump@FE87228ACE66FE8482AAAC789941A13E;"
		"WriteFullDump@5FE3AFD309D5CE8168071BC1F937E0BC;"
	"";
}
