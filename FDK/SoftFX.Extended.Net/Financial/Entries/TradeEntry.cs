namespace SoftFX.Extended.Financial
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Represents trade entry.
    /// </summary>
    public class TradeEntry : FinancialEntry<AccountEntry>
    {
        #region Construction

        /// <summary>
        /// Creates a new instance of trade entry.
        /// </summary>
        /// <param name="owner">valid account entry instance</param>
        public TradeEntry(AccountEntry owner)
            : base(owner)
        {
            this.Type = TradeRecordType.Position;
            this.Side = TradeRecordSide.Buy;
            this.Symbol = string.Empty;
        }

        /// <summary>
        /// Creates a new instance of trade entry.
        /// </summary>
        /// <param name="owner">valid account entry instance</param>
        /// <param name="type"></param>
        /// <param name="side"></param>
        /// <param name="symbol"></param>
        /// <param name="volume"></param>
        /// <param name="maxVisibleVolume"></param>
        /// <param name="price"></param>
        /// <param name="stopPrice"></param>
        /// <param name="staticMarginRate"></param>
        public TradeEntry(AccountEntry owner, TradeRecordType type, TradeRecordSide side, string symbol, double volume, double? maxVisibleVolume, double? price, double? stopPrice, double? staticMarginRate = null)
            : base(owner)
        {
            this.Type = type;
            this.Side = side;
            this.Symbol = symbol;
            this.Volume = volume;
            this.MaxVisibleVolume = maxVisibleVolume;
            this.Price = price;
            this.StopPrice = stopPrice;
            //this.StaticMarginRate = staticMarginRate;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Resets all calculated properties to null.
        /// </summary>
        public void Clear()
        {
            this.Profit = null;
            this.Margin = null;
            this.ProfitStatus = TradeEntryStatus.NotCalculated;
            this.MarginStatus = TradeEntryStatus.NotCalculated;
        }

        #endregion

        #region Parameters

        /// <summary>
        /// Gets and sets type of the trade entry.
        /// </summary>
        [Category("Parameters")]
        [Description("Type of trade entry: Position, Limit or Stop")]
        public TradeRecordType Type { get; set; }

        /// <summary>
        /// Gets and sets side of the trade entry.
        /// </summary>
        [Category("Parameters")]
        [Description("Side of trade entry: Buy or Sell")]
        public TradeRecordSide Side { get; set; }

        /// <summary>
        /// Gets and sets trade symbol.
        /// </summary>
        [Category("Parameters")]
        public string Symbol { get; set; }

        /// <summary>
        /// Gets and sets trade volume.
        /// </summary>
        [Category("Parameters")]
        public double Volume { get; set; }

        /// <summary>
        /// Gets and sets trade max visible volume.
        /// </summary>
        [Category("Parameters")]
        public double? MaxVisibleVolume { get; set; }

        /// <summary>
        /// Gets and sets trade price.
        /// </summary>
        [Category("Parameters")]
        public double? Price { get; set; }

        /// <summary>
        /// Gets and sets trade stop price.
        /// </summary>
        [Category("Parameters")]
        public double? StopPrice { get; set; }

        /// <summary>
        /// Gets or sets static margin rate.
        /// </summary>
        [Category("Parameters")]
        [DisplayName("Static Margin Rate")]
        [Description("Margin rate for Static and StaticIfPossible mode. StaticMarginRate is obsolete, only Dynamic margin mode supported.")]
        [Obsolete("StaticMarginRate is obsolete, only Dynamic margin mode supported.")]
        public double? StaticMarginRate { get; set; }

        /// <summary>
        /// Gets and sets trade commission.
        /// </summary>
        [Category("Parameters")]
        public double Commission { get; set; }

        /// <summary>
        /// ets and sets trade agent commission.
        /// </summary>
        [Category("Parameters")]
        [DisplayName("Agent Commission")]
        public double AgentCommission { get; set; }

        /// <summary>
        /// Gets and sets trade swap.
        /// </summary>
        [Category("Parameters")]
        public double Swap { get; set; }

        #endregion

        #region Calculated

        /// <summary>
        /// Gets status of Profit property.
        /// </summary>
        [Category("Calculated")]
        [DisplayName("Profit Status")]
        public TradeEntryStatus ProfitStatus { get; internal set; }

        /// <summary>
        /// Gets status of Margin property.
        /// </summary>
        [Category("Calculated")]
        [DisplayName("Margin Status")]
        public TradeEntryStatus MarginStatus { get; internal set; }

        /// <summary>
        /// Gets calculated profit if it is available, otherwise returns null.
        /// </summary>
        [Category("Calculated")]
        public double? Profit { get; internal set; }

        /// <summary>
        /// Gets calculated margin if it is available, otherwise returns null.
        /// </summary>
        [Category("Calculated")]
        public double? Margin { get; internal set; }

        #endregion

        #region Internal Methods

        internal bool PrepareForCalculation()
        {
            if (this.SymbolEntry == null)
            {
                this.ProfitStatus = TradeEntryStatus.UnknownSymbol;
                this.MarginStatus = TradeEntryStatus.UnknownSymbol;
                return false;
            }

            return true;
        }

        #endregion

        #region Internal Properties

        internal SymbolEntry SymbolEntry { get; set; }

        internal double NativeVolume
        {
            get
            {
                return this.SymbolEntry.ContractSize * this.Volume;
            }
        }

        #endregion
    }
}
