#pragma once


class CQuoteEntry
{
public:
	double Price;
	double Volume;
public:
	inline CQuoteEntry() : Price(), Volume()
	{
	}
	inline CQuoteEntry(double price, double volume) : Price(price), Volume(volume)
	{
	}
};