namespace SoftFX.Extended.Financial.Adapter
{
    using System;
    using TickTrader.BusinessLogic;
    using TickTrader.BusinessObjects;

    sealed class CalculatorOrder : ICalculatorOrder
    {
        readonly TradeEntry entry;
        readonly Func<decimal, decimal> roundMargin;
        readonly Func<decimal, decimal> roundProfit;
        OrderError error;

        public CalculatorOrder(TradeEntry entry)
        {
            this.entry = entry;

            if (this.entry.Owner.Type != AccountType.Cash)
            {
                this.roundMargin = this.entry.Owner.RoundingService.RoundMargin;
                this.roundProfit = this.entry.Owner.RoundingService.RoundProfit;
            }
            else
            {
                var assetCurrency = this.entry.Side == TradeRecordSide.Buy ? this.entry.SymbolEntry.ProfitCurrency : this.entry.SymbolEntry.MarginCurrency;

                this.roundMargin = o => this.entry.Owner.RoundingService.RoundMargin(assetCurrency, o);
                this.roundProfit = o => this.entry.Owner.RoundingService.RoundProfit(assetCurrency, o);
            }
        }

        public decimal? AgentCommision
        {
            get { return (decimal)this.entry.AgentCommission; }
        }

        public decimal Amount
        {
            get { return (decimal)this.entry.NativeVolume; }
        }

        public decimal? Commission
        {
            get { return (decimal)this.entry.Commission; }
        }

        public decimal? CurrentPrice { get; set; }

        public bool IsCalculated
        {
            get { return this.error == null || this.error.Code == OrderErrorCode.None; }
        }

        public long OrderId
        {
            get { return this.entry.GetHashCode(); }
        }

        public decimal? Price
        {
            get { return (decimal)this.entry.Price; }
        }

        public decimal RemainingAmount
        {
            get { return this.Amount; }
        }

        public OrderSides Side
        {
            get { return CalculatorConvert.ToOrderSides(this.entry.Side); }
        }

        public decimal? Swap
        {
            get { return (decimal)this.entry.Swap; }
        }

        public string Symbol
        {
            get { return this.entry.Symbol; }
        }

        public OrderTypes Type
        {
            get { return CalculatorConvert.ToOrderTypes( this.entry.Type); }
        }

        #region Calculated Properties

        public OrderError CalculationError
        {
            get
            {
                return this.error;
            }
            set
            {
                this.error = value;

                if (this.IsCalculated)
                    this.entry.ProfitStatus = this.entry.MarginStatus = TradeEntryStatus.Calculated;
                else if (this.error.Code == OrderErrorCode.OffQuotes)
                    this.entry.ProfitStatus = this.entry.MarginStatus = TradeEntryStatus.OffQuotes;
                else if (this.error.Code == OrderErrorCode.Misconfiguration)
                    this.entry.ProfitStatus = this.entry.MarginStatus = TradeEntryStatus.UnknownSymbol;
                else
                    this.entry.ProfitStatus = this.entry.MarginStatus = TradeEntryStatus.NotCalculated;
            }
        }

        OrderCalculator ICalculatorOrder.Calculator { get; set; }

        public decimal? Margin
        {
            get
            {
                return (decimal?)this.entry.Margin;
            }
            set
            {
                this.entry.Margin = Round(value, this.roundMargin);
            }
        }

        public decimal? MarginRateCurrent { get; set; }

        public decimal? Profit
        {
            get
            {
                return (decimal?)this.entry.Profit;
            }
            set
            {
                this.entry.Profit = Round(value, this.roundProfit);
            }
        }


        static double? Round(decimal? value, Func<decimal, decimal> rounder)
        {
            if (!value.HasValue)
                return null;

            return (double)rounder(value.Value);
        }

        #endregion

        #region Events

        event Action<ICalculatorOrder> ICalculatorOrder.EssentialParametersChanged
        {
            add
            {
            }
            remove
            {
            }
        }

        #endregion
    }
}
