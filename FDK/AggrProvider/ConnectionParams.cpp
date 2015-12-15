#include "stdafx.h"
#include "ConnectionParams.h"

CConnectionParams::CConnectionParams() : Port()
{
}
bool operator < (const CConnectionParams& first, const CConnectionParams& second)
{
	if (first.Address < second.Address)
	{
		return true;
	}
	else if (first.Address > second.Address)
	{
		return false;
	}

	if (first.Port < second.Port)
	{
		return true;
	}
	else if (first.Port > second.Port)
	{
		return false;
	}

	if (first.Username < second.Username)
	{
		return true;
	}
	else if (first.Username > second.Username)
	{
		return false;
	}

	return (first.Password < second.Password);
}