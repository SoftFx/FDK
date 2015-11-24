namespace SoftFX.Extended.Financial.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// For internal usage only
    /// </summary>
    [XmlRoot("Account")]
    public class AccountData
    {
        /// <summary>
        /// For internal usage only
        /// </summary>
        public AccountData()
        {
            this.Trades = new List<TradeData>();
        }

        internal AccountData(AccountEntry entry)
            : this()
        {
            if (entry == null)
                throw new ArgumentNullException("entry");

            this.Tag = entry.Tag != null ? entry.Tag.ToString() : string.Empty;
            this.Type = entry.Type;
            this.Leverage = entry.Leverage;
            this.Balance = entry.Balance;
            this.Currency = entry.Currency;
            this.Profit = entry.Profit;
            this.ProfitStatus = entry.ProfitStatus;
            this.Margin = entry.Margin;
            this.MarginStatus = entry.MarginStatus;

            foreach (var element in entry.Trades)
            {
                var data = new TradeData(element);
                this.Trades.Add(data);
            }
        }

        internal AccountEntry CreateEntry(FinancialCalculator owner)
        {
            var result = new AccountEntry(owner)
            {
                Tag = this.Tag,
                Type = this.Type,
                Leverage = this.Leverage,
                Balance = this.Balance,
                Currency = this.Currency,
                Profit = this.Profit,
                ProfitStatus = this.ProfitStatus,
                Margin = this.Margin,
                MarginStatus = this.MarginStatus
            };

            foreach (var element in this.Trades)
            {
                var entry = element.CreateEntry(result);
                result.Trades.Add(entry);
            }

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
        public AccountType Type { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("Leverage")]
        public double Leverage { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("Balance")]
        public double Balance { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("Currency")]
        public string Currency { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        public double? Profit { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("ProfitStatus")]
        public AccountEntryStatus ProfitStatus { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        public double? Margin { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("MarginStatus")]
        public AccountEntryStatus MarginStatus { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlArray("Trades")]
        public List<TradeData> Trades { get; set; }
    }
}
