#include "stdafx.h"
#include "Messages.h"
#include "Channel.h"

CMessages::CMessages(CChannel& owner) :
    m_owner(owner), m_messagesNumberLimit(owner.GetParameters().MessagesNumberLimit),
    m_messagesSizeLimit(owner.GetParameters().MessagesSizeLimit), m_count(), m_size()
{
    m_message.GetBuffer().Construct(0);
}
bool CMessages::IsEmpty() const
{
    const bool result = (0 == m_size);
    return result;
}
HRESULT CMessages::Add(const CMessage& message)
{
    const ptrdiff_t key = message.GetKey();
    if (key < 0)
    {
        return DoAdd(message);
    }
    else
    {
        return DoReplace(key, message);
    }
}
HRESULT CMessages::DoAdd(const CMessage& message)
{
    if (m_count == m_messagesNumberLimit)
    {
        return E_FAIL;
    }
    const size_t newSize = m_size + message.GetSize();
    if (newSize > m_messagesSizeLimit)
    {
        return E_FAIL;
    }
    m_messages.push_back(message);
    m_size = newSize;
    ++m_count;
    return S_OK;
}
HRESULT CMessages::DoReplace(const ptrdiff_t key, const CMessage& message)
{
    auto it = m_messages.begin();
    auto end = m_messages.end();

    for (; it != end; ++it)
    {
        if (key == it->GetKey())
        {
            break;
        }
    }

    if (it == end)
    {
        return DoAdd(message);
    }

    const size_t newSize = m_size + message.GetSize() - it->GetSize();

    if (newSize > m_messagesSizeLimit)
    {
        return E_FAIL;
    }
    m_messages.push_back(message);
    m_messages.erase(it);
    m_size = newSize;

    return S_FALSE;
}
namespace
{
    size_t DoBegin(const CMessage& message, const size_t position, size_t capacity, MemoryBuffer& buffer)
    {
        assert(position < capacity);

        const MemoryBuffer& source = message.GetBuffer();
        size_t requiredSize = source.GetSize() - source.GetPosition();

        size_t result = position + requiredSize;
        if (result > capacity)
        {
            result = capacity;
            requiredSize = capacity - position;
        }
        const uint8* pData = reinterpret_cast<const uint8*>(source.GetData()) + source.GetPosition();
        buffer.WriteRaw(pData, requiredSize);
        return result;
    }
}
void CMessages::Begin(MemoryBuffer& buffer)
{
    assert(0 == buffer.GetPosition());
    assert(0 == buffer.GetSize());
    const size_t capacity = buffer.GetCapacity();

    size_t position = DoBegin(m_message, 0, capacity, buffer);

    for each(const auto& element in m_messages)
    {
        if (position == capacity)
        {
            break;
        }
        position = DoBegin(element, position, capacity, buffer);
    }
    buffer.SetPosition(0);
}

size_t CMessages::End(MemoryBuffer& buffer)
{
    size_t result = m_count;

    const size_t initialSize = buffer.GetPosition();
    size_t size = initialSize;

    if (size > 0)
    {
        size = DoEndLast(size);
    }

    for (; size > 0;)
    {
        size = DoEnd(size);
    }

    result -= m_count;
    m_size -= initialSize;
    return result;
}

size_t CMessages::DoEndLast(const size_t sent)
{
    MemoryBuffer& buffer = m_message.GetBuffer();
    const size_t position = buffer.GetPosition();
    const size_t size = buffer.GetSize();

    const size_t required = size - position;
    if (0 == required)
    {
        return sent;
    }

    if (required <= sent)
    {
        buffer.SetPosition(size);
        --m_count;
        m_owner.WriteOutgoingMessage(m_message);
        return (sent - required);
    }

    buffer.SetPosition(position + sent);
    return 0;
}
size_t CMessages::DoEnd(const size_t sent)
{
    assert(!m_messages.empty());

    MemoryBuffer& buffer = m_messages.front().GetBuffer();

    const size_t position = buffer.GetPosition();
    const size_t size = buffer.GetSize();

    const size_t required = size - position;
    if (required <= sent)
    {
        m_owner.WriteOutgoingMessage(m_messages.front());
        m_messages.pop_front();
        --m_count;
        return (sent - required);
    }

    buffer.SetPosition(position + sent);
    m_message = m_messages.front();
    m_messages.pop_front();
    return 0;
}
