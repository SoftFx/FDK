#pragma once

class MemoryBuffer;

class CBitReader
{
public:
	inline CBitReader(MemoryBuffer& buffer) : m_buffer(buffer), m_data(), m_position(8)
	{
	}
private:
	CBitReader(const CBitReader&);
	CBitReader& operator = (const CBitReader&);
public:
	LRPCORE_API bool ReadBool();
	LRPCORE_API unsigned __int32 ReadUInt32(unsigned __int32 size);
	LRPCORE_API __int32 ReadInt32(unsigned __int32 size);
private:
	void DoReadIfNeeded();
	void DoRead();
private:
	MemoryBuffer& m_buffer;
private:
	unsigned __int8 m_data;
	unsigned __int32 m_position;
};