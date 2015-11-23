// This is always generated file. Do not change anything.


// handlers of FixCodec component
namespace
{
	const unsigned short LrpComponent_FixCodec_Id = 0;
	const unsigned short LrpMethod_FixCodec_Constructor_Id = 0;
	const unsigned short LrpMethod_FixCodec_Destructor_Id = 1;
	const unsigned short LrpMethod_FixCodec_GetCount_Id = 2;
	const unsigned short LrpMethod_FixCodec_GetSize_Id = 3;
	const unsigned short LrpMethod_FixCodec_GetTime_Id = 4;
	const unsigned short LrpMethod_FixCodec_EncodeSlow_Id = 5;
	const unsigned short LrpMethod_FixCodec_EncodeFast_Id = 6;
	const unsigned short LrpMethod_FixCodec_Clear_Id = 7;

	typedef void (*LrpInvoke_FixCodec_Method_Handler)(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel);
	void LrpInvoke_FixCodec_Constructor(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFixCodec();
		component; // if all methods of component are static then the next line generates warning #4189
		auto result = component.Constructor();
		buffer.Reset(offset);
		WriteLocalPointer(result, buffer);
	}
	void LrpInvoke_FixCodec_Destructor(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFixCodec();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		component.Destructor(arg0);
		buffer.Reset(offset);
	}
	void LrpInvoke_FixCodec_GetCount(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFixCodec();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.GetCount(arg0);
		buffer.Reset(offset);
		WriteInt64(result, buffer);
	}
	void LrpInvoke_FixCodec_GetSize(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFixCodec();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.GetSize(arg0);
		buffer.Reset(offset);
		WriteInt64(result, buffer);
	}
	void LrpInvoke_FixCodec_GetTime(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFixCodec();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.GetTime(arg0);
		buffer.Reset(offset);
		WriteDouble(result, buffer);
	}
	void LrpInvoke_FixCodec_EncodeSlow(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFixCodec();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadQuoteArray(buffer);
		component.EncodeSlow(arg0, arg1);
		buffer.Reset(offset);
	}
	void LrpInvoke_FixCodec_EncodeFast(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFixCodec();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto arg1 = ReadQuoteArray(buffer);
		component.EncodeFast(arg0, arg1);
		buffer.Reset(offset);
	}
	void LrpInvoke_FixCodec_Clear(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFixCodec();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		component.Clear(arg0);
		buffer.Reset(offset);
	}

	LrpInvoke_FixCodec_Method_Handler gFixCodecHandlers[] = 
	{
		LrpInvoke_FixCodec_Constructor,
		LrpInvoke_FixCodec_Destructor,
		LrpInvoke_FixCodec_GetCount,
		LrpInvoke_FixCodec_GetSize,
		LrpInvoke_FixCodec_GetTime,
		LrpInvoke_FixCodec_EncodeSlow,
		LrpInvoke_FixCodec_EncodeFast,
		LrpInvoke_FixCodec_Clear,
	};

	HRESULT LrpInvoke_FixCodec(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		if(methodId >= 8)
		{
			return LRP_INVALID_METHOD_ID;
		}
		gFixCodecHandlers[methodId](offset, buffer, pChannel); 
		return S_OK;
	}
}


// components handler
namespace
{
	typedef HRESULT (*LrpInvoke_Component_Handler)(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel);
	LrpInvoke_Component_Handler gHandlers[] = 
	{
		LrpInvoke_FixCodec,
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
	"$FixCodec;"
		"Constructor@208BBD5C462457D264AC717DBD4C2FF4;"
		"Destructor@578A3F61986EADEE8A91305955BD65E4;"
		"GetCount@EDA3639084D8E6296CD95DDD7CC7C5E3;"
		"GetSize@152461390C73587B395ABDB6165C88C7;"
		"GetTime@EDFDEC993D2002D4DBF5C1D0064D2AC3;"
		"EncodeSlow@8EB96E3F7665DC9B8DB37EF800108458;"
		"EncodeFast@935E9A9001C8A051B896EE6EB47EAD58;"
		"Clear@6CCE3D6F1AF00194855B0EED0F5914A0;"
	"";
}
