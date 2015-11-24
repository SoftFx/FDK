#include "stdafx.h"
#include "BitReader.h"
#include "MemoryBuffer.h"


namespace
{
	const uint32 cDataSize = 8; // number of bits in one byte
}

bool CBitReader::ReadBool()
{
	DoReadIfNeeded();
	const bool result = ((m_data & (1 << m_position)) != 0);
	++m_position;
	return result;
}
__int32 CBitReader::ReadInt32(unsigned __int32 size)
{
	if (size > 32)
	{
		throw overflow_error("CBitReader::ReadInt32(): size");
	}
	bool sign = ReadBool();
	uint32 value = ReadUInt32(size - 1);
	if (sign)
	{
		const uint32 result = - static_cast<int32>(value) - 1;
		return result;
	}
	else
	{
		return static_cast<int32>(value);
	}
}
uint32 CBitReader::ReadUInt32(uint32 size)
{
	if (size > 32)
	{
		throw overflow_error("CBitReader::ReadUInt32(): size");
	}

	const uint32 position = m_position;

	uint64 value = 0;
	uint8* pData = reinterpret_cast<uint8*>(&value);



	// the first step: we should read the first byte of data
	*pData = m_data;

	uint32 read = min(size, cDataSize - m_position);
	size -= read;
	m_position += read;

	// the second step: we should read full bytes of data
	for (; size >= cDataSize; size -= cDataSize)
	{
		++pData;
		*pData = ReadUInt8(m_buffer);
	}

	if (size > 0)
	{
		++pData;
		DoRead();
		*pData = m_data;
		m_position = size;
	}

	*pData = (*pData) & ((1 << m_position) - 1);

	value >>= position;

	uint32 result = static_cast<uint32>(value);
	return result;
}
void CBitReader::DoReadIfNeeded()
{
	if (cDataSize == m_position)
	{
		DoRead();
	}
}
void CBitReader::DoRead()
{
	assert(cDataSize == m_position);
	m_data = ReadUInt8(m_buffer);
	m_position = 0;
}

