using System;
using System.IO;
using SoftFX.Extended;
using SoftFX.Extended.Events;

namespace AccountInfo
{
    class Program
    {
        public static string LogPath = "Logs";

        static FixConnectionStringBuilder CreateBuilder(string addres, string login, string password)
        {
            var builder = new FixConnectionStringBuilder();
            builder.ProtocolVersion = FixProtocolVersion.TheLatestVersion.ToString();
            builder.Address = addres;
            builder.Port = 5004;
            builder.SecureConnection = true;
            builder.Username = login;
            builder.Password = password;
            builder.TargetCompId = "EXECUTOR";
            builder.DecodeLogFixMessages = true;
            builder.FixLogDirectory = LogPath;
            builder.FixEventsFileName = $"{login}.trade.events.log";
            builder.FixMessagesFileName = $"{login}.trade.messages.log";
            return builder;
        }

        static void OnLogon(object sender, LogonEventArgs e)
        {
            Console.WriteLine("OnLogon(): {0}", e);
        }

        static void OnLogout(object sender, LogoutEventArgs e)
        {
            Console.WriteLine("OnLogout(): {0}", e);
        }

        static void OnAccountInfo(object sender, AccountInfoEventArgs e)
        {
            Console.WriteLine("OnAccountInfo(): {0}", e.Information);
            Console.WriteLine("Press any key to exit...");
        }

        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: AccountInfo.exe address login password");
            }

            try
            {
                if (!Directory.Exists(LogPath))
                    Directory.CreateDirectory(LogPath);

                Library.Path = "<FRE>";

                string address = args[0];
                string login = args[1];
                string password = args[2];

                // Create data trade interface
                var trade = new DataTrade { SynchOperationTimeout = 30000 };

                // Create connection string
                FixConnectionStringBuilder builder = CreateBuilder(address, login, password);
                var connectionString = builder.ToString();

                // Initialize data trade interface
                trade.Initialize(connectionString);

                // Subscribe to data trade events
                trade.Logon += OnLogon;
                trade.Logout += OnLogout;
                trade.AccountInfo += OnAccountInfo;

                // Start data trade
                trade.Start();
                Console.WriteLine("DataTrade started!");
                Console.WriteLine("Please wait for login status...");

                // Wait for exit
                Console.ReadKey();

                // Stop data trade
                trade.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
