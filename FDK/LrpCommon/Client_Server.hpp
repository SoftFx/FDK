// This is always generated file. Do not change anything.


// handlers of Client component
namespace
{
	const unsigned short LrpComponent_Client_Id = 0;
	const unsigned short LrpMethod_Client_OnHeartBeatRequest_Id = 0;
	const unsigned short LrpMethod_Client_OnHeartBeatResponse_Id = 1;
	const unsigned short LrpMethod_Client_OnLogonMsg_Id = 2;
	const unsigned short LrpMethod_Client_OnLogoutMsg_Id = 3;
	const unsigned short LrpMethod_Client_OnSessionInfoMsg_Id = 4;
	const unsigned short LrpMethod_Client_OnSessionInfoMsg2_Id = 5;
	const unsigned short LrpMethod_Client_OnCurrenciesInfoMsg_Id = 6;
	const unsigned short LrpMethod_Client_OnSymbolsInfoMsg_Id = 7;
	const unsigned short LrpMethod_Client_OnSymbolsInfoMsg2_Id = 8;
	const unsigned short LrpMethod_Client_OnSymbolsInfoMsg3_Id = 9;
	const unsigned short LrpMethod_Client_OnSymbolsInfoMsg4_Id = 10;
	const unsigned short LrpMethod_Client_OnSymbolsInfoMsg5_Id = 11;
	const unsigned short LrpMethod_Client_OnSymbolsInfoMsg6_Id = 12;
	const unsigned short LrpMethod_Client_OnSymbolsInfoMsg7_Id = 13;
	const unsigned short LrpMethod_Client_OnComponentsInfoMsg_Id = 14;
	const unsigned short LrpMethod_Client_OnQuotesSubscriptionMsg_Id = 15;
	const unsigned short LrpMethod_Client_OnFileChunkMsg_Id = 16;
	const unsigned short LrpMethod_Client_OnDataHistoryMetaInfoResponseMsg_Id = 17;
	const unsigned short LrpMethod_Client_OnDataHistoryMetaInfoRejectMsg_Id = 18;
	const unsigned short LrpMethod_Client_OnDataHistoryResponseMsg_Id = 19;
	const unsigned short LrpMethod_Client_OnDataHistoryRejectMsg_Id = 20;
	const unsigned short LrpMethod_Client_OnQuoteRawMsg_Id = 21;
	const unsigned short LrpMethod_Client_OnNotificationMsg_Id = 22;
	const unsigned short LrpMethod_Client_OnBusinessRejectMsg_Id = 23;

