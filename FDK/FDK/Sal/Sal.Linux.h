#include <pthread.h>
#include <sys/socket.h>

#define EXPORT_API
#define IMPORT_API

// Windows specific types
typedef int HRESULT;
typedef uint64 FILETIME;
typedef int SOCKET;
typedef int sockoptval_t;

// HRESULT codes
#define E_POINTER  0x80000005
#define S_OK 0
#define S_FALSE 1
#define E_OUTOFMEMORY 0x80000002
#define E_FAILE 0x80004005
#define E_FAIL 0x80000008
#define E_INVALIDARG 0x80000003

// HRESULT macro
#define SUCCEEDED(hr) (((HRESULT)(hr)) >= 0)
#define FAILED(hr) (((HRESULT)(hr)) < 0)

#define ZeroMemory(destination, length) memset((destination), 0, (length))

#define foreach(entry, container) for(entry : container)


#define INVALID_SOCKET (SOCKET)(~0)
#define SOCKET_ERROR (-1)
#define SD_BOTH SHUT_RDWR

#define UNREFERENCED_PARAMETER(expression) (expression)


class FxSocketIterator
{
public:
	FxSocketIterator(const fd_set& arg);
	FxSocketIterator(const fd_set& arg, SOCKET socket);
public:
	FxSocketIterator& operator ++ ();
	SOCKET operator * ()const;
private:
	const fd_set* m_set;
	SOCKET m_socket;
private:
	void Next();
private:
	friend bool operator == (const FxSocketIterator& first, const FxSocketIterator& second);
	friend bool operator != (const FxSocketIterator& first, const FxSocketIterator& second);
};

inline FxSocketIterator::FxSocketIterator(const fd_set& arg) : m_set(&arg), m_socket(0)
{
	if (!FD_ISSET(m_socket, m_set))
	{
		Next();
	}
}
inline FxSocketIterator::FxSocketIterator(const fd_set& arg, SOCKET socket) : m_set(&arg), m_socket(socket)
{
}
inline void FxSocketIterator::Next()
{
	for (++m_socket; m_socket < __FD_SETSIZE; ++m_socket)
	{
		if (FD_ISSET(m_socket, m_set))
		{
			break;
		}
	}
}
inline FxSocketIterator& FxSocketIterator::operator ++ ()
{
	Next();
	return *this;
}
inline SOCKET FxSocketIterator::operator * () const
{
	return m_socket;
}
inline bool operator == (const FxSocketIterator& first, const FxSocketIterator& second)
{
	const bool result = (first.m_set == second.m_set) && (first.m_socket == second.m_socket);
	return result;
}
inline bool operator != (const FxSocketIterator& first, const FxSocketIterator& second)
{
	const bool result = (first.m_set != second.m_set) || (first.m_socket != second.m_socket);
	return result;
}




class FxSockets
{
public:
	typedef FxSocketIterator const_iterator;
public:
	FxSockets(const fd_set& arg);
private:
	FxSockets(const FxSockets& arg);
	FxSockets& operator = (const FxSockets&);
public:
	const_iterator begin()const;
	const_iterator end()const;
private:
	const fd_set& m_set;
};


inline FxSockets::FxSockets(const fd_set& arg) : m_set(arg)
{
}
inline FxSockets::const_iterator FxSockets::begin()const
{
	return FxSocketIterator(m_set);
}
inline FxSockets::const_iterator FxSockets::end()const
{
	return FxSocketIterator(m_set, __FD_SETSIZE);
}

