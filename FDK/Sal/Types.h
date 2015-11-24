#ifndef __Sal_Types__
#define __Sal_Types__
// generic types
typedef signed char			int8;
typedef unsigned char		uint8;


typedef short				int16;
typedef unsigned short		uint16;


typedef int					int32;
typedef unsigned int		uint32;

typedef long long			int64;
typedef unsigned long long	uint64;

#ifdef UNICODE
typedef std::wstring tstring;
#else
typedef std::string tstring;
#endif



#endif
