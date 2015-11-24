namespace AutomaticTrading.Ui
{
    using System.Windows;
    using AutomaticTrading.Ui.Discovery;
    using AutomaticTrading.Ui.Presentation;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            var locator = new Locator();
            this.DataContext = new MainViewModel(locator, locator);
        }
    }
}
