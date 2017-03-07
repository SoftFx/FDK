#pragma once

class CORE_API CFxStatusGroupInfo
{
public:

	std::string StatusGroupId;
	SessionStatus Status;
	CDateTime StartTime;
	CDateTime EndTime;
    CDateTime OpenTime;
    CDateTime CloseTime;
};

