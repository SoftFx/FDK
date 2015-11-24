#pragma once

#include <stdexcept> 


#define LRP_CUSTOMER				0x20000000L

#define LRP_ERROR					(0xC0000000L | LRP_CUSTOMER)



#define LRP_INVALID_COMPONENT_ID	(0 | LRP_ERROR)
#define LRP_INVALID_METHOD_ID		(1 | LRP_ERROR)
#define LRP_EXCEPTION				(2 | LRP_ERROR)



#ifdef LRPCORE_EXPORTS
#ifndef LRPCORE_API
#define LRPCORE_API __declspec(dllexport)
#endif
#else
#ifndef LRPCORE_API
#define LRPCORE_API __declspec(dllimport)
#endif
#endif




class MemoryBuffer
{
public:
	LRPCORE_API MemoryBuffer(size_t size);
	MemoryBuffer(unsigned __int16 componentId, unsigned __int16 methodId);
public:
	inline MemoryBuffer() : m_heap(nullptr), m_data(nullptr), m_size(), m_capacity(), m_position()
	{
	}
	inline MemoryBuffer(void* heap) : m_heap(heap), m_data(nullptr), m_size(4), m_capacity(4), m_position()
	{
		m_data= reinterpret_cast<char*>(HeapAlloc(heap, 0, 4));
		if (nullptr == m_data)
		{
			throw std::bad_alloc();
		}
	}
	inline MemoryBuffer(void* heap, void* data, size_t size, size_t capacity) : m_heap(heap), m_data(reinterpret_cast<char*>(data)), m_size(size), m_capacity(capacity), m_position()
	{
	}
	inline ~MemoryBuffer()
	{
		if ((nullptr != m_heap) && (nullptr != m_data))
		{
			HeapFree(m_heap, 0, m_data);
		}
	}
public:
	void Swap(MemoryBuffer& buffer)
	{
		std::swap(m_heap, buffer.m_heap);
		std::swap(m_data, buffer.m_data);
		std::swap(m_size, buffer.m_size);
		std::swap(m_capacity, buffer.m_capacity);
		std::swap(m_position, buffer.m_position);
	}
public:
	void Construct(size_t size)
	{
		if (m_capacity < size)
		{
			ReAlloc(size);
		}
		m_size = size;
		m_position = 0;
	}
	void ReInitialize(size_t newCapacity, size_t newSize, void* newData)
	{
		m_data = (char*)newData;
		m_position = 0;
		m_size = newSize;
		m_capacity = newCapacity;
	}
public:
	std::string ReadAString()
	{
		size_t currentPosition = m_position;

		size_t length = ReadImpl<unsigned __int32>();
		size_t newPosition = m_position + length;
		if (newPosition > m_size)
		{
			m_position = currentPosition; // revert position
			throw std::runtime_error("End of stream has been reached.");
		}
		const char* data = m_data + m_position;
		m_position = newPosition;

		try
		{
			return std::string(data, data + length);
		}
		catch (...)
		{
			m_position = currentPosition; // revert position
			throw;
		}
	}
	std::wstring ReadWString()
	{
		size_t currentPosition = m_position;

		size_t length = ReadImpl<unsigned __int32>();
		size_t newPosition = m_position + sizeof(wchar_t) * length;
		if (newPosition > m_size)
		{
			m_position = currentPosition; // revert position
			throw std::runtime_error("End of stream has been reached.");
		}
		const wchar_t* data = reinterpret_cast<const wchar_t*>(m_data + m_position);
		m_position = newPosition;

		try
		{
			return std::wstring(data, data + length);
		}
		catch (...)
		{
			m_position = currentPosition; // revert position
			throw;
		}
	}
	void WriteAString(const std::string& arg)
	{
		size_t newPosition = m_position + sizeof(unsigned __int32) + arg.length();
		if (newPosition > m_capacity)
		{
			ReAlloc(newPosition);
		}
		unsigned __int32* pointer = (unsigned __int32*)(m_data + m_position);
		*pointer = static_cast<unsigned __int32>(arg.length());
		++pointer;
		memcpy(pointer, arg.data(), arg.length());
		m_position = newPosition;
	}
	void WriteAString(const char* arg, const size_t maxCount)
	{
		size_t length = 0;
		for (; length < maxCount; ++length)
		{
			if (0 == arg[length])
			{
				break;
			}
		}
		size_t newPosition = m_position + sizeof(unsigned __int32) + length;
		if (newPosition > m_capacity)
		{
			ReAlloc(newPosition);
		}
		unsigned __int32* pointer = (unsigned __int32*)(m_data + m_position);
		*pointer = static_cast<unsigned __int32>(length);
		++pointer;
		memcpy(pointer, arg, length);
		m_position = newPosition;
	}
	void WriteWString(const std::wstring& arg)
	{
		size_t newPosition = m_position + sizeof(unsigned __int32) + sizeof(wchar_t) * arg.length();
		if (newPosition > m_capacity)
		{
			ReAlloc(newPosition);
		}
		unsigned __int32* pointer = (unsigned __int32*)(m_data + m_position);
		*pointer = static_cast<unsigned __int32>(arg.length());
		++pointer;
		memcpy(pointer, arg.data(), sizeof(wchar_t) * arg.length());
		m_position = newPosition;
	}
public:
	size_t* GetSizePointer()
	{
		return &m_size;
	}
	size_t* GetCapacityPointer()
	{
		return &m_capacity;
	}
	void** GetDataPointer()
	{
		return reinterpret_cast<void**>(&m_data);
	}
	void* GetHeap()
	{
		return m_heap;
	}
public:
	void SetPosition(const size_t newPosition)
	{
		if (newPosition > m_size)
		{
			throw std::out_of_range("New position is out of range.");
		}
		if (m_size < m_position)
		{
			m_size = m_position;
		}
		m_position = newPosition;
	}
	void Reset(size_t offset = 0)
	{
		m_position = offset;
		m_size = offset;
	}
	size_t GetPosition() const
	{
		return m_position;
	}
	size_t MovePosition(const ptrdiff_t moving)
	{
		size_t result = GetPosition();
		result += moving;
		SetPosition(result);
		return result;
	}
	size_t GetSize() const
	{
		size_t result = (m_size > m_position) ? m_size : m_position;
		return result;
	}
	size_t GetCapacity() const
	{
		return m_capacity;
	}
	void* GetData() const 
	{
		return m_data;
	}
public:
	size_t ReadCount()
	{
		size_t newPosition = m_position + sizeof(__int32);
		if (newPosition > m_size)
		{
			throw std::runtime_error("End of stream has been reached.");
		}
		const __int32 count = *(const __int32*)(m_data + m_position);
		if (count < 0)
		{
			throw std::runtime_error("Container size can not be negative");
		}
		const size_t result = static_cast<size_t>(count);
		if (newPosition + result > m_size)
		{
			throw std::runtime_error("Container size can not be more than available size");
		}
		m_position = newPosition;
		return result;
	}
public:
	template<typename T> void WriteImpl(const T& value)
	{
		size_t newPosition = m_position + sizeof(T);
		if (newPosition > m_capacity)
		{
			ReAlloc(newPosition);
		}
		T* pointer = (T*)(m_data + m_position);
		*pointer = value;
		m_position = newPosition;
	}
	template<typename T> T ReadImpl()
	{
		size_t newPosition = m_position + sizeof(T);
		if (newPosition > m_size)
		{
			throw std::runtime_error("End of stream has been reached.");
		}
		auto result = *(const T*)(m_data + m_position);
		m_position = newPosition;
		return result;
	}
public:
	void WriteRaw(const void* data, const size_t size)
	{
		const size_t newPosition = m_position + size;
		if (newPosition > m_capacity)
		{
			ReAlloc(newPosition);
		}
		char* pointer = m_data + m_position;
		memcpy(pointer, data, size);

		m_position = newPosition;
	}
private:
	void ReAlloc(size_t requiredSize)
	{
		size_t newSize = m_capacity;

		do
		{
			newSize *= 2;
		} while (newSize < requiredSize);

		void* newPointer = HeapAlloc(m_heap, 0, newSize);
		if (nullptr == newPointer)
		{
			throw std::bad_alloc();
		}
		memcpy(newPointer, m_data, GetSize());

		HeapFree(m_heap, 0, m_data);
		m_data = reinterpret_cast<char*>(newPointer);
		m_capacity = newSize;
	}
private:
	void* m_heap;		// memory heap for allocation; used for writing.
private:
	char* m_data;		// start of data block; used for reading and writing.
	size_t m_size;		// size of written data; used for reading.
	size_t m_capacity;	// size of allocated memory; used for writing.
private:
	size_t m_position;	// current stream position; used for reading and writing.
};

