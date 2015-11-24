using NUnit.Framework;
using RHost;

namespace TestRClrHost
{
    [TestFixture]
    public class TestSymbolsData
    {
        [Test]
        public void TestSymbols()
        {
            Assert.AreEqual(0, FdkHelper.ConnectToFdk("", "", "", ""));
            var symbolInfos = FdkSymbolInfo.GetSymbolInfos();
            FdkSymbolInfo.GetRoundLot(symbolInfos);
            FdkSymbolInfo.GetSymbolComission(symbolInfos);
            FdkSymbolInfo.GetSymbolContractMultiplier(symbolInfos);
            FdkSymbolInfo.GetSymbolCurrency(symbolInfos);
            FdkSymbolInfo.GetSymbolLimitsCommission(symbolInfos);
            FdkSymbolInfo.GetSymbolMaxTradeVolume(symbolInfos);
            FdkSymbolInfo.GetSymbolMinTradeVolume(symbolInfos);
            FdkSymbolInfo.GetSymbolName(symbolInfos);
            FdkSymbolInfo.GetSymbolPrecision(symbolInfos);
            FdkSymbolInfo.GetSymbolSettlementCurrency(symbolInfos);
            FdkSymbolInfo.GetSymbolSwapSizeLong(symbolInfos);
            FdkSymbolInfo.GetSymbolSwapSizeShort(symbolInfos);
            FdkVars.Unregister(symbolInfos);

            FdkHelper.Disconnect();
        } 

        [Test]
        public void TestSymbolsStaging()
        {
            Assert.AreEqual(0, FdkHelper.ConnectToFdk("tp.st.soft-fx.eu", "100033", "123qwe!", ""));
            var symbolInfos = FdkSymbolInfo.GetSymbolInfos();
            FdkSymbolInfo.GetRoundLot(symbolInfos);
            FdkSymbolInfo.GetSymbolComission(symbolInfos);
            FdkSymbolInfo.GetSymbolContractMultiplier(symbolInfos);
            FdkSymbolInfo.GetSymbolCurrency(symbolInfos);
            FdkSymbolInfo.GetSymbolLimitsCommission(symbolInfos);
            FdkSymbolInfo.GetSymbolMaxTradeVolume(symbolInfos);
            FdkSymbolInfo.GetSymbolMinTradeVolume(symbolInfos);
            FdkSymbolInfo.GetSymbolName(symbolInfos);
            FdkSymbolInfo.GetSymbolPrecision(symbolInfos);
            FdkSymbolInfo.GetSymbolSettlementCurrency(symbolInfos);
            FdkSymbolInfo.GetSymbolSwapSizeLong(symbolInfos);
            FdkSymbolInfo.GetSymbolSwapSizeShort(symbolInfos);
            FdkVars.Unregister(symbolInfos);
            
            FdkHelper.Disconnect();
        }

        [Test]
        public void TestCurrency()
        {
            Assert.AreEqual(0, FdkHelper.ConnectToFdk("", "", "", ""));
            
            var symbolInfos = FdkCurrencyInfo.GetCurrencyInfos();
            FdkCurrencyInfo.GetCurrencyName(symbolInfos);
            FdkCurrencyInfo.GetCurrencyDescription(symbolInfos);
            FdkCurrencyInfo.GetCurrencyPrecision(symbolInfos);
            FdkCurrencyInfo.GetCurrencySortOrder(symbolInfos);

            FdkVars.Unregister(symbolInfos);

            FdkHelper.Disconnect();
        }
    }
}