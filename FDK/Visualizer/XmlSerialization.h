#pragma once
#pragma warning (push, 4)

#include "XmlNetContract.h"


HRESULT XmlCreateStream(const string& text, CComPtr<IStream>& stream);
HRESULT XmlCreateStream(CComPtr<IStream>& stream);
HRESULT XmlTextFromStream(CComPtr<IStream>& stream, string& text);

const char* XmlClassName(const type_info& info);
//HRESULT XmlReadNextNode(CComPtr<IXmlReader>& provider);



HRESULT XmlPrefixProcess(CComPtr<IXmlReader>& provider, const char* memberOrTypeName);
HRESULT XmlPrefixOfSequenceElementProcess(CComPtr<IXmlReader>& provider, const char* memberOrTypeName);
HRESULT XmlPrefixProcess(CComPtr<IXmlWriter>& provider, const char* memberOrTypeName);


HRESULT XmlProcess(CComPtr<IXmlReader>& provider,string& arg);
HRESULT XmlProcess(CComPtr<IXmlWriter>& provider, string& arg);
HRESULT XmlProcess(CComPtr<IXmlReader>& provider, bool& arg);
HRESULT XmlProcess(CComPtr<IXmlWriter>& provider, bool& arg);


HRESULT XmlPostfixProcess(CComPtr<IXmlReader>& provider, const char* memberOrTypeName);
HRESULT XmlPostfixProcess(CComPtr<IXmlWriter>& provider, const char* memberOrTypeName);



#pragma region writing of primitive types
template<typename T> inline HRESULT XmlPostfixProcess(CComPtr<IXmlWriter>& provider, T& /*arg*/, const char* memberOrTypeName)
{
	return XmlPostfixProcess(provider, memberOrTypeName);
}
template<typename T> HRESULT XmlPrimitiveProcess(CComPtr<IXmlWriter>& provider, T& arg)
{
	stringstream stream;
	stream.precision(DBL_DIG);
	stream << arg;
	string text = stream.str();
	HRESULT result = XmlProcess(provider, text);
	return result;
}
inline HRESULT XmlProcess(CComPtr<IXmlWriter>& provider, int8& arg)
{
	return XmlPrimitiveProcess(provider, arg);
}
inline HRESULT XmlProcess(CComPtr<IXmlWriter>& provider, uint8& arg)
{
	return XmlPrimitiveProcess(provider, arg);
}
inline HRESULT XmlProcess(CComPtr<IXmlWriter>& provider, int16& arg)
{
	return XmlPrimitiveProcess(provider, arg);
}
inline HRESULT XmlProcess(CComPtr<IXmlWriter>& provider, uint16& arg)
{
	return XmlPrimitiveProcess(provider, arg);
}
inline HRESULT XmlProcess(CComPtr<IXmlWriter>& provider, int32& arg)
{
	return XmlPrimitiveProcess(provider, arg);
}
inline HRESULT XmlProcess(CComPtr<IXmlWriter>& provider, uint32& arg)
{
	return XmlPrimitiveProcess(provider, arg);
}
inline HRESULT XmlProcess(CComPtr<IXmlWriter>& provider, int64& arg)
{
	return XmlPrimitiveProcess(provider, arg);
}
inline HRESULT XmlProcess(CComPtr<IXmlWriter>& provider, uint64& arg)
{
	return XmlPrimitiveProcess(provider, arg);
}
inline HRESULT XmlProcess(CComPtr<IXmlWriter>& provider, float& arg)
{
	return XmlPrimitiveProcess(provider, arg);
}
inline HRESULT XmlProcess(CComPtr<IXmlWriter>& provider, double& arg)
{
	return XmlPrimitiveProcess(provider, arg);
}

