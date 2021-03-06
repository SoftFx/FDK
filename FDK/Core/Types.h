#ifndef __Core_Api_Types__
#define __Core_Api_Types__

//error constants
#define FX_MAX_ENUM INT_MAX

// customer's bit
#define FX_CODE_CUSTOMER            0x20000000L

#define FX_CODE_SUCCESS             (0x00000000L | FX_CODE_CUSTOMER)
#define FX_CODE_INFORMATION         (0x40000000L | FX_CODE_CUSTOMER)
#define FX_CODE_WARNING             (0x80000000L | FX_CODE_CUSTOMER)
#define FX_CODE_ERROR               (0xC0000000L | FX_CODE_CUSTOMER)


#define FX_CODE_START 0


#define FX_CODE_ERROR_SEND          ((FX_CODE_START + 0) | FX_CODE_ERROR)
#define FX_CODE_ERROR_TIMEOUT           ((FX_CODE_START + 1) | FX_CODE_ERROR)
#define FX_CODE_ERROR_RECEIVE           ((FX_CODE_START + 2) | FX_CODE_ERROR)
#define FX_CODE_ERROR_EXCEPTION         ((FX_CODE_START + 3) | FX_CODE_ERROR)
#define FX_CODE_ERROR_SYMBOL_NOT_FOUND      ((FX_CODE_START + 4) | FX_CODE_ERROR)
#define FX_CODE_ERROR_OFF_QUOTES        ((FX_CODE_START + 5) | FX_CODE_ERROR)
#define FX_CODE_ERROR_CACHE_NOT_INITIALIZED ((FX_CODE_START + 6) | FX_CODE_ERROR)
#define FX_CODE_ERROR_REJECT            ((FX_CODE_START + 7) | FX_CODE_ERROR)
#define FX_CODE_ERROR_LOGOUT            ((FX_CODE_START + 8) | FX_CODE_ERROR)
#define FX_CODE_ERROR_INVALID_HANDLE        ((FX_CODE_START + 9) | FX_CODE_ERROR)



#define FX_ER_TYPE_OPERATION 0
#define FX_ER_TYPE_LIST      1


enum FxTradeRecordType
{
    FxTradeRecordType_None                  = -1,
    FxTradeRecordType_Market                = 0,
    FxTradeRecordType_Position              = 1,
    FxTradeRecordType_Limit                 = 2,
    FxTradeRecordType_Stop                  = 3,
    FxTradeRecordType_IoC                   = 4,
    FxTradeRecordType_MarketWithSlippage    = 5,
    FxTradeRecordType_StopLimit             = 6,
    FxTradeRecordType_StopLimit_IoC         = 7,
    FxTradeRecordType_Last                  = FX_MAX_ENUM
};

enum FxOrderType
{
    FxOrderType_None                = -1,
    FxOrderType_Market              = 0,
    FxOrderType_Position            = 1,
    FxOrderType_Limit               = 2,
    FxOrderType_Stop                = 3,
    FxOrderType_IoC                 = 4,
    FxOrderType_MarketWithSlippage  = 5,
    FxOrderType_StopLimit           = 6,
    FxOrderType_StopLimit_IoC       = 7,
    FxOrderType_Last                = FX_MAX_ENUM
};

enum FxRejectReason
{
    FxRejectReason_None                         = -1,
    FxRejectReason_DealerReject                 = 0,
    FxRejectReason_UnknownSymbol                = 1,
    FxRejectReason_TradeSessionIsClosed         = 2,
    FxRejectReason_OrderExceedsLImit            = 3,
    FxRejectReason_OffQuotes                    = 4,
    FxRejectReason_UnknownOrder                 = 5,
    FxRejectReason_DuplicateClientOrderId       = 6,
    FxRejectReason_InvalidTradeRecordParameters = 11,
    FxRejectReason_IncorrectQuantity            = 13,
    FxRejectReason_IncorrectAllocatedQuantity   = 14,
    FxRejectReason_UnknownAccounts              = 15,
    FxRejectReason_Throttling                   = 16,
    FxRejectReason_Timeout                      = 17,
    FxRejectReason_CloseOnly                    = 18,
    FxRejectReason_Unknown                      = 99,
    FxRejectReason_Last                         = FX_MAX_ENUM
};

enum FxTradeRecordSide
{
    FxTradeRecordSide_None  = -1,
    FxTradeRecordSide_Buy   = 1,
    FxTradeRecordSide_Sell  = 2,
    FxTradeRecordSide_Last  = FX_MAX_ENUM
};

enum FxAccountType
{
    FxAccountType_None  = -1,
    FxAccountType_Net   = 0,
    FxAccountType_Gross = 1,
    FxAccountType_Cash  = 2,
    FxAccountType_Last  = FX_MAX_ENUM
};

enum FxPriceType
{
    FxPriceType_None = 0,
    FxPriceType_Bid  = 1,
    FxPriceType_Ask  = 2,
    FxPriceType_Last = FX_MAX_ENUM
};

enum class FxCommissionChargeType
{
    FxCommissionChargeType_PerLot   = 0,
    FxCommissionChargeType_PerTrade = 1,
};

