namespace SoftFX.AutomaticTrading.Hosting.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using SoftFX.AutomaticTrading.Core.Indicators;
    using SoftFX.AutomaticTrading.Hosting.Indicators;

    public class IndicatorBindingFactory
    {
        readonly Lazy<IIndicatorBinding> binding;
        readonly Type indicatorType;

        public IndicatorBindingFactory(Type indicatorType)
        {
            if (indicatorType == null)
                throw new ArgumentNullException("indicatorType");

            this.indicatorType = indicatorType;
            this.binding = new Lazy<IIndicatorBinding>(this.CreateBinding);
        }

        public IIndicatorBinding GetBinding()
        {
            return this.binding.Value;
        }

        IIndicatorBinding CreateBinding()
        {
            var indicator = this.indicatorType
                                .GetInterfaces()
                                .SingleOrDefault(o => o.IsGenericType && !o.IsGenericTypeDefinition && o.GetGenericTypeDefinition() == typeof(IIndicator<,>));

            if (indicator == null)
                throw new InvalidOperationException("Type must implement IIndicator<TInput, TResult> interface.");

            var valueType = indicator.GenericTypeArguments[0];

            var nameAttribute = this.indicatorType.GetCustomAttribute<DisplayNameAttribute>(inherit: false);
            var descriptionAttribute = this.indicatorType.GetCustomAttribute<DescriptionAttribute>(inherit: false);
            var companyAttribute = this.indicatorType.GetCustomAttribute<CompanyAttribute>(inherit: false);
            var categoryAttribute = this.indicatorType.GetCustomAttribute<CategoryAttribute>(inherit: false);

            var parameters = this.indicatorType
                                 .GetCustomAttributes<IndicatorParameterAttribute>()
                                 .Select(o => IndicatorParameter.Create(o.Name, o.Type))
                                 .ToArray();

            return new CommonIndicatorBinding(
                nameAttribute != null ? nameAttribute.DisplayName : string.Empty,
                descriptionAttribute != null ? descriptionAttribute.Description : string.Empty,
                companyAttribute != null ? companyAttribute.Company : string.Empty,
                categoryAttribute != null ? categoryAttribute.Category : string.Empty,
                valueType,
                parameters,
                this.CreateIndicator
                );
        }

        IIndicator CreateIndicator(IHostContext context, IDictionary<string, object> parameters)
        {
            IIndicator indicator;

            try
            {
                indicator = (IIndicator)Activator.CreateInstance(this.indicatorType, context, parameters);
            }
            catch (MissingMethodException)
            {
                indicator = (IIndicator)Activator.CreateInstance(this.indicatorType);
            }

            return indicator;
        }
    }
}
