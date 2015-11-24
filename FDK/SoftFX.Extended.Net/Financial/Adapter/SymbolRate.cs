namespace SoftFX.Extended.Financial.Adapter
{
    using TickTrader.Common.Business;

    sealed class SymbolRate : ISymbolRate
    {
        readonly PriceEntry price;
        readonly string symbol;

        public SymbolRate(string symbol, PriceEntry price)
        {
            this.price = price;
            this.symbol = symbol;
        }

        public decimal Ask
        {
            get { return (decimal)this.price.Ask; }
        }

        public decimal Bid
        {
            get { return (decimal)this.price.Bid; }
        }

        public decimal? NullableAsk
        {
            get { return this.Ask; }
        }

        public decimal? NullableBid
        {
            get { return this.Bid; }
        }

        public string Symbol
        {
            get { return this.symbol; }
        }
    }
}