enum class FxCommissionChargeMethod
{
    FxCommissionChargeMethod_OneWay    = 0,
    FxCommissionChargeMethod_RoundTurn = 1,
};

enum class FxCommissionType
{
   FxCommissionType_PerUnit                  = 0,
   FxCommissionType_Percent                  = 1,
   FxCommissionType_Absolute                 = 2,
   FxCommissionType_PercentageWaivedCash     = 3,
   FxCommissionType_PercentageWaivedEnhanced = 4,
   FxCommissionType_PerBond                  = 5
};

enum FxMarketHistoryRejectType
{
    FxSuccess               = 0,
    FxInvalidSymbol         = 1,
    FxInvalidPeriodicity    = 2,
    FxUnknownError          = 99
};

#define FX_GRAPH_PERIOD_S1      0
#define FX_GRAPH_PERIOD_S10     1
#define FX_GRAPH_PERIOD_M1      2
#define FX_GRAPH_PERIOD_M10     3
#define FX_GRAPH_PERIOD_M15     4
#define FX_GRAPH_PERIOD_H1      5
#define FX_GRAPH_PERIOD_H4      6
#define FX_GRAPH_PERIOD_D1      7
#define FX_GRAPH_PERIOD_W1      8
#define FX_GRAPH_PERIOD_MN1     9


#define FX_REPORT_TYPE_GROUPS   0
#define FX_REPORT_TYPE_BINARY   1
#define FX_REPORT_TYPE_FILE 2

#define FX_GRAPH_TYPE_BARS  0
#define FX_GRAPH_TYPE_TICKS 1
#define FX_GRAPH_TYPE_LEVEL2    2


// ERS == EXECUTION_REPORT_STATUS, but the second case is too long
enum FxOrderStatus
{
    FxOrderStatus_None          = -1,
    FxOrderStatus_New           = 0,
    FxOrderStatus_Calculated        = 1,
    FxOrderStatus_Filled        = 2,
    FxOrderStatus_PartiallyFilled   = 3,
    FxOrderStatus_Canceled      = 4,
    FxOrderStatus_PendingCancel     = 5,
    FxOrderStatus_Rejected      = 6,
    FxOrderStatus_Expired       = 7,
    FxOrderStatus_PendingReplace    = 8,
    FxOrderStatus_Done          = 9,
    FxOrderStatus_PendingClose      = 10,
    FxOrderStatus_Activated     = 11,
    FxOrderStatus_Last          = FX_MAX_ENUM
};




#define FX_PROFIT_CALC_MODE_FOREX       0
#define FX_PROFIT_CALC_MODE_CFD         1
#define FX_PROFIT_CALC_MODE_FUTURES     2
#define FX_PROFIT_CALC_MODE_CFD_INDEX       3
#define FX_PROFIT_CALC_MODE_CFD_LEVERAGE    4


#define FX_MARGIN_CALC_MODE_FOREX   0
#define FX_MARGIN_CALC_MODE_CFD     1
#define FX_MARGIN_CALC_MODE_FUTURES 2


enum FxExecutionType
{
    FxExecutionType_None        = -1,
    FxExecutionType_New         = 0,
    FxExecutionType_Trade       = 1,
    FxExecutionType_Expired     = 2,
    FxExecutionType_Canceled        = 3,
    FxExecutionType_PendingCancel   = 4,
    FxExecutionType_Rejected        = 5,
    FxExecutionType_Calculated      = 6,
    FxExecutionType_PendingReplace  = 7,
    FxExecutionType_Replace     = 8,
    FxExecutionType_OrderStatus     = 9,
    FxExecutionType_PendingClose    = 10,
    FxExecutionType_Last        = FX_MAX_ENUM
};

enum FxLogoutReason
{
    FxLogoutReason_None         = -1,
    FxLogoutReason_Unknown      = 0,
    FxLogoutReason_NetworkError     = 1,
    FxLogoutReason_Timeout      = 2,
    FxLogoutReason_BlockedAccount   = 3,
    FxLogoutReason_ClientInitiated  = 4,
    FxLogoutReason_InvalidCredentials   = 5,
    FxLogoutReason_SlowConnection   = 6,
    FxLogoutReason_InvalidSession   = 7,
    FxLogoutReason_ServerError      = 8,
    FxLogoutReason_LoginTimeout     = 9,
    FxLogoutReason_LoginDeleted     = 10,
    FxLogoutReason_ServerLogout     = 11,
    FxLogoutReason_MustChangePassword = 12,
    FxLogoutReason_Last         = FX_MAX_ENUM
};

enum FxTwoFactorReason
{
    FxTwoFactorReason_None = -1,
    FxTwoFactorReason_Unknown = 0,
    FxTwoFactorReason_ServerRequest = 1,
    FxTwoFactorReason_ServerSuccess = 2,
    FxTwoFactorReason_ServerError = 3,
    FxTwoFactorReason_ClientResponse = 4,
    FxTwoFactorReason_ClientResume = 5,
    FxTwoFactorReason_Last = FX_MAX_ENUM
};

