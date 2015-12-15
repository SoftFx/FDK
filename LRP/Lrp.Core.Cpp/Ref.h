#ifndef __FXOpenApi_Core_Fx_Ref__
#define __FXOpenApi_Core_Fx_Ref__

template<class T> class _NoAcquireReleaseInterface : public T
{
protected:
	_NoAcquireReleaseInterface();
private:
	void Acquire();
	void Release(); 
};

#define REF_OPERATOR(type)	friend inline bool operator type (const Ref<T>& first, const Ref<T>& second)\
							{\
								const bool result = (first.m_pointer type second.m_pointer);\
								return result;\
							}

template<typename T> class Ref
{
public:
	Ref() : m_pointer()
	{
	}
	Ref(T* arg) : m_pointer(arg)
	{
	}
	Ref(const Ref& arg) : m_pointer(arg.m_pointer)
	{
		Acquire();
	}
	Ref(const Ref&& arg) : m_pointer(arg.m_pointer)
	{
		arg.m_pointer = nullptr;
	}
	Ref& operator = (T* arg)
	{
		Release();
		m_pointer = arg;
		Acquire();
	}
	Ref& operator = (const Ref& arg)
	{
		if (m_pointer != arg.m_pointer)
		{
			Release();
			m_pointer = arg.m_pointer;
			Acquire();
		}
		return *this;
	}
	~Ref()
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
public:
	REF_OPERATOR(<);
	REF_OPERATOR(>);
	REF_OPERATOR(==);
	REF_OPERATOR(!=);
	REF_OPERATOR(<=);
	REF_OPERATOR(>=);
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