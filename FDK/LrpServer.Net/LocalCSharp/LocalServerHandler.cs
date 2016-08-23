namespace LrpServer.Net.LocalCSharp
{
    using System;
    using System.Runtime.InteropServices;
    using LrpServer.Net.Threading;
    using SoftFX.Lrp;

    class LocalServerHandler
    {
        public void BeginNewConnectionRequest(LPtr handle, long id, string address, int port)
        {
            var acceptor = AcceptorFromHandle(handle);
            TaskEx.Start(acceptor.Handler.BeginNewConnectionRequest, id, address, port);
        }

        public void BeginShutdownConnectionNotification(LPtr handle, long id)
        {
            var acceptor = AcceptorFromHandle(handle);
            TaskEx.Start(acceptor.Handler.BeginShutdownConnectionNotification, id);
        }

        public void BeginLogonRequest(LPtr handle, long id, string address, int port, string username, string password)
        {
            var acceptor = AcceptorFromHandle(handle);
            TaskEx.Start(acceptor.Handler.BeginLogonRequest, id, address, port, username, password);
        }

        public void BeginLogoutRequest(LPtr handle, long id)
        {
            var acceptor = AcceptorFromHandle(handle);
            TaskEx.Start(acceptor.Handler.BeginLogoutRequest, id);
        }

        public void BeginTwoFactorAuthRequest(LPtr handle, long id, LrpTwoFactorReason reason, string otp)
        {
            var acceptor = AcceptorFromHandle(handle);
            TaskEx.Start(acceptor.Handler.BeginTwoFactorAuthRequest, id, reason, otp);
        }

        public void BeginCurrenciesInfoRequest(LPtr handle, long id, string requestId)
        {
            var acceptor = AcceptorFromHandle(handle);
            TaskEx.Start(acceptor.Handler.BeginCurrenciesInfoRequest, id, requestId);
        }

        public void BeginSymbolsInfoRequest(LPtr handle, long id, string requestId)
        {
            var acceptor = AcceptorFromHandle(handle);
            TaskEx.Start(acceptor.Handler.BeginSymbolsInfoRequest, id, requestId);
        }

        public void BeginSessionInfoRequest(LPtr handle, long id, string requestId)
        {
            var acceptor = AcceptorFromHandle(handle);
            TaskEx.Start(acceptor.Handler.BeginSessionInfoRequest, id, requestId);
        }

        public void BeginSubscribeToQuotesRequest(LPtr handle, long id, string requestId, string[] symbols, int depth)
        {
            var acceptor = AcceptorFromHandle(handle);
            TaskEx.Start(acceptor.Handler.BeginSubscribeToQuotesRequest, id, requestId, symbols, depth);
        }

        public void BeginUnsubscribeQuotesRequest(LPtr handle, long id, string requestId, string[] symbols)
        {
            var acceptor = AcceptorFromHandle(handle);
            TaskEx.Start(acceptor.Handler.BeginUnsubscribeQuotesRequest, id, requestId, symbols);
        }

        public void BeginComponentsInfoRequest(LPtr handle, long id, string requestId, int clientVersion)
        {
            var acceptor = AcceptorFromHandle(handle);
            TaskEx.Start(acceptor.Handler.BeginComponentsInfoRequest, id, requestId, clientVersion);
        }

        public void BeginDataHistoryRequest(LPtr handle, long id, string requestId, LrpDataHistoryRequest request)
        {
            var acceptor = AcceptorFromHandle(handle);
            TaskEx.Start(acceptor.Handler.BeginDataHistoryRequest, id, requestId, request);
        }

        public void BeginFileChunkRequest(LPtr handle, long id, string requestId, string fileId, uint chunkId)
        {
            var acceptor = AcceptorFromHandle(handle);
            TaskEx.Start(acceptor.Handler.BeginFileChunkRequest, id, requestId, fileId, chunkId);
        }

        public void BeginBarsHistoryMetaInfoFileRequest(LPtr handle, long id, string requestId, string symbol, int priceType, string period)
        {
            var acceptor = AcceptorFromHandle(handle);
            TaskEx.Start(acceptor.Handler.BeginBarsHistoryMetaInfoFileRequest, id, requestId, symbol, priceType, period);
        }

        public void BeginQuotesHistoryMetaInfoFileRequest(LPtr handle, long id, string requestId, string symbol, bool includeLevel2)
        {
            var acceptor = AcceptorFromHandle(handle);
            TaskEx.Start(acceptor.Handler.BeginQuotesHistoryMetaInfoRequest, id, requestId, symbol, includeLevel2);
        }

        static LrpAcceptor AcceptorFromHandle(LPtr handle)
        {
            var ptr = new IntPtr(handle.ToInt64());
            var gc = GCHandle.FromIntPtr(ptr);
            var result = (LrpAcceptor)gc.Target;
            return result;
        }
    }
}
