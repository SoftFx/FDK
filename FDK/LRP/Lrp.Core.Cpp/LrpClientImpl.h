#pragma once
#include "Translator.h"



class CLrpClientImpl
{
public:
	CLrpClientImpl(const char* localSignature);
public:
	void Translate(uint16& componentId, uint16& methodId) const;
public:
	bool IsSupported(const uint16 componentId, const uint16 methodId) const;
	bool IsSupported(const uint16 componentId) const;
protected:
	void Initialize(const string& remoteSignature);
	void FlushTranlators(CLogger& logger);
private:
	Translator m_exceptions;
	vector<Translator> m_translators;
};