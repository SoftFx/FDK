#pragma once
#include "Simple.h"
#include "Extended.h"


class CChannel
{
public:
	CSimple& GetSimple();
	CExtended& GetExtended();
private:
	CSimple m_simple;
	CExtended m_extended;
};