using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Mql2Fdk.Converter.Common
{
    class NotificationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }


        protected void OnPropertyChanged<T>(Expression<Func<T>> propertyName)
        {
            var body = propertyName.Body as MemberExpression;
            if (body == null)
                throw new ArgumentException("'propertyExpression' should be a member expression");

            OnPropertyChanged(body.Member.Name);
        }
    }
}