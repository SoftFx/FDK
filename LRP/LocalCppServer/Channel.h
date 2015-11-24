#pragma once

#include "Simple.h"
#include "Extended.h"


class CChannel
{
public:
	static CSimple& GetSimple();
	static CExtended& GetExtended();
};