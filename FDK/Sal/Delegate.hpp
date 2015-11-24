template<typename Functor>
class CAsynchJob<Functor, void ()> : public CJob
{
public:
	CAsynchJob(const Functor& func, CCallsWaiter* waiter):  CJob(waiter), m_func(func)
	{
	}
	virtual void Invoke()
	{
		m_func();
	}
private:
	Functor m_func;
};
template<typename R> class Delegate<R ()> : public CDelegateInfo
{
public:
	typedef R Signature ();
private:
	class Call : public BaseCall
	{
	public:
		virtual R Invoke() = 0;
	};
	template<typename T> class FuncCall : public Call
	{
	public:
		inline FuncCall(T function):m_function(function)
		{
		}
		virtual R Invoke()
		{
			return m_function();
		}
	private:
		T m_function;
	};
	template<typename T, typename Method> class MethodCall : public Call
	{
	public:
		inline MethodCall(T* instance, Method method): m_instance(instance), m_method(method)
		{
			Acquire();
		}
		virtual void Acquire()
		{
			Referenceable<T>::Acquire(m_instance);
		}
		virtual void Release()
		{
			Referenceable<T>::Release(m_instance);
		}
		virtual R Invoke()
		{
			return (m_instance->*m_method)();
		}
	private:
		T* m_instance;
		Method m_method;
	};
	template<typename T, typename Method> class ConstMethodCall : public Call
	{
	public:
		inline ConstMethodCall(const T* instance, Method method): m_instance(instance), m_method(method)
		{
			Acquire();
		}
		virtual void Acquire()
		{
			Referenceable<T>::Acquire(m_instance);
		}
		virtual void Release()
		{
			Referenceable<T>::Release(m_instance);
		}
		virtual R Invoke()
		{
			return (m_instance->*m_method)();
		}
	private:
		const T* m_instance;
		Method m_method;
	};
public:
	inline Delegate()
	{
	}
	inline Delegate(const CDelegateInfo& func):CDelegateInfo(func)
	{
	}
	template<typename T> inline Delegate(T function)
	{
		void* pointer = Memory();
		static_assert(sizeof(CDelegateInfo) >= sizeof(FuncCall<T>), "Input argument is not supported");
		new(pointer)FuncCall<T>(function);
	}
	template<typename T, typename Method> inline Delegate(T* instance, Method method)
	{
		void* pointer = Memory();
		static_assert(sizeof(CDelegateInfo) >= sizeof(MethodCall<T, Method>), "Input argument is not supported");
		new (pointer)MethodCall<T, Method>(instance, method);
	}
	template<typename T, typename Method> inline Delegate(const T* instance, Method method)
	{
		void* pointer = Memory();
		static_assert(sizeof(CDelegateInfo) >= sizeof(ConstMethodCall<T, Method>), "Input argument is not supported");
		new (pointer)ConstMethodCall<T, Method>(instance, method);
	}
	inline R operator()()
	{
		void* pointer = Memory();
		Call* call = reinterpret_cast<Call*>(pointer);
		return call->Invoke();
	}
	void DoAsynch(CThreadPool& pool, CCallsWaiter* waiter = nullptr)
	{
		CJob* pJob = new CAsynchJob<Delegate<R ()>, void ()>(*this, waiter);
		pool.AddJob(pJob);
	}
};



