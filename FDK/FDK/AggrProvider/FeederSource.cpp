#include "stdafx.h"
#include "SocketHelper.h"
#include "FeederSource.h"

namespace
{

	const char* socket_hostname( const char* name )
	{

		struct hostent* host_ptr = 0;
		struct in_addr** paddr;
		struct in_addr saddr;

#if( GETHOSTBYNAME_R_INPUTS_RESULT || GETHOSTBYNAME_R_RETURNS_RESULT )
		hostent host;
		char buf[1024];
		int error;
#endif

		saddr.s_addr = inet_addr( name );
		if ( saddr.s_addr != ( unsigned ) - 1 ) return name;

#if GETHOSTBYNAME_R_INPUTS_RESULT
		gethostbyname_r( name, &host, buf, sizeof(buf), &host_ptr, &error );
#elif GETHOSTBYNAME_R_RETURNS_RESULT
		host_ptr = gethostbyname_r( name, &host, buf, sizeof(buf), &error );
#else
		host_ptr = gethostbyname( name );
#endif

		if ( host_ptr == 0 ) return 0;

		paddr = ( struct in_addr ** ) host_ptr->h_addr_list;
		return inet_ntoa( **paddr );


	}
	int socket_connect(int socket, const char* address, int port)
	{

		const char* hostname = socket_hostname( address );
		if( hostname == 0 ) return -1;

		sockaddr_in addr;
		addr.sin_family = PF_INET;
		addr.sin_port = htons( port );
		addr.sin_addr.s_addr = inet_addr( hostname );

		int result = connect( socket, reinterpret_cast < sockaddr* > ( &addr ), sizeof( addr ) );

		return result;
	}
}

namespace
{
	template<typename K, typename V> ostream& operator << (ostream& stream, const map<K, V>& arg)
	{
		stream<<"["<<arg.size()<<"]"<<endl;
		for each(const auto& element in arg)
		{
			stream<<"\t["<<element.first<<"] = {"<<element.second<<"}"<<endl;
		}
		return stream;
	}
	template<typename K> ostream& operator << (ostream& stream, const set<K>& arg)
	{
		stream<<"["<<arg.size()<<"] = {";
		auto it = arg.begin();
		auto end = arg.end();
		if (it != end)
		{
			stream<<*it<<';';
			++it;
		}
		for (; it != end; ++it)
		{
			stream<<" "<<*it<<';';
		}
		stream<<'}';
		return stream;
	}
}


CFeederSource::CFeederSource(const CConnectionParams& params) : m_counter(), m_socket(INVALID_SOCKET), m_thread(), m_params(params), m_isContinue(false), m_sleepInterval()
{
	if (!params.LogPath.empty())
	{
		stringstream stream;
		stream<<"feeder_"<<params.Address<<'_'<<params.Port<<".log";
		string filename = stream.str();
		string path = FxCombinePath(params.LogPath, filename);
		m_logger.reset(new CLogger(path));
	}
}
CFeederSource::~CFeederSource()
{
	FinalizeInternal();
	CloseSocket();
}
bool CFeederSource::ConstructInternal()
{
	m_isContinue = true;
	m_thread = reinterpret_cast<HANDLE>(_beginthreadex(NULL, 0, &CFeederSource::ThreadFunction, this, 0, nullptr));
	const bool result = (nullptr != m_thread);
	return result;
}
bool CFeederSource::Construct()
{
	CLock lock(m_publicSection);
	FinalizeInternal();
	const bool result = ConstructInternal();
	if (!result)
	{
		FinalizeInternal();
	}
	return result;
}

