#pragma once

class CORE_API CFxTradeServerInfo
{
public:

    wstring CompanyName;
    wstring CompanyFullName;
    wstring CompanyDescription;
    wstring CompanyAddress;
    string CompanyEmail;
    string CompanyPhone;
    string CompanyWebSite;
    wstring ServerName;
    wstring ServerFullName;
    wstring ServerDescription;
    string ServerAddress;
    Nullable<int32_t> ServerFixFeedSslPort;
    Nullable<int32_t> ServerFixTradeSslPort;
    Nullable<int32_t> ServerWebSocketFeedPort;
    Nullable<int32_t> ServerWebSocketTradePort;
    Nullable<int32_t> ServerRestPort;
};

