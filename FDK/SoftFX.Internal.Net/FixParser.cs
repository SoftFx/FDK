namespace SoftFX.Internal
{
    using System;
    using SoftFX.Extended;
    using SoftFX.Lrp;

    public class FixParser : IDisposable
    {
        public FixParser(string bridgeCommonDllPath, string fixDictionaryPath)
        {
            this.client = new LocalClient(bridgeCommonDllPath, Generated.BridgeCommon.Signature.Value);
            this.parser = new Generated.BridgeCommon.FixParser(this.client);
            this.handle = parser.Create(fixDictionaryPath);
        }

        public FixParsingResult Parse(string message)
        {
            this.symbols = null;
            this.sessions = null;
            return parser.Parse(this.handle, message);
        }

        public void Dispose()
        {
            this.parser.Delete(handle);
            this.handle = LPtr.Zero;
            this.parser = null;
            this.client.Dispose();
            this.client = null;
        }

        /// <summary>
        /// List of all symbols.
        /// </summary>
        public string[] Symbols
        {
            get
            {
                return this.symbols ?? (this.symbols = this.parser.GetSymbols(handle));
            }
        }

        /// <summary>
        /// List of all active sessions.
        /// </summary>
        public FixSessionId[] Sessions
        {
            get
            {
                return this.sessions ?? (this.sessions = this.parser.GetSessions(handle));
            }
        }

        public bool TryGetQuote(string symbol, FixSessionId sessionId, out Quote quote)
        {
            return this.parser.TryGetQuote(this.handle, symbol, sessionId, out quote);
        }

        public Quote GetQuote(string symbol, FixSessionId sessionId)
        {
            Quote result = null;

            if (!this.TryGetQuote(symbol, sessionId, out result))
            {
                var message = string.Format("Quotes is not presented for symbol = {0} and session id = {1}", symbol, sessionId);
                throw new ArgumentException(message);
            }

            return result;
        }

        #region Members

        LocalClient client;
        LPtr handle;
        Generated.BridgeCommon.FixParser parser;

        string[] symbols;
        FixSessionId[] sessions;

        #endregion
    }
}
