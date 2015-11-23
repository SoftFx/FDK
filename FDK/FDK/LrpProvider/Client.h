#pragma once



class CClient
{
public:
	CClient();
	CClient(IReceiver* pReceiver);
public:
	void OnTick(MemoryBuffer& buffer);
private:
	IReceiver* m_receiver;
};