unsigned int __stdcall CFeederSource::ThreadFunction(void* pointer)
{
	CFeederSource* client = reinterpret_cast<CFeederSource*>(pointer);
	__try
	{
		client->ThreadMethod();
	}
	__except(EXCEPTION_EXECUTE_HANDLER)
	{
		return 1;
	}
	return 0;
}
void CFeederSource::EnableKeepAlive()
{
	DWORD size = 0;
	tcp_keepalive keepAlive = {1, 10000, 3000};
	WSAIoctl(m_socket, SIO_KEEPALIVE_VALS, &keepAlive, sizeof(keepAlive), NULL, 0, &size, NULL, NULL);
}
void CFeederSource::ThreadMethod()
{
	try
	{
		MainLoop();
	}
	catch (const exception&)
	{
	}
}
void CFeederSource::MainLoop()
{
	Output("CFeederSource::MainLoop(): start");
	RunLoop();
	for (; m_isContinue; )
	{
		Sleep(m_sleepInterval);
		RunLoop();
	}
	Output("CFeederSource::MainLoop(): finish");
}
void CFeederSource::RunLoop()
{
	Output("CFeederSource::RunLoop(): start");
	const bool status = Connect();
	if (status)
	{
		Loop();
	}
	Output("CFeederSource::RunLoop(): finish");
}
void CFeederSource::Loop()
{
	try
	{
		vector<char> buffer;
		for (;;)
		{
			const bool status = ReceiveBuffer(m_socket, buffer);
			if (!status)
			{
				break;
			}
			CBinaryReader stream(buffer);
			OnCommand(stream);
		}
	}
	catch (const exception&)
	{
	}
	ShutdownSocket();
	CloseSocket();
}

void CFeederSource::Finalize()
{
	CLock lock(m_publicSection);
	FinalizeInternal();
}

void CFeederSource::FinalizeInternal()
{
	m_isContinue = false;
	ShutdownSocket();
	if (nullptr != m_thread)
	{
		WaitForSingleObject(m_thread, INFINITE);
		m_thread = nullptr;
	}
}

bool CFeederSource::Connect()
{
	Output("CFeederSource::Connect(): start");
	{// don't remove the bracket
		CLock lock(m_privateSection);
		if (!m_isContinue)
		{
			Output("CFeederSource::Connect(): finish because stopping");
			return false;
		}
		CreateSocket();
		Output("CFeederSource::Connect(): connecting");
		const int status = socket_connect(static_cast<int>(m_socket), m_params.Address.c_str(), m_params.Port);
		if (SOCKET_ERROR == status)
		{
			Output("CFeederSource::Connect(): couldn't connect WSAGetLastError() = ", WSAGetLastError());
			return false;
		}
		Output("CFeederSource::Connect(): new connection has been established");
		EnableKeepAlive();
	}// don't remove the bracket
	Output("CFeederSource::Connect(): sending username = ", m_params.Username);
	vector<char> buffer(m_params.Username.begin(), m_params.Username.end());
	bool result = SendBuffer(m_socket, buffer);
	if (!result)
	{
		ShutdownSocket();
		CloseSocket();
		Output("CFeederSource::Connect(): couldn't send username");
		return result;
	}
	Output("CFeederSource::Connect(): username has been sent");
	Output("CFeederSource::Connect(): sending password = ", m_params.Password);
	buffer.clear();
	buffer.insert(buffer.end(), m_params.Password.begin(), m_params.Password.end());
	result = SendBuffer(m_socket, buffer);
	if (!result)
	{
		ShutdownSocket();
		CloseSocket();
		Output("CFeederSource::Connect(): couldn't send password");
		return false;
	}
	Output("CFeederSource::Connect(): password has been sent");
	return true;
}
void CFeederSource::CloseSocket()
{
	CLock lock(m_privateSection);
	if (INVALID_SOCKET != m_socket)
	{
		closesocket(m_socket);
		m_socket = INVALID_SOCKET;
	}
}
void CFeederSource::CreateSocket()
{
	CLock lock(m_privateSection);
	if (INVALID_SOCKET == m_socket)
	{
		m_socket = ::socket(PF_INET, SOCK_STREAM, IPPROTO_TCP);
	}
}
void CFeederSource::ShutdownSocket()
{
	CLock lock(m_privateSection);
	if (INVALID_SOCKET != m_socket)
	{
		shutdown(m_socket, SD_BOTH);
	}
}
namespace
{
	typedef void (CFeederSource::*CommandMethod)(CBinaryReader& stream);
	CommandMethod cMethods[] =
	{
		&CFeederSource::OnUpdateFilteredQuote,	// eQuotesBroadcastCommands_UpdateFilteredQuote
		&CFeederSource::OnLogon,				// eQuotesBroadcastCommands_Logon
		&CFeederSource::OnLogout,				// eQuotesBroadcastCommands_Logout
		&CFeederSource::OnUpdateRawQuote,		// eQuotesBroadcastCommands_UpdateRawQuote
		&CFeederSource::OnUpdateShiftedQuote,	// eQuotesBroadcastCommands_UpdateShiftedQuote
		&CFeederSource::OnUpdatePositions,		// eQuotesBroadcastCommands_UpdatePositionReport
		&CFeederSource::OnUpdateAccountInfo,	// eQuotesBroadcastCommands_UpdateAccountInfo
		&CFeederSource::OnActivateOrders,		// eQuotesBroadcastCommands_ActivateOrders
		&CFeederSource::OnResetECNTCLogins,		// eQuotesBroadcastCommands_ResetECNTransferringCoefficient
		&CFeederSource::OnProtocolVersion,		// eQuotesBroadcastCommands_ProtocolVersion
		&CFeederSource::OnSessionInfo,			// eQuotesBroadcastCommands_SessionInfo
		&CFeederSource::OnSymbolsInfo,			// eQuotesBroadcastCommands_SymbolsInfo
		&CFeederSource::OnSkip, 				// eQuotesBroadcastCommands_UpdateFilteredQuote = 12
		&CFeederSource::OnSkip,					// eQuotesBroadcastCommands_UpdateRawQuote = 13
		&CFeederSource::OnUpdateShiftedQuote2	// eQuotesBroadcastCommands_UpdateShiftedQuote = 14

	};
}

