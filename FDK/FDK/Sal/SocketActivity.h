#ifndef __Sal_SocketActivity__
#define __Sal_SocketActivity__


class SAL_API CSocketActivity
{
public:
	CSocketActivity();
	CSocketActivity(SOCKET socket);
public:
	CSocketActivity& operator += (const CSocketActivity& activity);
	CSocketActivity& operator -= (const CSocketActivity& activity);
public:
	uint64 LogicalBytesSent;
	uint64 PhysicalBytesSent;
	uint64 LogicalBytesReceived;
	uint64 PhysicalBytesReceived;
};

#endif