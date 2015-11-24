#include "stdafx.h"
#include "XmlNode.h"

namespace
{
	HRESULT LoadResource2Stream(HMODULE module, UINT resourceID, CComPtr<IStream>& stream)
	{
		HRSRC info = FindResource(module, MAKEINTRESOURCE(resourceID), TEXT("XML"));
		if (0 == info)
		{
			return E_FAIL;
		}
		HGLOBAL temp = LoadResource(module, info);
		DWORD size = SizeofResource(module, info);
		HGLOBAL descriptor = GlobalAlloc(GMEM_MOVEABLE, size);
		if (nullptr == descriptor)
		{
			return E_OUTOFMEMORY;
		}
		void* source = LockResource(temp);
		void* destination = GlobalLock(descriptor);
		CopyMemory(destination, source, size);
		GlobalUnlock(descriptor);		
		HRESULT result = CreateStreamOnHGlobal(descriptor, TRUE, &stream);
		if (FAILED(result))
		{
			GlobalFree(descriptor);			
		}
		return result;
	}	
}
CXmlNode::CXmlNode()
{
}
CXmlNode::CXmlNode(const string& name):Name(name)
{
}
HRESULT CXmlNode::LoadXML(const CComPtr<IStream>& stream)
{	
	CComPtr<IXmlReader> reader;
	HRESULT result = CreateXmlReader(__uuidof(IXmlReader), reinterpret_cast<void**>(&reader), NULL);
	return_if_failed(result);
	result = reader->SetProperty(XmlReaderProperty_DtdProcessing, DtdProcessing_Prohibit);
	return_if_failed(result);
	result = reader->SetInput(stream);
	return_if_failed(result);
	XmlNodeType type = XmlNodeType_None;
	result = reader->Read(&type);
	for (; S_OK == result; result = reader->Read(&type))
	{
		break_if_true(XmlNodeType_Element == type);
	}
	return_if_failed(result);
	const wchar_t* name = 0;
	result = reader->GetLocalName(&name, NULL);
	return_if_failed(result);
	Name = CW2A(name);
	result = LoadElement(reader, 0);
	return result;
}
HRESULT CXmlNode::LoadNode(const CComPtr<IXmlReader>& reader, UINT depth)
{
	UINT _depth;
	HRESULT result = reader->GetDepth(&_depth);
	return_if_failed(result);
	const wchar_t* name = 0;
	result = reader->GetLocalName(&name, NULL);
	return_if_failed(result);
	if (_depth == depth)
	{
		string st = CW2A(name);
		Nodes.push_back(st);
		CXmlNode& node = Nodes.back();
		result = node.LoadAttributes(reader);
		return_if_failed(result);
		return S_OK;
	}
	assert(_depth == depth + 1);
	CXmlNode& last = Nodes.back();
	string st = CW2A(name);
	last.Nodes.push_back(st);
	CXmlNode& node = last.Nodes.back();
	result = node.LoadAttributes(reader);
	return_if_failed(result);
	result = last.LoadNodes(reader, depth + 1);
	return result;
}
HRESULT CXmlNode::LoadText(const CComPtr<IXmlReader>& reader)
{
	const wchar_t* text = nullptr;	
	HRESULT result = reader->GetValue(&text, NULL);
	if (SUCCEEDED(result))
	{
		Text = CW2A(text);
	}	
	return result;
}
HRESULT CXmlNode::LoadNodes(const CComPtr<IXmlReader>& reader, UINT depth)
{
	XmlNodeType type = XmlNodeType_None;
	HRESULT result = reader->Read(&type);
	for (; S_OK == result; result = reader->Read(&type))
	{		
		if (XmlNodeType_Text == type)
		{
			assert(!Nodes.empty());
			CXmlNode& node = Nodes.back();
			result = node.LoadText(reader);
		}
		else if (XmlNodeType_Element  == type)
		{
			result = LoadNode(reader, depth);
		}
		else if (XmlNodeType_EndElement == type)
		{
			UINT _depth = 0;
			result = reader->GetDepth(&_depth);
			return_if_failed(result);
			// + 1 because it's end node
			if (_depth != (depth + 1))
			{
				return result;
			}
		}
		return_if_failed(result);
	}
	return result;
}
HRESULT CXmlNode::LoadElement(const CComPtr<IXmlReader>& reader, UINT depth)
{
	HRESULT result = LoadAttributes(reader);
	return_if_failed(result);
	result = LoadNodes(reader, depth + 1);
	return result;
}
HRESULT CXmlNode::LoadAttributes(const CComPtr<IXmlReader>& reader)
{
	HRESULT result = reader->MoveToFirstAttribute();
	for (; S_OK == result; result = reader->MoveToNextAttribute())
	{
		const wchar_t* name = 0;
		result = reader->GetLocalName(&name, NULL);
		return_if_failed(result);
		const wchar_t* text = 0;
		result = reader->GetValue(&text, NULL);
		return_if_true(S_OK != result, E_FAIL);

		string key = CW2A(name);
		string value = CW2A(text);
		Attributes[key] = value;
	}
	if (S_FALSE != result)
	{
		result = E_FAIL;		
	}
	return result;
}
HRESULT CXmlNode::LoadFromResource(HMODULE module, UINT resourceID)
{
	try
	{
		// first of all clear
		Name.clear();
		Nodes.clear();
		Attributes.clear();
		// load resource
		CComPtr<IStream> stream;
		HRESULT result = LoadResource2Stream(module, resourceID, stream);
		return_if_failed(result);
		// parse xml
		result = LoadXML(stream);
		return result;
	}
	catch (bad_alloc&)
	{
		return E_OUTOFMEMORY;
	}
	catch (exception&)
	{	
		return E_FAIL;
	}	
}

void CXmlNode::SerializeExistingAttributeByName(const string& name, ostream& stream)const
{
	auto it = Attributes.find(name);
	if (Attributes.end() == it)
	{
		throw out_of_range(name + " attribute was not found");
	}
	stream<<it->second;	
}
void CXmlNode::VerifyStreamState(const stringstream& stream)
{
	if (stream.bad())
	{
		throw runtime_error("converting error for " + stream.str());
	}
}
const string& CXmlNode::GetAttributeValueByName(const string& name) const
{
	auto it = Attributes.find(name);
	if (Attributes.end() == it)
	{
		throw out_of_range(name + " attribute was not found");
	}
	return it->second;
}
