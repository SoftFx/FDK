#include "stdafx.h"
#include "Extended.h"
#include "Level2.h"
#include "TypesSerializer.hpp"



CReturnType CExtended::Do(CInType inArg, CInOutType& inOutArg, COutType& outArg)
{
	inOutArg.Used = inArg.Used;
	outArg.Used = inArg.Used;
	inOutArg.Value2 = inOutArg.Value2 * inOutArg.Value2;
	outArg.Value3 = inArg.Value - 1;

	CReturnType result;
	result.Used = inArg.Used;
	result.Value4 = inArg.Value * 3;
	return result;
}
int CExtended::MarketBuy(const string& symbol, double price, double& volume, double& amount)
{
	amount = volume * price;
	volume = 0;
	return 0;
}

namespace
{
	CLevel2 MakeLevel2()
	{
		CLevel2 result("EUR/USD");
		result.Bids.push_back(CQuoteEntry(1.20001, 100000));
		result.Bids.push_back(CQuoteEntry(1.20002, 100000));
		result.Bids.push_back(CQuoteEntry(1.20003, 100000));
		result.Bids.push_back(CQuoteEntry(1.20004, 100000));
		result.Bids.push_back(CQuoteEntry(1.20005, 100000));


		result.Asks.push_back(CQuoteEntry(1.30001, 100000));
		result.Asks.push_back(CQuoteEntry(1.30002, 100000));
		result.Asks.push_back(CQuoteEntry(1.30003, 100000));
		result.Asks.push_back(CQuoteEntry(1.30004, 100000));
		result.Asks.push_back(CQuoteEntry(1.30005, 100000));

		return result;
	}
	void ReadLevel2Ex(MemoryBuffer& buffer, CLevel2& level2)
	{
		level2.Bids.clear();
		level2.Asks.clear();
		level2.CreatingTime = ReadTime(buffer);
		{
			const size_t count = ReadUInt32(buffer);
			for (size_t index = 0; index < count; ++index)
			{
				const double price = ReadDouble(buffer);
				const double volume = ReadDouble(buffer);

				level2.Bids.push_back(CQuoteEntry(price, volume));
			}
		}
		{
			const size_t count = ReadUInt32(buffer);
			for (size_t index = 0; index < count; ++index)
			{
				const double price = ReadDouble(buffer);
				const double volume = ReadDouble(buffer);

				level2.Asks.push_back(CQuoteEntry(price, volume));
			}
		}
		level2.Symbol = ReadAString(buffer);
	}
}


namespace
{
	void* gHeap = HeapCreate(0, 0, 0);
}


extern "C" void SpeedTest()
{
	CLevel2 level2 = MakeLevel2();
	MemoryBuffer buffer(gHeap);
	WriteLevel2(level2, buffer);


	const size_t count = 16 * 1024 * 1024;
	size_t totalLength = 0;

	const size_t start = GetTickCount();

	for (size_t index = 0; index < count; ++index)
	{
		buffer.SetPosition(0);
		ReadLevel2Ex(buffer, level2);
	}


	const size_t finish = GetTickCount();

	cout<<"count = "<<count<<endl;
	cout<<"total length = "<<totalLength<<endl;
	double interval = (finish - start) / 1000.0;
	cout<<"interval = "<<interval<<endl;
	double speed = count / interval;
	cout<<"speed = "<<speed<<endl;
}


void CExtended::SpeedTest()
{
	


}




namespace
{
	CExtended gExtended;
}

CExtended& GetExtended()
{
	return gExtended;
}

