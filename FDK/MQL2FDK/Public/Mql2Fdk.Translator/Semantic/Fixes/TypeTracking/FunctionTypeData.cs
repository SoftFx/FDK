using System.Collections.Generic;

namespace Mql2Fdk.Translator.Semantic.Fixes.TypeTracking
{
    class FunctionTypeData
    {
        readonly Dictionary<string, TypeParameterTable> _functionBools =
            new Dictionary<string, TypeParameterTable>();

        readonly Dictionary<string, TypeData> _globalVariables =
            new Dictionary<string, TypeData>();

        public static TypeParameterTable DefineFunction(string functionName, int countParams)
        {
            var result = new TypeParameterTable(countParams);
            Instance._functionBools[functionName] = result;
            return result;
        }

        public static TypeData DefineGlobalVariable(string variableName)
        {
            var result = new TypeData();
            Instance._globalVariables[variableName] = result;
            return result;
        }

        public static bool HasFunction(string functionName)
        {
            return Instance._functionBools.ContainsKey(functionName);
        }

        public static bool HasGlobalVariable(string functionName)
        {
            return Instance._globalVariables.ContainsKey(functionName);
        }

        static FunctionTypeData()
        {
            Instance = new FunctionTypeData();
        }

        public static FunctionTypeData Instance { get; private set; }

        public static void Clear()
        {
            Instance._functionBools.Clear();
            Instance._globalVariables.Clear();
        }

        public static TypeParameterTable GetFunctionData(string functionName)
        {
            TypeParameterTable result;
            return Instance._functionBools.TryGetValue(functionName, out result)
                ? result 
                : null;
        }

        public static TypeData GetGlobalVariable(string functionName)
        {
            return Instance._globalVariables[functionName];
        }
    }
}