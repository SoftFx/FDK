#ifndef __Core_Fx_Trade_History_Response__
#define __Core_Fx_Trade_History_Response__


class CORE_API CFxTradeHistoryResponse : public FxTradeHistoryResponse
{
public:
	CFxTradeHistoryResponse();
	CFxTradeHistoryResponse(const CFxTradeHistoryResponse& arg);
	CFxTradeHistoryResponse& operator = (const CFxTradeHistoryResponse& arg);
	~CFxTradeHistoryResponse();
};

#endif
