﻿namespace SoftFX.Extended
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents FIX connection parameters.
    /// </summary>
    public class FixConnectionStringBuilder : ConnectionStringBuilder
    {
        /// <summary>
        /// Creates a new instance of FixConnectionStringBuilder.
        /// </summary>
        public FixConnectionStringBuilder()
        {
            this.FixVersion = "FIX.4.4";
            this.TargetCompId = "EXECUTOR";
            this.ProtocolVersion = FixProtocolVersion.TheLatestVersion.ToString();
            this.DecodeLogFixMessages = true;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the fix version of the data feed instance. Currently only "FIX.4.4" is supported.
        /// Can not be modified, when the data feed is running.
        /// </summary>
        public string FixVersion { get; set; }

        /// <summary>
        /// Gets or sets trading platform address of the data feed instance. Can be IP address or host name.
        /// Can not be modified, when the data feed is running.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets trading platform port of the data feed instance.
        /// Can not be modified, when the data feed is running.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets the sender ID of the data feed instance. Currently this property is generated by API and can not be specified by client.
        /// </summary>
        public string SenderCompId { get; set; }

        /// <summary>
        /// Gets or sets the target comp ID of the data feed instance.
        /// Can not be modified, when the data feed is running.
        /// </summary>
        public string TargetCompId { get; set; }

        /// <summary>
        /// Gets or sets SSL using mode.
        /// Can not be modified, when data feed is running.
        /// </summary>
        public bool SecureConnection { get; set; }

        /// <summary>
        /// Gets or sets log dictionary for FIX messages.
        /// </summary>
        public string FixLogDirectory { get; set; }

        /// <summary>
        /// By default FDK generates file name of events based on session id, but you can override the name.
        /// </summary>
        public string FixEventsFileName { get; set; }

        /// <summary>
        /// By default FDK generates file name of events based on session id, but you can override the name.
        /// </summary>
        public string FixMessagesFileName { get; set; }

        /// <summary>
        /// If true, the FDK converts FIX messages to good readable format by FIX dictionary using.
        /// </summary>
        public bool DecodeLogFixMessages { get; set; }

        /// <summary>
        /// Gets or sets protocol version.
        /// </summary>
        public string ProtocolVersion { get; set; }

        /// <summary>
        /// Gets or sets regular expression filter to exclude messages from logs.
        /// Empty string means all messages will be written to logs.
        /// "W|y|0" means to skip the following messages: security list, market data snapshot and heartbeat.
        /// </summary>
        public string ExcludeMessagesFromLogs { get; set; }

        /// <summary>
        /// Gets or sets whether network usage statistics should be enabled.
        /// </summary>
        public bool EnableNetworkStatistics { get; set; }

        /// <summary>
        /// </summary>
        public string ProxyType { get; set; }

        /// <summary>
        /// </summary>
        public string ProxyAddress { get; set; }

        /// <summary>
        /// </summary>
        public int ProxyPort { get; set; }

        /// <summary>
        /// </summary>
        public string ProxyUserName { get; set; }

        /// <summary>
        /// </summary>
        public string ProxyPassword { get; set; }

        /// <summary>
        /// Gets protocol type name = "Fix".
        /// </summary>
        public override string ProtocolType
        {
            get
            {
                return "Fix";
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
        public static FixConnectionStringBuilder[] TestFixConnections(string address, IEnumerable<int> ports = null, int timeoutInMs = 0)
        {
            CheckConnectionParameters(address, string.Empty, string.Empty);
            var testers = new List<ConnectionTester>();
            PrepareConnectionTesters(address, string.Empty, string.Empty, ports, testers);
            var result = TestConnections<FixConnectionStringBuilder>(testers, timeoutInMs);
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
        public static FixConnectionStringBuilder[] TestFeedFixConnections(string address, string username, string password, IEnumerable<int> ports = null, int timeoutInMs = 0)
        {
            CheckConnectionParameters(address, username, password);
            var testers = new List<ConnectionTester>();
            PrepareFeedConnectionTesters(address, username, password, ports, testers);
            var result = TestFeedConnections<FixConnectionStringBuilder>(testers, timeoutInMs);
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
        public static FixConnectionStringBuilder[] TestTradeFixConnections(string address, string username, string password, IEnumerable<int> ports = null, int timeoutInMs = 0)
        {
            CheckConnectionParameters(address, username, password);
            var testers = new List<ConnectionTester>();
            PrepareTradeConnectionTesters(address, username, password, ports, testers);
            var result = TestTradeConnections<FixConnectionStringBuilder>(testers, timeoutInMs);
            return result;
        }

        internal static void PrepareFeedConnectionTesters(string address, string username, string password, IEnumerable<int> ports, ICollection<ConnectionTester> testers)
        {
            PrepareConnectionTesters(address, username, password, ports, testers);
        }

        internal static void PrepareTradeConnectionTesters(string address, string username, string password, IEnumerable<int> ports, ICollection<ConnectionTester> testers)
        {
            PrepareConnectionTesters(address, username, password, ports, testers);
        }

        internal static void PrepareConnectionTesters(string address, string username, string password, IEnumerable<int> ports, ICollection<ConnectionTester> testers)
        {
            if (ports == null)
                ports = Ports;

            foreach(var element in ports)
            {
                var builder = new FixConnectionStringBuilder
                {
                    Address = address,
                    SecureConnection = true,
                    Port = element,
                    Username = username,
                    Password = password
                };

                var tester = new ConnectionTester(builder);
                testers.Add(tester);
            }

            foreach (var element in ports)
            {
                var builder = new FixConnectionStringBuilder
                {
                    Address = address,
                    SecureConnection = false,
                    Port = element,
                    Username = username,
                    Password = password
                };

                var tester = new ConnectionTester(builder);
                testers.Add(tester);
            }
        }

        #endregion

        #region Members and Constants

        static readonly int[] Ports = { 5001, 5002, 5003, 5004 };

        #endregion
    }
}
