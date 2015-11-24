#pragma once

#include "BinaryReader.h"


#ifdef BINARY_READER
#error macro redefinition
#endif
#define BINARY_READER(T)\
	CBinaryReader& operator >> (CBinaryReader& stream, T& argument);\

#include "BinaryReadHelpers.hpp"

#undef BINARY_READER


CBinaryReader& operator >> (CBinaryReader& stream, FIX::UtcTimeStamp& argument);


