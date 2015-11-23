#include "stdafx.h"
#include "SecureSocket.h"

namespace
{
	int Password(char *buf, int num, int /*rwflag*/, void* userdata)
	{
		if (nullptr == userdata)
		{
			return 0;
		}
		const char* source = reinterpret_cast<const char*>(userdata);
		const size_t result = strlen(source);
		if (static_cast<size_t>(num) < 1 + result)
		{
			return 0;
		}
		strcpy_s(buf, static_cast<size_t>(num), source);
		return static_cast<int>(result);
	}
}

int Offset(int after, int before)
{
	if (after >= before)
	{
		return (after - before);
	}
	int result = (after - numeric_limits<int>::min()) + (numeric_limits<int>::max() - before) + 1;
	return result;
}

SecureSocket::SecureSocket(SOCKET s, int af, int type, int protocol, bool enabledStats) : Socket(s, enabledStats), m_af(af), m_type(type), m_protocol(protocol)
{
	m_method = SSLv23_method();
	m_context = SSL_CTX_new(m_method);
	m_ssl = SSL_new(m_context);
	m_bio = BIO_new_socket(static_cast<int>(m_socket), BIO_NOCLOSE);
	SSL_set_bio(m_ssl, m_bio, m_bio);
}
SecureSocket::SecureSocket(bool enabledStats) : Socket(0, enabledStats),  m_af(), m_type(), m_protocol(), m_method(), m_context(), m_ssl(), m_bio()
{
}
bool SecureSocket::Initialize(SOCKET s, const char* ceritificateFileName, const char* password)
{
	m_method = SSLv23_method();
	m_context = SSL_CTX_new(m_method);

	int status = SSL_CTX_use_certificate_file(m_context, ceritificateFileName, SSL_FILETYPE_PEM);
	if (!status)
	{
		return false;
	}
	SSL_CTX_set_default_passwd_cb_userdata(m_context, const_cast<char*>(password));
	SSL_CTX_set_default_passwd_cb(m_context, Password); 
	status = SSL_CTX_use_PrivateKey_file(m_context, ceritificateFileName, SSL_FILETYPE_PEM);
	if(!status)
	{
		return false;
	}

	m_ssl = SSL_new(m_context);
	m_bio = BIO_new_socket(static_cast<int>(s), BIO_NOCLOSE);
	SSL_set_bio(m_ssl, m_bio, m_bio);

	m_socket = s;
	m_isAccepting = true;

	u_long opt = 1;
	::ioctlsocket(s, FIONBIO, &opt);
	return true;
}
SecureSocket::~SecureSocket()
{
	SSL_free(m_ssl);
	SSL_CTX_free(m_context);
}
Socket* SecureSocket::PhysicalAccept(struct sockaddr* addr, socklen_t* addrlen, const char* ceritificateFileName, const char* password)
{
	if ((nullptr == ceritificateFileName) || (nullptr == password))
	{
		return nullptr;
	}
	SOCKET s = ::accept(m_socket, addr, addrlen);
	if (INVALID_SOCKET == s)
	{
		return nullptr;
	}

	const bool enabledStats = EnabledStats();
	auto_ptr<SecureSocket> sentry(new SecureSocket(enabledStats));
	SecureSocket* result = sentry.release();

	if (!result->Initialize(s, ceritificateFileName, password))
	{
		return nullptr;
	}

	sentry.release();
	return result;
}
HRESULT SecureSocket::LogicalAccept()
{
	if (!m_isAccepting)
	{
		return E_FAIL;
	}
	const int status = SSL_accept(m_ssl);
	if (1 != status)
	{
		const int code = SSL_get_error(m_ssl, status);
		if ((SSL_ERROR_WANT_READ != code) && (SSL_ERROR_WANT_WRITE != code))
		{
			return E_FAIL;
		}
		return S_FALSE;
	}
	m_isAccepting = false;
	return S_OK;
}
int SecureSocket::Connect(const struct sockaddr* name, int namelen)
{
	int timeout = 0;
	const int result = DoConnect(name, namelen, timeout);
	if (SOCKET_ERROR == result)
	{
		return SOCKET_ERROR;
	}
	setsockopt(m_socket, SOL_SOCKET, SO_SNDTIMEO, (char*)&timeout, sizeof(timeout));
	setsockopt(m_socket, SOL_SOCKET, SO_RCVTIMEO, (char*)&timeout, sizeof(timeout));

	const int status = SSL_connect(m_ssl);

	timeout = 0;
	setsockopt(m_socket, SOL_SOCKET, SO_SNDTIMEO, (char*)&timeout, sizeof(timeout));
	setsockopt(m_socket, SOL_SOCKET, SO_RCVTIMEO, (char*)&timeout, sizeof(timeout));

	if (status <= 0)
	{
		shutdown(m_socket, SD_BOTH);
		return SOCKET_ERROR;
	}
	return result;
}

