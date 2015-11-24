namespace Mql2Fdk.Launcher.Presentation
{
    using System;
    using System.Windows.Input;

    class ActionCommand : ICommand
    {
        readonly Action action;
        readonly Func<bool> canExecute;

        public ActionCommand(Action action, Func<bool> canExecute = null)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            this.action = action;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return (canExecute ?? (() => true))();
        }

        public void Execute(object parameter)
        {
            this.action();
        }

        public void Requery()
        {
            var eh = this.CanExecuteChanged;
            if (eh != null)
                eh(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;
    }
}
