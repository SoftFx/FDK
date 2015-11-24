#' Initialize the CLR runtime and loads the FDK host assembly
#' 
#' @export 
ttInit <- function() {t
  require(rClr)
  if(!require(data.table)){
	  install.packages("data.table", repos="http://cran.us.r-project.org")
	  require(data.table)
  }
  fileName <-system.file("data", "FdkRHost.dll", package="rFdk")
  clrLoadAssembly(fileName)
}
#' Connects to a TT server
#' 
#' @param address Url of the running server
#' @param login Account number you login
#' @param password Password for the account you login
#' @param fdkPath Path with connection related cache data
#' @export
ttConnect <- function(address = "", login= "", password= "", fdkPath = "") {
  ttInit()
  rClr::clrCallStatic('RHost.FdkStatic', 'ConnectToFdk', address, login, password, fdkPath)
}

#' Disconnect from a TT server
#' 
#' @export
ttDisconnect <- function() {
  rClr::clrCallStatic('RHost.FdkStatic', 'Disconnect')
}

#' Displays a DateTime
#' 
#' @param dateToShow An R datetime
#' @export
ttDisplayDate <- function(dateToShow) {
  rClr::clrCallStatic('RHost.FdkStatic', 'DisplayDate', dateToShow)
}
