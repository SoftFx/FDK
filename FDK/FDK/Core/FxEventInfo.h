#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)
#pragma warning (disable : 4251)

class CORE_API CFxEventInfo
{
public:
	HRESULT Status;
	string ID;
	CDateTime SendingTime;
	CDateTime ReceivingTime;
	string Message;
	string Description;
public:
	bool IsExternalSynchCall()const;
	bool IsInternalAsynchCall()const;
	bool IsNotification()const;
	bool IsCall()const;
public:
	CFxEventInfo();
	CFxEventInfo(const string& id);
	CFxEventInfo(const string& id, const string& message);
};

#pragma warning (pop)
