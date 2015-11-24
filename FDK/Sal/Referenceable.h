#pragma once

template<typename T, bool> class ReferenceableEx;

template<typename T> class ReferenceableEx<T, true>
{
public:
	static inline void Acquire(T* pInstance)
	{
		__if_exists(T::Acquire)
		{
			pInstance->Acquire();
		}
		__if_not_exists(T::Acquire)
		{
			pInstance->AddRef();
		}
	}
	static inline void Acquire(const T* pInstance)
	{
		__if_exists(T::Acquire)
		{
			pInstance->Acquire();
		}
		__if_not_exists(T::Acquire)
		{
			pInstance->AddRef();
		}
	}
	static inline void Release(T* pInstance)
	{
		pInstance->Release();
	}
	static inline void Release(const T* pInstance)
	{
		pInstance->Release();
	}
};
template<typename T> class ReferenceableEx<T, false>
{
public:
	static inline void Acquire(T* /*pInstance*/)
	{
	}
	static inline void Acquire(const T* /*pInstance*/)
	{
	}
	static inline void Release(T* /*pInstance*/)
	{
	}
	static inline void Release(const T* /*pInstance*/)
	{
	}
};
template<typename T> class IsReferenceable
{
public:
	const static bool Value = false;
};

#define DEFINE_REFERENCEABLE(T) template<> class IsReferenceable<T>\
								{\
								public:\
									const static bool Value = true;\
								};



template<typename T> class Referenceable : public ReferenceableEx<T, IsReferenceable<T>::Value >
{
};