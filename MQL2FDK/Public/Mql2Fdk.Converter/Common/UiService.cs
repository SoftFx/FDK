using System;
using System.Windows;
using System.Windows.Threading;

namespace Mql2Fdk.Converter.Common
{
    public static class UiService
    {
        static UiService()
        {
            Dispatcher = Application.Current.Dispatcher;
        }

        public static Dispatcher Dispatcher { get; private set; }

        public static void InvokeMainThread(this Action action)
        {
            if (!Dispatcher.CheckAccess())
                Dispatcher.Invoke(action);
            else
                action();
        }
		public static void InvokeMainThread<T>(this Action<T> action, T arg)
		{
			if (!Dispatcher.CheckAccess())
				Dispatcher.Invoke(action, arg);
			else
				action(arg);
		}


        public static Visibility ToVisibility(this bool pathSelected)
        {
            return pathSelected ? Visibility.Visible : Visibility.Collapsed;
        }

        public static void Notify(this UiEvent.OnUiEvent evnt, object data = null)
        {
            if (evnt != null)
                evnt.Invoke(data);
        }

    }
}