#include "stdafx.h"
#include "CriticalSection.h"
#include "Lock.h"
#include "Networking.h"
#include "Socket.h"
#include "SimpleSocket.h"
#include "SecureSocket.h"

#ifdef _MSC_VER
#pragma warning (disable: 4127)
#endif
namespace
{
	CCriticalSection gSynchronizer;
	Socket* gHandle2Socket[64 * 1024];
	const timeval gZeroTimeout = {0, 0};
}
namespace
{
	ptrdiff_t AllocateHandle()
	{
		CLock lock(gSynchronizer);
		for (ptrdiff_t result = 0; result < countof(gHandle2Socket); ++result)
		{
			if (nullptr == gHandle2Socket[result])
			{
				return result;
			}
		}
		return -1;
	}
	void DestroyHandle(ptrdiff_t index)
	{
		assert((index >= 0) && (index < countof(gHandle2Socket)));
		CLock lock(gSynchronizer);
		assert(nullptr != gHandle2Socket[index]);
		gHandle2Socket[index] = nullptr;
	}
}



extern "C"
{
	SOCKET FxSocket(int af, int type, int protocol, int mode)
	{
		bool enableStats = (0 != (mode & FX_SOCKET_ENABLE_STATS));
		mode &= (~FX_SOCKET_ENABLE_STATS);

		SOCKET s = socket(af, type, protocol);
		if (INVALID_SOCKET == s)
		{
			return INVALID_SOCKET;
		}
		Socket* ptr = nullptr;
		if (FX_SOCKET_MODE_SIMPLE == mode)
		{
			ptr = new SimpleSocket(s, enableStats);
		}
		else if (FX_SOCKET_MODE_SECURE == mode)
		{
			ptr = new SecureSocket(s, af, type, protocol, enableStats);
		}
		if (nullptr == ptr)
		{
			#ifdef _MSC_VER
			closesocket(s);
			#else
			close(s);
			#endif
			return INVALID_SOCKET;
		}
		const ptrdiff_t result = AllocateHandle();
		if (result < 0)
		{
			delete ptr;
			return INVALID_SOCKET;
		}
		gHandle2Socket[result] = ptr;
		return static_cast<SOCKET>(result);
	}
	int FxCloseSocket(SOCKET s)
	{
		const ptrdiff_t index = static_cast<ptrdiff_t>(s);
		if ((index < 0) || (index > countof(gHandle2Socket)))
		{
			return SOCKET_ERROR;
		}
		Socket* pSocket = gHandle2Socket[index];
		assert(nullptr != pSocket);
		if (nullptr == pSocket)
		{
			return SOCKET_ERROR;
		}
		delete pSocket;
		DestroyHandle(index);
		return 0;
	}
	int FxShutdown(SOCKET s, int how)
	{
		const ptrdiff_t index = static_cast<ptrdiff_t>(s);
		Socket* pSocket = gHandle2Socket[index];
		assert(nullptr != pSocket);
		if (nullptr == pSocket)
		{
			return SOCKET_ERROR;
		}
		const int result = pSocket->Shutdown(how);
		return result;
	}
	int FxBind(SOCKET s, const struct sockaddr* name, int namelen)
	{
		const ptrdiff_t index = static_cast<ptrdiff_t>(s);
		Socket* pSocket = gHandle2Socket[index];
		assert(nullptr != pSocket);
		if (nullptr == pSocket)
		{
			return SOCKET_ERROR;
		}
		const int result = pSocket->Bind(name, namelen);
		return result;
	}

	int FxListen(SOCKET s, int backlog)
	{
		const ptrdiff_t index = static_cast<ptrdiff_t>(s);
		Socket* pSocket = gHandle2Socket[index];
		assert(nullptr != pSocket);
		if (nullptr == pSocket)
		{
			return SOCKET_ERROR;
		}
		const int result = pSocket->Listen(backlog);
		return result;
	}
	SOCKET FxPhysicalAccept(SOCKET s, struct sockaddr* addr, socklen_t* addrlen, const char* ceritificateFileName /* = nullptr */, const char* password /* = nullptr */)
	{
		const ptrdiff_t index = static_cast<ptrdiff_t>(s);
		Socket* pSocket = gHandle2Socket[index];
		assert(nullptr != pSocket);
		if (nullptr == pSocket)
		{
			return INVALID_SOCKET;
		}
		Socket* const ptr = pSocket->PhysicalAccept(addr, addrlen, ceritificateFileName, password);
		if (nullptr == ptr)
		{
			return INVALID_SOCKET;
		}
		const ptrdiff_t newIndex = AllocateHandle();
		if (newIndex < 0)
		{
			delete ptr;
			return INVALID_SOCKET;
		}
		gHandle2Socket[newIndex] = ptr;
		return static_cast<SOCKET>(newIndex);
	}
	HRESULT FxLogicalAccept(SOCKET s)
	{
		const ptrdiff_t index = static_cast<ptrdiff_t>(s);
		Socket* pSocket = gHandle2Socket[index];
		assert(nullptr != pSocket);
		if (nullptr == pSocket)
		{
			return E_POINTER;
		}
		const HRESULT result = pSocket->LogicalAccept();
		return result;
	}
	SOCKET FxAccept(SOCKET s, struct sockaddr* addr, socklen_t* addrlen, const char* ceritificateFileName /* = nullptr */, const char* password /* = nullptr */)
	{
		SOCKET result = FxPhysicalAccept(s, addr, addrlen, ceritificateFileName, password);
		if (INVALID_SOCKET == result)
		{
			return INVALID_SOCKET;
		}
		const HRESULT status = FxLogicalAccept(result);
		if (S_OK == status)
		{
			return result;
		}
		FxCloseSocket(result);
		return INVALID_SOCKET;
	}
	int FxConnect(SOCKET s, const struct sockaddr* name, int namelen)
	{
		const ptrdiff_t index = static_cast<ptrdiff_t>(s);
		Socket* pSocket = gHandle2Socket[index];
		assert(nullptr != pSocket);
		if (nullptr == pSocket)
		{
			return SOCKET_ERROR;
		}
		const int result = pSocket->Connect(name, namelen);
		return result;
	}
	int FxGetPeerName(SOCKET s, struct sockaddr* name, socklen_t* namelen)
	{
		const ptrdiff_t index = static_cast<ptrdiff_t>(s);
		Socket* pSocket = gHandle2Socket[index];
		assert(nullptr != pSocket);
		if (nullptr == pSocket)
		{
			return SOCKET_ERROR;
		}
		const int result = pSocket->GetPeerName(name, namelen);
		return result;
	}
	int FxGetSockName(SOCKET s, struct sockaddr* name, socklen_t* namelen)
	{
		const ptrdiff_t index = static_cast<ptrdiff_t>(s);
		Socket* pSocket = gHandle2Socket[index];
		assert(nullptr != pSocket);
		if (nullptr == pSocket)
		{
			return SOCKET_ERROR;
		}
		const int result = pSocket->GetSockName(name, namelen);
		return result;
	}
	int FxGetSockOpt(SOCKET s, int level, int optname, char* optval, socklen_t* optlen)
	{
		const ptrdiff_t index = static_cast<ptrdiff_t>(s);
		Socket* pSocket = gHandle2Socket[index];
		assert(nullptr != pSocket);
		if (nullptr == pSocket)
		{
			return SOCKET_ERROR;
		}
		const int result = pSocket->GetSockOpt(level, optname, optval, optlen);
		return result;
	}
	int FxRecv(SOCKET s, char* buf, int len, int flags)
	{
		const ptrdiff_t index = static_cast<ptrdiff_t>(s);
		Socket* pSocket = gHandle2Socket[index];
		assert(nullptr != pSocket);
		if (nullptr == pSocket)
		{
			return SOCKET_ERROR;
		}
		const int result = pSocket->Recv(buf, len, flags);
		return result;
	}
	int FxRecvFrom(SOCKET s, char* buf, int len, int flags, struct sockaddr* from, socklen_t* fromlen)
	{
		const ptrdiff_t index = static_cast<ptrdiff_t>(s);
		Socket* pSocket = gHandle2Socket[index];
		assert(nullptr != pSocket);
		if (nullptr == pSocket)
		{
			return SOCKET_ERROR;
		}
		const int result = pSocket->RecvFrom(buf, len, flags, from, fromlen);
		return result;
	}
	int FxSend(SOCKET s, const char* buf, int len, int flags)
	{
		const ptrdiff_t index = static_cast<ptrdiff_t>(s);
		Socket* pSocket = gHandle2Socket[index];
		assert(nullptr != pSocket);
		if (nullptr == pSocket)
		{
			return SOCKET_ERROR;
		}
		const int result = pSocket->Send(buf, len, flags);
		return result;
	}
	int FxSendTo(SOCKET s, const char* buf, int len, int flags, const struct sockaddr* to, int tolen)
	{
		const ptrdiff_t index = static_cast<ptrdiff_t>(s);
		Socket* pSocket = gHandle2Socket[index];
		assert(nullptr != pSocket);
		if (nullptr == pSocket)
		{
			return SOCKET_ERROR;
		}
		const int result = pSocket->SendTo(buf, len, flags, to, tolen);
		return result;
	}
	int FxSetSockOpt(SOCKET s, int level, int optname, const sockoptval_t* optval, int optlen)
	{
		const ptrdiff_t index = static_cast<ptrdiff_t>(s);
		Socket* pSocket = gHandle2Socket[index];
		assert(nullptr != pSocket);
		if (nullptr == pSocket)
		{
			return SOCKET_ERROR;
		}
		const int result = pSocket->SetSockOpt(level, optname, optval, optlen);
		return result;
	}
	fd_set* ConvertFromHandlesToSockets(fd_set* pSource, fd_set& destination, map<SOCKET, ptrdiff_t>& socketsToHandles)
	{
		FD_ZERO(&destination);
		if (nullptr == pSource)
		{
			return nullptr;
		}
		FxSockets sockets(*pSource);
		foreach(auto current, sockets)
		{
			const ptrdiff_t index = static_cast<ptrdiff_t>(current);
			assert((index >= 0) && (index < countof(gHandle2Socket)));
			if ((index < 0) || (index >= countof(gHandle2Socket)))
			{
				continue;
			}
			Socket* pSocket = gHandle2Socket[index];
			assert(nullptr != pSocket);
			if (nullptr == pSocket)
			{
				continue;
			}
			SOCKET s = pSocket->Handle();
			socketsToHandles[s] = index;
			FD_SET(s, &destination);
		}
		return &destination;
	}
	void ConvertFromSocketsToHandles(const map<SOCKET, ptrdiff_t>& socketsToHandles, fd_set* pSource, fd_set* pDestination)
	{
		if (nullptr == pSource)
		{
			assert(nullptr == pDestination);
			return;
		}
		FD_ZERO(pDestination);

		FxSockets sockets(*pSource);
		foreach(auto current, sockets)
		{
			auto it = socketsToHandles.find(current);
			if (socketsToHandles.end() == it)
			{
				assert(!"unexpected handle");
				continue;
			}
			const SOCKET s = static_cast<SOCKET>(it->second);
			FD_SET(s, pDestination);
		}
	}
	int Win32Select(int nfds, fd_set* readfds, fd_set* writefds, fd_set* exceptfds, const struct timeval* timeout)
	{
		try
		{
			map<SOCKET, ptrdiff_t> socketsToHandles;
			fd_set reads;
			fd_set* pReads = ConvertFromHandlesToSockets(readfds, reads, socketsToHandles);

			fd_set writes;
			fd_set* pWrites = ConvertFromHandlesToSockets(writefds, writes, socketsToHandles);

			fd_set excepts;
			fd_set* pExcepts = ConvertFromHandlesToSockets(exceptfds, excepts, socketsToHandles);

			const int result = select(nfds, pReads, pWrites, pExcepts, const_cast<timeval*>(timeout));

			ConvertFromSocketsToHandles(socketsToHandles, pReads, readfds);
			ConvertFromSocketsToHandles(socketsToHandles, pWrites, writefds);
			ConvertFromSocketsToHandles(socketsToHandles, pExcepts, exceptfds);
			return result;
		}
		catch (const exception&)
		{
			return SOCKET_ERROR;
		}
	}
	int SslSelect(fd_set& requestForReading, fd_set& readyForReading, fd_set& notReadyForReading)
	{
		int result = 0;
		FxSockets sockets(requestForReading);
		foreach(auto current, sockets)
		{
			const ptrdiff_t index = static_cast<ptrdiff_t>(current);
			assert((index >= 0) && (index < countof(gHandle2Socket)));
			if ((index < 0) || (index >= countof(gHandle2Socket)))
			{
				continue;
			}
			Socket* pSocket = gHandle2Socket[index];
			assert(nullptr != pSocket);
			if (nullptr == pSocket)
			{
				continue;
			}
			if (pSocket->Pending())
			{
				FD_SET(current, &readyForReading);
				++result;
			}
			else
			{
				FD_SET(current, &notReadyForReading);
			}
		}
		return result;
	}
	int FxSelect(int nfds, fd_set* readfds, fd_set* writefds, fd_set* exceptfds, const struct timeval* timeout)
	{
		if (nullptr == readfds)
		{
			return Win32Select(nfds, readfds, writefds, exceptfds, timeout);
		}
		fd_set requestForReading = *readfds;

		fd_set readyForReading;
		FD_ZERO(&readyForReading);

		fd_set notReadyForReading;
		FD_ZERO(&notReadyForReading);

		int result = SslSelect(requestForReading, readyForReading, notReadyForReading);
		if (0 == result)
		{
			return Win32Select(nfds, readfds, writefds, exceptfds, timeout);
		}
		result += Win32Select(nfds, &notReadyForReading, writefds, exceptfds, &gZeroTimeout);

		FD_ZERO(readfds);

		FxSockets readyForReadingSockets(readyForReading);
		foreach(auto current, readyForReadingSockets)
		{
			FD_SET(current, readfds);
		}

		FxSockets notReadyForReadingSockets(notReadyForReading);
		foreach(auto current, notReadyForReadingSockets)
		{
			FD_SET(current, readfds);
		}
		return result;
	}
	int FxGetSocketActivity(SOCKET s, uint64* pDataBytesSent, uint64* pSslBytesSent, uint64* pDataBytesReceived, uint64* pSslBytesReceived)
	{
		const ptrdiff_t index = static_cast<ptrdiff_t>(s);
		Socket* pSocket = gHandle2Socket[index];
		assert(nullptr != pSocket);
		if (nullptr == pSocket)
		{
			return SOCKET_ERROR;
		}
		pSocket->GetActivity(pDataBytesSent, pSslBytesSent, pDataBytesReceived, pSslBytesReceived);
		return 0;
	}
	int FxIoctlsocket(SOCKET s, long cmd, u_long FAR* argp)
	{
		const ptrdiff_t index = static_cast<ptrdiff_t>(s);
		Socket* pSocket = gHandle2Socket[index];
		assert(nullptr != pSocket);
		if (nullptr == pSocket)
		{
			return SOCKET_ERROR;
		}
		return pSocket->Ioctlsocket(cmd, argp);
	}
}

bool FxCanRead(SOCKET s)
{
	Socket* pSocket = gHandle2Socket[static_cast<size_t>(s)];
	assert(nullptr != pSocket);
	return pSocket->CanRead();
}
bool FxCanWrite(SOCKET s)
{
	Socket* pSocket = gHandle2Socket[static_cast<size_t>(s)];
	assert(nullptr != pSocket);
	return pSocket->CanWrite();
}
CSocketState FxCanReadWrite(SOCKET s)
{
	Socket* pSocket = gHandle2Socket[static_cast<size_t>(s)];
	assert(nullptr != pSocket);
	return pSocket->CanReadWrite();
}
bool FxEnableKeepAlive(SOCKET s, u_long keepalivetimeInMilliseconds /* = 10000 */, u_long keepaliveintervalInMilliseconds /* = 3000 */)
{
	Socket* pSocket = gHandle2Socket[static_cast<size_t>(s)];
	assert(nullptr != pSocket);
	return pSocket->EnableKeepAlive(keepalivetimeInMilliseconds, keepaliveintervalInMilliseconds);
}