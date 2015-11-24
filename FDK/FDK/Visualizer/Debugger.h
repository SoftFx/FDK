#pragma once


typedef struct tagDEBUGHELPER
{
	DWORD dwVersion;
	HRESULT (WINAPI *ReadDebuggeeMemory)( struct tagDEBUGHELPER *pThis, DWORD dwAddr, DWORD nWant, VOID* pWhere, DWORD *nGot );
	// from here only when dwVersion >= 0x20000
	DWORDLONG (WINAPI *GetRealAddress)( struct tagDEBUGHELPER *pThis );
	HRESULT (WINAPI *ReadDebuggeeMemoryEx)( struct tagDEBUGHELPER *pThis, DWORDLONG qwAddr, DWORD nWant, VOID* pWhere, DWORD *nGot );
	int (WINAPI *GetProcessorType)( struct tagDEBUGHELPER *pThis );
} DEBUGHELPER;

//typedef HRESULT (WINAPI *CUSTOMVIEWER)( DWORD dwAddress, DEBUGHELPER *pHelper, int nBase, BOOL bUniStrings, char *pResult, size_t max, DWORD reserved );

class Debugger
{
public:
	Debugger(DEBUGHELPER* argument, DWORD address);
public:
	inline DWORD Version();	
	inline HRESULT ReadDebuggeeMemory(DWORD dwAddr, DWORD nWant, VOID* pWhere, DWORD *nGot);	
	inline DWORDLONG GetRealAddress();	
	inline HRESULT ReadDebuggeeMemoryEx(DWORDLONG qwAddr, DWORD nWant, VOID* pWhere, DWORD *nGot);	
	inline int GetProcessorType();
	inline HRESULT ReadBuffer(DWORD address, void* buffer, DWORD size);
public:
	template<typename T> HRESULT Read(T& argument)
	{
		return Read(m_address, argument);
	}
	template<typename T> HRESULT Read(DWORD address, T& argument)
	{
		return ReadBuffer(address, &argument, sizeof(T));
	}
private:
	DEBUGHELPER*	m_this;
	DWORD			m_address;
};
HRESULT ReadStdString(Debugger debugger, DWORD address, DWORD maximumLength, string& st);


inline Debugger::Debugger(DEBUGHELPER* argument, DWORD address):m_this(argument), m_address(address)
{
}


inline DWORD Debugger::Version()
{
	const DWORD result = m_this->dwVersion;
	return result;
}
inline HRESULT Debugger::ReadDebuggeeMemory(DWORD dwAddr, DWORD nWant, VOID* pWhere, DWORD *nGot)
{
	const HRESULT result = m_this->ReadDebuggeeMemory(m_this, dwAddr, nWant, pWhere, nGot);
	return result;
}
inline DWORDLONG Debugger::GetRealAddress()
{
	const DWORDLONG result = m_this->GetRealAddress(m_this);
	return result;
}
inline HRESULT Debugger::ReadDebuggeeMemoryEx(DWORDLONG qwAddr, DWORD nWant, VOID* pWhere, DWORD *nGot)
{
	const HRESULT result = m_this->ReadDebuggeeMemoryEx(m_this, qwAddr, nWant, pWhere, nGot);
	return result;
}
inline int Debugger::GetProcessorType()
{
	const int result = m_this->GetProcessorType(m_this);
	return result;
}
inline HRESULT Debugger::ReadBuffer(DWORD address, void* buffer, DWORD size)
{
	DWORD read = 0;
	const HRESULT result = ReadDebuggeeMemory(address, size, buffer, &read);
	return_if_failed(result);
	return_if_true(size != read, E_FAIL);
	return result;	
}