	typedef void (*LrpInvoke_Client_Method_Handler)(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel);
	void LrpInvoke_Client_OnHeartBeatRequest(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClient();
		component; // if all methods of component are static then the next line generates warning #4189
		component.OnHeartBeatRequest();
		buffer.Reset(offset);
	}
	void LrpInvoke_Client_OnHeartBeatResponse(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClient();
		component; // if all methods of component are static then the next line generates warning #4189
		component.OnHeartBeatResponse();
		buffer.Reset(offset);
	}
	void LrpInvoke_Client_OnLogonMsg(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClient();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		component.OnLogonMsg(arg0);
		buffer.Reset(offset);
	}
	void LrpInvoke_Client_OnLogoutMsg(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClient();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLogoutReason(buffer);
		auto arg1 = ReadAString(buffer);
		component.OnLogoutMsg(arg0, arg1);
		buffer.Reset(offset);
	}
	void LrpInvoke_Client_OnSessionInfoMsg(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClient();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto arg1 = ReadLrpSessionInfo(buffer);
		component.OnSessionInfoMsg(arg0, arg1);
		buffer.Reset(offset);
	}
	void LrpInvoke_Client_OnSessionInfoMsg2(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClient();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto arg1 = ReadLrpSessionInfo2(buffer);
		component.OnSessionInfoMsg2(arg0, arg1);
		buffer.Reset(offset);
	}
	void LrpInvoke_Client_OnCurrenciesInfoMsg(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClient();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto arg1 = ReadCurrencyInfoArray(buffer);
		component.OnCurrenciesInfoMsg(arg0, arg1);
		buffer.Reset(offset);
	}
	void LrpInvoke_Client_OnSymbolsInfoMsg(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClient();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto arg1 = ReadSymbolInfoArray(buffer);
		component.OnSymbolsInfoMsg(arg0, arg1);
		buffer.Reset(offset);
	}
	void LrpInvoke_Client_OnSymbolsInfoMsg2(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClient();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto arg1 = ReadSymbolInfoArray2(buffer);
		component.OnSymbolsInfoMsg2(arg0, arg1);
		buffer.Reset(offset);
	}
	void LrpInvoke_Client_OnSymbolsInfoMsg3(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClient();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto arg1 = ReadSymbolInfoArray3(buffer);
		component.OnSymbolsInfoMsg3(arg0, arg1);
		buffer.Reset(offset);
	}
	void LrpInvoke_Client_OnSymbolsInfoMsg4(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClient();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto arg1 = ReadSymbolInfoArray4(buffer);
		component.OnSymbolsInfoMsg4(arg0, arg1);
		buffer.Reset(offset);
	}
	void LrpInvoke_Client_OnSymbolsInfoMsg5(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClient();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto arg1 = ReadSymbolInfoArray5(buffer);
		component.OnSymbolsInfoMsg5(arg0, arg1);
		buffer.Reset(offset);
	}
	void LrpInvoke_Client_OnSymbolsInfoMsg6(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClient();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto arg1 = ReadSymbolInfoArray6(buffer);
		component.OnSymbolsInfoMsg6(arg0, arg1);
		buffer.Reset(offset);
	}
	void LrpInvoke_Client_OnSymbolsInfoMsg7(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClient();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto arg1 = ReadSymbolInfoArray7(buffer);
		component.OnSymbolsInfoMsg7(arg0, arg1);
		buffer.Reset(offset);
	}
	void LrpInvoke_Client_OnComponentsInfoMsg(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClient();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto arg1 = ReadInt32(buffer);
		component.OnComponentsInfoMsg(arg0, arg1);
		buffer.Reset(offset);
	}
	void LrpInvoke_Client_OnQuotesSubscriptionMsg(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClient();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto arg1 = ReadInt32(buffer);
		auto arg2 = ReadAString(buffer);
		component.OnQuotesSubscriptionMsg(arg0, arg1, arg2);
		buffer.Reset(offset);
	}
	void LrpInvoke_Client_OnFileChunkMsg(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClient();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto arg1 = ReadFileChunk(buffer);
		component.OnFileChunkMsg(arg0, arg1);
		buffer.Reset(offset);
	}
	void LrpInvoke_Client_OnDataHistoryMetaInfoResponseMsg(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClient();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto arg1 = ReadInt32(buffer);
		auto arg2 = ReadAString(buffer);
		component.OnDataHistoryMetaInfoResponseMsg(arg0, arg1, arg2);
		buffer.Reset(offset);
	}
	void LrpInvoke_Client_OnDataHistoryMetaInfoRejectMsg(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClient();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto arg1 = ReadInt32(buffer);
		auto arg2 = ReadAString(buffer);
		component.OnDataHistoryMetaInfoRejectMsg(arg0, arg1, arg2);
		buffer.Reset(offset);
	}
	void LrpInvoke_Client_OnDataHistoryResponseMsg(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClient();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto arg1 = ReadDataHistoryResponse(buffer);
		component.OnDataHistoryResponseMsg(arg0, arg1);
		buffer.Reset(offset);
	}
	void LrpInvoke_Client_OnDataHistoryRejectMsg(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClient();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto arg1 = ReadMarketHistoryRejectType(buffer);
		auto arg2 = ReadAString(buffer);
		component.OnDataHistoryRejectMsg(arg0, arg1, arg2);
		buffer.Reset(offset);
	}
	void LrpInvoke_Client_OnQuoteRawMsg(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClient();
		component; // if all methods of component are static then the next line generates warning #4189
		auto& arg0 = ReadRaw(buffer);
		component.OnQuoteRawMsg(arg0);
		buffer.Reset(offset);
	}
	void LrpInvoke_Client_OnNotificationMsg(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClient();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadNotification(buffer);
		component.OnNotificationMsg(arg0);
		buffer.Reset(offset);
	}
	void LrpInvoke_Client_OnBusinessRejectMsg(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetClient();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto arg1 = ReadAString(buffer);
		component.OnBusinessRejectMsg(arg0, arg1);
		buffer.Reset(offset);
	}

