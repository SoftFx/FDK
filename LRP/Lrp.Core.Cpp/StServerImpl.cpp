#include "stdafx.h"
#include "StServerImpl.h"
#include "LrpStServer.h"
#include "Functions.h"


CStServerImpl::CStServerImpl(const int port, CLrpStServer& server, const char* signature, LrpInvokeHandler handler) :
	m_server(server), m_signature(signature), m_handler(handler), m_acceptor(port), m_thread(nullptr), m_continue(true), m_logger(nullptr, nullptr)
{
	Construct(handler);
}
CStServerImpl::CStServerImpl(const int port, CLrpStServer& server, const char* signature, LrpInvokeHandler handler, LrpLogHandler pLogHandler, void* pUserParam) :
	m_server(server), m_signature(signature), m_handler(handler), m_acceptor(port), m_thread(nullptr), m_continue(true), m_logger(pLogHandler, pUserParam)
{
	Construct(handler);
}
CStServerImpl::CStServerImpl(const int port, CLrpStServer& server, const char* signature, LrpInvokeHandler handler, const char* logPath) :
	m_server(server), m_signature(signature), m_handler(handler), m_acceptor(port), m_thread(nullptr), m_continue(true), m_logger(&CStServerImpl::OnOutput, this)
{
	m_logStream.reset(new ofstream(logPath));
	Construct(handler);
}
void CStServerImpl::Construct(LrpInvokeHandler handler)
{
	m_logger.Output("Checking of LRP invoke handler");
	if (nullptr == handler)
	{
		m_logger.Output("LRP invoke handler is null");
		throw runtime_error("LrpInvokeHandler can not be null");
	}
	m_logger.Output("LPR invoke handler is valid");
	m_logger.Output("Creating a new thread");
	m_thread = reinterpret_cast<HANDLE>(_beginthreadex(nullptr, 0, &CStServerImpl::ThreadFunction, this, 0, nullptr));
	if (nullptr == m_thread)
	{
		m_logger.Output("Couldn't create a new thread; GetLastError() = {0}", GetLastError());
		throw runtime_error("Can not start a new thread");
	}
	m_logger.Output("New thread has been created");
}
CStServerImpl::~CStServerImpl()
{
	m_continue = false;
	m_logger.Output("Shutting down acceptor");
	m_acceptor.Finalize();
	m_logger.Output("Acceptor has been shut down");

	m_logger.Output("Waiting for thread finishing");
	WaitForSingleObject(m_thread, INFINITE);
	CloseHandle(m_thread);
	m_logger.Output("Thread has been finished");
	m_thread = nullptr;

	set<CStChannelImpl*> channels;
	{
		CLock lock(m_synchronizer);
		swap(m_channels, channels);
	}
	m_logger.Output("Closing channels = {0}", channels.size());
	for each(auto element in channels)
	{
		m_logger.Output("Closing a channel");
		element->Finalize(false);
		delete element;
		m_logger.Output("The channel has been closed");
	}
	channels.clear();
	m_logger.Output("All channels have been closed");
}
unsigned __stdcall CStServerImpl::ThreadFunction(void* arg)
{
	CStServerImpl* pServer = reinterpret_cast<CStServerImpl*>(arg);
	__try
	{
		pServer->Loop();
		return 0;
	}
	__except(LrpExceptionHandler(GetExceptionInformation()))
	{
		return 1;
	}
}
void CStServerImpl::Loop()
{
	m_logger.Output("Accepting loop is started");
	for (; m_continue; )
	{
		const SOCKET client = m_acceptor.Accept();
		if (INVALID_SOCKET != client)
		{
			m_logger.Output("A new connection has been accepted; socket = {0}", client);
			CreateNewChannel(client);
		}
		else
		{
			m_logger.Output("A new connection has been accepted; socket = INVALID_SOCKET");
			if (m_continue)
				Sleep(1000);
		}
	}
	m_logger.Output("Accepting loop is finished");
}
void CStServerImpl::CreateNewChannel(SOCKET client)
{
	void* handle = ApproveNewClient(client);
	if (nullptr != handle)
	{
		CreateNewChannel(client, handle);
	}
	else
	{
		closesocket(client);
	}
}
void CStServerImpl::CreateNewChannel(SOCKET client, void* handle)
{
	m_logger.Output("Creating a new channel for an user-defined LRP handler = 0x{0}", handle);
	EnableKeepAlive(client);
	CStChannelImpl* pChannel = nullptr;
	CLock lock(m_synchronizer);
	try
	{
		pChannel = new CStChannelImpl(*this, client, handle);
		auto_ptr<CStChannelImpl> sentry(pChannel);
		m_channels.insert(pChannel);
		sentry.release();
		m_logger.Output("A new channel for an user-defined LRP handler = 0x{0} has been created", handle);
	}
	catch (const exception&)
	{
		if (0 == pChannel)
		{
			closesocket(client);
			m_logger.Output("A new channel for an user-defined LRP handler = 0x{0} has not been created", handle);
		}
		else
			m_logger.Output("A new channel for an user-defined LRP handler = 0x{0} has been created with exception", handle);
	}
}
void* CStServerImpl::ApproveNewClient(SOCKET client)
{
	sockaddr_in addr;
	int length = sizeof(addr);
	m_logger.Output("Getting address information from socket = {0}", client);
	const int status = getpeername(client, (sockaddr*)&addr, &length);
	if (SOCKET_ERROR == status)
	{
		m_logger.Output("Couldn't get address information from socket; WSAGetLastError() = {0}", WSAGetLastError());
		return nullptr;
	}
	m_logger.Output("Address information from socket has been got");
	m_logger.Output("Getting IP address from socket address information");
	const char* address = inet_ntoa(addr.sin_addr);
	if (nullptr == address)
	{
		m_logger.Output("Couldn't get IP address from socket address information; WSAGetLastError() = ", WSAGetLastError());
		return nullptr;
	}
	m_logger.Output("IP address has been got from socket address information = {0}", address);
	m_logger.Output("Creating new LRP user-defined handler for address = {0}", address);
	void* result = m_server.CreateNewConnectionInternal(address);
	if (nullptr != result)
	{
		m_logger.Output("New LRP user-defined handler has been created for address = {0}; handle = 0x{1}", address, result);
	}
	else
	{
		m_logger.Output("New LRP user-defined handler has not been created for address = {0}", address);
	}
	return result;
}

