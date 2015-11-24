#include "stdafx.h"
#include "Translators.h"

namespace
{
	CTranslator ParseSignature(const char* signature, vector<CTranslator>& translators)
	{
		stringstream stream;
		stream<<signature;

		string st;
		getline(stream, st, '$');
		if (!getline(stream, st, '$'))
		{
			throw runtime_error("Couldn't get component name");
		}
		CTranslator result(st);
		
		while(getline(stream, st, '$'))
		{
			uint16 id = static_cast<uint16>(translators.size());
			translators.push_back(CTranslator(id, st));
		}
		return result;
	}
}
CTranslators::CTranslators(const char* localSignature)
{
	m_exceptions = ParseSignature(localSignature, m_translators);
}
void CTranslators::Translate(uint16& componentId, uint16& methodId) const
{
	const CTranslator& translator = m_translators.at(componentId);
	translator.Translate(componentId, methodId);
}
void CTranslators::Initialize(const string& remoteSignature)
{
	vector<CTranslator> translators;
	const CTranslator exceptions = ParseSignature(remoteSignature.c_str(), translators);
	m_exceptions.MatchExceptions(exceptions);

	map<string, const CTranslator*> dict;
	for each (const auto& element in translators)
	{
		dict[element.Name()] = &element;
	}
	for each (const auto& element in m_translators)
	{
		const CTranslator* pTranslator = nullptr;
		auto it = dict.find(element.Name());
		if (dict.end() != it)
		{
			pTranslator = it->second;
		}
		CTranslator& translator = const_cast<CTranslator&>(element);
		translator.MatchMethods(pTranslator);
	}
}
bool CTranslators::IsSupported(const uint16 componentId, const uint16 methodId) const
{
	if (componentId >= m_translators.size())
	{
		return false;
	}
	CTranslator translator = m_translators[componentId];
	bool result = translator.IsSupported(methodId);
	return result;
}
bool CTranslators::IsSupported(const uint16 componentId) const
{
	if (componentId >= m_translators.size())
	{
		return false;
	}
	CTranslator translator = m_translators[componentId];
	bool result = translator.IsSupported();
	return result;
}
