#ifndef __FixProviderstdafx__
#define __FixProviderstdafx__
// or project specific include files that are used frequently, but
// are changed infrequently
//


#ifdef _MSC_VER
#ifdef _DEBUG
#define _CRTDBG_MAP_ALLOC
#endif
#endif

#ifndef _MSC_VER
#pragma GCC diagnostic ignored "-Wdeprecated-declarations"
#else
#pragma warning (disable : 4290)
#endif

#include <string>
#include <map>
#include <stack>
#include <vector>
#include <numeric>
#include <set>
#include <queue>
#include <iostream>
#include <fstream>
#include <assert.h>
#include <memory.h>
#include <memory>
#include <exception>
#include <limits>
#include <sstream>
#include <regex>
#include <atlbase.h>



using namespace std;

#define FX_OVERRIDE_WINSOCKS
#include "../Sal/Sal.h"


#define HAVE_CUSTOM_DATE_TIME
#include "../../External/Include/lrp/Lrp.Core.h"


#pragma warning (push)
#pragma warning (disable : 4251)
#include "../Core/Core.h"
#include "../QuickFix/FixMessage.h"
#pragma warning (pop)
// TODO: reference additional headers your program requires here

#pragma warning (push, 3)

#include "../QuickFix/Fields.h"
#include "../QuickFix/Values.h"
#include "../QuickFix/Application.h"
#include "../QuickFix/SocketInitiator.h"
#include "../QuickFix/FileLog.h"
#include "../QuickFix/DictionariesManager.h"
#include "../QuickFix/NullStore.h"

#include "../QuickFix/fix44/Logon.h"
#include "../QuickFix/FileStore.h"
#include "../QuickFix/fix44/Logon.h"
#include "../QuickFix/fix44/Logout.h"
#include "../QuickFix/fix44/SecurityListRequest.h"
#include "../QuickFix/fix44/SecurityList.h"
#include "../QuickFix/fix44/CurrencyList.h"
#include "../QuickFix/fix44/CurrencyListRequest.h"
#include "../QuickFix/fix44/MarketDataRequest.h"
#include "../QuickFix/fix44/MarketDataRequestReject.h"
#include "../QuickFix/fix44/MarketDataSnapshotFullRefresh.h"
#include "../QuickFix/fix44/NewOrderSingle.h"
#include "../QuickFix/fix44/ExecutionReport.h"
#include "../QuickFix/fix44/TradingSessionStatusRequest.h"
#include "../QuickFix/fix44/TradingSessionStatus.h"
#include "../QuickFix/fix44/OrderMassStatusRequest.h"
#include "../QuickFix/fix44/MarketDataRequestAck.h"
#include "../QuickFix/fix44/OrderCancelRequest.h"
#include "../QuickFix/fix44/OrderCancelReject.h"
#include "../QuickFix/fix44/AccountInfoRequest.h"
#include "../QuickFix/fix44/AccountInfo.h"
#include "../QuickFix/fix44/ClosePositionRequest.h"
#include "../QuickFix/fix44/ClosePositionRequestAck.h"
#include "../QuickFix/fix44/OrderCancelReplaceRequest.h"
#include "../QuickFix/fix44/MarketDataHistoryRequest.h"
#include "../QuickFix/fix44/MarketDataHistory.h"
#include "../QuickFix/fix44/MarketDataHistoryRequestReject.h"
#include "../QuickFix/fix44/TradeCaptureReportRequest.h"
#include "../QuickFix/fix44/TradeCaptureReportRequestAck.h"
#include "../QuickFix/fix44/TradeCaptureReport.h"
#include "../QuickFix/fix44/FileChunkReq.h"
#include "../QuickFix/fix44/FileChunk.h"
#include "../QuickFix/fix44/FileChunkReqReject.h"
#include "../QuickFix/fix44/MarketDataHistoryMetadataRequest.h"
#include "../QuickFix/fix44/MarketDataHistoryMetadataReport.h"
#include "../QuickFix/fix44/BusinessMessageReject.h"
#include "../QuickFix/fix44/TradeCaptureReportRequest.h"
#include "../QuickFix/fix44/TradeCaptureReportRequestAck.h"
#include "../QuickFix/fix44/TradeTransactionReport.h"
#include "../QuickFix/fix44/TradeTransactionReportRequest.h"
#include "../QuickFix/fix44/TradeTransactionReportRequestAck.h"
#include "../QuickFix/fix44/PositionReport.h"
#include "../QuickFix/fix44/RequestForPositions.h"
#include "../QuickFix/fix44/RequestForPositionsAck.h"
#include "../QuickFix/fix44/Notification.h"
#include "../QuickFix/fix44/ComponentsInfoRequest.h"
#include "../QuickFix/fix44/ComponentsInfoReport.h"
#include "../QuickFix/fix44/TwoFactorLogon.h"
#include "../QuickFix/fix44/TradeServerInfoRequest.h"
#include "../QuickFix/fix44/TradeServerInfoReport.h"
#pragma warning (pop)
using namespace FIX;




#ifdef _MSC_VER
#ifdef _DEBUG
#include <crtdbg.h>
#define new new (_CLIENT_BLOCK, __FILE__, __LINE__)
#endif
#endif


#endif
