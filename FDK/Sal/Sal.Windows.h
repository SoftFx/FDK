#include <SDKDDKVer.h>
#include <WinSock2.h>
#include <MSTcpIP.h>

#ifdef max
#undef max
#endif

#ifdef min
#undef min
#endif

#define EXPORT_API __declspec(dllexport)
#define IMPORT_API __declspec(dllimport)

typedef char sockoptval_t;


#define foreach(entry, container) for each(entry in container)
typedef int socklen_t;




//SocketIterator FxFdSetBegin(const fd_set& arg)


class FxSockets
{
public:
	typedef SOCKET* iterator;
	typedef const SOCKET* const_iterator;
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
	return m_set.fd_array;
}
inline FxSockets::const_iterator FxSockets::end()const
{
	return m_set.fd_array + m_set.fd_count;
}
