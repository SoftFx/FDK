
#' Gets the bars' low as requested
#' 
#' @param symbol Symbol looked
#' @param barPeriodStr (default 'M1') values like: S1, S10, M1, M5, M15, M30, H1, H4, D1, W1, MN1
#' @param startTime R time as start of interval
#' @param endTime R time as end of interval
#' @param barCountDbl Bar count
#' @export
ttBarsQuotesHistory <- function(symbol, barPeriodStr = "M1",
     startTime = ttTimeZero(),  endTime = ttNow(),
     barCountDbl = 10000){
  bars = ComputeGetPairBars(symbol, barPeriodStr, startTime, endTime, barCountDbl)
  
  getBarPairFrame(bars)
}

#' Extracts bar pair array data as a full data frame
#' 
#' @param bars Bars array variable
getBarPairFrame <- function (bars){
  askHigh = GetBarsAskHigh(bars)
  askLow = GetBarsAskLow(bars)
  askopen = GetBarsAskOpen(bars)
  askClose = GetBarsAskClose(bars)
  askVolume = GetBarsAskVolume(bars)
  
  bidHigh = GetBarsBidHigh(bars)
  bidLow = GetBarsBidLow(bars)
  bidOpen = GetBarsBidOpen(bars)
  bidClose = GetBarsBidClose(bars)
  bidVolume = GetBarsBidVolume(bars)
  
  
  from = GetBarsAskFrom(bars)
  to = GetBarsAskTo(bars)
  
  UnregisterVar(bars)
  data.table(askHigh, askLow, askopen, askClose, askVolume, 
             bidHigh, bidLow, bidOpen, bidClose, bidVolume, 
             from, to)
}

#' Gets the bars pairs as requested
#' 
#' @param symbol Symbol looked
#' @param barPeriodStr (default 'M1') values like: S1, S10, M1, M5, M15, M30, H1, H4, D1, W1, MN1
#' @param startTime R time as start of interval
#' @param endTime R time as end of interval
#' @param barCount Bar count
ComputeGetPairBars <- function(symbol, barPeriodStr, startTime, endTime, barCount) {
  rClr::clrCallStatic('RHost.FdkBarPairs', 'ComputeGetPairBars', symbol, barPeriodStr, startTime, endTime, barCount)
}

#' Gets the bars' ask as requested
#' 
#' @param barsPairVar RHost variable that stores quotes array
GetBarsAskHigh <- function(barsPairVar) {
  rClr::clrCallStatic('RHost.FdkBarPairs', 'GetBarsAskHigh', barsPairVar)
}

#' Gets the bars' ask as requested
#' 
#' @param barsPairVar RHost variable that stores quotes array
GetBarsAskLow <- function(barsPairVar) {
  rClr::clrCallStatic('RHost.FdkBarPairs', 'GetBarsAskLow', barsPairVar)
}

#' Gets the bars' ask as requested
#' 
#' @param barsPairVar RHost variable that stores quotes array
GetBarsAskOpen <- function(barsPairVar) {
  rClr::clrCallStatic('RHost.FdkBarPairs', 'GetBarsAskOpen', barsPairVar)
}

#' Gets the bars' ask as requested
#' 
#' @param barsPairVar RHost variable that stores quotes array
GetBarsAskClose <- function(barsPairVar) {
  rClr::clrCallStatic('RHost.FdkBarPairs', 'GetBarsAskClose', barsPairVar)
}

#' Gets the bars' ask as requested
#' 
#' @param barsPairVar RHost variable that stores quotes array
GetBarsAskVolume <- function(barsPairVar) {
  rClr::clrCallStatic('RHost.FdkBarPairs', 'GetBarsAskVolume', barsPairVar)
}

#' Gets the bars' ask as requested
#' 
#' @param barsPairVar RHost variable that stores quotes array
GetBarsAskFrom <- function(barsPairVar) {
  rClr::clrCallStatic('RHost.FdkBarPairs', 'GetBarsAskFrom', barsPairVar)
}

#' Gets the bars' ask as requested
#' 
#' @param barsPairVar RHost variable that stores quotes array
GetBarsAskTo <- function(barsPairVar) {
  rClr::clrCallStatic('RHost.FdkBarPairs', 'GetBarsAskTo', barsPairVar)
}

#' Gets the bars' ask as requested
#' 
#' @param barsPairVar RHost variable that stores quotes array
GetBarsBidHigh <- function(barsPairVar) {
  rClr::clrCallStatic('RHost.FdkBarPairs', 'GetBarsBidHigh', barsPairVar)
}

#' Gets the bars' ask as requested
#' 
#' @param barsPairVar RHost variable that stores quotes array
GetBarsBidLow <- function(barsPairVar) {
  rClr::clrCallStatic('RHost.FdkBarPairs', 'GetBarsBidLow', barsPairVar)
}

#' Gets the bars' ask as requested
#' 
#' @param barsPairVar RHost variable that stores quotes array
GetBarsBidOpen <- function(barsPairVar) {
  rClr::clrCallStatic('RHost.FdkBarPairs', 'GetBarsBidOpen', barsPairVar)
}

#' Gets the bars' ask as requested
#' 
#' @param barsPairVar RHost variable that stores quotes array
GetBarsBidClose <- function(barsPairVar) {
  rClr::clrCallStatic('RHost.FdkBarPairs', 'GetBarsBidClose', barsPairVar)
}

#' Gets the bars' ask as requested
#' 
#' @param barsPairVar RHost variable that stores quotes array
GetBarsBidVolume <- function(barsPairVar) {
  rClr::clrCallStatic('RHost.FdkBarPairs', 'GetBarsBidVolume', barsPairVar)
}

#' Gets the bars' ask as requested
#' 
#' @param barsPairVar RHost variable that stores quotes array
GetBarsBidFrom <- function(barsPairVar) {
  rClr::clrCallStatic('RHost.FdkBarPairs', 'GetBarsBidFrom', barsPairVar)
}

#' Gets the bars' ask as requested
#' 
#' @param barsPairVar RHost variable that stores quotes array
GetBarsBidTo <- function(barsPairVar) {
  rClr::clrCallStatic('RHost.FdkBarPairs', 'GetBarsBidTo', barsPairVar)
}
