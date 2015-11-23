
#' Gets the quotes history
#' 
#' @param symbol Symbol looked
#' @param startTime Starting time. Use ttGetEpochFromText if you want to take from text a valid date.
#' @param endTime Ending time. Use ttGetEpochFromText if you want to take from text a valid date.
#' @param depth Quotes depth
#' @export
ttQuotesHistory <- function(symbol,startTime= ttTimeZero() , endTime, depth=1){
  quotesHistory <- ComputeQuoteHistory(symbol,startTime, endTime, depth)
  
  ask <- QuotesAsk(quotesHistory)
  bid <- QuotesBid(quotesHistory)
  createTime <- QuotesCreatingTime(quotesHistory)
  
  UnregisterVar(quotesHistory)
  data.table(ask=ask, bid=bid, createTime=createTime)       
}

# ****
#' Gets the bars as requested
#' 
#' @param symbol Symbol looked
#' @param startTime Starting time. Use ttGetEpochFromText if you want to take from text a valid date.
#' @param endTime Ending time. Use ttGetEpochFromText if you want to take from text a valid date.
#' @param depth Quotes depth
ComputeQuoteHistory <- function(symbol, startTime, endTime, depth) {
  rClr::clrCallStatic('RHost.FdkQuotes', 'ComputeQuoteHistory', symbol, startTime, endTime, depth)
}

#' Gets the bars' ask as requested
#' 
#' @param quotesVar RHost variable that stores quotes array
QuotesAsk <- function(quotesVar) {
  rClr::clrCallStatic('RHost.FdkQuotes', 'QuotesAsk', quotesVar)
}
#' Gets the bars' ask as requested
#' 
#' @param quotesVar RHost variable that stores quotes array
QuotesBid <- function(quotesVar) {
  rClr::clrCallStatic('RHost.FdkQuotes', 'QuotesBid', quotesVar)
}
#' Gets the bars' ask as requested
#' 
#' @param quotesVar RHost variable that stores quotes array
QuotesCreatingTime <- function(quotesVar) {
  rClr::clrCallStatic('RHost.FdkQuotes', 'QuotesCreatingTime', quotesVar)
}
#' Gets the quotes' spread as requested
#' 
#' @param quotesVar RHost variable that stores quotes array
QuotesSpread <- function(quotesVar) {
  rClr::clrCallStatic('RHost.FdkQuotes', 'QuotesSpread', quotesVar)
}

#' Gets the bars' volume as requested
#' 
#' @param quotesVar RHost variable that stores quotes array
QuotesVolume <- function(quotesVar) {
  rClr::clrCallStatic('RHost.FdkQuotes', 'QuotesVolume', quotesVar)
}