#pragma once
#include "Logger.h"


class Translator
{
public:
	Translator();
	Translator(const string& text);
	Translator(uint16 componentId, const string& text);
public:
	void Translate(uint16& componentId, uint16& methodId) const;
	const string& Name() const;
	void MatchMethods(const Translator* translator);
	void MatchExceptions(const Translator& translator);
	void FlushMethod(CLogger& logger) const;
public:
	bool IsSupported() const;
	bool IsSupported(uint16 methodId) const;
private:
	void Construct(uint16 componentId, const string& text);
	void DoMatchMethods(const Translator& translator);
	void DoResetMethods();
private:
	string m_name;
	uint16 m_componentId;
	vector<string> m_names;
	vector<uint16> m_methods;
};