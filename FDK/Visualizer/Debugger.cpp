#include "stdafx.h"
#include "Debugger.h"

namespace
{
	struct StdString
	{
		void* DebugStub;
		union _Data
		{
			char Buffer[16];
			DWORD Pointer;
		}Data;
		size_t Size;
		size_t Resource;
	};
}


HRESULT ReadStdString(Debugger debugger, DWORD address, DWORD maximumLength, string& st)
{
	st.clear();
	return_if_true(0 == maximumLength, S_OK)	
	StdString temp;
	ZeroMemory(&temp, sizeof(temp));

	HRESULT result = debugger.Read(address, temp);
	return_if_failed(result);

	try
	{
		DWORD size = min<DWORD>(temp.Size, maximumLength);
		if (temp.Resource <= 16)
		{
			st.insert(st.end(), temp.Data.Buffer, temp.Data.Buffer + size);
			return S_OK;
		}
		else
		{
			st.insert(st.end(), size, 0);
			address = temp.Data.Pointer;
			char* ptr = &st.front();
			result = debugger.ReadBuffer(address, ptr, size);
		}
	}
	catch (exception&)
	{	
		result = E_FAIL;
	}
	return result;
}