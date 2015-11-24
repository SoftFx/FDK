#ifndef __Native_IWaiter__
#define __Native_IWaiter__

class IWaiter
{
public:
	virtual bool VResponse(const CFxEventInfo& info) = 0;
	virtual bool VResponse(const CFxEventInfo& info, void* pData) = 0;
	virtual void Disconnect() = 0;
	virtual ~IWaiter()
    {
    }
};

#endif
