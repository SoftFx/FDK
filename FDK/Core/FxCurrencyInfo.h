#ifndef __Core_CurrencyInfo__
#define __Core_CurrencyInfo__

class CFxCurrencyInfo
{
public:
    CFxCurrencyInfo();
    CFxCurrencyInfo(const string& name, const string& description, const int& sortOrder, const int& precision);

public:
    string Name;
    string Description;
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

inline CFxCurrencyInfo::CFxCurrencyInfo(const string& name, const string& description, const int& sortOrder, const int& precision)
    : Name(name)
    , Description(description)
    , SortOrder(sortOrder)
    , Precision(precision)
{
}

#endif
