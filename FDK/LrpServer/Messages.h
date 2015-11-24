#pragma once

class CChannel;
class CMessages
{
public:
	CMessages(CChannel& owner);
private:
	CMessages(const CMessages&);
	CMessages& operator = (const CMessages&);
public:
	HRESULT Add(const CMessage& message);
	bool IsEmpty() const;
public:
	void Begin(MemoryBuffer& buffer);
	size_t End(MemoryBuffer& buffer);
private:
	size_t DoEnd(const size_t count, const size_t& size, CMessage& message);
	size_t DoEndLast(const size_t sent);
	size_t DoEnd(const size_t sent);
private:
	HRESULT DoAdd(const CMessage& message);
	HRESULT DoReplace(const ptrdiff_t key, const CMessage& message);
private:
	const size_t m_messagesNumberLimit;
	const size_t m_messagesSizeLimit;
private:
	CChannel& m_owner;
	size_t m_count;
	size_t m_size;
	CMessage m_message;
	list<CMessage> m_messages;
};