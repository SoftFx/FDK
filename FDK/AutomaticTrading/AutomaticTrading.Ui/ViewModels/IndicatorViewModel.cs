namespace AutomaticTrading.Ui.Presentation
{
    using SoftFX.AutomaticTrading.Hosting.Indicators;

    class IndicatorViewModel
    {
        readonly IIndicatorBinding binding;

        public IndicatorViewModel(IIndicatorBinding binding)
        {
            this.binding = binding;
        }

        public string Name
        {
            get
            {
                return this.binding.Name;
            }
        }

        public string Description
        {
            get
            {
                return this.binding.Descrption;
            }
        }
   }
}
