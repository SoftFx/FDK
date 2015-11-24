#pragma once
#include "Referenceable.h"

class CMessageData : public CReferenceable
{
public:
	const ptrdiff_t Key;
	const uint16 ComponentId;
	const uint16 MethodId;
	MemoryBuffer Buffer;
public:
	CMessageData(const ptrdiff_t key);
	CMessageData(const ptrdiff_t key, const uint16 componentId, const uint16 methodId, MemoryBuffer& buffer);
private:
	CMessageData(const CMessageData&);
	CMessageData& operator = (const CMessageData&);
};