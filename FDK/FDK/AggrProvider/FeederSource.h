#pragma once
#include "FeederEvents.h"
#include "ConnectionParams.h"
#include "BinaryReader.h"



class CBinaryReader;
class CFeederSource
{
public:
	CFeederSource(const CConnectionParams& params);
	~CFeederSource();
public:
	bool Construct();
	void Finalize();
public:
	void Acquire();
	bool Release();
	const CConnectionParams& Params()const;
private:
	void ThreadMethod();
	bool ConstructInternal();
	void FinalizeInternal();
	void EnableKeepAlive();
	void MainLoop();
	void RunLoop();
	void Loop();
	bool Connect();
	void CloseSocket();
	void CreateSocket();
	void ShutdownSocket();
	static unsigned int __stdcall ThreadFunction(void* pointer);
public:
	void OnCommand(CBinaryReader& stream);
	void OnUpdateFilteredQuote(CBinaryReader& stream);
	void OnUpdateRawQuote(CBinaryReader& stream);
	void OnUpdateShiftedQuote(CBinaryReader& stream);
	void OnUpdatePositions(CBinaryReader& stream);
	void OnUpdateAccountInfo(CBinaryReader& stream);
	void OnActivateOrders(CBinaryReader& stream);
	void OnResetECNTCLogins(CBinaryReader& stream);
	void OnLogon(CBinaryReader& stream);
	void OnLogout(CBinaryReader& stream);
	void OnProtocolVersion(CBinaryReader& stream);
	void OnSessionInfo(CBinaryReader& stream);
	void OnSymbolsInfo(CBinaryReader& stream);
	void OnUpdateShiftedQuote2(CBinaryReader& stream);
	void OnSkip(CBinaryReader& stream);
public:
	void operator += (IFeederHandler* pHandler);
	void operator -= (IFeederHandler* pHandler);
private:
	void Output(const char* message);
	void Output(const char* message, const map<int32, set<CFSymbolInfo> >& arg);
	template<typename T> void Output(const char* message, const T& arg)
	{
		CLogger* pLogger = m_logger.get();
		if (nullptr != pLogger)
		{
			stringstream stream;
			stream.precision(DBL_DIG);
			stream<<boolalpha;
			stream<<message<<arg;
			string st = stream.str();
			pLogger->Output(st);
		}
	}
private:
	size_t m_counter;
	SOCKET m_socket;
	HANDLE m_thread;
	volatile bool m_isContinue;
	DWORD m_sleepInterval;
	CCriticalSection m_publicSection; // must be used for public methods only
	CCriticalSection m_privateSection;
	CConnectionParams m_params;
private:
	CFeederEvents m_events;
	auto_ptr<CLogger> m_logger;
};
