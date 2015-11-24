#pragma once


class CSimpleEntry
{
public:
	uint32 Price;
	uint32 Volume;
	int32 Delta;
public:
	inline CSimpleEntry() : Price(), Volume(), Delta()
	{
	}
	inline CSimpleEntry(uint32 price, uint32 volume) : Price(price), Volume(volume), Delta()
	{
	}
};