#ifndef __Core_Fx_Handle__
#define __Core_Fx_Handle__


class CHandlesPool;

class CORE_API CFxHandle
{
public:
	CFxHandle();
	CFxHandle(const CFxHandle&);
	CFxHandle& operator = (const CFxHandle&);
	virtual ~CFxHandle();
public:
	void Acquire();
	void Release();
private:
	void DoAcquire();
	bool DoRelease();
private:
	size_t m_counter;
private:
	friend class CHandlesPool;
};
#endif
