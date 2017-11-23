﻿namespace SoftFX.Extended.Financial.Adapter
{
    using System;
    using System.Collections.Generic;
    using TickTrader.BusinessLogic;
    using TickTrader.BusinessObjects;
    using TickTrader.Common.Business;

    static class CalculatorConvert
    {
        public static CurrencyInfo ToCurrencyInfo(CurrencyEntry currency, int priority)
        {
            return new CurrencyInfo
            {
                Name = currency.Name,
                Precision = currency.Precision,
                SortOrder = currency.SortOrder,
                Id = (short)currency.GetHashCode()
            };
        }

        public static SymbolInfo ToSymbolInfo(SymbolEntry symbol)
        {
            return new SymbolInfo
            {
                Symbol = symbol.Symbol,
                MarginCurrency = symbol.MarginCurrency,
                MarginCurrencyId = (short)symbol.MarginCurrency.GetHashCode(),
                ProfitCurrency = symbol.ProfitCurrency,
                ProfitCurrencyId = (short)symbol.ProfitCurrency.GetHashCode(),
                ContractSizeFractional = symbol.ContractSize,
                MarginFactorFractional = symbol.MarginFactor,
                MarginHedged = symbol.Hedging,
                SortOrder = symbol.SortOrder,
                MarginMode = ToMarginCalculationModes(symbol.MarginCalcMode),
                StopOrderMarginReduction = symbol.StopOrderMarginReduction ?? 1,
                HiddenLimitOrderMarginReduction = symbol.HiddenLimitOrderMarginReduction ?? 1
            };
        }

        static MarginCalculationModes ToMarginCalculationModes(MarginCalcMode mode)
        {
            return (MarginCalculationModes)mode;
        }

        public static ISymbolRate ToSymbolRate(KeyValuePair<string, PriceEntry> price)
        {
            return new SymbolRate(price.Key, price.Value);
        }

        public static AccountingTypes ToAccountingTypes(AccountType type)
        {
            switch (type)
            {
                case AccountType.Gross:
                    return AccountingTypes.Gross;
                case AccountType.Net:
                    return AccountingTypes.Net;
                case AccountType.Cash:
                    return AccountingTypes.Cash;
            }

            throw new ArgumentException("type");
        }

        public static IMarginAccountInfo ToMarginAccountInfo(AccountEntry account)
        {
            return new MarginAccountInfo(account);
        }

        public static IOrderModel ToCalculatorOrder(TradeEntry trade)
        {
            return new CalculatorOrder(trade);
        }

        public static OrderSides ToOrderSides(TradeRecordSide side)
        {
            switch (side)
            {
                case TradeRecordSide.Buy:
                    return OrderSides.Buy;
                case TradeRecordSide.Sell:
                    return OrderSides.Sell;
            }

            throw new ArgumentException("side");
        }

        public static OrderTypes ToOrderTypes(TradeRecordType type)
        {
            switch (type)
            {
                case TradeRecordType.Market:
                    return OrderTypes.Market;
                case TradeRecordType.Position:
                    return OrderTypes.Position;
                case TradeRecordType.Limit:
                    return OrderTypes.Limit;
                case TradeRecordType.Stop:
                    return OrderTypes.Stop;
                case TradeRecordType.IoC:
                    return OrderTypes.Limit;
                case TradeRecordType.MarketWithSlippage:
                    return OrderTypes.Limit;
                case TradeRecordType.StopLimit:
                    return OrderTypes.StopLimit;
                case TradeRecordType.StopLimit_IoC:
                    return OrderTypes.StopLimit;
            }

            throw new ArgumentException("type");
        }

        public static IAssetModel ToAssetModel(Asset asset)
        {
            return new CalculatorAsset(asset);
        }
    }
}
