#pragma once
#include "XmlSerialization.h"




enum DestructorTypes
{
	XML_ENUM(DestructorTypes, No, 0)
	XML_ENUM(DestructorTypes, Yes, 1)
	XML_ENUM(DestructorTypes, Auto, 2)
};




class Config
{
public:
	bool IsDestructorVirtual;
public:
	Config();
public:
	XML_MAP_BEGIN()
		XML_MEMBER(IsDestructorVirtual);
	XML_MAP_END()
};


extern Config gConfig;