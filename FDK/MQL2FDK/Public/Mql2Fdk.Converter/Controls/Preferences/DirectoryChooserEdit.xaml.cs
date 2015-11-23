using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Mql2Fdk.Converter.Common;

namespace Mql2Fdk.Converter.Controls.Preferences
{
    /// <summary>
    /// Interaction logic for DirectoryChooserEdit.xaml
    /// </summary>
    public partial class DirectoryChooserEdit
    {
        public DirectoryChooserEdit()
        {
            InitializeComponent();
        }

        public UiEvent.OnUiEvent OnEditText { get; set; }
        void OnBrowse(object sender, RoutedEventArgs e)
        {
            var browser = new FolderBrowserDialog();
            var result = browser.ShowDialog();
            if(result==DialogResult.Cancel)
                return;
            SelectedPath = browser.SelectedPath;
        }

        public string SelectedPath
        {
            get
            {
                return fileBox.Text;
            }
            set
            {
                fileBox.Text = value;
            }
        }

        void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            OnEditText.Notify();
        }
    }
}
