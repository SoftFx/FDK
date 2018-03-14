namespace SoftFX.Extended.Reports
{
    using SoftFX.Extended.Core;
    using SoftFX.Lrp;

    class TradeTransactionReportsIterator : StreamIterator<TradeTransactionReport>
    {
        public TradeTransactionReportsIterator(DataClient dataClient, LPtr handleIterator)
            : base(StreamIteratorType.TradeHistory, dataClient, handleIterator)
        {
        }

        internal override TradeTransactionReport ItemFromPointer(LPtr handle)
        {
            return Native.TradeHistoryIterator.GetTradeTransactionReport(handle);
        }
    }
}
