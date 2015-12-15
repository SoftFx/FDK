using System.IO;
using System.Windows;
using System.Windows.Controls;
using Mql2Fdk.SharedLogic;

namespace Mql2Fdk.Converter.Controls.Preferences
{
    /// <summary>
    /// Interaction logic for IncludeDirPicker.xaml
    /// </summary>
    public partial class IncludeDirPicker
    {
        public IncludeDirPicker()
        {
            InitializeComponent();
            ViewModel = new IncludeDirViewModel();
            var includeDirectories = UserSettings.Default.IncludeDirectories;
            ViewModel.Includes.AddRange(includeDirectories.SplitByChar('|'));

            var blackList = UserSettings.Default.BlacklistFiles;
            ViewModel.BlackList.AddRange(blackList.SplitByChar('|'));

            UpdateButtonsStatus();
            chooserEdit.OnEditText += data => UpdateButtonsStatus();
        }

        void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (directoryNames.SelectedIndex >= 0)
                chooserEdit.SelectedPath = (string)directoryNames.SelectedItem;
            UpdateButtonsStatus();
        }

        public void SaveStates()
        {
            UserSettings.Default.IncludeDirectories = string.Join("|", ViewModel.Includes);
        }

        void UpdateButtonsStatus()
        {
            var itemSelected = directoryNames.SelectedIndex >= 0;
            var pathSelected = Directory.Exists(chooserEdit.SelectedPath) && !ViewModel.Includes.Contains(chooserEdit.SelectedPath);
            btnAdd.IsEnabled = pathSelected;
            btnRemove.IsEnabled = itemSelected;
            btnReplace.IsEnabled = itemSelected && pathSelected;
        }

        void OnAdd(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(chooserEdit.SelectedPath))
            {
                MessageBox.Show(string.Format("Folder: '{0}' does not exist",
                    chooserEdit.SelectedPath));
                return;
            }
            ViewModel.AddInclude(chooserEdit.SelectedPath);
            chooserEdit.SelectedPath = string.Empty;
        }

        void OnRemove(object sender, RoutedEventArgs e)
        {
            ViewModel.RemoveAtInclude(directoryNames.SelectedIndex);
        }

        void OnReplace(object sender, RoutedEventArgs e)
        {
            ViewModel.ReplaceAtInclude(directoryNames.SelectedIndex, chooserEdit.SelectedPath);
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
