namespace SoftFX.Extended
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents LRP connection parameters.
    /// </summary>
    public class LrpConnectionStringBuilder : ConnectionStringBuilder
    {
        /// <summary>
        /// Creates a new instance of LrpConnectionStringBuilder and initializes all properties to default values.
        /// </summary>
        public LrpConnectionStringBuilder()
        {
            this.Port = 5010;
            this.EnableQuotesLogging = false;
            this.SecureConnection = false;
        }

        #region Properties

        /// <summary>
        /// Gets or sets SSL using mode.
        /// Can not be modified, when data feed is running.
        /// </summary>
        public bool SecureConnection { get; set; }

        /// <summary>
        /// Gets or sets trading platform address of the data feed/trade instance. Can be IP address or host name.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets trading platform port of the data feed/trade instance.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets file name for events log.
        /// </summary>
        public string EventsLogFileName { get; set; }

        /// <summary>
        /// Gets or sets file name for messages log.
        /// </summary>
        public string MessagesLogFileName { get; set; }

        /// <summary>
        /// Enables or disables quotes logging in message log.
        /// Anyway quotes logging does not write in log raw message information,
        /// because for quotes transmitting LRP uses special codecs.
        /// </summary>
        public bool EnableQuotesLogging { get; set; }

        /// <summary>
        /// Gets or sets whether network usage statistics should be enabled.
        /// </summary>
        public bool EnableNetworkStatistics { get; set; }

        /// <summary>
        /// Gets protocol type name = "Lrp".
        /// </summary>
        public override string ProtocolType
        {
            get
            {
                return "Lrp";
            }
        }

        #endregion

        #region Test Connection Methods

        /// <summary>
        /// The method tries to connect to remote server and returns list of valid connection strings.
        /// </summary>
        /// <param name="address">host name or IP address; can not benull</param>
        /// <param name="ports">list of ports, which should be checked; can be null in this case list of predefined ports will be checked</param>
        /// <param name="timeoutInMs">timeout in ms of connection establishing; can be zero or negative in this case default timeout will be used</param>
        /// <returns>list of valid connection strings</returns>
        public static LrpConnectionStringBuilder[] TestLrpConnections(string address, IEnumerable<int> ports = null, int timeoutInMs = 0)
        {
            CheckConnectionParameters(address, string.Empty, string.Empty);
            var testers = new List<ConnectionTester>();
            PrepareConnectionTesters(address, string.Empty, string.Empty, ports, testers);
            var result = TestConnections<LrpConnectionStringBuilder>(testers, timeoutInMs);
            return result;
        }

        /// <summary>
        /// The method tries to connect to remote server and returns list of valid connection strings.
        /// </summary>
        /// <param name="address">host name or IP address; can not benull</param>
        /// <param name="username">a valid username</param>
        /// <param name="password">a valid password</param>
        /// <param name="ports">list of ports, which should be checked; can be null in this case list of predefined ports will be checked</param>
        /// <param name="timeoutInMs">timeout in ms of connection establishing; can be zero or negative in this case default timeout will be used</param>
        /// <returns>list of valid connection strings</returns>
        public static LrpConnectionStringBuilder[] TestFeedLrpConnections(string address, string username, string password, IEnumerable<int> ports = null, int timeoutInMs = 0)
        {
            CheckConnectionParameters(address, username, password);
            var testers = new List<ConnectionTester>();
            PrepareFeedConnectionTesters(address, string.Empty, string.Empty, ports, testers);
            var result = TestFeedConnections<LrpConnectionStringBuilder>(testers, timeoutInMs);
            return result;
        }

        /// <summary>
        /// The method tries to connect to remote server and returns list of valid connection strings.
        /// </summary>
        /// <param name="address">host name or IP address; can not benull</param>
        /// <param name="username">a valid username</param>
        /// <param name="password">a valid password</param>
        /// <param name="ports">list of ports, which should be checked; can be null in this case list of predefined ports will be checked</param>
        /// <param name="timeoutInMs">timeout in ms of connection establishing; can be zero or negative in this case default timeout will be used</param>
        /// <returns>list of valid connection strings</returns>
        public static LrpConnectionStringBuilder[] TestTradeLrpConnections(string address, string username, string password, IEnumerable<int> ports = null, int timeoutInMs = 0)
        {
            CheckConnectionParameters(address, username, password);
            var testers = new List<ConnectionTester>();
            PrepareTradeConnectionTesters(address, string.Empty, string.Empty, ports, testers);
            var result = TestTradeConnections<LrpConnectionStringBuilder>(testers, timeoutInMs);
            return result;
        }

        internal static void PrepareFeedConnectionTesters(string address, string username, string password, IEnumerable<int> ports, ICollection<ConnectionTester> testers)
        {
            if (address == null)
                throw new ArgumentNullException("address");

            if (ports == null)
                ports = Ports;

            foreach (var element in ports)
            {
                var builder = new LrpConnectionStringBuilder
                {
                    Address = address,
                    Port = element,
                    Username = username,
                    Password = password
                };

                var tester = new ConnectionTester(builder);
                testers.Add(tester);
            }
        }

        internal static void PrepareTradeConnectionTesters(string address, string username, string password, IEnumerable<int> ports, ICollection<ConnectionTester> testers)
        {
        }

        internal static void PrepareConnectionTesters(string address, string username, string password, IEnumerable<int> ports, ICollection<ConnectionTester> testers)
        {
            PrepareFeedConnectionTesters(address, username, password, ports, testers);
            PrepareTradeConnectionTesters(address, username, password, ports, testers);
        }

        #endregion

        #region Members and Constants

        static readonly int[] Ports = { 5010 };

        #endregion
    }
}
