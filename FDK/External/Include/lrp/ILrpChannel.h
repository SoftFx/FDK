#pragma once


class ILrpChannel
{
public:
	virtual void Initialize(MemoryBuffer& buffer) = 0;
	virtual HRESULT Invoke(unsigned __int16 componentId, unsigned __int16 methodId, MemoryBuffer& buffer) = 0;
	virtual bool IsSupported(unsigned __int16 componentId) const = 0;
	virtual bool IsSupported(unsigned __int16 componentId, unsigned __int16 methodId) const = 0;
};