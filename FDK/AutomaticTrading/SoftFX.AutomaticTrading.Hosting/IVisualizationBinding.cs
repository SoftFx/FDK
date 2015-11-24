namespace SoftFX.AutomaticTrading.Hosting
{
    using System.Collections.Generic;

    public interface IVisualizationBinding
    {
        IEnumerable<IDataSeries> Data { get; }
    }
}
