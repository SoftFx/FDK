#pragma once



class CFixVersion
{
public:
    int Major;
    int Minor;
public:
    CFixVersion();
    CFixVersion(int major, int minor);
    CFixVersion(const std::string& st);
    bool SupportsMarketWithSlippage();
    bool SupportsAppId();
    bool SupportsTradeRequestType();
    bool SupportsModifyIOCandIFM();
private:
    friend bool operator < (const CFixVersion& first, const CFixVersion& second);
    friend bool operator > (const CFixVersion& first, const CFixVersion& second);
    friend bool operator <= (const CFixVersion& first, const CFixVersion& second);
    friend bool operator >= (const CFixVersion& first, const CFixVersion& second);
};