// This is always generated file. Do not change anything.


// handlers of Serializer component
namespace
{
	const unsigned short LrpComponent_Serializer_Id = 0;
	const unsigned short LrpMethod_Serializer_Serialize_Id = 0;
	const unsigned short LrpMethod_Serializer_Deserialize_Id = 1;

	typedef void (*LrpInvoke_Serializer_Method_Handler)(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel);
	void LrpInvoke_Serializer_Serialize(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetSerializer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadCalculatorData(buffer);
		auto result = component.Serialize(arg0);
		buffer.Reset(offset);
		WriteAString(result, buffer);
	}
	void LrpInvoke_Serializer_Deserialize(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetSerializer();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto result = component.Deserialize(arg0);
		buffer.Reset(offset);
		WriteCalculatorData(result, buffer);
	}

	LrpInvoke_Serializer_Method_Handler gSerializerHandlers[] = 
	{
		LrpInvoke_Serializer_Serialize,
		LrpInvoke_Serializer_Deserialize,
	};

	HRESULT LrpInvoke_Serializer(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		if(methodId >= 2)
		{
			return LRP_INVALID_METHOD_ID;
		}
		gSerializerHandlers[methodId](offset, buffer, pChannel); 
		return S_OK;
	}
}


// handlers of FinCalc component
namespace
{
	const unsigned short LrpComponent_FinCalc_Id = 1;
	const unsigned short LrpMethod_FinCalc_Constructor_Id = 0;
	const unsigned short LrpMethod_FinCalc_Destructor_Id = 1;
	const unsigned short LrpMethod_FinCalc_Calculate_Id = 2;
	const unsigned short LrpMethod_FinCalc_Clear_Id = 3;
	const unsigned short LrpMethod_FinCalc_Format_Id = 4;

	typedef void (*LrpInvoke_FinCalc_Method_Handler)(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel);
	void LrpInvoke_FinCalc_Constructor(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFinCalc();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto result = component.Constructor(arg0);
		buffer.Reset(offset);
		WriteLocalPointer(result, buffer);
	}
	void LrpInvoke_FinCalc_Destructor(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFinCalc();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		component.Destructor(arg0);
		buffer.Reset(offset);
	}
	void LrpInvoke_FinCalc_Calculate(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFinCalc();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		component.Calculate(arg0);
		buffer.Reset(offset);
	}
	void LrpInvoke_FinCalc_Clear(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFinCalc();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		component.Clear(arg0);
		buffer.Reset(offset);
	}
	void LrpInvoke_FinCalc_Format(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetFinCalc();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadLocalPointer(buffer);
		auto result = component.Format(arg0);
		buffer.Reset(offset);
		WriteAString(result, buffer);
	}

	LrpInvoke_FinCalc_Method_Handler gFinCalcHandlers[] = 
	{
		LrpInvoke_FinCalc_Constructor,
		LrpInvoke_FinCalc_Destructor,
		LrpInvoke_FinCalc_Calculate,
		LrpInvoke_FinCalc_Clear,
		LrpInvoke_FinCalc_Format,
	};

	HRESULT LrpInvoke_FinCalc(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		if(methodId >= 5)
		{
			return LRP_INVALID_METHOD_ID;
		}
		gFinCalcHandlers[methodId](offset, buffer, pChannel); 
		return S_OK;
	}
}


// components handler
namespace
{
	typedef HRESULT (*LrpInvoke_Component_Handler)(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel);
	LrpInvoke_Component_Handler gHandlers[] = 
	{
		LrpInvoke_Serializer,
		LrpInvoke_FinCalc,
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
	"$Serializer;"
		"Serialize@FF8910FF4BA3A6985A6E718A6D9ABFAC;"
		"Deserialize@A2ECE2C977631B1FC0C5A7518E3CB856;"
	"$FinCalc;"
		"Constructor@9891140067A7206F09C78A3E4B47A617;"
		"Destructor@578A3F61986EADEE8A91305955BD65E4;"
		"Calculate@4890B56FD1CF1BB2E9647BBF19FBB59C;"
		"Clear@6CCE3D6F1AF00194855B0EED0F5914A0;"
		"Format@0129B65989C066C77CB83894056C3B8E;"
	"";
}