enum FxTimeDirection
{
    FxTimeDirection_None    = 0,
    FxTimeDirection_Forward = 1,
    FxTimeDirection_Backward    = 2,
    FxTimeDirection_Last    = FX_MAX_ENUM
};

enum FxTradeTransactionReportType
{
    FxTradeTransactionReportType_None           = -1,
    FxTradeTransactionReportType_OrderOpened        = 0,
    FxTradeTransactionReportType_OrderCanceled      = 1,
    FxTradeTransactionReportType_OrderExpired       = 2,
    FxTradeTransactionReportType_OrderFilled        = 3,
    FxTradeTransactionReportType_PositionClosed     = 4,
    FxTradeTransactionReportType_BalanceTransaction = 5,
    FxTradeTransactionReportType_Credit         = 6,
    FxTradeTransactionReportType_PositionOpened     = 7,
    FxTradeTransactionReportType_OrderActivated     = 8,
    FxTradeTransactionReportType_Last           = FX_MAX_ENUM
};


enum FxTradeTransactionReason
{
    FxTradeTransactionReason_None           = -1,
    FxTradeTransactionReason_ClientRequest      = 0,
    FxTradeTransactionReason_PendingOrderActivation = 1,
    FxTradeTransactionReason_StopOut            = 2,
    FxTradeTransactionReason_StopLossActivation     = 3,
    FxTradeTransactionReason_TakeProfitActivation   = 4,
    FxTradeTransactionReason_DealerDecision     = 5,
    FxTradeTransactionReason_Rollover           = 6,
    FxTradeTransactionReason_DeleteAccount      = 7,
    FxTradeTransactionReason_Expired            = 8,
    FxTradeTransactionReason_TransferMoney      = 9,
    FxTradeTransactionReason_Last           = FX_MAX_ENUM
};


enum ProfitCalcMode
{
    FxProfitCalcMode_Forex      = 0,
    FxProfitCalcMode_Cfd        = 1,
    FxProfitCalcMode_Futures        = 2,
    FxProfitCalcMode_CfdIndex       = 3,
    FxProfitCalcMode_CfdLeverage    = 4
};

enum MarginCalcMode
{
    FxMarginCalcMode_Forex      = 0,
    FxMarginCalcMode_Cfd        = 1,
    FxMarginCalcMode_Futures        = 2,
    FxMarginCalcMode_CfdIndex       = 3,
    FxMarginCalcMode_CfdLeverage    = 4
};

enum SessionStatus
{
    SessionStatus_Open   = 2,
    SessionStatus_Closed = 3,
    SessionStatus_Last   = FX_MAX_ENUM
};

enum SwapType
{
    FxSwapType_Points           = 0,
    FxSwapType_PercentPerYear   = 1
};

enum FxThrottlingMethod
{
    FxThrottlingMethod_Login = 0,
    FxThrottlingMethod_TwoFactor = 1,
    FxThrottlingMethod_SessionInfo = 2,
    FxThrottlingMethod_Currencies = 3,
    FxThrottlingMethod_Symbols = 4,
    FxThrottlingMethod_Ticks = 5,
    FxThrottlingMethod_Level2 = 6,
    FxThrottlingMethod_Tickers = 7,
    FxThrottlingMethod_FeedSubscribe = 8,
    FxThrottlingMethod_QuoteHistory = 9,
    FxThrottlingMethod_QuoteHistoryCache = 10,
    FxThrottlingMethod_TradeSessionInfo = 11,
    FxThrottlingMethod_TradeServerInfo = 12,
    FxThrottlingMethod_Account = 13,
    FxThrottlingMethod_Assets = 14,
    FxThrottlingMethod_Positions = 15,
    FxThrottlingMethod_Trades = 16,
    FxThrottlingMethod_TradeCreate = 17,
    FxThrottlingMethod_TradeModify = 18,
    FxThrottlingMethod_TradeDelete = 19,
    FxThrottlingMethod_TradeHistory = 20,
    FxThrottlingMethod_DailyAccountSnapshots = 21,
    FxThrottlingMethod_UnknownMethod = 999
};

enum FxPosReportType
{
    FxPosReportType_Login = 0,
    FxPosReportType_Response = 1,
    FxPosReportType_Rollover = 2,
    FxPosReportType_CreatePosition = 3,
    FxPosReportType_ModifyPosition = 4,
    FxPosReportType_CancelPosition = 5,
    FxPosReportType_ClosePosition = 6,
    FxPosReportType_UNKNOWN = 999
};

typedef void* FxHandle;
typedef void* FxString;
typedef void* FxParams;
typedef void* FxArray;
typedef void* FxIterator;


typedef struct _FxError
{
    HRESULT Status;
    int32 Code;
    FxString Param;
    FxString Message;
} FxError;


typedef struct _FxLogon
{
    FxString ProtocolVersion;
} FxLogon;

typedef struct _FxTradeHistoryResponse
{
    int32 TradeReportsNumber;
    bool Result;
    bool Status;
    bool EndOfStream;
} FxTradeHistoryResponse;


typedef struct _FxTradeHistoryReport
{
} FxTradeHistoryReport;


// common functions
HRESULT     FxDelete(FxHandle handle);

#endif