#include "Nullable.h"




#define LRP_EMBEDDED_TYPE_SERIALIZATION(name, T)	inline void Write##name(const T& value, MemoryBuffer& buffer)\
													{\
														buffer.WriteImpl(value);\
													}\
													inline void WriteNull##name(const Nullable<T>& value, MemoryBuffer& buffer)\
													{\
														if(value.HasValue())\
														{\
															WriteBoolean(true, buffer);\
															Write##name(*value, buffer);\
														}\
														else\
														{\
															WriteBoolean(false, buffer);\
														}\
													}\
													inline T Read##name(MemoryBuffer& buffer)\
													{\
														return buffer.ReadImpl<T>();\
													}\
													inline Nullable<T> ReadNull##name(MemoryBuffer& buffer)\
													{\
														if (ReadBoolean(buffer))\
														{\
															return Read##name(buffer);\
														}\
														return nullptr;\
													}


LRP_EMBEDDED_TYPE_SERIALIZATION(Boolean, bool);

LRP_EMBEDDED_TYPE_SERIALIZATION(Int8, __int8);
LRP_EMBEDDED_TYPE_SERIALIZATION(UInt8, unsigned __int8);

LRP_EMBEDDED_TYPE_SERIALIZATION(Int16, __int16);
LRP_EMBEDDED_TYPE_SERIALIZATION(UInt16, unsigned __int16);

