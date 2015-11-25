namespace SoftFX.Extended.Financial
{
    using System;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Represents list of the most quoted currencies.
    /// </summary>
    public class CurrencyEntries : Collection<string>
    {
        #region Overrides 

        /// <summary>
        /// Inserts a currency at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which currency should be inserted.</param>
        /// <param name="currency">The currency to insert. The value cannot be null.</param>
        protected override void InsertItem(int index, string currency)
        {
            if (currency == null)
                throw new ArgumentNullException(nameof(currency));

            this.VerifyAlredyExists(currency);

            base.InsertItem(index, currency);
        }

        /// <summary>
        /// Replaces the currency at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to replace.</param>
        /// <param name="currency">The new value for the currency at the specified index. The value cannot be null.</param>
        protected override void SetItem(int index, string currency)
        {
            if (currency == null)
                throw new ArgumentNullException(nameof(currency));

            if (this[index] == currency)
                return;

            this.VerifyAlredyExists(currency);

            base.SetItem(index, currency);
        }

        void VerifyAlredyExists(string currency)
        {
            if (this.Contains(currency))
            {
                var message = string.Format("The container is already contains currency = {0}.", currency);
                throw new ArgumentException(message, nameof(currency));
            }
        }

        #endregion

        /// <summary>
        /// Exchanges two currencies.
        /// </summary>
        /// <param name="first">A zero based index of a currency</param>
        /// <param name="second">A zero based index of a currency</param>
        public void Exchange(int first, int second)
        {
            var firstItem = this.Items[first];
            this.Items[first] = this.Items[second];
            this.Items[second] = firstItem;
        }
    }
}
