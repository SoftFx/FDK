
#' Gets the bars as requested
#' 
#' @param symbol Symbol looked
#' @param priceTypeStr Bid or Ask
#' @param barPeriodStr Values like: S1, S10, M1, M5, M15, M30, H1, H4, D1, W1, MN1 (default 'M1')
#' @param startTime Start of the time intervals  
#' @param endTime End of time interval. If startTime is not set, the bar count is taken from barCount variable
#' @param barCount Number of items of startTime is not set 
#' @export
ttBarsHistory <- function(symbol, 
     priceTypeStr="Bid", barPeriodStr = "M1", 
     startTime= ttTimeZero() , endTime = ttNow(),
     barCount = 10000
     ){
  symbolBars <- ComputeBarsRange(symbol, priceTypeStr, barPeriodStr, startTime, endTime, barCount)
  getBarsFrame(symbolBars)
}

#' Extracts bar array data as a full data frame
#' 
#' @param symbolBars Bars array variable
getBarsFrame <- function(symbolBars){
  
  high <- BarHighs(symbolBars)
  low <- BarLows(symbolBars)
  open <- BarOpens(symbolBars)
  close <- BarCloses(symbolBars)
  volume <- BarVolumes(symbolBars)
  from <- BarFroms(symbolBars)
  to <- BarTos(symbolBars)
  UnregisterVar(symbolBars)
  data.table(high, low, open, close, volume, from, to)
}


#' Gets the bars as requested
#' 
#' @param symbol Symbol looked
#' @param priceTypeStr Ask
#' @param barPeriodStr Values like: M1, H1
#' @param startTime Start of the time range
#' @param endTime End of the time range
#' @param barCount Items used
ComputeBarsRange <- function(symbol, 
      priceTypeStr, barPeriodStr, startTime, endTime, barCount) {
  rClr::clrCallStatic('RHost.FdkBars', 'ComputeBarsRangeTime', symbol, priceTypeStr, barPeriodStr, startTime, endTime, barCount)
}

#' Gets the bars' high  as requested
#' 
#' @param barsVar RHost variable that stores bar array
BarHighs <- function(barsVar) {
  rClr::clrCallStatic('RHost.FdkBars', 'BarHighs', barsVar)
}
#' Gets the bars' low as requested
#' 
#' @param barsVar RHost variable that stores bar array
BarLows <- function(barsVar) {
  rClr::clrCallStatic('RHost.FdkBars', 'BarLows', barsVar)
}
#' Gets the bars' open as requested
#' 
#' @param barsVar RHost variable that stores bar array
BarOpens <- function(barsVar) {
  rClr::clrCallStatic('RHost.FdkBars', 'BarOpens', barsVar)
}

#' Gets the bars' closed as requested
#' 
#' @param barsVar RHost variable that stores bar array
BarCloses <- function(barsVar) {
  rClr::clrCallStatic('RHost.FdkBars', 'BarCloses', barsVar)
}

#' Gets the bars' volume as requested
#' 
#' @param barsVar RHost variable that stores bar array
BarVolumes <- function(barsVar) {
  rClr::clrCallStatic('RHost.FdkBars', 'BarVolumes', barsVar)
}

#' Gets the bars' volume as requested
#' 
#' @param barsVar RHost variable that stores bar array
BarFroms <- function(barsVar) {
  rClr::clrCallStatic('RHost.FdkBars', 'BarFroms', barsVar)
}

#' Gets the bars' volume as requested
#' 
#' @param barsVar RHost variable that stores bar array
BarTos <- function(barsVar) {
  rClr::clrCallStatic('RHost.FdkBars', 'BarTos', barsVar)
}
