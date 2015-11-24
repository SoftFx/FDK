namespace SoftFX.AutomaticTrading.Hosting.Indicators
{
    using System;
    using System.Collections.Generic;

    public static class IndicatorParameters
    {
        public static T Get<T>(this IDictionary<string, object> parameters, string name, T defaultValue)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            object value;
            if (parameters.TryGetValue(name, out value))
                return (T)Convert.ChangeType(value, typeof(T));

            return defaultValue;
        }

        public static T Get<T>(this IDictionary<string, object> parameters, string name)
        {
            return Get(parameters, name, default(T));
        }
    }
}
