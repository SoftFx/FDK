#' Gets the session fields
#' 
#' @export
ttSessionInfo <- function(){
  varName = GetSessionInfo()
  
  platformCompany = PlatformCompany(varName)
  platformName = PlatformName(varName)
  tradingSessionId = TradingSessionId(varName)
  closeTime = CloseTime(varName)
  endTime = EndTime(varName)
  openTime = OpenTime(varName)
  startTime = StartTime(varName)
  serverTimeZoneOffset = ServerTimeZoneOffset(varName)
  status = Status(varName)
  
  UnregisterVar(varName)
  data.table(platformCompany, platformName, tradingSessionId, 
	startTime, endTime, openTime, closeTime, 
	serverTimeZoneOffset, status)      
}

# ****
#' Gets the bars as requested
#' 
GetSessionInfo <- function() {
  rClr::clrCallStatic('RHost.FdkSessionInfo', 'GetSessionInfo')
}

PlatformCompany <- function(varName)
{
 rClr::clrCallStatic('RHost.FdkSessionInfo', 'PlatformCompany', varName)
}

PlatformName <- function(varName)
{
 rClr::clrCallStatic('RHost.FdkSessionInfo', 'PlatformName', varName)
}

TradingSessionId <- function(varName)
{
 rClr::clrCallStatic('RHost.FdkSessionInfo', 'TradingSessionId', varName)
}

CloseTime <- function(varName)
{
 rClr::clrCallStatic('RHost.FdkSessionInfo', 'CloseTime', varName)
}

EndTime <- function(varName)
{
 rClr::clrCallStatic('RHost.FdkSessionInfo', 'EndTime', varName)
}

OpenTime <- function(varName)
{
 rClr::clrCallStatic('RHost.FdkSessionInfo', 'OpenTime', varName)
}

StartTime <- function(varName)
{
 rClr::clrCallStatic('RHost.FdkSessionInfo', 'StartTime', varName)
}

IsClosed <- function(varName)
{
 rClr::clrCallStatic('RHost.FdkSessionInfo', 'IsClosed', varName)
}

ServerTimeZoneOffset <- function(varName)
{
 rClr::clrCallStatic('RHost.FdkSessionInfo', 'ServerTimeZoneOffset', varName)
}

Status <- function(varName)
{
 rClr::clrCallStatic('RHost.FdkSessionInfo', 'Status', varName)
}
