namespace AutomaticTrading.Ui.Discovery
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using AutomaticTrading.Sources.DataSources;
    using AutomaticTrading.Sources.Indicators;
    using SoftFX.AutomaticTrading.Hosting.DataSources;
    using SoftFX.AutomaticTrading.Hosting.Indicators;
    using SoftFX.AutomaticTrading.Hosting.Infrastructure;

    sealed class Locator : IDataSourceProviderDiscovery, IIndicatorBindingDiscovery
    {
        public Locator()
        {
            this.Providers = new IDataSourceProvider[]
                {
                    new StorageDataSourceProvider(),
                };

            this.Indicators = new IIndicatorBinding[]
                {
                    new SimpleMovingAverageBinding(),
                    new WeightedMovingAverageBinding(),
                    new ExponentialMovingAverageBinding(),
                    new IndicatorBindingFactory(typeof(ExampleIndicator)).GetBinding()
                };


            this.LogItems();
        }

        public IEnumerable<IDataSourceProvider> Providers { get; private set; }

        public IEnumerable<IIndicatorBinding> Indicators { get; private set; }

        [Conditional("DEBUG")]
        void LogItems()
        {
            foreach (var provider in this.Providers)
            {
                Debug.WriteLine(string.Format("Found provider {0}.", provider.Name), "PROVIDERS");
            }

            foreach (var binding in this.Indicators)
            {
                Debug.WriteLine(string.Format("Found binding {0}.", binding.Name), "BINDING");
            }
        }
    }
}