template<typename Functor, typename P0>
class CAsynchJob<Functor, void (P0)> : public CJob
{
public:
	CAsynchJob(const Functor& func, P0 a0, CCallsWaiter* waiter):  CJob(waiter), m_func(func)
	, m_a0(a0)
	{
	}
	virtual void Invoke()
	{
		m_func(m_a0);
	}
private:
	Functor m_func;
	typename Type2Member<P0>::Type m_a0;
};
template<typename R, typename P0> class Delegate<R (P0 a0)> : public CDelegateInfo
{
public:
	typedef R Signature (P0 a0);
private:
	class Call : public BaseCall
	{
	public:
		virtual R Invoke(P0 a0) = 0;
	};
	template<typename T> class FuncCall : public Call
	{
	public:
		inline FuncCall(T function):m_function(function)
		{
		}
		virtual R Invoke(P0 a0)
		{
			return m_function(a0);
		}
	private:
		T m_function;
	};
	template<typename T, typename Method> class MethodCall : public Call
	{
	public:
		inline MethodCall(T* instance, Method method): m_instance(instance), m_method(method)
		{
			Acquire();
		}
		virtual void Acquire()
		{
			Referenceable<T>::Acquire(m_instance);
		}
		virtual void Release()
		{
			Referenceable<T>::Release(m_instance);
		}
		virtual R Invoke(P0 a0)
		{
			return (m_instance->*m_method)(a0);
		}
	private:
		T* m_instance;
		Method m_method;
	};
	template<typename T, typename Method> class ConstMethodCall : public Call
	{
	public:
		inline ConstMethodCall(const T* instance, Method method): m_instance(instance), m_method(method)
		{
			Acquire();
		}
		virtual void Acquire()
		{
			Referenceable<T>::Acquire(m_instance);
		}
		virtual void Release()
		{
			Referenceable<T>::Release(m_instance);
		}
		virtual R Invoke(P0 a0)
		{
			return (m_instance->*m_method)(a0);
		}
	private:
		const T* m_instance;
		Method m_method;
	};
public:
	inline Delegate()
	{
	}
	inline Delegate(const CDelegateInfo& func):CDelegateInfo(func)
	{
	}
	template<typename T> inline Delegate(T function)
	{
		void* pointer = Memory();
		static_assert(sizeof(CDelegateInfo) >= sizeof(FuncCall<T>), "Input argument is not supported");
		new(pointer)FuncCall<T>(function);
	}
	template<typename T, typename Method> inline Delegate(T* instance, Method method)
	{
		void* pointer = Memory();
		static_assert(sizeof(CDelegateInfo) >= sizeof(MethodCall<T, Method>), "Input argument is not supported");
		new (pointer)MethodCall<T, Method>(instance, method);
	}
	template<typename T, typename Method> inline Delegate(const T* instance, Method method)
	{
		void* pointer = Memory();
		static_assert(sizeof(CDelegateInfo) >= sizeof(ConstMethodCall<T, Method>), "Input argument is not supported");
		new (pointer)ConstMethodCall<T, Method>(instance, method);
	}
	inline R operator()(P0 a0)
	{
		void* pointer = Memory();
		Call* call = reinterpret_cast<Call*>(pointer);
		return call->Invoke(a0);
	}
	void DoAsynch(CThreadPool& pool, P0 a0, CCallsWaiter* waiter = nullptr)
	{
		CJob* pJob = new CAsynchJob<Delegate<R (P0 a0)>, void (P0 a0)>(*this, a0, waiter);
		pool.AddJob(pJob);
	}
};



template<typename Functor, typename P0, typename P1>
class CAsynchJob<Functor, void (P0, P1)> : public CJob
{
public:
	CAsynchJob(const Functor& func, P0 a0, P1 a1, CCallsWaiter* waiter):  CJob(waiter), m_func(func)
	, m_a0(a0)
	, m_a1(a1)
	{
	}
	virtual void Invoke()
	{
		m_func(m_a0, m_a1);
	}
private:
	Functor m_func;
	typename Type2Member<P0>::Type m_a0;
	typename Type2Member<P1>::Type m_a1;
};
template<typename R, typename P0, typename P1> class Delegate<R (P0 a0, P1 a1)> : public CDelegateInfo
{
public:
	typedef R Signature (P0 a0, P1 a1);
private:
	class Call : public BaseCall
	{
	public:
		virtual R Invoke(P0 a0, P1 a1) = 0;
	};
	template<typename T> class FuncCall : public Call
	{
	public:
		inline FuncCall(T function):m_function(function)
		{
		}
		virtual R Invoke(P0 a0, P1 a1)
		{
			return m_function(a0, a1);
		}
	private:
		T m_function;
	};
	template<typename T, typename Method> class MethodCall : public Call
	{
	public:
		inline MethodCall(T* instance, Method method): m_instance(instance), m_method(method)
		{
			Acquire();
		}
		virtual void Acquire()
		{
			Referenceable<T>::Acquire(m_instance);
		}
		virtual void Release()
		{
			Referenceable<T>::Release(m_instance);
		}
		virtual R Invoke(P0 a0, P1 a1)
		{
			return (m_instance->*m_method)(a0, a1);
		}
	private:
		T* m_instance;
		Method m_method;
	};
	template<typename T, typename Method> class ConstMethodCall : public Call
	{
	public:
		inline ConstMethodCall(const T* instance, Method method): m_instance(instance), m_method(method)
		{
			Acquire();
		}
		virtual void Acquire()
		{
			Referenceable<T>::Acquire(m_instance);
		}
		virtual void Release()
		{
			Referenceable<T>::Release(m_instance);
		}
		virtual R Invoke(P0 a0, P1 a1)
		{
			return (m_instance->*m_method)(a0, a1);
		}
	private:
		const T* m_instance;
		Method m_method;
	};
public:
	inline Delegate()
	{
	}
	inline Delegate(const CDelegateInfo& func):CDelegateInfo(func)
	{
	}
	template<typename T> inline Delegate(T function)
	{
		void* pointer = Memory();
		static_assert(sizeof(CDelegateInfo) >= sizeof(FuncCall<T>), "Input argument is not supported");
		new(pointer)FuncCall<T>(function);
	}
	template<typename T, typename Method> inline Delegate(T* instance, Method method)
	{
		void* pointer = Memory();
		static_assert(sizeof(CDelegateInfo) >= sizeof(MethodCall<T, Method>), "Input argument is not supported");
		new (pointer)MethodCall<T, Method>(instance, method);
	}
	template<typename T, typename Method> inline Delegate(const T* instance, Method method)
	{
		void* pointer = Memory();
		static_assert(sizeof(CDelegateInfo) >= sizeof(ConstMethodCall<T, Method>), "Input argument is not supported");
		new (pointer)ConstMethodCall<T, Method>(instance, method);
	}
	inline R operator()(P0 a0, P1 a1)
	{
		void* pointer = Memory();
		Call* call = reinterpret_cast<Call*>(pointer);
		return call->Invoke(a0, a1);
	}
	void DoAsynch(CThreadPool& pool, P0 a0, P1 a1, CCallsWaiter* waiter = nullptr)
	{
		CJob* pJob = new CAsynchJob<Delegate<R (P0 a0, P1 a1)>, void (P0 a0, P1 a1)>(*this, a0, a1, waiter);
		pool.AddJob(pJob);
	}
};



