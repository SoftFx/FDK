#include "stdafx.h"
#include "Functions.h"
#include "Sockets.h"



void ReadFailed(const size_t count)
{
	CSockets sockets;
	sockets.Initialize();


	const uint64 start = GetTickCount64();

	for (size_t index = 0; index < count; ++index)
	{
		char ch;
		recv(sockets.Server, &ch, sizeof(ch), 0);
	}

	const uint64 finish = GetTickCount64();

	double speed = (double)(count * 1000);
	speed /= (finish - start);
	cout<<"read failed = "<<speed<<endl;

}

void WriteFailed(const size_t count)
{
	CSockets sockets;
	sockets.Initialize();

	for (;;)
	{
		char ch;
		const int status = send(sockets.Server, &ch, sizeof(ch), 0);
		if (-1 == status)
		{
			break;
		}
	}


	const uint64 start = GetTickCount64();

	for (size_t index = 0; index < count; ++index)
	{
		char ch;
		send(sockets.Server, &ch, sizeof(ch), 0);
	}

	const uint64 finish = GetTickCount64();

	double speed = (double)(count * 1000);
	speed /= (finish - start);
	cout<<"write failed = "<<speed<<endl;
}


namespace
{
	DWORD __stdcall WriteThread(void* arg)
	{
		char buffer[16 * 1024];

		CSockets* pSockets = reinterpret_cast<CSockets*>(arg);
		for (; pSockets->Continue;)
		{
			send(pSockets->Client, buffer, sizeof(buffer), 0);
		}
		return 0;
	}
}

void ReadSuccess(const size_t count)
{
	CSockets sockets;
	sockets.Initialize();

	HANDLE thread = CreateThread(nullptr, 0, &WriteThread, &sockets, 0, nullptr);

	size_t counter = 0;
	const uint64 start = GetTickCount64();

	for (size_t index = 0; index < count; ++index)
	{
		char ch;
		if (recv(sockets.Server, &ch, sizeof(ch), 0) > 0)
		{
			counter++;
		}
	}

	const uint64 finish = GetTickCount64();

	double speed = (double)(count * 1000);
	speed /= (finish - start);

	double factor = (double)counter;
	factor /= counter;

	cout<<"read success = "<<speed<<"; factor = "<<factor<<endl;


	sockets.Continue = false;

	for (;;)
	{
		char buffer[16 * 1024];
		recv(sockets.Server, buffer, sizeof(buffer), 0);
		const DWORD status = WaitForSingleObject(thread, 0);
		if (WAIT_OBJECT_0 == status)
		{
			break;
		}
	}
	CloseHandle(thread);
}



namespace
{
	DWORD __stdcall ReadThread(void* arg)
	{
		char buffer[16 * 1024];

		CSockets* pSockets = reinterpret_cast<CSockets*>(arg);
		for (; pSockets->Continue;)
		{
			recv(pSockets->Client, buffer, sizeof(buffer), 0);
		}
		return 0;
	}
}

void WriteSuccess(const size_t count)
{
	CSockets sockets;
	sockets.Initialize();

	HANDLE thread = CreateThread(nullptr, 0, &ReadThread, &sockets, 0, nullptr);

	size_t counter = 0;
	const uint64 start = GetTickCount64();

	for (size_t index = 0; index < count; ++index)
	{
		char ch;
		if (send(sockets.Server, &ch, sizeof(ch), 0) > 0)
		{
			counter++;
		}
	}

	const uint64 finish = GetTickCount64();

	double speed = (double)(count * 1000);
	speed /= (finish - start);

	double factor = (double)counter;
	factor /= counter;

	cout<<"write success = "<<speed<<"; factor = "<<factor<<endl;


	sockets.Continue = false;

	for (;;)
	{
		char buffer[16 * 1024];
		send(sockets.Server, buffer, sizeof(buffer), 0);
		const DWORD status = WaitForSingleObject(thread, 0);
		if (WAIT_OBJECT_0 == status)
		{
			break;
		}
	}
	CloseHandle(thread);
}





int main(int argc, char* argv[])
{
	size_t count = 0;
	if (!Parse(argc, argv, count))
	{
		PrintUsage();
		return 1;
	}

	try
	{
		InitializeSocketsLibrary();
		ReadFailed(count);
		WriteFailed(count);
		ReadSuccess(count);
		WriteSuccess(count);
	}
	catch (const exception& ex)
	{
		cout<<ex.what()<<endl;
	}
	return 0;
}

