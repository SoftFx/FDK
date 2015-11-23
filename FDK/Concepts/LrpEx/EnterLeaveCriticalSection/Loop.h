#pragma once


class CLoop
{
public:
	CLoop(const size_t count, HANDLE event, CRITICAL_SECTION* pSynchronizer);
	~CLoop();
public:
	void Construct();
	void WaitFor() const;
	size_t GetDuration() const;
private:
	static DWORD __stdcall ThreadFunction(void* arg);
private:
	void Loop();
private:
	const size_t m_count;
	HANDLE m_event;
	CRITICAL_SECTION* m_synchronizer;
	HANDLE m_thread;
	size_t m_duration;
private:
	friend ostream& operator << (ostream& stream, const CLoop& loop);
};
