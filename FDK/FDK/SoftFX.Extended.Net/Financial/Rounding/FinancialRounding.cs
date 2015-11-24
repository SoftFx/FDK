namespace SoftFX.Extended.Financial
{
    using System;
    using TickTrader.Common;

    sealed class FinancialRounding : IRounding
    {
        public static readonly IRounding Instance = new FinancialRounding();

        FinancialRounding()
        {
        }

        public decimal RoundProfit(int precision, decimal profit)
        {
            return RoundDown(precision, profit);
        }

        public double RoundProfit(int precision, double profit)
        {
            return RoundDown(precision, profit);
        }

        public decimal RoundMargin(int precision, decimal margin)
        {
            return RoundUp(precision, margin);
        }

        public double RoundMargin(int precision, double margin)
        {
            return RoundUp(precision, margin);
        }

        static decimal RoundDown(int precision, decimal value)
        {
            return ObjectCaches.RoundingTools.WithPrecision(precision).Floor(value);
        }

        static double RoundDown(int precision, double value)
        {
            return Math.Floor(value * Math.Pow(10, precision)) / Math.Pow(10, precision);
        }

        static decimal RoundUp(int precision, decimal value)
        {
            return ObjectCaches.RoundingTools.WithPrecision(precision).Ceil(value);
        }

        static double RoundUp(int precision, double value)
        {
            return Math.Ceiling(value * Math.Pow(10, precision)) / Math.Pow(10, precision);
        }
    }
}