template<typename Functor, typename P0, typename P1, typename P2>
class CAsynchJob<Functor, void (P0, P1, P2)> : public CJob
{
public:
	CAsynchJob(const Functor& func, P0 a0, P1 a1, P2 a2, CCallsWaiter* waiter):  CJob(waiter), m_func(func)
	, m_a0(a0)
	, m_a1(a1)
	, m_a2(a2)
	{
	}
	virtual void Invoke()
	{
		m_func(m_a0, m_a1, m_a2);
	}
private:
	Functor m_func;
	typename Type2Member<P0>::Type m_a0;
	typename Type2Member<P1>::Type m_a1;
	typename Type2Member<P2>::Type m_a2;
};
template<typename R, typename P0, typename P1, typename P2> class Delegate<R (P0 a0, P1 a1, P2 a2)> : public CDelegateInfo
{
public:
	typedef R Signature (P0 a0, P1 a1, P2 a2);
private:
	class Call : public BaseCall
	{
	public:
		virtual R Invoke(P0 a0, P1 a1, P2 a2) = 0;
	};
	template<typename T> class FuncCall : public Call
	{
	public:
		inline FuncCall(T function):m_function(function)
		{
		}
		virtual R Invoke(P0 a0, P1 a1, P2 a2)
		{
			return m_function(a0, a1, a2);
		}
	private:
		T m_function;
	};
	template<typename T, typename Method> class MethodCall : public Call
	{
	public:
		inline MethodCall(T* instance, Method method): m_instance(instance), m_method(method)
		{
			Acquire();
		}
		virtual void Acquire()
		{
			Referenceable<T>::Acquire(m_instance);
		}
		virtual void Release()
		{
			Referenceable<T>::Release(m_instance);
		}
		virtual R Invoke(P0 a0, P1 a1, P2 a2)
		{
			return (m_instance->*m_method)(a0, a1, a2);
		}
	private:
		T* m_instance;
		Method m_method;
	};
	template<typename T, typename Method> class ConstMethodCall : public Call
	{
	public:
		inline ConstMethodCall(const T* instance, Method method): m_instance(instance), m_method(method)
		{
			Acquire();
		}
		virtual void Acquire()
		{
			Referenceable<T>::Acquire(m_instance);
		}
		virtual void Release()
		{
			Referenceable<T>::Release(m_instance);
		}
		virtual R Invoke(P0 a0, P1 a1, P2 a2)
		{
			return (m_instance->*m_method)(a0, a1, a2);
		}
	private:
		const T* m_instance;
		Method m_method;
	};
public:
	inline Delegate()
	{
	}
	inline Delegate(const CDelegateInfo& func):CDelegateInfo(func)
	{
	}
	template<typename T> inline Delegate(T function)
	{
		void* pointer = Memory();
		static_assert(sizeof(CDelegateInfo) >= sizeof(FuncCall<T>), "Input argument is not supported");
		new(pointer)FuncCall<T>(function);
	}
	template<typename T, typename Method> inline Delegate(T* instance, Method method)
	{
		void* pointer = Memory();
		static_assert(sizeof(CDelegateInfo) >= sizeof(MethodCall<T, Method>), "Input argument is not supported");
		new (pointer)MethodCall<T, Method>(instance, method);
	}
	template<typename T, typename Method> inline Delegate(const T* instance, Method method)
	{
		void* pointer = Memory();
		static_assert(sizeof(CDelegateInfo) >= sizeof(ConstMethodCall<T, Method>), "Input argument is not supported");
		new (pointer)ConstMethodCall<T, Method>(instance, method);
	}
	inline R operator()(P0 a0, P1 a1, P2 a2)
	{
		void* pointer = Memory();
		Call* call = reinterpret_cast<Call*>(pointer);
		return call->Invoke(a0, a1, a2);
	}
	void DoAsynch(CThreadPool& pool, P0 a0, P1 a1, P2 a2, CCallsWaiter* waiter = nullptr)
	{
		CJob* pJob = new CAsynchJob<Delegate<R (P0 a0, P1 a1, P2 a2)>, void (P0 a0, P1 a1, P2 a2)>(*this, a0, a1, a2, waiter);
		pool.AddJob(pJob);
	}
};



