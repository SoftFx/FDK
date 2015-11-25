namespace SoftFX.Extended.Financial
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    sealed class CurrencyPrecisionProvider : IPrecisionProvider
    {
        readonly IDictionary<string, int> precisions;

        public CurrencyPrecisionProvider(IEnumerable<CurrencyInfo> currencies)
        {
            if (currencies == null)
                throw new ArgumentNullException(nameof(currencies));

            this.precisions = currencies.ToDictionary(o => o.Name, o => o.Precision);
        }

        public int GetCurrencyPrecision(string currency)
        {
            if (currency == null)
                throw new ArgumentNullException(nameof(currency));

            return this.precisions[currency];
        }
    }
}
