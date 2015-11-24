#include "stdafx.h"
#include "XmlSerialization.h"


namespace
{
	const string cXmlUtf16Declaration = "<?xml version=\"1.0\" encoding=\"utf-16\"?>";
	const string cXmlUtf8Declaration = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
	string cTrue = "true";
	string cFalse = "false";

	string FixXmlText(const string& text)
	{
		const regex pattern("<([A-Za-z]+)\\s*/>");
		const string format("<$1></$1>");

		string result = regex_replace(text, pattern, format);
		// fix utf-16
		size_t position = result.find(cXmlUtf16Declaration);
		if (0 == position)
		{
			result.replace(result.begin(), result.begin() + cXmlUtf16Declaration.length(), cXmlUtf8Declaration.c_str());
		}
		return result;
	}
}
HRESULT XmlCreateStream(const string& text, CComPtr<IStream>& stream)
{
	string st = FixXmlText(text);

	const size_t size = st.length() + 1;
	HGLOBAL descriptor = GlobalAlloc(GMEM_MOVEABLE, size);
	if (nullptr == descriptor)
	{
		return E_OUTOFMEMORY;
	}
	const void* source = st.c_str();
	void* destination = GlobalLock(descriptor);
	CopyMemory(destination, source, size);
	GlobalUnlock(descriptor);
	const HRESULT result = CreateStreamOnHGlobal(descriptor, TRUE, &stream);
	if (FAILED(result))
	{
		GlobalFree(descriptor);
	}
	return result;
}
HRESULT XmlCreateStream(CComPtr<IStream>& stream)
{
	const HRESULT result = CreateStreamOnHGlobal(NULL, TRUE, &stream);
	return result;
}
HRESULT XmlTextFromStream(CComPtr<IStream>& stream, string& text)
{
	HGLOBAL descriptor = NULL;
	HRESULT result = GetHGlobalFromStream(stream, &descriptor);
	RETURN_IF_FAILED(result);

	const size_t size = GlobalSize(descriptor);
	const char* source = reinterpret_cast<const char*>(GlobalLock(descriptor));
	try
	{
		text.insert(text.end(), source, source + size);
	}
	catch (const bad_alloc&)
	{
		result = E_OUTOFMEMORY;
	}
	GlobalUnlock(descriptor);
	return result;
}

namespace
{
	const char* XmlTemplateName(const type_info& info)
	{
		const char* name = info.name();
		if (0 == strcmp(name, typeid(string).name()))
		{
			return "string";
		}
		throw runtime_error(string("XmlTemplateName(): Unknown type - ") + name);
	}
}




const char* XmlClassName(const type_info& info)
{
	const char* name = info.name();

	const char* result = name;
	for (char ch = *name; 0 != ch; ++name, ch = *name)
	{
		if ('<' == ch)
		{
			return XmlTemplateName(info);
		}
		if ((' ' == ch) || (':' == ch))
		{
			result = name + 1;
		}
	}
	return result;
}
HRESULT XmlReadNextNode(CComPtr<IXmlReader>& provider)
{
	XmlNodeType nodeType = XmlNodeType_None;
	HRESULT result = provider->Read(&nodeType);
	for (; S_OK == result; result = provider->Read(&nodeType))
	{
		BREAK_IF_TRUE(XmlNodeType_Element == nodeType);
	}
	return result;
}
HRESULT XmlReadNodeName(CComPtr<IXmlReader>& provider, string& name)
{
	// check the current node type
	XmlNodeType nodeType = XmlNodeType_None;
	HRESULT result = provider->GetNodeType(&nodeType);
	RETURN_IF_FAILED(result);
	if (XmlNodeType_EndElement == nodeType)
	{
		result = XmlReadNextNode(provider);
		RETURN_IF_FAILED(result);
		result = provider->GetNodeType(&nodeType);
		RETURN_IF_FAILED(result);
	}
	if (XmlNodeType_Element != nodeType)
	{
		return E_FAIL;
	}
	// read the node name
	const wchar_t* st = NULL;
	result = provider->GetLocalName(&st, NULL);
	RETURN_IF_FAILED(result);
	name = CW2A(st);
	return result;
}

