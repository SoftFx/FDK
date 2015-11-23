#pragma once


class CBinaryReader
{
private:
	CBinaryReader(const CBinaryReader&);
	CBinaryReader& operator = (const CBinaryReader&);
public:
	CBinaryReader(const vector<char>& buffer);
public:
	template<typename T> CBinaryReader& operator >> (vector<T>& argument)
	{
		argument.clear();
		uint32 count = 0;
		(*this)>>count;
		argument.insert(argument.end(), count, T());
		for (size_t index = 0; index < count; index++)
		{
			T& element = argument[index];
			(*this)>>element;
		}
		return *this;
	}
	template<typename T> CBinaryReader& operator >> (set<T>& argument)
	{
		argument.clear();
		uint32 count = 0;
		(*this)>>count;
		for (uint32 index = 0; index < count; index++)
		{
			T element;
			(*this)>>element;
			// this insert works faster than argument.insert(element),
			// because all set elements have been send by network in correct order
			argument.insert(argument.end(), element);
		}
		return *this;
	}
	template<typename T> CBinaryReader& operator >> (deque<T>& argument)
	{
		argument.clear();
		size_t count = 0;
		(*this)>>count;
		for (size_t index = 0; index < count; index++)
		{
			T element;
			(*this)>>element;
			// this insert works faster than argument.insert(element),
			// because all set elements have been send by network in correct order
			argument.insert(argument.end(), element);
		}
		return *this;
	}
	template<typename Key, typename Value> CBinaryReader& operator >> (map<Key, Value>& argument)
	{
		argument.clear();
		unsigned __int32 count = 0;
		(*this)>>count;
		for (unsigned __int32 index = 0; index < count; index++)
		{
			Key key;
			Value value;
			(*this)>>key;
			(*this)>>value;
			// this insert works faster than argument.insert(element),
			// because all map elements have been send by network in correct order
			argument.insert(argument.end(), pair<Key, Value>(key, value));
		}
		return *this;
	}
	template<typename T1, typename T2> CBinaryReader& operator >> (pair<T1, T2>& argument)
	{
		(*this)>>argument.first;
		(*this)>>argument.second;
		return *this;
	}

	CBinaryReader& operator >> (string& argument);
	void Skip(size_t numberOfBytes);
	bool EndOfStream()const;
public:
	template<typename T> void Read(const T& argument)
	{
		UNREFERENCED_PARAMETER(argument);
	}
	template<typename T> void Read(T& argument)
	{
		(*this)>>argument;
	}
public:
	void Read(const int count, void* data);
private:
	const char* m_current;
	const char* m_end;
};
inline bool CBinaryReader::EndOfStream() const
{
	return (m_current == m_end);
}

#include "BinaryReadHelpers.h"

