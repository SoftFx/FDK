#pragma once

#pragma warning (push)
#pragma warning (disable : 4275)
#pragma warning (disable : 4251)

enum Severity
{
	Severity_None = 0,
	Severity_Event = 1,
	Severity_Warning = 2,
	Severity_Error = 3,
	Severity_Last = FX_MAX_ENUM
};


enum NotificationType
{
	NotificationType_None = 0,
	NotificationType_MarginCall = 1,
	NotificationType_MarginCallRevocation = 2,
	NotificationType_StopOut = 3,
	NotificationType_Balance = 4,
    NotificationType_ConfigUpdated = 5,
	NotificationType_Last = FX_MAX_ENUM
};



class CORE_API CNotification
{
public:
	CNotification();
public:
	Severity Severity;
	NotificationType Type;
	string Text;
	double Balance;
	double TransactionAmount;
	string TransactionCurrency;
};

#pragma warning (pop)