#pragma once

namespace FDK
{
	double ValidatePositiveOrZeroValue(const char* functionName, const char* argumentName, double value);
	double ValidatePositiveValue(const char* functionName, const char* argumentName, double value);
	double ValidateFiniteValue(const char* functionName, const char* argumentName, double value);
	double ValidateValueFromZeroToOne(const char* functionName, const char* argumentName, double value);
}