template<typename Functor, typename P0, typename P1, typename P2, typename P3>
class CAsynchJob<Functor, void (P0, P1, P2, P3)> : public CJob
{
public:
	CAsynchJob(const Functor& func, P0 a0, P1 a1, P2 a2, P3 a3, CCallsWaiter* waiter):  CJob(waiter), m_func(func)
	, m_a0(a0)
	, m_a1(a1)
	, m_a2(a2)
	, m_a3(a3)
	{
	}
	virtual void Invoke()
	{
		m_func(m_a0, m_a1, m_a2, m_a3);
	}
private:
	Functor m_func;
	typename Type2Member<P0>::Type m_a0;
	typename Type2Member<P1>::Type m_a1;
	typename Type2Member<P2>::Type m_a2;
	typename Type2Member<P3>::Type m_a3;
};
template<typename R, typename P0, typename P1, typename P2, typename P3> class Delegate<R (P0 a0, P1 a1, P2 a2, P3 a3)> : public CDelegateInfo
{
public:
	typedef R Signature (P0 a0, P1 a1, P2 a2, P3 a3);
private:
	class Call : public BaseCall
	{
	public:
		virtual R Invoke(P0 a0, P1 a1, P2 a2, P3 a3) = 0;
	};
	template<typename T> class FuncCall : public Call
	{
	public:
		inline FuncCall(T function):m_function(function)
		{
		}
		virtual R Invoke(P0 a0, P1 a1, P2 a2, P3 a3)
		{
			return m_function(a0, a1, a2, a3);
		}
	private:
		T m_function;
	};
	template<typename T, typename Method> class MethodCall : public Call
	{
	public:
		inline MethodCall(T* instance, Method method): m_instance(instance), m_method(method)
		{
			Acquire();
		}
		virtual void Acquire()
		{
			Referenceable<T>::Acquire(m_instance);
		}
		virtual void Release()
		{
			Referenceable<T>::Release(m_instance);
		}
		virtual R Invoke(P0 a0, P1 a1, P2 a2, P3 a3)
		{
			return (m_instance->*m_method)(a0, a1, a2, a3);
		}
	private:
		T* m_instance;
		Method m_method;
	};
	template<typename T, typename Method> class ConstMethodCall : public Call
	{
	public:
		inline ConstMethodCall(const T* instance, Method method): m_instance(instance), m_method(method)
		{
			Acquire();
		}
		virtual void Acquire()
		{
			Referenceable<T>::Acquire(m_instance);
		}
		virtual void Release()
		{
			Referenceable<T>::Release(m_instance);
		}
		virtual R Invoke(P0 a0, P1 a1, P2 a2, P3 a3)
		{
			return (m_instance->*m_method)(a0, a1, a2, a3);
		}
	private:
		const T* m_instance;
		Method m_method;
	};
public:
	inline Delegate()
	{
	}
	inline Delegate(const CDelegateInfo& func):CDelegateInfo(func)
	{
	}
	template<typename T> inline Delegate(T function)
	{
		void* pointer = Memory();
		static_assert(sizeof(CDelegateInfo) >= sizeof(FuncCall<T>), "Input argument is not supported");
		new(pointer)FuncCall<T>(function);
	}
	template<typename T, typename Method> inline Delegate(T* instance, Method method)
	{
		void* pointer = Memory();
		static_assert(sizeof(CDelegateInfo) >= sizeof(MethodCall<T, Method>), "Input argument is not supported");
		new (pointer)MethodCall<T, Method>(instance, method);
	}
	template<typename T, typename Method> inline Delegate(const T* instance, Method method)
	{
		void* pointer = Memory();
		static_assert(sizeof(CDelegateInfo) >= sizeof(ConstMethodCall<T, Method>), "Input argument is not supported");
		new (pointer)ConstMethodCall<T, Method>(instance, method);
	}
	inline R operator()(P0 a0, P1 a1, P2 a2, P3 a3)
	{
		void* pointer = Memory();
		Call* call = reinterpret_cast<Call*>(pointer);
		return call->Invoke(a0, a1, a2, a3);
	}
	void DoAsynch(CThreadPool& pool, P0 a0, P1 a1, P2 a2, P3 a3, CCallsWaiter* waiter = nullptr)
	{
		CJob* pJob = new CAsynchJob<Delegate<R (P0 a0, P1 a1, P2 a2, P3 a3)>, void (P0 a0, P1 a1, P2 a2, P3 a3)>(*this, a0, a1, a2, a3, waiter);
		pool.AddJob(pJob);
	}
};



