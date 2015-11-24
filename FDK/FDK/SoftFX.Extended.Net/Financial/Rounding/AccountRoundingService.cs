namespace SoftFX.Extended.Financial
{
    using System;

    sealed class AccountRoundingService : IAccountRoundingService
    {
        readonly IRounding rounder;
        readonly IPrecisionProvider precisionProvider;
        readonly int? precision;

        public AccountRoundingService(IRounding rounder, IPrecisionProvider precisionProvider, string currency)
        {
            if (rounder == null)
                throw new ArgumentNullException("rounder");

            if (precisionProvider == null)
                throw new ArgumentNullException("precisionProvider");

            if (currency == null)
                throw new ArgumentNullException("currency");


            this.rounder = rounder;
            this.precisionProvider = precisionProvider;

            if (currency.Length > 0)
                this.precision = precisionProvider.GetCurrencyPrecision(currency);
        }

        public decimal RoundProfit(string currency, decimal profit)
        {
            return this.rounder.RoundProfit(this.precisionProvider.GetCurrencyPrecision(currency), profit);
        }

        public double RoundProfit(string currency, double profit)
        {
            return this.rounder.RoundProfit(this.precisionProvider.GetCurrencyPrecision(currency), profit);
        }

        public decimal RoundMargin(string currency, decimal margin)
        {
            return this.rounder.RoundMargin(this.precisionProvider.GetCurrencyPrecision(currency), margin);
        }

        public double RoundMargin(string currency, double margin)
        {
            return this.rounder.RoundMargin(this.precisionProvider.GetCurrencyPrecision(currency), margin);
        }

        public decimal RoundProfit(decimal profit)
        {
            if (!this.precision.HasValue)
                return profit;

            return this.rounder.RoundProfit(this.precision.Value, profit);
        }

        public double RoundProfit(double profit)
        {
            if (!this.precision.HasValue)
                return profit;

            return this.rounder.RoundProfit(this.precision.Value, profit);
        }

        public decimal RoundMargin(decimal margin)
        {
            if (!this.precision.HasValue)
                return margin;

            return this.rounder.RoundMargin(this.precision.Value, margin);
        }

        public double RoundMargin(double margin)
        {
            if (this.precision.HasValue)
                return margin;

            return this.rounder.RoundMargin(this.precision.Value, margin);
        }
    }
}
