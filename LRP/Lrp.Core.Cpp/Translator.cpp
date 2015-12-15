#include "stdafx.h"
#include "Translator.h"
namespace
{
	const string cWrongExceptionsSpecification = "Remote exceptions specification can not be matched to local exceptions specification.";
}

Translator::Translator() : m_componentId()
{
}
Translator::Translator(const string& text)
{
	Construct(numeric_limits<uint16>::max(), text);
}
Translator::Translator(uint16 componentId, const string& text)
{
	Construct(componentId, text);
}
const string& Translator::Name() const
{
	return m_name;
}
void Translator::Translate(uint16& componentId, uint16& methodId) const
{
	if (numeric_limits<uint16>::max() == m_componentId)
	{
		throw runtime_error("Incorrect component ID");
	}
	uint16 newMethodId = m_methods.at(methodId);
	if (numeric_limits<uint16>::max() == newMethodId)
	{
		throw runtime_error("Incorrect method ID");
	}
	componentId = m_componentId;
	methodId = newMethodId;
}
void Translator::Construct(uint16 componentId, const string& text)
{
	m_componentId = componentId;
	stringstream stream;
	stream<<text;

	if (!getline(stream, m_name, ';'))
	{
		throw runtime_error("Couldn't get component name");
	}

	string st;
	while(getline(stream, st, ';'))
	{
		m_names.push_back(st);
	}
}
void Translator::MatchMethods(const Translator* translator)
{
	if (nullptr != translator)
	{
		DoMatchMethods(*translator);
	}
	else
	{
		DoResetMethods();
	}
}
void Translator::MatchExceptions(const Translator& translator)
{
	if (translator.m_names.size() > m_names.size())
	{
		throw runtime_error(cWrongExceptionsSpecification);
	}
	size_t count = translator.m_names.size();
	for (size_t index = 0; index < count; ++index)
	{
		if (m_names[index] != translator.m_names[index])
		{
			throw runtime_error(cWrongExceptionsSpecification);
		}
	}
}
void Translator::DoMatchMethods(const Translator& translator)
{
	map<string, uint16> dict;
	for each (const auto& element in translator.m_names)
	{
		dict[element] = static_cast<uint16>(dict.size());
	}
	size_t count = this->m_names.size();
	this->m_methods.clear();

	for (size_t index = 0; index < count; ++index)
	{
		uint16 methodId = numeric_limits<uint16>::max();
		auto it = dict.find(m_names[index]);
		if (dict.end() != it)
		{
			methodId = it->second;
		}
		this->m_methods.push_back(methodId);
	}
	m_componentId = translator.m_componentId;
}
void Translator::DoResetMethods()
{
	this->m_componentId = numeric_limits<uint16>::max();
	this->m_methods.clear();
}

bool Translator::IsSupported(uint16 methodId) const
{
	if (numeric_limits<uint16>::max() == m_componentId)
	{
		return false;
	}
	if (methodId >= m_methods.size())
	{
		return false;
	}
	bool result = (numeric_limits<uint16>::max() != m_methods[methodId]);
	return result;
}
bool Translator::IsSupported() const
{
	return (numeric_limits<uint16>::max() != m_componentId);
}

void Translator::FlushMethod(CLogger& logger) const
{
	if (IsSupported())
	{
		logger.Output("{0} component is supported", m_name);
		size_t count = m_names.size();
		for (size_t index = 0; index < count; ++index)
		{
			string name = m_names[index];
			size_t position = name.find_first_of('@');
			name = name.substr(0, position);;
			if (IsSupported(static_cast<uint16>(index)))
			{
				logger.Output("\t{0} method is supported", name);
			}
			else
			{
				logger.Output("\t{0} method is not supported", name);
			}
		}
	}
	else
	{
		logger.Output("{0} component is not supported", m_name);
	}
}