template<typename Functor, typename P0, typename P1, typename P2, typename P3, typename P4>
class CAsynchJob<Functor, void (P0, P1, P2, P3, P4)> : public CJob
{
public:
	CAsynchJob(const Functor& func, P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, CCallsWaiter* waiter):  CJob(waiter), m_func(func)
	, m_a0(a0)
	, m_a1(a1)
	, m_a2(a2)
	, m_a3(a3)
	, m_a4(a4)
	{
	}
	virtual void Invoke()
	{
		m_func(m_a0, m_a1, m_a2, m_a3, m_a4);
	}
private:
	Functor m_func;
	typename Type2Member<P0>::Type m_a0;
	typename Type2Member<P1>::Type m_a1;
	typename Type2Member<P2>::Type m_a2;
	typename Type2Member<P3>::Type m_a3;
	typename Type2Member<P4>::Type m_a4;
};
template<typename R, typename P0, typename P1, typename P2, typename P3, typename P4> class Delegate<R (P0 a0, P1 a1, P2 a2, P3 a3, P4 a4)> : public CDelegateInfo
{
public:
	typedef R Signature (P0 a0, P1 a1, P2 a2, P3 a3, P4 a4);
private:
	class Call : public BaseCall
	{
	public:
		virtual R Invoke(P0 a0, P1 a1, P2 a2, P3 a3, P4 a4) = 0;
	};
	template<typename T> class FuncCall : public Call
	{
	public:
		inline FuncCall(T function):m_function(function)
		{
		}
		virtual R Invoke(P0 a0, P1 a1, P2 a2, P3 a3, P4 a4)
		{
			return m_function(a0, a1, a2, a3, a4);
		}
	private:
		T m_function;
	};
	template<typename T, typename Method> class MethodCall : public Call
	{
	public:
		inline MethodCall(T* instance, Method method): m_instance(instance), m_method(method)
		{
			Acquire();
		}
		virtual void Acquire()
		{
			Referenceable<T>::Acquire(m_instance);
		}
		virtual void Release()
		{
			Referenceable<T>::Release(m_instance);
		}
		virtual R Invoke(P0 a0, P1 a1, P2 a2, P3 a3, P4 a4)
		{
			return (m_instance->*m_method)(a0, a1, a2, a3, a4);
		}
	private:
		T* m_instance;
		Method m_method;
	};
	template<typename T, typename Method> class ConstMethodCall : public Call
	{
	public:
		inline ConstMethodCall(const T* instance, Method method): m_instance(instance), m_method(method)
		{
			Acquire();
		}
		virtual void Acquire()
		{
			Referenceable<T>::Acquire(m_instance);
		}
		virtual void Release()
		{
			Referenceable<T>::Release(m_instance);
		}
		virtual R Invoke(P0 a0, P1 a1, P2 a2, P3 a3, P4 a4)
		{
			return (m_instance->*m_method)(a0, a1, a2, a3, a4);
		}
	private:
		const T* m_instance;
		Method m_method;
	};
public:
	inline Delegate()
	{
	}
	inline Delegate(const CDelegateInfo& func):CDelegateInfo(func)
	{
	}
	template<typename T> inline Delegate(T function)
	{
		void* pointer = Memory();
		static_assert(sizeof(CDelegateInfo) >= sizeof(FuncCall<T>), "Input argument is not supported");
		new(pointer)FuncCall<T>(function);
	}
	template<typename T, typename Method> inline Delegate(T* instance, Method method)
	{
		void* pointer = Memory();
		static_assert(sizeof(CDelegateInfo) >= sizeof(MethodCall<T, Method>), "Input argument is not supported");
		new (pointer)MethodCall<T, Method>(instance, method);
	}
	template<typename T, typename Method> inline Delegate(const T* instance, Method method)
	{
		void* pointer = Memory();
		static_assert(sizeof(CDelegateInfo) >= sizeof(ConstMethodCall<T, Method>), "Input argument is not supported");
		new (pointer)ConstMethodCall<T, Method>(instance, method);
	}
	inline R operator()(P0 a0, P1 a1, P2 a2, P3 a3, P4 a4)
	{
		void* pointer = Memory();
		Call* call = reinterpret_cast<Call*>(pointer);
		return call->Invoke(a0, a1, a2, a3, a4);
	}
	void DoAsynch(CThreadPool& pool, P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, CCallsWaiter* waiter = nullptr)
	{
		CJob* pJob = new CAsynchJob<Delegate<R (P0 a0, P1 a1, P2 a2, P3 a3, P4 a4)>, void (P0 a0, P1 a1, P2 a2, P3 a3, P4 a4)>(*this, a0, a1, a2, a3, a4, waiter);
		pool.AddJob(pJob);
	}
};



