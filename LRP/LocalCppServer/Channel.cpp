#include "stdafx.h"
#include "Channel.h"


namespace
{
	CSimple gSimple;
	CExtended gExtended;
}

CSimple& CChannel::GetSimple()
{
	return gSimple;
}

CExtended& CChannel::GetExtended()
{
	return gExtended;
}