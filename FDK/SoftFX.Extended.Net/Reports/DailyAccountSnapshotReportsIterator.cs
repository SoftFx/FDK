namespace SoftFX.Extended.Reports
{
    using SoftFX.Extended.Core;
    using SoftFX.Lrp;

    class DailyAccountSnapshotReportsIterator : StreamIterator<DailyAccountSnapshotReport>
    {
        public DailyAccountSnapshotReportsIterator(DataClient dataClient, LPtr handleIterator)
            : base(dataClient, handleIterator)
        {
        }

        internal override DailyAccountSnapshotReport ItemFromPointer(LPtr handle)
        {
            return Native.DailySnapshotsIterator.GetDailyAccountSnapshotReport(handle);
        }
    }
}
