namespace SoftFX.Extended.Financial.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;

    /// <summary>
    /// For internal usage only
    /// </summary>
    [XmlRoot("Calculator")]
    public class CalculatorData
    {
        /// <summary>
        /// For internal usage only
        /// </summary>
        public CalculatorData() 
        {
            this.Prices = new List<PriceData>();
            this.Symbols = new List<SymbolData>();
            this.Accounts = new List<AccountData>();
            this.Currencies = new List<string>();
        }

        internal CalculatorData(FinancialCalculator calculator)
            : this()
        {
            if (calculator == null)
                throw new ArgumentNullException("calculator");

            //this.MarginMode = calculator.MarginMode;

            this.Prices.AddRange(calculator.Prices.Select(o => new PriceData(o)));

            this.Symbols.AddRange(calculator.Symbols.Select(o => new SymbolData(o)));

            this.Accounts.AddRange(calculator.Accounts.Select(o => new AccountData(o)));

            this.Currencies.AddRange(calculator.Currencies);
        }

        internal FinancialCalculator CreateCalculator()
        {
            var result = new FinancialCalculator();
            //{
            //    MarginMode = this.MarginMode
            //};

            foreach (var element in this.Prices)
            {
                result.Prices.Update(element.Symbol, element.Bid, element.Ask);
            }

            foreach (var element in this.Symbols)
            {
                var entry = element.CreateEntry(result);
                result.Symbols.Add(entry);
            }

            foreach (var element in this.Accounts)
            {
                var entry = element.CreateEntry(result);
                result.Accounts.Add(entry);
            }

            foreach (var element in this.Currencies)
            {
                result.Currencies.Add(element);
            }

            return result;
        }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlAttribute("MarginMode")]
        public MarginMode MarginMode { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlArray("Prices")]
        public List<PriceData> Prices { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlArray("Symbols")]
        public List<SymbolData> Symbols { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlArray("Accounts")]
        public List<AccountData> Accounts { get; set; }

        /// <summary>
        /// For internal usage only
        /// </summary>
        [XmlArray("Currencies")]
        public List<string> Currencies { get; set; }
    }
}
