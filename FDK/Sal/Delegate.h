#ifndef __Sal_Delegate__
#define __Sal_Delegate__
#include "ThreadPool.h"



template<typename T> class Type2Member
{
public:
	typedef T Type;
};
template<typename T> class Type2Member<const T&>
{
public:
	typedef T Type;
};
template<typename T> class Type2Member<T&>
{
public:
	typedef T Type;
};

class CDelegateInfo
{
public:
	friend bool operator == (const CDelegateInfo& first, const CDelegateInfo& second)throw();
	friend bool operator != (const CDelegateInfo& first, const CDelegateInfo& second)throw();
	friend bool operator < (const CDelegateInfo& first, const CDelegateInfo& second)throw();
	friend bool operator > (const CDelegateInfo& first, const CDelegateInfo& second)throw();
	friend bool operator <= (const CDelegateInfo& first, const CDelegateInfo& second)throw();
	friend bool operator >= (const CDelegateInfo& first, const CDelegateInfo& second)throw();
public:
	bool IsNull()const throw();
	const void* Memory()const throw();
public:
	CDelegateInfo()throw();
	CDelegateInfo(const CDelegateInfo& info);
	CDelegateInfo& operator = (const CDelegateInfo& info);
	~CDelegateInfo();
protected:
	void* Memory()throw();
	void Acquire();
	void Release();
protected:
	class BaseCall
	{
	public:
		virtual void Acquire(){};
		virtual void Release(){};
	public:
		virtual ~BaseCall(){};
	};
private:
	/// <summary>
	/// [2/22/2010 marmysh]
	/// A memory buffer for internal delegate construction. To be sure that
	/// is enough memory for object creation I used compile time assert.
	/// </summary>
	void* m_buffer[6];
};

/// <summary>
/// [2/22/2010 marmysh]
/// So two delegates are equal if:
/// 1) They have the same internal type (it means the same pointer to a virtual table)
/// 2) They contain references to the same functions or methods
/// 3) They contain references to the same objects (optional, for methods only)
/// </summary>
/// <param name="first">an delegate</param>
/// <param name="second">an delegate</param>
/// <returns>true or false</returns>
inline bool operator == (const CDelegateInfo& first, const CDelegateInfo& second)throw()
{
	return (0 == memcmp(first.m_buffer, second.m_buffer, sizeof(first.m_buffer)));
}
/// <summary>
/// [2/22/2010 marmysh]
/// So two delegates are equal if:
/// 1) They have the same internal type (it means the same pointer to a virtual table)
/// 2) They contain references to the same functions or methods
/// 3) They contain references to the same objects (optional, for methods only)
/// </summary>
/// <param name="first">an delegate</param>
/// <param name="second">an delegate</param>
/// <returns>true or false</returns>
inline bool operator != (const CDelegateInfo& first, const CDelegateInfo& second)throw()
{
	return (0 != memcmp(first.m_buffer, second.m_buffer, sizeof(first.m_buffer)));
}
inline bool operator < (const CDelegateInfo& first, const CDelegateInfo& second)throw()
{
	return (memcmp(first.m_buffer, second.m_buffer, sizeof(first.m_buffer)) < 0);
}
inline bool operator > (const CDelegateInfo& first, const CDelegateInfo& second)throw()
{
	return (memcmp(first.m_buffer, second.m_buffer, sizeof(first.m_buffer)) > 0);
}
inline bool operator <= (const CDelegateInfo& first, const CDelegateInfo& second)throw()
{
	return (memcmp(first.m_buffer, second.m_buffer, sizeof(first.m_buffer)) <= 0);
}
inline bool operator >= (const CDelegateInfo& first, const CDelegateInfo& second)throw()
{
	return (memcmp(first.m_buffer, second.m_buffer, sizeof(first.m_buffer)) >= 0);
}
/// <summary>
/// Just zero memory buffer.
/// </summary>
inline CDelegateInfo::CDelegateInfo()throw()
{
	memset(m_buffer, 0, sizeof(m_buffer));
}
inline CDelegateInfo::CDelegateInfo(const CDelegateInfo& info)
{
	memcpy(m_buffer, info.m_buffer, sizeof(m_buffer));
	Acquire();
}
inline CDelegateInfo& CDelegateInfo::operator = (const CDelegateInfo& info)
{
	if (this != &info)
	{
		Release();
		memcpy(m_buffer, info.m_buffer, sizeof(m_buffer));
		Acquire();
	}
	return *this;
}
inline CDelegateInfo::~CDelegateInfo()
{
	Release();
}
/// <summary>
/// The method returns a pointer to the internal memory buffer.
/// </summary>
/// <returns>can't be null</returns>
inline void* CDelegateInfo::Memory()throw()
{
	return m_buffer;
}
/// <summary>
/// The method returns a pointer to the internal memory buffer.
/// </summary>
/// <returns>can't be null</returns>
inline const void* CDelegateInfo::Memory()const throw()
{
	return m_buffer;
}
/// <summary>
/// [2/19/2010 marmysh]
/// The method returns true, if the delegate hasn't been initialized.
/// </summary>
/// <returns>true or false</returns>
inline bool CDelegateInfo::IsNull()const throw()
{
	// if the delegate has been created then m_buffer[0] contains pointer to a virtual table
	// in this case it can't be null
	return (nullptr == m_buffer[0]);
}
inline void CDelegateInfo::Acquire()
{
	if (nullptr != m_buffer[0])
	{
		BaseCall* pCall = reinterpret_cast<BaseCall*>(Memory());
		pCall->Acquire();
	}
}
inline void CDelegateInfo::Release()
{
	if (nullptr != m_buffer[0])
	{
		BaseCall* pCall = reinterpret_cast<BaseCall*>(Memory());
		pCall->Release();
	}
}


#ifdef new
#pragma push_macro("new")
#undef new
#define DFTS_RESTORE_NEW
#endif

#include "Types.h"
#include "Referenceable.h"
#include "Job.h"
template<typename Signature> class Delegate;
template<typename Functor, typename Signature> class CAsynchJob;

#include "Delegate.hpp"


#ifdef DFTS_RESTORE_NEW
#pragma pop_macro("new")
#undef DFTS_RESTORE_NEW
#endif

#endif