LRP_EMBEDDED_TYPE_SERIALIZATION(Int32, __int32);
LRP_EMBEDDED_TYPE_SERIALIZATION(UInt32, unsigned __int32);

LRP_EMBEDDED_TYPE_SERIALIZATION(Int64, __int64);
LRP_EMBEDDED_TYPE_SERIALIZATION(UInt64, unsigned __int64);


LRP_EMBEDDED_TYPE_SERIALIZATION(Single, float);
LRP_EMBEDDED_TYPE_SERIALIZATION(Double, double);

#undef LRP_EMBEDDED_TYPE_SERIALIZATION

inline void WriteLocalPointer(void* value, MemoryBuffer& buffer)
{
	buffer.WriteImpl<void*>(value);
}
inline void* ReadLocalPointer(MemoryBuffer& buffer)
{
	return buffer.ReadImpl<void*>();
}

inline void WriteRemotePointer(void* value, MemoryBuffer& buffer)
{
	__int64 handle = reinterpret_cast<__int64>(value);
	WriteInt64(handle, buffer);
}
inline void* ReadRemotePointer(MemoryBuffer& buffer)
{
	__int64 handle = ReadInt64(buffer);
	void* result = reinterpret_cast<void*>(handle);
	return result;
}
inline void WriteAString(const std::string& value, MemoryBuffer& buffer)
{
	buffer.WriteAString(value);
}
template<size_t count> void WriteAString(const char (&value)[count], MemoryBuffer& buffer)
{
	buffer.WriteAString(value, count);
}
inline const std::string ReadAString(MemoryBuffer& buffer)
{
	return buffer.ReadAString();
}
inline void WriteWString(const std::wstring& value, MemoryBuffer& buffer)
{
	buffer.WriteWString(value);
}
inline const std::wstring ReadWString(MemoryBuffer& buffer)
{
	return buffer.ReadWString();
}
#ifndef HAVE_CUSTOM_DATE_TIME
#include "DateTime.h"
#endif


inline void WriteTime(const CDateTime arg, MemoryBuffer& buffer)
{
	WriteUInt64(arg, buffer);
}
inline void WriteNullTime(const Nullable<CDateTime>& arg, MemoryBuffer& buffer)
{
	if (arg.HasValue())
	{
		WriteBoolean(true, buffer);
		WriteTime(*arg, buffer);
	}
	else
	{
		WriteBoolean(false, buffer);
	}
}
inline CDateTime ReadTime(MemoryBuffer& buffer)
{
	return ReadUInt64(buffer);
}
inline Nullable<CDateTime> ReadNullTime(MemoryBuffer& buffer)
{
	bool isNotNull = ReadBoolean(buffer);
	if (isNotNull)
	{
		CDateTime result = ReadTime(buffer);
		return result;
	}
	else
	{
		return Nullable<CDateTime>();
	}
}
inline void WriteRaw(const MemoryBuffer& data, MemoryBuffer& buffer)
{
	buffer.WriteRaw(data.GetData(), data.GetSize());
}
inline MemoryBuffer& ReadRaw(MemoryBuffer& buffer)
{
	return buffer;
}





namespace std
{
	inline void swap(MemoryBuffer& first, MemoryBuffer& second)
	{
		first.Swap(second);
	}
}