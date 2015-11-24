
# Connect to server before running the sample
# ttConnect()
ttConnect()
bars = ttBarsHistory("EURUSD", "Bid", "H1")

boxplot(bars$highs)

plot(highs, data = bars)

endTime <- as.POSIXlt(Sys.time())
startTime <- strptime("20/3/2 11:16:16.683", "%d/%m/%y %H:%M:%OS")

st1 <- as.POSIXct(startTime)
et1 <- as.POSIXct(endTime)

quotes <- ttQuotes("EURUSD", st1, et1, 1)
plot(quotes$ask, type="o")

bars = ttBarsHistory(symbol = "EURUSD", barPeriodStr = "M15", priceTypeStr = "Ask", barCount = 100000000)

barRange = ttBarsHistory(symbol = "EURUSD", barPeriodStr = "M1", priceTypeStr = "Ask", startTime = startTime)
