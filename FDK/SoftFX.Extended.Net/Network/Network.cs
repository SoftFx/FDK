namespace SoftFX.Extended
{
    using SoftFX.Extended.Core;

    /// <summary>
    /// The class contains information about network usage by client connection.
    /// </summary>
    public class Network
    {
        internal Network(FxDataClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// Returns network activity of last session. Can not be null.
        /// </summary>
        public NetworkActivity GetLastSessionActivity()
        {
            var result = this.client.GetNetworkActivity();
            return result;
        }

        #region Members

        readonly FxDataClient client;

        #endregion
    }
}
