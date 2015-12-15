#include "stdafx.h"
#include "DateTime.h"

CDateTime FxUtcNow()
{
	timeb time;
	ftime(&time);
	int64 result = time.time * 1000 + time.millitm;
	return result;
}

std::ostream&  operator << (std::ostream& stream, const CDateTime& arg)
{
	uint64 ft = arg;
	ft += 11644473600000;
	ft *= 10000;
	SYSTEMTIME st;
	ZeroMemory(&st, sizeof(st));

	FileTimeToSystemTime(reinterpret_cast<const FILETIME*>(&ft), &st);

	stream<<setw(4)<<setfill('0')<<st.wYear<<'.';
	stream<<setw(2)<<setfill('0')<<st.wMonth<<'.';
	stream<<setw(2)<<setfill('0')<<st.wDay;

	stream<<" ";

	stream<<setw(2)<<setfill('0')<<st.wHour<<':';
	stream<<setw(2)<<setfill('0')<<st.wMinute<<':';
	stream<<setw(2)<<setfill('0')<<st.wSecond<<".";
	stream<<setw(3)<<setfill('0')<<st.wMilliseconds;
	return stream;
}