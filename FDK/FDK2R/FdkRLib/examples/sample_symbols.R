# Connect to server before running the sample
# ttConnect()
symbolData = ttGetSymbolData()
View(symbolData)

#gets the currency data
currencyData = ttGetCurrencyData()
View(currencyData)
