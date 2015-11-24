#pragma once





class CExecutor
{
public:
	CExecutor(const char* text);
	virtual ~CExecutor();
public:
	void OnLogon();
	void OnTick();
	void OnLogout();
	void OnTimeout();
public:
	void Acquire();
	void Release();
private:
	mutable volatile LONG m_counter;
	string m_text;
};