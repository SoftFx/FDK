namespace SoftFX.Extended.Financial.Serialization
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// For internal usage only
    /// </summary>
    [XmlRoot("Symbol")]
    public class SymbolData
    {
        /// <summary>
        /// For internal usage only
        /// </summary>
        public SymbolData()
        {
        }

        internal SymbolData(SymbolEntry entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            this.Tag = entry.Tag != null ? entry.Tag.ToString() : string.Empty;
            this.Symbol = entry.Symbol;
            this.From = entry.From;
            this.To = entry.To;
            this.ContractSize = entry.ContractSize;
            this.Hedging = entry.Hedging;
            this.MarginFactorOfPositions = entry.MarginFactorOfPositions;
            this.MarginFactorOfLimitOrders = entry.MarginFactorOfLimitOrders;
            this.MarginFactorOfStopOrders = entry.MarginFactorOfStopOrders;
            this.StopOrderMarginReduction = entry.StopOrderMarginReduction;
            this.HiddenLimitOrderMarginReduction = entry.HiddenLimitOrderMarginReduction;
        }

        internal SymbolEntry CreateEntry(FinancialCalculator owner)
        {
            var result = new SymbolEntry(owner, this.Symbol, this.From, this.To)
            {
                Tag = this.Tag,
                ContractSize = this.ContractSize,
                Hedging = this.Hedging,
                MarginFactorOfPositions = this.MarginFactorOfPositions,
                MarginFactorOfLimitOrders = this.MarginFactorOfLimitOrders,
                MarginFactorOfStopOrders = this.MarginFactorOfStopOrders,
                StopOrderMarginReduction = this.StopOrderMarginReduction,
                HiddenLimitOrderMarginReduction = this.HiddenLimitOrderMarginReduction
            };

            return result;
        }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("Tag")]
        public string Tag { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("Symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("From")]
        public string From { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("To")]
        public string To { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("ContractSize")]
        public double ContractSize { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("Hedging")]
        public double Hedging { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("MarginFactorOfPositions")]
        public double MarginFactorOfPositions { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("MarginFactorOfLimitOrders")]
        public double MarginFactorOfLimitOrders { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("MarginFactorOfStopOrders")]
        public double MarginFactorOfStopOrders { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("StopOrderMarginReduction")]
        public double? StopOrderMarginReduction { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("HiddenLimitOrderMarginReduction")]
        public double? HiddenLimitOrderMarginReduction { get; set; }
    }
}