	LrpInvoke_Client_Method_Handler gClientHandlers[] = 
	{
		LrpInvoke_Client_OnHeartBeatRequest,
		LrpInvoke_Client_OnHeartBeatResponse,
		LrpInvoke_Client_OnLogonMsg,
		LrpInvoke_Client_OnLogoutMsg,
		LrpInvoke_Client_OnSessionInfoMsg,
		LrpInvoke_Client_OnSessionInfoMsg2,
		LrpInvoke_Client_OnCurrenciesInfoMsg,
		LrpInvoke_Client_OnSymbolsInfoMsg,
		LrpInvoke_Client_OnSymbolsInfoMsg2,
		LrpInvoke_Client_OnSymbolsInfoMsg3,
		LrpInvoke_Client_OnSymbolsInfoMsg4,
		LrpInvoke_Client_OnSymbolsInfoMsg5,
		LrpInvoke_Client_OnSymbolsInfoMsg6,
		LrpInvoke_Client_OnSymbolsInfoMsg7,
		LrpInvoke_Client_OnComponentsInfoMsg,
		LrpInvoke_Client_OnQuotesSubscriptionMsg,
		LrpInvoke_Client_OnFileChunkMsg,
		LrpInvoke_Client_OnDataHistoryMetaInfoResponseMsg,
		LrpInvoke_Client_OnDataHistoryMetaInfoRejectMsg,
		LrpInvoke_Client_OnDataHistoryResponseMsg,
		LrpInvoke_Client_OnDataHistoryRejectMsg,
		LrpInvoke_Client_OnQuoteRawMsg,
		LrpInvoke_Client_OnNotificationMsg,
		LrpInvoke_Client_OnBusinessRejectMsg,
	};

	HRESULT LrpInvoke_Client(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		if(methodId >= 24)
		{
			return LRP_INVALID_METHOD_ID;
		}
		gClientHandlers[methodId](offset, buffer, pChannel); 
		return S_OK;
	}
}


// handlers of SimpleCodec component
namespace
{
	const unsigned short LrpComponent_SimpleCodec_Id = 1;
	const unsigned short LrpMethod_SimpleCodec_OnSymbolIndexMsg_Id = 0;
	const unsigned short LrpMethod_SimpleCodec_OnTimeSynchMsg_Id = 1;
	const unsigned short LrpMethod_SimpleCodec_OnSymbol8Time32Level2Msg_Id = 2;
	const unsigned short LrpMethod_SimpleCodec_OnQuoteZipRawMsg_Id = 3;

	typedef void (*LrpInvoke_SimpleCodec_Method_Handler)(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel);
	void LrpInvoke_SimpleCodec_OnSymbolIndexMsg(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetSimpleCodec();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto arg1 = ReadUInt32(buffer);
		component.OnSymbolIndexMsg(arg0, arg1);
		buffer.Reset(offset);
	}
	void LrpInvoke_SimpleCodec_OnTimeSynchMsg(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetSimpleCodec();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadTime(buffer);
		component.OnTimeSynchMsg(arg0);
		buffer.Reset(offset);
	}
	void LrpInvoke_SimpleCodec_OnSymbol8Time32Level2Msg(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetSimpleCodec();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadUInt8(buffer);
		auto arg1 = ReadInt32(buffer);
		auto& arg2 = ReadRaw(buffer);
		component.OnSymbol8Time32Level2Msg(arg0, arg1, arg2);
		buffer.Reset(offset);
	}
	void LrpInvoke_SimpleCodec_OnQuoteZipRawMsg(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetSimpleCodec();
		component; // if all methods of component are static then the next line generates warning #4189
		auto& arg0 = ReadRaw(buffer);
		component.OnQuoteZipRawMsg(arg0);
		buffer.Reset(offset);
	}

