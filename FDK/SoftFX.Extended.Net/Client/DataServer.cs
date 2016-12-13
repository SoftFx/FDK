using System;

namespace SoftFX.Extended
{
    /// <summary>
    /// Contains common server side methods of feed and trade.
    /// </summary>
    public class DataServer
    {
        internal DataServer(DataClient client)
        {
            this.Client = client;
        }

        /// <summary>
        /// The method send two factor response with one time password.
        /// </summary>
        /// <param name="reason">Two factor response reason</param>
        /// <param name="otp">One time password</param>
        /// <returns>can not be null.</returns>
        public void SendTwoFactorResponse(TwoFactorReason reason, string otp)
        {
            this.Client.Handle.SendTwoFactorResponse(reason, otp);
        }

        /// <summary>
        /// Gets a specified chunk of a specified file.
        /// </summary>
        /// <param name="fileId">A requested file id; can not be null.</param>
        /// <param name="chunkId">A requested chunk id; can not be negative.</param>
        /// <returns>Returns data and information of downloaded chunk; can not be null</returns>
        internal FxFileChunk GetFileChunk(string fileId, int chunkId)
        {
            return this.GetFileChunkEx(fileId, chunkId, this.Client.SynchOperationTimeout);
        }

        /// <summary>
        /// Gets a specified chunk of a specified file.
        /// </summary>
        /// <param name="fileId">A requested file id; can not be null.</param>
        /// <param name="chunkId">A requested chunk id; can not be negative.</param>
        /// <param name="timeoutInMilliseconds">Timeout of the operation in milliseconds.</param>
        /// <returns>Returns data and information of downloaded chunk; can not be null</returns>
        internal FxFileChunk GetFileChunkEx(string fileId, int chunkId, int timeoutInMilliseconds)
        {
            return this.Client.Handle.GetFileChunk(fileId, chunkId, timeoutInMilliseconds);
        }

        /// <summary>
        /// The method returns the current trade session information.
        /// </summary>
        /// <returns>can not be null.</returns>
        public SessionInfo GetSessionInfo()
        {
            return this.GetSessionInfoEx(this.Client.SynchOperationTimeout);
        }

        /// <summary>
        /// The method returns the current trade session information.
        /// </summary>
        /// <param name="timeoutInMilliseconds">timeout of the operation in milliseconds</param>
        /// <returns>can not be null.</returns>
        public SessionInfo GetSessionInfoEx(int timeoutInMilliseconds)
        {
            return this.Client.Handle.GetSessionInfo(timeoutInMilliseconds);
        }

        /// <summary>
        /// Data client.
        /// </summary>
        protected DataClient Client { get; private set; }
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TClient"></typeparam>
    public class DataServer<TClient> : DataServer
        where TClient : DataClient
    {
        internal DataServer(TClient client)
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
