
getTickData <- function(symbolName){
  basicUrl = "http://tp.dev.soft-fx.eu:5021/api/v1/public/tick/"
  fullUrl = paste(basicUrl, symbolName, sep = "")
  fromJSON(fullUrl)
}
symbol = "EURUSD"
tickData <- getTickData(symbol)
bestBid <- tickData$BestBid$Price
oldTimeStamp <-tickData$Timestamp

tickData <- getTickData(symbol)

timeStamp <- tickData$Timestamp
if(timeStamp!=oldTimeStamp)
{
  bestBid <- c(bestBid, tickData$BestBid$Price)
  plot(bestBid)
  oldTimeStamp = timeStamp
}
