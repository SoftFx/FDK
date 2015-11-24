namespace Mql2Fdk
{
    using System.Configuration;
    using SoftFX.Extended;

    /// <summary>
    /// Provides methods to initialize, start and stop an adviser.
    /// </summary>
    public class MqlLauncher : StrategyLauncher

    {
        /// <summary>
        /// Starts execution of MqlAdapter.
        /// </summary>
        /// <param name="address">Server address.</param>
        /// <param name="username">User name.</param>
        /// <param name="password">Password.</param>
        /// <param name="location">Storage and logs location.</param>
        /// <param name="symbol">Symbol.</param>
        /// <param name="priceType">Price type.</param>
        /// <param name="periodicity">Periodicity.</param>
        /// <param name="adapter">Mql adapter.</param>
        public MqlLauncher(string address, string username, string password, string location, string symbol, PriceType priceType, string periodicity, MqlAdapter adapter)
            : base(address, username, password, location, symbol, priceType, periodicity, adapter)
        {
        }

        /// <summary>
        /// Starts execution of MqlAdapter.
        /// </summary>
        /// <param name="settings">Settings to look for configuration</param>
        /// <param name="adapter">Mql adapter.</param>
        public MqlLauncher(SettingsBase settings, MqlAdapter adapter)
            : base(settings, adapter)
        {

        }
    }
}
