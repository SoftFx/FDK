namespace AutomaticTrading.Ui.Presentation
{
    using SoftFX.AutomaticTrading.Hosting.DataSources;

    class DataSourceViewModel
    {
        readonly IDataSource dataSource;

        public DataSourceViewModel(IDataSource dataSource)
        {
            this.dataSource = dataSource;
        }

        public string Name
        {
            get
            {
                return this.dataSource.Name;
            }
        }
    }
}