template<typename Provider, typename T, bool isEnum> class XmlProcessor;
/// <summary>
/// Implementation for classes
/// </summary>
template<typename Provider, typename T> class XmlProcessor<Provider, T, false>
{
public:
	static HRESULT Process(Provider& provider, T& arg)
	{
		return arg.Process(provider);
	}
};
/// <summary>
/// Serialization of enumeration
/// </summary>
template<typename T> class XmlProcessor<CComPtr<IXmlWriter>, T, true>
{
public:
	static HRESULT Process(CComPtr<IXmlWriter>& provider, T& arg)
	{
		uint64 value = arg; // should be for serialization
		const HRESULT result = XmlProcess(provider, value);
		return result;
	}
};
/// <summary>
/// Deserialization of enumeration
/// </summary>
template<typename T> class XmlProcessor<CComPtr<IXmlReader>, T, true>
{
public:
	static HRESULT Process(CComPtr<IXmlReader>& provider, T& arg)
	{
		uint64 value = 0;
		const HRESULT result = XmlProcess(provider, value);
		arg = static_cast<T>(value);
		return result;
	}
};
template<typename T> inline HRESULT XmlProcess(CComPtr<IXmlWriter>& provider, T& arg)
{
	return XmlProcessor<CComPtr<IXmlWriter>, T, std::tr1::is_enum<T>::value >::Process(provider, arg);
}
template<typename T> HRESULT XmlProcess(CComPtr<IXmlWriter>& provider, vector<T>& arg)
{
	HRESULT result = S_OK;

	const char* typeName = XmlClassName(typeid(T));
	const size_t count = arg.size();

	for(size_t index = 0; index < count; ++index)
	{
		T& element = arg[index];
		result = XmlPrefixProcess(provider, typeName);
		RETURN_IF_FAILED(result);

		result = XmlProcess(provider, element);
		RETURN_IF_FAILED(result);

		result = XmlPostfixProcess(provider, typeName);
		RETURN_IF_FAILED(result);
	}
	return result;
}

template<typename Key, typename Value> HRESULT XmlProcess(CComPtr<IXmlWriter>& provider, map<Key, Value>& arg)
{

	HRESULT result = S_OK;
	const char* typeName = "Item";
	const char* keyTypeName = XmlClassName(typeid(Key));
	const char* valueTypeName = XmlClassName(typeid(Value));
	map<Key, Value>::iterator it = arg.begin();
	map<Key, Value>::iterator end = arg.end();
	for (; it != end; ++it)
	{
		// key writing
		result = XmlPrefixProcess(provider, typeName);
		BREAK_IF_TRUE(S_FALSE == result);
		RETURN_IF_FAILED(result);

		result = XmlPrefixProcess(provider, "Key");
		RETURN_IF_FAILED(result);

		result = XmlPrefixProcess(provider, keyTypeName);
		RETURN_IF_FAILED(result);

		result = XmlProcess(provider, const_cast<Key&>(it->first));
		RETURN_IF_FAILED(result);

		result = XmlPostfixProcess(provider, keyTypeName);
		RETURN_IF_FAILED(result);

		result = XmlPostfixProcess(provider, "Key");
		RETURN_IF_FAILED(result);


		// value writing

		result = XmlPrefixProcess(provider, "Value");
		RETURN_IF_FAILED(result);

		result = XmlPrefixProcess(provider, valueTypeName);
		RETURN_IF_FAILED(result);

		result = XmlProcess(provider, it->second);
		RETURN_IF_FAILED(result);

		result = XmlPostfixProcess(provider, valueTypeName);
		RETURN_IF_FAILED(result);

		result = XmlPostfixProcess(provider, "Value");
		RETURN_IF_FAILED(result);

		result = XmlPostfixProcess(provider, "Item");
		RETURN_IF_FAILED(result);
	}
	return result;
}

#pragma endregion


