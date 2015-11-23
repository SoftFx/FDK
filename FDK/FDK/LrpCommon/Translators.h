#pragma once
#include "Translator.h"



class CTranslators
{
public:
	CTranslators(const char* localSignature);
public:
	void Translate(uint16& componentId, uint16& methodId) const;
public:
	bool IsSupported(const uint16 componentId, const uint16 methodId) const;
	bool IsSupported(const uint16 componentId) const;
public:
	void Initialize(const string& remoteSignature);
private:
	CTranslator m_exceptions;
	vector<CTranslator> m_translators;
};