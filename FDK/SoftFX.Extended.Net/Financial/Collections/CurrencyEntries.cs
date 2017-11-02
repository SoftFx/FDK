namespace SoftFX.Extended.Financial
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provides access to currency entries collection.
    /// </summary>
    public class CurrencyEntries : FinancialEntries<CurrencyEntry>
    {
        #region Construction

        /// <summary>
        /// Creates a new instance of currency entires.
        /// </summary>
        /// <param name="owner">a valid instance of financial calculator</param>
        public CurrencyEntries(FinancialCalculator owner)
            : base(owner)
        {
            this.currencies = new Dictionary<string, CurrencyEntry>();
            this.HasBeenChanged = true;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a new currency entry to the container.
        /// </summary>
        /// <param name="currency">a valid currency entry</param>
        public override void Add(CurrencyEntry currency)
        {
            if (this.Any(o => o.Name == currency.Name))
            {
                var message = string.Format("Duplicate currency = {0}", currency.Name);
                throw new ArgumentException(message, nameof(currency));
            }

            base.Add(currency);
            currencies.Add(currency.Name, currency);
            this.HasBeenChanged = true;
        }

        /// <summary>
        /// Adds a new currency entry to the container.
        /// </summary>
        /// <param name="currency">currency name</param>
        public void Add(string currency)
        {
            Add(new CurrencyEntry(this.Owner as FinancialCalculator, currency, 2, 0));
        }

        /// <summary>
        /// Removes an existing currency entry from the container.
        /// </summary>
        /// <param name="currency">a valid currency entry</param>
        public override bool Remove(CurrencyEntry currency)
        {
            this.HasBeenChanged = true;
            currencies.Remove(currency.Name);
            return base.Remove(currency);
        }

        /// <summary>
        /// Removes an existing currency entry from the container.
        /// </summary>
        /// <param name="currency">currency name</param>
        public bool Remove(string currency)
        {
            var entry = TryGetCurrencyEntry(currency);
            if (entry != null)
            {
                this.HasBeenChanged = true;
                return base.Remove(entry);
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public bool Contains(string currency)
        {
            return TryGetCurrencyEntry(currency) != null;
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

        internal CurrencyEntry TryGetCurrencyEntry(string currency)
        {
            CurrencyEntry result;
            this.currencies.TryGetValue(currency, out result);
            return result;
        }

        /// <summary>
        /// Exchanges two currencies.
        /// </summary>
        /// <param name="first">A zero based index of a currency</param>
        /// <param name="second">A zero based index of a currency</param>
        public void Exchange(int first, int second)
        {
        }

        #region Internal Properties

        internal bool HasBeenChanged { get; private set; }

        #endregion

        #region Fields

        readonly IDictionary<string, CurrencyEntry> currencies;

        #endregion
    }
}
