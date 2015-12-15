#include "stdafx.h"
#include "NullLogFactory.h"
namespace
{
	FIX::NullLog gLog;
}


FIX::Log* NullLogFactory::create(const FIX::SessionID& /*id*/)
{
	return &gLog;
}
Log* NullLogFactory::create()
{
	return &gLog;
}
void NullLogFactory::destroy(Log* pLog)
{
	UNREFERENCED_PARAMETER(pLog);
	assert(&gLog == pLog);
}
