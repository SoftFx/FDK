namespace SoftFX.Extended.Core
{
    using System;
    using System.IO;
    using System.Reflection;
    using SoftFX.Extended.Generated;
    using SoftFX.Lrp;
    using ManagedLibrary = SoftFX.Extended.Library;
    using Resources;

    static class Native
    {
        #region Core Constants

        public const int FX_SOCKET_MODE_SIMPLE = 0;
        public const int FX_SOCKET_MODE_SECURE = 1;

        #endregion

        #region API Constants

        public const int FX_TRADING_PLATFORM_ADDRESS = 0;
        public const int FX_TRADING_PLATFORM_PORT    = 1;
        public const int FX_FIX_VERSION              = 2;
        public const int FX_SENDER_COMP_ID           = 3;
        public const int FX_TARGET_COMP_ID           = 4;
        public const int FX_USERNAME                 = 5;
        public const int FX_PASSWORD                 = 6;
        public const int FX_LOG_DIRECTORY            = 7;
        public const int FX_SYNCH_OPERATION_TIMEOUT  = 8;
        public const int FX_SECURE_CONNECTION        = 9;

        #endregion

        #region API Message Constants

        public const int FX_MSG_LOGON = 0;
        public const int FX_MSG_LOGOUT = 1;
        public const int FX_MSG_TICK = 2;
        public const int FX_MSG_SESSION_INFO = 3;
        public const int FX_MSG_CACHE_UPDATED = 4;
        public const int FX_MSG_ACCOUNT_INFO = 5;
        public const int FX_MSG_SYMBOL_INFO = 6;
        public const int FX_MSG_EXECUTION_REPORT = 7;
        public const int FX_MSG_TRADE_TRANSACTION_REPORT = 8;
        public const int FX_MSG_UPDATE_TRADE_RECORD = 9;
        public const int FX_MSG_POSITION_REPORT = 10;
        public const int FX_MSG_NOTIFICATION = 11;
        public const int FX_MSG_QUOTES_HISTORY_RESPONSE = 12;
        public const int FX_MSG_CURRENCY_INFO = 13;

        #endregion

        static Native()
        {
            ManagedLibrary.CheckRedistPackages();
            ManagedLibrary.MarkAsReadOnly();
            ManagedLibrary.ModulesManager.Extract();

            if (ManagedLibrary.ResolveDotNetAssemblies)
            {
                AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += OnAssemblyResolve;
                AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolve;
            }
            Construct();
        }

        static void Construct()
        {
            LrpClient = CreateLocalClient(Signature.Value, "SoftFX.LlApi.");
            LrpLlCommonClient = CreateLocalClient(Financial.Generated.Signature.Value, "SoftFX.LlCommon.");

            Serializer = new Financial.Generated.Serializer(LrpLlCommonClient);

            Handle = new Handle(LrpClient);
            Params = new Params(LrpClient);
            Client = new ClientServer(LrpClient);
            ClientCache = new ClientCache(LrpClient);
            FeedServer = new FeedServer(LrpClient);
            FeedCache = new FeedCache(LrpClient);
            TradeServer = new TradeServer(LrpClient);
            TradeCache = new TradeCache(LrpClient);
            Converter = new Converter(LrpClient);
            Iterator = new Iterator(LrpClient);
            Library = new Library(LrpClient);
        }
        
        static LocalClient CreateLocalClient(string signature, string name)
        {
            var path = Path.Combine(ManagedLibrary.Path, name);
            path += ManagedLibrary.Platform;
            path += ".dll";

            var result = new LocalClient(path, signature, Mode.InProcess);
            return result;
        }

        public static void Initialize()
        {
        }

        static Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            var length = args.Name.IndexOf(',');
            if (length < 0)
                return null;

            TraceUtils.WriteLine("Resolving: '{0}'", args.Name);

            var name = args.Name.Substring(0, length);
            var result = ManagedLibrary.ModulesManager.LoadAssembly(name);

            if (result == null)
                return null;

            if (result.FullName != args.Name)
                return null;

            return result;
        }

        #region LRP Properties

        public static LocalClient LrpClient { get; private set; }
        public static LocalClient LrpLlCommonClient { get; private set; }

        public static Financial.Generated.Serializer Serializer { get; private set; }

        public static Handle Handle { get; private set; }
        public static Params Params { get; private set; }
        public static ClientServer Client { get; private set; }
        public static ClientCache ClientCache { get; private set; }
        public static FeedServer FeedServer { get; private set; }
        public static FeedCache FeedCache { get; private set; }
        public static TradeServer TradeServer { get; private set; }
        public static TradeCache TradeCache { get; private set; }
        public static Converter Converter { get; private set; }
        public static Iterator Iterator { get; private set; }
        public static Library Library { get; private set; }

        #endregion

        #region Factory methods

        public static Financial.Generated.FinCalcProxy CreateFinCalcProxy(string text)
        {
            return new Financial.Generated.FinCalcProxy(LrpLlCommonClient, text);
        }

        #endregion
    }
}