template<typename Functor, typename P0, typename P1, typename P2, typename P3, typename P4, typename P5>
class CAsynchJob<Functor, void (P0, P1, P2, P3, P4, P5)> : public CJob
{
public:
	CAsynchJob(const Functor& func, P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5, CCallsWaiter* waiter):  CJob(waiter), m_func(func)
	, m_a0(a0)
	, m_a1(a1)
	, m_a2(a2)
	, m_a3(a3)
	, m_a4(a4)
	, m_a5(a5)
	{
	}
	virtual void Invoke()
	{
		m_func(m_a0, m_a1, m_a2, m_a3, m_a4, m_a5);
	}
private:
	Functor m_func;
	typename Type2Member<P0>::Type m_a0;
	typename Type2Member<P1>::Type m_a1;
	typename Type2Member<P2>::Type m_a2;
	typename Type2Member<P3>::Type m_a3;
	typename Type2Member<P4>::Type m_a4;
	typename Type2Member<P5>::Type m_a5;
};
template<typename R, typename P0, typename P1, typename P2, typename P3, typename P4, typename P5> class Delegate<R (P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5)> : public CDelegateInfo
{
public:
	typedef R Signature (P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5);
private:
	class Call : public BaseCall
	{
	public:
		virtual R Invoke(P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5) = 0;
	};
	template<typename T> class FuncCall : public Call
	{
	public:
		inline FuncCall(T function):m_function(function)
		{
		}
		virtual R Invoke(P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5)
		{
			return m_function(a0, a1, a2, a3, a4, a5);
		}
	private:
		T m_function;
	};
	template<typename T, typename Method> class MethodCall : public Call
	{
	public:
		inline MethodCall(T* instance, Method method): m_instance(instance), m_method(method)
		{
			Acquire();
		}
		virtual void Acquire()
		{
			Referenceable<T>::Acquire(m_instance);
		}
		virtual void Release()
		{
			Referenceable<T>::Release(m_instance);
		}
		virtual R Invoke(P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5)
		{
			return (m_instance->*m_method)(a0, a1, a2, a3, a4, a5);
		}
	private:
		T* m_instance;
		Method m_method;
	};
	template<typename T, typename Method> class ConstMethodCall : public Call
	{
	public:
		inline ConstMethodCall(const T* instance, Method method): m_instance(instance), m_method(method)
		{
			Acquire();
		}
		virtual void Acquire()
		{
			Referenceable<T>::Acquire(m_instance);
		}
		virtual void Release()
		{
			Referenceable<T>::Release(m_instance);
		}
		virtual R Invoke(P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5)
		{
			return (m_instance->*m_method)(a0, a1, a2, a3, a4, a5);
		}
	private:
		const T* m_instance;
		Method m_method;
	};
public:
	inline Delegate()
	{
	}
	inline Delegate(const CDelegateInfo& func):CDelegateInfo(func)
	{
	}
	template<typename T> inline Delegate(T function)
	{
		void* pointer = Memory();
		static_assert(sizeof(CDelegateInfo) >= sizeof(FuncCall<T>), "Input argument is not supported");
		new(pointer)FuncCall<T>(function);
	}
	template<typename T, typename Method> inline Delegate(T* instance, Method method)
	{
		void* pointer = Memory();
		static_assert(sizeof(CDelegateInfo) >= sizeof(MethodCall<T, Method>), "Input argument is not supported");
		new (pointer)MethodCall<T, Method>(instance, method);
	}
	template<typename T, typename Method> inline Delegate(const T* instance, Method method)
	{
		void* pointer = Memory();
		static_assert(sizeof(CDelegateInfo) >= sizeof(ConstMethodCall<T, Method>), "Input argument is not supported");
		new (pointer)ConstMethodCall<T, Method>(instance, method);
	}
	inline R operator()(P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5)
	{
		void* pointer = Memory();
		Call* call = reinterpret_cast<Call*>(pointer);
		return call->Invoke(a0, a1, a2, a3, a4, a5);
	}
	void DoAsynch(CThreadPool& pool, P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5, CCallsWaiter* waiter = nullptr)
	{
		CJob* pJob = new CAsynchJob<Delegate<R (P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5)>, void (P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5)>(*this, a0, a1, a2, a3, a4, a5, waiter);
		pool.AddJob(pJob);
	}
};



