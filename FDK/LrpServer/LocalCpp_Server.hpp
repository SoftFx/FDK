// This is always generated file. Do not change anything.


// handlers of Library component
namespace
{
	const unsigned short LrpComponent_Library_Id = 0;
	const unsigned short LrpMethod_Library_SetDotNetDllPath_Id = 0;

	typedef void (*LrpInvoke_Library_Method_Handler)(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel);
	void LrpInvoke_Library_SetDotNetDllPath(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLibrary();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		component.SetDotNetDllPath(arg0);
		buffer.Reset(offset);
	}

	LrpInvoke_Library_Method_Handler gLibraryHandlers[] = 
	{
		LrpInvoke_Library_SetDotNetDllPath,
	};

	HRESULT LrpInvoke_Library(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		if(methodId >= 1)
		{
			return LRP_INVALID_METHOD_ID;
		}
		gLibraryHandlers[methodId](offset, buffer, pChannel); 
		return S_OK;
	}
}


// handlers of LocalServer component
namespace
{
	const unsigned short LrpComponent_LocalServer_Id = 1;
	const unsigned short LrpMethod_LocalServer_Constructor_Id = 0;
	const unsigned short LrpMethod_LocalServer_Destructor_Id = 1;
	const unsigned short LrpMethod_LocalServer_Start_Id = 2;
	const unsigned short LrpMethod_LocalServer_Stop_Id = 3;
	const unsigned short LrpMethod_LocalServer_EndConnection_Id = 4;
	const unsigned short LrpMethod_LocalServer_EndLogon_Id = 5;
	const unsigned short LrpMethod_LocalServer_SendSessionInfo_Id = 6;
	const unsigned short LrpMethod_LocalServer_SendCurrenciesInfo_Id = 7;
	const unsigned short LrpMethod_LocalServer_SendSymbolsInfo_Id = 8;
	const unsigned short LrpMethod_LocalServer_SendQuotesSubscriptionConfirm_Id = 9;
	const unsigned short LrpMethod_LocalServer_SendQuotesSubscriptionReject_Id = 10;
	const unsigned short LrpMethod_LocalServer_SendQuotesHistoryVersion_Id = 11;
	const unsigned short LrpMethod_LocalServer_SendQuote_Id = 12;
	const unsigned short LrpMethod_LocalServer_SendMarketHistoryMetadataReport_Id = 13;
	const unsigned short LrpMethod_LocalServer_SendDataHistoryResponse_Id = 14;
	const unsigned short LrpMethod_LocalServer_SendFileChunk_Id = 15;
	const unsigned short LrpMethod_LocalServer_SendNotification_Id = 16;

