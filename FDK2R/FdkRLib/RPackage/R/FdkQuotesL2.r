
#' Gets the quotes history
#' 
#' @param symbol Symbol looked
#' @param startTime Starting time. Use ttGetEpochFromText if you want to take from text a valid date.
#' @param endTime Ending time. Use ttGetEpochFromText if you want to take from text a valid date.
#' @param depth The market depth. Default is 2
#' @export
ttQuotesLevel2History <- function(symbol,startTime, endTime, depth = 1){
  quotesHistory <- GetQuotePacked(symbol,startTime, endTime, depth)
  
  createTime <- QuotesL2CreatingTime(quotesHistory)
  volumeBid <- QuotesVolumeBid(quotesHistory)
  volumeAsk <- QuotesVolumeAsk(quotesHistory)
  priceBid <- QuotesPriceBid(quotesHistory)
  priceAsk <- QuotesPriceAsk(quotesHistory)
  index <- QuotesIndex(quotesHistory)
  level <- QuotesLevel(quotesHistory)
  
  UnregisterVar(quotesHistory)
  data.table(volumeBid=volumeBid, volumeAsk=volumeAsk, 
         priceBid=priceBid, priceAsk=priceAsk, createTime=createTime,
         quoteIndex=index, level=level)
}


# ****
#' Gets the bars as requested
#' 
#' @param symbol Symbol looked
#' @param startTime Starting time. Use ttGetEpochFromText if you want to take from text a valid date.
#' @param endTime Ending time. Use ttGetEpochFromText if you want to take from text a valid date.
#' @param depth Quotes depth
GetQuotePacked <- function(symbol, startTime, endTime, depth) {
  rClr::clrCallStatic('RHost.FdkLevel2', 'GetQuotePacked', symbol, startTime, endTime, depth)
}

#' Gets the bars' time
#'
#' @param quotesVar RHost variable that stores quotes array
#' 
QuotesL2CreatingTime <- function(quotesVar) {
  rClr::clrCallStatic('RHost.FdkLevel2', 'QuotesCreateTime', quotesVar)
}

#' Gets the quote's bid as requested
#' 
#' @param quotesVar RHost variable that stores quotes array
QuotesVolumeBid <- function(quotesVar) {
  rClr::clrCallStatic('RHost.FdkLevel2', 'QuotesVolumeBid', quotesVar)
}

#' Gets the quote's ask as requested
#' 
#' @param quotesVar RHost variable that stores quotes array
QuotesVolumeAsk <- function(quotesVar) {
  rClr::clrCallStatic('RHost.FdkLevel2', 'QuotesVolumeAsk', quotesVar)
}
#' Gets the quote's bid as requested
#' 
#' @param quotesVar RHost variable that stores quotes array
QuotesPriceBid <- function(quotesVar) {
  rClr::clrCallStatic('RHost.FdkLevel2', 'QuotesPriceBid', quotesVar)
}

#' Gets the bars' ask as requested
#' 
#' @param quotesVar RHost variable that stores quotes array
QuotesPriceAsk <- function(quotesVar) {
  rClr::clrCallStatic('RHost.FdkLevel2', 'QuotesPriceAsk', quotesVar)
}

#' Gets the quote's index
#' 
#' @param quotesVar RHost variable that stores quotes array
QuotesIndex <- function(quotesVar) {
  rClr::clrCallStatic('RHost.FdkLevel2', 'QuotesIndex', quotesVar)
}

#' Gets the quote's ask as requested
#' 
#' @param quotesVar RHost variable that stores quotes array
QuotesLevel <- function(quotesVar) {
  rClr::clrCallStatic('RHost.FdkLevel2', 'QuotesLevel', quotesVar)
}


