namespace SoftFX.Extended.Financial.Serialization
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// For internal usage only
    /// </summary>
    public class PriceData
    {
        /// <summary>
        /// For internal usage only
        /// </summary>
        public PriceData()
        {
        }

        internal PriceData(KeyValuePair<string, PriceEntry> entry)
        {
            this.Symbol = entry.Key;
            this.Bid = entry.Value.Bid;
            this.Ask = entry.Value.Ask;
        }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("Symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("Bid")]
        public double Bid { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("Ask")]
        public double Ask { get; set; }
    }
}
