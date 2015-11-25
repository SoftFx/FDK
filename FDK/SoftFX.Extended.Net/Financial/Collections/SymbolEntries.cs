namespace SoftFX.Extended.Financial
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provides access to symbol entries collection.
    /// </summary>
    public class SymbolEntries : FinancialEntries<SymbolEntry>
    {
        #region Construction

        /// <summary>
        /// Creates a new instance of symbol entires.
        /// </summary>
        /// <param name="owner">a valid instance of financial calculator</param>
        public SymbolEntries(FinancialCalculator owner)
            : base(owner)
        {
            this.symbols = new Dictionary<string, SymbolEntry>();
            this.currencyToIndex = new Dictionary<string, int>();
            this.indexToCurrency = new List<string>();

            this.HasBeenChanged = true;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a new symbol entry to the container.
        /// </summary>
        /// <param name="symbol">a valid symbol entry</param>
        public override void Add(SymbolEntry symbol)
        {
            if (this.Any(o => o.Symbol == symbol.Symbol))
            {
                var message = string.Format("Duplicate symbol alias = {0}", symbol.Symbol);
                throw new ArgumentException(message, nameof(symbol));
            }

            base.Add(symbol);
            this.HasBeenChanged = true;
        }

        /// <summary>
        /// Removes an existing symbol entry from the container.
        /// </summary>
        /// <param name="symbol">a valid symbol entry</param>
        public override bool Remove(SymbolEntry symbol)
        {
            this.HasBeenChanged = true;
            return base.Remove(symbol);
        }

        /// <summary>
        /// Removes all existing entries.
        /// </summary>
        public override void Clear()
        {
            base.Clear();
            this.HasBeenChanged = true;
        }

        #endregion

        #region Internal methods

        internal int TryGetCurrencyIndex(string currency)
        {
            int result;
            if (!this.currencyToIndex.TryGetValue(currency, out result))
            {
                result = -1;
            }
            return result;
        }

        internal string GetCurrencyFromIndex(int index)
        {
            return this.indexToCurrency[index];
        }

        internal SymbolEntry TryGetSymbolEntry(string symbol)
        {
            SymbolEntry result;
            this.symbols.TryGetValue(symbol, out result);
            return result;
        }

        internal void MakeIndex()
        {
            if (!this.HasBeenChanged)
                return;

            this.symbols.Clear();
            this.currencyToIndex.Clear();
            this.indexToCurrency.Clear();

            foreach (var element in this)
            {
                this.symbols[element.Symbol] = element;
                element.FromIndex = this.IndexFromCurrency(element.From);
                element.ToIndex = this.IndexFromCurrency(element.To);
            }
            this.HasBeenChanged = false;
        }

        int IndexFromCurrency(string currency)
        {
            int result;
            if (!this.currencyToIndex.TryGetValue(currency, out result))
            {
                result = this.currencyToIndex.Count;
                this.currencyToIndex[currency] = result;
                this.indexToCurrency.Add(currency);
            }
            return result;
        }

        #endregion

        #region Internal Properties

        internal bool HasBeenChanged { get; private set; }

        internal int CurrenciesCount
        {
            get
            {
                return this.currencyToIndex.Count;
            }
        }

        internal IEnumerable<string> Currencies
        {
            get
            {
                return this.currencyToIndex.Keys;
            }
        }

        #endregion

        #region Fields

        readonly IDictionary<string, SymbolEntry> symbols;
        readonly IDictionary<string, int> currencyToIndex;
        readonly IList<string> indexToCurrency;

        #endregion
    }
}
