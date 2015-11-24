using System;
using System.Linq;
using log4net;
using SoftFX.Extended;
using SoftFX.Extended.Extensions;
using SoftFX.Extended.Financial;
using SoftFX.Extended.Events;

namespace RHost
{
	public class FdkSymbolInfo
	{
		public static string GetSymbolInfos()
		{
			try
			{
                var symbolInfos = Symbols;
				var varName = FdkVars.RegisterVariable(symbolInfos, "symbolsInfo");
				return varName;
			}
			catch (Exception ex)
			{
				Log.Error(ex);
				throw;
			}     
        }

        public static void RegisterToFeed(DataFeed feed, FinancialCalculator calculator)
        {
            feed.Tick += (object sender, TickEventArgs e) =>
            {
                calculator.Prices.Update(e.Tick.Symbol, e.Tick.Bid, e.Tick.Ask);
            };
            feed.Server.SubscribeToQuotes(Symbols.Select(symbol=>symbol.Name), 1);            
        }



        public static SymbolInfo[] Symbols
        {
            get
            {
                return Feed.Cache.Symbols;
            }
        }

        public static DataFeed Feed
        {
            get
            {
                return FdkHelper.Wrapper.ConnectLogic.Feed;
            }
        }
        static readonly ILog Log = LogManager.GetLogger(typeof(FdkSymbolInfo));

        public static double[] GetSymbolComission(string symbolsInfo)
        {
            var symbolInfos = FdkVars.GetValue<SymbolInfo[]>(symbolsInfo);
            return symbolInfos.SelectToArray(b => b.Commission);
        }

		public static double[] GetSymbolContractMultiplier(string symbolsInfo)
        {
            var symbolInfos = FdkVars.GetValue<SymbolInfo[]>(symbolsInfo);
            return symbolInfos.SelectToArray(b => b.ContractMultiplier);
        }

        public static string[] GetSymbolCurrency(string symbolsInfo)
        {
            var symbolInfos = FdkVars.GetValue<SymbolInfo[]>(symbolsInfo);
            return symbolInfos.SelectToArray(b => b.Currency);
        }
        public static double[] GetSymbolLimitsCommission(string symbolsInfo)
        {
            var symbolInfos = FdkVars.GetValue<SymbolInfo[]>(symbolsInfo);
            return symbolInfos.SelectToArray(b => b.LimitsCommission);
        }
        public static double[] GetSymbolMaxTradeVolume(string symbolsInfo)
        {
            var symbolInfos = FdkVars.GetValue<SymbolInfo[]>(symbolsInfo);
            return symbolInfos.SelectToArray(b => b.MaxTradeVolume);
        }
        public static double[] GetSymbolMinTradeVolume(string symbolsInfo)
        {
            var symbolInfos = FdkVars.GetValue<SymbolInfo[]>(symbolsInfo);
            return symbolInfos.SelectToArray(b => b.MinTradeVolume);
        }
        public static string[] GetSymbolName(string symbolsInfo)
        {
            var symbolInfos = FdkVars.GetValue<SymbolInfo[]>(symbolsInfo);
            return symbolInfos.SelectToArray(b => b.Name);
        }
        public static double[] GetSymbolPrecision(string symbolsInfo)
        {
            var symbolInfos = FdkVars.GetValue<SymbolInfo[]>(symbolsInfo);
            return symbolInfos.SelectToArray(b => (double)b.Precision);
        }
        public static double[] GetRoundLot(string symbolsInfo)
        {
            var symbolInfos = FdkVars.GetValue<SymbolInfo[]>(symbolsInfo);
            return symbolInfos.SelectToArray(b => b.RoundLot);
        }
        public static string[] GetSymbolSettlementCurrency(string symbolsInfo)
        {
            var symbolInfos = FdkVars.GetValue<SymbolInfo[]>(symbolsInfo);
            return symbolInfos.SelectToArray(b => b.SettlementCurrency);
        }
        public static double[] GetSymbolSwapSizeLong(string symbolsInfo)
        {
            var symbolInfos = FdkVars.GetValue<SymbolInfo[]>(symbolsInfo);
            return symbolInfos.SelectToArray(b => b.SwapSizeLong ?? double.NaN);
        }

        public static double[] GetSymbolSwapSizeShort(string symbolsInfo)
        {
            var symbolInfos = FdkVars.GetValue<SymbolInfo[]>(symbolsInfo);
            return symbolInfos.SelectToArray(b => b.SwapSizeShort ?? double.NaN);
        }

        public static double[] GetSymbolPipsValue(string symbolsInfo)
        {
            var symbolInfos = FdkVars.GetValue<SymbolInfo[]>(symbolsInfo);
            return symbolInfos.SelectToArray(b => CalculatePipsValue(b));
        }


        public static double[] GetSymbolCurrentPriceBid(string symbolsInfo)
        {
            SymbolInfo[] symbolInfos = FdkVars.GetValue<SymbolInfo[]>(symbolsInfo);
            return symbolInfos.SelectToArray(b => CalculatePriceBid(b));
        }

        public static double CalculatePipsValue(SymbolInfo symbol)
        {
            FinancialCalculator financialCalculator = FdkStatic.Calculator;
            int decimals = symbol.Precision;
            double contractSize = symbol.ContractMultiplier;
            double? rateK = financialCalculator.CalculateAssetRate(1, symbol.SettlementCurrency, "USD");
            if (!rateK.HasValue)
                throw new InvalidOperationException(
                    string.Format("No rate for currency pair: {0}/USD", symbol.SettlementCurrency));
            double formula = Math.Pow(10, -decimals) * contractSize * rateK.Value;
            return formula;
        }
        public static double CalculatePriceBid(SymbolInfo symbol)
        {
            FinancialCalculator financialCalculator = FdkStatic.Calculator;
            double? rateK = financialCalculator.CalculateAssetRate(1, symbol.SettlementCurrency, "USD");
            if (!rateK.HasValue)
                return double.NaN;
            return rateK.Value;
        }
    }
}