	typedef void (*LrpInvoke_LocalServer_Method_Handler)(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel);
	void LrpInvoke_LocalServer_Constructor(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLocalServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadInt32(buffer);
		auto arg2 = ReadAString(buffer);
		auto arg3 = ReadAString(buffer);
		auto arg4 = ReadLocalPointer(buffer);
		auto result = component.Constructor(arg0, arg1, arg2, arg3, arg4);
		buffer.Reset(offset);
		WriteLocalPointer(result, buffer);
	}
	void LrpInvoke_LocalServer_Destructor(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLocalServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		component.Destructor(arg0);
		buffer.Reset(offset);
	}
	void LrpInvoke_LocalServer_Start(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLocalServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		component.Start(arg0);
		buffer.Reset(offset);
	}
	void LrpInvoke_LocalServer_Stop(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLocalServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		component.Stop(arg0);
		buffer.Reset(offset);
	}
	void LrpInvoke_LocalServer_EndConnection(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLocalServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadInt64(buffer);
		auto arg2 = ReadInt32(buffer);
		component.EndConnection(arg0, arg1, arg2);
		buffer.Reset(offset);
	}
	void LrpInvoke_LocalServer_EndLogon(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLocalServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadInt64(buffer);
		auto arg2 = ReadInt32(buffer);
		auto arg3 = ReadAString(buffer);
		component.EndLogon(arg0, arg1, arg2, arg3);
		buffer.Reset(offset);
	}
	void LrpInvoke_LocalServer_SendSessionInfo(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLocalServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadInt64(buffer);
		auto arg2 = ReadAString(buffer);
		auto arg3 = ReadLrpSessionInfo(buffer);
		component.SendSessionInfo(arg0, arg1, arg2, arg3);
		buffer.Reset(offset);
	}
	void LrpInvoke_LocalServer_SendCurrenciesInfo(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLocalServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadInt64(buffer);
		auto arg2 = ReadAString(buffer);
		auto arg3 = ReadCurrencyInfoArray(buffer);
		component.SendCurrenciesInfo(arg0, arg1, arg2, arg3);
		buffer.Reset(offset);
	}
	void LrpInvoke_LocalServer_SendSymbolsInfo(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLocalServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadInt64(buffer);
		auto arg2 = ReadAString(buffer);
		auto arg3 = ReadSymbolInfoArray(buffer);
		component.SendSymbolsInfo(arg0, arg1, arg2, arg3);
		buffer.Reset(offset);
	}
	void LrpInvoke_LocalServer_SendQuotesSubscriptionConfirm(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLocalServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadInt64(buffer);
		auto arg2 = ReadAString(buffer);
		component.SendQuotesSubscriptionConfirm(arg0, arg1, arg2);
		buffer.Reset(offset);
	}
	void LrpInvoke_LocalServer_SendQuotesSubscriptionReject(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLocalServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadInt64(buffer);
		auto arg2 = ReadAString(buffer);
		auto arg3 = ReadAString(buffer);
		component.SendQuotesSubscriptionReject(arg0, arg1, arg2, arg3);
		buffer.Reset(offset);
	}
	void LrpInvoke_LocalServer_SendQuotesHistoryVersion(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLocalServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadInt64(buffer);
		auto arg2 = ReadAString(buffer);
		auto arg3 = ReadInt32(buffer);
		component.SendQuotesHistoryVersion(arg0, arg1, arg2, arg3);
		buffer.Reset(offset);
	}
	void LrpInvoke_LocalServer_SendQuote(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLocalServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadInt64(buffer);
		auto arg2 = ReadQuote(buffer);
		component.SendQuote(arg0, arg1, arg2);
		buffer.Reset(offset);
	}
	void LrpInvoke_LocalServer_SendMarketHistoryMetadataReport(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLocalServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadInt64(buffer);
		auto arg2 = ReadAString(buffer);
		auto arg3 = ReadInt32(buffer);
		auto arg4 = ReadAString(buffer);
		component.SendMarketHistoryMetadataReport(arg0, arg1, arg2, arg3, arg4);
		buffer.Reset(offset);
	}
	void LrpInvoke_LocalServer_SendDataHistoryResponse(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLocalServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadInt64(buffer);
		auto arg2 = ReadAString(buffer);
		auto arg3 = ReadDataHistoryResponse(buffer);
		component.SendDataHistoryResponse(arg0, arg1, arg2, arg3);
		buffer.Reset(offset);
	}
	void LrpInvoke_LocalServer_SendFileChunk(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLocalServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadInt64(buffer);
		auto arg2 = ReadAString(buffer);
		auto arg3 = ReadFileChunk(buffer);
		component.SendFileChunk(arg0, arg1, arg2, arg3);
		buffer.Reset(offset);
	}
	void LrpInvoke_LocalServer_SendNotification(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLocalServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadInt64(buffer);
		auto arg2 = ReadNotification(buffer);
		component.SendNotification(arg0, arg1, arg2);
		buffer.Reset(offset);
	}

	LrpInvoke_LocalServer_Method_Handler gLocalServerHandlers[] = 
	{
		LrpInvoke_LocalServer_Constructor,
		LrpInvoke_LocalServer_Destructor,
		LrpInvoke_LocalServer_Start,
		LrpInvoke_LocalServer_Stop,
		LrpInvoke_LocalServer_EndConnection,
		LrpInvoke_LocalServer_EndLogon,
		LrpInvoke_LocalServer_SendSessionInfo,
		LrpInvoke_LocalServer_SendCurrenciesInfo,
		LrpInvoke_LocalServer_SendSymbolsInfo,
		LrpInvoke_LocalServer_SendQuotesSubscriptionConfirm,
		LrpInvoke_LocalServer_SendQuotesSubscriptionReject,
		LrpInvoke_LocalServer_SendQuotesHistoryVersion,
		LrpInvoke_LocalServer_SendQuote,
		LrpInvoke_LocalServer_SendMarketHistoryMetadataReport,
		LrpInvoke_LocalServer_SendDataHistoryResponse,
		LrpInvoke_LocalServer_SendFileChunk,
		LrpInvoke_LocalServer_SendNotification,
	};

