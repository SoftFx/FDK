@echo off
setlocal enabledelayedexpansion
for /l %%R in (0 1 8) do (
	SET /A COUNT=%%R - 1

rem typename R, typename P0, typename P1, typename P2
	set TYPES=typename R
	for /l %%I in (0 1 !COUNT!) do (
		set TYPES=!TYPES!, typename P%%I
	)
	set TYPES2=
	for /l %%I in (0 1 !COUNT!) do (
		set TYPES2=!TYPES2!, typename P%%I
	)

	rem (P0 a0, P1 a1, P2 a2)
	set PARAMS=^(
	if not "%%R" == "0" (SET PARAMS=^(P0 a0)
	for /l %%I in (1 1 !COUNT!) do (
		set PARAMS=!PARAMS!, P%%I a%%I
	)
	set PARAMS=!PARAMS!^)

	rem , P0 a0, P1 a1, P2 a2
	set PARAMS2=
	for /l %%I in (0 1 !COUNT!) do (
		set PARAMS2=!PARAMS2!, P%%I a%%I
	)

	rem (a0, a1, a2)
	set ARGS=^(
	if not "%%R" == "0" (SET ARGS=^(a0)
	for /l %%I in (1 1 !COUNT!) do (
		set ARGS=!ARGS!, a%%I
	)
	set ARGS=!ARGS!^)

	rem (m_a0, m_a1, m_a2)
	set ARGS2=^(
	if not "%%R" == "0" (SET ARGS2=^(m_^a0)
	for /l %%I in (1 1 !COUNT!) do (
		set ARGS2=!ARGS2!, m^_a%%I
	)

	set ARGS2=!ARGS2!^)

	rem a0, a1, a2
	set ARGS3=
	for /l %%I in (0 1 !COUNT!) do (
		set ARGS3=!ARGS3!, a%%I
	)


	rem Functor, void (P0, P1, P2)
	set SIGN=Functor, void ^(
	if not "%%R" == "0" (SET SIGN=!SIGN!P0)
	for /l %%I in (1 1, !COUNT!) do (
		set SIGN=!SIGN!, P%%I
	)
	set SIGN=!SIGN!^)


	echo template^<typename Functor!TYPES2!^>
	echo class CAsynchJob^<!SIGN!^> ^: public CJob
	echo {
	echo public^:
	echo 	CAsynchJob(const Functor^& func!PARAMS2!^, CCallsWaiter* waiter^)^:  CJob^(waiter^), m_func(func^)
	for /l %%I in (0 1 !COUNT!) do (
		echo 	, m^_a%%I(a%%I^)
	)
	echo 	{
	echo 	}
	echo 	virtual void Invoke^(^)
	echo 	{
	echo 		m_func!ARGS2!;
	echo 	}
	echo private^:
	echo 	Functor m^_func;
	for /l %%I in (0 1 !COUNT!) do (
		echo 	typename Type2Member^<P%%I^>::Type m^_a%%I;
	)
	echo };



	echo template^<!TYPES!^> class Delegate^<R !PARAMS!^> ^: public CDelegateInfo
	echo {
	echo public^:
	echo 	typedef R Signature !PARAMS!;
	echo private^:
	echo 	class Call ^: public BaseCall
	echo 	{
	echo 	public^:
	echo 		virtual R Invoke!PARAMS! = 0;
	echo 	};
	echo 	template^<typename T^> class FuncCall ^: public Call
	echo 	{
	echo 	public^:
	echo 		inline FuncCall^(T function^)^:m^_function^(function^)
	echo 		{
	echo 		}
	echo 		virtual R Invoke!PARAMS!
	echo 		{
	echo 			return m_function!ARGS!;
	echo 		}
	echo 	private^:
	echo 		T m_function;
	echo 	};
	echo 	template^<typename T^, typename Method^> class MethodCall ^: public Call
	echo 	{
	echo 	public^:
	echo 		inline MethodCall^(T^* instance, Method method^)^: m_instance(instance^), m_method(method^)
	echo 		{
	echo 			Acquire(^);
	echo 		}
	echo 		virtual void Acquire(^)
	echo 		{
	echo 			Referenceable^<T^>^:^:Acquire(m_instance^);
	echo 		}
	echo 		virtual void Release(^)
	echo 		{
	echo 			Referenceable^<T^>^:^:Release(m_instance^);
	echo 		}
	echo 		virtual R Invoke!PARAMS!
	echo 		{
	echo 			return (m_instance-^>^*m_method^)!ARGS!;
	echo 		}
	echo 	private^:
	echo 		T* m_instance;
	echo 		Method m_method;
	echo 	};
	echo 	template^<typename T^, typename Method^> class ConstMethodCall ^: public Call
	echo 	{
	echo 	public^:
	echo 		inline ConstMethodCall^(const T^* instance, Method method^)^: m_instance(instance^), m_method(method^)
	echo 		{
	echo 			Acquire(^);
	echo 		}
	echo 		virtual void Acquire(^)
	echo 		{
	echo 			Referenceable^<T^>^:^:Acquire(m_instance^);
	echo 		}
	echo 		virtual void Release(^)
	echo 		{
	echo 			Referenceable^<T^>^:^:Release(m_instance^);
	echo 		}
	echo 		virtual R Invoke!PARAMS!
	echo 		{
	echo 			return (m_instance-^>^*m_method^)!ARGS!;
	echo 		}
	echo 	private^:
	echo 		const T* m_instance;
	echo 		Method m_method;
	echo 	};
	echo public^:
	echo 	inline Delegate(^)
	echo 	{
	echo 	}
	echo 	inline Delegate(const CDelegateInfo^& func^)^:CDelegateInfo(func^)
	echo 	{
	echo 	}
	echo 	template^<typename T^> inline Delegate(T function^)
	echo 	{
	echo 		void^* pointer = Memory(^);
	echo 		static^_assert(sizeof(CDelegateInfo^) ^>= sizeof(FuncCall^<T^>^)^, "Input argument is not supported"^);
	echo 		new(pointer^)FuncCall^<T^>(function^);
	echo 	}
	echo 	template^<typename T, typename Method^> inline Delegate(T^* instance, Method method^)
	echo 	{
	echo 		void^* pointer = Memory(^);
	echo 		static^_assert(sizeof(CDelegateInfo^) ^>= sizeof(MethodCall^<T^, Method^>^)^, "Input argument is not supported"^);
	echo 		new (pointer^)MethodCall^<T, Method^>(instance, method^);
	echo 	}
	echo 	template^<typename T, typename Method^> inline Delegate(const T^* instance, Method method^)
	echo 	{
	echo 		void^* pointer = Memory(^);
	echo 		static^_assert(sizeof(CDelegateInfo^) ^>= sizeof(ConstMethodCall^<T^, Method^>^)^, "Input argument is not supported"^);
	echo 		new (pointer^)ConstMethodCall^<T, Method^>(instance, method^);
	echo 	}
	echo 	inline R operator(^)!PARAMS!
	echo 	{
	echo 		void^* pointer = Memory(^);
	echo 		Call^* call = reinterpret_cast^<Call^*^>(pointer^);
	echo 		return call-^>Invoke!ARGS!;
	echo 	}
	echo 	void DoAsynch(CThreadPool^& pool!PARAMS2!, CCallsWaiter* waiter = nullptr^)
	echo 	{
	echo 		CJob^* pJob = new CAsynchJob^<Delegate^<R !PARAMS!^>, void !PARAMS!^>(^*this!ARGS3!, waiter^);
	echo 		pool^.AddJob(pJob^);
	echo 	}
	echo };



	echo.
	echo.
	echo.
)
