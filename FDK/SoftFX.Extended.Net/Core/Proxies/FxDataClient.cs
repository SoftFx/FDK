namespace SoftFX.Extended.Core
{
    using System;
    using SoftFX.Lrp;

    unsafe struct FxDataClient
    {
        #region Creating and Converting

        public FxDataClient(LPtr handle)
            : this()
        {
            this.handle = handle;
        }

        public FxHandle Handle
        {
            get
            {
                return new FxHandle(this.handle);
            }
        }

        #endregion

        #region Methods

        public bool Start()
        {
            this.VerifyInitialized();

            return Native.Client.Start(this.handle);
        }

        public bool WaitForLogon(int timeoutInMilliseconds)
        {
            this.VerifyInitialized();

            return Native.Client.WaitForLogon(this.handle, (uint)timeoutInMilliseconds);
        }

        public bool Shutdown()
        {
            this.VerifyInitialized();

            var status = Native.Client.Shutdown(this.handle);
            return status == HResults.S_OK;
        }

        public bool Stop()
        {
            this.VerifyInitialized();

            var status = Native.Client.Stop(this.handle);
            return status == HResults.S_OK;
        }

        public string NextId()
        {
            this.VerifyInitialized();

            return Native.Client.NextId(this.handle);
        }

        public bool GetNextMessage(out FxMessage message)
        {
            this.VerifyInitialized();

            return Native.Client.GetNextMessage(this.handle, out message);
        }

        public NetworkActivity GetNetworkActivity()
        {
            var logicalBytesSent = 0UL;
            var physicalBytesSent = 0UL;
            var logicalBytesReceived = 0UL;
            var physicalBytesReceived = 0UL;

            this.VerifyInitialized();

            Native.Client.GetNetworkActivity(this.handle, out logicalBytesSent, out physicalBytesSent, out logicalBytesReceived, out physicalBytesReceived);
            return new NetworkActivity((long)logicalBytesSent, (long)physicalBytesSent, (long)logicalBytesReceived, (long)physicalBytesReceived);
        }

        public void DispatchMessage(FxMessage message)
        {
            this.VerifyInitialized();

            Native.Client.DispatchMessage(this.handle, message);
        }

        public void SendTwoFactorResponse(TwoFactorReason reason, string otp)
        {
            this.VerifyInitialized();

            Native.Client.SendTwoFactorResponse(this.handle, reason, otp);
        }

        public SessionInfo GetSessionInfo(int timeoutInMilliseconds)
        {
            this.VerifyInitialized();

            return Native.Client.GetSessionInfo(this.handle, (uint)timeoutInMilliseconds);
        }

        public SessionInfo SessionInformation
        {
            get
            {
                this.VerifyInitialized();

                return Native.ClientCache.GetSessionInfo(this.handle);
            }
        }

        public AccountInfo GetAccountInfo(int timeoutInMilliseconds)
        {
            this.VerifyInitialized();

            return Native.TradeServer.GetAccountInfo(this.handle, (uint)timeoutInMilliseconds);
        }

        public string ProtocolVersion
        {
            get
            {
                this.VerifyInitialized();

                return Native.Client.GetProtocolVersion(this.handle);
            }
        }

        public FxFileChunk GetFileChunk(string fileId, int chunkId, int timeoutInMilliseconds)
        {
            this.VerifyInitialized();

            return Native.Client.GetFileChunk(this.handle, fileId, chunkId, (uint)timeoutInMilliseconds);
        }

        #endregion

        void VerifyInitialized()
        {
            if (this.handle.IsZero)
                throw new InvalidOperationException(string.Format("Cannot use not initialized {0} object.", this.GetType().Name));
        }

        #region Members

        readonly LPtr handle;

        #endregion
    }
}
