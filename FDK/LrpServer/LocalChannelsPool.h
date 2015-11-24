#pragma once
#include "Parameters.h"

class CLocalChannelsPool
{
public:
	static void* Constructor(const CParameters& params);
	static void Destructor(void* handle);
};