bool CStServerImpl::RemoveChannel(CStChannelImpl* pChannel)
{
	m_logger.Output("Closing channel");
	CLock lock(m_synchronizer);
	auto it = m_channels.find(pChannel);
	if (m_channels.end() == it)
	{
		m_logger.Output("Server-destruction of closing channel");
		return false;
	}
	m_channels.erase(it);
	m_logger.Output("Self-destruction of closing channel");
	return true;
}
bool CStServerImpl::ValidateCredentials(const string& username, const string& password, void* handle)
{
	m_logger.Output("Validating username/password = \"{0}\"/\"{1}\"", username, password);
	const bool result = m_server.ValidateCredentialsInternal(username.c_str(), password.c_str(), handle);
	if (result)
	{
		m_logger.Output("username/password = \"{0}\"/\"{1}\" have been approved", username, password);
	}
	else
	{
		m_logger.Output("username/password = \"{0}\"/\"{1}\"} have been rejected", username, password);
	}
	return result;
}
const char* CStServerImpl::Signature() const
{
	return m_signature;
}
LrpInvokeHandler CStServerImpl::Handler() const
{
	return m_handler;
}
void CStServerImpl::OnOutput(void* pUserParam, const char* message)
{
	CStServerImpl* pImpl = reinterpret_cast<CStServerImpl*>(pUserParam);
	ofstream& stream = *(pImpl->m_logStream);

	SYSTEMTIME utcNow;
	ZeroMemory(&utcNow, sizeof(utcNow));
	GetSystemTime(&utcNow);
	CLock lock(pImpl->m_loggerSynchronizer);
	stream<<utcNow<<", "<<GetCurrentThreadId()<<">: "<<message<<endl<<flush;
}
 void CStServerImpl::ShutdownChannel(void* handle)
{
	m_server.ShutdownConnectionInternal(handle);
}
