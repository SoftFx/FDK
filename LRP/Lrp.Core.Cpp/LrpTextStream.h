#pragma once

class ILrpTextStream
{
public:
    virtual void Write(const std::string& text) = 0;
};




template<typename T> inline void LrpWriteImpl(const char* name, const T& value, std::ostream& stream)
{
    if (nullptr != name)
    {
        stream<<name<<" = ";
    }
    stream<<value;
}



#define LRP_EMBEDDED_TYPE_SERIALIZATION(suffix, T)  inline void LrpWrite##suffix(const char* name, const T& value, std::ostream& stream)\
                                                    {\
                                                        LrpWriteImpl(name, value, stream);\
                                                    }\





LRP_EMBEDDED_TYPE_SERIALIZATION(Int16, __int16);
LRP_EMBEDDED_TYPE_SERIALIZATION(Int32, __int32);
LRP_EMBEDDED_TYPE_SERIALIZATION(Int64, __int64);


LRP_EMBEDDED_TYPE_SERIALIZATION(UInt16, unsigned __int16);
LRP_EMBEDDED_TYPE_SERIALIZATION(UInt32, unsigned __int32);
LRP_EMBEDDED_TYPE_SERIALIZATION(UInt64, unsigned __int64);

inline void LrpWriteUInt8(const char* name, const __int8 value, std::ostream& stream)
{
    LrpWriteUInt32(name, value, stream);
}
inline void LrpWriteInt8(const char* name, const unsigned __int8 value, std::ostream& stream)
{
    LrpWriteInt32(name, value, stream);
}
inline void LrpWriteBinaryData(const size_t value, std::ostream& stream)
{
    if (value < 10)
    {
        stream<<static_cast<char>('0' + value);
    }
    else
    {
        stream<<static_cast<char>('A' + value - 10);
    }
}
inline void LrpWriteBinaryData(const void* data, const size_t count, std::ostream& stream)
{
    stream<<"(0x";
    const unsigned char* current = reinterpret_cast<const unsigned char*>(data);
    const unsigned char* end= current + count;
    for (; current < end; ++current)
    {
        size_t first = *current;
        size_t second = first % 16;
        first /= 16;
        LrpWriteBinaryData(first, stream);
        LrpWriteBinaryData(second, stream);
    }
    stream<<")";
}
inline void LrpWriteNullInt32(const char* name, const Nullable<__int32>& value, std::ostream& stream)
{
    if (!value.HasValue())
        return;

    LrpWriteImpl(name, value.Value(), stream);
    LrpWriteBinaryData(&value.Value(), sizeof(__int32), stream);
}
inline void LrpWriteNullInt64(const char* name, const Nullable<__int64>& value, std::ostream& stream)
{
    if (!value.HasValue())
        return;

    LrpWriteImpl(name, value.Value(), stream);
    LrpWriteBinaryData(&value.Value(), sizeof(__int64), stream);
}
inline void LrpWriteTime(const char* name, const CDateTime& value, std::ostream& stream)
{
    LrpWriteImpl(name, value, stream);
    LrpWriteBinaryData(&value, sizeof(CDateTime), stream);
}
inline void LrpWriteSingle(const char* name, const float& value, std::ostream& stream)
{
    LrpWriteImpl(name, value, stream);
    LrpWriteBinaryData(&value, sizeof(float), stream);
}
inline void LrpWriteDouble(const char* name, const double& value, std::ostream& stream)
{
    LrpWriteImpl(name, value, stream);
    LrpWriteBinaryData(&value, sizeof(double), stream);
}
inline void LrpWriteNullDouble(const char* name, const Nullable<double>& value, std::ostream& stream)
{
    if (!value.HasValue())
        return;

    LrpWriteImpl(name, value.Value(), stream);
    LrpWriteBinaryData(&value.Value(), sizeof(double), stream);
}
inline void LrpWriteBoolean(const char* name, const bool& value, std::ostream& stream)
{
    if (nullptr != name)
    {
        stream<<name<<" = ";
    }
    stream<<(value? "true" : "false");
}
inline void LrpWriteNullBoolean(const char* name, const Nullable<bool>& value, std::ostream& stream)
{
    if (!value.HasValue())
        return;

    LrpWriteImpl(name, value.Value(), stream);
    LrpWriteBinaryData(&value.Value(), sizeof(bool), stream);
}
inline void LrpWriteAChar(const char ch, std::ostream& stream)
{
    if ('\n' == ch)
    {
        stream<<"\\n";
    }
    else if ('\t' == ch)
    {
        stream<<"\\t";
    }
    else if ('\r' == ch)
    {
        stream<<"\\r";
    }
    else if ('"' == ch)
    {
        stream<<"\\\"";
    }
    else if ('\0' == ch)
    {
        stream<<"\0";
    }
    else if ('\\' == ch)
    {
        stream<<"\\\\";
    }
    else
    {
        stream<<ch;
    }
}
inline void LrpWriteAString(const char* name, const std::string& value, std::ostream& stream)
{
    if (nullptr != name)
    {
        stream<<name<<" = ";
    }
    stream<<"\"";
    for each (auto element in value)
    {
        LrpWriteAChar(element, stream);
    }
    stream<<'"';
}
template<size_t count> void WirteAString(const char* name, const char (&value)[count], std::ostream& stream)
{
    if (nullptr != name)
    {
        stream<<name<<" = ";
    }
    stream<<"\"";
    for each (auto element in value)
    {
        if (0 == element)
        {
            break;
        }
        LrpWriteAChar(element, stream);
    }
    stream<<'"';
}
template<typename T> inline typename T::const_iterator LrpBeginIterator(const T& value)
{
    return value.begin();
}
template<typename T, size_t count> inline const T* LrpBeginIterator(const T (&value)[count])
{
    const T* result = value;
    return result;
}
template<typename T> inline typename T::const_iterator LrpEndIterator(const T& value)
{
    return value.end();
}
template<typename T, size_t count> inline const T* LrpEndIterator(const T (&value)[count])
{
    const T* result = value;
    result += count;
    return result;
}





#undef LRP_EMBEDDED_TYPE_SERIALIZATION