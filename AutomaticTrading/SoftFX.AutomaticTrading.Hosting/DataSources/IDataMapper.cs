namespace SoftFX.AutomaticTrading.Hosting.DataSources
{
    public interface IDataMapper
    {
        string Name { get; }

        object Map(object value);
    }
}
