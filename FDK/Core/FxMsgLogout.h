#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)
#pragma warning (disable : 4251)

#include "FxHandle.h"


class CORE_API CFxMsgLogout : public CFxHandle
{
public:
	FxLogoutReason Reason;
	int32 Code; 
	string Text;
public:
	CFxMsgLogout(const FxLogoutReason reason, int32 code, const string& text);
};

#pragma warning (pop)

