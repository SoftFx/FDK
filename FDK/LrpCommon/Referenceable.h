#pragma once

class CReferenceable
{
public:
	CReferenceable();
	virtual ~CReferenceable();
public:
	void Acquire();
	void Release();
private:
	volatile LONG m_counter;
};
