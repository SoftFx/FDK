#include "stdafx.h"






void PrintUsage()
{
	cout<<"Usage:"<<endl;
	cout<<"\tWaitForReleaseEvent <iterations in millions>"<<endl;
	cout<<"Example:"<<endl;
	cout<<"\tWaitForReleaseEvent 16"<<endl;
}

bool Parse(int argc, char** argv, size_t& count)
{
	if (2 != argc)
	{
		return false;
	}
	count = atoi(argv[1]);

	size_t newCount = count * 1000000;
	if (newCount < count)
	{
		return false;
	}
	count = newCount;
	return true;
}



void TryEnterLeave(size_t count)
{
	CRITICAL_SECTION synchronizer;
	InitializeCriticalSection(&synchronizer);

    const ULONG start = (ULONG)GetTickCount64();

	for (size_t index = 0; index < count; ++index)
	{
		if (TryEnterCriticalSection(&synchronizer))
		{
			LeaveCriticalSection(&synchronizer);
		}
	}

    const ULONG finish = (ULONG)GetTickCount64();

	DeleteCriticalSection(&synchronizer);

	const size_t duration = static_cast<size_t>(finish - start);

	const double speed = count * 1000.0 / duration;


	cout<<"TryEnterLeave"<<endl;
	cout<<"\tduration = "<<duration<<", speed = "<<speed<<endl;
}


namespace
{
	DWORD __stdcall LockCriticalSection(void* arg)
	{
		CRITICAL_SECTION* pSynchronizer = reinterpret_cast<CRITICAL_SECTION*>(arg);
		EnterCriticalSection(pSynchronizer);
		return 0;
	}
}



void TryEnterFailed(size_t count)
{
	CRITICAL_SECTION synchronizer;
	InitializeCriticalSection(&synchronizer);

	HANDLE thread = CreateThread(nullptr, 0, &LockCriticalSection, &synchronizer, 0, nullptr);
	WaitForSingleObject(thread, INFINITE);
	CloseHandle(thread);
	thread = nullptr;

	if (TryEnterCriticalSection(&synchronizer))
	{
		cout<<"TryEnterFailed(): Incorrect behavior"<<endl;
	}

    const ULONG start = (ULONG)GetTickCount64();

	for (size_t index = 0; index < count; ++index)
	{
		TryEnterCriticalSection(&synchronizer);
	}

    const ULONG finish = (ULONG)GetTickCount64();

	DeleteCriticalSection(&synchronizer);

	const size_t duration = static_cast<size_t>(finish - start);
	const double speed = count * 1000.0 / duration;

	cout<<"TryEnterFailed"<<endl;
	cout<<"\tduration = "<<duration<<", speed = "<<speed<<endl;
}

int main(int argc, char* argv[])
{
	size_t count = 0;
	if (!Parse(argc, argv, count))
	{
		PrintUsage();
		return 1;
	}

	TryEnterLeave(count);
	TryEnterFailed(count);



	return 0;
}

