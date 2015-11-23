namespace SoftFX.Extended.Reports
{
    using SoftFX.Extended.Core;
    using SoftFX.Lrp;

    class TradeTransactionReportsIterator : StreamIterator<TradeTransactionReport>
    {
        public TradeTransactionReportsIterator(DataClient dataClient, LPtr handleIterator)
            : base(dataClient, handleIterator)
        {
        }

        internal override TradeTransactionReport ItemFromPointer(LPtr handle)
        {
            return Native.Iterator.GetTradeTransactionReport(handle);
        }
    }
}
