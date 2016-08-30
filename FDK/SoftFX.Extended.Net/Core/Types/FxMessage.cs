namespace SoftFX.Extended.Core
{
    using System;
    using SoftFX.Extended.Data;
    using SoftFX.Extended.Reports;
    using SoftFX.Lrp;

    class FxMessage
    {
        #region Properties

        public int Type { get; set; }
        public DateTime? SendingTime { get; set; }
        public DateTime? ReceivingTime { get; set; }
        public LPtr Data { get; set; }

        #endregion

        #region Methods

        public TwoFactorAuth TwoFactorAuth()
        {
            return Native.Converter.TwoFactorAuthFromHandle(this.Data);
        }

        public SessionInfo SessionInfo()
        {
            return Native.Converter.SessionInfoFromHandle(this.Data);
        }

        public CurrencyInfo[] Currencies()
        {
            return Native.Converter.CurrenciesFromHandle(this.Data);
        }

        public SymbolInfo[] Symbols(FixProtocolVersion protocolVersion)
        {
            var symbols = Native.Converter.SymbolsFromHandle(this.Data);

            foreach (var symbol in symbols)
            {
                symbol.ProtocolVersion = protocolVersion;
            }

            return symbols;
        }

        public Notification Notification()
        {
            return Native.Converter.NotificationFromHandle(this.Data);
        }

        public Quote Quote()
        {
            return Native.Converter.QuoteFromHandle(this.Data);
        }

        public string ProtocolVersion()
        {
            return Native.Converter.ProtocolVersionFromHandle(this.Data);
        }

        public AccountInfo AccountInfo()
        {
            return Native.Converter.AccountInfoFromHandle(this.Data);
        }

        public Position Position()
        {
            return Native.Converter.PositionFromHandle(this.Data);
        }

        public TradeTransactionReport TradeTransactionReport()
        {
            return Native.Converter.TradeTransactionReportFromHandle(this.Data);
        }

        public void GetLogoutInfo(out string text, out LogoutReason reason, out int code)
        {
            Native.Converter.GetLogoutInfoFromHandle(this.Data, out text, out reason, out code);
        }

        public ExecutionReport ExecutionReport()
        {
            return Native.Converter.ExecutionReportFromHandle(this.Data);
        }

        #endregion
    }
}
