#include "stdafx.h"
#include "DictionariesManager.h"
#include "FixMessage.h"

namespace
{
	void* gModule = nullptr;
	CDictionariesManager gDictionariesManager;
}
CDictionariesManager::CDictionariesManager()
{
}
CDictionariesManager::~CDictionariesManager()
{
	auto it = m_key2dictionary.begin();
	auto end = m_key2dictionary.end();
	for (; it != end; ++it)
	{
		delete it->second;
	}
	m_key2dictionary.clear();
}

void CDictionariesManager::SetModuleHandle(void* module)
{
	gModule = module;
}
FIX::DataDictionary* CDictionariesManager::DictionaryFromResourceID(const std::string& key)
{
	FIX::Locker lock(m_synchronizer);
	auto it = m_key2dictionary.find(key);
	if (m_key2dictionary.end() != it)
	{
		return it->second;
	}
	return nullptr;
}

void CDictionariesManager::LoadDictionary(const std::string& key)
{
	std::string st = FxStringFromResource(gModule, key, "XML");
	std::stringstream stream;
	stream<<st;
	FIX::DataDictionary* result = new FIX::DataDictionary(stream);
	m_key2dictionary[key] = result;

	CFixMessage::LoadDictionary(st);
}

FIX::DataDictionary* CreateDictionaryFromResourceID(const std::string& id)
{
	FIX::DataDictionary* result = gDictionariesManager.DictionaryFromResourceID(id);
	return result;
}

void LoadDictionaryFromResourceID(const std::string& id)
{
	gDictionariesManager.LoadDictionary(id);
}

