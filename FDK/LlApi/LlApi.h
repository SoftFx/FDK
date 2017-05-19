#ifndef __Native_F_X_Open_Api__
#define __Native_F_X_Open_Api__

#ifdef LLAPI_EXPORTS
#define LLAPI_API EXPORT_API
#else
#define LLAPI_API IMPORT_API
#endif


// types

typedef void* FxDataClient;
typedef void* FxDataFeed;
typedef void* FxDataTrade;
typedef void* FxQueue;
typedef void* FxIterator;


// parameter constants

#define FX_TRADING_PLATFORM_ADDRESS 0
#define FX_TRADING_PLATFORM_PORT 1
#define FX_FIX_VERSION 2
#define FX_SENDER_COMP_ID 3
#define FX_TARGET_COMP_ID 4
#define FX_USERNAME 5
#define FX_PASSWORD 6
#define FX_LOG_DIRECTORY 7
#define FX_SYNCH_OPERATION_TIMEOUT 8
#define FX_SECURE_CONNECTION 9
#define FX_CACHE_MODE 10

// message constants
#define FX_MSG_LOGON 0
#define FX_MSG_LOGOUT 1
#define FX_MSG_TICK 2
#define FX_MSG_SESSION_INFO 3
#define FX_MSG_CACHE_UPDATED 4
#define FX_MSG_ACCOUNT_INFO 5
#define FX_MSG_SYMBOL_INFO 6
#define FX_MSG_EXECUTION_REPORT 7
#define FX_MSG_TRADE_TRANSACTION_REPORT 8
#define FX_MSG_UPDATE_TRADE_RECORD 9
#define FX_MSG_POSITION_REPORT 10
#define FX_MSG_NOTIFICATION 11
#define FX_MSG_QUOTES_HISTORY_RESPONSE 12
#define FX_MSG_CURRENCY_INFO 13
#define FX_MSG_TWO_FACTOR_AUTH 14
#define FX_MSG_SUBSCRIBED 15
#define FX_MSG_UNSUBSCRIBED 16

#endif
