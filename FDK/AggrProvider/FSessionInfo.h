#pragma once

class CBinaryReader;

class CFSessionInfo
{
public:
	CFSessionInfo();
public:
	CDateTime StartTime;
	CDateTime EndTime;
	CDateTime OpenTime;
	CDateTime CloseTime;
	int PlatformTimezoneOffset;
private:
	friend CBinaryReader& operator >> (CBinaryReader& stream, CFSessionInfo& info);
	friend ostream& operator << (ostream& stream, const CFSessionInfo& info);
};
