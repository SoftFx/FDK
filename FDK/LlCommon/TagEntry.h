#pragma once

namespace FDK
{
	class ITagFormatter
	{
	public:
		virtual void VFormat(const char* text) = 0;
	};
}

namespace FDK
{
	class ITagProvider
	{
	public:
		virtual void VCloneValue(void** pointer, const void* data) = 0;
		virtual void VCloneData(void** pointer, void* const * data) = 0;
		virtual void VDelete(void** pointer) = 0;
		virtual void* VGet(void** pointer) = 0;
		virtual void VFormat(void* const * pointer, ITagFormatter* pFormatter) = 0;
	};
}

namespace FDK
{
	class CTagFormatter : public ITagFormatter
	{
	public:
		inline CTagFormatter(std::ostream& stream)
            : m_stream(stream)
		{
		}
	public:
		virtual void VFormat(const char* text)
		{
			m_stream << text;
		}
	private:
		CTagFormatter(const CTagFormatter&);
		CTagFormatter& operator = (const CTagFormatter&);
	private:
		std::ostream& m_stream;
	};
}

namespace FDK
{
	template<typename T, bool largeObject> class TagProvider;
}


#ifdef new
#pragma push_macro("new")
#undef new
#define FDK_RESTORE_NEW
#endif

namespace FDK
{
	template<typename T> class TagProvider<T, false> : ITagProvider
	{
	public:
		virtual void VCloneValue(void** pointer, const void* data)
		{
			const T* pData = reinterpret_cast<const T*>(data);
			new(pointer) T(*pData);
		}
		virtual void VCloneData(void** pointer, void* const * data)
		{
			const T* pData = reinterpret_cast<const T*>(data);
			new(pointer) T(*pData);
		}
		virtual void VDelete(void** pointer)
		{
			T* pData = reinterpret_cast<T*>(pointer);
			pData;
			pData->~T();
		}
		virtual void* VGet(void** pointer)
		{
			return pointer;
		}
		virtual void VFormat(void* const * pointer, ITagFormatter* pFormatter)
		{
			std::stringstream stream;
			const T* pData = reinterpret_cast<const T*>(pointer);
			stream<<(*pData);
			std::string st = stream.str();
			pFormatter->VFormat(st.c_str());
		}
	public:
		static ITagProvider* Instance()
		{
			static TagProvider<T, false> gInstance;
			return &gInstance;
		}
	};
	template<> class TagProvider<const char*, false> : ITagProvider
	{
	public:
		virtual void VCloneValue(void** pointer, const void* data)
		{
			const char* pData = *reinterpret_cast<const char* const *>(data);
			size_t lenght = 1 + strlen(pData);
			*pointer = new char[lenght];
			memcpy(*pointer, pData, lenght);
		}
		virtual void VCloneData(void** pointer, void* const * data)
		{
			const char* pData = *reinterpret_cast<const char* const *>(data);
			size_t lenght = 1 + strlen(pData);
			*pointer = new char[lenght];
			memcpy(*pointer, pData, lenght);;
		}
		virtual void VDelete(void** pointer)
		{
			const char* pData = reinterpret_cast<const char*>(*pointer);
			delete[] pData;
		}
		virtual void* VGet(void** pointer)
		{
			return pointer;
		}
		virtual void VFormat(void* const * pointer, ITagFormatter* pFormatter)
		{
			const char* pData = reinterpret_cast<const char*>(*pointer);
			pFormatter->VFormat(pData);
		}
	public:
		static ITagProvider* Instance()
		{
			static TagProvider<const char*, false> gInstance;
			return &gInstance;
		}
	};
}

#ifdef FDK_RESTORE_NEW
#pragma pop_macro("new")
#undef FDK_RESTORE_NEW
#endif


namespace FDK
{
	template<typename T> class TagProvider<T, true> : ITagProvider
	{
	public:
		virtual void VCloneValue(void** pointer, const void* data)
		{
			const T* pData = reinterpret_cast<const T*>(data);
			*pointer = new T(*pData);
		}
		virtual void VCloneData(void** pointer, void* const * data)
		{
			const T* pData = reinterpret_cast<const T*>(*data);
			*pointer = new T(*pData);
		}
		virtual void VDelete(void** pointer)
		{
			T* pData = reinterpret_cast<T*>(*pointer);
			delete pData;
		}
		virtual void* VGet(void** pointer)
		{
			return pointer;
		}
		virtual void VFormat(void* const * pointer, ITagFormatter* pFormatter)
		{
			std::stringstream stream;
			const T* pData = reinterpret_cast<const T*>(*pointer);
			stream<<(*pData);
			std::string st = stream.str();
			pFormatter->VFormat(st.c_str());
		}
	public:
		static ITagProvider* Instance()
		{
			static TagProvider<T, true> gInstance;
			return &gInstance;
		}
	};
}

namespace FDK
{
	class CTagEntry
	{
	public:
		inline CTagEntry() : m_data(), m_provider()
		{
		}
		inline CTagEntry(const CTagEntry& entry) : m_data(), m_provider(entry.m_provider)
		{
			if (nullptr != m_provider)
			{
				m_provider->VCloneData(&m_data, &entry.m_data);
			}
		}
		inline CTagEntry(const char* value) : m_data(), m_provider()
		{
			SetData(value);
		}
		template<typename T> inline CTagEntry(const T& value) : m_data(), m_provider()
		{
			SetData<T>(value);
		}
		inline CTagEntry& operator = (const char* value)
		{
			SetData(value);
			return *this;
		}
		template<typename T> inline CTagEntry& operator = (const T& value)
		{
			SetData<T>(value);
			return *this;
		}
		inline CTagEntry& operator = (const CTagEntry& entry)
		{
			if (this != &entry)
			{
				Finalize();
				ITagProvider* provider = entry.m_provider;
				if (nullptr != provider)
				{
					provider->VCloneData(&m_data, &entry.m_data);
					m_provider = provider;
				}
			}
			return *this;
		}
		inline ~CTagEntry()
		{
			Finalize();
		}
	private:
		inline void Finalize()
		{
			if (nullptr != m_provider)
			{
				m_provider->VDelete(&m_data);
				m_provider = nullptr;
			}
		}
	public:
		template<typename T> inline const T& GetData() const
		{
			CTagEntry* pThis = const_cast<CTagEntry*>(this);
			return pThis->GetData<T>();
		}
		template<typename T> inline T& GetData()
		{
			if (nullptr == m_provider)
			{
				throw std::runtime_error("Tag provider is null");
			}
			if (typeid(*m_provider) != typeid(TagProvider<T, (sizeof(T) > sizeof(m_data))>))
			{
				throw std::runtime_error("Bad type");
			}
			T* pData = reinterpret_cast<T*>(m_provider->VGet(&m_data));
			T& result = *pData;
			return result;
		}
		inline void SetData(const char* newData)
		{
			SetData<const char*>(newData);
		}
		template<typename T> inline void SetData(const T& newData)
		{
			Finalize();
			ITagProvider* provider = TagProvider<T, (sizeof(T) > sizeof(m_data))>::Instance();
			provider->VCloneValue(&m_data, &newData);
			m_provider = provider;
		}
	public:
		inline void Format(ITagFormatter* pFormatter) const
		{
			if (nullptr != m_provider)
			{
				m_provider->VFormat(&m_data, pFormatter);
			}
		}
	private:
		void* m_data;
		ITagProvider* m_provider;
	private:
	};

	inline std::ostream& operator << (std::ostream& stream, const CTagEntry& entry)
	{
		CTagFormatter formatter(stream);
		entry.Format(&formatter);
		return stream;
	}
}