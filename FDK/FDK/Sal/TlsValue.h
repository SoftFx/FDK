#ifndef __TlsValue__
#define __TlsValue__

#ifdef _MSC_VER

template<class T> class TlsValue
{
public:
	TlsValue():m_tlsIndex(TLS_OUT_OF_INDEXES)
	{
		m_tlsIndex = TlsAlloc();
	}
	~TlsValue()
	{
		TlsFree(m_tlsIndex);
	}
public:
	void operator = (T argument)
	{
		void* value = reinterpret_cast<void*>(argument);
		TlsSetValue(m_tlsIndex, value);
	}
	operator T ()
	{
		void* value = TlsGetValue(m_tlsIndex);
		T result = reinterpret_cast<T>(value);
		return result;
	}	
private:
	DWORD m_tlsIndex;
};
#endif



#ifdef _MSC_VER
#define TLS_VALUE(type, name) TlsValue<type> name;
#else
#define TLS_VALUE(type, name) __thread type name;
#endif


#endif
