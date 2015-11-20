#pragma once
//#include "Ref.h"
//
//class MemoryBuffer;
//class CMtServerImpl;
//
//
//class CMtChannelImpl
//{
//public:
//	CMtChannelImpl(CMtServerImpl& server, SOCKET socket, void* handle, LrpInvokeHandler handler);
//	~CMtChannelImpl();
//public:
//	void Finalize();
//private:
//	void Acquire();
//	void Release();
//private:
//	bool Construct();
//public:
//	void Process();
//private: // receiving part
//	static unsigned __stdcall ReceivingThread(void* arg);
//	void ReceivingLoop();
//	bool ReceiveData();
//private: // sending part
//	static unsigned __stdcall SendingThread(void* arg);
//	void SendingLoop();
//	bool SendData();
//	bool SendData(MemoryBuffer& buffer);
//private:
//	CMtServerImpl& m_server;
//	LrpInvokeHandler m_handler;
//	SOCKET m_socket;
//	void* m_handle;
//private:// common part
//	volatile bool m_continue;
//private:// receiving part
//	CriticalSection m_receivingSynchronizer;
//	deque<MemoryBuffer*> m_receivingData;
//	HANDLE m_receivingThread;
//private:// sending part
//	CriticalSection m_sendingSynchronizer;
//	vector<MemoryBuffer*> m_sendingData;
//	vector<MemoryBuffer*> m_sendingData2;
//	HANDLE m_sendingThread;
//	HANDLE m_sendingEvent;
//private:
//	volatile LONG m_counter;
//private:
//	friend Ref<CMtChannelImpl>;
//};
//
//
//typedef Ref<CMtChannelImpl> Channel;