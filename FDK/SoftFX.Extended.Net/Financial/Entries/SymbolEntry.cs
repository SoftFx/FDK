namespace SoftFX.Extended.Financial
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// Represents normalized symbol in the following format Symbol = To / From.
    /// </summary>
    [DebuggerDisplay("{Symbol} = {To} / {From}")]
    public class SymbolEntry : FinancialEntry
    {
        #region Construction

        /// <summary>
        /// Creates a new symbol entry from symbol in XXX/YYY format.
        /// </summary>
        /// <param name="owner">valid instance of financial calculator</param>
        /// <param name="symbol">symbol inf XXX/YYY format</param>
        public SymbolEntry(FinancialCalculator owner, string symbol)
            : base(owner)
        {
            if (symbol == null)
                throw new ArgumentNullException(nameof(symbol));

            var from = string.Empty;
            var to = string.Empty;
            var index = symbol.IndexOf('/');

            if (index != -1)
            {
                to = symbol.Substring(0, index);
                from = symbol.Substring(1 + index, symbol.Length - index - 1);
            }

            if (index == -1 || from.Contains('/') || to.Contains('/'))
            {
                var message = string.Format("Symbol = {0} should has XXX/YYY format", symbol);
                throw new ArgumentException(message, nameof(symbol));
            }

            this.Symbol = symbol;
            this.From = from;
            this.To = to;

            this.MarginCalcMode = MarginCalcMode.Forex;
        }

        /// <summary>
        /// Creates a new symbol entry.
        /// </summary>
        /// <param name="owner">va</param>
        /// <param name="symbol">Financial instrument name</param>
        /// <param name="profitCurrency">Profit currency</param>
        /// <param name="marginCurrency">Trade currency</param>
        public SymbolEntry(FinancialCalculator owner, string symbol, string profitCurrency, string marginCurrency)
            : base(owner)
        {
            if (symbol == null)
                throw new ArgumentNullException(nameof(symbol));

            if (profitCurrency == null)
                throw new ArgumentNullException(nameof(profitCurrency));

            if (marginCurrency == null)
                throw new ArgumentNullException(nameof(marginCurrency));

            this.Symbol = symbol;
            this.From = profitCurrency;
            this.To = marginCurrency;

            this.MarginCalcMode = MarginCalcMode.Forex;
        }

        #endregion

        #region Symbols Properties

        /// <summary>
        /// Gets symbol name of financial instrument.
        /// </summary>
        [Category("Parameters")]
        public string Symbol { get; private set; }

        /// <summary>
        /// Gets profit currency.
        /// </summary>
        [Category("Parameters")]
        [DisplayName("Profit Currency")]
        public string ProfitCurrency
        {
            get
            {
                return this.From;
            }
        }

        internal string From { get; private set; }

        /// <summary>
        /// Gets margin currency.
        /// </summary>
        [Category("Parameters")]
        [DisplayName("Margin Currency")]
        public string MarginCurrency
        {
            get
            {
                return this.To;
            }
        }

        internal string To { get; private set; }

        /// <summary>
        /// Gets or sets optional contract size
        /// </summary>
        [Category("Parameters")]
        [DisplayName("Contract Size")]
        public double ContractSize
        {
            get
            {
                return this.contractSize;
            }
            set
            {
                if (double.IsNaN(value) || double.IsInfinity(value) || value <= 0)
                {
                    var message = string.Format("Contract size should be positive");
                    throw new ArgumentOutOfRangeException(nameof(value), value, message);
                }
                this.contractSize = value;
            }
        }

        /// <summary>
        /// Gets margin calculation mode of the symbol.
        /// </summary>
        [Category("Parameters")]
        [DisplayName("Margin Calculation Mode")]
        public MarginCalcMode MarginCalcMode {get; set;}

        #endregion

        #region Internal Properties

        public int GroupSortOrder { get; internal set; }
        public int SortOrder { get; internal set; }

        internal int FromIndex { get; set; }
        internal int ToIndex { get; set; }

        #endregion

        #region Coefficients of Margin Equation

        /// <summary>
        /// Gets or sets of hedging.
        /// </summary>
        [Category("Parameters")]
        public double Hedging
        {
            get
            {
                return this.hedging;
            }
            set
            {
                this.hedging = CheckAndReturnMarginFactor(value, "hedging");
            }
        }

        /// <summary>
        /// Gets or sets symbol margin factor.
        /// </summary>
        [Category("Parameters")]
        [DisplayName("Symbol Margin Factor")]
        public double MarginFactor
        {
            get
            {
                return this.marginFactor;
            }
            set
            {
                this.marginFactor = value;
            }
        }


        /// <summary>
        /// Gets or sets margin factor of positions.
        /// </summary>
        [Category("Parameters")]
        [DisplayName("Margin Factor of Positions")]
        public double MarginFactorOfPositions
        {
            get
            {
                return this.marginFactorOfPositions;
            }
            set
            {
                this.marginFactorOfPositions = CheckAndReturnMarginFactor(value, "margin factor of positions");
            }
        }

        /// <summary>
        /// Gets or sets margin factor of limit orders.
        /// </summary>
        [Category("Parameters")]
        [DisplayName("Margin Factor of Limit Orders")]
        public double MarginFactorOfLimitOrders
        {
            get
            {
                return this.marginFactorOfLimitOrders;
            }
            set
            {
                this.marginFactorOfLimitOrders = CheckAndReturnMarginFactor(value, "margin factor of limit orders");
            }
        }

        /// <summary>
        /// Gets or sets margin factor of stop orders.
        /// </summary>
        [Category("Parameters")]
        [DisplayName("Margin Factor of Stop Orders")]
        public double MarginFactorOfStopOrders
        {
            get
            {
                return this.marginFactorOfStopOrders;
            }
            set
            {
                this.marginFactorOfStopOrders = CheckAndReturnMarginFactor(value, "margin factor of stop orders");
            }
        }

        /// <summary>
        /// Gets or sets margin factor of limit orders.
        /// </summary>
        [Category("Parameters")]
        [DisplayName("Stop Order Margin Reduction")]
        public double? StopOrderMarginReduction
        {
            get
            {
                return this.stopOrderMarginReduction;
            }
            set
            {
                this.stopOrderMarginReduction = value;
            }
        }

        /// <summary>
        /// Gets or sets margin factor of limit orders.
        /// </summary>
        [Category("Parameters")]
        [DisplayName("Hidden Limit Order Margin Reduction")]
        public double? HiddenLimitOrderMarginReduction
        {
            get
            {
                return this.hiddenLimitOrderMarginReduction;
            }
            set
            {
                this.hiddenLimitOrderMarginReduction = value;
            }
        }

        static double CheckAndReturnMarginFactor(double value, string name)
        {
            if (double.IsNaN(value) || double.IsInfinity(value) || value < 0 || value > 1)
            {
                var message = string.Format("{0} should be not less than zero and not more than 1", name);
                throw new ArgumentOutOfRangeException(nameof(value), value, message);
            }
            return value;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Returns a formatted string to simplify debugging.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var result = string.Format("{0} = {1}/{2}", this.Symbol, this.To, this.From);
            return result;
        }

        #endregion

        #region Fields

        double contractSize = 1;
        double marginFactor = 1;
        double marginFactorOfPositions = 1;
        double marginFactorOfLimitOrders = 1;
        double marginFactorOfStopOrders = 1;
        double hedging = 1;
        double? stopOrderMarginReduction = null;
        double? hiddenLimitOrderMarginReduction = null;

        #endregion
    }
}
