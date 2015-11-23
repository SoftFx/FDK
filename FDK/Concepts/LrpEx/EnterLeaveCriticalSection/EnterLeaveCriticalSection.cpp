#include "stdafx.h"
#include "Loop.h"

void PrintUsage()
{
	cout<<"Usage:"<<endl;
	cout<<"\tEnterLeaveCriticalSection <threads number> <iterations in millions>"<<endl;
	cout<<"\tthreads number should be not more than 64"<<endl;
	cout<<"Example:"<<endl;
	cout<<"\tEnterLeaveCriticalSection 2 16"<<endl;
}

bool Parse(int argc, char** argv, size_t& threadsNumber, size_t& count)
{
	if (3 != argc)
	{
		return false;
	}
	threadsNumber = atoi(argv[1]);
	count = atoi(argv[2]);

	if ((threadsNumber > 64) || (0 == threadsNumber))
	{
		return false;
	}

	size_t newCount = count * 1000000;
	if (newCount < count)
	{
		return false;
	}
	count = newCount;
	return true;
}




int main(int argc, char* argv[])
{
	size_t threadsNumber = 0;
	size_t count = 0;
	if (!Parse(argc, argv, threadsNumber, count))
	{
		PrintUsage();
		return 1;
	}

	HANDLE event = CreateEvent(nullptr, true, false, nullptr);
	CRITICAL_SECTION synchronizer;
	InitializeCriticalSection(&synchronizer);

	vector<CLoop> loops;


	try
	{
		cout<<"reserving memory"<<endl;
		for (size_t index = 0; index < threadsNumber; ++index)
		{
			loops.push_back(CLoop(count, event, &synchronizer));
		}
		cout<<"memory has been reserved"<<endl<<endl;

		cout<<"creating threads"<<endl;
		for (size_t index = 0; index < threadsNumber; ++index)
		{
			loops[index].Construct();
		}
		cout<<"threads have been reserved"<<endl<<endl;

		cout<<"waiting for DllMain"<<endl;
		Sleep(5000);
		cout<<"starting"<<endl;

		SetEvent(event);

		for each(const auto& element in loops)
		{
			element.WaitFor();
		}
		cout<<"all threads have been finished"<<endl;

		cout<<"threads = "<<threadsNumber<<endl;
		cout<<"#\tduration\tspeed"<<endl;
		size_t theBestDuration = numeric_limits<size_t>::max();
		size_t theWorstDuration = 0;
		size_t theBestIndex = 0;
		size_t theWorstIndex = 0;

		for (size_t index = 0; index < threadsNumber; ++index)
		{
			const CLoop& loop = loops[index];
			const size_t duration = loop.GetDuration();
			if (duration < theBestDuration)
			{
				theBestDuration = duration;
				theBestIndex = index;
			}
			if (duration > theWorstDuration)
			{
				theWorstDuration = duration;
				theWorstIndex = index;
			}
			cout<<index<<'\t'<<loop<<endl;
		}
		cout<<"the best:"<<endl;
		cout<<loops[theBestIndex]<<endl;

		cout<<"the worst:"<<endl;
		cout<<loops[theWorstIndex]<<endl;

	}
	catch (const exception& ex)
	{
		cout<<ex.what()<<endl;
	}

	DeleteCriticalSection(&synchronizer);
	CloseHandle(event);

	return 0;
}

