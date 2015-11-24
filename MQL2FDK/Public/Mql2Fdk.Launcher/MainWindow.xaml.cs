namespace Mql2Fdk.Launcher
{
    using Mql2Fdk.Launcher.Core;
    using Mql2Fdk.Launcher.ViewModels;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.Content = new LauncherViewModel(new StrategyFinder(Settings.Default.StrategiesLocation), new StrategyViewLauncher(), Settings.Default);
        }
    }
}
