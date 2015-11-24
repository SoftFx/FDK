ttConnect("tp.st.soft-fx.eu", "100064", "123qwe!")

endTime <- as.POSIXct(0, origin=ISOdatetime(2015,7,7,19,00,0))
startTime <- as.POSIXct(0, origin=ISOdatetime(2015,7,7,18,30,0))
st1 <- as.POSIXct(startTime, tz = "GMT")
et1 <- as.POSIXct(endTime, tz = "GMT")
as.double(et1)

barPairsRange = ttQuotesHistory("#SPX", endTime = et1)
#barPairsRange = ttQuotesHistory(symbol = "#SPX", startTime= st1, endTime = st1)
View(barPairsRange)