namespace LrpServer.Net
{
    using SoftFX.Lrp;

    static class Native
    {
        static Native()
        {
            var path = LrpLibrary.Path;
            var client = new LocalClient(path, LocalCpp.Signature.Value, Mode.InProcess);
            LocalCpp.Library proxy = new LocalCpp.Library(client);
            proxy.SetDotNetDllPath(typeof(Native).Assembly.Location);
            Native.Client = client;
        }

        public static void Initialize()
        {
        }

        #region Properties

        public static LocalClient Client { get; private set; }

        #endregion
    }
}
