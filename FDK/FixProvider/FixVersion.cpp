#include "stdafx.h"
#include "FixVersion.h"

namespace
{
    regex gPattern("ext\\.(\\d{1,4})\\.(\\d{1,4})");
}
CFixVersion::CFixVersion() : Major(), Minor()
{
}
CFixVersion::CFixVersion(int major, int minor) : Major(major), Minor(minor)
{
}
CFixVersion::CFixVersion(const std::string& st) : Major(), Minor()
{
    if (!st.empty())
    {
        regex pattern("ext\\.(\\d{1,4})\\.(\\d{1,4})");
        cmatch what;
        if (!regex_match(st.c_str(), what, pattern))
        {
            throw runtime_error("Invalid fix version: " + st);
        }
        Major = atoi(what[1].first);
        Minor = atoi(what[2].first);
    }
}
bool CFixVersion::SupportsMarketWithSlippage()
{
    return ((Major > 1) || ((Major == 1) && (Minor == 42)));
}
bool operator < (const CFixVersion& first, const CFixVersion& second)
{
    if (first.Major == second.Major)
    {
        return (first.Minor < second.Minor);
    }
    return (first.Major < second.Major);
}
bool operator > (const CFixVersion& first, const CFixVersion& second)
{
    if (first.Major == second.Major)
    {
        return (first.Minor > second.Minor);
    }
    return (first.Major > second.Major);
}
bool operator <= (const CFixVersion& first, const CFixVersion& second)
{
    if (first.Major == second.Major)
    {
        return (first.Minor <= second.Minor);
    }
    return (first.Major <= second.Major);
}
bool operator >= (const CFixVersion& first, const CFixVersion& second)
{
    if (first.Major == second.Major)
    {
        return (first.Minor >= second.Minor);
    }
    return (first.Major >= second.Major);
}