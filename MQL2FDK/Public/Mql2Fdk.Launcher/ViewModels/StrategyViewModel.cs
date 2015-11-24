namespace Mql2Fdk.Launcher.ViewModels
{
    using System;
    using System.Text;
    using Mql2Fdk.Launcher.Presentation;

    sealed class StrategyViewModel : ObservableObject, IStrategyLog
    {
        readonly StringBuilder logBuilder;

        public StrategyLauncher Launcher { get; set; }

        public StrategyViewModel()
        {
            this.logBuilder = new StringBuilder();
            this.CloseCommand = new ActionCommand(this.RaiseCloseEvent);
        }

        public string LogText
        {
            get
            {
                lock (this.logBuilder)
                {
                    return this.logBuilder.ToString();
                }
            }
        }

        public ActionCommand CloseCommand { get; private set; }

        void RaiseCloseEvent()
        {
            var eh = this.Close;
            if (eh != null)
                eh(this, EventArgs.Empty);
        }

        public void Stop()
        {
            if (this.Launcher == null)
                return;

            this.Launcher.Stop();
            this.Launcher.Dispose();
        }

        public event EventHandler Close;


        void IStrategyLog.Alert(params object[] args)
        {
            this.Log("ALERT", args);
        }

        void IStrategyLog.Comment(params object[] args)
        {
            this.Log("COMMENT", args);
        }

        void IStrategyLog.Print(params object[] args)
        {
            this.Log("PRINT", args);
        }

        void Log(string category, params object[] args)
        {
            lock (this.logBuilder)
            {
                foreach (var arg in args)
                {
                    this.logBuilder.AppendFormat("{0} {1}: {2}", DateTime.Now, category, arg);
                    this.logBuilder.AppendLine();
                }
            }
            this.RaisePropertyChanged("LogText");
        }
    }
}
