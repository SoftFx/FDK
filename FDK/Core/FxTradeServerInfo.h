#pragma once

class CORE_API CFxTradeServerInfo
{
public:

    string CompanyName;
    string CompanyFullName;
    wstring CompanyDescription;
    string CompanyAddress;
    string CompanyEmail;
    string CompanyPhone;
    string CompanyWebSite;
    string ServerName;
    string ServerFullName;
    wstring ServerDescription;
    string ServerAddress;
    Nullable<int32_t> ServerFixFeedSslPort;
    Nullable<int32_t> ServerFixTradeSslPort;
    Nullable<int32_t> ServerWebSocketFeedPort;
    Nullable<int32_t> ServerWebSocketTradePort;
    Nullable<int32_t> ServerRestPort;
};

