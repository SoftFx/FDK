namespace Mql2Fdk
{
    using SoftFX.Extended;

    class FixConnectionStrings : ConnectionStrings
    {
        public FixConnectionStrings(string address, string username, string password, string logLocation)
        {
            this.FeedConnectionString = CreateFeedConnectionString(address, username, password, logLocation);
            this.TradeConnectionString = CreateTradeConnectionString(address, username, password, logLocation);
        }

        static string CreateFeedConnectionString(string address, string username, string password, string logLocation)
        {
            var builder = new FixConnectionStringBuilder
            {
                TargetCompId = "EXECUTOR",
                ProtocolVersion = FixProtocolVersion.TheLatestVersion.ToString(),
                Address = address,
                Username = username,
                Password = password,
                SecureConnection = true,
                Port = 5003,
                ExcludeMessagesFromLogs = "W",
                FixLogDirectory = logLocation,

                DecodeLogFixMessages = true
            };

            return builder.ToString();
        }

        static string CreateTradeConnectionString(string address, string username, string password, string logsLocation)
        {
            var builder = new FixConnectionStringBuilder
            {
                TargetCompId = "EXECUTOR",
                ProtocolVersion = FixProtocolVersion.TheLatestVersion.ToString(),
                Address = address,
                Username = username,
                Password = password,
                SecureConnection = true,
                Port = 5004,

                DecodeLogFixMessages = true,

                FixLogDirectory = logsLocation,
            };

            return builder.ToString();
        }
    }
}
