namespace Lrp.Configuration
{
    using System.Collections.Generic;

    public class Aliases
    {
        public static string ValueFromAliase(string aliase)
        {
            string result;

            if (Instance.aliaseToValue.TryGetValue(aliase, out result))
                return result;

            return aliase;
        }

        Aliases()
        {
            this.aliaseToValue["c#"] = "csharp";
            this.aliaseToValue["C#"] = "csharp";
            this.aliaseToValue["c++"] = "cpp";
            this.aliaseToValue["C++"] = "cpp";
        }

        #region Fields

        readonly IDictionary<string, string> aliaseToValue = new Dictionary<string, string>();
        static readonly Aliases Instance = new Aliases();

        #endregion
    }
}
