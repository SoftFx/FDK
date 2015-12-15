#include "stdafx.h"
#include "BinaryReadHelpers.h"




#ifdef BINARY_READER
#error macro redefinition
#endif
#define BINARY_READER(T)\
	CBinaryReader& operator >> (CBinaryReader& stream, T& argument)\
	{\
		stream.Read(sizeof(T), &argument);\
		return stream;\
	}\

#include "BinaryReadHelpers.hpp"

#undef BINARY_READER

CBinaryReader& operator >> (CBinaryReader& stream, FIX::UtcTimeStamp& argument)
{
	stream>>argument.m_date>>argument.m_time;
	return stream;
}