#pragma region reading
template<typename T> inline HRESULT XmlPostfixProcess(CComPtr<IXmlReader>& provider, T& /*arg*/, const char* memberOrTypeName)
{
	return XmlPostfixProcess(provider, memberOrTypeName);
}
template<typename T> inline HRESULT XmlPostfixProcess(CComPtr<IXmlReader>& /*provider*/, vector<T>& /*arg*/, const char* /*memberOrTypeName*/)
{
	return S_OK;
}
template<typename Key, typename Value> inline HRESULT XmlPostfixProcess(CComPtr<IXmlReader>& /*provider*/, map<Key, Value>& /*arg*/, const char* /*memberOrTypeName*/)
{
	return S_OK;
}
template<typename T> HRESULT XmlPrimitiveProcess(CComPtr<IXmlReader>& provider, T& arg)
{
	string text;
	HRESULT result = XmlProcess(provider, text);
	RETURN_IF_FAILED(result);
	stringstream stream;
	stream<<text;
	stream>>arg;
	if (stream.bad() || !stream.eof())
	{
		return E_FAIL;
	}
	return result;
}


inline HRESULT XmlProcess(CComPtr<IXmlReader>& provider, int8& arg)
{
	return XmlPrimitiveProcess(provider, arg);
}
inline HRESULT XmlProcess(CComPtr<IXmlReader>& provider, uint8& arg)
{
	return XmlPrimitiveProcess(provider, arg);
}
inline HRESULT XmlProcess(CComPtr<IXmlReader>& provider, int16& arg)
{
	return XmlPrimitiveProcess(provider, arg);
}
inline HRESULT XmlProcess(CComPtr<IXmlReader>& provider, uint16& arg)
{
	return XmlPrimitiveProcess(provider, arg);
}
inline HRESULT XmlProcess(CComPtr<IXmlReader>& provider, int32& arg)
{
	return XmlPrimitiveProcess(provider, arg);
}
inline HRESULT XmlProcess(CComPtr<IXmlReader>& provider, uint32& arg)
{
	return XmlPrimitiveProcess(provider, arg);
}
inline HRESULT XmlProcess(CComPtr<IXmlReader>& provider, int64& arg)
{
	return XmlPrimitiveProcess(provider, arg);
}
inline HRESULT XmlProcess(CComPtr<IXmlReader>& provider, uint64& arg)
{
	return XmlPrimitiveProcess(provider, arg);
}
inline HRESULT XmlProcess(CComPtr<IXmlReader>& provider, float& arg)
{
	return XmlPrimitiveProcess(provider, arg);
}
inline HRESULT XmlProcess(CComPtr<IXmlReader>& provider, double& arg)
{
	return XmlPrimitiveProcess(provider, arg);
}
template<typename T> inline HRESULT XmlProcess(CComPtr<IXmlReader>& provider, T& arg)
{
	return XmlProcessor<CComPtr<IXmlReader>, T, std::tr1::is_enum<T>::value>::Process(provider, arg);
}
template<typename T> HRESULT XmlProcess(CComPtr<IXmlReader>& provider, vector<T>& arg)
{
	HRESULT result = S_OK;
	const char* typeName = XmlClassName(typeid(T));

	for (; ;)
	{
		// prefix reading
		result = XmlPrefixOfSequenceElementProcess(provider, typeName);
		BREAK_IF_TRUE(S_FALSE == result);
		RETURN_IF_FAILED(result);

		T element;
		result = XmlProcess(provider, element);
		RETURN_IF_FAILED(result);
		arg.push_back(element);

		if (XML_PARSE_CONTINUE != result)
		{
			result = XmlPostfixProcess(provider, typeName);
			RETURN_IF_FAILED(result);
		}
	}
	return result;
}
template<typename Key, typename Value> HRESULT XmlProcess(CComPtr<IXmlReader>& provider, map<Key, Value>& arg)
{
	HRESULT result = S_OK;
	const char* typeName = "Item";
	const char* keyTypeName = XmlClassName(typeid(Key));
	const char* valueTypeName = XmlClassName(typeid(Value));

	for (; ;)
	{
		// prefix reading
		result = XmlPrefixOfSequenceElementProcess(provider, typeName);
		BREAK_IF_TRUE(S_FALSE == result);
		RETURN_IF_FAILED(result);

		// key reading

		result = XmlPrefixProcess(provider, "Key");
		RETURN_IF_FAILED(result);

		result = XmlPrefixProcess(provider, keyTypeName);
		RETURN_IF_FAILED(result);

		typename Key key = Key();
		result = XmlProcess(provider, key);
		RETURN_IF_FAILED(result);

		result = XmlPostfixProcess(provider, keyTypeName);
		RETURN_IF_FAILED(result);

		result = XmlPostfixProcess(provider, "Key");
		RETURN_IF_FAILED(result);

		if (arg.count(key) > 0) // should be unique
		{
			return E_FAIL;
		}

		// reading value

		typename Value value = Value();

		result = XmlPrefixProcess(provider, "Value");
		RETURN_IF_FAILED(result);

		result = XmlPrefixProcess(provider, valueTypeName);
		RETURN_IF_FAILED(result);

		result = XmlProcess(provider, value);
		RETURN_IF_FAILED(result);
		
		arg[key] = value;

		result = XmlPostfixProcess(provider, valueTypeName);
		RETURN_IF_FAILED(result);

		result = XmlPostfixProcess(provider, "Value");
		RETURN_IF_FAILED(result);

		result = XmlPostfixProcess(provider, "Item");
		RETURN_IF_FAILED(result);
	}
	return result;
}

