#pragma once
#include "Logger.h"



template<typename T> string ToString(const T& arg)
{
	stringstream stream;
	stream.precision(DBL_DIG);
	stream<<boolalpha;
	stream<<arg;
	return stream.str();
}
inline const string& ToString(const string& arg)
{
	return arg;
}

ostream& operator << (ostream& stream, SYSTEMTIME& time);

//formatting
string FormatEx(const char* format, const size_t count, string* args);
// template formating
template<size_t count> string FormatEx(const string& format, string (&args)[count])
{
	return FormatEx(format.c_str(), count, args);
}
template<typename P0>
string Format(const string& format, const P0& a0)
{
	string args[1];
	args[0] = ToString(a0);
	return FormatEx(format, args);
}
template<typename P0, typename P1>
string Format(const string& format, const P0& a0, const P1& a1)
{
	string args[2];
	args[0] = ToString(a0);
	args[1] = ToString(a1);
	return FormatEx(format, args);
}
template<typename P0, typename P1, typename P2>
string Format(const string& format, const P0& a0, const P1& a1, const P2& a2)
{
	string args[3];
	args[0] = ToString(a0);
	args[1] = ToString(a1);
	args[2] = ToString(a2);
	return FormatEx(format, args);
}
template<typename P0, typename P1, typename P2, typename P3>
string Format(const string& format, const P0& a0, const P1& a1, const P2& a2, const P3& a3)
{
	string args[4];
	args[0] = ToString(a0);
	args[1] = ToString(a1);
	args[2] = ToString(a2);
	args[3] = ToString(a3);
	return FormatEx(format, args);
}
template<typename P0, typename P1, typename P2, typename P3, typename P4>
string Format(const string& format, const P0& a0, const P1& a1, const P2& a2, const P3& a3, const P4& a4)
{
	string args[5];
	args[0] = ToString(a0);
	args[1] = ToString(a1);
	args[2] = ToString(a2);
	args[3] = ToString(a3);
	args[4] = ToString(a4);
	return Format(format, 5, args);
}
