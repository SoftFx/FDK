#include "stdafx.h"


namespace
{
	const size_t cMaximumBufferSize = 64 * 1024;
}


void PrintUsage()
{
	cout<<"Usage:"<<endl;
	cout<<"\tMemCpy <chunk size in bytes> <chunks number in thousands> <loops number>"<<endl;
	cout<<"\t <chunk size in bytes> can not be more than 64 kbytes"<<endl;
	cout<<"Example:"<<endl;
	cout<<"\tMemCpy 4096 256 16"<<endl;
}

bool Parse(int argc, char** argv, size_t& chunkSizeInBytes, size_t& chunksNumber, size_t& count)
{
	if (4 != argc)
	{
		return false;
	}
	chunkSizeInBytes = atoi(argv[1]);
	chunksNumber = atoi(argv[2]);
	count = atoi(argv[3]);

	size_t newCount = count * 1000;
	if (newCount < count)
	{
		return false;
	}
	if ((0 == chunkSizeInBytes) || (chunkSizeInBytes > cMaximumBufferSize))
	{
		return false;
	}
	if (0 == chunksNumber)
	{
		return false;
	}
	count = newCount;
	return true;
}


void SpeedTest(const vector<char*>& buffers, const size_t chunkSizeInBytes, const size_t count)
{
	char stack[cMaximumBufferSize];

	const uint64 start = GetTickCount64();
	size_t result = 0;


	for (size_t step = 0; step < count; ++step)
	{
		for each(const auto element in buffers)
		{
			void* ptr = memcpy(stack, element, chunkSizeInBytes);
			result += reinterpret_cast<size_t>(ptr);
		}
	}

	const uint64 finish = GetTickCount64();

	double speed = (double)buffers.size();
	speed *= count;
	speed *= 1000;
	speed /= (finish - start);

	cout<<"result = "<<result<<endl;
	cout<<"speed = "<<speed<<endl;
}

int main(int argc, char* argv[])
{
	size_t chunkSizeInBytes = 0;
	size_t chunkNumbers = 0;
	size_t count = 0;
	if (!Parse(argc, argv, chunkSizeInBytes, chunkNumbers, count))
	{
		PrintUsage();
		return 1;
	}

	vector<char*> buffers;
	try
	{
		buffers.reserve(chunkNumbers);
		for (size_t index = 0; index < chunkNumbers; ++index)
		{
			char* data = new char[chunkSizeInBytes];
			buffers.push_back(data);
		}
		SpeedTest(buffers, chunkSizeInBytes, count);
	}
	catch (const std::exception& ex)
	{
		cout<<ex.what()<<endl;
	}

	for each(auto element in buffers)
	{
		delete[] element;
	}
	buffers.clear();
	return 0;
}

