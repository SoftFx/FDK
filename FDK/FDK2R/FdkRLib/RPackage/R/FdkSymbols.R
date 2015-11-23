#' Gets the symbol info
#' 
#' @export
ttGetSymbolData <- function(){
  symInfo = GetSymbolInfos()
  comission = GetSymbolComission(symInfo)
  # contractMultiplier = GetSymbolContractMultiplier(symInfo)
  marginCurrency = GetSymbolCurrency(symInfo)
  limitsComission = GetSymbolLimitsCommission(symInfo)
  maxTradeVolume = GetSymbolMaxTradeVolume(symInfo)
  minTradeVolume = GetSymbolMinTradeVolume(symInfo)
  name = GetSymbolName(symInfo)
  precision = GetSymbolPrecision(symInfo)
  profitCurrency = GetSymbolSettlementCurrency(symInfo)
  swapLong = GetSymbolSwapSizeLong(symInfo)
  swapShort = GetSymbolSwapSizeShort(symInfo)
  pipsValue = GetSymbolPipsValue(symInfo)
  priceBid = GetSymbolCurrentPriceBid(symInfo)
  
  UnregisterVar(symInfo)
  
  data.table(
    name, precision, 
    comission, marginCurrency, profitCurrency,
    limitsComission,
    minTradeVolume, maxTradeVolume, 
    swapLong, swapShort,
	pipsValue, priceBid
    )
}
#' Get symbol field
GetSymbolInfos <- function() {
  rClr::clrCallStatic('RHost.FdkSymbolInfo', 'GetSymbolInfos')
}
#' Get symbol field
#' @param symInfo RHost variable that stores the array
GetSymbolComission <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkSymbolInfo', 'GetSymbolComission', symInfo)
}
#' Get symbol field
#' @param symInfo RHost variable that stores the array
GetSymbolContractMultiplier <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkSymbolInfo', 'GetSymbolContractMultiplier', symInfo)
}
#' Get symbol field
#' @param symInfo RHost variable that stores the array
GetSymbolCurrency <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkSymbolInfo', 'GetSymbolCurrency', symInfo)
}
#' Get symbol field
#' @param symInfo RHost variable that stores the array
GetSymbolLimitsCommission <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkSymbolInfo', 'GetSymbolLimitsCommission', symInfo)
}
#' Get symbol field
#' @param symInfo RHost variable that stores the array
GetSymbolMaxTradeVolume <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkSymbolInfo', 'GetSymbolMaxTradeVolume', symInfo)
}
#' Get symbol field
#' @param symInfo RHost variable that stores the array
GetSymbolMinTradeVolume <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkSymbolInfo', 'GetSymbolMinTradeVolume', symInfo)
}
#' Get symbol field
#' @param symInfo RHost variable that stores the array
GetSymbolName <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkSymbolInfo', 'GetSymbolName', symInfo)
}
#' Get symbol field
#' @param symInfo RHost variable that stores the array
GetSymbolPrecision <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkSymbolInfo', 'GetSymbolPrecision', symInfo)
}
#' Get symbol field
#' @param symInfo RHost variable that stores the array
GetSymbolSettlementCurrency <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkSymbolInfo', 'GetSymbolSettlementCurrency', symInfo)
}
#' Get symbol field
#' @param symInfo RHost variable that stores the array
GetSymbolSwapSizeLong <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkSymbolInfo', 'GetSymbolSwapSizeLong', symInfo)
}
#' Get symbol field
#' @param symInfo RHost variable that stores the array
GetSymbolSwapSizeShort <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkSymbolInfo', 'GetSymbolSwapSizeShort', symInfo)
}

#' Get symbol field
#' @param symInfo RHost variable that stores the array
GetSymbolPipsValue <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkSymbolInfo', 'GetSymbolPipsValue', symInfo)
}
#' Get symbol field
#' @param symInfo RHost variable that stores the array
GetSymbolCurrentPriceBid <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkSymbolInfo', 'GetSymbolCurrentPriceBid', symInfo)
}

