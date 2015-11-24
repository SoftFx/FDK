using System.Windows;
using FdkRTest.Dialogs;

namespace FdkRTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = new MainWindow();
            mainWindow.Setup();
            
            mainWindow.ShowDialog();
        }
    }
}
