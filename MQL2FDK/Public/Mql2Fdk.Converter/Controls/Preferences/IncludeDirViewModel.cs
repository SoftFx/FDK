using System.Collections.ObjectModel;
using Mql2Fdk.Converter.Common;

namespace Mql2Fdk.Converter.Controls.Preferences
{
    class IncludeDirViewModel : NotificationViewModel
    {
        ObservableCollection<string> _includes;
        ObservableCollection<string> _blackList;

        public IncludeDirViewModel()
        {
            Includes = new ObservableCollection<string>();
            BlackList = new ObservableCollection<string>();
        }

        public ObservableCollection<string> Includes
        {
            get { return _includes; }
            set
            {
                _includes = value;
                OnPropertyChanged(() => Includes);
            }
        }

        public ObservableCollection<string> BlackList
        {
            get { return _blackList; }
            set
            {
                _blackList = value;
                OnPropertyChanged(()=>BlackList);
            }
        }

        public void AddInclude(string folder)
        {
            if (Includes.Contains(folder)) return;
            Includes.Add(folder);
        }

        public void RemoveAtInclude(int index)
        {
            Includes.RemoveAt(index);
        }

        public void ReplaceAtInclude(int index, string newText)
        {
            Includes[index] = newText;
        }

        public void AddBlacklistedFile(string folder)
        {
            if (BlackList.Contains(folder)) return;
            BlackList.Add(folder);
        }

        public void RemoveAtBlacklistedFile(int index)
        {
            BlackList.RemoveAt(index);
        }

        public void ReplaceAtBlacklistedFile(int index, string newText)
        {
            BlackList[index] = newText;
        }
    }
}