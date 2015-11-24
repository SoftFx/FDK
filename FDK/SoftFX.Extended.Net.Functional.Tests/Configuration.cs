namespace SoftFX.Extended.Functional.Tests
{
    using System;
    using System.Collections.Generic;
    using SoftFX.Extended;

    static class Configuration
    {
        #region Properties

        public static ConnectionStringBuilder[] DataFeedConnectionBuilders
        {
            get
            {
                var builders = new List<ConnectionStringBuilder>();
                InitializeDataFeed(builders);
                var result = builders.ToArray();
                return result;
            }
        }

        public static ConnectionStringBuilder[] DataTradeGrossConnectionBuilders
        {
            get
            {
                var builders = new List<ConnectionStringBuilder>();
                InitializeDataTradeGross(builders);
                var result = builders.ToArray();
                return result;
            }
        }

        public static ConnectionStringBuilder[] DataTradeNetConnectionBuilders
        {
            get
            {
                var builders = new List<ConnectionStringBuilder>();
                InitializeDataTradeNet(builders);
                var result = builders.ToArray();
                return result;
            }
        }

        #endregion

        #region Construction

        static void InitializeDataFeed(List<ConnectionStringBuilder> builders)
        {
            foreach (var element in new[] { FixProtocolVersion.TheLatestVersion })
            {
                builders.Add(CreateDataFeedFixConnectionStringBuilder(false, element));
                builders.Add(CreateDataFeedFixConnectionStringBuilder(true, element));
            }
        }

        static void InitializeDataTradeGross(List<ConnectionStringBuilder> builders)
        {
            builders.Add(CreateDataTradeFixConnectionStringBuilder(AccountType.Gross, false, FixProtocolVersion.TheLatestVersion));
            builders.Add(CreateDataTradeFixConnectionStringBuilder(AccountType.Gross, true, FixProtocolVersion.TheLatestVersion));
        }

        static void InitializeDataTradeNet(List<ConnectionStringBuilder> builders)
        {
            builders.Add(CreateDataTradeFixConnectionStringBuilder(AccountType.Net, false, FixProtocolVersion.TheLatestVersion));
            builders.Add(CreateDataTradeFixConnectionStringBuilder(AccountType.Net, true, FixProtocolVersion.TheLatestVersion));
        }

        static FixConnectionStringBuilder CreateFixConnectionStringBuilder(AccountType type, FixProtocolVersion protocolVersion)
        {
            var result = new FixConnectionStringBuilder
            {
                FixVersion = "FIX.4.4",
              //  Address = "tp.st.soft-fx.eu",
                Address = "ttdemo.fxopen.com",
                TargetCompId = "EXECUTOR"
            };

            if (type == AccountType.Gross)
            {
                result.Username = "999999";
                result.Password = "e2pllch2";
            }
            else if (type == AccountType.Net)
            {
                result.Username = "26";
                result.Password = "37kw22bNkBcy";
            }
            
            result.ProtocolVersion = protocolVersion.ToString();

#if DEBUG
            result.DecodeLogFixMessages = true;
            result.FixLogDirectory = @"C:\Temporary\Logs";
#endif

            return result;
        }

        static FixConnectionStringBuilder CreateDataFeedFixConnectionStringBuilder(Boolean isSecure, FixProtocolVersion protocolVersion)
        {
            var result = CreateFixConnectionStringBuilder(AccountType.Gross, protocolVersion);
            result.SecureConnection = isSecure;
            result.Port = isSecure ? 5003 : 5001;
            return result;
        }

        static FixConnectionStringBuilder CreateDataTradeFixConnectionStringBuilder(AccountType type, Boolean isSecure, FixProtocolVersion protocolVersion)
        {
            var result = CreateFixConnectionStringBuilder(type, protocolVersion);
            result.SecureConnection = isSecure;
            result.Port = isSecure ? 5004 : 5002;
            return result;
        }

        #endregion

        public static void Initialize()
        {
        }

        static Configuration()
        {
            Library.Path = "<FRE>";
        }
    }
}
