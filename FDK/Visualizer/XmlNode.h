#pragma once

class CXmlNode
{
public:
	string Name;
	string Text;
	vector<CXmlNode> Nodes;
	map<string, string> Attributes;
public:
	template<typename T> T GetAttributeValueByName(const string& name)const
	{
		stringstream stream;
		SerializeExistingAttributeByName(name, stream);
		T result = T();
		stream>>result;
		VerifyStreamState(stream);
		return result;
	}
	const string& GetAttributeValueByName(const string& name)const;
public:
	CXmlNode();
	CXmlNode(const string& name);
	HRESULT LoadFromResource(HMODULE module, UINT resourceID);
private:
	void SerializeExistingAttributeByName(const string& name, ostream& stream)const;
	static void VerifyStreamState(const stringstream& stream);
private:
	HRESULT LoadXML			(const CComPtr<IStream>& stream);
	HRESULT LoadText		(const CComPtr<IXmlReader>& reader);
	HRESULT LoadNode		(const CComPtr<IXmlReader>& reader, UINT depth);
	HRESULT LoadNodes		(const CComPtr<IXmlReader>& reader, UINT depth);
	HRESULT LoadElement		(const CComPtr<IXmlReader>& reader, UINT depth);
	HRESULT LoadAttributes	(const CComPtr<IXmlReader>& reader);
};
