#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)
#pragma warning (disable : 4251)

class CORE_API CFxClosePositionsResponse
{
public:
	HRESULT Status;
	string Description;
	vector<string> Orders;
public:
	CFxClosePositionsResponse();
};

#pragma warning (pop)
