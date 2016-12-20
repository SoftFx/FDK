namespace SoftFX.Extended
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using SoftFX.Extended.Core;

    /// <summary>
    /// Contains common methods of all connection string builders.
    /// </summary>
    public abstract class ConnectionStringBuilder
    {
        /// <summary>
        /// Sets all string properties to empty value.
        /// </summary>
        protected ConnectionStringBuilder()
        {
            foreach (var element in this.GetValidProperties())
            {
                if (element.CanWrite && element.PropertyType == typeof(string))
                    element.SetValue(this, string.Empty, null);
            }
            DeviceId = "";
            AppSessionId = "";
            AppId = "FDK";
        }

        #region Properties

        /// <summary>
        /// Gets or sets the username of the data feed instance.
        /// Can not be modified, when the data feed is running.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password of the data feed instance.
        /// Can not be modified, when the data feed is running.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the device ID of the data feed instance.
        /// Can not be modified, when the data feed is running.
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the application ID of the data feed instance.
        /// Can not be modified, when the data feed is running.
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// Gets or sets the application session ID of the data feed instance.
        /// Can not be modified, when the data feed is running.
        /// </summary>
        public string AppSessionId { get; set; }

        /// <summary>
        /// Gets protocol type name.
        /// </summary>
        public abstract string ProtocolType { get; }

        #endregion

        #region Test Connection Methods

        /// <summary>
        /// The method tries to connect to remote server and returns list of valid connection strings.
        /// </summary>
        /// <param name="address">host name or IP address; can not benull</param>
        /// <param name="ports">list of ports, which should be checked; can be null in this case list of predefined ports will be checked</param>
        /// <param name="timeoutInMs">timeout in ms of connection establishing; can be zero or negative in this case default timeout will be used</param>
        /// <returns>list of valid connection strings</returns>
        public static ConnectionStringBuilder[] TestConnections(string address, IEnumerable<int> ports = null, int timeoutInMs = 0)
        {
            CheckConnectionParameters(address, string.Empty, string.Empty);
            var testers = new List<ConnectionTester>();
            FixConnectionStringBuilder.PrepareConnectionTesters(address, string.Empty, string.Empty, ports, testers);
            LrpConnectionStringBuilder.PrepareConnectionTesters(address, string.Empty, string.Empty, ports, testers);
            var result = TestConnections<ConnectionStringBuilder>(testers, timeoutInMs);
            return result;
        }

        /// <summary>
        /// The method tries to connect to remote server and returns list of valid connection strings.
        /// </summary>
        /// <param name="username">a valid username</param>
        /// <param name="password">a valid password</param>
        /// <param name="address">host name or IP address; can not benull</param>
        /// <param name="ports">list of ports, which should be checked; can be null in this case list of predefined ports will be checked</param>
        /// <param name="timeoutInMs">timeout in ms of connection establishing; can be zero or negative in this case default timeout will be used</param>
        /// <returns>list of valid connection strings</returns>
        public static ConnectionStringBuilder[] TestFeedConnections(string address, string username, string password, IEnumerable<int> ports = null, int timeoutInMs = 0)
        {
            CheckConnectionParameters(address, username, password);
            var testers = new List<ConnectionTester>();
            FixConnectionStringBuilder.PrepareFeedConnectionTesters(address, username, password, ports, testers);
            LrpConnectionStringBuilder.PrepareFeedConnectionTesters(address, username, password, ports, testers);
            var result = TestFeedConnections<ConnectionStringBuilder>(testers, timeoutInMs);
            return result;
        }

        /// <summary>
        /// The method tries to connect to remote server and returns list of valid connection strings.
        /// </summary>
        /// <param name="username">a valid username</param>
        /// <param name="password">a valid password</param>
        /// <param name="address">host name or IP address; can not benull</param>
        /// <param name="ports">list of ports, which should be checked; can be null in this case list of predefined ports will be checked</param>
        /// <param name="timeoutInMs">timeout in ms of connection establishing; can be zero or negative in this case default timeout will be used</param>
        /// <returns>list of valid connection strings</returns>
        public static ConnectionStringBuilder[] TestTradeConnections(string address, string username, string password, IEnumerable<int> ports = null, int timeoutInMs = 0)
        {
            CheckConnectionParameters(address, username, password);
            var testers = new List<ConnectionTester>();
            FixConnectionStringBuilder.PrepareTradeConnectionTesters(address, username, password, ports, testers);
            LrpConnectionStringBuilder.PrepareTradeConnectionTesters(address, username, password, ports, testers);
            var result = TestTradeConnections<ConnectionStringBuilder>(testers, timeoutInMs);
            return result;
        }

        internal static void CheckConnectionParameters(string address, string username, string password)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));

            if (username == null)
                throw new ArgumentNullException(nameof(username));

            if (password == null)
                throw new ArgumentNullException(nameof(password));
        }

        internal static T[] TestConnections<T>(IEnumerable<ConnectionTester> testers, int timeoutInMs)
            where T : ConnectionStringBuilder
        {
            var builders = new List<ConnectionStringBuilder>(testers.Count());

            try
            {
                foreach (var element in testers)
                {
                    element.Start(TestUsername, TestPassword);
                }

                WaitAndCollect(testers, timeoutInMs, builders);
            }
            finally
            {
                foreach (var element in testers)
                {
                    element.Dispose();
                }
            }

            return builders.Cast<T>().ToArray();
        }

        internal static T[] TestFeedConnections<T>(IEnumerable<ConnectionTester> testers, int timeoutInMs) where T : ConnectionStringBuilder
        {
            var builders = new List<ConnectionStringBuilder>(testers.Count());

            try
            {
                foreach (var element in testers)
                {
                    element.StartFeed();
                }

                WaitAndCollect(testers, timeoutInMs, builders);
            }
            finally
            {
                foreach (var element in testers)
                {
                    element.Dispose();
                }
            }

            return builders.Cast<T>().ToArray();
        }

        internal static T[] TestTradeConnections<T>(IEnumerable<ConnectionTester> testers, int timeoutInMs)
            where T : ConnectionStringBuilder
        {
            var builders = new List<ConnectionStringBuilder>(testers.Count());

            try
            {
                foreach (var element in testers)
                {
                    element.StartTrade();
                }

                WaitAndCollect(testers, timeoutInMs, builders);
            }
            finally
            {
                foreach (var element in testers)
                {
                    element.Dispose();
                }
            }

            return builders.Cast<T>().ToArray();
        }

        static void WaitAndCollect(IEnumerable<ConnectionTester> testers, int timeoutInMs, ICollection<ConnectionStringBuilder> builders)
        {
            if (timeoutInMs <= 0)
                timeoutInMs = TimeoutInMs;

            var sw = Stopwatch.StartNew();

            foreach (var element in testers)
            {
                var value = timeoutInMs - (int)sw.ElapsedMilliseconds;
                if (value < 0)
                    value = 0;

                element.WaitFor(value);
            }

            foreach (var element in testers)
            {
                element.CollectValidConnectionStrings(builders);
            }
        }

        #endregion

        IEnumerable<PropertyInfo> GetValidProperties()
        {
            var type = this.GetType();
            var properties = type.GetProperties();

            return properties.Where(o => !o.GetCustomAttributes(typeof(ObsoleteAttribute), true).Any());
        }

        /// <summary>
        /// Makes and returns connection string.
        /// </summary>
        /// <returns>Can not be null.</returns>
        public override string ToString()
        {
            using (var parameters = new FxParams())
            {
                foreach (var element in this.GetValidProperties())
                {
                    var obj = element.GetValue(this, null);
                    var type = obj.GetType();

                    if (type == typeof(int))
                    {
                        var value = (int)obj;
                        parameters.SetInt32(element.Name, value);
                    }
                    else if (type == typeof(double))
                    {
                        var value = (double)obj;
                        parameters.SetReal(element.Name, value);
                    }
                    else if (type == typeof(string))
                    {
                        var value = (string)obj;
                        parameters.SetString(element.Name, value);
                    }
                    else if (type == typeof(bool))
                    {
                        var value = (bool)obj;
                        parameters.SetBool(element.Name, value);
                    }
                    else
                    {
                        var message = string.Format("Unsupported property type = {0}", type);
                        throw new ArgumentException(message);
                    }
                }

                var result = parameters.ToString();
                return result;
            }
        }

        #region Members and Constants

        const string TestUsername = "{6FBC0931-39D5-412A-8FFF-D36AA2359260}";
        const string TestPassword = "{5513F6C7-85C7-434C-8DDB-572CEC2C0BA1}";
        const int TimeoutInMs = 5000;

        #endregion
    }
}
