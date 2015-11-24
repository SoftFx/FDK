#pragma once

class CTranslator
{
public:
	CTranslator();
	CTranslator(const string& text);
	CTranslator(uint16 componentId, const string& text);
public:
	void Translate(uint16& componentId, uint16& methodId) const;
	const string& Name() const;
	void MatchMethods(const CTranslator* translator);
	void MatchExceptions(const CTranslator& translator);
public:
	bool IsSupported() const;
	bool IsSupported(uint16 methodId) const;
private:
	void Construct(uint16 componentId, const string& text);
	void DoMatchMethods(const CTranslator& translator);
	void DoResetMethods();
private:
	string m_name;
	uint16 m_componentId;
	vector<string> m_names;
	vector<uint16> m_methods;
};