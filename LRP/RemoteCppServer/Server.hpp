// This is always generated file. Do not change anything.


// handlers of Simple component
namespace
{
	const unsigned short LrpComponent_Simple_Id = 0;
	const unsigned short LrpMethod_Simple_Inverse_Id = 0;
	const unsigned short LrpMethod_Simple_Factorial_Id = 1;

	typedef void (*LrpInvoke_Simple_Method_Handler)(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel);
	void LrpInvoke_Simple_Inverse(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetSimple();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto result = component.Inverse(arg0);
		buffer.Reset(offset);
		WriteAString(result, buffer);
	}
	void LrpInvoke_Simple_Factorial(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetSimple();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadInt32(buffer);
		auto arg1 = __int32();
		auto result = component.Factorial(arg0, arg1);
		buffer.Reset(offset);
		WriteInt32(arg1, buffer);
		WriteBoolean(result, buffer);
	}

	LrpInvoke_Simple_Method_Handler gSimpleHandlers[] = 
	{
		LrpInvoke_Simple_Inverse,
		LrpInvoke_Simple_Factorial,
	};

	HRESULT LrpInvoke_Simple(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		if(methodId >= 2)
		{
			return LRP_INVALID_METHOD_ID;
		}
		gSimpleHandlers[methodId](offset, buffer, pChannel); 
		return S_OK;
	}
}


// handlers of Extended component
namespace
{
	const unsigned short LrpComponent_Extended_Id = 1;
	const unsigned short LrpMethod_Extended_Do_Id = 0;
	const unsigned short LrpMethod_Extended_MarketBuy_Id = 1;
	const unsigned short LrpMethod_Extended_Update_Id = 2;

	typedef void (*LrpInvoke_Extended_Method_Handler)(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel);
	void LrpInvoke_Extended_Do(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetExtended();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadInType(buffer);
		auto arg1 = ReadInOutType(buffer);
		auto arg2 = COutType();
		auto result = component.Do(arg0, arg1, arg2);
		buffer.Reset(offset);
		WriteInOutType(arg1, buffer);
		WriteOutType(arg2, buffer);
		WriteReturnType(result, buffer);
	}
	void LrpInvoke_Extended_MarketBuy(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetExtended();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadAString(buffer);
		auto arg1 = ReadDouble(buffer);
		auto arg2 = ReadDouble(buffer);
		auto arg3 = double();
		auto result = component.MarketBuy(arg0, arg1, arg2, arg3);
		buffer.Reset(offset);
		WriteDouble(arg2, buffer);
		WriteDouble(arg3, buffer);
		WriteInt32(result, buffer);
	}
	void LrpInvoke_Extended_Update(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100
		auto& component = pChannel->GetExtended();
		component; // if all methods of component are static then the next line generates warning #4189
		auto arg0 = ReadPositionReports(buffer);
		auto arg1 = ReadPositionReports2(buffer);
		component.Update(arg0, arg1);
		buffer.Reset(offset);
		WritePositionReports(arg0, buffer);
		WritePositionReports2(arg1, buffer);
	}

	LrpInvoke_Extended_Method_Handler gExtendedHandlers[] = 
	{
		LrpInvoke_Extended_Do,
		LrpInvoke_Extended_MarketBuy,
		LrpInvoke_Extended_Update,
	};

	HRESULT LrpInvoke_Extended(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)
	{
		if(methodId >= 3)
		{
			return LRP_INVALID_METHOD_ID;
		}
		gExtendedHandlers[methodId](offset, buffer, pChannel); 
		return S_OK;
	}
}


// components handler
namespace
{
	typedef HRESULT (*LrpInvoke_Component_Handler)(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel);
	LrpInvoke_Component_Handler gHandlers[] = 
	{
		LrpInvoke_Simple,
		LrpInvoke_Extended,
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

namespace
{
	HRESULT __stdcall LrpInvoke(size_t componentId, size_t methodId, MemoryBuffer& buffer, void* handle)
	{
		LrpChannel* pChannel = reinterpret_cast<LrpChannel*>(handle);
		HRESULT result = LrpInvokeEx(16, componentId, methodId, buffer, pChannel);
		buffer.SetPosition(12);
		WriteInt32(result, buffer);
		return result;
	}
}
extern "C" const char* __stdcall LrpSignature()
{
	return 
	"$Exceptions;"
	"$Simple;"
		"Inverse@ADF364EFED3D0F564F4D4A36853F2229;"
		"Factorial@939C1201433D52418FDB538296DCE114;"
	"$Extended;"
		"Do@BEEE63AC422F3523B9B5C153C72F45F4;"
		"MarketBuy@E702CD1024421011387D175E1AEACF8B;"
		"Update@5C78A3BBFEC16165EBFA922EA8AC13F3;"
	"";
}
