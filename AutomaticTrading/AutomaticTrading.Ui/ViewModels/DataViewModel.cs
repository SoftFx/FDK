namespace AutomaticTrading.Ui.Presentation
{
    class DataViewModel
    {
        public DataViewModel(string name)
        {
            this.Info = name;
        }

        public string Info { get; private set; }

        public string Data { get; private set; }
    }
}
