#ifndef __Core_Fx_Msg_Data__
#define __Core_Fx_Msg_Data__

template<typename T> class CFxMsgData : public CFxHandle
{
private:
    T m_data;
public:
    CFxMsgData(T& data)
    {
        swap(m_data, data);
    }
    CFxMsgData(const T& data) : m_data(data)
    {
    }
public:
    const T& Data() const
    {
        return m_data;
    }
};



#include "FxQuote.h"
#include "FxAccountInfo.h"
#include "FxSessionInfo.h"
#include "FxTradeTransactionReport.h"
#include "FxPositionReport.h"

typedef CFxMsgData<CFxQuote> CFxMsgSubscribed;
typedef CFxMsgData<string> CFxMsgUnsubscribed;
typedef CFxMsgData<CFxQuote> CFxMsgTick;
typedef CFxMsgData<CFxAccountInfo> CFxMsgAccountInfo;
typedef CFxMsgData<vector<CFxCurrencyInfo>> CFxMsgCurrencyInfo;
typedef CFxMsgData<vector<CFxSymbolInfo>> CFxMsgSymbolInfo;
typedef CFxMsgData<CFxTwoFactorAuth> CFxMsgTwoFactorAuth;
typedef CFxMsgData<CFxSessionInfo> CFxMsgSessionInfo;
typedef CFxMsgData<CFxExecutionReport> CFxMsgExecutionReport;
typedef CFxMsgData<CFxTradeTransactionReport> CFxMsgTradeTransactionReport;
typedef CFxMsgData<CFxPositionReport> CFxMsgPositionReport;
typedef CFxMsgData<CNotification> CFxMsgNotification;
typedef CFxMsgData<string> CFxMsgLogon;

#endif
