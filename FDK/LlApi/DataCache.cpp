#include "stdafx.h"
#include "DataCache.h"

CDataCache::CDataCache(CFxQueue& queue)
    : m_queue(queue)
    , m_isEventGenerated(false)
    , m_isSessionInfoInitialized(false)
{
}

CFxSessionInfo CDataCache::GetSessionInfo() const
{
    CSharedLocker lock(m_synchronizer);
    return m_sessionInfo;
}

void CDataCache::UpdateSessionInfo(const CFxSessionInfo& sessionInfo)
{
    CExclusiveLocker lock(m_synchronizer);
    m_sessionInfo = sessionInfo;
    m_isSessionInfoInitialized = true;
}

void CDataCache::Clear()
{
    m_sessionInfo = CFxSessionInfo();
    m_isSessionInfoInitialized = false;
    m_isEventGenerated = false;
}

void CDataCache::Update(bool isInitialized)
{
    const bool isCacheInitialized = isInitialized && m_isSessionInfoInitialized;

    if (!isCacheInitialized)
    {
        return;
    }

    if (!m_isEventGenerated)
    {
        CFxEventInfo eventInfo;
        CFxMessage message(FX_MSG_CACHE_UPDATED, eventInfo);
        m_queue.ProcessMessage(message);
        m_isEventGenerated = true;
    }

}
