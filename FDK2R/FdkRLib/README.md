# FdkRLib
Added the SoftFX R wrapper package over FDK (Financial Development Kit)

# Prerequisites
If you see this error: "You are probably missing the Visual C++ Redistributable for Visual Studio 2013", then please download it from here:
https://www.microsoft.com/en-us/download/details.aspx?id=40784

# How to install it?
Look inside [install.r](https://github.com/SoftFx/FdkRLib/blob/master/install.r)  file (you can copy/paste the content into your R environment).

For later sessions of R environment will require just to reload the library with this command:
library(FdkRLib)

# How to test it?
You have sample code inside examples/sample_bars.r with various snippets of code. 

A simple code sample code is the following:
```

ttConnect("", "", "")

#Get configuration information of your account
head(ttGetSymbolData())
head(ttGetCurrencyData())

#Quotes in the last 5 minutes
now <-as.POSIXct(Sys.time())
# 300 seconds from present
prevNow <-as.POSIXct(now-(5*60))
head(ttQuotesHistory("EURUSD", startTime = prevNow, endTime=now))

# Get quotes level 2
# 1000 seconds from present
now <-as.POSIXct(Sys.time())
prevNow <-as.POSIXct(now-1000)
qt2= ttQuotesLevel2History('EURUSD', prevNow, now)
head(qt2)
```
Follow this link with expanded example and output:

Configuration:
http://rpubs.com/ciplogic/105460

History:
http://rpubs.com/ciplogic/105462

Trades:
http://rpubs.com/ciplogic/105463
