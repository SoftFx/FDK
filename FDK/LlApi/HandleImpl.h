#pragma once


class CHandleImpl
{
public:
	void Delete(void* handle);
	CNotification NotificationFromHandle(void* handle);
};