template<typename Functor, typename P0, typename P1, typename P2, typename P3, typename P4, typename P5, typename P6>
class CAsynchJob<Functor, void (P0, P1, P2, P3, P4, P5, P6)> : public CJob
{
public:
	CAsynchJob(const Functor& func, P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5, P6 a6, CCallsWaiter* waiter):  CJob(waiter), m_func(func)
	, m_a0(a0)
	, m_a1(a1)
	, m_a2(a2)
	, m_a3(a3)
	, m_a4(a4)
	, m_a5(a5)
	, m_a6(a6)
	{
	}
	virtual void Invoke()
	{
		m_func(m_a0, m_a1, m_a2, m_a3, m_a4, m_a5, m_a6);
	}
private:
	Functor m_func;
	typename Type2Member<P0>::Type m_a0;
	typename Type2Member<P1>::Type m_a1;
	typename Type2Member<P2>::Type m_a2;
	typename Type2Member<P3>::Type m_a3;
	typename Type2Member<P4>::Type m_a4;
	typename Type2Member<P5>::Type m_a5;
	typename Type2Member<P6>::Type m_a6;
};
template<typename R, typename P0, typename P1, typename P2, typename P3, typename P4, typename P5, typename P6> class Delegate<R (P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5, P6 a6)> : public CDelegateInfo
{
public:
	typedef R Signature (P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5, P6 a6);
private:
	class Call : public BaseCall
	{
	public:
		virtual R Invoke(P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5, P6 a6) = 0;
	};
	template<typename T> class FuncCall : public Call
	{
	public:
		inline FuncCall(T function):m_function(function)
		{
		}
		virtual R Invoke(P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5, P6 a6)
		{
			return m_function(a0, a1, a2, a3, a4, a5, a6);
		}
	private:
		T m_function;
	};
	template<typename T, typename Method> class MethodCall : public Call
	{
	public:
		inline MethodCall(T* instance, Method method): m_instance(instance), m_method(method)
		{
			Acquire();
		}
		virtual void Acquire()
		{
			Referenceable<T>::Acquire(m_instance);
		}
		virtual void Release()
		{
			Referenceable<T>::Release(m_instance);
		}
		virtual R Invoke(P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5, P6 a6)
		{
			return (m_instance->*m_method)(a0, a1, a2, a3, a4, a5, a6);
		}
	private:
		T* m_instance;
		Method m_method;
	};
	template<typename T, typename Method> class ConstMethodCall : public Call
	{
	public:
		inline ConstMethodCall(const T* instance, Method method): m_instance(instance), m_method(method)
		{
			Acquire();
		}
		virtual void Acquire()
		{
			Referenceable<T>::Acquire(m_instance);
		}
		virtual void Release()
		{
			Referenceable<T>::Release(m_instance);
		}
		virtual R Invoke(P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5, P6 a6)
		{
			return (m_instance->*m_method)(a0, a1, a2, a3, a4, a5, a6);
		}
	private:
		const T* m_instance;
		Method m_method;
	};
public:
	inline Delegate()
	{
	}
	inline Delegate(const CDelegateInfo& func):CDelegateInfo(func)
	{
	}
	template<typename T> inline Delegate(T function)
	{
		void* pointer = Memory();
		static_assert(sizeof(CDelegateInfo) >= sizeof(FuncCall<T>), "Input argument is not supported");
		new(pointer)FuncCall<T>(function);
	}
	template<typename T, typename Method> inline Delegate(T* instance, Method method)
	{
		void* pointer = Memory();
		static_assert(sizeof(CDelegateInfo) >= sizeof(MethodCall<T, Method>), "Input argument is not supported");
		new (pointer)MethodCall<T, Method>(instance, method);
	}
	template<typename T, typename Method> inline Delegate(const T* instance, Method method)
	{
		void* pointer = Memory();
		static_assert(sizeof(CDelegateInfo) >= sizeof(ConstMethodCall<T, Method>), "Input argument is not supported");
		new (pointer)ConstMethodCall<T, Method>(instance, method);
	}
	inline R operator()(P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5, P6 a6)
	{
		void* pointer = Memory();
		Call* call = reinterpret_cast<Call*>(pointer);
		return call->Invoke(a0, a1, a2, a3, a4, a5, a6);
	}
	void DoAsynch(CThreadPool& pool, P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5, P6 a6, CCallsWaiter* waiter = nullptr)
	{
		CJob* pJob = new CAsynchJob<Delegate<R (P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5, P6 a6)>, void (P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5, P6 a6)>(*this, a0, a1, a2, a3, a4, a5, a6, waiter);
		pool.AddJob(pJob);
	}
};



