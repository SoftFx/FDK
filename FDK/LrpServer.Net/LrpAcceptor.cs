namespace LrpServer.Net
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using LrpServer.Net.LocalCpp;
    using SoftFX.Lrp;

    /// <summary>
    ///
    /// </summary>
    public class LrpAcceptor : IDisposable
    {
        #region Construction and Destruction

        /// <summary>
        ///
        /// </summary>
        /// <param name="port"></param>
        /// <param name="handler"></param>
        /// <param name="channels"></param>
        public LrpAcceptor(int port, ILrpServerHandler handler, LrpChannelsPool channels)
            : this(port, null, null, handler, channels)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="port"></param>
        /// <param name="sertificateFilename"></param>
        /// <param name="sertificatePassword"></param>
        /// <param name="handler"></param>
        /// <param name="channels"></param>
        public LrpAcceptor(int port, string sertificateFilename, string sertificatePassword, ILrpServerHandler handler, LrpChannelsPool channels)
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            if (sertificateFilename == null)
                sertificateFilename = string.Empty;

            if (sertificatePassword == null)
                sertificatePassword = string.Empty;

            this.handler = handler;
            this.handle = GCHandle.Alloc(this);
            var handle = new LPtr(GCHandle.ToIntPtr(this.handle));

            try
            {
                this.proxy = new LocalServerProxy(Native.Client, channels.Handle, port, sertificateFilename, sertificatePassword, handle);
            }
            catch
            {
                this.handle.Free();
                throw;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void Dispose()
        {
            var proxy = this.proxy;

            if (proxy != null)
            {
                this.proxy = null;
                proxy.Dispose();
            }

            if (this.handle.IsAllocated)
            {
                this.handle.Free();
            }
        }

        #endregion

        #region Control Methods

        /// <summary>
        ///
        /// </summary>
        public void Start()
        {
            this.proxy.Start();
        }

        /// <summary>
        ///
        /// </summary>
        public void Stop()
        {
            this.proxy.Stop();
        }

        #endregion

        #region Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        public void EndConnection(long id, int status)
        {
            this.proxy.EndConnection(id, status);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <param name="message"></param>
        public void EndLogon(long id, int status, string message)
        {
            if (message == null)
                message = string.Empty;

            this.proxy.EndLogon(id, status, message);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestId"></param>
        /// <param name="sessionInfo"></param>
        public void SendSessionInfo(long id, string requestId, LrpSessionInfo sessionInfo)
        {
            if (requestId == null)
                requestId = string.Empty;

            this.proxy.SendSessionInfo(id, requestId, sessionInfo);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestId"></param>
        /// <param name="symbolsInfo"></param>
        public void SendCurrenciesInfo(long id, string requestId, List<LrpCurrencyInfo> currenciesInfo)
        {
            if (requestId == null)
                requestId = string.Empty;

            this.proxy.SendCurrenciesInfo(id, requestId, currenciesInfo.ToArray());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestId"></param>
        /// <param name="symbolsInfo"></param>
        public void SendSymbolsInfo(long id, string requestId, List<LrpSymbolInfo> symbolsInfo)
        {
            if (requestId == null)
                requestId = string.Empty;

            this.proxy.SendSymbolsInfo(id, requestId, symbolsInfo.ToArray());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestId"></param>
        public void SendQuotesSubscriptionConfirm(long id, string requestId)
        {
            if (requestId == null)
                requestId = string.Empty;

            this.proxy.SendQuotesSubscriptionConfirm(id, requestId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestId"></param>
        /// <param name="message"></param>
        public void SendQuotesSubscriptionReject(long id, string requestId, string message)
        {
            if (requestId == null)
                requestId = string.Empty;

            if (message == null)
                message = string.Empty;

            this.proxy.SendQuotesSubscriptionReject(id, requestId, message);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestId"></param>
        /// <param name="version"></param>
        public void SendQuotesHistoryVersion(long id, string requestId, int version)
        {
            proxy.SendQuotesHistoryVersion(id, requestId, version);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quote"></param>
        public void SendQuote(long id, LrpQuote quote)
        {
            this.proxy.SendQuote(id, quote);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestId"></param>
        /// <param name="status"></param>
        /// <param name="field"></param>
        public void SendMarketHistoryMetadataResponse(long id, string requestId, int status, string field)
        {
            if (requestId == null)
                requestId = string.Empty;

            if (field == null)
                field = string.Empty;

            this.proxy.SendMarketHistoryMetadataResponse(id, requestId, status, field);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestId"></param>
        /// <param name="status"></param>
        /// <param name="field"></param>
        public void SendMarketHistoryMetadataReject(long id, string requestId, int status, string field)
        {
            if (requestId == null)
                requestId = string.Empty;

            if (field == null)
                field = string.Empty;

            this.proxy.SendMarketHistoryMetadataReject(id, requestId, status, field);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestId"></param>
        /// <param name="response"></param>
        public void SendDataHistoryResponse(long id, string requestId, LrpDataHistoryResponse response)
        {
            this.proxy.SendDataHistoryResponse(id, requestId, response);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestId"></param>
        /// <param name="rejectType"></param>
        /// <param name="rejectReason"></param>
        public void SendDataHistoryReject(long id, string requestId, LrpMarketHistoryRejectType rejectType, string rejectReason)
        {
            this.proxy.SendDataHistoryReject(id, requestId, rejectType, rejectReason);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestId"></param>
        /// <param name="chunk"></param>
        public void SendFileChunk(long id, string requestId, LrpFileChunk chunk)
        {
            this.proxy.SendFileChunk(id, requestId, chunk);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="notification"></param>
        public void SendNotification(long id, LrpNotification notification)
        {
            this.proxy.SendNotification(id, notification);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="notification"></param>
        public void SendReject(long id, string rejectReason, string rejectTag)
        {
            this.proxy.SendReject(id, rejectReason, rejectTag);
        }

        #endregion

        #region Properties

        /// <summary>
        ///
        /// </summary>
        public ILrpServerHandler Handler
        {
            get
            {
                return this.handler;
            }
        }

        #endregion

        #region Members

        readonly ILrpServerHandler handler;
        readonly GCHandle handle;
        LocalServerProxy proxy;

        #endregion
    }
}
