using System.Reflection;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes.ReferenceParameterTracking
{
    public class FunctionExtractingRefsInSoftFxApis : SemanticAnalysisFixBase
    {
        public override void Perform(ParseNode node)
        {
            var typeToBeReflected = typeof(MqlAdapter);
            var methods = typeToBeReflected.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
            methods.Each(
                MapApiReference
                );
            methods = typeToBeReflected.GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
            methods.Each(
                MapApiReference
                );
        }

        static void MapApiReference(MethodInfo method)
        {
            var functionName = method.Name;
            var parameters = method.GetParameters();
            var definition = FunctionReferenceData.DefineFunction(functionName, parameters.Length);
            parameters.EachWithIndex(
                (param, id) =>
                    {
                        ParameterKind parameterKind;
                        if (param.ParameterType.IsByRef)
                        {
                            parameterKind = ParameterKind.Ref;
                        }
                        else parameterKind = ParameterKind.None;
                        definition[id] = parameterKind;
                    });
        }
    }
}