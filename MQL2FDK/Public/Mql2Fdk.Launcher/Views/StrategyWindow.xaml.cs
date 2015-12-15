namespace Mql2Fdk.Launcher.Views
{
    using System;
    using System.ComponentModel;
    using Mql2Fdk.Launcher.ViewModels;

    /// <summary>
    /// Interaction logic for StrategyWindow.xaml
    /// </summary>
    partial class StrategyWindow
    {
        StrategyViewModel viewModel;

        public StrategyWindow()
        {
            this.InitializeComponent();
        }

        void OnViewModelClose()
        {
            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (this.viewModel != null)
                this.viewModel.Stop();
        }

        public StrategyViewModel ViewModel
        {
            get
            {
                return this.viewModel;
            }
            set
            {
                if (this.viewModel != null)
                    this.viewModel.Close -= this.OnViewModelClose;

                this.Content = this.viewModel = value;
                this.viewModel.Close += this.OnViewModelClose;
            }
        }

        void OnViewModelClose(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
