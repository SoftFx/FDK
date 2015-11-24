namespace SoftFX.Extended.Financial
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provides access to price entries collection.
    /// </summary>
    public class PriceEntries : IEnumerable<KeyValuePair<string, PriceEntry>>
    {
        #region Construction

        /// <summary>
        /// Creates a new instance of price entires.
        /// </summary>
        public PriceEntries()
        {
            this.entries = new SortedDictionary<string, PriceEntry>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public PriceEntry? TryGetPriceEntry(string symbol)
        {
            var result = default(PriceEntry);
            if (this.entries.TryGetValue(symbol, out result))
                return result;

            return null;
        }

        /// <summary>
        /// Gets all prices as dictionary.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, PriceEntry> ToDictionary()
        {
            var result = this.entries.ToDictionary(k => k.Key, v => v.Value);
            return result;
        }

        /// <summary>
        /// Updates bid/ask prices for a symbol.
        /// </summary>
        /// <param name="symbol">an updating symbol</param>
        /// <param name="bid">a bid price</param>
        /// <param name="ask">an ask price</param>
        public void Update(string symbol, double bid, double ask)
        {
            this.entries[symbol] = new PriceEntry(bid, ask);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prices"></param>
        public void Update(IEnumerable<KeyValuePair<string, PriceEntry>> prices)
        {
            foreach (var element in prices)
            {
                this.entries[element.Key] = element.Value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        public void Remove(string symbol)
        {
            this.entries.Remove(symbol);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            this.entries.Clear();
        }

        #endregion

        #region IEnumerable interface implementation

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<string, PriceEntry>> GetEnumerator()
        {
            return this.entries.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.entries.GetEnumerator();
        }

        #endregion

        #region Fields

        readonly SortedDictionary<string, PriceEntry> entries;

        #endregion
    }
}
