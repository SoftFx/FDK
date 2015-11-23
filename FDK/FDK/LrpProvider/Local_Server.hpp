// This is always generated file. Do not change anything.


// handlers of LrpCodec component
namespace
{
	const unsigned short LrpComponent_LrpCodec_Id = 0;
	const unsigned short LrpMethod_LrpCodec_Constructor_Id = 0;
	const unsigned short LrpMethod_LrpCodec_Destructor_Id = 1;
	const unsigned short LrpMethod_LrpCodec_GetCount_Id = 2;
	const unsigned short LrpMethod_LrpCodec_GetSize_Id = 3;
	const unsigned short LrpMethod_LrpCodec_GetTime_Id = 4;
	const unsigned short LrpMethod_LrpCodec_EncodeRaw_Id = 5;
	const unsigned short LrpMethod_LrpCodec_EncodeSlow_Id = 6;
	const unsigned short LrpMethod_LrpCodec_EncodeFast_Id = 7;
	const unsigned short LrpMethod_LrpCodec_Clear_Id = 8;

	typedef void (*LrpInvoke_LrpCodec_Method_Handler)(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel);
	void LrpInvoke_LrpCodec_Constructor(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLrpCodec();
		component; // if all methods of component are static then the next line generates warning #4189
		auto result = component.Constructor();
		buffer.Reset(offset);
		WriteLocalPointer(result, buffer);
	}
	void LrpInvoke_LrpCodec_Destructor(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLrpCodec();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		component.Destructor(arg0);
		buffer.Reset(offset);
	}
	void LrpInvoke_LrpCodec_GetCount(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLrpCodec();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.GetCount(arg0);
		buffer.Reset(offset);
		WriteInt64(result, buffer);
	}
	void LrpInvoke_LrpCodec_GetSize(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLrpCodec();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.GetSize(arg0);
		buffer.Reset(offset);
		WriteInt64(result, buffer);
	}
	void LrpInvoke_LrpCodec_GetTime(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLrpCodec();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.GetTime(arg0);
		buffer.Reset(offset);
		WriteDouble(result, buffer);
	}
	void LrpInvoke_LrpCodec_EncodeRaw(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLrpCodec();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadQuoteArray(buffer);
		component.EncodeRaw(arg0, arg1);
		buffer.Reset(offset);
	}
	void LrpInvoke_LrpCodec_EncodeSlow(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLrpCodec();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadQuoteArray(buffer);
		component.EncodeSlow(arg0, arg1);
		buffer.Reset(offset);
	}
	void LrpInvoke_LrpCodec_EncodeFast(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLrpCodec();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadUInt32(buffer);
		auto arg2 = ReadDouble(buffer);
		auto arg3 = ReadQuoteArray(buffer);
		component.EncodeFast(arg0, arg1, arg2, arg3);
		buffer.Reset(offset);
	}
	void LrpInvoke_LrpCodec_Clear(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetLrpCodec();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		component.Clear(arg0);
		buffer.Reset(offset);
	}

	LrpInvoke_LrpCodec_Method_Handler gLrpCodecHandlers[] = 
	{
		LrpInvoke_LrpCodec_Constructor,
		LrpInvoke_LrpCodec_Destructor,
		LrpInvoke_LrpCodec_GetCount,
		LrpInvoke_LrpCodec_GetSize,
		LrpInvoke_LrpCodec_GetTime,
		LrpInvoke_LrpCodec_EncodeRaw,
		LrpInvoke_LrpCodec_EncodeSlow,
		LrpInvoke_LrpCodec_EncodeFast,
		LrpInvoke_LrpCodec_Clear,
	};

	HRESULT LrpInvoke_LrpCodec(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		if(methodId >= 9)
		{
			return LRP_INVALID_METHOD_ID;
		}
		gLrpCodecHandlers[methodId](offset, buffer, pChannel); 
		return S_OK;
	}
}


// components handler
namespace
{
	typedef HRESULT (*LrpInvoke_Component_Handler)(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel);
	LrpInvoke_Component_Handler gHandlers[] = 
	{
		LrpInvoke_LrpCodec,
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
	"$LrpCodec;"
		"Constructor@208BBD5C462457D264AC717DBD4C2FF4;"
		"Destructor@578A3F61986EADEE8A91305955BD65E4;"
		"GetCount@EDA3639084D8E6296CD95DDD7CC7C5E3;"
		"GetSize@152461390C73587B395ABDB6165C88C7;"
		"GetTime@EDFDEC993D2002D4DBF5C1D0064D2AC3;"
		"EncodeRaw@BC21A2C4AE460B876E4B5D6957C23EC7;"
		"EncodeSlow@8EB96E3F7665DC9B8DB37EF800108458;"
		"EncodeFast@FD783821233DA1B466F8C40529929958;"
		"Clear@6CCE3D6F1AF00194855B0EED0F5914A0;"
	"";
}
