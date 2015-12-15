#include "stdafx.h"
#include "Executor.h"

CExecutor::CExecutor(const char* text) : m_counter(), m_text(text)
{
}
CExecutor::~CExecutor()
{
	cout<<"CExecutor::~CExecutor()"<<endl;
}
void CExecutor::Acquire()
{
	InterlockedIncrement(&m_counter);
}
void CExecutor::Release()
{
	if (0 == InterlockedDecrement(&m_counter))
	{
		delete this;
	}
}
void CExecutor::OnLogon()
{
	Sleep(1000);
	cout<<"CExecutor::OnLogon(): "<<m_text<<endl;
}
void CExecutor::OnTick()
{
	cout<<"CExecutor::OnTick(): "<<m_text<<endl;
}
void CExecutor::OnLogout()
{
	cout<<"CExecutor::OnLogout(): "<<m_text<<endl;
}
