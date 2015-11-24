#include "stdafx.h"
#include "Formatting.h"


namespace
{
	const char* DoFormat(const char* format, const size_t count, string* args, stringstream& stream)
	{
		const char* result = strchr(format, '{');
		if (nullptr == result)
		{
			size_t length = strlen(format);
			stream.write(format, length);
			return nullptr;
		}
		stream.write(format, static_cast<size_t>(result - format));
		if (0 == *result)
		{
			return result;
		}
		++result;
		char* end = nullptr;
		size_t index = strtoul(result, &end, 10);
		if (end == result)
		{
			throw runtime_error("Could not parse argument index");
		}
		if (index >= count)
		{
			throw runtime_error("Argument index is out of range");
		}
		if ('}' != *end)
		{
			throw runtime_error("Argument index formatting is out of range");
		}
		result = 1 + end;
		stream<<args[index];
		return result;
	}
}

string FormatEx(const char* format, const size_t count, string* args)
{
	stringstream stream;
	do
	{
		format = DoFormat(format, count, args, stream);
	} while (nullptr != format);
	string result = stream.str();
	return result;
}

ostream& operator<<(ostream& stream, SYSTEMTIME& time)
{
	stream<<setw(4)<<setfill('0')<<time.wYear<<':';
	stream<<setw(2)<<setfill('0')<<time.wMonth<<':';
	stream<<setw(2)<<setfill('0')<<time.wDay;
	stream<<" ";
	stream<<setw(2)<<setfill('0')<<time.wHour<<':';
	stream<<setw(2)<<setfill('0')<<time.wMinute<<':';
	stream<<setw(2)<<setfill('0')<<time.wSecond<<".";
	stream<<setw(3)<<setfill('0')<<time.wMilliseconds;
	return stream;
}
