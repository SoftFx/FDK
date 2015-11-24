#include "stdafx.h"
#include "ProtocolVersion.h"


namespace
{
	regex gPattern("ext\\.(\\d{1,4})\\.(\\d{1,4})");
}

CProtocolVersion::CProtocolVersion()
    : Major()
    , Minor()
{
}

CProtocolVersion::CProtocolVersion(int major, int minor)
    : Major(major)
    , Minor(minor)
{
}

CProtocolVersion::CProtocolVersion(const string& st)
    : Major()
    , Minor()
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