#' Gets the symbol info
#' 
#' @export
ttGetCurrencyData <- function(){
  symInfo = GetCurrencyInfos()
  
  currency = GetCurrencyName(symInfo)
  description = GetCurrencyDescription(symInfo)
  precision = GetCurrencyPrecision(symInfo)
  sortOrder = GetCurrencySortOrder(symInfo)
  
  UnregisterVar(symInfo)
  
  data.table(currency, description, precision, sortOrder)
}
#' Get symbol field
GetCurrencyInfos <- function() {
  rClr::clrCallStatic('RHost.FdkCurrencyInfo', 'GetCurrencyInfos')
}
#' Get symbol field
#' @param symInfo RHost variable that stores the array
GetCurrencyName <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkCurrencyInfo', 'GetCurrencyName', symInfo)
}
#' Get symbol field
#' @param symInfo RHost variable that stores the array
GetCurrencyDescription <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkCurrencyInfo', 'GetCurrencyDescription', symInfo)
}

#' Get symbol field
#' @param symInfo RHost variable that stores the array
GetCurrencyPrecision <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkCurrencyInfo', 'GetCurrencyPrecision', symInfo)
}
#' Get symbol field
#' @param symInfo RHost variable that stores the array
GetCurrencySortOrder <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkCurrencyInfo', 'GetCurrencySortOrder', symInfo)
}