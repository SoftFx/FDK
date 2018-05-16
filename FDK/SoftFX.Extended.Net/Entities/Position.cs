using System;
using System.Text;

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
        /// 
        /// </summary>
        public DateTime? PosModified { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string PosID { get; internal set; }

        /// <summary>
        /// Returns formatted string for the class instance.
        /// </summary>
        /// <returns>Can not be null.</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            if (!String.IsNullOrEmpty(this.PosID))
                builder.Append(string.Format("ID = {0}; ", this.PosID));
            builder.Append(string.Format("Symbol = {0}; Settlement Price = {1}; Buy Amount = {2}; Sell Amount = {3}; Commission = {4}", this.Symbol, this.SettlementPrice, this.BuyAmount, this.SellAmount, this.Commission));
            if (this.Swap != 0)
                builder.Append(string.Format("; Swap = {0}", this.Swap));
            if (this.PosModified != null)
                builder.Append(string.Format("; Modification Time = {0}", this.PosModified));
            return builder.ToString();
        }
    }
}
