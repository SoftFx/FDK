using System;

namespace LrpServer.Net
{
    /// <summary>
    ///
    /// </summary>
    public interface ILrpServerHandler
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="address"></param>
        /// <param name="port"></param>
        void BeginNewConnectionRequest(long id, string address, int port);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        void BeginShutdownConnectionNotification(long id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="address"></param>
        /// <param name="port"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        void BeginLogonRequest(long id, string address, int port, string username, string password);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        void BeginLogoutRequest(long id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reason"></param>
        /// <param name="otp"></param>
        void BeginTwoFactorAuthResponse(long id, LrpTwoFactorReason reason, string otp);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestId"></param>
        void BeginCurrenciesInfoRequest(long id, string requestId);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestId"></param>
        void BeginSymbolsInfoRequest(long id, string requestId);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestId"></param>
        void BeginSessionInfoRequest(long id, string requestId);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestId"></param>
        /// <param name="symbols"></param>
        /// <param name="marketDepth"></param>
        void BeginSubscribeToQuotesRequest(long id, string requestId, string[] symbols, int marketDepth);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestId"></param>
        /// <param name="symbols"></param>
        void BeginUnsubscribeQuotesRequest(long id, string requestId, string[] symbols);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestId"></param>
        /// <param name="clientVersion"></param>
        void BeginComponentsInfoRequest(long id, string requestId, int clientVersion);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestId"></param>
        /// <param name="request"></param>
        void BeginDataHistoryRequest(long id, string requestId, LrpDataHistoryRequest request);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestId"></param>
        /// <param name="fileId"></param>
        /// <param name="chunkId"></param>
        void BeginFileChunkRequest(long id, string requestId, string fileId, uint chunkId);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestId"></param>
        /// <param name="symbol"></param>
        /// <param name="priceType"></param>
        /// <param name="period"></param>
        void BeginBarsHistoryMetaInfoFileRequest(long id, string requestId, string symbol, int priceType, string period);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestId"></param>
        /// <param name="symbol"></param>
        /// <param name="includeLevel2"></param>
        void BeginQuotesHistoryMetaInfoRequest(long id, string requestId, string symbol, bool includeLevel2);
    }
}
