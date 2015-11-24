#' Gets the account trades
#' 
#' @export
ttTrades <- function(){
  symInfo = GetTradeHistory()
  
  agentComission = GetTradeAgentCommission(symInfo)
  tradeClientOrderId = GetTradeClientOrderId(symInfo)
  tradeComment = GetTradeComment(symInfo)
  created = GetTradeCreated(symInfo)
  expiration = GetTradeExpiration(symInfo)
  initialVolume = GetTradeInitialVolume(symInfo)
  symbol = GetTradeSymbol(symInfo)
  
  isLimitOrder = GetTradeIsLimitOrder(symInfo)
  isPendingOrder = GetTradeIsPendingOrder(symInfo)
  isPosition = GetTradeIsPosition(symInfo)
  isStopOrder = GetTradeIsStopOrder(symInfo)
  
  modified = GetTradeModified(symInfo)
  orderId = GetTradeOrderId(symInfo)
  price = GetTradePrice(symInfo)
  profit = GetTradeProfit(symInfo)
  side = GetTradeSide(symInfo)
  stopLoss = GetTradeStopLoss(symInfo)
  swap = GetTradeSwap(symInfo)
  takeProfit = GetTradeTakeProfit(symInfo)
  type = GetTradeType(symInfo)
  volume = GetTradeVolume(symInfo)
  
  UnregisterVar(symInfo)
  
  data.table(agentComission, tradeClientOrderId, tradeComment, created,
             expiration, symbol, initialVolume, isLimitOrder, isPendingOrder,
             isPosition, isStopOrder, modified, orderId, price, profit, 
             side, stopLoss, swap, takeProfit, type, volume)
}
#' Get trade history
GetTradeHistory <- function() {
  rClr::clrCallStatic('RHost.FdkTrade', 'GetTradeHistory')
}

#' Get trade comission
#' @param symInfo RHost variable that stores the array
GetTradeAgentCommission <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkTrade', 'GetTradeAgentCommission', symInfo)
}

#' Get trade trade client order id
#' @param symInfo RHost variable that stores the array
GetTradeClientOrderId <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkTrade', 'GetTradeClientOrderId', symInfo)
}

#' Get trade comment
#' @param symInfo RHost variable that stores the array
GetTradeComment <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkTrade', 'GetTradeComment', symInfo)
}

#' Get trade create time
#' @param symInfo RHost variable that stores the array
GetTradeCreated <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkTrade', 'GetTradeCreated', symInfo)
}

#' Get trade expiration date
#' @param symInfo RHost variable that stores the array
GetTradeExpiration <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkTrade', 'GetTradeExpiration', symInfo)
}
#' Get trade initial volume
#' @param symInfo RHost variable that stores the array
GetTradeInitialVolume <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkTrade', 'GetTradeInitialVolume', symInfo)
}
#' Get trade commission
#' @param symInfo RHost variable that stores the array
GetTradeIsLimitOrder <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkTrade', 'GetTradeIsLimitOrder', symInfo)
}

#' Get trade comission
#' @param symInfo RHost variable that stores the array
GetTradeIsPendingOrder <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkTrade', 'GetTradeIsPendingOrder', symInfo)
}

#' Get trade comission
#' @param symInfo RHost variable that stores the array
GetTradeIsPosition <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkTrade', 'GetTradeIsPosition', symInfo)
}
#' Get trade comission
#' @param symInfo RHost variable that stores the array
GetTradeIsStopOrder <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkTrade', 'GetTradeIsStopOrder', symInfo)
}

#' Get trade comission
#' @param symInfo RHost variable that stores the array
GetTradeModified <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkTrade', 'GetTradeModified', symInfo)
}
#' Get trade comission
#' @param symInfo RHost variable that stores the array
GetTradeOrderId <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkTrade', 'GetTradeOrderId', symInfo)
}
#' Get trade comission
#' @param symInfo RHost variable that stores the array
GetTradePrice <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkTrade', 'GetTradePrice', symInfo)
}
#' Get trade comission
#' @param symInfo RHost variable that stores the array
GetTradeProfit <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkTrade', 'GetTradeProfit', symInfo)
}
#' Get trade comission
#' @param symInfo RHost variable that stores the array
GetTradeSide <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkTrade', 'GetTradeSide', symInfo)
}
#' Get trade comission
#' @param symInfo RHost variable that stores the array
GetTradeStopLoss <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkTrade', 'GetTradeStopLoss', symInfo)
}
#' Get trade comission
#' @param symInfo RHost variable that stores the array
GetTradeSwap <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkTrade', 'GetTradeSwap', symInfo)
}
#' Get trade comission
#' @param symInfo RHost variable that stores the array
GetTradeAgentCommission <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkTrade', 'GetTradeAgentCommission', symInfo)
}

#' Get trade comission
#' @param symInfo RHost variable that stores the array
GetTradeTakeProfit <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkTrade', 'GetTradeTakeProfit', symInfo)
}
#' Get trade comission
#' @param symInfo RHost variable that stores the array
GetTradeType <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkTrade', 'GetTradeType', symInfo)
}
#' Get trade comission
#' @param symInfo RHost variable that stores the array
GetTradeVolume <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkTrade', 'GetTradeVolume', symInfo)
}
#' Get trade comission
#' @param symInfo RHost variable that stores the array
GetTradeSymbol <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkTrade', 'GetTradeSymbol', symInfo)
}
#' Get trade comission
#' @param symInfo RHost variable that stores the array
GetTradeAgentCommission <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkTrade', 'GetTradeAgentCommission', symInfo)
}