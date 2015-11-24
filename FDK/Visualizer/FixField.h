#pragma once
#include "XmlNode.h"


class CFixFieldFormatter
{
public:
	CFixFieldFormatter();
	CFixFieldFormatter(const CXmlNode& node);
public:
	void Format(const string& st, ostream& stream);
private:
	string m_name;
	map<string, string> m_enums;
};