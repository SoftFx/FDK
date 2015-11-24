#include "stdafx.h"
#include "FAccountInfo.h"
#include "BinaryReadHelpers.h"

CFAccountInfo::CFAccountInfo() : Equity(), FreeMargin()
{
}
CBinaryReader& operator>>(CBinaryReader& stream, CFAccountInfo& info)
{
	stream>>info.Equity>>info.FreeMargin;
	return stream;
}
bool operator == (const CFAccountInfo& first, const CFAccountInfo& second)
{
	return (first.Equity == second.Equity) && (first.FreeMargin == second.FreeMargin);
}
bool operator != (const CFAccountInfo& first, const CFAccountInfo& second)
{
	return (first.Equity != second.Equity) || (first.FreeMargin != second.FreeMargin);
}
ostream& operator << (ostream& stream, const CFAccountInfo& info)
{
	stream<<"Equity = "<<info.Equity;
	stream<<"; FreeMargin = "<<info.FreeMargin;
	stream<<";";
	return stream;
}