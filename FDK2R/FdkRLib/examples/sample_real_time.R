ttConnect()
idMonitor <- ttQuotesSubscribe('EURUSD', 2)
# Wait for some time before running this line
# it can be run multiple times and the snapshot reflects
snapshot <- ttQuotesLevel2(idMonitor)
View(snapshot)

ttQuotesUnsubscribe(idMonitor)
