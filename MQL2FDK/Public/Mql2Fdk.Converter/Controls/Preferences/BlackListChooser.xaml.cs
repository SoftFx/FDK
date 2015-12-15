using System.Windows;
using System.Windows.Controls;
using Mql2Fdk.SharedLogic;

namespace Mql2Fdk.Converter.Controls.Preferences
{
    /// <summary>
    /// Interaction logic for BlackListChooser.xaml
    /// </summary>
    public partial class BlackListChooser
    {
        public BlackListChooser()
        {
            InitializeComponent();
            ViewModel = new IncludeDirViewModel();
            var blackList = UserSettings.Default.BlacklistFiles;
            ViewModel.BlackList.AddRange(blackList.SplitByChar('|'));

            UpdateButtonsStatus();
            chooserEdit.TextChanged += (sender, evnt) => UpdateButtonsStatus();
        }

        void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (directoryNames.SelectedIndex >= 0)
                chooserEdit.Text = (string)directoryNames.SelectedItem;
            UpdateButtonsStatus();
        }

        public void SaveStates()
        {
            UserSettings.Default.BlacklistFiles = string.Join("|", ViewModel.BlackList);
        }

        void UpdateButtonsStatus()
        {
            var itemSelected = directoryNames.SelectedIndex >= 0;
            var pathSelected =
                !ViewModel.BlackList.Contains(chooserEdit.Text)
                && !string.IsNullOrEmpty(chooserEdit.Text);
            btnAdd.IsEnabled = pathSelected;
            btnRemove.IsEnabled = itemSelected;
            btnReplace.IsEnabled = itemSelected && pathSelected;
        }

        void OnAdd(object sender, RoutedEventArgs e)
        {
            ViewModel.AddBlacklistedFile(chooserEdit.Text);
            chooserEdit.Text = string.Empty;
        }

        void OnRemove(object sender, RoutedEventArgs e)
        {
            ViewModel.RemoveAtBlacklistedFile(directoryNames.SelectedIndex);
        }

        void OnReplace(object sender, RoutedEventArgs e)
        {
            ViewModel.ReplaceAtBlacklistedFile(directoryNames.SelectedIndex, chooserEdit.Text);
        }

        #region MVVM
        IncludeDirViewModel ViewModel
        {
            get
            {
                return (IncludeDirViewModel)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }
        #endregion
    }
}
