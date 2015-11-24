#include "stdafx.h"
#include "Proxy.h"

namespace
{
	const char* cLrpSignatureName = "LrpSignature";
	const char* cLrpInvokeName = "LrpInvoke";
}

CProxy::CProxy(const TCHAR* pipeId, const TCHAR* dllPath) : m_dll(dllPath), m_pipe(pipeId)
{
	HANDLE semaphore = CreateSemaphore(nullptr, 0, 1, pipeId);
	if (nullptr == semaphore)
	{
		throw runtime_error("Couldn't create a new semaphore");
	}
	ReleaseSemaphore(semaphore, 1, nullptr);
	CloseHandle(semaphore);
}


int CProxy::Run()
{
	m_pipe.Accept();
	if (!RunProlog())
	{
		return 1;
	}
	for (;;)
	{
		void* heap = m_heap.Heap();
		MemoryBuffer buffer(heap);

		unsigned short componentId = 0;
		unsigned short methodId = 0;
		m_pipe.ReadBuffer(componentId, methodId, buffer);


		size_t* pSize = buffer.GetSizePointer();
		size_t* pCapacity = buffer.GetCapacityPointer();
		void** ppData = buffer.GetDataPointer();

		unsigned __int32 size = static_cast<unsigned __int32>(buffer.GetSize());
		unsigned __int32 capacity = static_cast<unsigned __int32>(buffer.GetCapacity());

		int result = m_dll.Invoke(componentId, methodId, heap, &size, ppData, &capacity);

		*pSize = size;
		*pCapacity = capacity;
		m_pipe.WriteBuffer(buffer, result);

	}



	return 1;
}

bool CProxy::RunProlog()
{
	MemoryBuffer buffer(m_heap.Heap());
	const bool result = m_dll.IsValid();
	buffer.MovePosition(4);
	WriteBoolean(result, buffer);
	if (result)
	{
		const string signature = m_dll.Signature();
		WriteAString(signature, buffer);
	}
	else
	{
		const DWORD error = m_dll.GetError();
		const string& errorDescription = m_dll.GetErrorDescription();
		WriteUInt32(error, buffer);
		WriteAString(errorDescription, buffer);
	}
	buffer.SetPosition(0);
    WriteUInt32(static_cast<unsigned>(buffer.GetSize()) - 4, buffer);

	m_pipe.WriteBuffer(buffer);
	return result;
}
