namespace SoftFX.AutomaticTrading.Hosting.DataSources
{
    using System;

    public interface ITypedDataMapper : IDataMapper
    {
        Type ValueType { get; }

        Type ResultType { get; }
    }
}
