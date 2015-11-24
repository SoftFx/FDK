#ifndef __Core_Fx_Ref__
#define __Core_Fx_Ref__

template<class T> class _NoAcquireReleaseInterface : public T
{
protected:
	_NoAcquireReleaseInterface();
private:
	void Acquire();
	void Release(); 
};

template<typename T> class FxRef
{
public:
	FxRef() : m_pointer()
	{
	}
	FxRef(T* arg) : m_pointer(arg)
	{
	}
	FxRef(const FxRef& arg) : m_pointer(arg.m_pointer)
	{
		Acquire();
	}
	FxRef(FxRef&& arg) : m_pointer(arg.m_pointer)
	{
		arg.m_pointer = nullptr;
	}
	FxRef& operator = (T* arg)
	{
		Release();
		m_pointer = arg;
		Acquire();
	}
	FxRef& operator = (const FxRef& arg)
	{
		if (m_pointer != arg.m_pointer)
		{
			Release();
			m_pointer = arg.m_pointer;
			Acquire();
		}
	}
	~FxRef()
	{
		Release();
	}
public:
	void Release()
	{
		T* pointer = m_pointer;
		if (nullptr != pointer)
		{
			m_pointer = nullptr;
			pointer->Release();
		}
	}
	inline _NoAcquireReleaseInterface<T>* operator ->()
	{
		return reinterpret_cast<_NoAcquireReleaseInterface<T>*>(m_pointer);
	}
	T& operator *()
	{
		return *m_pointer;
	}
	operator T* ()
	{
		return m_pointer;
	}
private:
	void Acquire()
	{
		if (nullptr != m_pointer)
		{
			m_pointer->Acquire();
		}
	}
private:
	T* m_pointer;
};

#endif