#' Gets the account trades
#' 
#' @export
ttTradesHistory <- function(){
  symInfo = GetTradeTransactionReportAll()
  GetTradeReportDataFrame(symInfo)
}

#' Get symbol field
#' @param tradeSide Trade side
#' @param tradeType Trade type
GetTradeReportTransactionReport <- function(tradeSide, tradeType) {
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradeTransactionReport', tradeSide, tradeType)
}

#' Get symbol field
#' @param tradeSide Trade side
#' @param tradeType Trade type
GetTradeTransactionReportAll <- function(tradeSide, tradeType) {
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradeTransactionReportAll')
}



GetTradeReportDataFrame <- function(symInfo)
{
  AgentCommission = GetTradeReportAgentCommission(symInfo)
  ClientId = GetTradeReportClientId(symInfo)
  CloseConversionRate = GetTradeReportCloseConversionRate(symInfo)
  InitialVolume = GetTradeReportInitialVolume(symInfo)
  Comment = GetTradeReportComment(symInfo)
  Commission = GetTradeReportCommission(symInfo)
  Id = GetTradeReportId(symInfo)
  LeavesQuantity = GetTradeReportLeavesQuantity(symInfo)
  OpenConversionRate = GetTradeReportOpenConversionRate(symInfo)
  OrderCreated = GetTradeReportOrderCreated(symInfo)
  OrderFillPrice = GetTradeReportOrderFillPrice(symInfo)
  OrderLastFillAmount = GetTradeReportOrderLastFillAmount(symInfo)
  OrderModified = GetTradeReportOrderModified(symInfo)
  PosOpenPrice = GetTradeReportPosOpenPrice(symInfo)
  PositionClosePrice = GetTradeReportPositionClosePrice(symInfo)
  PositionCloseRequestedPrice = GetTradeReportPositionCloseRequestedPrice(symInfo)
  PositionClosed = GetTradeReportPositionClosed(symInfo)
  PositionLastQuantity = GetTradeReportPositionLastQuantity(symInfo)
  PositionLeavesQuantity = GetTradeReportPositionLeavesQuantity(symInfo)
  PositionModified = GetTradeReportPositionModified(symInfo)
  PositionOpened = GetTradeReportPositionOpened(symInfo)
  PositionQuantity = GetTradeReportPositionQuantity(symInfo)
  Price = GetTradeReportPrice(symInfo)
  Quantity = GetTradeReportQuantity(symInfo)
  StopLoss = GetTradeReportStopLoss(symInfo)
  StopPrice = GetTradeReportStopPrice(symInfo)
  Swap = GetTradeReportSwap(symInfo)
  Symbol = GetTradeReportSymbol(symInfo)
  TakeProfit = GetTradeReportTakeProfit(symInfo)
  TradeRecordSide = GetTradeReportTradeRecordSide(symInfo)
  TradeRecordType = GetTradeReportTradeRecordType(symInfo)
  TradeTransactionReason = GetTradeReportTradeTransactionReason(symInfo)
  TradeTransactionReportType = GetTradeReportTradeTransactionReportType(symInfo)
  TransactionAmount = GetTradeReportTransactionAmount(symInfo)
  TransactionCurrency = GetTradeReportTransactionCurrency(symInfo)
  TransactionTime = GetTradeReportTransactionTime(symInfo)
  
  UnregisterVar(symInfo)
  
  data.table(AgentCommission, ClientId, CloseConversionRate, InitialVolume, Comment, Commission,
	Id, LeavesQuantity, OpenConversionRate, OrderCreated, OrderFillPrice, OrderLastFillAmount, OrderModified,
	PosOpenPrice, PositionClosePrice, PositionCloseRequestedPrice, PositionClosed,
	PositionLastQuantity, PositionLeavesQuantity, PositionModified, PositionOpened, PositionQuantity,
	Price, Quantity, StopLoss, Swap, Symbol, TakeProfit, TradeRecordSide, TradeRecordType, 
	TradeTransactionReason, TradeTransactionReportType, TransactionAmount, TransactionCurrency, TransactionTime
	)
}
#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportAccountBalance <- function(varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradeReportAccountBalance', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportAgentCommission <- function(varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradeAgentCommission', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportClientId <- function(varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradeClientId', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportCloseConversionRate <- function(varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradeCloseConversionRate', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportInitialVolume <- function(varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradeInitialVolume', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportComment <- function(varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradeComment', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportCommission <- function(varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradeCommission', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportId <- function(varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradeId', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportLeavesQuantity <- function(varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradeLeavesQuantity', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportOpenConversionRate <- function(varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradeOpenConversionRate', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportOrderCreated <- function(varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradeOrderCreated', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportOrderFillPrice <- function(varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradeOrderFillPrice', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportOrderLastFillAmount <- function(varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradeOrderLastFillAmount', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportOrderModified <- function(varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradeOrderModified', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportPosOpenPrice <- function(varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradePosOpenPrice', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportPositionClosePrice <- function(varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradePositionClosePrice', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportPositionCloseRequestedPrice <- function(varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradePositionCloseRequestedPrice', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportPositionClosed <- function(varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradePositionClosed', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportPositionId <- function(varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradePositionId', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportPositionLastQuantity <- function (varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradePositionLastQuantity', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportPositionLeavesQuantity <- function (varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradePositionLeavesQuantity', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportPositionModified <- function (varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradePositionModified', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportPositionOpened <- function (varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradePositionOpened', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportPositionQuantity <- function (varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradePositionQuantity', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportPrice <- function (varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradePrice', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportQuantity <- function (varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradeQuantity', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportStopLoss <- function (varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradeStopLoss', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportStopPrice <- function (varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradeStopPrice', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportSwap <- function (varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradeSwap', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportSymbol <- function (varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradeSymbol', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportTakeProfit <- function (varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradeTakeProfit', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportTradeRecordSide <- function (varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradeTradeRecordSide', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportTradeRecordType <- function (varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradeTradeRecordType', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportTradeTransactionReason <- function (varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradeTradeTransactionReason', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportTradeTransactionReportType <- function (varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradeTradeTransactionReportType', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportTransactionAmount <- function (varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradeTransactionAmount', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportTransactionCurrency <- function (varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradeTransactionCurrency', varName)
}

#' Get trade report field
#' @param varName RHost variable that stores the array
GetTradeReportTransactionTime <- function (varName)
{
  rClr::clrCallStatic('RHost.FdkTradeReports', 'GetTradeTransactionTime', varName)
}
