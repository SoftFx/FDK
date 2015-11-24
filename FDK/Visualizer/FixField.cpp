#include "stdafx.h"
#include "resource.h"
#include "FixField.h"
#include "XmlNode.h"
#include "Visualizer.h"
#include "Config.h"




namespace
{
	const string cNumber = "number";
	const string cName = "name";
	const string cType = "type";
	const string cEnum = "enum";
	const string cDescription = "description";
	const DWORD cMaximumStringLength = 64 * 1024;
}

namespace
{
	map<int, CFixFieldFormatter> gFormatters;	
}

/// <summary>
/// map requires this constructor
/// </summary>
CFixFieldFormatter::CFixFieldFormatter()
{
}
CFixFieldFormatter::CFixFieldFormatter(const CXmlNode& node)
{
	m_name = node.GetAttributeValueByName(cName);
	for each(const CXmlNode& element in node.Nodes)
	{
		const string& key = element.GetAttributeValueByName(cEnum);
		const string& value = element.GetAttributeValueByName(cDescription);
		m_enums[key] = value;
	}
}
void CFixFieldFormatter::Format(const string& st, ostream& stream)
{
	stream<<m_name<<" = ";
	auto it = m_enums.find(st);
	if (it == m_enums.end())
	{
		stream<<st;
	}
	else
	{
		stream<<it->second;
	}
}
namespace
{

}




VISUALIZER_API HRESULT WINAPI FixField_Format(DWORD address, DEBUGHELPER* helper, int /*base*/, BOOL /*bUniStrings*/, char* buffer, size_t max, DWORD /*reserved*/)
{
	prolog;

	int field = 0;
	int offset = gConfig.IsDestructorVirtual? sizeof(int) : 0;
	HRESULT result = debugger.Read(address + offset, field);
	return_if_failed(result);
	try
	{
		string st;
		result = ReadStdString(debugger, address + sizeof(int) + offset, cMaximumStringLength, st);
		return_if_failed(result);
		
		auto it = gFormatters.find(field);
		if (gFormatters.end() == it)
		{
			stream<<"["<<field<<"] = "<<st;
		}
		else
		{
			it->second.Format(st, stream);
		}
	}
	catch (exception&)
	{	
		result = E_FAIL;
	}

	return result;
}



bool LoadFixFields(HANDLE module)
{
	try
	{
		CXmlNode fields;
		HRESULT hr = fields.LoadFromResource(reinterpret_cast<HMODULE>(module), IDR_XML_FIX_FIELDS);
		if (FAILED(hr))
		{
			return false;
		}
		for each(const CXmlNode& element in fields.Nodes)
		{
			const int key = element.GetAttributeValueByName<int>(cNumber);
			gFormatters[key] = CFixFieldFormatter(element);
		}
		return true;
	}
	catch (exception&)
	{	
		return false;
	}	
}