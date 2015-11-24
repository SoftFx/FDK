#ifndef __Core_Fx_Iterator__
#define __Core_Fx_Iterator__

class CFxIterator : public CFxHandle
{
public:
	virtual HRESULT VEndOfStream() = 0;
	virtual HRESULT VNext(const uint32 timeoutInMilliseconds) = 0;
	virtual void* VItem() = 0;
};




#endif