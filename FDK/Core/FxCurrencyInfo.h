#ifndef __Core_CurrencyInfo__
#define __Core_CurrencyInfo__

#include <string>
#include"Types.h"

class CFxCurrencyInfo
{
public:
    CFxCurrencyInfo();
    CFxCurrencyInfo(const std::string& name, const std::wstring& description, const int& sortOrder, const int& precision);

public:
    std::string Name;
    std::wstring Description;
    int32 SortOrder;
    int32 Precision;
};

inline CFxCurrencyInfo::CFxCurrencyInfo()
    : Name()
    , Description()
    , SortOrder()
    , Precision()
{
}

inline CFxCurrencyInfo::CFxCurrencyInfo(const std::string& name, const std::wstring& description, const int& sortOrder, const int& precision)
    : Name(name)
    , Description(description)
    , SortOrder(sortOrder)
    , Precision(precision)
{
}

#endif
