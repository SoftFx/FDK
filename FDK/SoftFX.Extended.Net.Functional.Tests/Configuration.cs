namespace SoftFX.Extended.Functional.Tests
{
    using System;
    using System.Collections.Generic;
    using SoftFX.Extended;

    static class Configuration
    {
        public enum ConnectionType{ Trade, Feed };
        #region Properties

        public static ConnectionStringBuilder[] ConnectionBuilders(AccountType type, ConnectionType connType)
        {
            var builders = new List<ConnectionStringBuilder>();

            FixConnectionStringBuilder result = CreateFixConnectionStringBuilder(type, FixProtocolVersion.TheLatestVersion);
            result.SecureConnection = true;
            result.Port = connType == ConnectionType.Trade ? 5004 : 5003;
            builders.Add(result);

            result = CreateFixConnectionStringBuilder(type, FixProtocolVersion.TheLatestVersion);
            result.SecureConnection = false;
            result.Port = connType == ConnectionType.Trade ? 5002 : 5001;
            builders.Add(result);

            return builders.ToArray();
        }

        #endregion

        static FixConnectionStringBuilder CreateFixConnectionStringBuilder(AccountType type, FixProtocolVersion protocolVersion)
        {
            var result = new FixConnectionStringBuilder
            {
                FixVersion = "FIX.4.4",
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
                result.Username = "100045";
                result.Password = "e2pllch2";
            }
            else if (type == AccountType.Cash)
            {
                result.Address = "crypto.ttdemo.fxopen.com";
                result.Username = "100002";
                result.Password = "e2pllch2";
            }

            
            result.ProtocolVersion = protocolVersion.ToString();

#if DEBUG
            result.DecodeLogFixMessages = true;
            result.FixLogDirectory = @"C:\Temporary\Logs";
#endif

            return result;
        }

    }
}
