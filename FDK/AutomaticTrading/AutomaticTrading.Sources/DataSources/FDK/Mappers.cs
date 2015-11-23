namespace AutomaticTrading.Sources.DataSources
{
    using System;
    using SoftFX.AutomaticTrading.Hosting.DataSources;
    using SoftFX.Extended;

    static class Mappers
    {
        public static readonly ITypedDataMapper Open = new BarMapper("Open", o => o.Open);
        public static readonly ITypedDataMapper Close = new BarMapper("Close", o => o.Close);
        public static readonly ITypedDataMapper High = new BarMapper("High", o => o.High);
        public static readonly ITypedDataMapper Low = new BarMapper("Low", o => o.Low);

        sealed class BarMapper : ITypedDataMapper
        {
            readonly Converter<Bar, double> converter;

            public BarMapper(string name, Converter<Bar, double> converter)
            {
                if (converter == null)
                    throw new ArgumentNullException("converter");

                this.Name = name;
                this.converter = converter;
            }

            public Type ValueType
            {
                get { return typeof(Bar); }
            }

            public Type ResultType
            {
                get { return typeof(double); }
            }

            public string Name { get; private set; }

            public object Map(object value)
            {
                return this.converter((Bar)value);
            }
        }
    }
}
