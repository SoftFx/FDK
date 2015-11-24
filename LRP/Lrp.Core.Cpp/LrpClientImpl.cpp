#include "stdafx.h"
#include "LrpClientImpl.h"

namespace
{
	Translator ParseSignature(const char* signature, vector<Translator>& translators)
	{
		stringstream stream;
		stream<<signature;

		string st;
		getline(stream, st, '$');
		if (!getline(stream, st, '$'))
		{
			throw runtime_error("Couldn't get component name");
		}
		Translator result(st);
		
		while(getline(stream, st, '$'))
		{
			uint16 id = static_cast<uint16>(translators.size());
			translators.push_back(Translator(id, st));
		}
		return result;
	}
}
CLrpClientImpl::CLrpClientImpl(const char* localSignature)
{
	m_exceptions = ParseSignature(localSignature, m_translators);
}
void CLrpClientImpl::Translate(uint16& componentId, uint16& methodId) const
{
	const Translator& translator = m_translators.at(componentId);
	translator.Translate(componentId, methodId);
}
void CLrpClientImpl::Initialize(const string& remoteSignature)
{
	vector<Translator> translators;
	const Translator exceptions = ParseSignature(remoteSignature.c_str(), translators);
	m_exceptions.MatchExceptions(exceptions);

	map<string, const Translator*> dict;
	for each (const auto& element in translators)
	{
		dict[element.Name()] = &element;
	}
	for each (const auto& element in m_translators)
	{
		const Translator* pTranslator = nullptr;
		auto it = dict.find(element.Name());
		if (dict.end() != it)
		{
			pTranslator = it->second;
		}
		Translator& translator = const_cast<Translator&>(element);
		translator.MatchMethods(pTranslator);
	}
}
bool CLrpClientImpl::IsSupported(const uint16 componentId, const uint16 methodId) const
{
	if (componentId >= m_translators.size())
	{
		return false;
	}
	Translator translator = m_translators[componentId];
	bool result = translator.IsSupported(methodId);
	return result;
}
bool CLrpClientImpl::IsSupported(const uint16 componentId) const
{
	if (componentId >= m_translators.size())
	{
		return false;
	}
	Translator translator = m_translators[componentId];
	bool result = translator.IsSupported();
	return result;
}

void CLrpClientImpl::FlushTranlators(CLogger& logger)
{
	logger.Output("Flushing of translators");
	for each (const auto& element in m_translators)
	{
		element.FlushMethod(logger);
	}
	logger.Output("Translators have been flushed");
}