HRESULT XmlProcess(CComPtr<IXmlReader>& provider, string& arg)
{
	XmlNodeType nodeType = XmlNodeType_None;
	HRESULT result = provider->GetNodeType(&nodeType);
	RETURN_IF_FAILED(result);
	arg.clear();
	if (provider->IsEmptyElement())
		return XML_PARSE_CONTINUE;
	if (XmlNodeType_Text != nodeType)
	{
		for (result = provider->Read(&nodeType); S_OK == result; result = provider->Read(&nodeType))
		{
			if (XmlNodeType_EndElement == nodeType)
			{
				return XML_PARSE_CONTINUE;
			}
			BREAK_IF_TRUE(XmlNodeType_Text == nodeType);
		}
		RETURN_IF_FAILED(result);
	}
	const wchar_t* text = NULL;
	result = provider->GetValue(&text, NULL);
	RETURN_IF_FAILED(result);
	arg = CW2A(text);
	return result;
}
HRESULT XmlProcess(CComPtr<IXmlReader>& provider, bool& arg)
{
	string st;
	HRESULT result = XmlProcess(provider, st);
	RETURN_IF_FAILED(result);
	if ("true" == st)
	{
		arg = true;
	}
	else if ("false" == st)
	{
		arg = false;
	}
	else
	{
		result = E_FAIL;
	}
	return result;
}
HRESULT XmlProcess(CComPtr<IXmlWriter>& provider, bool& arg)
{
	if (arg)
	{
		return XmlProcess(provider, cTrue);
	}
	else
	{
		return XmlProcess(provider, cFalse);
	}
}

HRESULT XmlPrefixOfSequenceElementProcess(CComPtr<IXmlReader>& provider, const char* memberOrTypeName)
{
	XmlNodeType nodeType = XmlNodeType_None;
	HRESULT result = provider->Read(&nodeType);
	for (; S_OK == result; result = provider->Read(&nodeType))
	{
		BREAK_IF_TRUE(XmlNodeType_Element == nodeType);
		if (XmlNodeType_EndElement == nodeType)
		{
			return S_FALSE;
		}
	}
	RETURN_IF_FAILED(result);
	// read the node name
	const wchar_t* st = NULL;
	result = provider->GetLocalName(&st, NULL);
	RETURN_IF_FAILED(result);
	string name = CW2A(st);
	if (name != memberOrTypeName)
	{
		return E_FAIL;
	}
	return result;
}

HRESULT XmlPrefixProcess(CComPtr<IXmlReader>& provider, const char* memberOrTypeName)
{
	XmlNodeType nodeType = XmlNodeType_None;
	HRESULT result = provider->Read(&nodeType);
	for (; S_OK == result; result = provider->Read(&nodeType))
	{
		BREAK_IF_TRUE(XmlNodeType_Element == nodeType);
	}
	if (S_FALSE == result)
	{
		return E_FAIL;
	}
	RETURN_IF_FAILED(result);
	// read the node name
	const wchar_t* st = NULL;
	result = provider->GetLocalName(&st, NULL);
	RETURN_IF_FAILED(result);
	string name = CW2A(st);
	if (name != memberOrTypeName)
	{
		return E_FAIL;
	}
	return result;
}
HRESULT XmlPrefixProcess(CComPtr<IXmlWriter>& provider, const char* memberOrTypeName)
{
	HRESULT result = provider->WriteStartElement(NULL, CA2W(memberOrTypeName), NULL);
	return result;
}
HRESULT XmlProcess(CComPtr<IXmlWriter>& provider, string& arg)
{
	HRESULT result = provider->WriteString(CA2W(arg.c_str()));
	return result;
}
HRESULT XmlPostfixProcess(CComPtr<IXmlReader>& provider, const char* memberOrTypeName)
{
	XmlNodeType nodeType = XmlNodeType_None;
	HRESULT result = provider->Read(&nodeType);
	for (; S_OK == result; result = provider->Read(&nodeType))
	{
		BREAK_IF_TRUE(XmlNodeType_EndElement == nodeType);
	}
	return result;
}
HRESULT XmlPostfixProcess(CComPtr<IXmlWriter>& provider, const char* memberOrTypeName)
{
	HRESULT result = provider->WriteEndElement();
	return result;
}

