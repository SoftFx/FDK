namespace SoftFX.AutomaticTrading.Hosting.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SoftFX.AutomaticTrading.Core.Indicators;
    using SoftFX.AutomaticTrading.Hosting.Indicators;
    
    sealed class CommonIndicatorBinding : IndicatorBinding
    {
        readonly Type valueType;
        readonly IEnumerable<IndicatorParameter> parameters;
        readonly Func<IHostContext, IDictionary<string, object>, IIndicator> factory;

        public CommonIndicatorBinding(
            string name,
            string description,
            string company,
            string category,
            Type valueType,
            IEnumerable<IndicatorParameter> parameters,
            Func<IHostContext, IDictionary<string, object>, IIndicator> factory
            )
            : base(name, description, company, category)
        {
            if (valueType == null)
                throw new ArgumentNullException("valueType");

            if (factory == null)
                throw new ArgumentNullException("factory");

            this.parameters = parameters ?? Enumerable.Empty<IndicatorParameter>();
            this.valueType = valueType;
            this.factory = factory;
        }

        public override IEnumerable<IndicatorParameter> Parameters
        {
            get { return this.parameters; }
        }

        protected override Type ValueType
        {
            get { return this.valueType; }
        }

        public override IIndicator CreateIndicator(IHostContext context, IDictionary<string, object> parameters)
        {
            return this.factory(context, parameters);
        }
    }
}
