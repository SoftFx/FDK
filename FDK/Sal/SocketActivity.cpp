#include "stdafx.h"
#include "SocketActivity.h"
#include "Networking.h"

CSocketActivity::CSocketActivity() : LogicalBytesSent(), PhysicalBytesSent(), LogicalBytesReceived(), PhysicalBytesReceived()
{
}
CSocketActivity::CSocketActivity(SOCKET socket) : LogicalBytesSent(), PhysicalBytesSent(), LogicalBytesReceived(), PhysicalBytesReceived()
{
	FxGetSocketActivity(socket, &LogicalBytesSent, &PhysicalBytesSent, &LogicalBytesReceived, &PhysicalBytesReceived);
}
CSocketActivity& CSocketActivity::operator += (const CSocketActivity& activity)
{
	LogicalBytesSent += activity.LogicalBytesSent;
	PhysicalBytesSent += activity.PhysicalBytesSent;
	LogicalBytesReceived += activity.LogicalBytesReceived;
	PhysicalBytesReceived += activity.PhysicalBytesReceived;
	return *this;
}
CSocketActivity& CSocketActivity::operator -= (const CSocketActivity& activity)
{
	LogicalBytesSent -= activity.LogicalBytesSent;
	PhysicalBytesSent -= activity.PhysicalBytesSent;
	LogicalBytesReceived -= activity.LogicalBytesReceived;
	PhysicalBytesReceived -= activity.PhysicalBytesReceived;
	return *this;
}