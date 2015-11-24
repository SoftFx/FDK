// This is always generated file. Do not change anything.


// handlers of Server component
namespace
{
	const unsigned short LrpComponent_Server_Id = 0;
	const unsigned short LrpMethod_Server_OnHeartBeatRequest_Id = 0;
	const unsigned short LrpMethod_Server_OnHeartBeatResponse_Id = 1;
	const unsigned short LrpMethod_Server_OnCurrenciesInfoRequest_Id = 2;
	const unsigned short LrpMethod_Server_OnSymbolsInfoRequest_Id = 3;
	const unsigned short LrpMethod_Server_OnSessionInfoRequest_Id = 4;
	const unsigned short LrpMethod_Server_OnSubscribeToQuotesRequest_Id = 5;
	const unsigned short LrpMethod_Server_OnUnsubscribeQuotesRequest_Id = 6;
	const unsigned short LrpMethod_Server_OnComponentsInfoRequest_Id = 7;
	const unsigned short LrpMethod_Server_OnDataHistoryRequest_Id = 8;
	const unsigned short LrpMethod_Server_OnFileChunkRequest_Id = 9;
	const unsigned short LrpMethod_Server_OnBarsHistoryMetaInfoFileRequest_Id = 10;
	const unsigned short LrpMethod_Server_OnQuotesHistoryMetaInfoFileRequest_Id = 11;

	typedef void (*LrpInvoke_Server_Method_Handler)(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel);
	void LrpInvoke_Server_OnHeartBeatRequest(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetServer();
		component; // if all methods of component are static then the next line generates warning #4189
		component.OnHeartBeatRequest();
		buffer.Reset(offset);
	}
	void LrpInvoke_Server_OnHeartBeatResponse(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetServer();
		component; // if all methods of component are static then the next line generates warning #4189
		component.OnHeartBeatResponse();
		buffer.Reset(offset);
	}
	void LrpInvoke_Server_OnCurrenciesInfoRequest(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		component.OnCurrenciesInfoRequest(arg0);
		buffer.Reset(offset);
	}
	void LrpInvoke_Server_OnSymbolsInfoRequest(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		component.OnSymbolsInfoRequest(arg0);
		buffer.Reset(offset);
	}
	void LrpInvoke_Server_OnSessionInfoRequest(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		component.OnSessionInfoRequest(arg0);
		buffer.Reset(offset);
	}
	void LrpInvoke_Server_OnSubscribeToQuotesRequest(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto arg1 = ReadAStringArray(buffer);
		auto arg2 = ReadInt32(buffer);
		component.OnSubscribeToQuotesRequest(arg0, arg1, arg2);
		buffer.Reset(offset);
	}
	void LrpInvoke_Server_OnUnsubscribeQuotesRequest(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto arg1 = ReadAStringArray(buffer);
		component.OnUnsubscribeQuotesRequest(arg0, arg1);
		buffer.Reset(offset);
	}
	void LrpInvoke_Server_OnComponentsInfoRequest(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto arg1 = ReadInt32(buffer);
		component.OnComponentsInfoRequest(arg0, arg1);
		buffer.Reset(offset);
	}
	void LrpInvoke_Server_OnDataHistoryRequest(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto arg1 = ReadDataHistoryRequest(buffer);
		component.OnDataHistoryRequest(arg0, arg1);
		buffer.Reset(offset);
	}
	void LrpInvoke_Server_OnFileChunkRequest(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto arg1 = ReadAString(buffer);
		auto arg2 = ReadUInt32(buffer);
		component.OnFileChunkRequest(arg0, arg1, arg2);
		buffer.Reset(offset);
	}
	void LrpInvoke_Server_OnBarsHistoryMetaInfoFileRequest(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto arg1 = ReadAString(buffer);
		auto arg2 = ReadInt32(buffer);
		auto arg3 = ReadAString(buffer);
		component.OnBarsHistoryMetaInfoFileRequest(arg0, arg1, arg2, arg3);
		buffer.Reset(offset);
	}
	void LrpInvoke_Server_OnQuotesHistoryMetaInfoFileRequest(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetServer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto arg1 = ReadAString(buffer);
		auto arg2 = ReadBoolean(buffer);
		component.OnQuotesHistoryMetaInfoFileRequest(arg0, arg1, arg2);
		buffer.Reset(offset);
	}

	LrpInvoke_Server_Method_Handler gServerHandlers[] = 
	{
		LrpInvoke_Server_OnHeartBeatRequest,
		LrpInvoke_Server_OnHeartBeatResponse,
		LrpInvoke_Server_OnCurrenciesInfoRequest,
		LrpInvoke_Server_OnSymbolsInfoRequest,
		LrpInvoke_Server_OnSessionInfoRequest,
		LrpInvoke_Server_OnSubscribeToQuotesRequest,
		LrpInvoke_Server_OnUnsubscribeQuotesRequest,
		LrpInvoke_Server_OnComponentsInfoRequest,
		LrpInvoke_Server_OnDataHistoryRequest,
		LrpInvoke_Server_OnFileChunkRequest,
		LrpInvoke_Server_OnBarsHistoryMetaInfoFileRequest,
		LrpInvoke_Server_OnQuotesHistoryMetaInfoFileRequest,
	};

	HRESULT LrpInvoke_Server(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		if(methodId >= 12)
		{
			return LRP_INVALID_METHOD_ID;
		}
		gServerHandlers[methodId](offset, buffer, pChannel); 
		return S_OK;
	}
}


// components handler
namespace
{
	typedef HRESULT (*LrpInvoke_Component_Handler)(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel);
	LrpInvoke_Component_Handler gHandlers[] = 
	{
		LrpInvoke_Server,
	};
}

namespace
{
	HRESULT LrpInvokeEx(size_t offset, size_t componentId, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		if(componentId >= 1)
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
	"$Server;"
		"OnHeartBeatRequest@649F60FAC31A75EF0179222C5C27655D;"
		"OnHeartBeatResponse@53584CC68A5EF9F98311B4D86431576D;"
		"OnCurrenciesInfoRequest@E6F0091DB9E7B73AE0CEA7CD77318241;"
		"OnSymbolsInfoRequest@83BCDF2EEBC55F2EF1AEFBCE150E9FCF;"
		"OnSessionInfoRequest@A941B0F5C96CF054661A0F6925751F99;"
		"OnSubscribeToQuotesRequest@48BD3703EA5F3D3D116930EC0F7D6E5B;"
		"OnUnsubscribeQuotesRequest@65D0C196E1F746602C717772463A9910;"
		"OnComponentsInfoRequest@80705679A4CE1A94C0A95C109D91661A;"
		"OnDataHistoryRequest@A51124B279EE15597282498B005B2ACF;"
		"OnFileChunkRequest@EFEF9C78B68ED7950A413964030A0149;"
		"OnBarsHistoryMetaInfoFileRequest@C9559DB271D8ACDD57378A460FB87FED;"
		"OnQuotesHistoryMetaInfoFileRequest@84B7650F68B7EE82E499673405B98DA0;"
	"";
}
