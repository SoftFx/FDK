#pragma once


class CSimple
{
public:
	void* Constructor();
	string Inverse(const string& text);
	bool Factorial(int value, int& result);
};


CSimple& GetSimple();
