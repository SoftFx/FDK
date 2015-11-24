namespace SoftFX.Extended
{
    /// <summary>
    /// Contains common local cache methods of feed and trade.
    /// </summary>
    public class DataCache
    {
        internal DataCache(DataClient client)
        {
            this.Client = client;
        }

        /// <summary>
        /// Returns cache of session information.
        /// </summary>
        public SessionInfo SessionInfo
        {
            get
            {
                return this.Client.Handle.SessionInformation;
            }
        }

        /// <summary>
        /// Data client.
        /// </summary>
        protected DataClient Client { get; private set; }
    }

    /// <summary>
    /// Contains common local cache methods of feed and trade.
    /// </summary>
    /// <typeparam name="TClient">Data client.</typeparam>
    public abstract class DataCache<TClient> : DataCache
        where TClient : DataClient
    {
        internal DataCache(TClient client)
            : base(client)
        {
        }

        /// <summary>
        /// Data client.
        /// </summary>
        protected new TClient Client
        {
            get { return (TClient)base.Client; }
        }
    }
}
