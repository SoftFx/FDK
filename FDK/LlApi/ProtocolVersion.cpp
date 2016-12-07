#include "stdafx.h"
#include "ProtocolVersion.h"


namespace
{
	regex gPattern("ext\\.(\\d{1,4})\\.(\\d{1,4})");
}

CProtocolVersion::CProtocolVersion()
    : Major(0)
    , Minor(0)
{
}

CProtocolVersion::CProtocolVersion(int major, int minor)
    : Major(major)
    , Minor(minor)
{
}

CProtocolVersion::CProtocolVersion(const string& st)
    : Major(0)
    , Minor(0)
{
	if (!st.empty())
	{
		regex pattern("ext\\.(\\d{1,4})\\.(\\d{1,4})");
		cmatch what;
		if (!std::tr1::regex_match(st.c_str(), what, pattern))
		{
			throw runtime_error("Invalid protocol version: " + st);
		}
		Major = atoi(what[1].first);
		Minor = atoi(what[2].first);
	}
}

int CProtocolVersion::getMajor() const
{
    return Major;
}

int CProtocolVersion::getMinor() const
{
    return Minor;
}

std::string CProtocolVersion::toString() const
{
    char sz[32];
    snprintf(sz, sizeof(sz), "ext.%i.%i", Major, Minor);
    return sz;
}

bool operator < (const CProtocolVersion& first, const CProtocolVersion& second)
{
	if (first.Major == second.Major)
	{
		return (first.Minor < second.Minor);
	}
	return (first.Major < second.Major);
}

bool operator > (const CProtocolVersion& first, const CProtocolVersion& second)
{
	if (first.Major == second.Major)
	{
		return (first.Minor > second.Minor);
	}
	return (first.Major > second.Major);
}

bool operator <= (const CProtocolVersion& first, const CProtocolVersion& second)
{
	if (first.Major == second.Major)
	{
		return (first.Minor <= second.Minor);
	}
	return (first.Major <= second.Major);
}

bool operator >= (const CProtocolVersion& first, const CProtocolVersion& second)
{
	if (first.Major == second.Major)
	{
		return (first.Minor >= second.Minor);
	}
	return (first.Major >= second.Major);
}