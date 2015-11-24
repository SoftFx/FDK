namespace SoftFX.Extended.Financial
{
    using System.Collections.Generic;
    using System.Linq;

    sealed class DefaultPrecision : IPrecisionProvider
    {
        const int DefaultCryptoCurrencyPrecision = 5;
        const int DefaultRegularCurrencyPrecision = 2;
        
        static readonly IEnumerable<string> CryptoCurrencies = new SortedSet<string> { "BTC", "NMC", "LTC", "PPC" };

        public static readonly IPrecisionProvider Instance = new DefaultPrecision();

        DefaultPrecision()
        {
        }

        public int GetCurrencyPrecision(string currency)
        {
            if (CryptoCurrencies.Contains(currency))
                return DefaultCryptoCurrencyPrecision;

            return DefaultRegularCurrencyPrecision;
        }
    }
}
