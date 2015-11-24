#include "stdafx.h"
#include "ConverterImpl.h"
#include "..\Core\FxMsgData.h"


namespace
{
	template<typename T> T DataFromHandle(void* handle)
	{
		if (nullptr == handle)
		{
			throw CArgumentNullException();
		}
		FxRef<CFxMsgData<T> > data = TypeFromHandle<CFxMsgData<T> >(handle);
		if (!data)
		{
			throw CInvalidHandleException(handle);
		}
		return data->Data();
	}
}

vector<CFxCurrencyInfo> CConverterImpl::CurrenciesFromHandle(void* handle)
{
    return DataFromHandle<vector<CFxCurrencyInfo> >(handle);
}

vector<CFxSymbolInfo> CConverterImpl::SymbolsFromHandle(void* handle)
{
	return DataFromHandle<vector<CFxSymbolInfo> >(handle);
}

CFxSessionInfo CConverterImpl::SessionInfoFromHandle(void* handle)
{
	return DataFromHandle<CFxSessionInfo>(handle);
}

CNotification CConverterImpl::NotificationFromHandle(void* handle)
{
	return DataFromHandle<CNotification>(handle);
}

CFxQuote CConverterImpl::QuoteFromHandle(void* handle)
{
	return DataFromHandle<CFxQuote>(handle);
}

std::string CConverterImpl::ProtocolVersionFromHandle(void* handle)
{
	return DataFromHandle<std::string>(handle);
}

CFxAccountInfo CConverterImpl::AccountInfoFromHandle(void* handle)
{
	return DataFromHandle<CFxAccountInfo>(handle);
}

CFxPositionReport CConverterImpl::PositionFromHandle(void* handle)
{
	return DataFromHandle<CFxPositionReport>(handle);
}

CFxTradeTransactionReport CConverterImpl::TradeTransactionReportFromHandle(void* handle)
{
	return DataFromHandle<CFxTradeTransactionReport>(handle);
}

CFxExecutionReport CConverterImpl::ExecutionReportFromHandle(void* handle)
{
	return DataFromHandle<CFxExecutionReport>(handle);
}

void CConverterImpl::GetLogoutInfoFromHandle(void* handle, string& text, FxLogoutReason& reason, int32& code)
{
	if (nullptr == handle)
	{
		throw CArgumentNullException();
	}
	FxRef<CFxMsgLogout> data = TypeFromHandle<CFxMsgLogout >(handle);
	if (!data)
	{
		throw CInvalidHandleException(handle);
	}
	text = data->Text;
	reason = data->Reason;
	code = data->Code;
}
