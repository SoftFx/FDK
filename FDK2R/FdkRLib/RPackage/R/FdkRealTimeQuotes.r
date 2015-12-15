#' Monitor a symbol
#' 
#' @param symbol Symbol looked
#' @param level Quote level
#' @export
ttQuotesSubscribe <- function(symbol, level){
  quotesHistory <- RealTimeComputeQuoteHistory(symbol, level)
}

#' Get a snapshot of current monitoring status
#' 
#' @param idMonitoring Id of monitoring session
#' @export
ttQuotesLevel2 <- function(idMonitoring){
  snapshot <- SnapshotMonitoredSymbol(idMonitoring)
  
  bidPrice <- RealTimeQuotesBidPrice(snapshot)
  bidVolume <- RealTimeQuotesBidVolume(snapshot)
  askPrice <- RealTimeQuotesAskPrice(snapshot)
  askVolume <- RealTimeQuotesAskVolume(snapshot)
  createTime <- RealTimeQuotesReceivingTime(snapshot)

  UnregisterVar(snapshot)
  
  data.table(bidPrice=bidPrice, bidVolume=bidVolume, 
                  askPrice=askPrice, askVolume=askVolume, 
                  createTime = createTime)
}

#' Stop monitoring a symbol
#' 
#' @param idMonitoring Id of monitoring session
#' @export
ttQuotesUnsubscribe <- function(idMonitoring){
  RealTimeRemoveEvent(idMonitoring)
}

# ****
#' Gets the quotes as requested
#' 
#' @param symbol Symbol looked
#' @param level Level of the quote
RealTimeComputeQuoteHistory <- function(symbol, level) {
  rClr::clrCallStatic('RHost.FdkRealTime', 'MonitorSymbol', symbol, level)
}

# ****
#' Gets the a snapshot based on the monitoring Id
#' 
#' @param idMonitoring Id of monitoring session
SnapshotMonitoredSymbol <- function(idMonitoring) {
  rClr::clrCallStatic('RHost.FdkRealTime', 'SnapshotMonitoredSymbol', idMonitoring)
}


# ****
#' Remove monitoring id
#' 
#' @param idMonitoring Id of monitoring session
RealTimeRemoveEvent <- function(idMonitoring) {
  rClr::clrCallStatic('RHost.FdkRealTime', 'RemoveEvent', idMonitoring)
}

#' Gets the bars' ask as requested
#' 
#' @param quotesVar RHost variable that stores quotes array
RealTimeQuotesAsk <- function(quotesVar) {
  rClr::clrCallStatic('RHost.FdkRealTime', 'QuoteArrayAsk', quotesVar)
}
#' Gets the bars' ask as requested
#' 
#' @param quotesVar RHost variable that stores quotes array
RealTimeQuotesBidPrice <- function(quotesVar) {
  rClr::clrCallStatic('RHost.FdkRealTime', 'QuoteRealTimeBidPrice', quotesVar)
}
#' Gets the bars' ask as requested
#' 
#' @param quotesVar RHost variable that stores quotes array
RealTimeQuotesBidVolume <- function(quotesVar) {
  rClr::clrCallStatic('RHost.FdkRealTime', 'QuoteRealTimeBidVolume', quotesVar)
}
#' Gets the bars' ask as requested
#' 
#' @param quotesVar RHost variable that stores quotes array
RealTimeQuotesAskPrice <- function(quotesVar) {
  rClr::clrCallStatic('RHost.FdkRealTime', 'QuoteRealTimeAskPrice', quotesVar)
}

#' Gets the bars' ask as requested
#' 
#' @param quotesVar RHost variable that stores quotes array
RealTimeQuotesAskVolume <- function(quotesVar) {
  rClr::clrCallStatic('RHost.FdkRealTime', 'QuoteRealTimeAskVolume', quotesVar)
}
#' Gets the bars' ask as requested
#' 
#' @param quotesVar RHost variable that stores quotes array
RealTimeQuotesReceivingTime <- function(quotesVar) {
  rClr::clrCallStatic('RHost.FdkRealTime', 'QuoteRealTimeReceivingTime', quotesVar)
}
