#include "stdafx.h"
#include "LrpPipe.h"

namespace
{
	const size_t cLargeSize = 1024 * 1024;
}


namespace
{
	struct __declspec(align(4)) CResultSize
	{
	public:
		int Result;
		int Size;
	public:
		inline CResultSize(int result, int size) : Result(result), Size(size)
		{
		}
	};
	struct __declspec(align(4)) CComponentMethodSize
	{
	public:
		unsigned short ComponentId;
		unsigned short MethodId;
		unsigned int Size;
	public:
		CComponentMethodSize() : ComponentId(), MethodId(), Size()
		{
		}
	};
}


CLrpPipe::CLrpPipe(const TCHAR* pipeId) :m_pipe(INVALID_HANDLE_VALUE)
{
	tstring pipeName = TEXT("\\\\.\\pipe\\");
	pipeName += pipeId;
	m_pipe = CreateNamedPipe(pipeName.c_str(), PIPE_ACCESS_DUPLEX, PIPE_TYPE_BYTE | PIPE_WAIT, 1, PIPE_BUFFER_SIZE, PIPE_BUFFER_SIZE, INFINITE, nullptr);
	if (INVALID_HANDLE_VALUE == m_pipe)
	{
		throw runtime_error("Couldn't create a new named pipe");
	}
}
CLrpPipe::~CLrpPipe()
{
	FlushFileBuffers(m_pipe);
	DisconnectNamedPipe(m_pipe);
	CloseHandle(m_pipe);
	m_pipe = INVALID_HANDLE_VALUE;
}

void CLrpPipe::Accept()
{
	if (!ConnectNamedPipe(m_pipe, nullptr))
	{
		const DWORD error = GetLastError();
		if (ERROR_PIPE_CONNECTED != error)
		{
			throw runtime_error("Couldn't accept clients");
		}
	}
}

void CLrpPipe::WriteData(size_t count, const void* buffer)
{
	const char* data = reinterpret_cast<const char*>(buffer);
	for (; count > 0; )
	{
		DWORD writtenCount = 0;
		if (!WriteFile(m_pipe, data, static_cast<DWORD>(count), &writtenCount, nullptr) || (0 == writtenCount))
		{
			throw runtime_error("Couldn't write data");
		}
		data += writtenCount;
		count -= writtenCount;
	}
}

void CLrpPipe::WriteBuffer(const MemoryBuffer& buffer)
{
	const void* data = buffer.GetData();
	size_t count = buffer.GetSize();
	WriteData(count, data);
}
void CLrpPipe::WriteBuffer(const MemoryBuffer& buffer, int result)
{
	CResultSize prolog(result, static_cast<int>(buffer.GetSize()));
	WriteData(sizeof(prolog), &prolog);

	unsigned int count = static_cast<unsigned>(buffer.GetSize());
	const void* data = buffer.GetData();
	WriteData(count, data);
}
void CLrpPipe::ReadBuffer(unsigned short& componentId, unsigned short& methodId, MemoryBuffer& buffer)
{
	CComponentMethodSize prolog;
	ReadData(sizeof(prolog), &prolog);

	componentId = prolog.ComponentId;
	methodId = prolog.MethodId;

	unsigned int size = prolog.Size;

	buffer.Construct(size);
	ReadData(size, buffer.GetData());
}
void CLrpPipe::ReadData(size_t count, void* buffer)
{
	char* data = reinterpret_cast<char*>(buffer);
	for (; count > 0;)
	{
		DWORD readCount = 0;
		if (!ReadFile(m_pipe, data, static_cast<DWORD>(count), &readCount, nullptr) || (0 == readCount))
		{
			throw runtime_error("Couldn't read data");
		}
		data += readCount;
		count -= readCount;
	}
}

