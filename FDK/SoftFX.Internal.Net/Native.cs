namespace SoftFX.Internal
{
    using System.IO;
    using SoftFX.Extended;
    using SoftFX.Lrp;

    static class Native
    {
        #region Construction

        static Native()
        {
            FixProvider = CreateLocalClient("SoftFX.FixProvider.", Generated.FixProvider.Signature.Value);
            LrpProvider = CreateLocalClient("SoftFX.LrpProvider.", Generated.LrpProvider.Signature.Value);
        }

        #endregion

        #region Properties

        public static LocalClient FixProvider { get; private set; }
        public static LocalClient LrpProvider { get; private set; }

        #endregion

        #region Methods

        static LocalClient CreateLocalClient(string filename, string signature)
        {
            var path = Library.Path;
            path = Path.Combine(path, filename);
            path += Library.Platform;
            path += ".dll";
            var client = new LocalClient(path, signature);
            return client;
        }

        #endregion
    }
}
