#include "stdafx.h"
#include "Functions.h"

namespace FDK
{
	double ValidatePositiveOrZeroValue(const char* functionName, const char* argumentName, double value)
	{
		if (_finite(value))
		{
			if (value >= 0)
			{
				return value;
			}
		}
		stringstream stream;
		stream<<functionName<<"(): argument "<<argumentName<<" has out of range value = "<<value;
		string st = stream.str();
		throw runtime_error(st);
	}

	double ValidatePositiveValue(const char* functionName, const char* argumentName, double value)
	{
		if (_finite(value))
		{
			if (value > 0)
			{
				return value;
			}
		}
		stringstream stream;
		stream<<functionName<<"(): argument "<<argumentName<<" has out of range value = "<<value;
		string st = stream.str();
		throw runtime_error(st);
	}

	double ValidateFiniteValue(const char* functionName, const char* argumentName, double value)
	{
		if (_finite(value))
		{
			return value;
		}
		stringstream stream;
		stream<<functionName<<"(): argument "<<argumentName<<" has out of range value = "<<value;
		string st = stream.str();
		throw runtime_error(st);
	}

	double ValidateValueFromZeroToOne(const char* functionName, const char* argumentName, double value)
	{
		if (_finite(value))
		{
			if ((value >= 0) && (value <= 1))
			{
				return value;
			}
		}
		stringstream stream;
		stream<<functionName<<"(): argument "<<argumentName<<" has out of range value = "<<value;
		string st = stream.str();
		throw runtime_error(st);
	}
}