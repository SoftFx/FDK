#pragma once
#include "DataDictionary.h"
#include "Mutex.h"


class CDictionariesManager
{
public:
	CDictionariesManager();
	~CDictionariesManager();
public:
	FIX::DataDictionary* DictionaryFromResourceID(const std::string& key);
	void LoadDictionary(const std::string& key);
	static void SetModuleHandle(void* module);
private:
	FIX::Mutex m_synchronizer;
	std::map<std::string, FIX::DataDictionary*> m_key2dictionary;
};


void LoadDictionaryFromResourceID(const std::string& id);
FIX::DataDictionary* CreateDictionaryFromResourceID(const std::string& id);