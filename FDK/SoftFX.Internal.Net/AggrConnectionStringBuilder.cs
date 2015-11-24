namespace SoftFX.Internal
{
    using SoftFX.Extended;

    /// <summary>
    /// Represents Feeder/Bridge connection parameters.
    /// </summary>
    public class AggrConnectionStringBuilder : ConnectionStringBuilder
    {
        #region Constants

        public static readonly string Feed = "Feed";
        public static readonly string Trade = "Trade";

        #endregion

        /// <summary>
        /// Creates a new instance of AggrConnectionStringBuilder
        /// </summary>
        public AggrConnectionStringBuilder()
        {
            this.FeederAddress = "127.0.0.1";
            this.FeederPort = 22060;
            this.BridgeAddress = "127.0.0.1";
            this.BridgePort = 22090;
        }

        /// <summary>
        /// Gets protocol type name = "Aggr".
        /// </summary>
        public override string ProtocolType
        {
            get
            {
                return "Aggr";
            }
        }

        /// <summary>
        /// Type of connection: feed or trade
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Use BankCode parameter of a bank section from Feeder/Bridge config file.
        /// </summary>
        public int BankCode { get; set; }

        #region Feeder Parameters
        /// <summary>
        /// IP address or DNS name of Feeder service.
        /// </summary>
        public string FeederAddress { get; set; }

        /// <summary>
        /// Use port specified by QuotesServerPort2 parameter of Feeder config.
        /// </summary>
        public int FeederPort { get; set; }

        /// <summary>
        /// User username specified by QuotesServerLogin2 parameter of Feeder config.
        /// </summary>
        public string FeederUsername { get; set; }

        /// <summary>
        /// Use password specified by QuotesServerPassword2 parameter of Feeder config.
        /// </summary>
        public string FeederPassword { get; set; }

        /// <summary>
        /// Specify path to an existing directory.
        /// </summary>
        public string FeederLogPath { get; set; }

        #endregion

        #region Bridge Parameters
        /// <summary>
        /// IP address or DNS name of Bridge service.
        /// </summary>
        public string BridgeAddress { get; set; }

        /// <summary>
        /// Use port specified by AdminEyePortExp parameter of Bridge config.
        /// </summary>
        public int BridgePort { get; set; }

        /// <summary>
        /// User username specified by AdminEyeUsernameExp parameter of Bridge config.
        /// </summary>
        public string BridgeUsername { get; set; }

        /// <summary>
        /// Use password specified by AdminEyePasswordExp parameter of Bridge config.
        /// </summary>
        public string BridgePassword { get; set; }

        /// <summary>
        /// Specify path to an existing directory.
        /// </summary>
        public string BridgeLogPath { get; set; }

        #endregion

        #region Meta Account Information

        /// <summary>
        /// Meta account
        /// </summary>
        public int MetaAccount { get; set; }

        /// <summary>
        /// Meta password
        /// </summary>
        public string MetaPassword { get; set; }

        #endregion
    }
}
