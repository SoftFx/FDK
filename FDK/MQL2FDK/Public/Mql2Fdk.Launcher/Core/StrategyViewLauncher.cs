namespace Mql2Fdk.Launcher.Core
{
    using System;
    using System.Windows;
    using Mql2Fdk.Launcher.ViewModels;
    using Mql2Fdk.Launcher.Views;

    sealed class StrategyViewLauncher
    {
        public void LaunchStrategy(LauncherViewModel settings)
        {
            var strategy = (Strategy)Activator.CreateInstance(settings.Strategy);

            var viewModel = new StrategyViewModel();

            var launcher = new StrategyLauncher
                (
                settings.Address,
                settings.Username,
                settings.Password,
                settings.Location,
                settings.Symbol,
                settings.PriceType.Type,
                settings.Periodicity.Code,
                strategy,
                viewModel);

            viewModel.Launcher = launcher;

            launcher.Start();

            var strategyWindow = new StrategyWindow
            {
                ViewModel = viewModel,
                Owner = Application.Current.MainWindow,
                Title = strategy.GetType().Name,
            };

            strategyWindow.Show();
        }
    }
}