template<typename Functor, typename P0, typename P1, typename P2, typename P3, typename P4, typename P5, typename P6, typename P7>
class CAsynchJob<Functor, void (P0, P1, P2, P3, P4, P5, P6, P7)> : public CJob
{
public:
	CAsynchJob(const Functor& func, P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5, P6 a6, P7 a7, CCallsWaiter* waiter):  CJob(waiter), m_func(func)
	, m_a0(a0)
	, m_a1(a1)
	, m_a2(a2)
	, m_a3(a3)
	, m_a4(a4)
	, m_a5(a5)
	, m_a6(a6)
	, m_a7(a7)
	{
	}
	virtual void Invoke()
	{
		m_func(m_a0, m_a1, m_a2, m_a3, m_a4, m_a5, m_a6, m_a7);
	}
private:
	Functor m_func;
	typename Type2Member<P0>::Type m_a0;
	typename Type2Member<P1>::Type m_a1;
	typename Type2Member<P2>::Type m_a2;
	typename Type2Member<P3>::Type m_a3;
	typename Type2Member<P4>::Type m_a4;
	typename Type2Member<P5>::Type m_a5;
	typename Type2Member<P6>::Type m_a6;
	typename Type2Member<P7>::Type m_a7;
};
template<typename R, typename P0, typename P1, typename P2, typename P3, typename P4, typename P5, typename P6, typename P7> class Delegate<R (P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5, P6 a6, P7 a7)> : public CDelegateInfo
{
public:
	typedef R Signature (P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5, P6 a6, P7 a7);
private:
	class Call : public BaseCall
	{
	public:
		virtual R Invoke(P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5, P6 a6, P7 a7) = 0;
	};
	template<typename T> class FuncCall : public Call
	{
	public:
		inline FuncCall(T function):m_function(function)
		{
		}
		virtual R Invoke(P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5, P6 a6, P7 a7)
		{
			return m_function(a0, a1, a2, a3, a4, a5, a6, a7);
		}
	private:
		T m_function;
	};
	template<typename T, typename Method> class MethodCall : public Call
	{
	public:
		inline MethodCall(T* instance, Method method): m_instance(instance), m_method(method)
		{
			Acquire();
		}
		virtual void Acquire()
		{
			Referenceable<T>::Acquire(m_instance);
		}
		virtual void Release()
		{
			Referenceable<T>::Release(m_instance);
		}
		virtual R Invoke(P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5, P6 a6, P7 a7)
		{
			return (m_instance->*m_method)(a0, a1, a2, a3, a4, a5, a6, a7);
		}
	private:
		T* m_instance;
		Method m_method;
	};
	template<typename T, typename Method> class ConstMethodCall : public Call
	{
	public:
		inline ConstMethodCall(const T* instance, Method method): m_instance(instance), m_method(method)
		{
			Acquire();
		}
		virtual void Acquire()
		{
			Referenceable<T>::Acquire(m_instance);
		}
		virtual void Release()
		{
			Referenceable<T>::Release(m_instance);
		}
		virtual R Invoke(P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5, P6 a6, P7 a7)
		{
			return (m_instance->*m_method)(a0, a1, a2, a3, a4, a5, a6, a7);
		}
	private:
		const T* m_instance;
		Method m_method;
	};
public:
	inline Delegate()
	{
	}
	inline Delegate(const CDelegateInfo& func):CDelegateInfo(func)
	{
	}
	template<typename T> inline Delegate(T function)
	{
		void* pointer = Memory();
		static_assert(sizeof(CDelegateInfo) >= sizeof(FuncCall<T>), "Input argument is not supported");
		new(pointer)FuncCall<T>(function);
	}
	template<typename T, typename Method> inline Delegate(T* instance, Method method)
	{
		void* pointer = Memory();
		static_assert(sizeof(CDelegateInfo) >= sizeof(MethodCall<T, Method>), "Input argument is not supported");
		new (pointer)MethodCall<T, Method>(instance, method);
	}
	template<typename T, typename Method> inline Delegate(const T* instance, Method method)
	{
		void* pointer = Memory();
		static_assert(sizeof(CDelegateInfo) >= sizeof(ConstMethodCall<T, Method>), "Input argument is not supported");
		new (pointer)ConstMethodCall<T, Method>(instance, method);
	}
	inline R operator()(P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5, P6 a6, P7 a7)
	{
		void* pointer = Memory();
		Call* call = reinterpret_cast<Call*>(pointer);
		return call->Invoke(a0, a1, a2, a3, a4, a5, a6, a7);
	}
	void DoAsynch(CThreadPool& pool, P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5, P6 a6, P7 a7, CCallsWaiter* waiter = nullptr)
	{
		CJob* pJob = new CAsynchJob<Delegate<R (P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5, P6 a6, P7 a7)>, void (P0 a0, P1 a1, P2 a2, P3 a3, P4 a4, P5 a5, P6 a6, P7 a7)>(*this, a0, a1, a2, a3, a4, a5, a6, a7, waiter);
		pool.AddJob(pJob);
	}
};



