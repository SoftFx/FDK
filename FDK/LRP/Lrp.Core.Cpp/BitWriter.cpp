#include "stdafx.h"
#include "BitWriter.h"
#include "MemoryBuffer.h"

#define DoFlushIfNeeded() if (cDataSize == m_position) { DoFlush(); }


namespace
{
	const uint32 cDataSize = 8; // number of bits in one byte
}
void CBitWriter::WriteBool(bool value)
{
	assert(m_position < cDataSize);

	if (value)
	{
		m_data |= (1 << m_position);
	}
	++m_position;
	DoFlushIfNeeded();
}
void CBitWriter::WriteUInt32(uint32 value, uint32 size)
{
	if (size > 32)
	{
		throw overflow_error("CBitWriter::WriteUInt32(): size");
	}
	if (value >= (1LL << size))
	{
		throw overflow_error("CBitWriter::WriteUInt32(): value");
	}

	uint64 _value = value;
	_value <<= m_position;

	const uint8* pData = reinterpret_cast<const uint8*>(&_value);

	// the first step: we should write the first byte of data
	m_data |= (*pData);
	uint32 written = min(cDataSize - m_position, size);

	m_position += written;
	++pData;
	size -= written;

	DoFlushIfNeeded();

	// the second step: we should write full bytes of data
	if (size >= cDataSize)
	{
		const uint32 count = size / cDataSize;
		m_buffer.WriteRaw(pData, count);
		pData += count;
		size -= count * cDataSize;
	}

	// the third step: we should write remained byte of data

	m_data |= (*pData);
	m_position += size;

	assert(m_position < cDataSize);
}
void CBitWriter::WriteInt32(__int32 value, unsigned __int32 size)
{
	if (size > 32)
	{
		throw overflow_error("CBitWriter::WriteInt32(): size");
	}
	if (value >= 0)
	{
		WriteBool(false);
		WriteUInt32(static_cast<uint32>(value), size - 1);
	}
	else
	{
		WriteBool(true);
		WriteUInt32(static_cast<uint32>(-(1 + value)), size - 1);
	}
}

void CBitWriter::Flush()
{
	if (m_position > 0)
	{
		m_position = cDataSize;
		DoFlush();
	}
}
void CBitWriter::DoFlush()
{
	assert(cDataSize == m_position);
	WriteUInt8(m_data, m_buffer);
	m_data = 0;
	m_position = 0;
}
