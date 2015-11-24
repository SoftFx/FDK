#pragma once

#include "TagEntry.h"

namespace FDK
{
	void Process(const char* name, const std::string& value, std::ostream& stream);
	void Process(const char* name, const double value, std::ostream& stream);
	void Process(const char* name, const Nullable<double> value, std::ostream& stream);
	void Process(const char* name, const CTagEntry value, std::ostream& stream);

	template<typename T> void Process(const char* name, const T value, std::ostream& stream)
	{
		if (nullptr != name)
		{
			stream << name << " = ";
		}
		stream << value << '(' << static_cast<int>(value) << "); ";
	}


	void ValidateVerbatimText(const char* text, std::istream& stream);
	void ValidateVerbatimText(const std::string& text, std::istream& stream);
	void ValidateEndOfStream(std::istream& stream);


	void Process(const char* name, std::string& value, std::istream& stream);
	void Process(const char* name, double& value, std::istream& stream);
	void Process(const char* name, Nullable<double>& value, std::istream& stream);
	void Process(const char* name, CTagEntry& value, std::istream& stream);

	int ReadEnumValue(const char* name, std::istream& stream);

	template<typename T> void Process(const char* name, T& value, std::istream& stream)
	{
		value = static_cast<T>(ReadEnumValue(name, stream));
	}
}