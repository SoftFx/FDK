#include "stdafx.h"
#include "Simple.h"

string CSimple::Inverse(const string& text)
{
	string result(text.rbegin(), text.rend());
	return result;
}

bool CSimple::Factorial(int value, int& result)
{
	if (value < 0)
	{
		return false;
	}
	int answer = 1;
	while(value > 1)
	{
		answer *= value;
		--value;
		if (answer < 0)
		{
			return false;
		}
	}
	result = answer;
	return true;
}
