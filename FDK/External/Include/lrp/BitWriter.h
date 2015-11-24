#pragma once

class MemoryBuffer;
class CBitWriter
{
public:
	inline CBitWriter(MemoryBuffer& buffer) : m_buffer(buffer), m_data(), m_position()
	{
	}
private:
	CBitWriter(const CBitWriter&);
	CBitWriter& operator = (const CBitWriter&);
public:
	LRPCORE_API void WriteBool(bool value);
	LRPCORE_API void WriteUInt32(unsigned __int32 value, unsigned __int32 size);
	LRPCORE_API void WriteInt32(__int32 value, unsigned __int32 size);
public:
	LRPCORE_API void Flush();
private:
	void DoFlush();
private:
	MemoryBuffer& m_buffer;
private:
	unsigned __int8 m_data;
	unsigned __int32 m_position;
};