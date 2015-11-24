namespace AutomaticTrading.Ui.Presentation
{
    using System.Collections.Generic;
    using System.Linq;
    using SoftFX.AutomaticTrading.Hosting.DataSources;

    class DataSourceProviderViewModel
    {
        readonly IDataSourceProvider provider;

        public DataSourceProviderViewModel(IDataSourceProvider provider)
        {
            this.provider = provider;

            this.DataSources = this.provider.DataSources.Select(o => new DataSourceViewModel(o));
        }

        public string Name
        {
            get
            {
                return this.provider.Name;
            }
        }

        public IEnumerable<DataSourceViewModel> DataSources { get; private set; }
    }
}
