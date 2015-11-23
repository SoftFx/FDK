#include "stdafx.h"
#include "FSessionInfo.h"
#include "BinaryReader.h"

CFSessionInfo::CFSessionInfo() : PlatformTimezoneOffset()
{

}

CBinaryReader& operator>>(CBinaryReader& stream, CFSessionInfo& info)
{
	stream>>info.StartTime;
	stream>>info.EndTime;
	stream>>info.OpenTime;
	stream>>info.CloseTime;
	stream>>info.PlatformTimezoneOffset;
	return stream;
}
ostream& operator << (ostream& stream, const CFSessionInfo& info)
{
	stream<<"StartTime = "<<info.StartTime;
	stream<<"; EndTime = "<<info.EndTime;
	stream<<"; OpenTime = "<<info.OpenTime;
	stream<<"; CloseTime = "<<info.CloseTime;
	stream<<"; PlatformTimezoneOffset = "<<info.PlatformTimezoneOffset;
	stream<<";";
	return stream;
}