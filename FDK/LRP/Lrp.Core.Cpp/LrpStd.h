#pragma once



#ifdef LRP_STD_DEBUG_MODE
#define _LRP_STD_SIZE_OF 1
#else
#define _LRP_STD_SIZE_OF 0
#endif



class CLrpString
{
#ifndef LRP_STD
	void* m_pointers[2 + _LRP_STD_SIZE_OF];
	char m_buffer[16];
#endif
};



class CLrpVector
{
#ifndef LRP_STD
	void* m_pointers[3 + _LRP_STD_SIZE_OF];
#endif
};


class CLrpMap
{
#ifndef LRP_STD
	void* m_pointers[2 + _LRP_STD_SIZE_OF];
#endif
};



#ifdef LRP_STD
#define LrpString std::string
#define LrpVector(T) std::vector<T>
#define LrpMap(K, V) std::map<K, V>
#else
#define LrpString CLrpString
#define LrpVector(T) CLrpVector
#define LrpMap(K, V) CLrpMap
#endif

