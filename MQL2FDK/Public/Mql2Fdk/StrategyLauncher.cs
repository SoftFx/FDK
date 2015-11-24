namespace Mql2Fdk
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using SoftFX.Basic;
    using SoftFX.Extended;

    using ApiPriceType = SoftFX.Extended.PriceType;


    /// <summary>
    /// Provides methods to initialize, start and stop an adviser.
    /// </summary>
    public class StrategyLauncher : IDisposable
    {
        /// <summary>
        /// Starts execution of strategy.
        /// </summary>
        /// <param name="address">Server address.</param>
        /// <param name="username">User name.</param>
        /// <param name="password">Password.</param>
        /// <param name="location">Storage and logs location.</param>
        /// <param name="symbol">Symbol.</param>
        /// <param name="priceType">Price type.</param>
        /// <param name="periodicity">Periodicity.</param>
        /// <param name="strategy">Strategy.</param>
        /// <param name="log">Logger.</param>
        public StrategyLauncher(string address, string username, string password, string location, string symbol, PriceType priceType, string periodicity, Strategy strategy, IStrategyLog log = null)
        {
            if (address == null)
                throw new ArgumentNullException("address");

            if (username == null)
                throw new ArgumentNullException("username");

            if (password == null)
                throw new ArgumentNullException("password");

            if (periodicity == null)
                throw new ArgumentNullException("periodicity");

            if (strategy == null)
                throw new ArgumentNullException("strategy");

            this.strategy = strategy;
            this.log = log ?? DefaultStrategyLog.Instance;

            var logsLocation = string.Empty;

            if (location == null)
            {
                location = Assembly.GetExecutingAssembly().Location;
                location = Path.GetDirectoryName(location);
                location = Path.Combine(location, "Advisers");
                Directory.CreateDirectory(location);
                location = Path.Combine(location, address);
                Directory.CreateDirectory(location);
                location = Path.Combine(location, username);
                Directory.CreateDirectory(location);
            }
            else if (!string.IsNullOrEmpty(location))
            {
                logsLocation = Path.Combine(location, "Logs");
                Directory.CreateDirectory(logsLocation);
            }

            var connections = new FixConnectionStrings(address, username, password, logsLocation);

            this.manager = new Manager(connections.TradeConnectionString, connections.FeedConnectionString, location);
            this.strategy.Initialize(this.manager, this.log, symbol, priceType == PriceType.Bid ? ApiPriceType.Bid : ApiPriceType.Ask, new BarPeriod(periodicity));
        }

        /// <summary>
        /// Starts execution of strategy.
        /// </summary>
        /// <param name="settings">Settings to look for configuration</param>
        /// <param name="strategy">Strategy.</param> 
        /// <param name="log">Logger.</param>
        public StrategyLauncher(SettingsBase settings, Strategy strategy, IStrategyLog log = null)
            : this
            (
                GetSetting<string>(settings, "Address"),
                GetSetting<string>(settings, "Username"),
                GetSetting<string>(settings, "Password"),
                GetSetting<string>(settings, "Location", throwIfMissing: false),
                GetSetting<string>(settings, "Symbol"),
                GetSetting<PriceType>(settings, "PriceType"),
                GetSetting<string>(settings, "Periodicity"),
                strategy,
                log
            )
        {
        }

        /// <summary>
        /// Starts adviser.
        /// </summary>
        public void Start()
        {
            this.manager.Start();
            this.strategy.Start();
        }

        /// <summary>
        /// Stops adviser.
        /// </summary>
        public void Stop()
        {
            this.strategy.Stop();
            this.manager.Stop();
        }

        /// <summary>
        /// Closes all connections and flushes all data.
        /// </summary>
        public void Dispose()
        {
            var manager = this.manager;
            if (manager != null)
                manager.Dispose();
        }

        static T GetSetting<T>(SettingsBase settings, string name, bool throwIfMissing = true)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");

            if (name == null)
                throw new ArgumentNullException("name");

            var value = default(T);

            try
            {
                value = (T)settings[name];
            }
            catch (SettingsPropertyNotFoundException)
            {
                if (throwIfMissing)
                    throw;
            }

            return value;
        }

        #region Members

        readonly IStrategyLog log;
        readonly Strategy strategy;
        readonly Manager manager;

        #endregion

        public enum PriceType
        {
            Bid,
            Ask
        }

        sealed class DefaultStrategyLog : IStrategyLog
        {
            public static readonly IStrategyLog Instance = new DefaultStrategyLog();

            DefaultStrategyLog()
            {
            }

            public void Alert(params object[] args)
            {
                Console.WriteLine(args);
            }

            public void Comment(params object[] args)
            {
                Debug.WriteLine(args);
            }

            public void Print(params object[] args)
            {
                Console.WriteLine(args);
            }
        }
    }
}

