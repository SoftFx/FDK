namespace Mql2Fdk.Launcher.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Mql2Fdk.Launcher.Core;
    using Mql2Fdk.Launcher.Data;
    using Mql2Fdk.Launcher.Presentation;

    sealed class LauncherViewModel : IDataErrorInfo
    {
        readonly IDataErrorInfo validations;
        readonly StrategyViewLauncher launcher;
        readonly Settings settings;

        public LauncherViewModel(StrategyFinder finder, StrategyViewLauncher launcher, Settings settings)
        {
            if (finder == null)
                throw new ArgumentNullException("finder");

            if (launcher == null)
                throw new ArgumentNullException("launcher");

            if (settings == null)
                throw new ArgumentNullException("settings");

            this.settings = settings;

            this.validations = new LauncherViewModelValidations(this);

            this.launcher = launcher;

            this.PriceTypes = new[]
            {
                new PriceType(StrategyLauncher.PriceType.Bid, "BID"),
                new PriceType(StrategyLauncher.PriceType.Ask, "ASK"),
            };

            this.Periodicities = new[]
            {
                new Periodicity("S10", "10 Seconds"),
                new Periodicity("M1", "1 Minute"),
                new Periodicity("M5", "5 Minutes"),
                new Periodicity("M15", "15 Minutes"),
                new Periodicity("M30", "30 Minutes"),
                new Periodicity("H1", "1 Hour"),
                new Periodicity("H4", "1 Hours"),
                new Periodicity("W1", "1 Week"),
                new Periodicity("MN1", "1 Month"),
            };

            this.Strategies = finder.Strategies;

            this.LaunchCommand = new ActionCommand(this.Launch, this.CanLaunch);
        }

        public string Address
        {
            get
            {
                return this.settings.Address;
            }
            set
            {
                this.settings.Address = value;
            }
        }

        public string Username
        {
            get
            {
                return this.settings.Username;
            }
            set
            {
                this.settings.Username = value;
            }
        }

        public string Password
        {
            get
            {
                return this.settings.Password;
            }
            set
            {
                this.settings.Password = value;
            }
        }

        public string Location
        {
            get
            {
                if (this.settings.Location.Length == 0)
                    return null;

                return this.settings.Location;
            }
            set
            {
                this.settings.Location = value;
            }
        }

        public string Symbol
        {
            get
            {
                return this.settings.Symbol;
            }
            set
            {
                this.settings.Symbol = value;
            }
        }


        public PriceType PriceType
        {
            get
            {
                return this.PriceTypes.SingleOrDefault(o => o.Type == this.settings.PriceType) ?? this.PriceTypes.First();
            }
            set
            {
                this.settings.PriceType = value.Type;
            }

        }

        public Periodicity Periodicity
        {
            get
            {
                return this.Periodicities.SingleOrDefault(o => o.Code == this.settings.Periodicity) ?? this.Periodicities.First();
            }
            set
            {
                this.settings.Periodicity = value.Code;
            }
        }

        public Type Strategy
        {
            get
            {
                return this.Strategies.FirstOrDefault(o => o.Name == this.settings.Strategy) ?? this.Strategies.FirstOrDefault(); ;
            }
            set
            {
                this.settings.Strategy = value.Name;
            }
        }


        public IEnumerable<Type> Strategies { get; private set; }

        public IEnumerable<PriceType> PriceTypes { get; private set; }

        public IEnumerable<Periodicity> Periodicities { get; private set; }


        public ActionCommand LaunchCommand { get; private set; }

        void Launch()
        {
            this.launcher.LaunchStrategy(this);
        }

        bool CanLaunch()
        {
            return !string.IsNullOrWhiteSpace(this.Address)  &&
                   !string.IsNullOrWhiteSpace(this.Username) &&
                   !string.IsNullOrWhiteSpace(this.Password) &&
                   !string.IsNullOrWhiteSpace(this.Symbol);
        }


        string IDataErrorInfo.Error
        {
            get { return this.validations.Error; }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                this.LaunchCommand.Requery();
                return this.validations[columnName];
            }
        }
    }

    sealed class LauncherViewModelValidations : IDataErrorInfo
    {
        readonly LauncherViewModel viewModel;

        const string ErrorTemplateIsMandatory = "{0} is mandatory.";

        public LauncherViewModelValidations(LauncherViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException("viewModel");

            this.viewModel = viewModel;
        }

        public string this[string columnName]
        {
            get { return this.Validate(columnName); }
        }

        public string Error
        {
            get { return string.Empty; }
        }

        public string Validate(string columnName)
        {
            if (columnName == "Address")
            {
                if (string.IsNullOrWhiteSpace(this.viewModel.Address))
                    return string.Format(ErrorTemplateIsMandatory, columnName);
            }
            else if (columnName == "Username")
            {
                if (string.IsNullOrWhiteSpace(this.viewModel.Username))
                    return string.Format(ErrorTemplateIsMandatory, columnName);
            }
            else if (columnName == "Password")
            {
                if (string.IsNullOrWhiteSpace(this.viewModel.Password))
                    return string.Format(ErrorTemplateIsMandatory, columnName);
            }
            else if (columnName == "Symbol")
            {
                if (string.IsNullOrWhiteSpace(this.viewModel.Symbol))
                    return string.Format(ErrorTemplateIsMandatory, columnName);
            }

            return string.Empty;
        }
    }
}
