namespace SoftFX.AutomaticTrading.Hosting.Indicators
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using SoftFX.AutomaticTrading.Core.Indicators;

    public abstract class IndicatorBinding : IIndicatorBinding
    {
        readonly string name;
        readonly string description;
        readonly string company;
        readonly string category;

        protected IndicatorBinding(string name)
            : this(name, string.Empty, string.Empty, string.Empty)
        {
        }

        protected IndicatorBinding(string name, string description, string company, string category)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Empty or null name is not allowed.", "name");

            this.name = name;
            this.description = description ?? string.Empty;
            this.company = company ?? string.Empty;
            this.category = category ?? string.Empty;
        }

        public string Name
        {
            get { return this.name; }
        }

        public string Descrption
        {
            get { return this.description; }
        }

        public string Company
        {
            get { return this.company; }
        }

        public string Category
        {
            get { return this.category; }
        }

        public abstract IEnumerable<IndicatorParameter> Parameters { get; }

        public abstract IIndicator CreateIndicator(IHostContext context, IDictionary<string, object> parameters);

        public virtual bool IsTypeSupported(Type type)
        {
            var converter = TypeDescriptor.GetConverter(type);
            return converter.CanConvertTo(this.ValueType);
        }

        protected abstract Type ValueType { get; }
    }
}
