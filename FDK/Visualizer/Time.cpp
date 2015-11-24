#include "stdafx.h"
#include "Visualizer.h"
#include "Config.h"




namespace
{
	const char* cDays[] = {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"};
}
namespace
{
	struct DateTime
	{
		int m_date;
		int m_time;
	};
}

ostream& operator << (ostream& stream, SYSTEMTIME& time)
{
	stream<<setw(4)<<setfill('0')<<time.wYear<<':';
	stream<<setw(2)<<setfill('0')<<time.wMonth<<':';
	stream<<setw(2)<<setfill('0')<<time.wDay;
	if (time.wDayOfWeek < _countof(cDays))
	{
		const char* day = cDays[time.wDayOfWeek];
		stream<<" ("<<day<<")";
	}
	stream<<" - ";
	stream<<setw(2)<<setfill('0')<<time.wHour<<':';
	stream<<setw(2)<<setfill('0')<<time.wMinute<<':';
	stream<<setw(2)<<setfill('0')<<time.wSecond<<".";
	stream<<setw(3)<<setfill('0')<<time.wMilliseconds;
	return stream;
}



VISUALIZER_API HRESULT WINAPI SYSTEMTIME_Format(DWORD address, DEBUGHELPER* helper, int /*base*/, BOOL /*bUniStrings*/, char* buffer, size_t max, DWORD /*reserved*/)
{
	prolog;
	SYSTEMTIME time;
	HRESULT result = debugger.Read(time);
	return_if_failed(result);
	stream<<time;
	return S_OK;
}

VISUALIZER_API HRESULT WINAPI CDateTime_Format(DWORD address, DEBUGHELPER* helper, int /*base*/, BOOL /*bUniStrings*/, char* buffer, size_t max, DWORD /*reserved*/)
{
	prolog;
	__int64 time;
	HRESULT result = debugger.Read(time);
	return_if_failed(result);
	time += 11644473600000;
	time *= 10000;
	SYSTEMTIME systemTime;
	FileTimeToSystemTime(reinterpret_cast<FILETIME*>(&time), &systemTime);
	stream<<systemTime;
	return S_OK;
}

VISUALIZER_API HRESULT WINAPI FILETIME_Format(DWORD address, DEBUGHELPER* helper, int /*base*/, BOOL /*bUniStrings*/, char* buffer, size_t max, DWORD /*reserved*/)
{
	prolog;
	FILETIME file;
	HRESULT result = debugger.Read(file);
	return_if_failed(result);
	SYSTEMTIME time;
	FileTimeToSystemTime(&file, &time);
	stream<<time;
	return S_OK;
}

namespace
{
	/// Magic numbers
	enum 
	{
		SECONDS_PER_DAY = 86400,
		SECONDS_PER_HOUR = 3600,
		SECONDS_PER_MIN = 60,
		MINUTES_PER_HOUR = 60,

		MILLIS_PER_DAY = 86400000,
		MILLIS_PER_HOUR = 3600000,
		MILLIS_PER_MIN = 60000,
		MILLIS_PER_SEC = 1000,

		// time_t epoch (1970-01-01) as a Julian date
		JULIAN_19700101 = 2440588
	};
	void GetYMD(int jday, WORD& year, WORD& month, WORD& day)
	{
		int a = jday + 32044;
		int b = (4 * a + 3) / 146097;
		int c = a - int ((b * 146097) / 4);
		int d = (4 * c + 3) / 1461;
		int e = c - int ((1461 * d) / 4);
		int m = (5 * e + 2) / 153;
		day = e - int ((153 * m + 2) / 5) + 1;
		month = m + 3 - 12 * int (m / 10);
		year = b * 100 + d - 4800 + int (m / 10);
	}
	void GetHMS(int time, WORD& hour, WORD& minute, WORD& second, WORD& millis)
	{
		int ticks = time / MILLIS_PER_SEC;
		hour = static_cast<WORD>(ticks / SECONDS_PER_HOUR);
		minute = static_cast<WORD>((ticks / SECONDS_PER_MIN) % MINUTES_PER_HOUR);
		second = static_cast<WORD>(ticks % SECONDS_PER_MIN);
		millis = static_cast<WORD>(time % MILLIS_PER_SEC);
	}
}



VISUALIZER_API HRESULT WINAPI FixTimeStamp_Format(DWORD address, DEBUGHELPER* helper, int /*base*/, BOOL /*bUniStrings*/, char* buffer, size_t max, DWORD /*reserved*/)
{
	prolog;
	DateTime fixtime;
	int offset = gConfig.IsDestructorVirtual? sizeof(int) : 0;
	HRESULT result = debugger.Read(address + offset, fixtime);
	return_if_failed(result);

	SYSTEMTIME time;
	ZeroMemory(&time, sizeof(time));

	GetYMD(fixtime.m_date, time.wYear, time.wMonth, time.wDay);
	GetHMS(fixtime.m_time, time.wHour, time.wMinute, time.wSecond, time.wMilliseconds);

	// calculate day of weak
	FILETIME fileTemp;
	ZeroMemory(&fileTemp, sizeof(fileTemp));

	SYSTEMTIME systemTemp;
	ZeroMemory(&systemTemp, sizeof(systemTemp));

	SystemTimeToFileTime(&time, &fileTemp);
	FileTimeToSystemTime(&fileTemp, &systemTemp);
	
	time.wDayOfWeek = systemTemp.wDayOfWeek;
	stream<<time;
	return S_OK;
}








