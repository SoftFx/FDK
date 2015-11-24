#include "stdafx.h"
#include "Channel.h"



CSimple& CChannel::GetSimple()
{
	return m_simple;
}

CExtended& CChannel::GetExtended()
{
	return m_extended;
}