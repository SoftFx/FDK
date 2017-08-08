namespace SoftFX.Extended.Financial.Serialization
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// For internal usage only
    /// </summary>
    [XmlRoot("Currency")]
    public class CurrencyData
    {
        /// <summary>
        /// For internal usage only
        /// </summary>
        public CurrencyData()
        {
        }

        internal CurrencyData(CurrencyEntry entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            this.Name = entry.Name;
            this.Precision = entry.Precision;
            this.SortOrder = entry.SortOrder;
        }

        internal CurrencyEntry CreateEntry(FinancialCalculator owner)
        {
            var result = new CurrencyEntry(owner, this.Name, this.Precision, this.SortOrder);
            return result;
        }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("Name")]
        public string Name { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("Precision")]
        public int Precision { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("SortOrder")]
        public int SortOrder { get; set; }
    }
}
