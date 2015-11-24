#ifndef __Native_Data_Cache__
#define __Native_Data_Cache__

#include "FxQueue.h"

class CDataCache
{
public:
	CDataCache(CFxQueue& queue);

private:
	CDataCache(const CDataCache&);
	CDataCache& operator = (const CDataCache&);

public:
	CFxSessionInfo GetSessionInfo() const;

public:
	void UpdateSessionInfo(const CFxSessionInfo& sessionInfo);

protected:
	void Clear();
	void Update(bool isInitialized);

protected:
	mutable CSharedExclusiveLock m_synchronizer;

private:
	CFxQueue& m_queue;
	bool m_isEventGenerated;
	bool m_isSessionInfoInitialized;
	CFxSessionInfo m_sessionInfo;
};
#endif
