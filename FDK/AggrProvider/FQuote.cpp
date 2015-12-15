#include "stdafx.h"
#include "FQuote.h"
namespace
{
	const FIX::UtcTimeStamp cZero(time_t(0));
}

CFQuote::CFQuote() : Price(), Volume(), Setting(cZero), Expiration(cZero)
{

}
CBinaryReader& operator>>(CBinaryReader& stream, CFQuote& arg)
{
	stream>>arg.EntryId;
	stream>>arg.Price;
	stream>>arg.Volume;
	stream>>arg.Setting;
	stream>>arg.Expiration;
	return stream;
}