	HRESULT LrpInvoke_LocalServer(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		if(methodId >= 17)
		{
			return LRP_INVALID_METHOD_ID;
		}
		gLocalServerHandlers[methodId](offset, buffer, pChannel); 
		return S_OK;
	}
}


// handlers of LocalChannelsPool component
namespace
{
	const unsigned short LrpComponent_LocalChannelsPool_Id = 2;
	const unsigned short LrpMethod_LocalChannelsPool_Constructor_Id = 0;
	const unsigned short LrpMethod_LocalChannelsPool_Destructor_Id = 1;

	typedef void (*LrpInvoke_LocalChannelsPool_Method_Handler)(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel);
	void LrpInvoke_LocalChannelsPool_Constructor(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLocalChannelsPool();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLrpParams(buffer);
		auto result = component.Constructor(arg0);
		buffer.Reset(offset);
		WriteLocalPointer(result, buffer);
	}
	void LrpInvoke_LocalChannelsPool_Destructor(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLocalChannelsPool();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		component.Destructor(arg0);
		buffer.Reset(offset);
	}

	LrpInvoke_LocalChannelsPool_Method_Handler gLocalChannelsPoolHandlers[] = 
	{
		LrpInvoke_LocalChannelsPool_Constructor,
		LrpInvoke_LocalChannelsPool_Destructor,
	};

	HRESULT LrpInvoke_LocalChannelsPool(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		if(methodId >= 2)
		{
			return LRP_INVALID_METHOD_ID;
		}
		gLocalChannelsPoolHandlers[methodId](offset, buffer, pChannel); 
		return S_OK;
	}
}


// components handler
namespace
{
	typedef HRESULT (*LrpInvoke_Component_Handler)(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel);
	LrpInvoke_Component_Handler gHandlers[] = 
	{
		LrpInvoke_Library,
		LrpInvoke_LocalServer,
		LrpInvoke_LocalChannelsPool,
	};
}

namespace
{
	HRESULT LrpInvokeEx(size_t offset, size_t componentId, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		if(componentId >= 3)
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
	"$Library;"
		"SetDotNetDllPath@6F2C4CE4292BC0FB67170303B88D56CF;"
	"$LocalServer;"
		"Constructor@31F8E5AA350AE1E6D33387979AF065A5;"
		"Destructor@578A3F61986EADEE8A91305955BD65E4;"
		"Start@8499C9CF2B655BFFCEA299DF849ADE26;"
		"Stop@A1A4AE7AFB43D7C671448EACD33040F5;"
		"EndConnection@0370DB62007740256A63BB50A7B94630;"
		"EndLogon@888D727D5D696B0840D375552D005AB0;"
		"SendSessionInfo@DE2512DED6A56D5A328F5D079AAFB5D4;"
		"SendCurrenciesInfo@EDFB427903E76678C835150E04B40CBF;"
		"SendSymbolsInfo@E2CFACC548B51397EB45C332C1D426EB;"
		"SendQuotesSubscriptionConfirm@FEE0176AACD68A73B96E563856A954DF;"
		"SendQuotesSubscriptionReject@0379040CD01D33FB84C8E175794A58FA;"
		"SendQuotesHistoryVersion@8577FE2BF553EC46CC93BEADC7F05E1C;"
		"SendQuote@9BBACA5E152556B34C53D9A444D73688;"
		"SendMarketHistoryMetadataReport@2EDF810D688C772D91523BBAF031FD58;"
		"SendDataHistoryResponse@3245C6614A269E1DA4A6CCBC72CAF60A;"
		"SendFileChunk@0450959066E76C838A2BCD7B598E8DC5;"
		"SendNotification@3A5516D3CE20D8D139D897401112591D;"
	"$LocalChannelsPool;"
		"Constructor@62EA0A46CB237981503C99A221EE79E2;"
		"Destructor@578A3F61986EADEE8A91305955BD65E4;"
	"";
}