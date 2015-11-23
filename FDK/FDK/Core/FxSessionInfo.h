#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)
#pragma warning (disable : 4251)

class CORE_API CFxSessionInfo
{
public:
	CDateTime StartTime;
	CDateTime OpenTime;
	CDateTime CloseTime;
	CDateTime EndTime;
	std::string TradingSessionId;
	int32 ServerTimeZoneOffset;
	std::string PlatformName;
	std::string PlatformCompany;
	SessionStatus Status;
};

#pragma warning (pop)
