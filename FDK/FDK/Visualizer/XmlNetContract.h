#pragma once


// common types definitions
// these types are invariant for x86/x64 platform

typedef wchar_t utf;

typedef __int8				int8;
typedef unsigned __int8		uint8;


typedef __int16				int16;
typedef unsigned __int16	uint16;


typedef __int32				int32;
typedef unsigned __int32	uint32;

typedef __int64				int64;
typedef unsigned __int64	uint64;


// useful macros
#define RETURN_IF_FAILED(result)	if (FAILED(result))\
									{\
										return result;\
									}


#define BREAK_IF_TRUE(condition)	if (condition)\
									{\
										break;\
									}




// specific types definitions
#ifndef __cplusplus_cli

const HRESULT XML_PARSE_CONTINUE = 0x20000000L;

typedef string String;

#define VECTOR(type) vector<type>
#define MAP(key, value) map<key, value>
#define PUBLIC
#define MAKE_REF_TYPE(type, reference) typedef type reference

#define XML_MAP_BEGIN()		template<typename XmlProvider> HRESULT Process(XmlProvider& provider)\
							{\
								HRESULT result = S_OK;

#define XML_MEMBER(member)	RETURN_IF_FAILED(result)\
							result = XmlPrefixProcess(provider, #member);\
							RETURN_IF_FAILED(result)\
							result = XmlProcess(provider, member);\
							RETURN_IF_FAILED(result)\
							if(XML_PARSE_CONTINUE == result) { result = S_OK; } else { result = XmlPostfixProcess(provider, member, #member); }

#define XML_MAP_END()		return result; }

#define XML_ENUM(type, name, value) type##_##name = value,


#else
typedef System::String^ String;
#define VECTOR(type) System::Collections::Generic::List<type>^
#define MAP(key, value) Map<key, value>^


#define PUBLIC public
#define MAKE_REF_TYPE(type, reference) typedef type^ reference;


#define XML_MAP_BEGIN()
#define XML_MEMBER(member)
#define XML_MAP_END()

#define XML_ENUM(type, name, value) [System::Xml::Serialization::XmlEnum(Name = #value)] name = value,



#endif