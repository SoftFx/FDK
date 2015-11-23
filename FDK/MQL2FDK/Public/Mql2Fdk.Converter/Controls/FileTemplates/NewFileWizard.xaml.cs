using System.Windows;

namespace Mql2Fdk.Converter.Controls.FileTemplates
{
    /// <summary>
    /// Interaction logic for NewFileWizard.xaml
    /// </summary>
    public partial class NewFileWizard
    {
        public NewFileWizard()
        {
            InitializeComponent();
        }
        public bool IsSuccessful { get; private set; }
        public string SelectedSource { get; private set; }

        void OnCancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        void OnOk(object sender, RoutedEventArgs e)
        {
            var index = projectTypeListBox.SelectedIndex;
            switch (index)
            {
                case 0:
                    SelectedSource = NewFielWizardScripts.StandardFile;
                    break;
                case 1:
                    SelectedSource = NewFielWizardScripts.ScriptFile;
                    break;
            }
            IsSuccessful = true;
            Close();
        }

    }
}
