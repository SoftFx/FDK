namespace SoftFX.Extended
{
    /// <summary>
    /// Contains position information for a symbol.
    /// </summary>
    public class Position
    {
        internal Position()
        {
        }

        /// <summary>
        /// Gets the position symbol.
        /// </summary>
        public string Symbol { get; internal set; }

        /// <summary>
        /// Gets the position settlement price.
        /// </summary>
        public double SettlementPrice { get; internal set; }

        /// <summary>
        /// Gets total amount, which has been bought.
        /// </summary>
        public double BuyAmount { get; internal set; }

        /// <summary>
        /// Gets total amount, which has been sold.
        /// </summary>
        public double SellAmount { get; internal set; }

        /// <summary>
        /// Gets commission.
        /// </summary>
        public double Commission { get; internal set; }

        /// <summary>
        /// Gets agent commission.
        /// </summary>
        public double AgentCommission { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double Swap { get; internal set; }

        /// <summary>
        /// It's used by FinancialCalculator.
        /// </summary>
        public double? Profit { get; internal set; }
        
        /// <summary>
        /// It's used by FinancialCalculator.
        /// </summary>
        public double? Margin { get; internal set; }

        /// <summary>
        /// Gets average price of buy position.
        /// </summary>
        public double? BuyPrice { get; internal set; }

        /// <summary>
        /// Gets average price of sell position.
        /// </summary>
        public double? SellPrice { get; internal set; }

        /// <summary>
        /// Returns formatted string for the class instance.
        /// </summary>
        /// <returns>Can not be null.</returns>
        public override string ToString()
        {
            return string.Format("Symbol = {0}; Settlement Price = {1}; Buy Amount = {2}; Sell Amount = {3}", this.Symbol, this.SettlementPrice, this.BuyAmount, this.SellAmount);
        }
    }
}