void CFeederSource::OnCommand(CBinaryReader& stream)
{
	uint32 index = 0;
	stream>>index;
	if (index >= _countof(cMethods))
	{
		assert(!"unknown command");
		return;
	}
	CommandMethod method = cMethods[index];
	(this->*method)(stream);
}
void CFeederSource::OnUpdateFilteredQuote(CBinaryReader& /*stream*/)
{
}
void CFeederSource::OnUpdateRawQuote(CBinaryReader& /*stream*/)
{
}
void CFeederSource::OnUpdateShiftedQuote(CBinaryReader& stream)
{
	shared_ptr<CFLevel2> symbol(new CFLevel2());
	stream>>(*symbol);
	assert(stream.EndOfStream());

	m_events.RaiseTick(symbol);
}

void CFeederSource::OnUpdateShiftedQuote2(CBinaryReader& stream)
{
	uint16 version = 0;
	stream>>version;
	shared_ptr<CFLevel2> symbol(new CFLevel2(version));
	stream>>(*symbol);
	//assert(stream.EndOfStream());
	m_events.RaiseTick(symbol);
}

void CFeederSource::OnUpdatePositions(CBinaryReader& stream)
{
	shared_ptr<map<int32, map<string, double> > > bankToPositions(new map<int32, map<string, double> >());
	stream>>(*bankToPositions);
	assert(stream.EndOfStream());
	m_events.RaisePositions(bankToPositions);
}
void CFeederSource::OnUpdateAccountInfo(CBinaryReader& stream)
{
	Output("CFeederSource::OnUpdateAccountInfo(): start");
	shared_ptr<map<int32, CFAccountInfo> > accountsInformation(new map<int32, CFAccountInfo>());
	stream>>*accountsInformation;
	assert(stream.EndOfStream());
	Output("CFeederSource::OnUpdateAccountInfo(): accountsInformation", *accountsInformation);

	m_events.RaiseAccountInfo(accountsInformation);
	Output("CFeederSource::OnUpdateAccountInfo(): finish");
}
void CFeederSource::OnActivateOrders(CBinaryReader& /*stream*/)
{
}
void CFeederSource::OnResetECNTCLogins(CBinaryReader& /*stream*/)
{
}
void CFeederSource::OnProtocolVersion(CBinaryReader& stream)
{
	Output("CFeederSource::OnProtocolVersion(): start");
	string protocolVersion;
	stream>>protocolVersion;
	assert(stream.EndOfStream());
	Output("CFeederSource::OnProtocolVersion(): protocolVersion = ", protocolVersion);

	m_events.RaiseProtocolVersion(protocolVersion);
	Output("CFeederSource::OnProtocolVersion(): finish");
}
void CFeederSource::OnSessionInfo(CBinaryReader& stream)
{
	Output("CFeederSource::OnSessionInfo(): start");
	map<int32, CFSessionInfo> bankToSession;
	stream>>bankToSession;
	assert(stream.EndOfStream());
	Output("CFeederSource::OnSessionInfo(): bankToSession", bankToSession);

	m_events.RaiseSessionInfo(bankToSession);
	Output("CFeederSource::OnSessionInfo(): finish");
}
void CFeederSource::OnSymbolsInfo(CBinaryReader& stream)
{
	Output("CFeederSource::OnSymbolsInfo(): start");
	map<int32, set<CFSymbolInfo> > bankToSymbols;
	stream>>bankToSymbols;
	assert(stream.EndOfStream());
	Output("CFeederSource::OnSymbolsInfo():", bankToSymbols);

	m_events.RaiseSymbolInfo(bankToSymbols);
	Output("CFeederSource::OnSymbolsInfo(): finish");
}
void CFeederSource::OnLogon(CBinaryReader& stream)
{
	Output("CFeederSource::OnLogon(): start");
	set<int32> ids;
	stream>>ids;
	assert(stream.EndOfStream());
	Output("CFeederSource::OnLogon(): ids", ids);

	m_events.RaiseLogon(ids);
	Output("CFeederSource::OnLogon(): finish");
}
void CFeederSource::OnLogout(CBinaryReader& stream)
{
	Output("CFeederSource::OnLogout(): start");
	set<int32> ids;
	stream>>ids;
	assert(stream.EndOfStream());
	Output("CFeederSource::OnLogout(): ids", ids);

	m_events.RaiseLogout(ids);
	Output("CFeederSource::OnLogout(): finish");
}
void CFeederSource::operator += (IFeederHandler* pHandler)
{
	m_events += pHandler;
}
void CFeederSource::operator-= (IFeederHandler* pHandler)
{
	m_events -= pHandler;
}
void CFeederSource::Acquire()
{
	++m_counter;
}
bool CFeederSource::Release()
{
	--m_counter;
	return (0 == m_counter);
}
const CConnectionParams& CFeederSource::Params() const
{
	return m_params;
}
void CFeederSource::OnSkip(CBinaryReader& /*stream*/)
{

}
void CFeederSource::Output(const char* message)
{
	CLogger* pLogger = m_logger.get();
	if (nullptr != pLogger)
	{
		pLogger->Output(message);
	}
}

void CFeederSource::Output(const char* message, const map<int32, set<CFSymbolInfo> >& arg)
{
	CLogger* pLogger = m_logger.get();
	if (nullptr == pLogger)
	{
		return;
	}
	stringstream stream;
	stream<<"test";
	stream<<message<<"["<<arg.size()<<"]"<<endl;
	for each(const auto& element in arg)
	{
		stream<<"\t["<<element.first<<"] = symbols["<<element.second.size()<<"]"<<endl;
		for each(const auto& symbol in element.second)
		{
			stream<<"\t\t"<<symbol<<endl;
		}
	}
	string st = stream.str();
	pLogger->Output(st);
}

