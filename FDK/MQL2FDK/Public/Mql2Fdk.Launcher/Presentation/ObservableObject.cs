namespace Mql2Fdk.Launcher.Presentation
{
    using System;
    using System.ComponentModel;

    abstract class ObservableObject : INotifyPropertyChanged
    {
        protected ObservableObject()
        {
        }

        protected void Set<T>(ref T property, T value, string propertyName)
        {
            property = value;
            this.RaisePropertyChanged(propertyName);
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            if (propertyName == null)
                throw new ArgumentNullException("propertyName");

            var eh = this.PropertyChanged;
            if (eh != null)
                eh(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
