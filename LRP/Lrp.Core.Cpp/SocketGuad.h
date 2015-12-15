#pragma once


class CSocketGuard
{
public:
	CSocketGuard(SOCKET s) : m_socket(s)
	{
	}
	~CSocketGuard()
	{
		Reset();
	}
public:
	void Reset()
	{
		if (INVALID_SOCKET != m_socket)
		{
			closesocket(m_socket);
			m_socket = INVALID_SOCKET;
		}
	}
	SOCKET Release()
	{
		SOCKET result = m_socket;
		m_socket = INVALID_SOCKET;
		return result;
	}
private:
	CSocketGuard(const CSocketGuard&);
	CSocketGuard& operator = (const CSocketGuard&);
private:
	SOCKET m_socket;
};