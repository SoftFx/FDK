// WaitForReleaseEvent.cpp : Defines the entry point for the console application.
//

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

void WaitForReleaseOneThread(size_t count)
{
	HANDLE event = CreateEvent(nullptr, false, false, nullptr);
	SetEvent(event);

    const ULONG start = (ULONG)GetTickCount64();

	for (size_t index = 0; index < count; ++index)
	{
		WaitForSingleObject(event, INFINITE);
		SetEvent(event);
	}

    const ULONG finish = (ULONG)GetTickCount64();

	CloseHandle(event);
	event = nullptr;

	const size_t duration = static_cast<size_t>(finish - start);

	const double speed = count * 1000.0 / duration;


	cout<<"WaitForRelease"<<endl;
	cout<<"\tduration = "<<duration<<", speed = "<<speed<<endl;
}

void WaitForSuccess(size_t count)
{
	HANDLE event = CreateEvent(nullptr, true, true, nullptr);
	

    const ULONG start = (ULONG)GetTickCount64();

	for (size_t index = 0; index < count; ++index)
	{
		WaitForSingleObject(event, INFINITE);
	}

    const ULONG finish = (ULONG)GetTickCount64();

	CloseHandle(event);
	event = nullptr;

	const size_t duration = static_cast<size_t>(finish - start);

	const double speed = count * 1000.0 / duration;


	cout<<"WaitForSuccess"<<endl;
	cout<<"\tduration = "<<duration<<", speed = "<<speed<<endl;
}

void WaitForTimeout(size_t count)
{
	HANDLE event = CreateEvent(nullptr, false, false, nullptr);


    const ULONG start = (ULONG)GetTickCount64();

	for (size_t index = 0; index < count; ++index)
	{
		WaitForSingleObject(event, 0);
	}

    const ULONG finish = (ULONG)GetTickCount64();

	CloseHandle(event);
	event = nullptr;

	const size_t duration = static_cast<size_t>(finish - start);

	const double speed = count * 1000.0 / duration;


	cout<<"WaitForTimeout"<<endl;
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
	WaitForReleaseOneThread(count);
	cout<<endl;
	WaitForSuccess(count);
	cout<<endl;
	WaitForTimeout(count);
	

	return 0;
}