int SecureSocket::DoConnect(const struct sockaddr* name, int namelen, int& timeout)
{
	int result = DoPhysicalConnect(name, namelen, timeout);
	if (SOCKET_ERROR != result)
	{
		return result;
	}
	const DWORD code = WSAGetLastError();
	if (WSAEISCONN != code)
	{
		return result;
	}
	// need to reinitialize
	if (DoReinitialize())
	{
		result = DoPhysicalConnect(name, namelen, timeout);
	}
	else
	{
		WSASetLastError(WSAEISCONN);
	}
	return result;
}
int SecureSocket::DoPhysicalConnect(const struct sockaddr* name, int namelen, int& timeout)
{
	const DWORD start = GetTickCount();
	const int result = ::connect(m_socket, name, namelen);
	if (SOCKET_ERROR == result)
	{
		return SOCKET_ERROR;
	}

	const DWORD finish = GetTickCount();
	if (0 == result)
	{
		EnableKeepAlive(m_socket);
	}
	if (finish > start)
	{
		timeout = finish - start;
	}
	else
	{
		timeout = finish + (std::numeric_limits<DWORD>::max() - start) + 1;
	}
	timeout = std::min<DWORD>(1000 + 4 * timeout, 16000);
	return result;
}

int SecureSocket::Send(const char* buf, int len, int /*flags*/)
{
	int received0 = m_bio->num_read;
	int sent0 = m_bio->num_write;

	int result = SSL_write(m_ssl, buf, len);

	int received1 = m_bio->num_read;
	int sent1 = m_bio->num_write;

	int physicalBytesSent = Offset(sent1, sent0);
	int physicalBytesReceived = Offset(received1, received0);

	int logicalBytesSent = (result > 0) ? result : 0;

	OnSent(logicalBytesSent, physicalBytesSent, physicalBytesReceived);

	return result;
}
int SecureSocket::RecvFrom(char* /*buf*/, int /*len*/, int /*flags*/, struct sockaddr* /*from*/, socklen_t* /*fromlen*/)
{
	throw std::runtime_error("The method or operation is not implemented.");
}
int SecureSocket::Recv(char* buf, int len, int /*flags*/)
{
	int received0 = m_bio->num_read;
	int sent0 = m_bio->num_write;

	const int result = SSL_read(m_ssl, buf, len);

	if(result > 0)
	{
		int received1 = m_bio->num_read;
		int sent1 = m_bio->num_write;

		int physicalBytesSent = Offset(sent1, sent0);
		int physicalBytesReceived = Offset(received1, received0);

		int logicalBytesReceived = (result > 0) ? result : 0;

		OnReceived(logicalBytesReceived, physicalBytesSent, physicalBytesReceived);

		return result;
	}
	const DWORD status = WSAGetLastError();
	if (WSAEWOULDBLOCK == status)
	{
		return 0;
	}
	const int code = SSL_get_error(m_ssl, result);
	if (SSL_ERROR_WANT_READ == code)
	{
		return 0;
	}
	return SOCKET_ERROR;
}
int SecureSocket::SendTo(const char* /*buf*/, int /*len*/, int /*flags*/, const struct sockaddr* /*to*/, int /*tolen*/)
{
	throw std::runtime_error("The method or operation is not implemented.");
}
bool SecureSocket::Pending()
{
	const int numberOfAvailableBytes = SSL_pending(m_ssl);
	const bool result = (numberOfAvailableBytes > 0);
	return result;
}
bool SecureSocket::DoReinitialize()
{
	SOCKET newSocket = ::socket(m_af, m_type, m_protocol);
	if (INVALID_SOCKET == newSocket)
	{
		return false;
	}

	#ifdef _MSC_VER
	SSL_METHOD* method = SSLv23_method();
	#else
	const SSL_METHOD* method = SSLv23_method();
	#endif

	SSL_CTX* context = SSL_CTX_new(method);
	SSL* ssl = SSL_new(m_context);
	BIO* bio = BIO_new_socket(static_cast<int>(newSocket), BIO_NOCLOSE);
	SSL_set_bio(ssl, bio, bio);

	SetSocket(newSocket);

	SSL_free(m_ssl);
	SSL_CTX_free(m_context);


	m_method = method;
	m_context = context;
	m_ssl = ssl;
	m_bio = bio;
	return true;
}
