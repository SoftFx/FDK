using System.Windows;

namespace FdkRTest.Dialogs
{
    /// <summary>
    /// Interaction logic for LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog
    {
        public LoginViewModel ViewModel
        {
            get { return (LoginViewModel)DataContext; }
        }
        public LoginDialog()
        {
            InitializeComponent();
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void OnOk(object sender, RoutedEventArgs e)
        {
            if (!ViewModel.Connect())
            {
                MessageBox.Show("Cannot connect!");
            }
            DialogResult = true;
            Close();
        }
    }
}
