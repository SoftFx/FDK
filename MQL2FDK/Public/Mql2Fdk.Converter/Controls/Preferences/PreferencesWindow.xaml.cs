using System.Windows;

namespace Mql2Fdk.Converter.Controls.Preferences
{
    /// <summary>
    /// Interaction logic for PreferencesWindow.xaml
    /// </summary>
    public partial class PreferencesWindow
    {
        public PreferencesWindow()
        {
            InitializeComponent();
        }

        void OnClose(object sender, RoutedEventArgs e)
        {
            IncludeDirPicker.SaveStates();
            BlackListChooser.SaveStates();
            UserSettings.Default.Save();
            Close();
        }
    }
}
