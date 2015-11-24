using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace FdkRTest.Dialogs
{
    public class NotificationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event 

        private void ChangedName(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        protected void Changed([CallerMemberName] string name = null)
        {
            ChangedName(name);
        }

        public void Changed<T>(Expression<Func<T>> propertyName)
        {
            var body = propertyName.Body as MemberExpression;
            if (body == null)
                throw new ArgumentException("'propertyExpression' should be a member expression");

            ChangedName(body.Member.Name);
        }
    }
}