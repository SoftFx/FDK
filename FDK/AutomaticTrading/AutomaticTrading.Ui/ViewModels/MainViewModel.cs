namespace AutomaticTrading.Ui.Presentation
{
    using System.Collections.Generic;
    using System.Linq;
    using SoftFX.AutomaticTrading.Hosting.Infrastructure;

    class MainViewModel
    {
        public MainViewModel(IDataSourceProviderDiscovery dataSources, IIndicatorBindingDiscovery indicators)
        {
            this.Providers = dataSources.Providers.Select(o => new DataSourceProviderViewModel(o));
            this.Indicators = indicators.Indicators.Select(o => new IndicatorViewModel(o));
            this.Data = new DataViewModel("Please Select a Data Source.");
        }

        public IEnumerable<IndicatorViewModel> Indicators { get; private set; }

        public IEnumerable<DataSourceProviderViewModel> Providers { get; private set; }

        public DataViewModel Data { get; private set; }


    }
}
