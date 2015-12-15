using System.Collections.Generic;
using System.Linq;

namespace Mql2Fdk.Translator.Semantic.Fixes.ReferenceParameterTracking
{
    class FunctionReferenceData
    {
        readonly Dictionary<string, ReferenceParamaterTable> _functionBools =
            new Dictionary<string, ReferenceParamaterTable>();

        public static ReferenceParamaterTable DefineFunction(string functionName, int countParams)
        {
            var result = new ReferenceParamaterTable(countParams);
            Instance._functionBools[functionName] = result;
            return result;
        }

        public static bool HasFunction(string functionName)
        {
            return Instance._functionBools.ContainsKey(functionName);
        }

        FunctionReferenceData()
        {
        }

        static FunctionReferenceData()
        {
            Instance = new FunctionReferenceData();
        }

        public static FunctionReferenceData Instance { get; private set; }

        public static ReferenceParamaterTable GetReferenceData(string functionName)
        {
            return Instance._functionBools[functionName];
        }

        public static bool FunctionHasRefArguments(string functionName)
        {
            if (!HasFunction(functionName))
                return false;
            var functionData = Instance._functionBools[functionName];
            return functionData.RefBools.Any(item => item != ParameterKind.None);
        }
    }
}