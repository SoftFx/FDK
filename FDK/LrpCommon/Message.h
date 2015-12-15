#pragma once


class CMessageData;

class CMessage
{
public:
	CMessage();
	CMessage(const CMessage& msg);
	CMessage(ptrdiff_t key, const uint16 componentId, const uint16 methodId, MemoryBuffer& buffer);
	CMessage& operator = (const CMessage& msg);
	~CMessage();
public:
	MemoryBuffer& GetBuffer();
	const MemoryBuffer& GetBuffer() const;
	const size_t GetSize() const;
	const ptrdiff_t GetKey() const;
	const uint16 GetComponentId() const;
	const uint16 GetMethodId() const;
private:
	CMessageData* m_data;
};