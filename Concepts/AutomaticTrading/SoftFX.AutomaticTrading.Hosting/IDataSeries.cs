namespace SoftFX.AutomaticTrading.Hosting
{
    public interface IDataSeries
    {
        string Name { get; }

        int Count { get; }

        double this[int index] { get; }
    }
}
