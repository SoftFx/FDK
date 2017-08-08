namespace SoftFX.Extended.Financial.Serialization
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// For internal usage only
    /// </summary>
    [XmlRoot("Trade")]
    public class TradeData
    {
        /// <summary>
        /// For internal usage only
        /// </summary>
        public TradeData()
        {
        }

        internal TradeData(TradeEntry entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            this.Tag = entry.Tag != null ? entry.Tag.ToString() : string.Empty;
            this.Type = TradeTypeFromTradeRecordType(entry.Type);
            this.Side  = entry.Side;
            this.Symbol = entry.Symbol;
            this.Volume = entry.Volume;
            this.MaxVisibleVolume = entry.MaxVisibleVolume;
            this.Price = entry.Price;
            this.StopPrice = entry.StopPrice;
            this.Commission = entry.Commission;
            this.AgentCommission = entry.AgentCommission;
            this.Swap = entry.Swap;
            //this.Rate = entry.StaticMarginRate;
            this.ProfitStatus = entry.ProfitStatus;
            this.MarginStatus = entry.MarginStatus;
            this.Profit  = entry.Profit;
            this.Margin = entry.Margin;
        }

        internal TradeEntry CreateEntry(AccountEntry owner)
        {
            var result = new TradeEntry(owner)
            {
                Tag = this.Tag,
                Type = TradeRecordTypeFromTradeType(this.Type),
                Side = this.Side,
                Symbol = this.Symbol,
                Volume = this.Volume,
                MaxVisibleVolume = this.MaxVisibleVolume,
                Price = this.Price,
                StopPrice = this.StopPrice,
                Commission = this.Commission,
                AgentCommission = this.AgentCommission,
                Swap = this.Swap,
                //StaticMarginRate = this.Rate,
                ProfitStatus = this.ProfitStatus,
                MarginStatus = this.MarginStatus,
                Profit = this.Profit,
                Margin = this.Margin
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
        [XmlAttribute("Type")]
        public TradeType Type { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("Side")]
        public TradeRecordSide Side { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("Symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("Volume")]
        public double Volume { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("MaxVisibleVolume")]
        public double? MaxVisibleVolume { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("Price")]
        public double? Price { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("StopPrice")]
        public double? StopPrice { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("Commission")]
        public double Commission { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("AgentCommission")]
        public double AgentCommission { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("Swap")]
        public double Swap { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        public double? Rate { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("ProfitStatus")]
        public TradeEntryStatus ProfitStatus { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("MarginStatus")]
        public TradeEntryStatus MarginStatus { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        public double? Profit { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        public double? Margin { get; set; }

        #region Methods

        static TradeRecordType TradeRecordTypeFromTradeType(TradeType type)
        {
            if (type == TradeType.Position)
            {
                return TradeRecordType.Position;
            }
            if (type == TradeType.Limit)
            {
                return TradeRecordType.Limit;
            }
            if (type == TradeType.Stop)
            {
                return TradeRecordType.Stop;
            }
            if (type == TradeType.StopLimit)
            {
                return TradeRecordType.StopLimit;
            }

            var message = string.Format("Unsupporred trade type = {0}", type);
            throw new ArgumentException(message, nameof(type));
        }

        static TradeType TradeTypeFromTradeRecordType(TradeRecordType type)
        {
            if (type == TradeRecordType.Position)
            {
                return TradeType.Position;
            }
            if (type == TradeRecordType.Limit)
            {
                return TradeType.Limit;
            }
            if (type == TradeRecordType.Stop)
            {
                return TradeType.Stop;
            }
            if (type == TradeRecordType.StopLimit)
            {
                return TradeType.StopLimit;
            }

            var message = string.Format("Unsupported trade record type = {0}", type);
            throw new ArgumentException(message, nameof(type));
        }

        #endregion
    }
}