	LrpInvoke_SimpleCodec_Method_Handler gSimpleCodecHandlers[] = 
	{
		LrpInvoke_SimpleCodec_OnSymbolIndexMsg,
		LrpInvoke_SimpleCodec_OnTimeSynchMsg,
		LrpInvoke_SimpleCodec_OnSymbol8Time32Level2Msg,
		LrpInvoke_SimpleCodec_OnQuoteZipRawMsg,
	};

	HRESULT LrpInvoke_SimpleCodec(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		if(methodId >= 4)
		{
			return LRP_INVALID_METHOD_ID;
		}
		gSimpleCodecHandlers[methodId](offset, buffer, pChannel); 
		return S_OK;
	}
}


// components handler
namespace
{
	typedef HRESULT (*LrpInvoke_Component_Handler)(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel);
	LrpInvoke_Component_Handler gHandlers[] = 
	{
		LrpInvoke_Client,
		LrpInvoke_SimpleCodec,
	};
}

namespace
{
	HRESULT LrpInvokeEx(size_t offset, size_t componentId, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		if(componentId >= 2)
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
	"$Client;"
		"OnHeartBeatRequest@649F60FAC31A75EF0179222C5C27655D;"
		"OnHeartBeatResponse@53584CC68A5EF9F98311B4D86431576D;"
		"OnLogonMsg@7D89BC637EEE097F74BA4D0D3043C09B;"
		"OnLogoutMsg@B5C67418D4A0289A849B6F135D1D6DFB;"
		"OnSessionInfoMsg@942F0D874BAD4A93A6BB742CB87129B9;"
		"OnSessionInfoMsg2@0DCAD5CB27B3263AA30C4D4287EA9C69;"
		"OnCurrenciesInfoMsg@E8FF29179A1AC30D070BAD0D71FC6423;"
		"OnSymbolsInfoMsg@4F1231F9B4C30CE60F1A49DCC456186A;"
		"OnSymbolsInfoMsg2@84965D744575881DF415433E755AE83A;"
		"OnSymbolsInfoMsg3@11E2D35691E5FA57FDFF70E8E26FD67A;"
		"OnSymbolsInfoMsg4@906AFCCFFE625B52222936EAA9DA5DF9;"
		"OnSymbolsInfoMsg5@CAF8FDD12D55BDACF93DC65B52EF6C42;"
		"OnSymbolsInfoMsg6@8E174DB8ED519E7EAA5EBE04BD145056;"
		"OnSymbolsInfoMsg7@1DE3AABB09196852E32FC18AFD0E585C;"
		"OnComponentsInfoMsg@45B9ED80F2ED2C785B1E07E1EB58C40B;"
		"OnQuotesSubscriptionMsg@9DF528C6AAB1961D4198C68CB84FE088;"
		"OnFileChunkMsg@E154EAE3A359C1F58CFF26CC566FCCFB;"
		"OnDataHistoryMetaInfoResponseMsg@9C22D7EB20BC03B2C5293A68406A0AEC;"
		"OnDataHistoryMetaInfoRejectMsg@35C5612E4B1DCB29D7D54D98F588A588;"
		"OnDataHistoryResponseMsg@CB965CDEB09B69B5148CF852BC67693D;"
		"OnDataHistoryRejectMsg@3E6DF0C36CD841F613ED6505F2E5819B;"
		"OnQuoteRawMsg@BA16C284C1106F73C5D011EEAD8CB4F7;"
		"OnNotificationMsg@E63C9A7B67F2E06181E3EB2330AC8681;"
		"OnBusinessRejectMsg@B306EB27B56F78F595D533865869108A;"
	"$SimpleCodec;"
		"OnSymbolIndexMsg@4C3B42F4CB0CEECC68AFD00D43791BC8;"
		"OnTimeSynchMsg@8E26F4681DEB634E809CF722E94A058B;"
		"OnSymbol8Time32Level2Msg@C81A805433E5C39639B854312D78BEFD;"
		"OnQuoteZipRawMsg@B6526DE221FB98BC44563408F24BED5D;"
	"";
}