#pragma endregion








/// <summary>
/// Internal serialization mechanism uses non const references,
/// because in this case XML serialization implementation is easier.
/// But serialization don't change an input object.
/// </summary>
template<typename T> HRESULT XmlSerialize(const T& arg, string& text)
{
	T& nonConstArg = const_cast<T&>(arg);

	CComPtr<IStream> stream;
	CComPtr<IXmlWriter> provider;

	// create stream
	HRESULT result = XmlCreateStream(stream);
	RETURN_IF_FAILED(result);

	// create xml writer
	result = CreateXmlWriter(__uuidof(IXmlWriter), reinterpret_cast<void**>(&provider), NULL);
	RETURN_IF_FAILED(result);

	result = provider->SetProperty(XmlWriterProperty_Indent, TRUE);
	RETURN_IF_FAILED(result);

	result = provider->SetOutput(stream);
	RETURN_IF_FAILED(result);

	result = provider->WriteStartDocument(XmlStandalone_Omit);
	RETURN_IF_FAILED(result);


	const char* typeName = XmlClassName(typeid(arg));
	result = provider->WriteStartElement(NULL, CA2W(typeName), NULL);
	RETURN_IF_FAILED(result);

	result = provider->WriteAttributeString(L"xmlns", L"xsd", NULL, L"http://www.w3.org/2001/XMLSchema");
	RETURN_IF_FAILED(result);

	result = provider->WriteAttributeString(L"xmlns", L"xsi", NULL, L"http://www.w3.org/2001/XMLSchema-instance");
	RETURN_IF_FAILED(result);

	result = XmlProcess(provider, nonConstArg);
	RETURN_IF_FAILED(result);

	result = provider->WriteEndElement();
	RETURN_IF_FAILED(result);

	result = provider->WriteEndDocument();
	RETURN_IF_FAILED(result);

	result = provider->Flush();
	RETURN_IF_FAILED(result);

	result = XmlTextFromStream(stream, text);
	return result;
}





template<typename T> HRESULT XmlDeserialize(const string& text, T& arg)
{
	CComPtr<IStream> stream;
	CComPtr<IXmlReader> provider;

	// create stream
	HRESULT result = XmlCreateStream(text, stream);
	RETURN_IF_FAILED(result);
	// create xml provider
	result = CreateXmlReader(__uuidof(IXmlReader), reinterpret_cast<void**>(&provider), NULL);
	RETURN_IF_FAILED(result);

	result = provider->SetProperty(XmlReaderProperty_DtdProcessing, DtdProcessing_Prohibit);
	RETURN_IF_FAILED(result);

	result = provider->SetInput(stream);
	RETURN_IF_FAILED(result);

	const char* typeName = XmlClassName(typeid(arg));

	// read the main node
	result = XmlPrefixProcess(provider, typeName);
	RETURN_IF_FAILED(result);
	
	result = XmlProcess(provider, arg);
	RETURN_IF_FAILED(result);

	result = XmlPostfixProcess(provider, typeName);
	return result;
}

#pragma warning (pop)