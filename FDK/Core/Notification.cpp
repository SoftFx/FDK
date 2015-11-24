#include "stdafx.h"
#include "Notification.h"

CNotification::CNotification()
    : Severity(Severity_None)
    , Type(NotificationType_None)
    , Balance()
    , TransactionAmount()
{
}
